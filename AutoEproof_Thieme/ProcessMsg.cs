using System;
using System.Text;
using System.Data.SqlClient;
using ProcessNotification;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using MsgRcvr;
using DatabaseLayer;

namespace AutoEproof
{
    class ProcessMsg : MessageEventArgs
    {
        bool _IsSuccess = false;
        bool _PrcsRslt = false;
        public bool IsSuccess
        {
            get { return _IsSuccess; }
        }

        List<MNTInfo> MNTList = new List<MNTInfo>();

        string _OPSConStr = string.Empty;
        OPSDB _OPSDBObj = null;

        List<MessageDetail> _MsgList = null;



        public List<MessageDetail> MsgList
        {
            get { return _MsgList; }
        }

        public ProcessMsg()
        {
            _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            _OPSDBObj = new OPSDB(_OPSConStr);
        }
        public bool GetMsgList()
        {
            if (string.IsNullOrEmpty(_OPSConStr))
            {
                ProcessEventHandler("OPS database Connection String is not set");
                _IsSuccess = false;
            }

            ProcessEventHandler("OPS Database initializion process start");
            try
            {
                ProcessEventHandler("OPS Database initialize successfully..");

                //if (Inialize())
                //{
                //    _IsSuccess = true;
                //}

                ProcessEventHandler("Getting InProcess Message Detail");
                InProcessMessageDetail();
                _IsSuccess = true;
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }






            return _IsSuccess;
        }
        public void ProcessTest(string Client, string JID, string AID, string Stage)
        {
            MNTInfo MNT = new MNTInfo(Client, JID, AID, Stage);
            ProcessEventHandler("Getting input files");
            GetInput GetInputOBj = new GetInput(MNT);

            GetInputOBj.ProcessNotification += ProcessEventHandler;
            GetInputOBj.ErrorNotification += this.ProcessErrorHandler;
            if (GetInputOBj.CopyXMLPDFFromServerOrFMS())
            {

                ProcessEventHandler("JID :: " + MNT.JID);
                ProcessEventHandler("AID :: " + MNT.AID);
                ProcessEventHandler("Stage :: " + MNT.Stage);

                ProcessEventHandler("Copy XML and PDF From Server Or FMS has been copied.");
                InputFiles InputFilesObj = new InputFiles(GetInputOBj.PDFPath, GetInputOBj.XMLPath);

                if (MNT.Stage.EndsWith("100"))
                {
                    ProcessFresh(MNT, InputFilesObj);

                    NIHProcess NIHObj = new NIHProcess(InputFilesObj.XMLPath, MNT);
                    NIHObj.ProcessNotification += ProcessEventHandler;
                    NIHObj.ErrorNotification += this.ProcessErrorHandler;
                    NIHObj.StartProess();
                }
                else if (MNT.Stage.EndsWith("200"))
                    ProcessRevise(MNT, InputFilesObj);
                else if (MNT.Stage.StartsWith("S275"))
                    ProcessFAX(MNT, InputFilesObj);
            }
        }
        private bool Inialize()
        {
            ProcessEventHandler("Connecting message queue to get messages.");

            try
            {

                MsgRcvr.MsgPublisherDtl MsgPubObj = new MsgPublisherDtl();
                MsgPubObj.IPAddress = ConfigurationSettings.AppSettings["IP"];
                MsgPubObj.UName = ConfigurationSettings.AppSettings["UID"];
                MsgPubObj.Password = ConfigurationSettings.AppSettings["PWD"];
                MsgPubObj.QName = ConfigurationSettings.AppSettings["QName"];


                ProcessEventHandler("Connecting message from queue.");
                MsgRcvr.MessageRcvr Msg = new MessageRcvr();

                Msg.ProcessNotification += ProcessEventHandler;
                Msg.ErrorNotification += ProcessErrorHandler;

                Msg.StartProcess(MsgPubObj);
                MNTList = Msg.MsgList;

                if (MNTList != null)
                    ProcessEventHandler("Message to be processed :: " + MNTList.Count);


                ProcessEventHandler("InsertMessageDetail");
                InsertMessageDetail(MsgPubObj.IPAddress);

            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
                return false;
            }
            return true;
        }

        public void ProcessArticles()
        {
            foreach (MessageDetail MsgDtl in _MsgList)
            {
                     MNTInfo MNT = new MNTInfo(MsgDtl.Client, MsgDtl.JID, MsgDtl.AID, MsgDtl.Stage);

                    MNT.Status = MsgDtl.Status;

                    ProcessEventHandler("Getting input files");

                    GetInput GetInputOBj = new GetInput(MNT);
                    GetInputOBj.ProcessNotification += ProcessEventHandler;
                    GetInputOBj.ErrorNotification += ProcessErrorHandler;


                    int PageCount = 0;


                    if (MNT.Status.Contains("FinalQC"))
                    {
                        continue;
                    }
                    else if (MNT.Stage.Contains("280"))
                    {
                    }
                    else if (MNT.Stage.Contains("280"))
                    {
                        //if (MsgDtl.IP != null && MsgDtl.IP.Contains(ConfigDetails.TDXPSNoidaIP))
                        //{
                        //    if (Program.SendMsgToNoida(MsgDtl.Client, MsgDtl.JID, MsgDtl.AID))
                        //    {
                        //        ProcessEventHandler(MNT.Stage + " Pagination complete messsage send for " + MsgDtl.JID + "_" + MsgDtl.AID);
                        //        _OPSDBObj.UpdateMessageDetal(MsgDtl.MsgID, PageCount);
                        //    }
                        //}
                        //else if (MsgDtl.IP != null)
                        //{
                        //    string [] IP1=ConfigDetails.TDXPSGangtokIP.Split('.');
                        //    string [] IP2=MsgDtl.IP.Split('.');
                        //    if (IP1.Length>1 && IP1.Length>1)
                        //    {
                        //        if (IP1[IP1.Length-1].Equals(IP2[IP2.Length-1]))
                        //        {
                        //            if (Program.SendMsgToGang(MsgDtl.Client, MsgDtl.JID, MsgDtl.AID))
                        //            {
                        //                ProcessEventHandler(MNT.Stage + " Pagination complete messsage send for " + MsgDtl.JID + "_" + MsgDtl.AID);
                        //                _OPSDBObj.UpdateMessageDetal(MsgDtl.MsgID, PageCount);
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    else if (GetInputOBj.CopyXMLPDFFromServerOrFMS())
                    {
                        _PrcsRslt = true;
                        ProcessEventHandler("JID :: " + MNT.JID);
                        ProcessEventHandler("AID :: " + MNT.AID);
                        ProcessEventHandler("Stage :: " + MNT.Stage);

                        ProcessEventHandler("Copy XML and PDF From Server Or FMS has been copied.");

                        InputFiles InputFilesObj = new InputFiles(GetInputOBj.PDFPath, GetInputOBj.XMLPath);

                        if (File.Exists(InputFilesObj.PDFPath))
                        {
                            //PageCount = PdfProcess.PDF.GetPdfPageCount(InputFilesObj.PDFPath);
                            MNT.PdfPages = GetInputOBj.PdfPageCount;
                            PageCount = MNT.PdfPages;
                        }

                        try
                        {
                            string[] IP1 = ConfigDetails.TDXPSGangtokIP.Split('.');
                            string[] IP2 = MsgDtl.IP.Replace(":61616", "").Split('.');

                            ProcessEventHandler("IP1 : " + IP1[IP1.Length - 1]);
                            ProcessEventHandler("IP2 : " + IP2[IP2.Length - 1]);

                            if (IP1[IP1.Length - 1].Equals(IP2[IP2.Length - 1]))
                            {
                                string PgCountLog = string.Empty;
                                string TempPgCountLog = @"\\" + ConfigDetails.TDXPSGangtokIP + @"\temp\files\MNT_CLIENT_JOURNAL_JID_AID_110\PgCount.log".Replace("CLIENT", MNT.Client).Replace("JID", MNT.JID).Replace("AID", MNT.AID);
                                string WrkAraPgCountLog = @"\\" + ConfigDetails.TDXPSGangtokIP + @"\d$\fms\centralized_server\CLIENT\JOURNAL\JID\AID\WorkArea\MNT_CLIENT_JOURNAL_JID_AID_110\PgCount.log".Replace("CLIENT", MNT.Client).Replace("JID", MNT.JID).Replace("AID", MNT.AID);

                                if (File.Exists(TempPgCountLog))
                                {
                                    PgCountLog = File.ReadAllText(TempPgCountLog);
                                    ProcessEventHandler("TempPgCountLog : " + TempPgCountLog);
                                }
                                else if (File.Exists(WrkAraPgCountLog))
                                {
                                    PgCountLog = File.ReadAllText(WrkAraPgCountLog);
                                    ProcessEventHandler("WrkAraPgCountLog : " + WrkAraPgCountLog);
                                }
                                else
                                {
                                    ProcessEventHandler("PgCount file is not accessible.");
                                    //continue;
                                    MNT.PgCountLog = GetInputOBj.PdfPageCount;
                                }
                                if (!string.IsNullOrEmpty(PgCountLog))
                                {
                                    PgCountLog = PgCountLog.Replace("Total Page:", "").Trim();

                                    int AutoPageCount;
                                    if (Int32.TryParse(PgCountLog, out AutoPageCount))
                                    {
                                        MNT.PgCountLog = AutoPageCount;
                                    }
                                }
                                ProcessEventHandler("PgCountLog : " + PgCountLog + "\t" + PageCount);
                            }
                            else
                            {
                                MNT.PgCountLog = GetInputOBj.PdfPageCount;
                            }
                        }
                        catch (Exception ex)
                        {
                            ProcessErrorHandler(ex);
                            continue;
                        }
                        //Eproof disable from ToAuthor   
                        //if (MNT.Status.Equals("ToAuthor"))
                        //{
                        //    MNT.PgCountLog = MNT.PdfPages;
                        //}

                        if (MNT.Stage.EndsWith("100"))
                            ProcessFresh(MNT, InputFilesObj);
                        else if (MNT.Stage.EndsWith("200"))
                            ProcessRevise(MNT, InputFilesObj);
                        else if (MNT.Stage.StartsWith("S275", StringComparison.OrdinalIgnoreCase))
                            ProcessFAX(MNT, InputFilesObj);


                        if (!string.IsNullOrEmpty(MNT.Status) && MNT.Status.Equals("PdfError", StringComparison.OrdinalIgnoreCase))
                        {
                        }
                        else
                            _OPSDBObj.UpdateMessageDetal(MsgDtl.MsgID, PageCount);
                    }
                    else
                    {
                        string FMSFolder = string.Empty;
                        string TempFolder = string.Empty;
                        if (MsgDtl.IP.Contains(ConfigDetails.TDXPSNoidaIP))
                        {
                            FMSFolder = ConfigDetails.TDXPSNoidaFMSFolder;
                            TempFolder = ConfigDetails.TDXPSNoidaTempFolder;
                            GetPDFXML s = new GetPDFXML(MNT.Client, MNT.JID, MNT.AID, FMSFolder, TempFolder);
                            s.ProcessNotification += ProcessEventHandler;
                            s.ErrorNotification += ProcessErrorHandler;
                            s.StartProcess();
                        }
                        else if (MsgDtl.IP.Contains("23.107"))
                        {
                            FMSFolder = ConfigDetails.TDXPSGangtokFMSFolder;
                            TempFolder = ConfigDetails.TDXPSGangtokTempFolder;
                            GetPDFXML s = new GetPDFXML(MNT.Client, MNT.JID, MNT.AID, FMSFolder, TempFolder);
                            s.ProcessNotification += ProcessEventHandler;
                            s.ErrorNotification += ProcessErrorHandler;
                            s.StartProcess();

                        }
                        else
                        {
                            if (InPutFilesMissing(MsgDtl))
                            {
                                _OPSDBObj.UpdateMessageDetal(MsgDtl.MsgID, 0);
                            }
                            ProcessEventHandler("Copy XML and PDF From Server Or FMS has been failed : " + MsgDtl.JID + "_" + MsgDtl.AID);
                        }
                    }
                }
        }
        public bool InPutFilesMissing(MessageDetail MsgDtl)
        {

            OPSDetail OpsDtl = _OPSDBObj.GetOPSDetails(MsgDtl.JID, MsgDtl.Client);
            bool Result = false;
            ProcessEventHandler("GetMailBody Start..");

            StringBuilder MailB = new StringBuilder();

            MailB.AppendLine("Dell All,");
            MailB.AppendLine(" ");
            MailB.AppendLine("Files are missing to process  : " + MsgDtl.JID + "_" + MsgDtl.AID);


            MailB.AppendLine("Copy XML and PDF From Server Or FMS has been failed : " + MsgDtl.JID + "_" + MsgDtl.AID);
            MailB.AppendLine(" ");
            MailB.AppendLine("Thanks,");




            ProcessEventHandler("GetMailBody Finish..");

            try
            {
                ProcessEventHandler("Process start to send mamil");
                MailDetail MailDetailOBJ = new MailDetail();
                MailDetailOBJ.MailFrom = "eproof@thomsondigital.com";
                MailDetailOBJ.MailTo = OpsDtl.FailEmail;

                MailDetailOBJ.MailSubject = "eProof Failure Notification : XML or PDF files missing for " + MsgDtl.JID + "_" + MsgDtl.AID;
                MailDetailOBJ.MailBody = MailB.ToString();

                eMailProcess eMailProcessOBJ = new eMailProcess();
                eMailProcessOBJ.ProcessNotification += ProcessEventHandler;
                eMailProcessOBJ.ErrorNotification += ProcessErrorHandler;
                if (eMailProcessOBJ.SendMailInternal(MailDetailOBJ))
                {
                    Result = true;
                }

            }
            catch (Exception ex)
            {
                this.ProcessErrorHandler(ex);
            }
            return Result;

        }
        private bool CopyCleanproof(MNTInfo MNT)
        {

            string CleanProofPdf = @"\\" + ConfigDetails.TDXPSGangtokIP + @"\temp\files\MNT_CLIENT_JOURNAL_JID_AID_110\clean_proof.pdf".Replace("CLIENT", MNT.Client).Replace("JID", MNT.JID).Replace("AID", MNT.AID);
            string GAbsPdf = @"\\" + ConfigDetails.TDXPSGangtokIP + @"\temp\files\MNT_CLIENT_JOURNAL_JID_AID_110\AIDc.pdf".Replace("CLIENT", MNT.Client).Replace("JID", MNT.JID).Replace("AID", MNT.AID);


            string CopyToMNTFolder = ConfigDetails.FinalQC + "\\" + MNT.MNTFolder;
            if (Directory.Exists(CopyToMNTFolder))
            {
                Directory.Delete(CopyToMNTFolder, true);
            }

            ProcessEventHandler("CleanProofPdf : " + CleanProofPdf);
            try
            {
                if (File.Exists(CleanProofPdf))
                {
                    if (!Directory.Exists(CopyToMNTFolder))
                    {
                        Directory.CreateDirectory(CopyToMNTFolder);
                    }
                    string CopyTo = CopyToMNTFolder + "\\" + MNT.JID + "_" + MNT.AID + "_clean_proof.pdf";

                    ProcessEventHandler("CopyTo : " + CopyTo);
                    File.Copy(CleanProofPdf, CopyTo, true);


                    if (File.Exists(GAbsPdf))
                    {
                        CopyTo = CopyToMNTFolder + "\\" + MNT.AID + "c.pdf";
                        File.Copy(GAbsPdf, CopyTo, true);
                    }
                    return true;
                }
                else
                {
                    ProcessEventHandler("Does not exist...");
                }
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }
            return false;
        }

        private void ProcessStripns()
        {

        }
        private void InsertMessageDetail(string IP)
        {
            foreach (MNTInfo MNT in MNTList)
            {
                MessageDetail MsgDtl = new DatabaseLayer.MessageDetail();
                MsgDtl.Client = MNT.Client;
                MsgDtl.JID = MNT.JID;
                MsgDtl.AID = MNT.AID;
                MsgDtl.Stage = MNT.Stage;
                MsgDtl.IP = IP.Replace("activemq:tcp://", "");
                _OPSDBObj.InsertMessageDetal(MsgDtl);


            }
        }

        public void InsertMessageDetail(WIPArticleInfo WipArticle)
        {

            MessageDetail MsgDtl = new DatabaseLayer.MessageDetail();
            MsgDtl.Client = WipArticle.Client;
            MsgDtl.JID = WipArticle.JID;
            MsgDtl.AID = WipArticle.AID;
            MsgDtl.Stage = WipArticle.ClientStage;
            MsgDtl.IP = WipArticle.IP;
            _OPSDBObj.InsertMessageDetal(MsgDtl);

        }
        private void InProcessMessageDetail()
        {

            _MsgList = _OPSDBObj.GetInprocessMessageDetail();


        }
        private void ProcessFresh(MNTInfo MNT, InputFiles InputFilesObj)
        {
            ProcessEventHandler("Start article processing in fresh stage");

            /////////Send Article to Editor first
            ////if ("#JABA#JEAB#".IndexOf("#" + MNT.JID + "#")!=-1  && !MNT.Status.Equals("ToAuthor", StringComparison.OrdinalIgnoreCase))
            ////{
            ////    ProcessToSendEditorFirst(MNT, InputFilesObj);
            ////    return;
            ////}

            if (!MNT.Client.Equals("Thieme", StringComparison.OrdinalIgnoreCase))
            {
                NIHProcess NIHObj = new NIHProcess(InputFilesObj.XMLPath, MNT);
                NIHObj.ProcessNotification += ProcessEventHandler;
                NIHObj.ErrorNotification += this.ProcessErrorHandler;
                NIHObj.StartProess();
            }

            if (MNT.Client.Equals("Thieme", StringComparison.OrdinalIgnoreCase))
            {
                ProcessThiemeEproof(MNT, InputFilesObj);
            }
            else if (_OPSDBObj.isOPSJID(MNT.JID))
            {
                ProcessEventHandler("eProof is processing using OPS");
                OPSProcess OPSProcessOBJ = new OPSProcess(MNT, InputFilesObj);
                OPSProcessOBJ.ProcessNotification += ProcessEventHandler;
                OPSProcessOBJ.ErrorNotification += this.ProcessErrorHandler;
                ProcessEventHandler("OPSProcess object created : Yes");
                OPSProcessOBJ.Start();

                if (OPSProcessOBJ.IsPdfProcessError)
                {
                    MNT.Status = "PdfError";
                }
            }
            else
            {

                ProcessEventHandler("eProof is processing using AEPS");
                eProof eProofOBJ = new eProof(MNT, InputFilesObj);

                eProofOBJ.ProcessNotification += ProcessEventHandler;
                eProofOBJ.ErrorNotification += this.ProcessErrorHandler;
                ProcessEventHandler("eProof object created : Yes");

                ProcessEventHandler("Start Validation....");
                eProofOBJ.StartValidation();

                if (eProofOBJ.ValidationResult)
                {
                    ProcessEventHandler("Input validation has been completed successfully");
                    ProcessEventHandler("Start process..");
                    if (eProofOBJ.ProcessMNT() == false)
                    {
                        eProofOBJ.ValidationResult = false;
                        ProcessEventHandler("Article could not processed successfully. Proess again.");
                    }
                    ProcessEventHandler("Process Finish..");
                }
                else
                    ProcessEventHandler("Input validation has been failed 1");



                eProofResultNotification.InternalAEPSMail(eProofOBJ);

                //if (eProofOBJ.IsAlreadyProcessed)
                //{
                //    ProcessEventHandler(MNT.JID + MNT.AID + " Article is Already Processed. No need to send email.");
                //}
                //else
                //{
                //    eProofResultNotification.InternalAEPSMail(eProofOBJ);
                //}

                if (eProofOBJ.IsPdfProcessError)
                {
                    MNT.Status = "PdfError";
                }
            }

        }
        private void ProcessThiemeEproof(MNTInfo MNT, InputFiles InputFilesObj)
        {

            ProcessEventHandler("Thieme is processing using AEPS");

            eProofThieme ThiemeObj = new eProofThieme(MNT, InputFilesObj);

            ThiemeObj.ProcessNotification += ProcessEventHandler;
            ThiemeObj.ErrorNotification += this.ProcessErrorHandler;

            ProcessEventHandler("Thieme Process object created : Yes");

            ProcessEventHandler("Start to validate input");

            ProcessEventHandler("Checking for Validation 1.1");

            ThiemeObj.StartValidation();


            ProcessEventHandler("Checking for Validation 2.1");

            ProcessEventHandler("Validation has been finished");
            //Rohit 2021
            //ThiemeObj.ValidationResult = true;

            if (ThiemeObj.ValidationResult)
            {
                ProcessEventHandler("Input is valid..");

                ProcessEventHandler("Start process..");

                if (ThiemeObj.ProcessMNT())
                    ThiemeObj.ValidationResult = true;
                else
                    ThiemeObj.ValidationResult = false;
            }
            else
            {
                ProcessEventHandler("Input validation has been failed 2" + ThiemeObj.ValidationResult);
            }
            eProofResultNotification.InternalAEPSMail(ThiemeObj);



            if (ThiemeObj.IsPdfProcessError)
            {
                MNT.Status = "PdfError";
            }
        }
        private void ProcessRevise(MNTInfo MNT, InputFiles InputFilesObj)
        {
            ProcessEventHandler("Start article processing in revise stage");

            ReviseProcess ReviseProcessOBJ = new ReviseProcess(MNT, InputFilesObj.PDFPath);
            ReviseProcessOBJ.ProcessNotification += ProcessEventHandler;
            ReviseProcessOBJ.ErrorNotification += this.ProcessErrorHandler;

            ReviseProcessOBJ.StartValidation();
            if (ReviseProcessOBJ.ValidationResult)
                ReviseProcessOBJ.Start();

            eProofResultNotification.InternalReviseMail(ReviseProcessOBJ);

            //if (ReviseProcessOBJ.IsValidJID && ReviseProcessOBJ.IsAlreadyProcessed == false)
            //{
            //    eProofResultNotification.InternalReviseMail(ReviseProcessOBJ);
            //}
            //else
            //{
            //    ProcessEventHandler("Validation failed in revise stage.");
            //    ProcessEventHandler(MNT.JID + MNT.AID + " Article is Already Processed. No need to send email.");
            //}

            if (ReviseProcessOBJ.IsPdfProcessError)
            {
                MNT.Status = "PdfError";
            }

        }
        private void ProcessFAX(MNTInfo MNT, InputFiles InputFilesObj)
        {
            ProcessEventHandler("Start article processing in FAX stage");

            FAXProcess FAXProcessOBJ = new FAXProcess(MNT, InputFilesObj.PDFPath);
            FAXProcessOBJ.ProcessNotification += ProcessEventHandler;
            FAXProcessOBJ.ErrorNotification += this.ProcessErrorHandler;
            FAXProcessOBJ.StartValidation();

            if (FAXProcessOBJ.ValidationResult)
                FAXProcessOBJ.Start();

            eProofResultNotification.InternalReviseMail(FAXProcessOBJ);

            //if (FAXProcessOBJ.IsValidJID && FAXProcessOBJ.IsAlreadyProcessed == false)
            //{
            //    eProofResultNotification.InternalReviseMail(FAXProcessOBJ);
            //}
            //else
            //{
            //    ProcessEventHandler("Validation failed in revise stage.");
            //    ProcessEventHandler(MNT.JID + MNT.AID + " Article is Already Processed. No need to send email.");
            //}

            if (FAXProcessOBJ.IsPdfProcessError)
            {
                MNT.Status = "PdfError";
            }
        }
        private void ProcessToSendEditorFirst(MNTInfo MNT, InputFiles InputFilesObj)
        {
            ProcessEventHandler("eProof is processing using AEPS");
            eProof eProofOBJ = new eProof(MNT, InputFilesObj);

            eProofOBJ.ProcessNotification += ProcessEventHandler;
            eProofOBJ.ErrorNotification += this.ProcessErrorHandler;
            ProcessEventHandler("eProof object created : Yes");
            ProcessEventHandler("Start Validation....");
            eProofOBJ.StartValidation();


            if (eProofOBJ.ValidationResult)
            {
                ProcessEventHandler("Input validation has been completed successfully");
                ProcessEventHandler("Start process..");

                if (MNT.JID.Equals("IEAM"))
                {
                    if (eProofOBJ.ProcessIEAMMNT() == false)
                    {
                        eProofOBJ.ValidationResult = false;
                        ProcessEventHandler("Article could not processed successfully. Proess again.");
                    }
                }
                else if (MNT.JID.Equals("ETC"))
                {
                    if (eProofOBJ.ProcessETCMNT() == false)
                    {
                        eProofOBJ.ValidationResult = false;
                        ProcessEventHandler("Article could not processed successfully. Proess again.");
                    }
                }
                else if ("#JABA#JEAB#".IndexOf("#" + MNT.JID + "#") != -1)
                {
                    if (eProofOBJ.ProcessJABA_JEAB_MNT() == false)
                    {
                        eProofOBJ.ValidationResult = false;
                        ProcessEventHandler("Article could not processed successfully. Process again.");
                    }
                }
                ProcessEventHandler("Process Finish..");

            }
            else if (eProofOBJ.IsAlreadyProcessed)
            {

                ProcessEventHandler("Process Finish..");

                //ProcessEventHandler("eProof is processing using OPS");
                //OPSProcess OPSProcessOBJ = new OPSProcess(MNT, InputFilesObj);
                //OPSProcessOBJ.ProcessNotification += ProcessEventHandler;
                //OPSProcessOBJ.ErrorNotification += this.ProcessErrorHandler; ;
                //ProcessEventHandler("OPSProcess object created : Yes");
                //OPSProcessOBJ.Start();
            }
            else
                ProcessEventHandler("Input validation has been failed 3");


            eProofResultNotification.InternalAEPSMail(eProofOBJ);

            if (eProofOBJ.IsPdfProcessError)
            {
                MNT.Status = "PdfError";
            }
        }


    }

    class InputFiles : MessageEventArgs
    {

        public string XMLPath
        {
            get;
            set;
        }

        public string PDFPath
        {
            get;
            set;
        }

        public InputFiles()
        {

        }

        public InputFiles(string PDFFile, string XMLFile)
        {
            XMLPath = XMLFile;
            PDFPath = PDFFile;
        }
    }
}

