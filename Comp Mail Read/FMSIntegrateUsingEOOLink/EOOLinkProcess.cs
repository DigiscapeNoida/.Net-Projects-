using System;
using System.Xml;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace FMSIntegrateUsingEOOLink
{
   

    class EOOLinkProcess
    {
        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;

        StringDictionary  StoredNoteID = new StringDictionary();
        List<ArticleInfo> ArticleList  = new List<ArticleInfo>();
        string NoteIDXML               = string.Empty;

        public EOOLinkProcess()
        {
            NoteIDXML = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\NoteID.xml";
            GetStoredNoteID();
        }
        public void StartProcessOnlyDownload()
        {
            OnlyDownload();
        }
        public  void StartProcess()
        {
            ProcessMessage("NoteIDXML :: " + NoteIDXML);

            LotusProcess LotusProcessOBJ         = new LotusProcess();
            LotusProcessOBJ.ProcessNotification += new NotifyMsg(   ArticleInfo_ProcessNotification);
            LotusProcessOBJ.ErrorNotification   += new NotifyErrMsg(ArticleInfo_ErrorNotification);

            LotusProcessOBJ.GettingEOOMails();
            
            try
            {
                foreach (MailInfo EOOMail in LotusProcessOBJ.EOOMails)
                {
                    ProcessEOOMail(EOOMail);
                }

                List<ArticleInfo> ExportArticleList = ArticleList.FindAll(x => x.EOOMail.ExportMailType);
                List<ArticleInfo> ImportArticleList = ArticleList.FindAll(x => x.EOOMail.ImportMailType);

                ADDPrefixJIDNameInRefCode(ImportArticleList,"BIT");
                
                List<ArticleInfo> ProcessArticleList    = new List<ArticleInfo>();

                foreach (ArticleInfo ArtInf in ExportArticleList)
                {
                    try
                    {
                        ArticleInfo ImprtArticleInfo = ImportArticleList.Find(x => x.RefCode.Equals(ArtInf.RefCode));

                        if (ImprtArticleInfo != null)
                        {
                            ProcessMessage("********************Process start to process export mail *********************************************");


                            ArtInf.RefCode   = ImprtArticleInfo.RefCode;
                            ArtInf.JID       = ImprtArticleInfo.JID;
                            ArtInf.AID       = ImprtArticleInfo.AID;

                            ArtInf.DownloadFileName = ImprtArticleInfo.DownloadFileName;
                            ArtInf.ImportMailNoteID = ImprtArticleInfo.EOOMail.NoteID;

                            ProcessArticleList.Add(ArtInf);

                            ProcessMessage("********************Import  mail details Start*********************************************");
                            StaticInfo.WriteLogMsg.AppendLog(ImprtArticleInfo);
                            ProcessMessage("********************Import  mail details End*********************************************");
                        }
                        else
                        {
                            ProcessMessage("-------------Import mail does not exist in mail box--------------");
                        }

                        ProcessMessage("********************Export  mail details Start*********************************************");
                        StaticInfo.WriteLogMsg.AppendLog(ArtInf);
                        ProcessMessage("********************Export  mail details End*********************************************");
                        
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }

                int Counter = 0;
                List<ArticleInfo> SuccessfulProcessArticleList = new List<ArticleInfo>();
                foreach (ArticleInfo ArtInf in ProcessArticleList)
                {
                    Counter++;
                    Console.WriteLine(Counter.ToString() + " out of " + ProcessArticleList.Count);

                    ProcessMessage(Counter.ToString() + " out of " + ProcessArticleList.Count);
                    ProcessMessage("ArtInf.EOOMail.NoteID : " + ArtInf.EOOMail.NoteID);
                    if (StoredNoteID.ContainsKey(ArtInf.EOOMail.NoteID) == false)
                    {
                        if (ArtInf != null && "#ETC#IEAM#JBMR#".IndexOf("#" + ArtInf.JID + "#") == -1 )
                        {
                            if (ProcessDownloadAndCreateOrder(ArtInf))
                                SuccessfulProcessArticleList.Add(ArtInf);
                        }
                        else
                            SuccessfulProcessArticleList.Add(ArtInf);
                    }
                    else
                    {
                        ProcessMessage("Already process NoteID : " + ArtInf.EOOMail.NoteID);
                    }
                }
                
                AppendStoredNoteID(SuccessfulProcessArticleList);
            }
            catch (Exception Ex)
            {
                ErrorMessage(Ex);
                Console.WriteLine(Ex.InnerException);
                Console.WriteLine(Ex.Message);
            }
        }
        public  void StartProcessToDownload()
        {
            ProcessMessage("NoteIDXML :: " + NoteIDXML);
            LotusProcess LotusProcessOBJ = new LotusProcess();

            LotusProcessOBJ.ProcessNotification += new NotifyMsg(ArticleInfo_ProcessNotification);
            LotusProcessOBJ.ErrorNotification   += new NotifyErrMsg(ArticleInfo_ErrorNotification);

            LotusProcessOBJ.GettingEOOMails();

            try
            {
                foreach (MailInfo EOOMail in LotusProcessOBJ.EOOMails)
                {
                    ProcessEOOMail(EOOMail);
                }

                List<ArticleInfo> ExportArticleList = ArticleList.FindAll(x => x.EOOMail.ExportMailType);
                List<ArticleInfo> ImportArticleList = ArticleList.FindAll(x => x.EOOMail.ImportMailType);

                ADDPrefixJIDNameInRefCode(ImportArticleList, "BIT");

                List<ArticleInfo> ProcessArticleList = new List<ArticleInfo>();

                //foreach (ArticleInfo ArtInf in ExportArticleList)
                foreach (ArticleInfo ArtInf in ImportArticleList)
                {
                    try
                    {
                            ProcessMessage("********************Process start to process export mail *********************************************");
                            ProcessArticleList.Add(ArtInf);

                            ProcessMessage("********************Import  mail details Start*********************************************");
                            //StaticInfo.WriteLogMsg.AppendLog(ImprtArticleInfo);
                            ProcessMessage("********************Import  mail details End*********************************************");
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }

                int Counter = 0;
                List<ArticleInfo> SuccessfulProcessArticleList = new List<ArticleInfo>();
                foreach (ArticleInfo ArtInf in ProcessArticleList)
                {
                    Counter++;
                    Console.WriteLine(Counter.ToString() + " out of " + ProcessArticleList.Count);

                    ProcessMessage(Counter.ToString() + " out of " + ProcessArticleList.Count);
                    ProcessMessage("ArtInf.EOOMail.NoteID : " + ArtInf.EOOMail.NoteID);
                    if (StoredNoteID.ContainsKey(ArtInf.EOOMail.NoteID) == false)
                    {
                       
                        if (ArtInf != null && "#ETC#IEAM#JBMR#".IndexOf("#" + ArtInf.JID + "#") == -1)
                        {
                            if (ProcessDownloadAndCreateOrder(ArtInf))
                                SuccessfulProcessArticleList.Add(ArtInf);
                        }
                        else
                            SuccessfulProcessArticleList.Add(ArtInf);
                    }
                    else
                    {
                        ProcessMessage("Already process NoteID : " + ArtInf.EOOMail.NoteID);
                    }
                }

                AppendStoredNoteID(SuccessfulProcessArticleList);
            }
            catch (Exception Ex)
            {
                ErrorMessage(Ex);
                Console.WriteLine(Ex.InnerException);
                Console.WriteLine(Ex.Message);
            }
        }
        private static void ADDPrefixJIDNameInRefCode(List<ArticleInfo> ImportArticleList, string  JID)
        {
            List<ArticleInfo> JIDImportArticleList = ImportArticleList.FindAll(x => x.JID == JID);
            foreach (ArticleInfo ArtInf in JIDImportArticleList)
            {
                ArtInf.RefCode = JID + "-" + ArtInf.RefCode;
            }
        }
        private bool ProcessDownloadAndCreateOrder(ArticleInfo ArtclInfo)
        {
            try
            {
                if (StaticInfo.USTATDays.ContainsKey(ArtclInfo.JID))
                {
                    //int Diff = DateTime.Today - ArtclInfo.EOOMail.MailTime;
                    //if (DateTime.Today.Day == ArtclInfo.EOOMail.MailTime.Day && DateTime.Today.Month == ArtclInfo.EOOMail.MailTime.Month && DateTime.Today.Year == ArtclInfo.EOOMail.MailTime.Year)
                    if (true)
                    {
                        ProcessMessage(" XmlOrder Object Created::");
                        XmlOrder XmlOrderOBJ = new XmlOrder(ArtclInfo);
                        ArtclInfo.OrderFile = XmlOrderOBJ.OrderFileName;
                        ArtclInfo.ZipFile = XmlOrderOBJ.OrderFileName.Replace(".xml", ".zip");

                        if (DataDownload(ArtclInfo.DownloadFileName, ArtclInfo.ZipFile))
                        {
                            StaticInfo.WriteLogMsg.AppendLog("AddTransmittalFile has been called");
                            AddTransmittalFile(ArtclInfo);

                            if (string.IsNullOrEmpty(XmlOrderOBJ.MSS) || XmlOrderOBJ.MSS.Equals("0"))
                            {
                                XmlOrderOBJ.MSS  = ArtclInfo.ManuscriptPages;
                                XmlOrderOBJ.Figs = ArtclInfo.Figs;
                            }


                            StaticInfo.WriteLogMsg.AppendLog("XmlOrderOBJ.CreateXMLOrder has been called");
                            XmlOrderOBJ.CreateXMLOrder();

                            StaticInfo.WriteLogMsg.AppendLog("CopyToTDNAS has been called");
                            CopyToTDNAS(ArtclInfo);

                            StaticInfo.WriteLogMsg.AppendLog("CopyOrderToTDNAS has been called");
                            CopyOrderToTDNAS(ArtclInfo);

                            StaticInfo.WriteLogMsg.AppendLog("CopyToFMS has been called");
                            CopyToFMS(ArtclInfo);
                            return true;
                        }
                    }
                    else
                    { 

                    }
                }
                else
                {
                    StaticInfo.WriteLogMsg.AppendLog("TAT not define for this below JID Please Check");
                    StaticInfo.WriteLogMsg.AppendLog("JID :: " + ArtclInfo.JID);
                }
            }
            catch(Exception Ex)
            {
                ErrorMessage(Ex);
                Console.WriteLine(Ex.InnerException);
                Console.WriteLine(Ex.Message);
            }
            return false;
        }
        private string GetOrderPath(ArticleInfo AI)
        {
            if (ConfigDetails.RootPath != null)
            {
                if (!ConfigDetails.RootPath.Equals(""))
                {
                    if (!Directory.Exists(ConfigDetails.RootPath))
                        Directory.CreateDirectory(ConfigDetails.RootPath);
                }
            }

            string OrderPath = ConfigDetails.RootPath.Trim(new char[] { '\\' }) + "\\" + "JWUSA" + "\\" + AI.JID + "\\" + AI.AID  ;
            if (!Directory.Exists(OrderPath))
                Directory.CreateDirectory(OrderPath);

            return OrderPath;
        }
        private int GetNextFileNo(string _DirectoryPath )
        {

            int MaxNo = 0;
            if (Directory.Exists(_DirectoryPath))
                MaxNo = Directory.GetFiles(_DirectoryPath, "*.zip").Length;

            //for testing if (MaxNo > 0) MaxNo--;
            return MaxNo;


        }
        private void CopyToTDNAS(ArticleInfo ArtclInfo)
        {
            try
            {
                string TDNasFreshFolder = @"\\td-nas\w-input\downloads\Material Input\US Materials\\" + ArtclInfo.JID + "\\" + ArtclInfo.AID + "\\Fresh";
                if (!Directory.Exists(TDNasFreshFolder))
                {
                    Directory.CreateDirectory(TDNasFreshFolder);
                }
                string TDNasFile = TDNasFreshFolder + "\\" + Path.GetFileName(ArtclInfo.ZipFile);

                ProcessMessage("ZipFile   :: " + ArtclInfo.ZipFile);
                ProcessMessage("TDNasFile :: " + TDNasFile);

                File.Copy(ArtclInfo.ZipFile, TDNasFile, true);
                ProcessMessage("Matierial copied to td-nas successfully");
            }
            catch (Exception Ex)
            {
                ErrorMessage(Ex);
            }
        }
        private void CopyOrderToTDNAS(ArticleInfo ArtclInfo)
        {

                string OrderFileName   =  Path.GetFileName( ArtclInfo.OrderFile);
                string AIDFolder = @"\\TD-NAS\W-Input\Downloads\xmlorder\JWUSA\" + ArtclInfo.JID + "\\FRESH\\" + ArtclInfo.AID;
                string CurrentOrderFolder = AIDFolder + "\\CurrentOrder\\";

                string TDNasXMLOrder      =  AIDFolder + "\\" + OrderFileName;
                string CurrentXMLOrder    =  AIDFolder + "\\CurrentOrder\\" +OrderFileName;

                 try
                 {

                     ProcessMessage("AIDFolder ::" + AIDFolder);

                        if (!Directory.Exists(AIDFolder))
                             Directory.CreateDirectory(AIDFolder);

                        File.Copy(ArtclInfo.OrderFile, TDNasXMLOrder,true);

                        ProcessMessage("CurrentOrderFolder ::" + CurrentOrderFolder);
                        if (Directory.Exists(CurrentOrderFolder))
                            Directory.Delete(CurrentOrderFolder,true);
                        
                        Directory.CreateDirectory(CurrentOrderFolder);

                        ProcessMessage("CurrentXMLOrder ::" + CurrentXMLOrder);

                        File.Copy(ArtclInfo.OrderFile, CurrentXMLOrder);
                }
                catch (Exception Ex)
                {
                    ErrorMessage(Ex);
                }

        }
        private void CopyToFMS(ArticleInfo ArtclInfo)
        {
            string FMSXMLOrder = StaticInfo.FmsPath +"\\" + Path.GetFileName(ArtclInfo.OrderFile);
            string FMSZip      = StaticInfo.FmsPath + "\\" + Path.GetFileName(ArtclInfo.ZipFile);

            File.Copy(ArtclInfo.OrderFile, FMSXMLOrder);
            File.Copy(ArtclInfo.ZipFile  , FMSZip);


            ProcessMessage("Order and Matierial copied to FMS2 successfully");
        }
        private void ProcessEOOMail(MailInfo EOOMail)
        {
             ArticleInfo _ArticleInfo = new ArticleInfo();
            //Rohit
            //_ArticleInfo.ProcessNotification += new NotifyMsg(ArticleInfo_ProcessNotification);
            //_ArticleInfo.ErrorNotification += new NotifyErrMsg(ArticleInfo_ErrorNotification);

            _ArticleInfo.ProcessArticleInfo(EOOMail);
             ArticleList.Add( _ArticleInfo);
        }
        private void ArticleInfo_ErrorNotification(Exception Ex)
        {
            ErrorMessage(Ex);
        }
        private void ArticleInfo_ProcessNotification(string NotificationMsg)
        {
            ProcessMessage(NotificationMsg);
        }
        public  bool DataDownload(string Fname, string TargateFname)
        {
            string WebURL = "http://eeolink.wiley.com/ws/download/" + Fname;
            try
            {
                if (File.Exists(TargateFname))
                {
                    ProcessMessage(TargateFname + "File exist . So return.");
                    return true;
                }

                ProcessMessage("TargateFname  :: " + TargateFname);
                Console.WriteLine("Material is downloading :: " + Fname);
                ProcessMessage("Web URL :: " + WebURL);

                WebClient objWebClient = new WebClient();

                objWebClient.DownloadFile(WebURL, TargateFname);

                ProcessMessage("Download successfully...........");
            }
            catch (WebException ex)
            {

                ProcessMessage("WEB URL :: " + WebURL);
                ProcessMessage("TargateFname :: " + TargateFname);
                ProcessMessage("Error accessing " + "" + " - " + ex.Message);

            }
            catch (Exception ex)
            {

                ProcessMessage("WEB URL :: " + WebURL);
                ProcessMessage("TargateFname :: " + TargateFname);
                ProcessMessage("Error accessing " + "" + " - " + ex.Message);

                ProcessMessage("Error in download process...........");
                ErrorMessage(ex);

                Console.WriteLine("WEB URL :: " + WebURL);
                Console.WriteLine("TargateFname :: " + TargateFname);
                Console.WriteLine("Error accessing " + "" + " - " + ex.Message);
                return false;
            }
            Console.WriteLine("Material downloaded successfully for " + Fname);
            return true;
        }
        public  bool AddTransmittalFile(ArticleInfo ArtclInfo )
        {
            string Source = ArtclInfo.ZipFile;
            string TempPath = "C:\\Temp\\"  + Path.GetFileNameWithoutExtension(Source);
            string ZipPath = TempPath + ".zip";

            try
            {
                string  TransmittalFile = string.Empty;
                FastZip unzip1 = new FastZip();
                unzip1.ExtractZip(Source, TempPath, "");

                ProcessMessage("Unzip done successfully..");


                ProcessMessage("Start Checking MSS and images..");
                CheckMSSandImages(ArtclInfo, TempPath);
                ProcessMessage("End Checking MSS and images..");

                if (Directory.GetDirectories(TempPath).Length == 1)
                {
                    string InnerDir = Directory.GetDirectories(TempPath)[0];
                    TransmittalFile = InnerDir + "\\Transmittal.txt";

                    ArtclInfo.ClientOrgFileName = Path.GetFileName(InnerDir);
                }
                else
                    TransmittalFile = TempPath + "\\Transmittal.txt";

                File.WriteAllText(TransmittalFile, ArtclInfo.EOOMail.MailBody);

                if (Directory.GetFiles(TempPath, "*.tex", SearchOption.AllDirectories).Length > 0)
                {

                    ProcessMessage("tex file exist in material...");

                    ArtclInfo.isTexFileExist = true;
                }

                unzip1.CreateZip(ZipPath, TempPath, true, "");

                ProcessMessage("Zip file created successfully..");

                File.Delete(Source);
                File.Move(ZipPath,Source);
                ProcessMessage("Zip file moved successfully..");

                Directory.Delete(TempPath,true);
                ProcessMessage("Temp Zip file deleted successfully..");
               
            }
            catch (Exception Ex)
            {
                ErrorMessage(Ex);
                Console.WriteLine(Ex.InnerException);
                Console.WriteLine("Error in extracting zip file" + Ex.Message);
                return false;
            }
            return true;
        }
        public bool AddTransmittalFile(string Source, ArticleInfo ArtclInfo)
        {
            string TempPath = "C:\\Temp\\" + Path.GetFileNameWithoutExtension(Source);
            string ZipPath = TempPath + ".zip";

            try
            {
                string TransmittalFile = string.Empty;
                FastZip unzip1 = new FastZip();
                unzip1.ExtractZip(Source, TempPath, "");

                ProcessMessage("Unzip done successfully..");


                if (Directory.GetDirectories(TempPath).Length == 1)
                {
                    string InnerDir = Directory.GetDirectories(TempPath)[0];
                    TransmittalFile = InnerDir + "\\Transmittal.txt";

                }
                else
                    TransmittalFile = TempPath + "\\Transmittal.txt";

                File.WriteAllText(TransmittalFile, ArtclInfo.EOOMail.MailBody);

                ProcessMessage("Process Metadata start..");
                InsertMetaData(TempPath, ArtclInfo);
                ProcessMessage("Process Metadata finish..");
                unzip1.CreateZip(ZipPath, TempPath, true, "");

                ProcessMessage("Zip file created successfully..");

                File.Delete(Source);
                File.Move(ZipPath, Source);
                ProcessMessage("Zip file moved successfully..");

                Directory.Delete(TempPath, true);
                ProcessMessage("Temp Zip file deleted successfully..");

            }
            catch (Exception Ex)
            {
                ErrorMessage(Ex);
                Console.WriteLine(Ex.InnerException);
                Console.WriteLine("Error in extracting zip file" + Ex.Message);
                return false;
            }
            return true;
        }
        private void InsertMetaData(string _ExtractTo, ArticleInfo ArtclInfo)
        {
            string[] MetaXMLFiles = Directory.GetFiles(_ExtractTo, "*-metadata.xml", SearchOption.AllDirectories);

            if (MetaXMLFiles.Length == 1)
            {

                string _MetaXMLFile = MetaXMLFiles[0];
                ProcessMessage("MetaXMLFile :: " + _MetaXMLFile);
                WileyMetaData.MetaData WileyMetaDataOBJ = new WileyMetaData.MetaData(_MetaXMLFile);
                WileyMetaDataOBJ.InsertMetaData("JWUSA", ArtclInfo.JID, ArtclInfo.AID);
            }
            else
            {
                ProcessMessage("MetaXMLFile :: No metadata xml found");
            }

        }
        private void CheckMSSandImages(ArticleInfo ArtclInfo, string InputPath)
        {
            ProcessMessage("Process start to checkMSS and Images");


            string[] FLS = Directory.GetFiles(InputPath, "*.*", SearchOption.AllDirectories);
            foreach (string FL in FLS)
            {
                ProcessMessage("File :: " + FL);
                    
            }

            string MSSDocFile = string.Empty;
            if (string.IsNullOrEmpty(ArtclInfo.ManuscriptPages) || ArtclInfo.ManuscriptPages.Equals("0")) 
            {
                ProcessMessage("Manuscript Pages  is zero so first attempt to get pages from pdf");
                string [] PdfDir= Directory.GetDirectories(InputPath, "pdf", SearchOption.AllDirectories);
                ProcessMessage("InputPath :: " + InputPath);
                if (PdfDir.Length == 1)
                {
                    string[] PDFFiles = Directory.GetFiles(PdfDir[0], "*.Pdf");

                    ProcessMessage("PDF Folder found...");
                    if (PDFFiles.Length == 1)
                    {
                        ProcessMessage("PDF File :: " + PDFFiles[0]);
                        PDFProcess PDFObj = new PDFProcess(PDFFiles[0]);
                        //ArtclInfo.Figs            = PDFObj.Images.ToString();
                        ArtclInfo.ManuscriptPages = PDFObj.Pages.ToString();

                        ProcessMessage("PDFObj.Pages :: " + PDFObj.Pages.ToString());
                    }
                    else
                    {
                        foreach (string PDF in PDFFiles)
                        {
                            ProcessMessage("PDf file :: " + PDF);
                        }
                        ProcessMessage("More than one pdf found in input path so second attempt to get pages from doc files");
                    }
                }
                else
                {
                    ProcessMessage("No pdf folder found in input path");
                }
            }


            if (string.IsNullOrEmpty(ArtclInfo.ManuscriptPages)|| ArtclInfo.ManuscriptPages.Equals("0"))
            {
                ProcessMessage("Manuscript Pages  is zero so second attempt to get pages from doc files");

                int Pages = 0;
                string[] DocFiles           = Directory.GetFiles(InputPath, "*.doc*", SearchOption.AllDirectories);
                if (DocFiles.Length > 0)
                {
                    WordProcess.WordProcess obj = new WordProcess.WordProcess();
                    foreach (string DocFile in DocFiles)
                    {
                        int TempPages = obj.GetMSSPages(DocFile);
                        if (TempPages > Pages)
                        {
                            Pages = TempPages;
                            MSSDocFile = DocFile;

                            ProcessMessage("DocFile :: " + DocFile);
                            ProcessMessage("Pages :: " + Pages.ToString());
                        }
                    }

                    ProcessMessage("MSSDocFile :: " + MSSDocFile);
                }
                else
                {
                    ProcessMessage("No doc file found in input file so MSS set default value zero");
                }

            }
            if (string.IsNullOrEmpty(ArtclInfo.Figs) || ArtclInfo.Figs.Equals("0"))
            { 
                string [] GraphicDir= Directory.GetDirectories(InputPath, "graphic", SearchOption.AllDirectories);

                ProcessMessage("Getting tiff  files count from graphic folder");
                if (GraphicDir.Length == 1)
                {

                    ProcessMessage("Graphic folder found");
                    ProcessMessage("Graphic folder path ::" + GraphicDir[0]);
                    int ImageCount  = 0;

                    string[] GraphicFiles = Directory.GetFiles(GraphicDir[0], "*.tif");
                               ImageCount = GraphicFiles.Length;

                    if (GraphicFiles.Length >= 0)
                    {
                       ProcessMessage("Getting eps  files count from graphic folder");
                       GraphicFiles = Directory.GetFiles(GraphicDir[0], "*.eps");
                       ImageCount   = ImageCount + GraphicFiles.Length;

                       ProcessMessage("ImageCount ::" + ImageCount);
                    }

                    if (ImageCount > 0)
                    {
                        ArtclInfo.Figs = ImageCount.ToString();
                    }
                    else
                    {
                         ProcessMessage("There is no tiff or eps file in graphic folder now check the doc file");
                         GraphicFiles = Directory.GetFiles(GraphicDir[0], "*.doc*");
                         ProcessMessage("Getting doc files from graphic folder");
                         if (GraphicFiles.Length > 0)
                         {
                            ProcessMessage("Doc files found in graphic folder");
                             string TempDIR = "C:\\010";
                             if (Directory.Exists(TempDIR))
                             {
                                 ProcessMessage("Delete folder :: "+ TempDIR );
                                 Directory.Delete(TempDIR);
                             }
                             foreach (string GraphicFile in GraphicFiles)
                             {
                                 string TempFile = "C:\\010\\" + Path.GetFileName(GraphicFile);
                                 WordProcess.WordProcess obj = new WordProcess.WordProcess();
                                 File.Copy(GraphicFile, TempFile);
                                 obj.SaveAsHTML(TempFile);

                                 ProcessMessage("Graphic Doc:: " + GraphicFile);
                                 ProcessMessage("Temp Doc:: "    + TempFile);
                             }
                             GraphicFiles = Directory.GetFiles(TempDIR, "*.jpg",SearchOption.AllDirectories);
                             ProcessMessage("Count jpg files from " + TempDIR);
                             ProcessMessage("GraphicFiles  " + GraphicFiles.Length.ToString());
                             ArtclInfo.Figs = GraphicFiles.Length.ToString();
                         }
                    }
                }
            }
        }
        private void ProcessMSSCount()
        { 

        }
        private void ProcessFIGSCount()
        {

        }
    
        public  void GetStoredNoteID()
        {
            XmlTextReader reader = null;
            if (File.Exists(NoteIDXML))
            {
                try
                {
                    reader = new XmlTextReader(NoteIDXML);
                    reader.WhitespaceHandling = WhitespaceHandling.All;
                    XmlDocument myXmlDocument = new XmlDocument();
                    myXmlDocument.Load(reader);
                    reader.Close();
                    XmlNodeList NL = myXmlDocument.GetElementsByTagName("NoteID");

                    ProcessMessage("Getting NOTE ID from XML File.....");

                    if (NL != null)
                    {
                        StoredNoteID.Clear();
                        for (int i = 0; i < NL.Count; i++)
                        {
                            if (NL[i].Attributes.Count > 0 && !StoredNoteID.ContainsKey(NL[i].InnerText))
                            {
                                StoredNoteID.Add(NL[i].InnerText, NL[i].Attributes[0].Value);
                                ProcessMessage(NL[i].InnerText);
                            }
                        }
                    }
                    myXmlDocument = null;
                }
                catch (Exception ex)
                {
                    ProcessMessage("Error in GetStoredNoteID");
                    ErrorMessage(ex);
                }
            }
            else
            {
                ProcessMessage("NodeID xml File is not exist");
                Console.WriteLine("NodeID XML File is not exist");
            }
        }
        public  void AppendStoredNoteID(List<ArticleInfo> ProcessArticleList)
        {
            XmlTextReader reader = null;
            if (File.Exists(NoteIDXML))
            {
                try
                {
                    reader = new XmlTextReader(NoteIDXML);
                    reader.WhitespaceHandling = WhitespaceHandling.All;
                    XmlDocument myXmlDocument = new XmlDocument();
                    myXmlDocument.Load(reader);
                    reader.Close();
                    foreach (ArticleInfo ArtInf in ProcessArticleList)
                    {
                        XmlElement ele = myXmlDocument.CreateElement("NoteID");
                            ele.SetAttribute("time", ArtInf.EOOMail.MailTime.ToString() );
                            ele.InnerText = ArtInf.EOOMail.NoteID;
                            myXmlDocument.DocumentElement.AppendChild(ele);
                    }

                    myXmlDocument.Save(NoteIDXML);
                    myXmlDocument = null;
                }
                catch (Exception ex)
                {
                    ProcessMessage("Error in GetStoredNoteID");
                    ErrorMessage(ex);
                }
            }
            else
            {
                ProcessMessage("NodeID xml File is not exist");
                Console.WriteLine("NodeID XML File is not exist");
            }
        }
        private void CreateNoteIdXml(List<ArticleInfo> ProcessArticleList)
        {

            if (ProcessArticleList.Count == 0)
            {
                return;
            }
            if (File.Exists(NoteIDXML))
            {
                try
                {
                    File.Delete(NoteIDXML);
                }
                catch (Exception d)
                {
                    Console.WriteLine("Error in Deleting File" + d.Message);
                }
            }
            StreamWriter Swrite = File.CreateText(NoteIDXML);
            Swrite.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            Swrite.WriteLine(@"<NoteIDS>");
            foreach (ArticleInfo ArtInf in ProcessArticleList)
            {
                Swrite.WriteLine("<NoteID time=\"" + ArtInf.EOOMail.MailTime.ToString() + "\">" + ArtInf.EOOMail.NoteID + @"</NoteID>");
            }
            
            Swrite.WriteLine(@"</NoteIDS>");
            Swrite.Flush();
            Swrite.Close();
        }
        private void ProcessMessage(string Msg)
        {
            if (ProcessNotification != null)
            {
                ProcessNotification(Msg);
            }
        }
        private void ErrorMessage(Exception Ex)
        {
            if (ErrorNotification != null)
            {
                ErrorNotification(Ex);
            }
        }


        private void OnlyDownload()
        {
            int Counter = 0;
           ProcessMessage("NoteIDXML :: " + NoteIDXML);
            LotusProcess LotusProcessOBJ = new LotusProcess();
            LotusProcessOBJ.ProcessNotification += new NotifyMsg(ArticleInfo_ProcessNotification);
            LotusProcessOBJ.ErrorNotification   += new NotifyErrMsg(ArticleInfo_ErrorNotification);
            LotusProcessOBJ.GettingEOOMails();

            ProcessMessage("GettingEOOMails :: Finished");

           

            //List<ArticleInfo> ImportArticleList = null;
            //List<ArticleInfo> ExportArticleList = null;
            //try
            //{
            //    foreach (MailInfo EOOMail in LotusProcessOBJ.EOOMails)
            //    {
            //        ProcessEOOMail(EOOMail);
            //    }

            //    if (ArticleList.Count == 0)
            //    {                 
            //        ProcessMessage("ArticleList.Count == 0");
            //        return;
            //    }

            //    ImportArticleList = ArticleList.FindAll(x => x.EOOMail.ImportMailType);

            //    ADDPrefixJIDNameInRefCode(ImportArticleList, "BIT");
            //    Counter = ImportArticleList.Count;
            //    ExportArticleList = ArticleList.FindAll(x => x.EOOMail.ExportMailType);

            //    //List<ArticleInfo> ss  = ExportArticleList.FindAll(x => x.RefCode == null);
            //    //List<ArticleInfo> ss1 = ImportArticleList.FindAll(x => x.RefCode == null);

            //    foreach (ArticleInfo ImportArticle in ImportArticleList)
            //    {
            //        try
            //        {
            //           if (ImportArticle.RefCode == null)
            //            {
            //                ArticleInfo_ProcessNotification("ImportArticle.RefCode == null");
            //            }
            //            else
            //            {
            //                ArticleInfo_ProcessNotification("ImportArticle.RefCode == " + ImportArticle.RefCode);
            //                ArticleInfo_ProcessNotification("ExportArticleList == "     + ExportArticleList.Count);

            //                ArticleInfo ExportArticleInfo =null;
            //                try
            //                {
            //                    ExportArticleInfo = ExportArticleList.Find(x => x.RefCode != null && x.RefCode.Equals(ImportArticle.RefCode));
            //                }
            //                catch (Exception Ex)
            //                {
            //                    ArticleInfo_ProcessNotification("Ex : " + Ex.StackTrace);
            //                }

            //                if (ExportArticleInfo != null)
            //                {
            //                   ImportArticle.EOOMail.MailBody = ExportArticleInfo.EOOMail.MailBody;
            //                }
            //                else
            //                {
            //                    ArticleInfo_ProcessNotification(ImportArticle.JID + ImportArticle.AID + "\t" + "RefCode  not found..");
            //                    ArticleInfo_ProcessNotification("ImportArticle.RefCode :: " + ImportArticle.RefCode);
            //                }
            //            }

            //        }
            //        catch (Exception Ex)
            //        {
            //            ArticleInfo_ErrorNotification(Ex);
            //            ArticleInfo_ProcessNotification( "Error :: " + Ex.StackTrace);
            //        }

            //    }
            //    foreach (ArticleInfo ImportArticle in ImportArticleList)
            //    {
            //        Counter--;
            //        Console.WriteLine( Counter + " mails remaining to process........");

            //        string OrderPath = GetOrderPath(ImportArticle);

            //        string _DirectoryPath   = OrderPath;

            //        string ZIPFileName = OrderPath + "\\" + ImportArticle.AID + "_" + GetNextFileNo(_DirectoryPath) + ".zip";
            //        try
            //        {
            //            if (StoredNoteID.ContainsKey(ImportArticle.EOOMail.NoteID) == false)
            //            {
            //                if (DataDownload(ImportArticle.DownloadFileName, ZIPFileName))
            //                {
            //                    ProcessMessage("AddTransmittalFile has been called");
            //                    AddTransmittalFile(ZIPFileName, ImportArticle);
            //                }
            //                else
            //                {
            //                    ImportArticleList.Remove(ImportArticle);
            //                }
            //            }
            //            else
            //            {
            //                ProcessMessage("Already process NoteID : " + ImportArticle.EOOMail.NoteID);
            //            }
            //        }
            //        catch (Exception Ex)
            //        {
            //            StaticInfo.WriteLogMsg.AppendLog(Ex);
            //            ImportArticleList.Remove(ImportArticle);
            //        }
            //    }
            //}
            //catch(Exception Ex)
            //{ 
            //}
            //if (ImportArticleList!= null)
            //    AppendStoredNoteID(ImportArticleList);
        }



    }
    class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 60 * 60 * 1000;
            return w;
        }
    }

}
    