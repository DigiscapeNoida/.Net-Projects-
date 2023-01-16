using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;
using DatabaseLayer;
using PdfProcess;
using System.Collections.Generic;
using System.Collections.Specialized;
using ProcessNotification;
using System.Text;
using MsgRcvr;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Threading;
using iTextSharp.text.pdf;

namespace AutoEproof
{
    class eProof : MessageEventArgs, IValidation
    {
        Random rand = new Random();
      
    

        
        string _XMLPath = string.Empty;
        string _PDFPath = string.Empty;
        string _OPSConStr = string.Empty;
        string _MailBody = string.Empty;

        string _ACRCorMail = string.Empty;
        bool _IsACRJID = false;

        bool _IsVCHAtchmnt = false;

        int _ePrfHstryCount = 0;

        string _UID = string.Empty;
        string _PWD = string.Empty;

        StringCollection _PDffiles = new StringCollection();

        ArticleInfo _ArticleInfoOBJ = null;
        OPSDetail _OPSDetailObj = null;
        MNTInfo _MNT = null;
        OPSDB _OPSDBObj = null;


        public int ePrfHstryCount
        {
            get { return _ePrfHstryCount; }
        }

        public eProof(MNTInfo MNT, InputFiles InputFiles_)
        {
            _MNT = MNT;
            _XMLPath = InputFiles_.XMLPath;
            _PDFPath = InputFiles_.PDFPath;

            _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            _OPSDBObj = new OPSDB(_OPSConStr);
            


        }

        public void TempProcessMNT()
        {
            PdfProcess();
            CommentEnablePDF();
            //UploadArticleOnFtp();
        }

        public bool ProcessMNT()
        {
            bool Result = false;
            if (_MNT.Client.Equals("JWVCH"))
            {
                _UID = GetUserName();
                _PWD = GetPassword();
            }
            else
            {
                _UID = _MNT.AID;
                _PWD = _MNT.AID;
            }

            ProcessEventHandler("GetMailBody Start..");

            _MailBody = GetMailBody();



            ProcessEventHandler("GetMailBody Finish..");

            try
            {
                ProcessEventHandler("Start PdfProcess..");
                if (PdfProcess())
                {
                    ProcessEventHandler("Start Comment Enable PDF..");
                    if (CommentEnablePDF())
                    {
                        if (UploadArticleOnFtp() == false)
                        {
                            ProcessEventHandler("Try again to upload article on Ftp...");
                            UploadArticleOnFtp();
                        }

                        if (_IsVCHAtchmnt)
                        {
                            string AtchmntFileName = Path.GetDirectoryName(_PDFPath) + "\\" + _MNT.JID.ToLower() + "_" + _MNT.AID + ".pdf";
                            File.Move(_PDFPath, AtchmntFileName);
                            _PDFPath = AtchmntFileName;
                            _PDffiles.Add(_PDFPath);
                        }

                        ProcessEventHandler("Process start to create eProof URL");
                        if (CreateURL())
                        {
                            ProcessEventHandler("Process start to insert eProof reminder");
                            if (InsertReminder())
                            {
                                ProcessEventHandler("Process start to insert eProof history");
                                if (InsertHistory())
                                {
                                    ProcessEventHandler("Process start to send mamil");
                                    if (ProcessMail())
                                    {
                                        Result = true;
                                    }
                                    else
                                    {
                                        ProcessEventHandler("Mail was nots not sent successfully. So rollback entry fron eproof history..");

                                        ProcessEventHandler("DeleteReminder");
                                        DeleteReminder();

                                        ProcessEventHandler("DeleteHistory");
                                        DeleteHistory();
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ProcessErrorHandler(ex);
            }

            return Result;
        }
        public bool ProcessETCMNT()
        {
            bool Result = false;
            ProcessEventHandler("GetMailBody Start..");

            _MailBody = GetMailBody();

            ProcessEventHandler("GetMailBody Finish..");

            try
            {
                ProcessEventHandler("Start PdfProcess..");
                ProcessEventHandler("Start Comment Enable PDF..");
                if (CommentEnablePDF())
                {
                    _ArticleInfoOBJ.AuthorEmail = "lgraup@infionline.net, emnels@umich.edu";
                    _ArticleInfoOBJ.AuthorName = "Editors";

                    if (Directory.Exists(ConfigDetails.InputFilesForEditor))
                    {
                        AddeProofPdf(_PDFPath, ConfigDetails.InputFilesForEditor);
                    }

                    if (UploadArticleOnFtp() == false)
                    {
                        ProcessEventHandler("Try again to upload article on Ftp...");
                        UploadArticleOnFtp();
                    }

                    if (InsertHistory())
                    {
                        ProcessEventHandler("Process start to send mamil");
                        MailDetail MailDetailOBJ = new MailDetail();
                        MailDetailOBJ.MailFrom = "wileyny.j@thomsondigital.com";
                        MailDetailOBJ.MailTo = "lgraup@infionline.net";
                        MailDetailOBJ.MailCC = "julieporter529@gmail.com,wileyus@thomsondigital.com,wileyny.j@thomsondigital.com";
                        MailDetailOBJ.MailBCC = "wileyproof@wiley.thomsondigital.com";

                        MailDetailOBJ.MailSubject = "Proofs of ETC " + MNT.AID + " for Approval";
                        MailDetailOBJ.MailBody = _MailBody;

                        //MailDetailOBJ.MailAtchmnt.Add(_PDFPath);

                        eMailProcess eMailProcessOBJ = new eMailProcess();
                        eMailProcessOBJ.ProcessNotification += ProcessEventHandler;
                        eMailProcessOBJ.ErrorNotification += ProcessErrorHandler;


                        if (eMailProcessOBJ.SendMailExternal(MailDetailOBJ))
                        {
                            Result = true;
                        }
                        else
                        {
                            ProcessEventHandler("Mail was nots not sent successfully. So rollback entry fron eproof history..");

                            ProcessEventHandler("DeleteReminder");
                            DeleteReminder();

                            ProcessEventHandler("DeleteHistory");
                            DeleteHistory();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                this.ProcessErrorHandler(ex);
            }
            return Result;

        }

        public bool ProcessIEAMMNT()
        {
            bool Result = false;
            ProcessEventHandler("GetMailBody Start..");

            _MailBody = GetMailBody();

            ProcessEventHandler("GetMailBody Finish..");

            try
            {
                ProcessEventHandler("Start PdfProcess..");
                ProcessEventHandler("Start Comment Enable PDF..");
                if (CommentEnablePDF())
                {
                    if (Directory.Exists(ConfigDetails.InputFilesForEditor))
                    {
                        AddeProofPdf(_PDFPath, ConfigDetails.InputFilesForEditor);
                    }

                    //if (UploadArticleOnFtp() == false)
                    //{
                    //    ProcessEventHandler("Try again to upload article on Ftp...");
                    //    UploadArticleOnFtp();
                    //}

                    _ArticleInfoOBJ.AuthorEmail = "ieamprod@wiley.com";
                    _ArticleInfoOBJ.AuthorName  = "Editors";

                   
                   
                        if (InsertHistory())
                        {
                            ProcessEventHandler("Process start to send mamil");
                            MailDetail MailDetailOBJ = new MailDetail();
                            MailDetailOBJ.MailFrom = "wileyny.j@thomsondigital.com";
                            

                            MailDetailOBJ.MailTo = "ieamprod@wiley.com";
                            MailDetailOBJ.MailCC = "wileyus@thomsondigital.com,wileyny.j@thomsondigital.com";
                            MailDetailOBJ.MailBCC = "wileyproof@wiley.thomsondigital.com";
                            MailDetailOBJ.MailSubject = "Proofs of " + MNT.JID + " " + MNT.AID + " for Approval";
                            MailDetailOBJ.MailBody = _MailBody;

                            MailDetailOBJ.MailAtchmnt.Add(_PDFPath);

                            eMailProcess eMailProcessOBJ = new eMailProcess();
                            eMailProcessOBJ.ProcessNotification += ProcessEventHandler;
                            eMailProcessOBJ.ErrorNotification += ProcessErrorHandler;


                            if (eMailProcessOBJ.SendMailExternal(MailDetailOBJ))
                            {
                                Result = true;
                            }
                            else
                            {
                                ProcessEventHandler("Mail was nots not sent successfully. So rollback entry fron eproof history..");

                                ProcessEventHandler("DeleteReminder");
                                DeleteReminder();

                                ProcessEventHandler("DeleteHistory");
                                DeleteHistory();
                            }

                        }
                    }
                
            }
            catch (Exception ex)
            {
                this.ProcessErrorHandler(ex);
            }
            return Result;

        }

        public string PDFPath
        {
            get { return _PDFPath; }
        }
        public string XMLPath
        {
            get { return _XMLPath; }
        }

        public string ACRCorMail
        {
            get { return _ACRCorMail; }
        }

        public ArticleInfo ArtclInfo
        {
            get { return _ArticleInfoOBJ; }
        }

        public OPSDetail OPSDetailObj
        {
            get { return _OPSDetailObj; }
        }

        public MNTInfo MNT
        {
            get { return _MNT; }
        }

        private void Intialize()
        {
            _ArticleInfoOBJ = new ArticleInfo();

            CorAuthorDetaill CorDetaill = _OPSDBObj.GetCorAuthorDetaill(_MNT.JID, _MNT.AID);

            if (!string.IsNullOrEmpty(_XMLPath) && File.Exists(_XMLPath))
            {
                InitiallizeAuthorInfo();
            }

            if (CorDetaill != null)
            {
                if (string.IsNullOrEmpty(_ArticleInfoOBJ.ArticleTitle))
                    _ArticleInfoOBJ.ArticleTitle = CorDetaill.Title;

                if (!string.IsNullOrEmpty(CorDetaill.CorMail))
                    _ArticleInfoOBJ.AuthorEmail = CorDetaill.CorMail;

                if (!string.IsNullOrEmpty(CorDetaill.CorName))
                    _ArticleInfoOBJ.AuthorName = CorDetaill.CorName;

                if (!string.IsNullOrEmpty(CorDetaill.CorMailCC))
                    _ArticleInfoOBJ.CorEmailCC = CorDetaill.CorMailCC;
            }


            if (string.IsNullOrEmpty(_ArticleInfoOBJ.AuthorName))
                _ArticleInfoOBJ.AuthorName = "Author";
            else if (_ArticleInfoOBJ.AuthorName.IndexOf("?") != -1)
                _ArticleInfoOBJ.AuthorName = "Author";

               
            return;
        }

        private bool ProcessMail()
        {
            //MailDetailOBJ.MailSubject  = "Your eProof is now available for " + _MNT.JID + " " + _MNT.AID;

            ProcessEventHandler("Start ProcessMail");

            MailDetail MailDetailOBJ = new MailDetail();
            MailDetailOBJ.MailFrom = _OPSDetailObj.FromMail;
            MailDetailOBJ.MailTo = _ArticleInfoOBJ.AuthorEmail;



            if (!string.IsNullOrEmpty(_ArticleInfoOBJ.CorEmailCC))
                MailDetailOBJ.MailCC = _ArticleInfoOBJ.CorEmailCC + "," + _OPSDetailObj.CCMail;
            else
                MailDetailOBJ.MailCC = _OPSDetailObj.CCMail;


            MailDetailOBJ.MailBCC = _OPSDetailObj.BccMail;
            MailDetailOBJ.MailSubject = GeteMailSubject();
            if (string.IsNullOrEmpty(MailDetailOBJ.MailSubject))
                return false;
            MailDetailOBJ.MailBody = _MailBody;
            MailDetailOBJ.Stage = _MNT.Stage;

            if ("#WMON#POLQ#MASY#PSSA#PSSB#ADEM#MABI#MATS#MREN#MAME#".IndexOf("#" + _MNT.JID + "#") != -1)
            {
                MailDetailOBJ.MailFrom = _OPSDetailObj.InternalPEmail;
                MailDetailOBJ.MailTo = _OPSDetailObj.InternalPEmail;
                MailDetailOBJ.MailAtchmnt = _PDffiles;
            }
            else if ("#IEAM#".IndexOf("#" + _MNT.JID + "#") != -1)
            {
                MailDetailOBJ.MailFrom = _OPSDetailObj.InternalPEmail;
                MailDetailOBJ.MailTo = _OPSDetailObj.InternalPEmail;
            }

            if ("#WSB#".IndexOf("#" + _MNT.JID + "#") != -1)
            {
                _PDffiles.Clear();
                _PDffiles.Add(@"C:\AEPS\COPYRIGHT\JWUSA\WSB\wsb_pcf.pdf");
                MailDetailOBJ.MailAtchmnt = _PDffiles;
            }

            if (_IsVCHAtchmnt)
            {
                MailDetailOBJ.MailAtchmnt = _PDffiles;
            }
            else if (_MNT.Client.Equals("JWVCH") && MailDetailOBJ.MailTo.IndexOf("thomsondigital", StringComparison.OrdinalIgnoreCase) != -1)
            {
                MailDetailOBJ.MailAtchmnt = _PDffiles;
            }


            eMailProcess eMailProcessOBJ = new eMailProcess();
            eMailProcessOBJ.ProcessNotification += ProcessEventHandler;
            eMailProcessOBJ.ErrorNotification += ProcessErrorHandler;

            if (eMailProcessOBJ.SendMailExternal(MailDetailOBJ))
            {
                ///////////Fortest
                //MailDetailOBJ.MailFrom = "eproof@thomsondigital.com";
                ////MailDetailOBJ.MailTo = "TDXPS.Wileyjournals@thomsondigital.Com,arshad.k@thomsondigital.Com";
                //MailDetailOBJ.MailTo = _OPSDetailObj.FailEmail;
                //MailDetailOBJ.MailCC = string.Empty;
                //MailDetailOBJ.MailBCC = string.Empty;
                //eMailProcessOBJ.SendMailInternal(MailDetailOBJ);
                /////////////
                return true;
            }

            return false;
        }


        private bool CommentEnablePDF()
        {
            try
            {
                string[] args = new string[1];
                args[0] = _PDFPath;
                PDFAnnotation.Program.Main(args);
                Thread.Sleep(1000);
                //return true;   //Comment it and open below comment

                ProcessEventHandler("PDF security Start");
                if (CallJavaJarFile(_PDFPath, _PDFPath.Replace(".pdf", "1.pdf")))
                {
                    ProcessEventHandler("PDF security END");
                    //args = new string[2];
                    //args[0] = _PDFPath.Replace(".pdf", "1.pdf");
                    //args[1] = "SaveACopyAEPS";
                    //PDFAnnotation.Program.Main(args);
                    //Thread.Sleep(500);
                    //if (File.Exists(_PDFPath))
                    //    return true;
                    //else
                    //    return false;
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
                IsPdfProcessError = true;
                base.ProcessErrorHandler(ex);
                return false;
            }
        }
        private void InitiallizeAuthorInfo()
        {
            ArticleXMLProcess ArticleXMLProcessOBJ = new ArticleXMLProcess(_XMLPath);
            _ArticleInfoOBJ = ArticleXMLProcessOBJ.ArticleInfoOBJ;

        }

        private string GeteMailSubject()
        {
            string emailSubject = string.Empty;
            string DOI = string.Empty;
            string JID = string.Empty;
            string AID = string.Empty;


            if (_MNT.Client.Equals("JWVCH"))
            {
                if (_MNT.JID.Equals("PSSA"))
                    //emailSubject = "Urgent: Proofs for immediate corrections of your paper in physica status solidi (a) - DOI 10.1002/pssa." + _MNT.AID;
                    emailSubject = "Your proof of " + _MNT.JID + " " + _MNT.AID + " is available for review.";

                    
                else if (_MNT.JID.Equals("PSSB"))
                    //emailSubject = "Urgent: Proofs for immediate corrections of your paper in physica status solidi (b) - DOI 10.1002/pssb." + _MNT.AID;
                    emailSubject = "Your proof of " + _MNT.JID + " " + _MNT.AID + " is available for review.";
                else if (_MNT.JID.Equals("ARDP"))
                   // emailSubject = "Page Proofs of your Manuscript ardp." + _MNT.AID;
                    emailSubject = "Your proof of " + _MNT.JID + " " + _MNT.AID + " is available for review.";
                else if (_MNT.JID.Equals("BIES"))
                {
                    string Issue = GetIssue();
                   // emailSubject = "Page Proofs of Your Manuscript [" + _MNT.AID + "] of BIES , Issue " + Issue;
                    emailSubject = "Your proof of " + _MNT.JID + " " + _MNT.AID + " is available for review.";
                }
                else
                   // emailSubject = "Proofs of " + _MNT.JID + " " + _MNT.JID + _MNT.AID;
                    emailSubject = "Your proof of " + _MNT.JID + " " + _MNT.AID + " is available for review.";
            }
            else
            // emailSubject = "Your eProof is now available for " + _MNT.JID + " " + _MNT.AID;
            {
                //Added on 14-12-2016 by Krunesh

                //Begin

                try
                {
                    if (_MNT.JID == "PIN")
                    {

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString);
                        con.Open();

                        string _inputStage = "S100";

                        SqlDataAdapter da = new SqlDataAdapter("Select * from wileywip where JID='" + _MNT.JID + "' and AID='" + _MNT.AID + "' and STAGE='" + _inputStage + "'", con);

                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string editorialRefCode = ds.Tables[0].Rows[0]["VOL"].ToString();
                            if (editorialRefCode != string.Empty)
                            {
                                emailSubject = "Your eProof is now available for " + _MNT.JID + " " + _MNT.AID + " (" + editorialRefCode + ")";
                            }

                        }

                        con.Close();

                    }
                    else
                    {
                      //  emailSubject = "Your eProof is now available for " + _MNT.JID + " " + _MNT.AID;
                        emailSubject = "Your proof of " + _MNT.JID + " " + _MNT.AID + " is available for review.";
                    }

                    
                }
                catch (Exception ex)
                {
                    
                 
                }
                
            }
            //End

                return emailSubject;
            
        }

        private bool PdfProcess()
        {
            try
            {
                _PDffiles.Clear();
                string COPYRIGHTPdfPath = @"C:\AEPS\COPYRIGHT\" + _MNT.Client + "\\" + _MNT.JID;

                string COPYRIGHTPdf = COPYRIGHTPdfPath + "\\" + _MNT.JID + ".pdf";
                string Annotation = COPYRIGHTPdfPath + "\\Using_E-Annotation_tools.pdf";

                _PDffiles.Add(_PDFPath);

                //_PDffiles.Add(COPYRIGHTPdf);

                if (File.Exists(Annotation))
                    _PDffiles.Add(Annotation);

                if (_MNT.JID.Equals("BIES"))
                {
                    string ReprintPDF = COPYRIGHTPdfPath + "\\BIES_reprint_order_form.pdf";
                    if (File.Exists(ReprintPDF))
                        _PDffiles.Add(ReprintPDF);
                }
                //Addition work on 28/07/2017
                PdfProcess.PDF PDFObj = new PDF(_PDFPath, MNT);

                string[] SrcFiles = new string[2];
                if (File.Exists(_PDFPath.Replace(".pdf", "Q.pdf")))
                {
                    ProcessEventHandler("Q PDF Merge Start");
                    PDFObj = new PDF(_PDFPath.Replace(".pdf", "Q.pdf"), MNT);
                    SrcFiles[0] = _PDFPath.Replace(".pdf", "Q.pdf");
                    SrcFiles[1] = _PDFPath;
                    PDFObj.MergePDF(SrcFiles);
                    Thread.Sleep(5000);
                    ProcessEventHandler("Q PDF Merge Complete");
                }
                string GAbsPdf = _PDFPath.Replace(".pdf", "c.pdf");

                if (File.Exists(_PDFPath.Replace(".pdf", "Q.pdf")))
                {
                    if (File.Exists(_PDFPath.Replace(".pdf", "c.pdf")))
                    {
                        ProcessEventHandler("C PDF Merge Start");
                        SrcFiles[0] = _PDFPath.Replace(".pdf", "Q.pdf");
                        SrcFiles[1] = _PDFPath.Replace(".pdf", "c.pdf");                         
                        PDFObj.MergePDF(SrcFiles);
                        Thread.Sleep(5000);
                        ProcessEventHandler("C PDF Merge Complete");
                    }
                }
                else
                {
                   
                    if (File.Exists(_PDFPath.Replace(".pdf", "c.pdf")))
                    {
                        ProcessEventHandler("C PDF Merge Start");
                        PDFObj = new PDF(_PDFPath.Replace(".pdf", "c.pdf"), MNT);
                        SrcFiles[0] = _PDFPath;
                        SrcFiles[1] = _PDFPath.Replace(".pdf", "c.pdf");                       
                        PDFObj.MergePDF(SrcFiles);
                        Thread.Sleep(5000);
                        ProcessEventHandler("C PDF Merge Complete");
                    }
                }

               // PdfProcess.PDF PDFObj = new PDF(_PDFPath, MNT);
                PDFObj.ProcessNotification += ProcessEventHandler;
                PDFObj.ErrorNotification += ProcessErrorHandler;



                if ("#FLAN#JEAB#".IndexOf("#" + MNT.JID + "#") != -1)
                {
                }
                else
                {
                    PDFObj.AddWatermark();
                }


                if (File.Exists(COPYRIGHTPdf))
                {
                    //if (_MNT.JID.Equals("BIES"))
                    //    PDFObj.MergePDF(new string[2] { COPYRIGHTPdf, _PDFPath.Replace(".pdf", "Q.pdf") });
                    //else if (_MNT.JID.Equals("TEA"))
                    //    PDFObj.MergePDF(new string[2] { COPYRIGHTPdf, _PDFPath.Replace(".pdf", "Q.pdf") });
                    //else
                    if(File.Exists(_PDFPath.Replace(".pdf", "Q.pdf")))
                        PDFObj.MergePDF(new string[2] { COPYRIGHTPdf, _PDFPath.Replace(".pdf", "Q.pdf") });
                    else if (File.Exists(_PDFPath.Replace(".pdf", "c.pdf")))
                    {
                        PDFObj.MergePDF(new string[2] { COPYRIGHTPdf, _PDFPath.Replace(".pdf", "c.pdf") });
                    }
                    else                   
                        PDFObj.MergePDF(new string[2] { COPYRIGHTPdf, _PDFPath});
                }

                if (File.Exists((_PDFPath.Replace(".pdf", "Q.pdf"))))
                {
                    File.Delete(_PDFPath);
                    File.Move(_PDFPath.Replace(".pdf", "Q.pdf"), _PDFPath.Replace(".pdf", "_Temp.pdf"));
                  
                }
                else
                {
                    if (File.Exists(_PDFPath.Replace(".pdf", "c.pdf")))
                    {   
                       if(File.Exists(_PDFPath))
                        {
                            File.Delete(_PDFPath);
                        }
                       File.Move(_PDFPath.Replace(".pdf", "c.pdf"), _PDFPath.Replace(".pdf", "_Temp.pdf"));
                    }
                 
                }
                if (File.Exists(_PDFPath.Replace(".pdf", "_Temp.pdf")))
                {
                    ProcessEventHandler("Query Linking Start");
                    Thread.Sleep(10000);
                    QueryLinking QL = new QueryLinking();
                    if (QL.Start(_PDFPath.Replace(".pdf", "_Temp.pdf")))
                    {
                        ProcessEventHandler("Query Linking Complete");
                        
                    }
                    else if (File.Exists(_PDFPath))
                        return true;
                    else
                    {
                        File.Move(_PDFPath.Replace(".pdf", "_Temp.pdf"), _PDFPath);
                        System.Threading.Thread.Sleep(5000);
                        return true;
                    }
                    if (QL != null)
                        QL = null;
                }
                

                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                base.ProcessErrorHandler(ex);
                return false;

            }

            return true;
        }

        private string GetMailBody()
        {
            try
            {
                StringBuilder MailBody = new StringBuilder("");

                string TemplatePath = ConfigDetails.EXELoc + "\\TEMPLATES\\" + _MNT.Client + "\\Template_" + _MNT.JID + ".txt";
                string url = string.Empty;

                if (File.Exists(TemplatePath))
                    MailBody = new StringBuilder(File.ReadAllText(TemplatePath));
                else
                    return string.Empty;



                if (_MNT.Client.Equals("JWVCH"))
                {

                    if (MailBody.ToString().IndexOf("<URL>") == -1)
                    {
                        _IsVCHAtchmnt = true;
                    }

                    if ("#PPAP#CVDE#MACO#".IndexOf("#" + _MNT.JID + "#") != -1)
                        url = "http://59.160.102.163/" + _MNT.Client + "/" + _MNT.JID + "/" + _MNT.JID + _MNT.AID + "/" + _MNT.JID.ToLower() + "_" + _MNT.AID + ".pdf";
                    else
                        url = "http://59.160.102.163/cgi-bin/" + _MNT.Client + "/" + _MNT.JID + "/" + _MNT.JID + _MNT.AID + "/" + _MNT.JID.ToLower() + "_" + _MNT.AID + ".cgi";
                }
                else
                    url = "http://59.160.102.163/cgi-bin/" + _MNT.Client + "/" + _MNT.JID + "/" + _MNT.AID + "/" + _MNT.AID + ".cgi";






                if (_MNT != null)
                {
                    MailBody.Replace("<JID>", _MNT.JID);
                    MailBody.Replace("<AID>", _MNT.AID);
                    MailBody.Replace("<PNO>", _MNT.AID);
                    MailBody.Replace("<USER>", _UID);
                    MailBody.Replace("<PASSWORD>", _PWD);
                    MailBody.Replace("<DOI>", _MNT.AID);
                }

                if (_ArticleInfoOBJ != null)
                    MailBody.Replace("<ART_TITLE>", _ArticleInfoOBJ.ArticleTitle);
                MailBody.Replace("<ARTICLETITLE>", _ArticleInfoOBJ.ArticleTitle);
                MailBody.Replace("<ARTICLE_TITLE>", _ArticleInfoOBJ.ArticleTitle);
                MailBody.Replace("<ARTTITLE>", _ArticleInfoOBJ.ArticleTitle);
                MailBody.Replace("<AUTHORNAME>", _ArticleInfoOBJ.AuthorName);

                if (_OPSDetailObj != null)
                {
                    MailBody.Replace("<URL>", url);
                    MailBody.Replace("<PEDESIG>", _OPSDetailObj.Designation);
                    MailBody.Replace("<PE>", _OPSDetailObj.Peditor);
                    MailBody.Replace("<PENAME>", _OPSDetailObj.Peditor);
                    MailBody.Replace("<PEName>", _OPSDetailObj.Peditor);

                    MailBody.Replace("<PEEMAILID>", _OPSDetailObj.Pe_email);
                    MailBody.Replace("<PEEMAIL>", _OPSDetailObj.Pe_email);

                    MailBody.Replace("<JNAME>", _OPSDetailObj.Jname);

                    MailBody.Replace("<PEPHONE>", _OPSDetailObj.Phone);
                    MailBody.Replace("<PEFAX>", _OPSDetailObj.Fax);
                    MailBody.Replace("<PEADDRESS>", _OPSDetailObj.Address);

                    MailBody.Replace("<ThomsonContact>", _OPSDetailObj.InternalPEName);

                    //Added on 14-12-2016 by  by Krunesh

                    //Begin

                    if (_MNT.JID == "PIN")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString);
                        con.Open();

                        string _inputStage = "S100";

                        SqlDataAdapter da = new SqlDataAdapter("Select * from wileywip where JID='" + _MNT.JID + "' and AID='" + _MNT.AID + "' and STAGE='" + _inputStage + "'", con);

                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string editorialRefCode = ds.Tables[0].Rows[0]["VOL"].ToString();
                            if (editorialRefCode != string.Empty)
                            {
                                MailBody.Replace("<VOL>", editorialRefCode.Trim());
                            }

                        }
                        con.Close();
                    }


                    //else
                    //{
                    //    string editorialRefCode = string.Empty;
                    //    MailBody.Replace("<VOL>", editorialRefCode);                       
                    //}  

                    //END


                }

                return MailBody.ToString();
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }
            return string.Empty;
        }

        private bool CreateURL()
        {
            ProcessEventHandler("Start CreateURL..");


            ProcessEventHandler("Upload pdf process has been  start..");
            ProcessEventHandler("Upload pdf process has been  start..Initiated");
            URLService.CreateEproofURL URLObj = new URLService.CreateEproofURL();
            ProcessEventHandler("Upload pdf process has been  start..Oject Created");
            string pdf = UploadFile(URLObj, _PDFPath);
            ProcessEventHandler("Upload pdf process has been  start..Value returned");

          

            if (!string.IsNullOrEmpty(pdf))
            {
                ProcessEventHandler("Upload pdf process has been completed.");
                try
                {
                    if (_MNT.Client.Equals("JWVCH"))
                    {
                        URLObj.CreateURL(_MNT.Client, _MNT.JID, _MNT.AID, pdf, _UID, _PWD);
                    }
                    else
                        URLObj.CreateURL(_MNT.Client, _MNT.JID, _MNT.AID, pdf);
                }
                catch (Exception ex)
                {
                    ProcessEventHandler("Error in url creation process");
                    ProcessErrorHandler(ex);
                    return false;

                }
            }
            else
            {
                ProcessEventHandler("Error in  uUpload pdf process.");
                return false;
            }

            ProcessEventHandler("URL created successfully");
            return true;
        }

        private string GetUserName()
        {
            string UserName = "";

            try
            {
                for (int i = 0; i < 5; i++)
                {

                    UserName = UserName + Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rand.NextDouble() + 65)));
                }
                return UserName;
            }
            catch
            {
                return UserName;
            }
        }

        private string GetPassword()
        {
            string UserPass = "";

            try
            {
                for (int i = 0; i < 5; i++)
                {
                    UserPass = UserPass + rand.Next(0, 9);
                }
                return UserPass;
            }
            catch
            {
                return UserPass;
            }

        }
        private string GetIssue()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            string BiesIssue = string.Empty;
            using (SqlConnection Con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand SqlCommand = Con.CreateCommand())
                {
                    SqlCommand.CommandText = "usp_GetBiesIssue";
                    SqlCommand.CommandType = CommandType.StoredProcedure;

                    SqlCommand.Parameters.Add(new SqlParameter("@JIDAID", SqlDbType.VarChar, 50));
                    SqlCommand.Parameters.Add(new SqlParameter("@ISSUE", SqlDbType.VarChar, 30));

                    SqlCommand.Parameters["@JIDAID"].Value = _MNT.JID + _MNT.AID;
                    SqlCommand.Parameters["@ISSUE"].Direction = ParameterDirection.Output;

                    Con.Open();

                    try
                    {
                        SqlCommand.ExecuteNonQuery();
                        BiesIssue = SqlCommand.Parameters["@ISSUE"].Value.ToString();
                    }
                    catch (SqlException Ex)
                    {
                        ProcessErrorHandler(Ex);
                    }
                    finally
                    {
                        Con.Close();

                    }
                }
            }
            return BiesIssue;
        }
        private string UploadFile(URLService.CreateEproofURL EproofURL, string filename)
        {
            try
            {
                ProcessEventHandler("EproofURL:" + EproofURL.ToString());
                ProcessEventHandler("filename:" + filename);
                // get the exact file name from the path
                String strFile = System.IO.Path.GetFileName(filename);

                // create an instance fo the web service


                // get the file information form the selected file
                FileInfo fInfo = new FileInfo(filename);

                // get the length of the file to see if it is possible
                // to upload it (with the standard 4 MB limit)
                long numBytes = fInfo.Length;
                double dLen = Convert.ToDouble(fInfo.Length / 1000000);

                // Default limit of 4 MB on web server
                // have to change the web.config to if
                // you want to allow larger uploads
                ProcessEventHandler("UploadFile: 1");
                if (true)
                {
                    // set up a file stream and binary reader for the 
                    // selected file
                    ProcessEventHandler("UploadFile: 2");
                    FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);

                    ProcessEventHandler("UploadFile: 3");
                    // convert the file to a byte array
                    byte[] data = br.ReadBytes((int)numBytes);
                    br.Close();

                    ProcessEventHandler("UploadFile: 4");
                    // pass the byte array (file) and file name to the web service
                    string sTmp = EproofURL.UploadFile(data, strFile);
                    ProcessEventHandler("UploadFile: 4.1");
                    fStream.Close();
                    fStream.Dispose();
                    ProcessEventHandler("Upload pdf path :: " + sTmp);
                    ProcessEventHandler("UploadFile: 5");
                    return sTmp;
                    // this will always say OK unless an error occurs,
                    // if an error occurs, the service returns the error message
                    //MessageBox.Show("File Upload Status: " + sTmp, "File Upload");
                }
                else
                {
                    // Display message if the file was too large to upload
                    //MessageBox.Show("The file selected exceeds the size limit for uploads.", "File Size");
                }
                ProcessEventHandler("UploadFile: 6");
            }
            catch (Exception ex)
            {
                // display an error message to the user
                //MessageBox.Show(ex.Message.ToString(), "Upload Error");
                base.ProcessErrorHandler(ex);
            }
            ProcessEventHandler("UploadFile: 7");
            return string.Empty;
        }

        private bool UploadArticleOnFtp()
        {

            OPSFtpDtl FtpDtl = _OPSDBObj.GetFtpDetails(_OPSDetailObj.OPSID);



            string DateFolder = DateTime.Today.ToString("dd-MM-yyyy");
            string FTPURL = string.Empty;
            string Uname = string.Empty;
            string PWD = string.Empty;

            if (FtpDtl != null && FtpDtl.FtpUpload.Equals("Yes", StringComparison.OrdinalIgnoreCase))
            {
                if (FtpDtl.isOPS.Equals("YES", StringComparison.OrdinalIgnoreCase))
                {
                    FTPURL = FtpDtl.FtpPath ;
                }
                else
                {
                    FTPURL = FtpDtl.FtpPath + "/" + _MNT.JID + "/Fresh%20Proofs/" + DateFolder;
                }
                Uname = FtpDtl.FtpUID;
                PWD = FtpDtl.ftpPWD;
            }
            else
            {
                return true;
            }
            ProcessEventHandler("UploadArticleOnFtp ...");


            string _UploadFileName = Path.GetDirectoryName(_PDFPath) + "\\" + _MNT.JID.ToLower() + "_" + _MNT.AID  + Path.GetExtension(_PDFPath) ;

            File.Move(_PDFPath, _UploadFileName);
            _PDFPath = _UploadFileName;
            try
            {
                FtpProcess FtpObj = new FtpProcess(FTPURL, Uname, PWD);

                FtpObj.ProcessNotification += ProcessEventHandler;
                FtpObj.ErrorNotification += ProcessErrorHandler;


                ProcessEventHandler(" FTPURL " + FTPURL);

                if (FtpObj.FtpDirectoryExists(FTPURL) == true)
                {
                 
                }
                

                ProcessEventHandler("Create Folder on FTP :: " + FTPURL);
                if (FtpObj.CreateFtpFolder(FTPURL) == false)
                {
                    FtpObj.CreateFtpFolder(FTPURL);
                }


                if (FtpObj.UploadFileToFTP(_UploadFileName) == false)
                {
                    FtpObj.UploadFileToFTP(_UploadFileName);


                    ProcessEventHandler("Successfully Upload Article On Ftp ...");
                }
            }
            catch (Exception ex)
            {
                base.ProcessErrorHandler(ex);
                ProcessEventHandler("Error while uploading article on ftp ...");
                return false;
            }

            return true;
        }

        private bool InsertReminder()
        {
            ProcessEventHandler("InsertReminder Start");

            String Todaydate = string.Empty;

            Todaydate = System.DateTime.Now.Year.ToString().Trim() + "-" +
                             System.DateTime.Now.Month.ToString().Trim() + "-" +
                             System.DateTime.Now.Day.ToString().Trim() + " " +
                             System.DateTime.Now.Hour.ToString().Trim() + ":" +
                             System.DateTime.Now.Minute.ToString().Trim() + ":" +
                             System.DateTime.Now.Second.ToString().Trim() + "." +
                             System.DateTime.Now.Millisecond.ToString().Trim();


            DateTime first_rminder;
            DateTime second_reminder;
            DateTime final_reminder;

            first_rminder = CalCulateReminderDate(DateTime.Today, 3);
            second_reminder = CalCulateReminderDate(first_rminder, 3);
            final_reminder = CalCulateReminderDate(second_reminder, 2);

            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@first_rminder", first_rminder);
            param[1] = new SqlParameter("@second_reminder", second_reminder);
            param[2] = new SqlParameter("@final_reminder", final_reminder);


            string RmndrConStr = ConfigurationManager.ConnectionStrings["ReminderConnectionString"].ConnectionString;
            string Uploadpath = "P:\\" + _MNT.Client + "\\" + _MNT.JID + "\\" + _MNT.AID;
            //string InsertCmdString = "Insert into reminder_data (cclient, cJournal_id, cArticle_id, cUsername, cPassword, vDOI,  vJournal_name, email_from, email_to_author, email_to_editor, email_to_bcc,email_to_cc,final_cc,ConfirmMailSend,UploadPath) " +
            //" VALUES ('" + _MNT.Client + "', '" + _MNT.JID + "', '" + _MNT.AID + "', '" + _MNT.AID + "', '" + _MNT.AID + "', '" + _MNT.AID + "', '" + _OPSDetailObj.Jname + "', '" + _OPSDetailObj.FromMail + "', '" + _ArticleInfoOBJ.AuthorEmail + "', '" + _OPSDetailObj.Pe_email + "', '" + _OPSDetailObj.BccMail + "', '" + _OPSDetailObj.CCMail + "', '" + _OPSDetailObj.final_cc + "','False','" + Uploadpath + "')";
            string InsertCmdString = "Insert into reminder_data (cclient, cJournal_id, cArticle_id, cUsername, cPassword, vDOI, dDate_of_uploading, vJournal_name,initial_reminder, next_reminder,final_reminder, email_from, email_to_author, email_to_editor, email_to_bcc,email_to_cc,final_cc,UploadPath,ToDo) " +
                                     " VALUES ('" + _MNT.Client + "', '" + _MNT.JID + "', '" + _MNT.AID + "', '" + _MNT.AID + "', '" + _MNT.AID + "', '" + _MNT.AID + "', '" + DateTime.Today.ToString() + "', '" + _OPSDetailObj.Jname + "', @first_rminder, @second_reminder,@final_reminder, '" + _OPSDetailObj.FromMail + "', '" + Todaydate + "', '" + _OPSDetailObj.Pe_email + "', '" + _OPSDetailObj.BccMail + "', '" + _OPSDetailObj.CCMail + "', '" + _OPSDetailObj.final_cc + "','" + Uploadpath + "','ToDo')";
            try
            {
                SqlHelper.ExecuteNonQuery(RmndrConStr, System.Data.CommandType.Text, InsertCmdString, param);

                ProcessEventHandler("InsertReminder successfully");
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                base.ProcessErrorHandler(ex);
            }
            return false;
        }
        private DateTime CalCulateReminderDate(DateTime dt, int Days)
        {
            DateTime ReminderDate;
            int DayIcr = 0;
            while (true)
            {
                dt = dt.AddDays(1);
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                }
                else if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                }
                else
                {
                    DayIcr = DayIcr + 1;
                    if (DayIcr == Days)
                    {
                        break;
                    }
                }
            }
            ReminderDate = dt;
            return ReminderDate;
        }
        private bool DeleteReminder()
        {
            ProcessEventHandler("DeleteReminder Start");
            string RmndrConStr = ConfigurationManager.ConnectionStrings["ReminderConnectionString"].ConnectionString;
            try
            {
                SqlParameter[] para = new SqlParameter[3];

                para[0] = new SqlParameter("@Client", _MNT.Client);
                para[1] = new SqlParameter("@JID", _MNT.JID);
                para[2] = new SqlParameter("@AID", _MNT.AID);
                SqlHelper.ExecuteNonQuery(RmndrConStr, System.Data.CommandType.StoredProcedure, "DeleteReminderDetail", para);

                ProcessEventHandler("DeleteReminder successfully");
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                base.ProcessErrorHandler(ex);
            }
            return false;
        }

        private bool DeleteHistory()
        {
            try
            {
                ProcessEventHandler("Start DeleteHistory");

                string AEPSJWConStr = ConfigurationManager.ConnectionStrings["AEPSConnectionString"].ConnectionString;
                SqlParameter[] para = new SqlParameter[3];

                para[0] = new SqlParameter("@Customer", _MNT.Client);
                para[1] = new SqlParameter("@JID", _MNT.JID);
                para[2] = new SqlParameter("@AID", _MNT.AID);

                SqlHelper.ExecuteNonQuery(AEPSJWConStr, System.Data.CommandType.StoredProcedure, "usp_DeleteProofHistory", para);


                para = new SqlParameter[2];
                para[0] = new SqlParameter("@OPSID", _OPSDetailObj.OPSID);
                para[1] = new SqlParameter("@AID", _MNT.AID);

                SqlHelper.ExecuteNonQuery(_OPSConStr, System.Data.CommandType.StoredProcedure, "usp_DeleteProofHistory", para);


                ProcessEventHandler("Finish DeleteHistory");
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                base.ProcessErrorHandler(ex);
            }
            return false;
        }

        private bool InsertHistory()
        {
            try
            {
                ProcessEventHandler("Start InsertHistory");

                string AEPSJWConStr = ConfigurationManager.ConnectionStrings["AEPSConnectionString"].ConnectionString;
                string CrntDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");

                string InsertCmdString = "insert into File_History (Original,Customer,Stage,JID,AID,Final,Extras,MailTo,DateCreated,DateModified,Lock) values ('" + _MNT.JID + "-" + _MNT.AID + "','" + _MNT.Client + "','" + "FRESH" + "','" + _MNT.JID + "','" + _MNT.AID + "','" + _MNT.AID + "','UPLOAD','" + _ArticleInfoOBJ.AuthorEmail + "','" + CrntDate + "','" + CrntDate + "','LOCK')";
                SqlHelper.ExecuteNonQuery(AEPSJWConStr, System.Data.CommandType.Text, InsertCmdString);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                base.ProcessErrorHandler(ex);
            }

            try
            {
                eProofHistory eHstryObj = new eProofHistory();
                eHstryObj.OPSID = _OPSDetailObj.OPSID;
                eHstryObj.AID = _MNT.AID;
                eHstryObj.DOI = _MNT.AID;
                eHstryObj.ArticleTitle = _ArticleInfoOBJ.ArticleTitle;
                eHstryObj.CorrName = _ArticleInfoOBJ.AuthorName;
                eHstryObj.MailFrom = _OPSDetailObj.FromMail;
                eHstryObj.MailTo = _ArticleInfoOBJ.AuthorEmail;

                if (string.IsNullOrEmpty(_ArticleInfoOBJ.CorEmailCC))
                    eHstryObj.MailCC = _OPSDetailObj.CCMail;
                else if (!string.IsNullOrEmpty(_ArticleInfoOBJ.CorEmailCC))
                    eHstryObj.MailCC = _ArticleInfoOBJ.CorEmailCC.Replace(';', ',') + "," + _OPSDetailObj.CCMail;

                eHstryObj.MailBCC = _OPSDetailObj.BccMail;

                _OPSDBObj.InsertEproofHistory(eHstryObj);

                ProcessEventHandler("Finish InsertHistory");
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                base.ProcessErrorHandler(ex);
            }
            return false;
        }

        public bool IsValidJID
        {
            get;
            set;
        }

        public bool IsAuthorNameExist
        {
            get;
            set;
        }

        public bool IsAuthorEmailExist
        {
            get;
            set;

        }

        public bool IsArticleTitleExist
        {
            get;
            set;
        }

        public bool IsAlreadyProcessed
        {
            get;
            set;
        }
        public bool IsMatchCorEmailXMLANDDB
        {
            get;
            set;
        }
        public bool IsMatchDOI
        {
            get;
            set;
        }
        public bool IsACRJID
        {
            get;
            set;
        }

        public bool ValidationResult
        {
            get;
            set;
        }

        public bool IsAuthorEMailWellForm
        {
            get;
            set;
        }

        public bool IsPdfProcessError
        {
            get;
            set;
        }
        public bool IsQueryIDExist
        {
            get;
            set;
        }
        public bool IscPDFExist
        {
            get;
            set;
        }
        public void StartValidation()
        {
            Intialize();
            AssignACREmail();
            _OPSDetailObj = _OPSDBObj.GetOPSDetails(_MNT.JID, _MNT.Client);

            if (_MNT.Client.Equals("JWVCH"))
            {
            }
            else if (string.IsNullOrEmpty( _ArticleInfoOBJ.AuthorEmail))
            {
                _ArticleInfoOBJ.AuthorEmail = string.Empty;
            }
            else if (_ArticleInfoOBJ.AuthorEmail.IndexOf("thomsondigital", StringComparison.OrdinalIgnoreCase) != -1)
            {
                _ArticleInfoOBJ.AuthorEmail = string.Empty;
            }

            string BlockedJID = ConfigurationManager.AppSettings["BlockedJID"];
            if (BlockedJID.IndexOf("#" + _MNT.JID.Trim() + "#",StringComparison.OrdinalIgnoreCase) != -1)
            {
                _ArticleInfoOBJ.AuthorEmail = string.Empty;
            }

            PdfPages = MNT.PdfPages;
            AutoPdfPages = MNT.PgCountLog;

            if (PdfPages == AutoPdfPages)
                IsPdfPagesEqualAutopage = true;

            IsAuthorEmailExist = string.IsNullOrEmpty(_ArticleInfoOBJ.AuthorEmail) ? false : true;
            IsArticleTitleExist = string.IsNullOrEmpty(_ArticleInfoOBJ.ArticleTitle) ? false : true;
            IsAuthorNameExist = string.IsNullOrEmpty(_ArticleInfoOBJ.AuthorName) ? false : true;
            IsAuthorEMailWellForm = CheckAuthorEmail(_ArticleInfoOBJ.AuthorEmail);
            ///////start Eproof disable from Toauthor
            //if ("#JABA#JEAB#".IndexOf("#" + MNT.JID + "#") != -1 && MNT.Status.Equals("ToAuthor", StringComparison.OrdinalIgnoreCase))
            //{
            //    int eProofCount;
            //    _OPSDBObj.CheckeProofExistence(_OPSDetailObj.OPSID, MNT.AID, out eProofCount);
            //    if (eProofCount == 1)
            //    {
            //        IsAlreadyProcessed = false;
            //    }
            //    else if (eProofCount > 1)
            //    {
            //        IsAlreadyProcessed = true;
            //    }
            //}
            //else
            //{
            //    IsAlreadyProcessed = _OPSDBObj.CheckeProofExistence(_OPSDetailObj.OPSID, _MNT.AID);
            //}
            ////////End
            IsAlreadyProcessed = _OPSDBObj.CheckeProofExistence(_OPSDetailObj.OPSID, _MNT.AID);

            IsValidJID = _OPSDetailObj == null ? false : true;
            if (ConfigurationManager.AppSettings["FMSJIDList"].IndexOf(_MNT.JID) != -1)
            {
                if (File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + _MNT.JID + _MNT.AID + "\\" + _MNT.JID + _MNT.AID + ".xml"))
                {
                    IsQueryIDExist = true;
                    IscPDFExist = true;
                }
                else
                {
                    if (File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + _MNT.JID + _MNT.AID + "\\" + _MNT.JID + "-" + _MNT.AID + "Q.pdf"))
                    {
                        IsQueryIDExist = true;
                        IscPDFExist = true;  //C PDF not exist for these Journals 
                    }
                    else
                        IsQueryIDExist = false;
                }
            }
            else
            {
                if (_ArticleInfoOBJ.IsQuerypage)
                {
                    if (File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + _MNT.JID + _MNT.AID + "\\" + _MNT.JID + "-" + _MNT.AID + "Q.pdf"))
                        IsQueryIDExist = true;
                    else
                    {
                        string Path = ConfigDetails.TDXPSGangtokFMSFolder + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\WorkArea\\" + MNT.MNTFolder + "\\" + _MNT.AID + "Q.pdf";
                        if (File.Exists(Path))
                        {
                            File.Move(Path, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + _MNT.JID + _MNT.AID + "\\" + _MNT.JID + "-" + _MNT.AID + "Q.pdf");
                            IsQueryIDExist = true;
                            System.Threading.Thread.Sleep(5000);
                        }
                        else
                            IsQueryIDExist = false;
                    }
                }
                else
                    //if (ConfigurationManager.AppSettings["QueryPageExcludeJID"].IndexOf(_MNT.JID) != -1)
                    //    IsQueryIDExist = true;
                    //else
                    IsQueryIDExist = true;

                if (_ArticleInfoOBJ.IsgraphicalAbs)
                {
                    if (File.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + _MNT.JID + _MNT.AID + "\\" + _MNT.JID + "-" + _MNT.AID + "c.pdf"))
                        IscPDFExist = true;
                    else
                    {
                        string Path = ConfigDetails.TDXPSGangtokFMSFolder + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\WorkArea\\" + MNT.MNTFolder + "\\" + _MNT.AID + "c.pdf";
                        if (File.Exists(Path))
                        {
                            File.Move(Path, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Process\\" + _MNT.JID + _MNT.AID + "\\" + _MNT.JID + "-" + _MNT.AID + "c.pdf");
                            IscPDFExist = true;
                            System.Threading.Thread.Sleep(5000);
                        }
                        else
                            IscPDFExist = false;
                    }
                }
                else
                    IscPDFExist = true;
            }
            AssignValidationResult();

            PDF PDFObj = new PDF(_PDFPath, _MNT);
            /////////////No need to eproof if AJt article's type is CDC
            if (_MNT.JID.Equals("AJT") && PDFObj.CheckCDC(_PDFPath))
            {
                IsCDCArticle = true;
                ValidationResult = false;
            }
            else
            {
                IsCDCArticle = false;
            }

        }

        private void AssignACREmail()
        {
            if (_OPSDBObj.CheckACRJIDExistence(_MNT.JID) > 0)
            {
                _IsACRJID = true;
                ArticleContentReport ACR = _OPSDBObj.GetACRDetails(_MNT.JID, _MNT.AID);
                if (ACR != null)
                {
                    string[] ProdStr = "prod ed#proded#Ed , Prod#Prod , Ed".Split('#');
                    bool isProd = false;
                    foreach (string Prod in ProdStr)
                    {
                        if (ACR.CorAuthor.Contains(Prod))
                            isProd = true;
                    }
                    if (isProd)
                        _ACRCorMail = _OPSDetailObj.PRMaill;
                    else
                        _ACRCorMail = ACR.CorEmail.Trim();
                }
                else
                {
                    _ACRCorMail = _OPSDBObj.GetAuthorEmailFromArticleContentReport(_MNT.JID, _MNT.AID);
                }
            }
        }

        private bool CheckAuthorEmail(string _CorrEmail)
        {
            if (_CorrEmail != null)
            {
                if (_CorrEmail.Contains(","))
                {
                    return true;
                }
                else if (!(new Regex(@"^['a-zA-Z0-9_\-\.]+@[a-zA-Z0-9_\-\.]+\.[a-zA-Z]{2,}$")).IsMatch(_CorrEmail))
                {
                    IsAuthorEMailWellForm = false;
                    return false;
                }
                else
                    return true;
            }
            else
                return false;

        }

        private void AssignValidationResult()
        {

            ValidationResult = IsAuthorEmailExist;

            ProcessEventHandler("IsAuthorEmailExist: " + ValidationResult);

            if (ValidationResult)
                ValidationResult = IsValidJID;
            ProcessEventHandler("IsAuthorEmailExist: " + ValidationResult);

            if (ValidationResult)
                ValidationResult = IsAlreadyProcessed ? false : true;
            ProcessEventHandler("IsAlreadyProcessed: " + ValidationResult);

            if (ValidationResult)
            {
                //ValidationResult = IsArticleTitleExist;
            }

            if (ValidationResult)
                ValidationResult = IsAuthorEmailExist;
            ProcessEventHandler("IsAuthorEmailExist: " + ValidationResult);

            if (ValidationResult)
                ValidationResult = IsAuthorEMailWellForm;
            ProcessEventHandler("IsAuthorEMailWellForm: " + ValidationResult);

            if (ValidationResult)
                ValidationResult = IsAuthorNameExist;
            ProcessEventHandler("IsAuthorNameExist: " + ValidationResult);

            if (ValidationResult)
                ValidationResult = IsPdfPagesEqualAutopage;
            ProcessEventHandler("IsPdfPagesEqualAutopage: " + ValidationResult);

            if (ValidationResult)
                ValidationResult = IsQueryIDExist;
            ProcessEventHandler("IsQueryIDExist: " + ValidationResult);

            if (ValidationResult)
                ValidationResult = IscPDFExist;
            ProcessEventHandler("IscPDFExist: " + ValidationResult);
        }

        public bool IsCDCArticle
        {
            get;
            set;
        }

        public string Remark
        {
            get;
            set;
        }

        public bool ProcessJABA_JEAB_MNT()
        {
            bool Result = false;
            ProcessEventHandler("GetMailBody Start..");

            StringBuilder MailB = new StringBuilder();

            MailB.AppendLine("Hi Angelo,");
            MailB.AppendLine(" ");
            MailB.AppendLine("Please find attached typeset proofs for review & approval. Once approved, we will release the same to author's.");
            MailB.AppendLine(" ");
            MailB.AppendLine("Thanks,");
            MailB.AppendLine("Praveen");

            _MailBody = MailB.ToString();

            ProcessEventHandler("GetMailBody Finish..");

            try
            {
                ProcessEventHandler("Start PdfProcess..");
                ProcessEventHandler("Start Comment Enable PDF..");
                if (CommentEnablePDF())
                {
                    if (InsertHistory())
                    {
                        ProcessEventHandler("Process start to send mamil");
                        MailDetail MailDetailOBJ = new MailDetail();
                        MailDetailOBJ.MailFrom = "wileyus@thomsondigital.com";
                        MailDetailOBJ.MailTo = "amorales@wiley.com";
                        MailDetailOBJ.MailCC = "wileyus@thomsondigital.com";

                        MailDetailOBJ.MailBCC = "wileyproof@wiley.thomsondigital.com";

                        MailDetailOBJ.MailSubject = "Proofs of " + MNT.JID + " " + MNT.AID + " for Approval";
                        MailDetailOBJ.MailBody = _MailBody;

                        MailDetailOBJ.MailAtchmnt.Add(_PDFPath);

                        eMailProcess eMailProcessOBJ = new eMailProcess();
                        eMailProcessOBJ.ProcessNotification += ProcessEventHandler;
                        eMailProcessOBJ.ErrorNotification += ProcessErrorHandler;


                        if (eMailProcessOBJ.SendMailExternal(MailDetailOBJ))
                        {
                            Result = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                this.ProcessErrorHandler(ex);
            }
            return Result;

        }

        public bool AddeProofPdf(string ZipFile, string PdfFile)
        {

            string[] ZipArr = Directory.GetFiles(ConfigDetails.InputFilesForEditor, MNT.JID + "_" + MNT.AID + "*.zip");

            if (ZipArr.Length == 0)
            { 
               ZipArr = Directory.GetFiles(ConfigDetails.InputFilesForEditor, "*_" + MNT.JID + "_FRESH_" + MNT.AID + "*.zip");
            }
            
            if (ZipArr.Length > 0)
            {
                string Source = ZipArr[0];
                string TempPath = "C:\\Temp\\" +  MNT.JID + "_" + MNT.AID ;
                string ZipPath = TempPath + ".zip";

                if (Directory.Exists(TempPath))
                {
                    Directory.Delete(TempPath,true);
                }
                Directory.CreateDirectory(TempPath);

                try
                {
                    string eProofPdf = string.Empty;
                    FastZip unzip1 = new FastZip();
                    unzip1.ExtractZip(Source, TempPath, "");


                    ProcessEventHandler("Unzip done successfully..");

                    string CopyTo = TempPath + "\\" + MNT.JID + "_" + MNT.AID + ".pdf";
                    File.Copy(_PDFPath, CopyTo);


                    unzip1.CreateZip(ZipPath, TempPath, true, "");

                    ProcessEventHandler("Zip file created successfully..");

                    //File.Delete(Source);
                    //File.Move(ZipPath, Source);
                    _PDFPath = ZipPath;
                    ProcessEventHandler("Zip file created successfully..");

                    Directory.Delete(TempPath, true);
                    ProcessEventHandler("Temp Zip file deleted successfully..");
                }
                catch (Exception Ex)
                {
                    ProcessErrorHandler(Ex);
                    Console.WriteLine(Ex.InnerException);
                    Console.WriteLine("Error in extracting zip file" + Ex.Message);
                    return false;
                }
            }
            return true;
        }

        private bool CallJavaJarFile(string source, string destination)
        {
            bool result = false;
            PdfReader reader;
            PdfStamper stamper;
            try
            {
                try
                {
                    reader = new PdfReader(source);
                    reader.RemoveUsageRights();
                    stamper = new PdfStamper(reader, new System.IO.FileStream(destination, System.IO.FileMode.CreateNew));
                    stamper.FormFlattening = true;
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
        public int PdfPages
        {
            get;
            set;
        }

        public int AutoPdfPages
        {
            get;
            set;
        }


        public bool IsPdfPagesEqualAutopage
        {
            get;
            set;
        }
    }

}