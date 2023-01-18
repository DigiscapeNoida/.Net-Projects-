using System;
using System.Configuration;
using System.IO;
using ProcessNotification;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWWeProof
{
    class GetInput : MessageEventArgs
    {
        string _ExePath = string.Empty;
        string _XMLPath = string.Empty;
        string _PDFPath = string.Empty;
        string _ProcessPath = string.Empty;
        string _ServerPath = string.Empty;
        string _PrcsAIDFolder = string.Empty;

        MNTInfo _MNT = null;
        bool _isCopied = false;


        public string XMLPath
        {
            get { return _XMLPath; }
        }

        public string PDFPath
        {

            get { return _PDFPath; }
        }
        public string PrcsAIDFolder
        {

            get { return _PrcsAIDFolder; }
        }
        
        public GetInput(MNTInfo MNT)
        {
            _MNT = MNT;
            _ProcessPath = ConfigDetails.ProcessPath;
        }

        public bool CopyXMLPDFFromServerOrFMS()
        {
            string XMLFileName =  _MNT.AID + ".xml";
            string PDFFileName =  _MNT.AID + ".pdf";

            _PrcsAIDFolder = _ProcessPath + "\\" + _MNT.AID;


            _XMLPath = _PrcsAIDFolder + "\\" + XMLFileName;
            _PDFPath = _PrcsAIDFolder + "\\" + PDFFileName;

            if (Directory.Exists(_PrcsAIDFolder))
            {
                Directory.Delete(_PrcsAIDFolder, true);
            }
            Directory.CreateDirectory(_PrcsAIDFolder);

            CopyFromFMSFolderIfExist();
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

            string[] MNTFolder = Directory.GetDirectories(FMSFolder, MNTPtrn + "*");

            ProcessEventHandler("FMSFolder : " + FMSFolder);
            ProcessEventHandler("MNTPtrn   : " + MNTPtrn);

            if (MNTFolder.Length == 0)
            {
                Directory.CreateDirectory(FMSFolder + "\\" + MNTPtrn);
                MNTFolder = Directory.GetDirectories(FMSFolder, MNTPtrn + "*");
            }
            else
            {
                Directory.Delete(FMSFolder + "\\" + MNTPtrn, true);
                Directory.CreateDirectory(FMSFolder + "\\" + MNTPtrn);
                MNTFolder = Directory.GetDirectories(FMSFolder, MNTPtrn + "*");
            }

            if (MNTFolder.Length > 0 )
            {
                for (int i = 0; i < MNTFolder.Length; i++)
                {
                    if (Directory.GetFiles(MNTFolder[i]).Length >= 0)
                    {
                        CopyFromFMSFolder(MNTFolder[i]);
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

            string XMLFileName = "tx1.xml";
            string PDFFileName = _MNT.AID + ".pdf";

            string FMSXML = MNTFolder + "\\" + XMLFileName;
            string FMSPDF = MNTFolder + "\\" + PDFFileName;
            if (File.Exists(FMSXML))
            {
                File.Copy(FMSXML, _XMLPath,true);
                ProcessEventHandler("FMSXML   : " + FMSXML);
            }
            else
            {
                ProcessEventHandler(FMSXML + " does not exist");
                FMSXML = string.Empty;
            }

            if (File.Exists(FMSPDF))
            {
                File.Copy(FMSPDF, _PDFPath,true);
                ProcessEventHandler("FMSPDF   : " + FMSPDF);
                _isCopied = true;
            }
            else
            {
                ProcessEventHandler(FMSPDF + " does not exist");
                string FMSPath = ConfigurationManager.AppSettings["FMS"];
                if (!string.IsNullOrEmpty(FMSPath))
                {
                    string OutPutPath = FMSPath.TrimEnd(new char[]{'\\'}) + "\\" + _MNT.JID + "\\" + _MNT.AID + "\\" + _MNT.AID + ".pdf";

                    if (File.Exists(OutPutPath))
                    {
                        File.Copy(OutPutPath, FMSPDF,true);
                        File.Copy(FMSPDF, _PDFPath,true);
                        _isCopied = true;
                    }
                }
            }
        }

        
    }
}
