using System;
using System.Net.NetworkInformation;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Collections.Generic;
using ProcessNotification;
using System.Text;
using System.Diagnostics;

namespace LWWeProof
{
    class ConfigDetails
    {
        static string _S100ToEditorInPut = string.Empty;
        static string _S200ToEditorInPut = string.Empty;
        static string _S100eProofInPut = string.Empty;
        static string _S200eProofInPut = string.Empty;
        static string _LWWConStr = string.Empty;
        static string _LWWPDFCountFile = string.Empty;
        static string _EXELoc = string.Empty;

        static string _FtpUrl = string.Empty;
        static string _FtpUsr = string.Empty;
        static string _FtpPwd = string.Empty;

        static string _MailTo = string.Empty;
        static string _MailCC = string.Empty;
        static string _MailBCC = string.Empty;

        static string _PrfPath = string.Empty;

        static string _ProcessPath = string.Empty;

        static string[] _S100TaskList = null;
        static string[] _S200TaskList = null;


        static void FillTask()
        {
            _S100TaskList = File.ReadAllLines(_EXELoc + "\\S100Task.txt");
            _S200TaskList = File.ReadAllLines(_EXELoc + "\\S200Task.txt");
        }
        public static string[] S100TaskList
        {
            get { return _S100TaskList; }
        }
        public static string[] S200TaskList
        {
            get { return _S200TaskList; }
        }




        private ConfigDetails()
        {
        }
        static ConfigDetails()
        {
            _EXELoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string[] KEYS = ConfigurationManager.AppSettings.AllKeys;
            foreach (string key in KEYS)
            {
                string KeyVal = ConfigurationManager.AppSettings[key];
                switch (key)
                {
                    case "S100ToEditorInPut":
                        {
                            _S100ToEditorInPut = KeyVal;
                            break;
                        }
                    case "S200ToEditorInPut":
                        {
                            _S200ToEditorInPut = KeyVal;
                            break;
                        }
                    case "S100eProofInPut":
                        {
                            _S100eProofInPut = KeyVal;
                            break;
                        }
                    case "S200eProofInPut":
                        {
                            _S200eProofInPut = KeyVal;
                            break;
                        }
                    case "FtpUrl":
                        {
                            _FtpUrl = KeyVal;
                            break;
                        }
                    case "FtpUsr":
                        {
                            _FtpUsr = KeyVal;
                            break;
                        }
                    case "FtpPwd":
                        {
                            _FtpPwd = KeyVal;
                            break;
                        }
                    case "MailTo":
                        {
                            _MailTo = KeyVal;
                            break;
                        }
                    case "MailCC":
                        {
                            _MailCC = KeyVal;
                            break;
                        }
                    case "MailBCC":
                        {
                            _MailBCC = KeyVal;
                            break;
                        }
                    case "PrfPath":
                        {
                            _PrfPath = KeyVal;
                            break;
                        }
                    case "LWWPDFCountFile":
                        {
                            _LWWPDFCountFile = KeyVal;
                            break;
                        }

                }
            }
            try
            {
                _LWWConStr = ConfigurationManager.ConnectionStrings["LWWConnectionString"].ConnectionString;
                FillTask();

                _ProcessPath = _EXELoc + "\\Process";
                if (!Directory.Exists(_ProcessPath))
                    Directory.CreateDirectory(_ProcessPath);
            }
            catch //(Exception e)
            {
            }
        }
        public static string MailTo { get { return _MailTo; } }
        public static string MailCC { get { return _MailCC; } }
        public static string MailBCC { get { return _MailBCC; } }
        public static string PrfPath { get { return _PrfPath; } }
        public static string LWWPDFCountFile { get { return _LWWPDFCountFile; } }


        public static string FtpUrl { get { return _FtpUrl; } }
        public static string FtpUsr { get { return _FtpUsr; } }
        public static string FtpPwd { get { return _FtpPwd; } }

        public static string EXELoc { get { return _EXELoc; } }
        public static string S100ToEditorInPut { get { return _S100ToEditorInPut; } }
        public static string S200ToEditorInPut { get { return _S200ToEditorInPut; } }
        public static string S100eProofInPut { get { return _S100eProofInPut; } }
        public static string S200eProofInPut { get { return _S200eProofInPut; } }
        public static string LWWConStr { get { return _LWWConStr; } }
        public static string ProcessPath { get { return _ProcessPath; } }

        public static void MakeAnn(string[] pdfFiles)
        {

            if (pdfFiles == null || pdfFiles.Length == 0)
            {
                return;
            }

            string AnnPDF = ConfigurationManager.AppSettings["AnnPDF"];

            string[] INFile = new string[pdfFiles.Length];
            string[] OutFile = new string[pdfFiles.Length];

            int count = 0;
            foreach (string pdfFile in pdfFiles)
            {
                string fileName = Path.GetFileName(pdfFile);
                INFile[count] = AnnPDF + "\\" + fileName;
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
                    while (File.Exists(INFile[count]))
                    {
                        System.Threading.Thread.Sleep(10000);
                        Console.WriteLine("Waiting for pdf annotation complete.");
                        //Console.WriteLine("Eproof called.");
                        //ProcessStartInfo startInfo = new ProcessStartInfo();
                        //startInfo.CreateNoWindow = false;
                        //startInfo.UseShellExecute = false;
                        //startInfo.FileName = @"D:\AutoEProof\AutoEProof.exe";
                        //startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                        //using (Process exeProcess = Process.Start(startInfo))
                        //{
                        //    exeProcess.WaitForExit();
                        //}
                        //Console.WriteLine("Eproof called success.");
                    }
                    while (true)
                    {
                        if (File.Exists(OutFile[count]))
                        {
                            if (File.Exists(pdfFile)) File.Delete(pdfFile);

                            File.Move(OutFile[count], pdfFile);
                            break;
                        }
                    }
                    count++;
                }
            }
        }

    }
}