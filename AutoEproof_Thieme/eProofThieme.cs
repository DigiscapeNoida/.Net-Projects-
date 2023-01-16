using System;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;
using DatabaseLayer;
using PdfProcess;
using System.Collections.Generic;
using ProcessNotification;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MsgRcvr;
using System.Threading;
namespace AutoEproof
{
    class eProofThieme : MessageEventArgs, IValidation
    {
        string _ServerPath = string.Empty;
        string _ProcessPath = string.Empty;
        string _ExePath = string.Empty;
        string _XMLPath = string.Empty;
        string _PDFPath = string.Empty;
        string _PDFName = string.Empty;
        string _OPSConStr = string.Empty;
        string _MailBody = string.Empty;
        string _CorMailID = string.Empty;
        string _CorDOI = string.Empty;

        string _uid = string.Empty;
        string _pass = string.Empty;

        static string[] _PDffiles = new string[2];

        ArticleInfo _ArticleInfoOBJ = null;
        OPSDetail _OPSDetailObj = null;
        MNTInfo _MNT = null;
        OPSDB _OPSDBObj = null;

        int _PageCount = 0;
        int _MrgPageCount = 0;

        Random rand = new Random();

        public eProofThieme()
        {

            _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            _OPSDBObj = new OPSDB(_OPSConStr);
        }
        public eProofThieme(MNTInfo MNT, InputFiles InputFiles_)
            : this()
        {
            _MNT = MNT;

            _XMLPath = InputFiles_.XMLPath;
            _PDFPath = InputFiles_.PDFPath;

        }


        public eProofThieme(string XMLPath, string PDFPath, MNTInfo MNT)
            : this()
        {
            _MNT = MNT;

            _XMLPath = XMLPath;
            _PDFPath = PDFPath;

            _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            _OPSDBObj = new OPSDB(_OPSConStr);
        }

        public bool ProcessMNT()
        {
            _PageCount = PDF.GetPdfPageCount(_PDFPath);
            bool Result = false;
            ProcessEventHandler("Thieme Process is started");
            try
            {

                if (ThiemePdfProcess())
                {
                    ProcessEventHandler("Pdf Process completed ::JID " + _MNT.JID + " Aid " + _MNT.AID);
                    _MrgPageCount = PDF.GetPdfPageCount(_PDFPath);

                    ProcessEventHandler("Last PageCount :: " + _PageCount);
                    ProcessEventHandler("Last MrgPageCount :: " + _MrgPageCount);

                    if (_MrgPageCount != _PageCount)
                    {
                        ProcessEventHandler("PageCount is not equal to merge pdf");

                        IsPdfProcessError = true;
                        ValidationResult = false;
                        return false;
                    }

                    if (_MNT.JID == "OM" || CreateURL())
                    {
                        ProcessEventHandler("Url created ::JID " + _MNT.JID + " Aid " + _MNT.AID);
                        _MailBody = GetMailBody();

                        ProcessEventHandler("Mail body configured ::JID " + _MNT.JID + " Aid " + _MNT.AID);
                        if (InsertHistory())
                        {
                            ProcessEventHandler("Data inserted ::JID " + _MNT.JID + " Aid " + _MNT.AID);

                            if (ProcessMail())
                            {
                                ProcessEventHandler("Mail is sent ::JID " + _MNT.JID + " Aid " + _MNT.AID);
                                ProcessEventHandler("Completed ::JID " + _MNT.JID + " Aid " + _MNT.AID);
                                Result = true;
                            }
                            else
                            {
                                DeleteHistory();
                                ProcessEventHandler("Mail is not processed ::JID " + _MNT.JID + " Aid " + _MNT.AID);
                            }
                        }
                        else
                        {
                            ProcessEventHandler("Unable to insert data ::JID " + _MNT.JID + " Aid " + _MNT.AID);

                        }

                    }
                    else
                    {
                        ProcessEventHandler("Url not created ::JID " + _MNT.JID + " Aid " + _MNT.AID);
                    }

                }
                else
                {
                    ProcessEventHandler("Pdf Process failled ::JID " + _MNT.JID + " Aid " + _MNT.AID);
                }
                string it = ConfigurationManager.AppSettings["thieme"] + _MNT.JID + "-" + _MNT.AID;
                string ot = ConfigurationManager.AppSettings["thieme"] + "processed\\" + _MNT.JID + "-" + _MNT.AID;
                if (!Directory.Exists(ot))
                {
                    Directory.CreateDirectory(ot);
                }
                File.Copy(it + "\\" + _MNT.JID + "-" + _MNT.AID + ".pdf", ot + "\\" + _MNT.JID + "-" + _MNT.AID + ".pdf", true);
                File.Copy(it + "\\" + _MNT.JID + _MNT.AID + ".xml", ot + "\\" + _MNT.JID + _MNT.AID + ".xml", true);
                Directory.Delete(it, true);
                return Result;
            }
            catch (Exception err)
            {
                ProcessEventHandler(err.Message);
                ProcessEventHandler("Process fail for ::JID " + _MNT.JID + " Aid " + _MNT.AID);
                base.ProcessErrorHandler(err);
                return Result;
            }
        }
        public string PDFPath
        {
            get { return _PDFPath; }
        }
        public string PDFName
        {
            get { return _PDFName; }
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
                _PDFName = CorDetaill.PdfName;

                if (!(_PDFName.ToUpper().Contains(".PDF")))
                {
                    _PDFName = _PDFName + ".pdf";
                }

                if (string.IsNullOrEmpty(_ArticleInfoOBJ.ArticleTitle))
                    _ArticleInfoOBJ.ArticleTitle = CorDetaill.Title;

                if (!string.IsNullOrEmpty(CorDetaill.CorMail))
                {
                    _ArticleInfoOBJ.AuthorEmail = CorDetaill.CorMail;
                    _CorMailID = string.Empty;
                    _CorMailID = CorDetaill.CorMail;
                }

                if (!string.IsNullOrEmpty(CorDetaill.CorName))
                    _ArticleInfoOBJ.AuthorName = CorDetaill.CorName;

                if (!string.IsNullOrEmpty(CorDetaill.CorMailCC))
                    _ArticleInfoOBJ.CorEmailCC = CorDetaill.CorMailCC;

                if (!string.IsNullOrEmpty(CorDetaill.DOI))
                    _CorDOI = CorDetaill.DOI;
            }
            else
            {
                ProcessEventHandler("There is no information about author in CorAuthorDetaill table in OPS database");
            }


            if (string.IsNullOrEmpty(_ArticleInfoOBJ.AuthorName))
                _ArticleInfoOBJ.AuthorName = "Author";

            return;
        }

        private bool ProcessMail()
        {
            try
            {

                MailDetail MailDetailOBJ = new MailDetail();
                //MailDetailOBJ.MailFrom = _OPSDetailObj.FromMail;
                MailDetailOBJ.MailTo = _MNT.JID == "OM" ? "thieme.j@thomsondigital.com" : _ArticleInfoOBJ.AuthorEmail;

                if (!string.IsNullOrEmpty(_ArticleInfoOBJ.CorEmailCC))
                    MailDetailOBJ.MailCC = _MNT.JID == "OM" ? "thieme.j@thomsondigital.com" : _ArticleInfoOBJ.CorEmailCC + "," + _OPSDetailObj.Pe_email;
                else
                    MailDetailOBJ.MailCC = _MNT.JID == "OM" ? "thieme.j@thomsondigital.com" : _OPSDetailObj.Pe_email;

                if (!string.IsNullOrEmpty(_OPSDetailObj.CCMail))
                    MailDetailOBJ.MailCC = _MNT.JID == "OM" ? "thieme.j@thomsondigital.com" : MailDetailOBJ.MailCC + "," + _OPSDetailObj.CCMail;

                MailDetailOBJ.MailBCC = _MNT.JID == "OM" ? "thieme.j@thomsondigital.com" : _OPSDetailObj.BccMail;
                if (_MNT.JID == "TCS" || _MNT.JID == "FPS")
                {
                    MailDetailOBJ.MailSubject = _MNT.JID + " " + _MNT.AID + ": Eproofs from Thieme";
                    MailDetailOBJ.MailFrom = _OPSDetailObj.FromMail;
                }
                else
                {
                    MailDetailOBJ.MailSubject = "Your eProof is now available for " + _MNT.JID + " " + _MNT.AID;
                    MailDetailOBJ.MailFrom = "thieme.j@thomsondigital.com";
                }
                MailDetailOBJ.MailBody = _MailBody;
                if (_MNT.JID == "OM")
                {
                    MailDetailOBJ.MailAtchmnt.Add(PDFPath);
                }
                eMailProcess eMailProcessOBJ = new eMailProcess();
                eMailProcessOBJ.ProcessNotification += ProcessEventHandler;
                eMailProcessOBJ.ErrorNotification += ProcessErrorHandler;

                if (eMailProcessOBJ.SendMailExternal(MailDetailOBJ))
                //if (eMailProcessOBJ.SendMailExternalWithReplyTo(MailDetailOBJ, _OPSDetailObj.Pe_email, _OPSDetailObj.Peditor))
                {
                    try
                    {
                        //Fortest
                        if (_MNT.JID != "OM")
                        {
                            ProcessEventHandler("Process start to send attachment ::JID " + _MNT.JID + " Aid " + _MNT.AID);
                            MailDetailOBJ.MailFrom = "thieme.j@thomsondigital.com";
                            MailDetailOBJ.MailTo = _OPSDetailObj.Pe_email;
                            MailDetailOBJ.MailCC = "thieme.j@thomsondigital.com";
                            MailDetailOBJ.MailBCC = string.Empty;
                            MailDetailOBJ.MailSubject = _MNT.JID + " " + _MNT.AID + " Typeset Proofs";
                            MailDetailOBJ.MailBody = "Dear " + _OPSDetailObj.Peditor + ",\n\nPlease find attached the typeset proofs for your records.\nThe same has also been released to authorvia e-proofing system.\n\n\nRegards,\nThomson Team\n";
                            if (File.Exists(PDFPath))
                            {
                                MailDetailOBJ.MailAtchmnt.Add(PDFPath);
                                if (ConfigurationManager.AppSettings["Attachment"].IndexOf(_OPSDetailObj.Pe_email) != -1)
                                {
                                    eMailProcessOBJ.SendMailExternal(MailDetailOBJ);
                                    ProcessEventHandler("Process completed to send attachment");
                                }
                            }
                            else
                                ProcessEventHandler(PDFPath + ": File does not exist.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ProcessEventHandler("Process fail to send attachment ::JID " + _MNT.JID + " Aid " + _MNT.AID);
                        base.ProcessErrorHandler(ex);
                    }
                    //eMailProcessOBJ.SendMailInternal(MailDetailOBJ);

                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private bool CommentEnablePDF()
        {
            try
            {
                string[] args = new string[1];
                args[0] = _PDFPath;
                PDFAnnotation.Program.Main(args);
                return true;
            }
            catch (Exception ex)
            {
                IsPdfProcessError = true;
                base.ProcessErrorHandler(ex);
                return false;
            }
        }
        public bool IsPdfProcessError
        {
            get;
            set;
        }
        private void InitiallizeAuthorInfo()
        {
            ThiemeXml _ThiemeObj = null;
            try
            {
                _ThiemeObj = new ThiemeXml(_XMLPath);
                _ThiemeObj.ErrorNotification += ProcessErrorHandler;
                _ThiemeObj.ProcessNotification += ProcessEventHandler;
                _ThiemeObj.FillArticleInfo(_ArticleInfoOBJ);  //sachin
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }
            /*
            ArticleXMLProcess ArticleXMLProcessOBJ = new ArticleXMLProcess(_XMLPath);
            _ArticleInfoOBJ                        = ArticleXMLProcessOBJ.ArticleInfoOBJ;
            */
        }
        private bool ThiemePdfProcess()
        {

            try
            {

                string InstructionsPdf = @"C:\AEPS\COPYRIGHT\" + _MNT.Client + "\\" + _MNT.JID + "\\" + _MNT.JID + "Instructions.pdf";
                string OffprintPdf = @"C:\AEPS\COPYRIGHT\" + _MNT.Client + "\\" + _MNT.JID + "\\" + _MNT.JID + "OffprintForm.pdf";
                string CTAFormTemplate = AppDomain.CurrentDomain.BaseDirectory + "\\" + "CTAForm.pdf";

                if (_MNT.JID == "OM")
                {
                    InstructionsPdf = "";
                    //CTAFormTemplate = AppDomain.CurrentDomain.BaseDirectory + "\\" + "CTAFormOM.pdf";
                }
                //Jitender 16 Aug 2016
                //if (_MNT.JID.ToUpper() == "IJNS" || _MNT.JID.ToUpper() == "ABN" || _MNT.JID.ToUpper() == "IAO" || _MNT.JID.ToUpper() == "RBGO" || _MNT.JID.ToUpper() == "RCHOT" || _MNT.JID.ToUpper() == "JOINTS" || _MNT.JID.ToUpper() == "JCIR" || _MNT.JID.ToUpper() == "CMTRO" || _MNT.JID.ToUpper() == "RICMA" || _MNT.JID.ToUpper() == "JCS" || _MNT.JID.ToUpper() == "JCCC")
                //{
                //    CTAFormTemplate = AppDomain.CurrentDomain.BaseDirectory + "\\" + "IJNS_CTAForm.pdf";
                //}
                //added on 26 June 2017   by Pradeep
                //if (ConfigurationManager.AppSettings["OPENACCESSJID"].IndexOf(_MNT.JID.ToUpper()) != -1)
                if (CtaForm(_MNT.JID.ToUpper(), ConfigurationManager.AppSettings["OPENACCESSJID"].ToString()))
                {
                    //CTAFormTemplate = AppDomain.CurrentDomain.BaseDirectory + "\\" + "IJNS_CTAForm.pdf";
                    CTAFormTemplate = "";
                }
                //if (_MNT.JID == "RBGO")
                //{
                //    CTAFormTemplate = AppDomain.CurrentDomain.BaseDirectory + "\\" + "RBGO_CTAForm.pdf";
                //}
                //if (ConfigurationManager.AppSettings["StandardCTA"].ToString().Contains(_MNT.JID.ToUpper()))
                if (CtaForm(_MNT.JID.ToUpper(), ConfigurationManager.AppSettings["StandardCTA"].ToString()))
                {
                    CTAFormTemplate = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Standardcopyrightform.pdf";
                }
                //if (ConfigurationManager.AppSettings["CCBYNCND"].ToString().Contains(_MNT.JID.ToUpper()))
                if (CtaForm(_MNT.JID.ToUpper(), ConfigurationManager.AppSettings["CCBYNCND"].ToString()))
                {
                    CTAFormTemplate = AppDomain.CurrentDomain.BaseDirectory + "\\" + "CCBYNCNDcopyrightform.pdf";
                }
                //if (ConfigurationManager.AppSettings["CCBY"].ToString().Contains(_MNT.JID.ToUpper()))
                if (CtaForm(_MNT.JID.ToUpper(), ConfigurationManager.AppSettings["CCBY"].ToString()))
                {
                    CTAFormTemplate = AppDomain.CurrentDomain.BaseDirectory + "\\" + "CCBYcopyrightform.pdf";
                }
                if (CtaForm(_MNT.JID.ToUpper(), ConfigurationManager.AppSettings["DYFORM"].ToString()))
                {
                    string xl = File.ReadAllText(_XMLPath);
                    if (xl.Contains("open-access=\"yes\""))
                    {
                        if (xl.Contains("license-type=\"CC BY-NC-ND 4.0\""))
                        {
                            CTAFormTemplate = AppDomain.CurrentDomain.BaseDirectory + "\\" + "CCBYNCNDcopyrightform.pdf";
                        }
                        if (xl.Contains("license-type=\"CC BY 4.0\""))
                        {
                            CTAFormTemplate = AppDomain.CurrentDomain.BaseDirectory + "\\" + "CCBYcopyrightform.pdf";
                        }
                    }
                }
                if (!File.Exists(CTAFormTemplate))
                {
                    ProcessEventHandler("Template not found : " + CTAFormTemplate);
                    if (CtaForm(_MNT.JID.ToUpper(), ConfigurationManager.AppSettings["OPENACCESSJID"].ToString()))
                    {
                    }
                    else
                    {
                        return false;
                    }
                }
                ProcessEventHandler(CTAFormTemplate);

                PdfProcess.PDF PDFObj = new PDF(_PDFPath, MNT);

                PDFObj.ProcessNotification += ProcessEventHandler;
                PDFObj.ErrorNotification += ProcessErrorHandler;

                string afterWMark = _PDFPath.Replace(".pdf", "_2.pdf");
                PDFObj.AddThiemeWatermark(_PDFPath, afterWMark);


                if (File.Exists(afterWMark))
                {
                    File.Delete(_PDFPath);
                    File.Move(afterWMark, _PDFPath);
                }

                if (!(CommentEnablePDF()))                       //3:Annotate
                {
                    ProcessEventHandler("Error while CommentEnable");
                    return false;
                }


                if (File.Exists(OffprintPdf) || File.Exists(InstructionsPdf))
                {
                    string CTAFormPdfPath = "";
                    if (CTAFormTemplate !="")
                    {
                        CTAFormPdfPath = FillCTAForm(CTAFormTemplate);

                        if (!string.IsNullOrEmpty(CTAFormPdfPath) && File.Exists(CTAFormPdfPath))
                            ProcessEventHandler("Form Pdf is fill properly");
                        else
                            return false;
                    }
                    ProcessEventHandler("_PageCount :: " + _PageCount.ToString());

                    if (File.Exists(OffprintPdf))
                    {
                        _PageCount += PDF.GetPdfPageCount(OffprintPdf);

                        ProcessEventHandler("OffprintPdf + _PageCount :: " + _PageCount.ToString());


                        PDFObj.MergePDF(new string[2] { OffprintPdf, _PDFPath });
                        ProcessEventHandler("OffprintPdf merged successfully");
                    }

                    if (File.Exists(CTAFormPdfPath))
                    {
                        _PageCount += PDF.GetPdfPageCount(CTAFormPdfPath);

                        ProcessEventHandler("CTAFormPdfPath + _PageCount :: " + _PageCount.ToString());

                        if (!String.IsNullOrEmpty(InstructionsPdf))
                            _PageCount += PDF.GetPdfPageCount(InstructionsPdf);

                        ProcessEventHandler("InstructionsPdf + _PageCount :: " + _PageCount.ToString());

                        PDFObj.ProcessNotification += ProcessEventHandler;
                        PDFObj.ErrorNotification += ProcessErrorHandler;

                        ProcessEventHandler("CTAFormPdfPath = " + CTAFormPdfPath);
                        string[] args1 = new string[3];
                        args1[0] = CTAFormPdfPath;
                        args1[1] = _PDFPath;
                        args1[2] = "ThiemeCTAMerge";
                        PDFAnnotation.Program.Main(args1);
                        Thread.Sleep(15000);
                        //PDFObj.MergePDF(new string[2] { _PDFPath, CTAFormPdfPath });

                        ProcessEventHandler("ThiemeCTAMerge Complete");
                    }

                    if (_MNT.JID != "OM")
                    {
                        if (File.Exists(CTAFormPdfPath) && File.Exists(InstructionsPdf))
                        {
                            //InstructionsPdf = _MNT.JID == "OM" ? "" : InstructionsPdf;
                            string[] args = new string[3];
                            args[0] = CTAFormPdfPath;
                            args[1] = InstructionsPdf;
                            args[2] = "ThiemeInstructionMerge";
                            PDFAnnotation.Program.Main(args);
                            ProcessEventHandler("ThiemeInstructionMerge Complete");
                            Thread.Sleep(3000);
                        }
                        else
                        {
                            if (!File.Exists(CTAFormPdfPath) && File.Exists(InstructionsPdf))
                            {
                                //string[] args2 = new string[3];
                                //args2[0] = _PDFPath;
                                //args2[1] = InstructionsPdf;
                                //args2[2] = "ThiemeInstructionMerge";
                                //PDFAnnotation.Program.Main(args2);
                                //ProcessEventHandler("ThiemeInstructionMerge Complete");
                                PDFObj.MergePDF(new string[2] { InstructionsPdf, _PDFPath });
                                Thread.Sleep(3000);
                                _PageCount += PDF.GetPdfPageCount(InstructionsPdf);
                            }
                        }
                    }

                    if (File.Exists(CTAFormPdfPath))
                    {
                        ProcessEventHandler("Trying to delete" + _PDFPath);
                        File.Delete(_PDFPath);


                        while (File.Exists(_PDFPath))
                        {
                            ProcessEventHandler(_PDFPath + " Waitt for file deleteiom complete..");
                        }

                        ProcessEventHandler("CTAFormPdf" + CTAFormPdfPath);
                        ProcessEventHandler("_PDFPath" + _PDFPath);
                        while (true)
                        {
                            try
                            {
                                File.Move(CTAFormPdfPath, _PDFPath);
                                break;
                            }
                            catch (Exception ex)
                            {
                                ProcessErrorHandler(ex);
                            }
                        }
                        ProcessEventHandler("CTAFormPdfPath Complete");
                    }
                }
                else
                {
                    string JIDPdf = @"C:\AEPS\COPYRIGHT\" + _MNT.Client + "\\" + _MNT.JID + "\\" + _MNT.JID + ".pdf";
                    if (File.Exists(JIDPdf))
                    {
                        _PageCount += PDF.GetPdfPageCount(JIDPdf);

                        PDFObj.MergePDF(new string[2] { JIDPdf, _PDFPath });

                        ProcessEventHandler("COPYRIGHT JIDPdf + _PageCount :: " + _PageCount.ToString());
                    }
                }

                ProcessEventHandler("Files are merged as " + _PDFPath);

                return true;

            }
            catch (Exception err)
            {
                ProcessEventHandler("Error while pdf process :" + err.ToString());

                base.ProcessErrorHandler(err);
                return false;
            }
        }

        private bool CtaForm(string jid, string srch)
        {
            bool ret = false;
            try
            {
                string[] jids = srch.Split(',');
                foreach (string jd in jids)
                {
                    if (jid.Trim().ToUpper() == jd.Trim().ToUpper())
                    {
                        ret = true;
                        break;
                    }
                }
            }
            catch { }
            return ret;
        }
        private string GetMailBody()
        {


            string TemplatePath = ConfigDetails.TemplatePathThieme + "\\" + _OPSDetailObj.TemplateFileName;

            string NewAid = PDFName.Replace(".pdf", "");
            if (NewAid == "")
            {
                NewAid = _MNT.AID;
            }

            // string url = "http://eproof1.thomsondigital.com/" + _MNT.Client + "/" + _MNT.JID + "/" + NewAid + "/" + NewAid + ".pdf";
            string url = "https://eproof1.thomsondigital.com/download.aspx?p=" + NewAid;

            StringBuilder MailBody = new StringBuilder("");
            MailBody = new StringBuilder(File.ReadAllText(TemplatePath));

            if (_MNT != null)
            {
                MailBody.Replace("<JID>", _MNT.JID);
                MailBody.Replace("<AID>", _MNT.AID);
                MailBody.Replace("<PNO>", _MNT.AID);
                MailBody.Replace("<USER>", _uid);//  _MNT.AID);   
                MailBody.Replace("<PASSWORD>", _pass);//_MNT.AID);
                MailBody.Replace("<DOI>", _MNT.AID);

                //SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString);
                //scon.Open();
                //SqlDataAdapter da = new SqlDataAdapter("Select * from wileywip where JID='" + _MNT.JID + "' and AID='" + _MNT.AID + "' and STAGE=S100", scon);

                //DataSet ds = new DataSet();
                //da.Fill(ds);
                //scon.Close();
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    MailBody.Replace("<VOL>", ds.Tables[0].Rows[0]["VOL"].ToString());
                //}




            }



            if (_ArticleInfoOBJ != null)
                MailBody.Replace("<ART_TITLE>", _ArticleInfoOBJ.ArticleTitle);


            MailBody.Replace("<ART_TITLE>", "");

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
                MailBody.Replace("<ThomsonContact>", _OPSDetailObj.InternalPEName);

                MailBody.Replace("<PEFAX>", _OPSDetailObj.Fax);
                if (_OPSDetailObj.Jid == "TH" || _OPSDetailObj.Jid == "VCOT")
                {
                    string address = string.Empty;
                    string[] stradd = _OPSDetailObj.Address.Split('$');
                    if (stradd.Length > 0)
                    {
                        foreach (string straddress in stradd)
                        {
                            address = address + straddress + Environment.NewLine;
                        }
                    }
                    MailBody.Replace("<ADDRESS>", address);
                    MailBody.Replace("<PEADDRESS>", _OPSDetailObj.Address.Replace("$", ", "));
                }
                else
                    MailBody.Replace("<PEADDRESS>", _OPSDetailObj.Address);
                MailBody.Replace("<PEPHONE>", _OPSDetailObj.Phone);

            }
            return MailBody.ToString();
        }

        private bool CreateURL()
        {
            ProcessEventHandler("Create url called");
            URLService.CreateEproofURL URLObj = new URLService.CreateEproofURL();
            string pdf = UploadFile(URLObj, _PDFPath);
            string tempAid = PDFName.Replace(".pdf", "");

            if (!string.IsNullOrEmpty(pdf))
            {
                _uid = GetUserName();
                _pass = GetPassword();


                if (_uid != "" && _pass != "")
                {
                    try
                    {
                        ProcessEventHandler("Create url " + _MNT.Client + _MNT.JID + tempAid + pdf + _uid + _pass);
                        URLObj.CreateURL(_MNT.Client, _MNT.JID, tempAid, pdf, _uid, _pass);
                        //=========================================================================
                        string aaid = tempAid.Substring(tempAid.IndexOf("-") + 1);
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString);
                        SqlDataAdapter da = new SqlDataAdapter("select * from thieme_download where jid='" + _MNT.JID + "' and aid='" + aaid + "'", con);
                        DataTable dt = new DataTable();
                        con.Open();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            SqlCommand cmd = new SqlCommand("update thieme_download set uid='" + _uid + "', pwd='" + _pass + "' where jid='" + _MNT.JID + "' and aid='" + aaid + "'", con);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd1 = new SqlCommand("insert into thieme_download (jid, aid, uid, pwd) values ('" + _MNT.JID + "', '" + aaid + "', '" + _uid + "', '" + _pass + "')", con);
                            cmd1.ExecuteNonQuery();
                        }
                        con.Close();
                        //===============================================================================
                    }
                    catch (Exception err)
                    {
                        base.ProcessErrorHandler(err);
                        ProcessEventHandler("Exception" + err.ToString());
                    }
                }
                else
                {
                    return false;
                }
            }
            else
                return false;

            return true;
        }
        private string UploadFile(URLService.CreateEproofURL EproofURL, string filename)
        {
            try
            {
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
                if (true)
                {
                    // set up a file stream and binary reader for the 
                    // selected file
                    FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fStream);

                    // convert the file to a byte array
                    byte[] data = br.ReadBytes((int)numBytes);
                    br.Close();

                    // pass the byte array (file) and file name to the web service
                    string sTmp = EproofURL.UploadFile(data, strFile);
                    //string TempDir = "C:\\Temp\\";
                    //if (!Directory.Exists(TempDir))
                    //{
                    //    Directory.CreateDirectory(TempDir);
                    //}
                    //string ProcessFile = TempDir + DateTime.Now.Ticks.ToString() + "_" + Path.GetFileName(filename);

                    //// the byte array argument contains the content of the file
                    //// the string argument contains the name and extension
                    //// of the file passed in the byte array
                    //    // instance a memory stream and pass the
                    //    // byte array to its constructor
                    //    MemoryStream ms = new MemoryStream(data);
                    //    // instance a filestream pointing to the 
                    //    // storage folder, use the original file name
                    //    // to name the resulting file
                    //    FileStream fs = new FileStream(ProcessFile, FileMode.Create);
                    //    // write the memory stream containing the original
                    //    // file as a byte array to the filestream
                    //    ms.WriteTo(fs);
                    //    // clean up
                    //    ms.Close();
                    //    fs.Close();
                    //    fs.Dispose();
                    // return OK if we made it this far
                    fStream.Close();
                    fStream.Dispose();

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
            }
            catch (Exception ex)
            {
                base.ProcessErrorHandler(ex);
                // display an error message to the user
                //MessageBox.Show(ex.Message.ToString(), "Upload Error");
            }
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
                FTPURL = FtpDtl.FtpPath + "/" + _MNT.JID + "/Fresh Proofs/" + DateFolder;
                Uname = FtpDtl.FtpUID;
                PWD = FtpDtl.ftpPWD;
            }
            else
            {
                return false;
            }


            //string DateFolder = DateTime.Today.ToString("dd-MM-yyyy");
            //string FTPURL = "ftp://sg-ftp.blackwellpublishing.com/From Thomson/"+ _MNT.JID + "/Fresh Proofs/" +DateFolder;
            //string Uname  = "Thomson";
            //string PWD    = "blackpass";

            string _UploadFileName = Path.GetDirectoryName(_PDFPath) + "\\" + _MNT.JID.ToLower() + "_" + _MNT.AID + ".pdf";

            File.Move(_PDFPath, _UploadFileName);

            try
            {
                FtpProcess FtpObj = new FtpProcess(FTPURL, Uname, PWD);
                FtpObj.ProcessNotification += ProcessEventHandler;
                FtpObj.ErrorNotification += ProcessErrorHandler;


                if (FtpObj.FtpDirectoryExists(FTPURL) == false)
                {
                    FtpObj.CreateFtpFolder(FTPURL);
                }

                if (FtpObj.UploadFileToFTP(_UploadFileName) == false)
                {
                    FtpObj.UploadFileToFTP(_UploadFileName);
                }
            }
            catch (Exception ex)
            {
                base.ProcessErrorHandler(ex);
                return false;
            }

            return true;
        }

        private void InsertReminder()
        {
            string RmndrConStr = ConfigurationManager.ConnectionStrings["ReminderConnectionString"].ConnectionString;
            string Uploadpath = "P:\\" + _MNT.Client + "\\" + _MNT.JID + "\\" + _MNT.AID;
            string InsertCmdString = "Insert into reminder_data (cclient, cJournal_id, cArticle_id, cUsername, cPassword, vDOI,  vJournal_name, email_from, email_to_author, email_to_editor, email_to_bcc,email_to_cc,final_cc,ConfirmMailSend,UploadPath) " +
                                               " VALUES ('" + _MNT.Client + "', '" + _MNT.JID + "', '" + _MNT.AID + "', '" + _MNT.AID + "', '" + _MNT.AID + "', '" + _MNT.AID + "', '" + _OPSDetailObj.Jname + "', '" + _OPSDetailObj.FromMail + "', '" + _ArticleInfoOBJ.AuthorEmail + "', '" + _OPSDetailObj.Pe_email + "', '" + _OPSDetailObj.BccMail + "', '" + _OPSDetailObj.CCMail + "', '" + _OPSDetailObj.final_cc + "','False','" + Uploadpath + "')";

            SqlHelper.ExecuteNonQuery(RmndrConStr, System.Data.CommandType.Text, InsertCmdString);
        }

        private bool InsertHistory()
        {
            try
            {
                eProofHistory eHstryObj = new eProofHistory();
                eHstryObj.OPSID = _OPSDetailObj.OPSID;
                eHstryObj.AID = _MNT.AID;
                eHstryObj.ArticleTitle = _ArticleInfoOBJ.ArticleTitle;
                eHstryObj.CorrName = _ArticleInfoOBJ.AuthorName;
                eHstryObj.MailFrom = _OPSDetailObj.FromMail;
                eHstryObj.MailTo = _ArticleInfoOBJ.AuthorEmail;
                //eHstryObj.MailCC = _OPSDetailObj.CCMail;

                if (string.IsNullOrEmpty(_ArticleInfoOBJ.CorEmailCC))
                    eHstryObj.MailCC = _OPSDetailObj.CCMail;
                else if (!string.IsNullOrEmpty(_ArticleInfoOBJ.CorEmailCC) && !string.IsNullOrEmpty(_OPSDetailObj.CCMail))
                    eHstryObj.MailCC = _ArticleInfoOBJ.CorEmailCC.Replace(';', ',') + "," + _OPSDetailObj.CCMail;
                else if (!string.IsNullOrEmpty(_ArticleInfoOBJ.CorEmailCC))
                    eHstryObj.MailCC = _ArticleInfoOBJ.CorEmailCC.Replace(';', ',');

                eHstryObj.MailBCC = _OPSDetailObj.BccMail;
                eHstryObj.DOI = _ArticleInfoOBJ.DOI;

                _OPSDBObj.InsertEproofHistory(eHstryObj);

                return true;
            }
            catch (Exception err)
            {
                ProcessEventHandler("Error while Inserting history " + err.ToString());
                base.ProcessErrorHandler(err);
                return false;
            }
        }

        private bool DeleteHistory()
        {

            try
            {
                eProofHistory eHstryObj = new eProofHistory();
                eHstryObj.OPSID = _OPSDetailObj.OPSID;
                eHstryObj.AID = _MNT.AID;
                _OPSDBObj.DeleteEproofHistory(eHstryObj);

                return true;
            }
            catch (Exception err)
            {
                ProcessEventHandler("Error while Delete history " + err.ToString());
                base.ProcessErrorHandler(err);
                return false;
            }

        }

        public bool IsValidJID
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
        public bool IsCDCArticle
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
        public void StartValidation()
        {
            ProcessEventHandler("Intialize Process Start");
            Intialize();
            ProcessEventHandler("Intialize Process End");


            ProcessEventHandler("Geting PE Details from  OPSDetails");
            _OPSDetailObj = _OPSDBObj.GetOPSDetails(_MNT.JID, _MNT.Client);
            ProcessEventHandler("Intialize Process Start");


            if (_ArticleInfoOBJ.AuthorEmail.IndexOf("thomsondigital", StringComparison.OrdinalIgnoreCase) != -1)
            {
                _ArticleInfoOBJ.AuthorEmail = string.Empty;
            }

            PdfPages = MNT.PdfPages;
            AutoPdfPages = MNT.PgCountLog;

            if (AutoPdfPages > 0 && (PdfPages > AutoPdfPages))
                IsPdfPagesEqualAutopage = false;
            else if (AutoPdfPages > 0 && (AutoPdfPages >= PdfPages - 1))
                IsPdfPagesEqualAutopage = true;
            else if (AutoPdfPages == 0)
                IsPdfPagesEqualAutopage = true;
            //if (_ArticleInfoOBJ.CorEmail == _CorMailID)
            if (!String.IsNullOrEmpty(_ArticleInfoOBJ.CorEmail))
            {
                string[] CorMailID = _ArticleInfoOBJ.CorEmail.Split(',');
                if (CorMailID.Length > 1)
                {
                    if (_ArticleInfoOBJ.CorEmail.Split(',')[0] == _CorMailID || _ArticleInfoOBJ.CorEmail.Split(',')[1].Trim() == _ArticleInfoOBJ.CorEmailCC)
                        IsMatchCorEmailXMLANDDB = true;
                }
                else if (_ArticleInfoOBJ.CorEmail.Split(',')[0] == _CorMailID || _ArticleInfoOBJ.CorEmail.Split(',')[0].Trim() == _ArticleInfoOBJ.CorEmailCC)
                    IsMatchCorEmailXMLANDDB = true;
                else
                    IsMatchCorEmailXMLANDDB = false;
            }
            IsMatchCorEmailXMLANDDB = _MNT.JID == "OM" ? true : IsMatchCorEmailXMLANDDB;

            IsAuthorEmailExist = string.IsNullOrEmpty(_ArticleInfoOBJ.AuthorEmail) ? false : true;
            IsArticleTitleExist = true;
            IsAuthorNameExist = true;
            IsAuthorEMailWellForm = CheckAuthorEmail(_ArticleInfoOBJ.AuthorEmail);

            IsAlreadyProcessed = _OPSDBObj.CheckeProofExistence(_OPSDetailObj.OPSID, _MNT.AID);

            IsValidJID = _OPSDetailObj == null ? false : true;
            if (_ArticleInfoOBJ.DOI == _CorDOI)
                IsMatchDOI = true;
            else
                IsMatchDOI = false;
            AssignValidationResult();

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
            ProcessEventHandler("Assigning Value" + ValidationResult + "asdasd" + IsAuthorEmailExist);

            ValidationResult = IsAuthorEmailExist;// Rohit 2021
            //ValidationResult = true;

            if (ValidationResult)
            {
                ValidationResult = IsValidJID;
                ProcessEventHandler("JID AID" + ValidationResult);
            }

            if (ValidationResult)
            {
                ValidationResult = IsAlreadyProcessed ? false : true;
                ProcessEventHandler("Already Processed" + ValidationResult);
            }

            if (ValidationResult)
            {
                ValidationResult = IsArticleTitleExist;
                ProcessEventHandler("IsArticleTitleExist" + ValidationResult);
            }

            if (ValidationResult)
            {
                ValidationResult = IsAuthorEmailExist;
                ProcessEventHandler("IsAuthorEmailExist" + ValidationResult);
            }

            if (ValidationResult)
            {
                ValidationResult = IsAuthorEMailWellForm;
                ProcessEventHandler("IsAuthorEMailWellForm" + ValidationResult);
            }

            if (ValidationResult)
            {
                ValidationResult = IsAuthorNameExist;
                ProcessEventHandler("IsAuthorNameExist" + ValidationResult);
            }

            if (ValidationResult)
            {
                ValidationResult = IsPdfPagesEqualAutopage;
                ProcessEventHandler("IsPdfPagesEqualAutopage" + ValidationResult);
            }

            if (ValidationResult)
            {
                ValidationResult = IsMatchCorEmailXMLANDDB;
                ProcessEventHandler("IsCorEmailfromXMLEqualCorEmailinDataBase" + ValidationResult);
            }
            if (ValidationResult)
            {
                ValidationResult = IsMatchDOI;
                ProcessEventHandler("IsDOIMatch" + ValidationResult);
            }
        }

        private string FillCTAForm(string FormTemplate)
        {

            StringBuilder Log = new StringBuilder();


            //ThiemeXml      _ThiemeObj = null;
            PDFFormProcess _PDFProObj = null;

            try
            {
                string fname = Path.GetFileName(FormTemplate);
                //ProcessEventHandler("fname = " + fname);
                string CTAFormPdfPath = Path.GetDirectoryName(_PDFPath) + "\\" + fname;
                //ProcessEventHandler("newCTAFormPdfPath = " + newCTAFormPdfPath);
                ProcessEventHandler("_PDFPath = " + _PDFPath);
                //string CTAFormPdfPath = Path.GetDirectoryName(_PDFPath) + "\\CTAForm.pdf";
                ProcessEventHandler("IsDOIMatch CTAFormPdfPath = " + CTAFormPdfPath);
                if (File.Exists(CTAFormPdfPath))
                {
                    File.Delete(CTAFormPdfPath);
                    File.Copy(FormTemplate, CTAFormPdfPath);
                }
                else
                {
                    File.Copy(FormTemplate, CTAFormPdfPath);
                }


                _ArticleInfoOBJ.JID = _MNT.JID;
                _ArticleInfoOBJ.AID = _MNT.AID;
                _ArticleInfoOBJ.Publisher = _OPSDetailObj.Peditor + ", " + _OPSDetailObj.Pe_email;
                _ArticleInfoOBJ.PEName = _OPSDetailObj.Peditor;
                _ArticleInfoOBJ.PEEmail = _OPSDetailObj.Pe_email;

                /* Already Assigned in Author Info  method
                _ThiemeObj = new ThiemeXml( _XMLPath);
                _ThiemeObj.ErrorNotification   +=  ProcessErrorHandler;
                _ThiemeObj.ProcessNotification += ProcessEventHandler;
                _ThiemeObj.FillArticleInfo(_ArticleInfoOBJ);  //sachin
                 * **/
                _PDFProObj = new PDFFormProcess(CTAFormPdfPath, _ArticleInfoOBJ);


                string result = _PDFProObj.ProcessOnPDF();
                if (result == "Yes")
                {
                    return CTAFormPdfPath;
                    Console.WriteLine("Form pdf is filled properly");
                }
                else
                {
                    Console.WriteLine("Form pdf is not filled properly");

                }
            }
            catch (Exception Err)
            {
                base.ProcessErrorHandler(Err);

            }
            return string.Empty;
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

        public string Remark
        {
            get;
            set;
        }

        public string XMLPath
        {
            get { return _XMLPath; }
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
