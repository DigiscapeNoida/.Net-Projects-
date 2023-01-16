using System;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using ProcessNotification;
using System.Text;
using MsgRcvr;
using System.Threading;

namespace AutoEproof
{
    class GetInput : MessageEventArgs
    {
        string _ExePath    = string.Empty;
        string _XMLPath    = string.Empty;
        string _PDFPath    = string.Empty;
        string _QPDFPath = string.Empty;
        string _cPDFPath = string.Empty;
        string _ProcessPath = string.Empty;
        string _ServerPath = string.Empty;
        int _PdfPageCount = 0;
        MNTInfo _MNT       = null;
        bool   _isCopied   = false;

        public int PdfPageCount
        {
            set { _PdfPageCount= value; }
            get { return _PdfPageCount; }
        }

        public string XMLPath 
        {
            get { return _XMLPath; }
        }

        public string PDFPath
        {

            get { return _PDFPath; }
        }
        MergeNjdPdf objNjd = null; 

        public  GetInput( MNTInfo MNT)
        {
            _MNT = MNT;
            _ServerPath = ConfigDetails.ServerPath;

            if (AppDomain.CurrentDomain.RelativeSearchPath == null)
                _ExePath = AppDomain.CurrentDomain.BaseDirectory;
            else
                _ExePath = AppDomain.CurrentDomain.RelativeSearchPath;


            _ProcessPath = _ExePath + "Process";

            if (!Directory.Exists(_ProcessPath))
                Directory.CreateDirectory(_ProcessPath);
        }

        public bool CopyXMLPDFFromServerOrFMS()
        {
            string JID = _MNT.JID;

            string XMLFileName = _MNT.JID + _MNT.AID + ".xml";
            string PDFFileName = _MNT.JID + "-" + _MNT.AID + ".pdf";
            string QPDFFileName = _MNT.JID + "-" + _MNT.AID + "Q.pdf";
            string cPDFFileName = _MNT.JID + "-" + _MNT.AID + "c.pdf";

            string Server_MNTFOLDER  = _ServerPath   + "\\" + _MNT.JID + _MNT.AID;
            string Process_MNTFOLDER = _ProcessPath + "\\" + _MNT.JID + _MNT.AID;

            string ServerXML = Server_MNTFOLDER + "\\" + XMLFileName;
            string ServerPDF = Server_MNTFOLDER + "\\" + PDFFileName;
            string ServerQPDF = Server_MNTFOLDER + "\\" + QPDFFileName;
            string ServercPDF = Server_MNTFOLDER + "\\" + cPDFFileName;

            _XMLPath = Process_MNTFOLDER + "\\" + XMLFileName;
            _PDFPath = Process_MNTFOLDER + "\\" + PDFFileName;
            _QPDFPath = Process_MNTFOLDER + "\\" + QPDFFileName;
            _cPDFPath = Process_MNTFOLDER + "\\" + cPDFFileName;

            //if (Directory.Exists(_ServerPath))
            //{
                if (Directory.Exists(Process_MNTFOLDER))
                {
                    Directory.Delete(Process_MNTFOLDER, true);
                }
                Directory.CreateDirectory(Process_MNTFOLDER);
            //============================================================
            if (_MNT.Client.ToLower() == "thieme")
            {
                string thieme_folder = System.Configuration.ConfigurationSettings.AppSettings["thieme"].ToString();
                DirectoryInfo dir=new DirectoryInfo(thieme_folder + _MNT.JID + "-" + _MNT.AID);
               // if (dir.LastWriteTime < DateTime.Now.AddMinutes(-15))
                {
                    ServerXML = thieme_folder + _MNT.JID + "-" + _MNT.AID + "\\" + XMLFileName;
                    ServerPDF = thieme_folder + _MNT.JID + "-" + _MNT.AID + "\\" + PDFFileName;
                }
                //else
                //{
                //    ServerXML = thieme_folder + _MNT.JID + "-" + _MNT.AID + "\\" + XMLFileName+"1";
                //    ServerPDF = thieme_folder + _MNT.JID + "-" + _MNT.AID + "\\" + PDFFileName+"1";
                //}
            }
            //============================================================
                if (Directory.Exists(Process_MNTFOLDER) && File.Exists(ServerXML) && File.Exists(ServerPDF))
                {
                    File.Copy(ServerXML, _XMLPath,true);
                    File.Copy(ServerPDF, _PDFPath,true);
                    if (File.Exists(ServerQPDF))
                        File.Copy(ServerQPDF, _QPDFPath, true);
                    else
                    {
                        ProcessEventHandler(" Q PDf does not exist in C FMS directory");
                        string Path = ConfigDetails.TDXPSGangtokFMSFolder + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\WorkArea\\" + _MNT.MNTFolder + "\\" + _MNT.AID + "Q.pdf";
                        if (File.Exists(Path))
                        {
                            ProcessEventHandler(" Q PDf exist at centralized_server.");
                            File.Copy(Path, _QPDFPath, true);
                            Thread.Sleep(10000);

                        }
                        else
                        {
                            Path = ConfigDetails.TDXPSGangtokTempFolder + "\\" + _MNT.MNTFolder + "\\" + _MNT.AID + "Q.pdf";
                            if (File.Exists(Path))
                            {
                                ProcessEventHandler(" Q PDf does not exist at centralized_server.");
                                File.Copy(Path, _QPDFPath, true);
                                ProcessEventHandler(" Q PDf exist at Temp Directory.");
                                Thread.Sleep(10000);
                            }
                        }
                    }
                    if (File.Exists(ServercPDF))
                        File.Copy(ServercPDF, _cPDFPath, true);
                    else
                    {
                        ProcessEventHandler("C PDf does not exist in C FMS directory.");
                        string Path = ConfigDetails.TDXPSGangtokFMSFolder + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\WorkArea\\" + _MNT.MNTFolder + "\\" + _MNT.AID + "c.pdf";
                        if (File.Exists(Path))
                        {
                            ProcessEventHandler(" C PDf exist at centralized_server.");
                            File.Copy(Path, _cPDFPath, true);
                            Thread.Sleep(10000);
                        }
                        else
                        {
                            Path = ConfigDetails.TDXPSGangtokTempFolder + "\\" + _MNT.MNTFolder + "\\" + _MNT.AID + "c.pdf";
                            if (File.Exists(Path))
                            {
                                ProcessEventHandler(" C PDf does not exist at centralized_server.");
                                File.Copy(Path, _cPDFPath, true);
                                 ProcessEventHandler(" C PDf exist at Temp Directory.");
                                Thread.Sleep(10000);
                            }
                        }
                    }

                    try
                    {
                        _PdfPageCount = PdfProcess.PDF.GetPdfPageCount(_PDFPath);
                    }
                    catch (Exception ex)
                    {
                        ProcessErrorHandler(ex);
                    }
                   // string GAbsPdf = ServerPDF.Replace(".pdf", "c.pdf");  ///////Graphical Abstract pdf
                    //if (File.Exists(GAbsPdf))
                    //{
                    //    string[] Arr = new string[2];
                    //    Arr[0] = GAbsPdf;
                    //    Arr[1] = _PDFPath;

                    //    if (_MNT.JID.Equals("IEAM") || _MNT.JID.Equals("WSB"))
                    //    {
                    //        Arr[0] = _PDFPath;
                    //        Arr[1] = GAbsPdf;
                    //    }

                    //    MergePDF(_PDFPath, Arr);
                    //}

                    //28/07/2017 NJD has been stopped.

                      //if (File.Exists(GAbsPdf))
                      //  {
                      //      objNjd = new MergeNjdPdf();
                                                       
                      //      if (!objNjd.StartProcess(_MNT.JID, _MNT.AID, _PDFPath, GAbsPdf,_MNT.Stage))
                      //      {
                      //          string[] Arr = new string[2];
                      //          Arr[0] = GAbsPdf;
                      //          Arr[1] = _PDFPath;

                      //          if (_MNT.JID.Equals("IEAM") || _MNT.JID.Equals("WSB"))
                      //          {
                      //              Arr[0] = _PDFPath;
                      //              Arr[1] = GAbsPdf;
                      //          }

                      //          MergePDF(_PDFPath, Arr);
                      //      }
                      //  }

                      //else
                      //{
                      //    ProcessEventHandler(GAbsPdf+" not found in CopyXMLPDFFromServerOrFMS");
                      //}
                   _isCopied = true;

                   ProcessEventHandler("PDFPath : " + _PDFPath);
                   ProcessEventHandler("XMLPath : " + _XMLPath);

                   ProcessEventHandler("ServerXML : " + ServerXML);
                   ProcessEventHandler("ServerPDF : " + ServerPDF);
                }
                else
                {
                   ProcessEventHandler("Trying to copy from FMS folder.");
                   CopyFromFMSFolderIfExist();
                }
            //}

            //For wiley issue
            AddToIssueArticleino();
            return _isCopied;
        }

        private void CopyFromFMSFolderIfExist()
        {
            string FMSFolder = "C:\\FMS";
           

            if (!Directory.Exists(FMSFolder))
            {
                Directory.CreateDirectory(FMSFolder);
            }

            string MNTPtrn = "MNT_" + _MNT.Client + "_" + "JOURNAL_" + _MNT.JID + "_" + _MNT.AID;

            

            ProcessEventHandler("MNTPtrn : " + MNTPtrn);

            string[] MNTFolder=  Directory.GetDirectories(FMSFolder ,MNTPtrn+ "*");

            ProcessEventHandler("FMSFolder : " + FMSFolder);
            ProcessEventHandler("MNTPtrn   : " + MNTPtrn );

            if (MNTFolder.Length > 0)
            {
                for (int i = 0; i < MNTFolder.Length; i++) 
                {
                    if (Directory.GetFiles(MNTFolder[i]).Length > 0)
                    {
                        CopyFromFMSFolder(MNTFolder[i]);
                        //string ProcessedMNTFolder = "C:\\FMS\\Processed\\" + Path.GetFileName(MNTFolder[i]);

                        //if (Directory.Exists(ProcessedMNTFolder))
                        //{
                        //    Directory.Delete(ProcessedMNTFolder, true);
                        //}
                        //Directory.Move(MNTFolder[i], ProcessedMNTFolder);
                    }
                    else
                    {
                        Directory.Delete(MNTFolder[i]);
                    }
                }
            }
            else
            {
               ProcessEventHandler(MNTPtrn + " does not exist.");    
            }
        }

        private void CopyFromFMSFolder(string MNTFolder)
        {

            string XMLFileName = _MNT.JID + _MNT.AID + ".xml";
            string PDFFileName = _MNT.AID + ".pdf";
            string QPDFFileName = _MNT.AID + "Q.pdf";
            string cPDFFileName = _MNT.AID + "c.pdf";


            string FMSXML = MNTFolder + "\\" + XMLFileName;
            string FMSPDF = MNTFolder + "\\" + PDFFileName;
            string FMSQPDF = MNTFolder + "\\" + QPDFFileName;

            string FMScPDF = MNTFolder + "\\" + cPDFFileName;
            if (File.Exists(FMSXML))
            {
                File.Copy(FMSXML, _XMLPath,true);
                ProcessEventHandler("FMSXML   : " + FMSXML);
            }
            else
            {
                ProcessEventHandler(FMSXML  + " does not exist");
                FMSXML = string.Empty;
            }

            if (File.Exists(FMSPDF))
            {
                File.Copy(FMSPDF, _PDFPath,true);

                
                if (File.Exists(FMSQPDF))
                {
                    ProcessEventHandler(" Q PDf exist at FMS.");
                    File.Copy(FMSQPDF, _QPDFPath, true);
                }
                else if (File.Exists("C:\\FMS\\" + _MNT.JID + _MNT.AID + "\\" + _MNT.JID + "-" + _MNT.AID + "Q.pdf"))
                {
                    File.Copy("C:\\FMS\\" + _MNT.JID + _MNT.AID + "\\" +  _MNT.JID + "-"  + _MNT.AID + "Q.pdf", _QPDFPath, true);
                }

                else
                {
                    ProcessEventHandler(" Q PDf start process in C FMS");
                    string Path = ConfigDetails.TDXPSGangtokFMSFolder + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\WorkArea\\" + _MNT.MNTFolder + "\\" + _MNT.AID + "Q.pdf";
                    if (File.Exists(Path))
                    {
                        ProcessEventHandler(" Q PDf exist at centralized_server.");
                        File.Copy(Path, _QPDFPath, true);
                        Thread.Sleep(10000);
                    }
                    else
                    {
                        Path = ConfigDetails.TDXPSGangtokTempFolder + "\\" + _MNT.MNTFolder + "\\" + _MNT.AID + "Q.pdf";
                        if (File.Exists(Path))
                        {
                            ProcessEventHandler(" Q PDf exist at Temp files.");
                            File.Copy(Path, _QPDFPath, true);
                            Thread.Sleep(10000);
                        }
                    }
                }
                if (File.Exists(FMScPDF))
                    File.Copy(FMScPDF, _cPDFPath, true);
                else if (File.Exists("C:\\FMC\\" + _MNT.JID + _MNT.AID + "\\" + _MNT.JID + "-" + _MNT.AID + "c.pdf"))
                {
                    File.Copy("C:\\FMC\\" + _MNT.JID + _MNT.AID + "\\" + _MNT.JID + "-" + _MNT.AID + "c.pdf", _QPDFPath, true);
                }
                else
                {
                    ProcessEventHandler("Is C PDf exist in C FMS");
                    string Path = ConfigDetails.TDXPSGangtokFMSFolder + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\WorkArea\\" + _MNT.MNTFolder + "\\" + _MNT.AID + "c.pdf";
                    if (File.Exists(Path))
                    {
                        ProcessEventHandler(" c PDf exist at centralized_server.");
                        File.Copy(Path, _cPDFPath, true);
                        Thread.Sleep(10000);
                    }
                    else
                    {
                        Path = ConfigDetails.TDXPSGangtokTempFolder + "\\" + _MNT.MNTFolder + "\\" + _MNT.AID + "c.pdf";
                        if (File.Exists(Path))
                        {
                            ProcessEventHandler(" c PDf exist at Temp files.");
                            File.Copy(Path, _cPDFPath, true);
                            Thread.Sleep(10000);
                        }
                    }
                }
                try
                {
                    _PdfPageCount = PdfProcess.PDF.GetPdfPageCount(_PDFPath);
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                }
                //===========================Read Author Query Form==============================
                //string Querypage = Path.GetDirectoryName(_PDFPath) + "\\QueryPage.pdf";
                //string OutPut2 = _PDFPath.Replace(".pdf", "_1.pdf");
                //try
                //{
                //    PdfReader reader = new PdfReader(_PDFPath);

                //    int PageCount = reader.NumberOfPages;
                //    for (int i = PageCount; i > (PageCount - 2); i--)
                //    {
                //        string PageText = PdfTextExtractor.GetTextFromPage(reader, i);
                //        if (PageText.Contains("AUTHOR QUERY FORM"))
                //        {

                //            ExtractPages(_PDFPath, Querypage, i, i);
                //            ExtractPages(_PDFPath, OutPut2, 1, i - 1);
                //            File.Delete(_PDFPath);
                //            File.Move(OutPut2, _PDFPath);

                //        }
                //        else
                //        {
                //            //LogWrite
                //        }
                //    }
                //    reader.Close();
                //    reader = null;

                //}
                //catch (Exception ex)
                //{
                //    ProcessErrorHandler(ex);
                //}
                //===========================Read end==============================
              //  string GAbsPdf = FMSPDF.Replace(".pdf", "c.pdf");  ///////Graphical Abstract pdf
                //if (File.Exists(GAbsPdf))
                //{
                //    string[] Arr = new string[2];
                //    Arr[0] = GAbsPdf;
                //    Arr[1] = _PDFPath;
                //    if (_MNT.JID.Equals("IEAM"))
                //    {
                //        Arr[0] = _PDFPath;
                //        Arr[1] = GAbsPdf;
                //    }
                //    MergePDF(_PDFPath, Arr);
                //}

                //28/07/2017 NJD has been stopped
                //if (File.Exists(GAbsPdf))
                //{
                //    objNjd = new MergeNjdPdf();
                //    if (!objNjd.StartProcess(_MNT.JID, _MNT.AID, _PDFPath, GAbsPdf,_MNT.Stage))
                //    {
                //        string[] Arr = new string[2];
                //        Arr[0] = GAbsPdf;
                //        Arr[1] = _PDFPath;
                //        if (_MNT.JID.Equals("IEAM"))
                //        {
                //            Arr[0] = _PDFPath;
                //            Arr[1] = GAbsPdf;
                //        }
                //        MergePDF(_PDFPath, Arr);
                //    }
                //}
                //else
                //{
                //    ProcessEventHandler(GAbsPdf+" not exist");
                //}
                ProcessEventHandler("FMSPDF   : " + FMSPDF);
                _isCopied = true;
            }
            else
            {
                ProcessEventHandler(FMSPDF + " does not exist");
            }

            
            
        }

        private void MergePDF(string  InputPDF,string[] sSrcFile)
        {
            string MergePDFPath = InputPDF.Replace(".pdf", "_1.pdf");

            PdfProcess.MergePdf MergePdfOBJ = new PdfProcess.MergePdf();
            MergePdfOBJ.MergeFiles(MergePDFPath, sSrcFile);
            if (File.Exists(MergePDFPath))
            {
                File.Delete(InputPDF);
                File.Move(MergePDFPath, InputPDF);
            }
        }

        public void AddToIssueArticleino()
        {
            if (File.Exists(_XMLPath) == false  || _MNT.Client.Equals("Thieme", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            try
            {
                string XML = File.ReadAllText(_XMLPath);
                int sPos = XML.IndexOf("<header");
                int ePos = sPos != -1 ? XML.IndexOf("<body", sPos) + 1 : 0;


                if (sPos > 0 && ePos > 0)
                {
                    string Header = ePos > 0 ? XML.Substring(sPos, ePos - sPos - 1) : "";
                    string OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;

                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@JID", _MNT.JID);
                    param[1] = new SqlParameter("@AID", _MNT.AID);
                    param[2] = new SqlParameter("@HEADER", Header.Replace("&","#$#"));
                    SqlHelper.ExecuteNonQuery(OPSConStr, System.Data.CommandType.StoredProcedure, "[usp_IssArticleInfo]", param);

                    ProcessEventHandler("Headed xml has been add to IssueArticleino table");
                }

            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }

        }
        //private static void ExtractPages(string sourcePDFpath, string outputPDFpath, int startpage, int endpage)
        //{
        //    PdfReader reader = null;
        //    Document sourceDocument = null;
        //    PdfCopy pdfCopyProvider = null;
        //    PdfImportedPage importedPage = null;

        //    reader = new PdfReader(sourcePDFpath);
        //    sourceDocument = new Document(reader.GetPageSizeWithRotation(startpage));
        //    pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPDFpath, System.IO.FileMode.Create));

        //    sourceDocument.Open();

        //    for (int i = startpage; i <= endpage; i++)
        //    {
        //        importedPage = pdfCopyProvider.GetImportedPage(reader, i);
        //        pdfCopyProvider.AddPage(importedPage);
        //    }
        //    sourceDocument.Close();
        //    reader.Close();
        //    System.Threading.Thread.Sleep(10000);
        //}
    }
}
