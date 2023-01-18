using System;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessNotification;

namespace LWWeProof
{
    class MoveToFMS:MessageEventArgs
    {
        readonly string NtwrkLoc = string.Empty;
        string _FMSMNT         = string.Empty;
        string _FMSXML         = string.Empty;
        string _FMSPDF         = string.Empty;

        string _NtwrkLocFMSXML = string.Empty;
        string _NtwrkLocFMSPDF = string.Empty;
        AIDInfo _AIDObj        = null;
        MNTInfo _MNT           = null;


        public AIDInfo AIDObj
        {
            get { return _AIDObj; }
        }
        public MoveToFMS()
        {
             NtwrkLoc = ConfigurationManager.AppSettings["NtwrkLoc"];
        }

        private void CreateMNTFolder()
        {
            string XMLFileName = _MNT.AID + ".xml";
            string PDFFileName = _MNT.AID + ".pdf";
           

            _FMSXML = _FMSMNT + "\\" + XMLFileName;
            _FMSPDF = _FMSMNT + "\\" + PDFFileName;

             string NtwrkMNTFolder = NtwrkLoc + "\\"+ _MNT.MNTFolder;

            _NtwrkLocFMSXML = NtwrkMNTFolder + "\\" + XMLFileName;
            _NtwrkLocFMSPDF = NtwrkMNTFolder + "\\" + PDFFileName;

            if (Directory.Exists(NtwrkMNTFolder))
                Directory.Delete(NtwrkMNTFolder, true);

            
            Directory.CreateDirectory(NtwrkMNTFolder);
            ProcessMessage(NtwrkMNTFolder + " folder created successfully..");

            if (File.Exists(_FMSXML))
            {
                File.Copy(_FMSXML, _NtwrkLocFMSXML,true);
                ProcessMessage(_NtwrkLocFMSXML + " file copied successfully..");
            }
            else
            {
                _FMSXML = _FMSMNT + "\\tx1.xml" ;
                if (File.Exists(_FMSXML))
                {
                    File.Copy(_FMSXML, _NtwrkLocFMSXML,true);
                    ProcessMessage(_NtwrkLocFMSXML + " file copied successfully..");
                }
                else
                {
                    _FMSXML = _FMSMNT + "\\" + _MNT.AID + ".xml";
                    if (File.Exists(_FMSXML))
                    {
                        File.Copy(_FMSXML, _NtwrkLocFMSXML,true);
                        ProcessMessage(_NtwrkLocFMSXML + " file copied successfully..");
                    }
                }



            }

            if (File.Exists(_FMSPDF))
            {
                File.Copy(_FMSPDF, _NtwrkLocFMSPDF,true);
               
                ProcessMessage(_NtwrkLocFMSPDF + " file copied successfully..");
            }
           
        }


       

        public  bool InsertMessage (string PDFPath,string Stage)
        {
             ProcessEventHandler("InsertMessage Start");
             ProcessEventHandler("NtwrkLoc :: " + NtwrkLoc);

            
             bool isMsgInsrt = false;

             ProcessEventHandler("Create DBProcess Object");
             DBProcess.DBConStr = ConfigDetails.LWWConStr;
             DBProcess _DBObj   = DBProcess.DBProcessObj;
             

             
             string AID       = Path.GetFileNameWithoutExtension(PDFPath);
             ProcessEventHandler("AID :: " + AID);
             ProcessEventHandler("FMS Stage :: " + Stage);

             ProcessEventHandler("Trying to fetch AID details" );

             var AIDDtl       = _DBObj.usp_GetAIDDetailsResult(AID, Stage);

             ProcessEventHandler("usp_GetAIDDetailsResult has been called");

            


            foreach (usp_GetAIDDetailsResult a in AIDDtl)
            {
                _AIDObj = new AIDInfo();
                _AIDObj.JID = a.JID;
                _AIDObj.AID = a.AID;

                ProcessEventHandler("_AIDObj.JID  :: " + _AIDObj.JID);
                ProcessEventHandler("_AIDObj.AID  :: " + _AIDObj.AID);

                ProcessEventHandler("Creating MNTInfo object");

                _MNT = new MNTInfo("LWW", a.JID, a.AID, Stage);
                
                string InPutDir = Path.GetDirectoryName(PDFPath);
                       _FMSMNT = InPutDir+"\\" + _MNT.MNTFolder;

                ProcessEventHandler("InPutDir :: " + InPutDir);

                ProcessEventHandler("_FMSMNT :: " + _FMSMNT);

                if (Directory.Exists(_FMSMNT))
                {
                    Directory.Delete(_FMSMNT,true);
                }
                Directory.CreateDirectory(_FMSMNT);
                string PDFinMNT = _FMSMNT + "\\" + Path.GetFileName(PDFPath);

                ProcessEventHandler("PDFinMNT :: " + PDFinMNT);

                if (File.Exists(PDFinMNT))
                {
                    File.Delete(PDFinMNT);
                }
                File.Move(PDFPath,PDFinMNT);


                ProcessEventHandler("CreateMNTFolder");
                CreateMNTFolder();

                ProcessEventHandler("InsertMessageDetail has been called");

                _DBObj.InsertMessageDetail(_AIDObj.JID,_AIDObj.AID,Stage);

                ProcessEventHandler("Message has been inserted successfully.");
                isMsgInsrt = true;
            }

            ProcessEventHandler("InsertMessage Finished");
            return  isMsgInsrt;
        }
        

        private void ProcessMessage(string Msg)
        {
            ProcessEventHandler(Msg);
                
        }

    }
}
