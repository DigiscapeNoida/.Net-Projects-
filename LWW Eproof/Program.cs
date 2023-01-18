using System;
using ProcessNotification;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace LWWeProof
{
    
    class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int SW_MINIMIZE = 6;
        private const int SW_MAXIMIZE = 3;
        private const int SW_RESTORE = 9;
        static WriteLog _WriteLogObj = null;
        public static string EXELoc = string.Empty;
        static List<FreshEProofProcess> FrshArtclList = new List<FreshEProofProcess>();
        static void Main(string[] args)
        {
            Console.Title = "LWWEproof";
            string ExeName = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (System.Diagnostics.Process.GetProcessesByName(ExeName).Length > 1)
            {
                return;
            }
            IntPtr winHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(winHandle, SW_MINIMIZE);
            EXELoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            EXELoc= ConfigDetails.EXELoc;

            if (args.Length==0)
                ToEditor();
            else
                Start();
        }


        private static void   Start()
        {
            try
            {
                _WriteLogObj = new WriteLog(EXELoc + "\\LWWDS");

                ProcessNotification("Start process to check pdf existence in Hot folder.");
                DBProcess.DBConStr = ConfigDetails.LWWConStr;

                ProcessNotification("Start to create MNT folder and moveing to FMS folder");
                ProcessEproof();
                ProcessNotification("Process end to create MNT folder and moving to FMS folder");


                ProcessNotification("Start process to check and create dataset called");

                LWWeProof eProofObj = new LWWeProof();
                eProofObj.ProcessNotification += ProcessNotification;
                eProofObj.ErrorNotification += ErrorNotification;
                eProofObj.StartProcess();

                ProcessNotification("Process finished..");

                _WriteLogObj.WriteLogFileInDate();
            }
            catch (Exception e)
            {
                ProcessNotification("Error : " + e.ToString());
                
            }

        }
        private static void ProcessEproof()
        {
            try
            {
                ProcessNotification("Process atart for S100" + ConfigDetails.S100eProofInPut);
                if (!string.IsNullOrEmpty(ConfigDetails.S100eProofInPut))
                {
                    ProcessNotification("S100 1");
                    if (Directory.Exists(ConfigDetails.S100eProofInPut))
                    {
                        ProcessNotification("S100 2");
                        string[] PDFs = Directory.GetFiles(ConfigDetails.S100eProofInPut, "*.pdf");
                        ProcessNotification("S100 Pdf Count : " + PDFs.Length);

                        ProcessNotification("S100 3");
                        foreach (string pdf in PDFs)
                        {
                            ProcessNotification("PDF-S100 " + pdf);
                            InsertMessage(pdf, "S100");
                        }
                        ProcessNotification("S100 4");
                    }
                }

                ProcessNotification("Processing for S200");
                if (!string.IsNullOrEmpty(ConfigDetails.S200eProofInPut))
                {
                    ProcessNotification("S200 1");
                    if (Directory.Exists(ConfigDetails.S200eProofInPut))
                    {
                        ProcessNotification("S200 3");
                        string[] PDFs = Directory.GetFiles(ConfigDetails.S200eProofInPut, "*.pdf");
                        ProcessNotification("S200 Pdf Count : " + PDFs.Length);
                        ProcessNotification("S200 4");
                        foreach (string pdf in PDFs)
                        {
                            ProcessNotification("PDF-S200 " + pdf);
                            InsertMessage(pdf, "S200");
                        }
                        ProcessNotification("S200 5");
                    }
                }
            }
            catch (Exception e)
            {
                ProcessNotification("ERROR :" + e.ToString());
                
            }

        }
        private static bool InsertMessage(string AIDPdf, string Stage)
        {
            ProcessNotification("Processing for AIDPDF " + Stage + ":" + AIDPdf);

            MoveToFMS MoveToFMSObj = new MoveToFMS();
            MoveToFMSObj.ProcessNotification += ProcessNotification;
            MoveToFMSObj.ErrorNotification += ErrorNotification;

            if (MoveToFMSObj.InsertMessage(AIDPdf, Stage) ==false)
            {
                ProcessNotification("InsertMessage has been failed");

                string ErrFile = AIDPdf.Replace(".pdf", ".err");

                ProcessNotification("Trying to pdf to err file.");

                if (File.Exists(ErrFile))
                    File.Delete(ErrFile);

                File.Move(AIDPdf, ErrFile);
            }
            return true;
        }

        public static void TaskNotFoundMail(string AIDPdf, string Stage)
        {
            MailDetail MailDtlObj = new MailDetail();
            MailDtlObj.MailFrom = "eproof@thomsondigital.com";
            MailDtlObj.MailTo = ConfigDetails.MailTo;
            MailDtlObj.MailCC = ConfigDetails.MailCC;
            MailDtlObj.MailBCC = ConfigDetails.MailBCC;
            MailDtlObj.MailSubject = "Failure notification of " + Path.GetFileName(AIDPdf) + "(" + Stage + ")";

            MailDtlObj.MailBody = "There is no information found for above mention article. Send it manually. \n\nWith regards,\n\nAuto eProof System";

            eMailProcess eMailProcessObj = new eMailProcess();
            eMailProcessObj.ProcessNotification += ProcessNotification;
            eMailProcessObj.ErrorNotification += ErrorNotification; ;


            ProcessNotification("Going to send error mail.");
            eMailProcessObj.SendMailInternal(MailDtlObj);
        }

        

        private static void ToEditor()
        {
            _WriteLogObj = new WriteLog(EXELoc);

            eProofObj_ProcessNotification("Getting S100Path Path from config file.");
            string S100Path = ConfigurationManager.AppSettings["S100InPut"];
            eProofObj_ProcessNotification("S100Path :: " + S100Path);

            eProofObj_ProcessNotification("Getting S100Path Path from config file.");
            string S200Path = ConfigurationManager.AppSettings["S200InPut"];
            eProofObj_ProcessNotification("S200Path :: " + S200Path);

            StartProcess(S100Path, "S100");

            StartProcess(S200Path, "S200");

            _WriteLogObj.WriteLogFileInDate();
        }
        static void StartProcess(string PrcsPath , string Stage)
        {
            FrshArtclList.Clear();
            string[] pdfFls = Directory.GetFiles(PrcsPath, "*.pdf");
            Array.Reverse(pdfFls);
            Array.Sort(pdfFls);

            foreach (string pdfFl in pdfFls)
            {
                string OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
                string Fname = Path.GetFileNameWithoutExtension(pdfFl);
                string[] JIDAIDArr = Fname.Split(new char[] { '_', '-' });
                string JIDAID = string.Empty;
                if (JIDAIDArr.Length > 1)
                {
                    string JID = JIDAIDArr[0].ToUpper();

                    if (Stage.Equals("S100") && "#MD#JAM#".IndexOf("#" + JID + "#") == -1)
                    {
                        Console.WriteLine("Invalid JID in S100. Only JAM and MD allow.");
                        MoveToErr(pdfFl);
                        
                        continue;
                    }
                    else if (Stage.Equals("S200") && "#MD#".IndexOf("#" + JID + "#") == -1)
                    {
                        Console.WriteLine("Invalid JID in S200. Only MD allow.");
                        MoveToErr(pdfFl);
                        continue;
                    }

                    string AID = Fname.ToUpper().Replace(JID, "").Trim(new char[] { ' ', '_', '-', '\t', '.' });
                    MNTInfo MNTObj = new MNTInfo("LWW", JID, AID, Stage);
                    FreshEProofProcess eProofObj = new FreshEProofProcess(MNTObj, pdfFl);
                    eProofObj.ProcessNotification += eProofObj_ProcessNotification;
                    eProofObj.ErrorNotification += eProofObj_ErrorNotification;
                    eProofObj.StartValidation();

                    if (eProofObj.IsValidJID)
                    {
                        if (eProofObj.ValidationResult)
                        {
                            FrshArtclList.Add(eProofObj);
                        }
                        else
                        {
                            eProofResultNotification.InternalReviseMail(eProofObj);
                            File.Delete(pdfFl);
                        }
                    }
                }
                else
                {
                    eProofObj_ProcessNotification("Wrong file name..");
                    eProofObj_ProcessNotification("File name must be JID_AID.pdf");
                }
            }

            var queryJIDWisePdfs = from Artcl in FrshArtclList
                                   group Artcl by Artcl.MNT.JID into newGroup
                                   orderby newGroup.Key
                                   select newGroup;
            foreach (var nameGroup in queryJIDWisePdfs)
            {
                ToProcess(nameGroup.ToList());
            }
        }

        private static void MoveToErr(string pdfFl)
        {
            string errFile = pdfFl.Replace(".pdf", ".err");
            if (File.Exists(errFile))
                File.Delete(errFile);
            File.Move(pdfFl, errFile);
        }
        static void eProofObj_ErrorNotification(Exception Ex)
        {
            _WriteLogObj.AppendLog("---------Start eProofObj_ErrorNotification detail------ ");
            _WriteLogObj.AppendLog(Ex.StackTrace);
            _WriteLogObj.AppendLog("---------End Error detail------ ");
        }
        static void eProofObj_ProcessNotification(string NotificationMsg)
        {
            Console.WriteLine(NotificationMsg);
            _WriteLogObj.AppendLog(NotificationMsg);  
        }

        static void ProcessNotification(string NotificationMsg)
        {
            Console.WriteLine(NotificationMsg);
            _WriteLogObj.AppendLog(NotificationMsg);
        }
        static void ErrorNotification(Exception Ex)
        {
            _WriteLogObj.AppendLog("---------Start ErrorNotification detail------ ");
            _WriteLogObj.AppendLog(Ex.StackTrace);
            _WriteLogObj.AppendLog("---------End Error detail------ ");
        }


        static void ToProcess(List<FreshEProofProcess> JIDwiseList)
        {


              List<FreshEProofProcess> SendArtclList = JIDwiseList.FindAll(x => x.IsAlreadyProcessed == false);
              if (SendArtclList != null && SendArtclList.Count>0) 
              {
                   string[] pdfFiles= new string[SendArtclList.Count];
                   int count =0;
                    string AIDs = string.Empty;
                    foreach (FreshEProofProcess ePrf in SendArtclList)
                    {
                        AIDs =AIDs + ePrf.MNT.JID+ "-" + ePrf.MNT.AID + "\n";
                        pdfFiles[count]= ePrf.PDFPath;
                        count++;
                    }

                  ////////////To make annotation

                    eProofObj_ProcessNotification("To make annotation" );
                    MakeAnn(pdfFiles);

                    FreshEProofProcess eProof= SendArtclList[0];
                    eMailProcess s = new eMailProcess ();
                    MailDetail MailDetailOBJ = new MailDetail();
                    MailDetailOBJ.MailFrom   = eProof.OPSRvsObj.MailFrom ;
                    MailDetailOBJ.MailTo     = eProof.OPSRvsObj.MailTo;
                    MailDetailOBJ.MailCC     = eProof.OPSRvsObj.MailCC;
                    MailDetailOBJ.MailBCC    = eProof.OPSRvsObj.MailBCC;


                  //Plz dont change sequence of below lines
                    MailDetailOBJ.MailBody = eProof.MailBody.Replace("<AID>", AIDs);

                     if (JIDwiseList[0].MNT.Stage.Equals("S100"))
                         MailDetailOBJ.MailSubject = eProof.MNT.JID + " Articles for review";
                     else
                     {
                         MailDetailOBJ.MailBody = MailDetailOBJ.MailBody.Replace("Fresh", "Revise");
                         MailDetailOBJ.MailSubject = eProof.MNT.JID + " Revised proofs for review";
                     }

                    

                    string ZipPath ;
                    AddeProofPdf(SendArtclList,out ZipPath);
                    if (File.Exists(ZipPath))
                    {
                        MailDetailOBJ.MailAtchmnt.Add(ZipPath);
                        eMailProcess eMailProcessOBJ = new eMailProcess();
                        if (eMailProcessOBJ.SendMailExternal(MailDetailOBJ))
                        {
                            foreach (FreshEProofProcess ePrf in SendArtclList)
                            {
                                ePrf.InsertHistory();
                                File.Delete(ePrf.PDFPath);
                            }
                        }
                    }
              }
        }
        static bool AddeProofPdf( List<FreshEProofProcess> SendArtclList , out string  ZipPath )
        {

            string TempPath = string.Empty;

            if (SendArtclList[0].MNT.Stage.Equals("S100"))
                TempPath = "C:\\Temp\\" + SendArtclList [0].MNT.JID+ " Fresh Articles Proofs";
            else
                TempPath = "C:\\Temp\\" + SendArtclList[0].MNT.JID + " Revised proofs Proofs";

            
                ZipPath = TempPath + ".zip";


                if (File.Exists(ZipPath))
                {
                    File.Delete(ZipPath);
                }
                if (Directory.Exists(TempPath))
                {
                    Directory.Delete(TempPath, true);
                }
                Directory.CreateDirectory(TempPath);

                foreach(FreshEProofProcess ePrf in SendArtclList)
                {
                    string Frm = ePrf.PDFPath;
                    string To   = TempPath + "\\" + Path.GetFileName(ePrf.PDFPath);
                    File.Copy(Frm, To,true);
                }

                try
                {
                    string eProofPdf = string.Empty;
                    FastZip unzip1 = new FastZip();
                    unzip1.CreateZip(ZipPath, TempPath, true, "");
                    Directory.Delete(TempPath, true);
                    return  true;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.InnerException);
                    Console.WriteLine("Error in extracting zip file" + Ex.Message);
                    return false;
                }
            }

        static void MakeAnn(string[] pdfFiles)
        {

            eProofObj_ProcessNotification("Annotation process start..");

            string AnnPDF = ConfigurationManager.AppSettings["AnnPDF"];
            eProofObj_ProcessNotification("Annotation process start..");

            string[] INFile  =  new string[pdfFiles.Length];
            string[] OutFile =  new string[pdfFiles.Length];

            int count=0;
            foreach (string pdfFile in pdfFiles)
            {
                  string fileName = Path.GetFileName(pdfFile);
                  INFile[count]  = AnnPDF + "\\" + fileName;
                  OutFile[count] = AnnPDF + "\\out\\temp_" + fileName;

                  if (File.Exists(OutFile[count]))
                      File.Delete(OutFile[count]);

                  File.Copy(pdfFile, INFile[count], true);

                  count++;
            }
            count = 0;
            if (Directory.Exists(AnnPDF))
            {
                foreach (string pdfFile in pdfFiles)
                {

                    eProofObj_ProcessNotification("pdfFile :" + pdfFile);

                  
                    
                    eProofObj_ProcessNotification(AnnPDF);

                    while (File.Exists(INFile[count]))
                    {
                        eProofObj_ProcessNotification("Waiting to annotation process finish..");

                        System.Threading.Thread.Sleep(10000);
                    }
                    while (true)
                    {
                        if (File.Exists(OutFile[count]))
                        {
                            if (File.Exists(pdfFile)) File.Delete(pdfFile);

                            File.Move(OutFile[count], pdfFile);

                            eProofObj_ProcessNotification("Annotation process finished.");
                            break;
                        }
                    }
                    count++;
                }
            }
        }
        static void MakeAnn(string _UploadFileName)
        {

            string AnnPDF = ConfigurationManager.AppSettings["AnnPDF"];
            eProofObj_ProcessNotification("Annotation process start..");

            string fileName = Path.GetFileName(_UploadFileName);

            if (Directory.Exists(AnnPDF))
            {
                string INFile = AnnPDF + "\\" + fileName;
                string OutFile =AnnPDF + "\\out\\temp_" + fileName;

                if (File.Exists(OutFile))
                    File.Delete(OutFile);

                File.Copy(_UploadFileName, INFile,true);

                eProofObj_ProcessNotification(AnnPDF);

                while (File.Exists(INFile))
                {
                    eProofObj_ProcessNotification("Waiting to annotation process finish..");

                    System.Threading.Thread.Sleep(1000);
                }
                while (true)
                {
                    if (File.Exists(OutFile))
                    {
                        if (File.Exists(_UploadFileName)) File.Delete(_UploadFileName);

                        File.Move(OutFile, _UploadFileName);

                        eProofObj_ProcessNotification("Annotation process finished.");
                        break;
                    }
                }
            }
            
        }
    }
}

