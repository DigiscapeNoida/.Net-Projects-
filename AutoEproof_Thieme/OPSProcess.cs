using System;
using System.Threading;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Data.SqlTypes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using ProcessNotification;
using System.Text;
using DatabaseLayer;
using MsgRcvr;
using iTextSharp.text.pdf;

namespace AutoEproof
{
    class OPSProcess : MessageEventArgs
    {
        string connString = string.Empty;
        eProofArticleInfo PdfProcessOBJ = null;
        MNTInfo MNT = null;

        public OPSProcess(MNTInfo MNT, InputFiles _InputFiles)
        {
            this.MNT = MNT;
            PdfProcessOBJ = new eProofArticleInfo(MNT, _InputFiles);

        }

        public void TestStart()
        {

            string ArticleCategory = string.Empty;
            ArticleCategory = GetArticleCategory().Trim();
            PdfProcessOBJ.StartValidation();
            PreProcessOnPDF();
            MoveToOPSServer(PdfProcessOBJ);
        }

        public void Start()
        {

            bool Result = false;
            ProcessEventHandler("OPS Validation start");

            PdfProcessOBJ.StartValidation();

            ProcessEventHandler("OPS Validation finished");

            if (PdfProcessOBJ.ValidationResult)
            {
                ProcessEventHandler("PreProcessOnPDF Start");

                if (PreProcessOnPDF())
                {
                    if (MoveToOPSServer(PdfProcessOBJ))
                    {
                        ProcessEventHandler("Checking: InsertReviewDetail");
                        if (InsertReviewDetail())
                        {
                            ProcessEventHandler("Checking In: InsertReviewDetail");
                            if (MailSend())
                            {
                                Result = true;
                            }
                            else
                            {
                                DeleteReviewDetails();
                                ProcessEventHandler("MailSend Failed");
                            }
                        }
                        else
                        {
                            ProcessEventHandler("Checking Out: InsertReviewDetail");
                            ProcessEventHandler("InsertReviewDetail Failed");
                        }
                    }
                    else
                    {
                        ProcessEventHandler("MoveToOPSServer Failed");
                    }
                }
                else
                {
                    ProcessEventHandler("PreProcessOnPDF Failed");
                }

            }
            else
            {
                ProcessEventHandler("OPS Validation is failed");
            }


            if (PdfProcessOBJ.ValidationResult && Result == false)
            {
                ProcessEventHandler("Remove review detail in case of error..");
                //@JID as varchar(50) ,@AID as varchar(50),@CLNT
                //[usp_DeleteArticleReviewDetail]
                IsPdfProcessError = true;
                PdfProcessOBJ.ValidationResult = false;

                try
                {
                    SqlParameter[] para = new SqlParameter[3];
                    para[0] = new SqlParameter("@JID", MNT.JID);
                    para[1] = new SqlParameter("@AID", MNT.AID);
                    para[2] = new SqlParameter("@CLNT", MNT.Client);

                    string OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
                    SqlHelper.ExecuteNonQuery(OPSConStr, System.Data.CommandType.StoredProcedure, "usp_DeleteArticleReviewDetail", para);
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                }

            }
            if (IsPdfProcessError)
                PdfProcessOBJ.ValidationResult = false;

            eProofResultNotification.InternalMail(PdfProcessOBJ);

            //if (PdfProcessOBJ.IsAlreadyProcessed)
            //    ProcessEventHandler(MNT.JID + MNT.AID + " Article is Already Processed. No need to send email.");
            //else
            //    eProofResultNotification.InternalMail(PdfProcessOBJ);
        }

        private bool InsertReviewDetail()
        {

            ProcessEventHandler("InsertReviewDetail");
            connString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(connString))
            {
                ProcessEventHandler("InsertReviewDetail: Connection Successful");
                using (SqlCommand InsertCommand = Con.CreateCommand())
                {
                    ProcessEventHandler("InsertReviewDetail: Create Command");
                    InsertCommand.CommandText = "InsertReviewDetail";
                    InsertCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter _JID = new SqlParameter("@JID", SqlDbType.VarChar, 30);
                    _JID.Value = PdfProcessOBJ.JID;
                    InsertCommand.Parameters.Add(_JID);

                    SqlParameter _AID = new SqlParameter("@AID", SqlDbType.VarChar, 30);
                    _AID.Value = PdfProcessOBJ.AID;
                    InsertCommand.Parameters.Add(_AID);


                    SqlParameter _Client = new SqlParameter("@Client", SqlDbType.NVarChar);
                    _Client.Value = PdfProcessOBJ.Client;
                    InsertCommand.Parameters.Add(_Client);

                    SqlParameter _QueryString = new SqlParameter("@QueryID", SqlDbType.VarChar);
                    _QueryString.Value = PdfProcessOBJ.EncryptedName;
                    InsertCommand.Parameters.Add(_QueryString);

                    SqlParameter _URL = new SqlParameter("@URL", SqlDbType.VarChar);
                    _URL.Value = PdfProcessOBJ.UploadPath;
                    InsertCommand.Parameters.Add(_URL);


                    SqlParameter _FileName = new SqlParameter("@FileName", SqlDbType.VarChar);
                    _FileName.Value = PdfProcessOBJ.ReviewPDFName;
                    InsertCommand.Parameters.Add(_FileName);

                    SqlParameter _FileSize = new SqlParameter("@FileSize", SqlDbType.Int);
                    _FileSize.Value = PdfProcessOBJ.PdfSize;
                    InsertCommand.Parameters.Add(_FileSize);

                    SqlParameter _ArticleTitle = new SqlParameter("@ArticleTitle", SqlDbType.NVarChar);


                    if (!string.IsNullOrEmpty(PdfProcessOBJ.ArticleTitle))
                        _ArticleTitle.Value = PdfProcessOBJ.ArticleTitle;

                    InsertCommand.Parameters.Add(_ArticleTitle);
                    InsertCommand.Parameters.Add(new SqlParameter("@RID", SqlDbType.Int, 4));
                    InsertCommand.Parameters["@RID"].Direction = ParameterDirection.Output;

                    ////Insert the detail for intially common reviewers
                    Con.Open();

                    try
                    {
                        ProcessEventHandler("Inserting Author Info");
                        InsertCommand.ExecuteNonQuery();
                    }
                    catch (SqlException Ex)
                    {
                        ProcessEventHandler("Error occured while inserting author info");
                        ProcessErrorHandler(Ex);

                        return false;
                    }
                    finally
                    {
                        Con.Close();

                    }

                    string QID = "";
                    int RID = (int)InsertCommand.Parameters["@RID"].Value;

                    ////////////////Insert Main Author detail in Database
                    if (PdfProcessOBJ.JID.Equals("IEAM"))
                    {
                        //ieam_editor@setac.org
                        //ieamprod@wiley.com	
                        //Editor,PE
                        PdfProcessOBJ.MainReviewerRole = "Editor";

                        //QID = PdfProcessOBJ.ReviewrEncryptedString("ieam_editor@setac.org".Split('@').GetValue(0).ToString());
                        QID = System.Guid.NewGuid().ToString();
                        PdfProcessOBJ.MainReviewerQID = QID;
                        ////Generate the URL for main Reviewer
                        PdfProcessOBJ.URI = "http://" + ConfigDetails.OPSServerIP + "/OPS/eProof.OPS?ID=" + QID;
                        AddReviewer(RID, "Editor", QID, "", "ieam_editor@setac.org", "InProgress", DateTime.Now);


                        //QID = PdfProcessOBJ.ReviewrEncryptedString(PdfProcessOBJ.AuthorEMail.Split('@').GetValue(0).ToString());
                        QID = System.Guid.NewGuid().ToString();
                        AddReviewer(RID, "Author", QID, PdfProcessOBJ.AuthorName, PdfProcessOBJ.AuthorEMail, null, DateTime.Now);

                        QID = System.Guid.NewGuid().ToString();
                        AddReviewer(RID, "PE", QID, "", PdfProcessOBJ.PEMail, null, DateTime.Now);

                        PdfProcessOBJ.AuthorEMail = "ieam_editor@setac.org";

                    }
                    else if (PdfProcessOBJ.JID.Equals("MIM"))
                    {
                        ProcessEventHandler("InsertReviewDetail: for MIM");
                        string ArticleCategory = string.Empty;
                        string EditorEmailID = string.Empty;

                        ArticleCategory = GetArticleCategory().Trim();

                        ProcessEventHandler("InsertReviewDetail: for MIMTEST" + ArticleCategory);

                        if (string.IsNullOrEmpty(ArticleCategory))
                            return false;

                        ProcessEventHandler("InsertReviewerDetail: for MIM");
                      
                        ProcessEventHandler("InsertReviewDetail: " + ArticleCategory);

                        if (ArticleCategory.Equals("Bacteriology", StringComparison.OrdinalIgnoreCase))
                            EditorEmailID = "terao@dent.niigata-u.ac.jp";
                        else if (ArticleCategory.Equals("Virology", StringComparison.OrdinalIgnoreCase))
                            EditorEmailID = "mtakeda@nih.go.jp";
                        else if (ArticleCategory.Equals("Immunology", StringComparison.OrdinalIgnoreCase))
                            EditorEmailID = "kawakami@med.tohoku.ac.jp";//remove on 06 Apr 2018 -> yoshikai@bioreg.kyushu-u.ac.jp,
                        else
                            return false;
                        ProcessEventHandler("InsertReviewerDetail: " + EditorEmailID);

                        QID = System.Guid.NewGuid().ToString();
                        PdfProcessOBJ.MainReviewerQID = QID;
                        PdfProcessOBJ.MainReviewerRole = "Author";
                        ////Generate the URL for main Reviewer
                        PdfProcessOBJ.URI = "http://" + ConfigDetails.OPSServerIP + "/OPS/eProof.OPS?ID=" + QID;
                        AddReviewer(RID, "Author", QID, PdfProcessOBJ.AuthorName, PdfProcessOBJ.AuthorEMail, "InProgress", DateTime.Now);

                        QID = System.Guid.NewGuid().ToString();
                        AddReviewer(RID, "Relevant Editor", QID, "", EditorEmailID, null, DateTime.Now);

                        QID = System.Guid.NewGuid().ToString();
                        AddReviewer(RID, "PE", QID, "", PdfProcessOBJ.ReviewerEmail, null, DateTime.Now);

                        ////OLD process  commented on 23/02/2018  `/////////////////////////////////////////////
                       
                        /////Dn't change sequence..
                        ////Relevant Editor > Editorial Assistant > Corresponding Author
                        //ProcessEventHandler("InsertReviewDetail: for MIM");
                        //string ArticleCategory = string.Empty;
                        //string EditorEmailID = string.Empty;
                        //string EditorialAssistantEmail = "mai@wiley.com";

                        //string PEEmail = "rchentes@wiley.com";

                        //ArticleCategory = GetArticleCategory().Trim();

                        //ProcessEventHandler("InsertReviewDetail: for MIMTEST" + ArticleCategory);

                        //if (string.IsNullOrEmpty(ArticleCategory))
                        //    return false;

                        //ProcessEventHandler("InsertReviewDetail: for MIM");

                        //ProcessEventHandler("InsertReviewDetail: " + ArticleCategory);

                        //if (ArticleCategory.Equals("Bacteriology", StringComparison.OrdinalIgnoreCase))
                        //    EditorEmailID = "a27k03n0@hirosaki-u.ac.jp";
                        //else if (ArticleCategory.Equals("Virology", StringComparison.OrdinalIgnoreCase))
                        //    EditorEmailID = "wakita@nih.go.jp";
                        //else if (ArticleCategory.Equals("Immunology", StringComparison.OrdinalIgnoreCase))
                        //    EditorEmailID = "kitada.yumiko.582@m.kyushu-u.ac.jp";  //yoshikai@bioreg.kyushu-u.ac.jp,
                        //else
                        //    return false;
                        //ProcessEventHandler("InsertReviewDetail: " + EditorEmailID);

                        //PdfProcessOBJ.MainReviewerRole = "Relevant Editor";

                        //// QID = PdfProcessOBJ.ReviewrEncryptedString(EditorEmailID.Split('@').GetValue(0).ToString());
                        //QID = System.Guid.NewGuid().ToString();
                        //PdfProcessOBJ.MainReviewerQID = QID;

                        //////Generate the URL for main Reviewer
                        //PdfProcessOBJ.URI = "http://" + ConfigDetails.OPSServerIP + "/OPS/eProof.OPS?ID=" + QID;
                        //AddReviewer(RID, "Relevant Editor", QID, "", EditorEmailID, "InProgress", DateTime.Now);

                        ////QID = PdfProcessOBJ.ReviewrEncryptedString(EditorialAssistantEmail.Split('@').GetValue(0).ToString());
                        //QID = System.Guid.NewGuid().ToString();
                        //AddReviewer(RID, "Editorial Assistant ", QID, "", EditorialAssistantEmail, null, DateTime.Now);

                        ////QID = PdfProcessOBJ.ReviewrEncryptedString(PdfProcessOBJ.AuthorEMail.Split('@').GetValue(0).ToString());
                        //QID = System.Guid.NewGuid().ToString();
                        //AddReviewer(RID, "Author", QID, PdfProcessOBJ.AuthorName, PdfProcessOBJ.AuthorEMail, null, DateTime.Now);

                        ////QID = PdfProcessOBJ.ReviewrEncryptedString(PEEmail.Split('@').GetValue(0).ToString());
                        //QID = System.Guid.NewGuid().ToString();
                        //AddReviewer(RID, "PE", QID, "", PEEmail, null, DateTime.Now);


                        //PdfProcessOBJ.AuthorEMail = EditorEmailID;

                    }
                    else
                    {

                        //QID = PdfProcessOBJ.ReviewrEncryptedString(PdfProcessOBJ.AuthorEMail.Split('@').GetValue(0).ToString());
                        QID = System.Guid.NewGuid().ToString();
                        PdfProcessOBJ.MainReviewerQID = QID;
                        ////Generate the URL for main Reviewer
                        PdfProcessOBJ.URI = "http://" + ConfigDetails.OPSServerIP + "/OPS/eProof.OPS?ID=" + QID;
                        PdfProcessOBJ.MainReviewerRole = "Author";
                        AddReviewer(RID, "Author", QID, PdfProcessOBJ.AuthorName, PdfProcessOBJ.AuthorEMail, "InProgress", DateTime.Now);

                        ProcessEventHandler("InsertOtherReviewer");

                        if (InsertOtherReviewer(RID) == false)
                        {

                            return false;
                        }
                    }

                }
            }
            return true;
        }
        private string GetArticleCategory()
        {

            connString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            OPSDB OPSDBObj = new OPSDB(connString);
            ProcessEventHandler("PdfProcessOBJ.Client:" + PdfProcessOBJ.Client + "--PdfProcessOBJ.JID:" + PdfProcessOBJ.JID + "--PdfProcessOBJ.AID:" + PdfProcessOBJ.AID);
            string Ctgry = OPSDBObj.GetArticleCategory(PdfProcessOBJ.Client, PdfProcessOBJ.JID, PdfProcessOBJ.AID);
            ProcessEventHandler("Category:" + Ctgry);
            return Ctgry;

        }
        private bool InsertOtherReviewer(int RID)
        {
            bool Rslt = false;
            string QID = "";
            ProcessEventHandler("Insert Other Reviewer Info");

            ////////////////Insert Other Reviewer detail in Database
            char[] SpliArr = { ',' };
            string[] ReviewerEmails = { "" };

            if (!string.IsNullOrEmpty(PdfProcessOBJ.ReviewerEmail))
            {
                ReviewerEmails = PdfProcessOBJ.ReviewerEmail.Split(SpliArr, StringSplitOptions.RemoveEmptyEntries);
                if (ReviewerEmails.Length > 0)
                {
                    string[] Roles = PdfProcessOBJ.Role.Split(SpliArr, StringSplitOptions.RemoveEmptyEntries);

                    if (Roles.Length == ReviewerEmails.Length)
                    {
                        int RoleCounter = 0;
                        foreach (string ReviewerEmail in ReviewerEmails)
                        {
                            ////Generate the query string for another reviewer
                            //if (ReviewerEmail.IndexOf('@') > 0)
                            //    QID = PdfProcessOBJ.ReviewrEncryptedString(ReviewerEmail.Split('@').GetValue(0).ToString());
                            //else
                            //    QID = PdfProcessOBJ.ReviewrEncryptedString("");
                            string editorforAJIM = string.Empty;
                            if (PdfProcessOBJ.JID.Equals("AJIM") && Roles[RoleCounter] == "Editor")
                            {
                                if (PdfProcessOBJ.EditorName.ToLower() == "john meyer")
                                    editorforAJIM = "jmeyer424@gmail.com,john.meyer@mssm.edu";
                                if (PdfProcessOBJ.EditorName.ToLower() == "rodney ehrlich")
                                    editorforAJIM = "rodney.ehrlich@uct.ac.za";
                            }


                            QID = System.Guid.NewGuid().ToString();
                            ////////////////Insert other Author detail in Database
                            if (PdfProcessOBJ.JID.Equals("AJIM") && Roles[RoleCounter] == "Editor")
                            {
                                if (AddReviewer(RID, Roles[RoleCounter], QID, "", editorforAJIM, null, DateTime.Now) == true)
                                {
                                    Rslt = true;
                                }
                            }
                            
                            ////////////////Insert other Author detail in Database
                            else if (AddReviewer(RID, Roles[RoleCounter], QID, "", ReviewerEmail.Replace(";", ","), null, DateTime.Now) == true)
                            {
                                Rslt = true;
                            }
                            RoleCounter++;
                        }
                    }
                    else
                    {
                        ProcessEventHandler("ReviewerEmails and Role does not match..");
                        Rslt = false;
                    }
                }
            }
            else
            {
                Rslt = true;
                ProcessEventHandler("No Reviwer to insert..");
            }

            return Rslt;
        }
        private bool AddReviewer(int RID, string Role, string QueryID, string ReviewerName, string ReviewerMailID, string ReviewStatus, DateTime ReviewStart)
        {
            bool Rslt = false;
            string ConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            using (SqlConnection Con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand InsertCommand = Con.CreateCommand())
                {
                    InsertCommand.CommandText = "InsertReviewerDetail";
                    InsertCommand.CommandType = CommandType.StoredProcedure;

                    SqlParameter _RID = new SqlParameter("@RID", SqlDbType.Int);
                    _RID.Value = RID;

                    InsertCommand.Parameters.Add(_RID);

                    SqlParameter _ReviewerMailID = new SqlParameter("@ReviewerMailID", SqlDbType.NVarChar);
                    _ReviewerMailID.Value = ReviewerMailID;

                    InsertCommand.Parameters.Add(_ReviewerMailID);

                    SqlParameter _QueryString = new SqlParameter("@QueryID", SqlDbType.VarChar);
                    _QueryString.Value = QueryID;
                    InsertCommand.Parameters.Add(_QueryString);


                    SqlParameter _ReviewerName = new SqlParameter("@ReviewerName", SqlDbType.VarChar);
                    _ReviewerName.Value = ReviewerName;
                    InsertCommand.Parameters.Add(_ReviewerName);


                    SqlParameter _ReviewStatus = new SqlParameter("@ReviewStatus", SqlDbType.NVarChar);

                    if (ReviewStatus == null)
                        _ReviewStatus.Value = DBNull.Value;
                    else
                        _ReviewStatus.Value = ReviewStatus;

                    InsertCommand.Parameters.Add(_ReviewStatus);



                    SqlParameter _ReviewStart = new SqlParameter("@ReviewStart", SqlDbType.DateTime);
                    if (ReviewStatus == null)
                    {
                        SqlDateTime NULLDT = SqlDateTime.Null;
                        _ReviewStart.Value = NULLDT;
                    }
                    else
                        _ReviewStart.Value = ReviewStart;

                    InsertCommand.Parameters.Add(_ReviewStart);

                    Con.Open();

                    SqlParameter _Role = new SqlParameter("@Role", SqlDbType.VarChar);
                    _Role.Value = Role;
                    InsertCommand.Parameters.Add(_Role);
                    try
                    {
                        InsertCommand.ExecuteNonQuery();
                        Rslt = true;
                        ProcessEventHandler("Other Reviewer Info Inserted Successfully");
                    }
                    catch (SqlException ex)
                    {
                        ProcessErrorHandler(ex);
                    }
                    finally
                    {
                        Con.Close();
                    }



                }
            }

            return Rslt;
        }
        private void DeleteReviewDetails()
        {
            connString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            OPSDB OPSDBObj = new OPSDB(connString);
            OPSDBObj.DeleteReviewDetails(PdfProcessOBJ.JID, PdfProcessOBJ.AID);
        }
        private bool PreProcessOnPDF()
        {

            try
            {
                string HowToAnnotateYourProof = ConfigDetails.HowToAnnotateYourProof;


                string JIDSpcfcFile = MNT.JID + "_" + Path.GetFileName(HowToAnnotateYourProof);
                ProcessEventHandler("JIDSpcfcFile  ==>" + JIDSpcfcFile);

                string JIDspcfcPath = Path.GetDirectoryName(HowToAnnotateYourProof) + "\\" + JIDSpcfcFile;
                ProcessEventHandler("JIDspcfcPath  ==>" + JIDspcfcPath);

                if (File.Exists(JIDspcfcPath))
                {
                    HowToAnnotateYourProof = JIDspcfcPath;
                }

                ProcessEventHandler("HowToAnnotateYourProof  ==>" + HowToAnnotateYourProof);
                string[] SrcFiles = new string[2];

                //Code comment for old process
                // PdfProcess.PDF PDFObj = new PdfProcess.PDF(PdfProcessOBJ.InputPDF, MNT);
                PdfProcess.PDF PDFObj = null;
                if (MNT.JID == "IEAM")
                {
                    if (File.Exists(PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf")) && File.Exists(PdfProcessOBJ.InputPDF.Replace(".pdf", "c.pdf")))
                    {
                        ProcessEventHandler("Q.pdf Exist  ==>" + PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf"));
                        PDFObj = new PdfProcess.PDF(PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf"), MNT);
                        SrcFiles[0] = PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf");
                        SrcFiles[1] = PdfProcessOBJ.InputPDF.Replace(".pdf", "c.pdf");
                        PDFObj.MergePDF(SrcFiles);
                        Thread.Sleep(5000);
                        SrcFiles[0] = PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf");
                        SrcFiles[1] = PdfProcessOBJ.InputPDF;
                        PDFObj.MergePDF(SrcFiles);
                        ProcessEventHandler("Q PDF Complete");
                    }

                    else if (File.Exists(PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf")))
                    {

                        PDFObj = new PdfProcess.PDF(PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf"), MNT);
                        SrcFiles[0] = PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf");

                        SrcFiles[1] = PdfProcessOBJ.InputPDF;
                        PDFObj.MergePDF(SrcFiles);
                        Thread.Sleep(5000);
                        ProcessEventHandler("Q PDF Complete");
                    }
                    else if (File.Exists(PdfProcessOBJ.InputPDF.Replace(".pdf", "c.pdf")))
                    {

                        PDFObj = new PdfProcess.PDF(PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf"), MNT);
                        SrcFiles[0] = PdfProcessOBJ.InputPDF.Replace(".pdf", "c.pdf");
                        SrcFiles[1] = PdfProcessOBJ.InputPDF;
                        PDFObj.MergePDF(SrcFiles);
                        Thread.Sleep(5000);
                        File.Move(PdfProcessOBJ.InputPDF.Replace(".pdf", "c.pdf"), PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf"));
                        Thread.Sleep(5000);
                        ProcessEventHandler("C PDF Complete");
                    }

                    else
                    {
                        PDFObj = new PdfProcess.PDF(PdfProcessOBJ.InputPDF, MNT);
                    }

                    try
                    {
                        ProcessEventHandler("Add Color Charge");
                        PDFObj.ADDColorChargePDF(ConfigDetails.OPSServerPath);
                        ProcessEventHandler("Add Color Charge Complete");
                    }
                    catch (Exception ex)
                    {
                        base.ProcessErrorHandler(ex);
                    }

                }
                else
                {
                    if (File.Exists(PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf")))
                    {
                        ProcessEventHandler("Q.pdf Exist  ==>" + PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf"));
                        PDFObj = new PdfProcess.PDF(PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf"), MNT);
                        SrcFiles[0] = PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf");
                        SrcFiles[1] = PdfProcessOBJ.InputPDF;
                        PDFObj.MergePDF(SrcFiles);
                        Thread.Sleep(1000);
                        ProcessEventHandler("Q PDF Complete");
                    }
                    else
                    {
                        PDFObj = new PdfProcess.PDF(PdfProcessOBJ.InputPDF, MNT);
                    }

                    try
                    {
                        if (MNT.JID != "MIM")
                        {
                            ProcessEventHandler("Add Color Charge");
                            PDFObj.ADDColorChargePDF(ConfigDetails.OPSServerPath);
                            ProcessEventHandler("Add Color Charge Complete");
                        }
                    }
                    catch (Exception ex)
                    {
                        base.ProcessErrorHandler(ex);
                    }
                    string GAbsPdf = PdfProcessOBJ.InputPDF.Replace(".pdf", "c.pdf");
                    if (File.Exists(GAbsPdf))
                    {
                        ProcessEventHandler("c PDF exist");
                        if (File.Exists(PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf")))
                            SrcFiles[0] = PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf");
                        else
                            SrcFiles[0] = PdfProcessOBJ.InputPDF;
                        SrcFiles[1] = GAbsPdf;
                        PDFObj.MergePDF(SrcFiles);
                        Thread.Sleep(2000);
                        ProcessEventHandler("Merge c PDF Complete");
                    }

                }
                //Comment on 28/07/2017 due to not functioning

                //if ("#WSB#".IndexOf("#" + MNT.JID + "#") != -1)
                //{
                //    string PageChargeForm = @"C:\AEPS\COPYRIGHT\JWUSA\WSB\wsb_pcf.pdf";
                //    if (File.Exists(PageChargeForm))
                //    {

                //        string CopyTo = Path.GetDirectoryName(PdfProcessOBJ.InputPDF) + "\\wsb_pcf.pdf";
                //        File.Copy(PageChargeForm, CopyTo);
                //        PDFFormProcess PDFFormProcessOBJ = new PDFFormProcess(CopyTo, PdfProcessOBJ.ArticleInfoOBJ);
                //        PDFFormProcessOBJ.ProcessOnWSBPDF();

                //        SrcFiles[0] = CopyTo;
                //        SrcFiles[1] = PdfProcessOBJ.InputPDF;
                //        PDFObj.MergePDF(SrcFiles);
                //    }
                //}

                //Code for new process

                //Code comment for old process
                SrcFiles[0] = HowToAnnotateYourProof;
                if (File.Exists(PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf")))
                    SrcFiles[1] = PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf");
                else
                    SrcFiles[1] = PdfProcessOBJ.InputPDF;



                if ("NUR#POI3".IndexOf(MNT.JID.ToUpper()) != -1)
                {
                }
                else
                {
                    PDFObj.AddWatermark();
                }

                PDFObj.MergePDF(SrcFiles);
                Thread.Sleep(5000);
                ProcessEventHandler("Merge Complete");

                //Code for new process
                if (File.Exists((PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf"))))
                {
                    File.Delete(PdfProcessOBJ.InputPDF);
                    File.Move(PdfProcessOBJ.InputPDF.Replace(".pdf", "Q.pdf"), PdfProcessOBJ.InputPDF.Replace(".pdf", "_Temp.pdf"));
                }
                else
                {
                    File.Move(PdfProcessOBJ.InputPDF, PdfProcessOBJ.InputPDF.Replace(".pdf", "_Temp.pdf"));
                }
                ProcessEventHandler("Query Linking Start");
                Thread.Sleep(10000);
                QueryLinking QL = new QueryLinking();
                if (QL.Start(PdfProcessOBJ.InputPDF.Replace(".pdf", "_Temp.pdf")))
                {
                    ProcessEventHandler("Query Linking Complete");
                    try
                    {
                        string[] args = new string[2];
                        args[0] = PdfProcessOBJ.InputPDF;
                        args[1] = "Review";
                        PDFAnnotation.Program.Main(args);
                        if (QL != null)
                            QL = null;
                       // return true;
                        ////////////
                        Thread.Sleep(1000);
                        ProcessEventHandler("PDF security Start");
                        if (CallJavaJarFile(PdfProcessOBJ.InputPDF.Replace(".pdf", "_review.pdf"), PdfProcessOBJ.InputPDF.Replace(".pdf", "_review1.pdf")))
                        {
                            ProcessEventHandler("PDF security END");
                            //args = new string[2];
                            //args[0] = PdfProcessOBJ.InputPDF.Replace(".pdf", "_review1.pdf");
                            //args[1] = "SaveACopy";
                            //PDFAnnotation.Program.Main(args);
                            //Thread.Sleep(500);
                            //if (File.Exists(PdfProcessOBJ.InputPDF.Replace(".pdf", "_review.pdf")))
                            //    return true;
                            //else
                              //  return false;
                            return true;
                        }
                        else
                        {
                            ProcessEventHandler("PDF security Fail");
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        base.ProcessErrorHandler(ex);
                        IsPdfProcessError = true;
                    }
                }
                if (QL != null)
                    QL = null;
                if (File.Exists(PdfProcessOBJ.InputPDF))
                    return true;
                else
                {
                    File.Move(PdfProcessOBJ.InputPDF.Replace(".pdf", "_Temp.pdf"), PdfProcessOBJ.InputPDF);
                    System.Threading.Thread.Sleep(10000);
                    return true;
                }
            }
            catch (Exception Ex)
            {

                base.ProcessErrorHandler(Ex);
                return false;
            }
        }
        public bool IsPdfProcessError
        {
            get;
            set;
        }

        private bool MoveToOPSServer(eProofArticleInfo eProofArticleInfoObj)
        {
            bool Result = false;

            string ReviewPDF = Path.GetDirectoryName(eProofArticleInfoObj.InputPDF) + "\\" + eProofArticleInfoObj.ReviewPDFName;
            string ReviewFolder = ConfigDetails.OPSPDFLoc.TrimEnd('\\') + "\\" + Path.GetFileName(eProofArticleInfoObj.ReviewPDFName);

            if (File.Exists(ReviewFolder) && File.Exists(ReviewPDF))
            {
                File.Delete(ReviewFolder);
            }

            while (true)
            {
                try
                {
                    File.Move(ReviewPDF, ReviewFolder);
                    Result = true;
                    break;
                }
                catch { }
            }


            string InputPDF = eProofArticleInfoObj.InputPDF;
            string MoveInputPDFToOPS = ConfigDetails.OPSPDFLoc.TrimEnd('\\') + "\\" + Path.GetFileName(eProofArticleInfoObj.InputPDF);

            if (File.Exists(ReviewFolder) && File.Exists(MoveInputPDFToOPS))
            {
                File.Delete(MoveInputPDFToOPS);
            }

            while (true)
            {
                try
                {
                    File.Move(InputPDF, MoveInputPDFToOPS);
                    Result = true;
                    break;
                }
                catch { }
            }

            return Result;
        }
        private bool MailSend()
        {
            string Client = MNT.Client;
            string Stage = "Fresh";
            string JID = MNT.JID;
            string AID = MNT.AID;

            string TemplateLoc = ConfigurationManager.AppSettings["TemplatePath"];
            string TemplatePath = string.Empty;
            string TemplateFile = string.Empty;

            if (TemplateLoc[1] != ':' && TemplateLoc[0] != '\\')
                TemplatePath = "\\\\" + TemplateLoc + "\\" + Client + "\\" + Stage + "\\" + JID;
            else
                TemplatePath = TemplateLoc + "\\" + Client + "\\" + Stage + "\\" + JID;

            TemplateFile = TemplatePath + "\\" + PdfProcessOBJ.MainReviewerRole.Trim() + ".html";

            //if (PdfProcessOBJ.MainReviewerRole.Equals("Author"))
            //    TemplateFile = TemplatePath + "\\Author.html";
            if (File.Exists(TemplateFile))
            {
            }
            else
                TemplateFile = TemplatePath + "\\NextReviewer.html";


            String MailBody = "";
            if (File.Exists(TemplateFile))
            {
                MailBody = ProcessArticle(TemplateFile);
            }
            else
            {
                //LogStr.Add("Author's mail template is not found on below location.....");
                //LogStr.Add(TemplateFile);
                //return false;
            }



            MailDetail MailDetailObj = new MailDetail();

            MailDetailObj.MailFrom = PdfProcessOBJ.MailFrom;

            if (MNT.Client.Equals("JWVCH", StringComparison.OrdinalIgnoreCase))
            {
                  MailDetailObj.MailSubject = "Your proof of " + JID + " " + AID + " is available for review. (" + PdfProcessOBJ.MainReviewerRole.Trim() + ")";
               // MailDetailObj.MailSubject = "Your proof of " + JID + " " + AID + " is available for review.";
            }
            else
                // MailDetailObj.MailSubject = "Your proof of " + JID + " " + AID + " is now available.";
                MailDetailObj.MailSubject = "Your proof of " + JID + " " + AID + " is now available for review.";

            MailDetailObj.MailTo = PdfProcessOBJ.AuthorEMail;
            MailDetailObj.MailCC = string.IsNullOrEmpty(PdfProcessOBJ.MailCC) ? "" : PdfProcessOBJ.MailCC.Trim();
            MailDetailObj.MailBCC = string.IsNullOrEmpty(PdfProcessOBJ.MailBCC) ? "" : PdfProcessOBJ.MailBCC.Trim();
            MailDetailObj.MailBody = MailBody;

            try
            {
                string SaveAuthorMail = "C:\\" + MNT.JID + MNT.AID + ".html";
                if (File.Exists(SaveAuthorMail))
                    File.Delete(SaveAuthorMail);

                File.WriteAllText(SaveAuthorMail, MailBody);
            }
            catch (Exception Ex)
            {
                base.ProcessErrorHandler(Ex);
                //return false;

            }
            eMailProcess eMailProcessObj = new eMailProcess();
            eMailProcessObj.ProcessNotification += base.ProcessEventHandler;
            eMailProcessObj.ErrorNotification += base.ProcessErrorHandler;
            /*
            if ("#WSB#".IndexOf("#" + MNT.JID + "#") != -1)
            {
                string PageChargeForm = @"C:\AEPS\COPYRIGHT\JWUSA\WSB\wsb_pcf.pdf";
                if (File.Exists(PageChargeForm))
                {
                    string CopyTo= Path.GetDirectoryName(PdfProcessOBJ.InputPDF)+ "\\wsb_pcf.pdf";
                    File.Copy(PageChargeForm, CopyTo);
                    PDFFormProcess PDFFormProcessOBJ = new PDFFormProcess(CopyTo, PdfProcessOBJ.ArticleInfoOBJ);
                    PDFFormProcessOBJ.ProcessOnWSBPDF();
                    StringCollection _PDffiles = new StringCollection();
                    _PDffiles.Add(CopyTo);
                    MailDetailObj.MailAtchmnt = _PDffiles;
                }
                else
                {
                    ProcessEventHandler(PageChargeForm +  " file does not exist");
                }
            }
            */
            if ("#MIM#".IndexOf("#" + MNT.JID + "#") != -1)
            {
                string ColorChargeForm = @"\\172.16.2.209\Document\ColorCharge\MIM.pdf";
                string PageChargeForm = @"\\172.16.2.209\Document\PageCharge\MIM.pdf";
                if (File.Exists(ColorChargeForm))
                {
                    string CopyTo = Path.GetDirectoryName(PdfProcessOBJ.InputPDF) + "\\MIM_ColorCharge.pdf";
                    File.Copy(ColorChargeForm, CopyTo);
                }
                if (File.Exists(PageChargeForm))
                {
                    string CopyTo = Path.GetDirectoryName(PdfProcessOBJ.InputPDF) + "\\MIM_PageCharge.pdf";
                    File.Copy(PageChargeForm, CopyTo);
                }
                System.Threading.Thread.Sleep(5000);
                MailDetailObj.MailAtchmnt.Add(Path.GetDirectoryName(PdfProcessOBJ.InputPDF) + "\\MIM_ColorCharge.pdf");
                MailDetailObj.MailAtchmnt.Add(Path.GetDirectoryName(PdfProcessOBJ.InputPDF) + "\\MIM_PageCharge.pdf");
            }
            if (eMailProcessObj.SendMailExternal(MailDetailObj))
            {
                MailDetailObj.MailFrom = "eproof@thomsondigital.com";
                MailDetailObj.MailTo = PdfProcessOBJ.JIDOPSDetail.FailEmail;
                MailDetailObj.MailCC = string.Empty;
                MailDetailObj.MailBCC = string.Empty;
                eMailProcessObj = new eMailProcess();

                //eMailProcessObj.SendMailInternal(MailDetailObj);
                return true;
            }

            return false;
        }
        private string ProcessArticle(string TemplateFile)
        {
            string _URI = PdfProcessOBJ.URI;
            StringBuilder TemplateStr = new StringBuilder(File.ReadAllText(TemplateFile));

            TemplateStr.Replace("<JID>", PdfProcessOBJ.JID);
            TemplateStr.Replace("<AID>", PdfProcessOBJ.AID);
            string URL = "<a href=\"" + _URI + "\">" + _URI + "</a>";

            TemplateStr.Replace("<URL>", URL);

            TemplateStr.Replace("<ARTICLETITLE>", PdfProcessOBJ.ArticleTitle);
            TemplateStr.Replace("<ART_TITLE>", PdfProcessOBJ.ArticleTitle);
            TemplateStr.Replace("<ARTICLE_TITLE>", PdfProcessOBJ.ArticleTitle);
            TemplateStr.Replace("<ARTTITLE>", PdfProcessOBJ.ArticleTitle);      
           // TemplateStr.Replace(", scheduled for publication in ", PdfProcessOBJ.ArticleTitle + ", scheduled for publication in ");
            TemplateStr.Replace("<AUTHORNAME>", PdfProcessOBJ.AuthorName);
            TemplateStr.Replace("<PEEMAIL>", PdfProcessOBJ.PEMail);
            TemplateStr.Replace("<PE>", PdfProcessOBJ.PEName);

            //, scheduled for publication in 

            TemplateStr.Replace("QID", PdfProcessOBJ.MainReviewerQID);
            return TemplateStr.ToString();
        }
        private bool CallJavaJarFile(string source, string destination)
        {
            bool result = false;
            PdfReader reader;
            PdfStamper stamper;
            try
            {
                //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                //Process process = new Process();
                //process.EnableRaisingEvents = false;
                //process.StartInfo.FileName = @"C:\Program Files\Java\jre1.8.0_40\bin\java.exe";
                ////process.StartInfo.FileName = @"C:\Program Files\Java\jre1.8.0_65\bin\java.exe";
                //process.StartInfo.Arguments = "-jar " + '"' + path + "\\EncryptionPdf.jar\" \"" + source + "\" \"" + destination + "\" ";
                //process.Start();

                //File.Delete(source);
                ////File.Move(destination, destination.Replace("1.pdf", ".pdf"));  
                try
                {
                    reader = new PdfReader(source);
                    reader.RemoveUsageRights();
                    stamper = new PdfStamper(reader, new System.IO.FileStream(destination, System.IO.FileMode.CreateNew));
                    stamper.FormFlattening = true;
                    //stamper.setEncryption(USER_PASS.getBytes(), OWNER_PASS.getBytes(),PdfWriter.ALLOW_PRINTING|PdfWriter.ALLOW_FILL_IN|PdfWriter.ALLOW_MODIFY_ANNOTATIONS, PdfWriter.ENCRYPTION_AES_128);
                    stamper.SetEncryption(null, System.Text.Encoding.UTF8.GetBytes("Th0MsOnD123"), PdfWriter.ALLOW_PRINTING | PdfWriter.ALLOW_FILL_IN | PdfWriter.ALLOW_MODIFY_ANNOTATIONS | PdfWriter.ALLOW_COPY, PdfWriter.DO_NOT_ENCRYPT_METADATA);                  
                    stamper.Close();
                    reader.Close();
                    if (File.Exists(source))
                    {
                        File.Delete(source);
                        System.Threading.Thread.Sleep(10000);
                    }
                        File.Move(destination, source);
                        System.Threading.Thread.Sleep(5000);
                        File.Delete(destination);

                    reader = new PdfReader(source);
                    if (!reader.IsEncrypted())
                    {
                        ProcessEventHandler("PDF file is not encrypted");
                        return false;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    ProcessEventHandler("Error in PDF Security: " + ex.Message);
                    return false;
                }

                result = true;
            }
            catch (Exception ex)
            {
                ProcessEventHandler(ex.Message);
                result = false;
            }
            return result;
        }
    }
}


