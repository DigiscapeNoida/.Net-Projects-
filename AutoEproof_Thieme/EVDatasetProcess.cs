using System;
using ProcessNotification;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLayer;

namespace AutoEproof
{
    class EVDatasetProcess:FMSStructure
    {
        public EVDatasetProcess(string Client, string JID, string AID, string FMSFolder)
            : base( Client, JID,  AID, FMSFolder)
        { 

        }
        public void StartProcess(string Zip)
        {
            ProcessToCopy(Zip);
        }
        void ProcessToCopy( string CopyFrom)
        {

            if (Directory.Exists(this.AIDFolder))
            {
                string S280dataset = this.S280dataset;
                if (!Directory.Exists(S280dataset))
                {
                    Directory.CreateDirectory(S280dataset);
                }
                string CopyTo = S280dataset + "\\" + Path.GetFileName(CopyFrom);

                File.Copy(CopyFrom, CopyTo, true);
                FileInfo CFrm = new FileInfo(CopyFrom);
                FileInfo CTo = new FileInfo(CopyTo);

                if (CFrm.Length == CTo.Length)
                {
                    File.Delete(CopyFrom);
                }
            }
        }
    }

    class EVDataset:MessageEventArgs
    {
        string _ProcesPath = string.Empty;
        public  EVDataset(string ProcessPath)
        {
            _ProcesPath = ProcessPath; 
        }

        public void StartProcess()
        { 
            string [] Zips = Directory.GetFiles(_ProcesPath,"*.zip");

            ProcessEventHandler("Zip files to copied " + Zips.Length);
            foreach (string Zip in Zips)
            {
                ProcessZip(Zip);
            }
        }

        private void ProcessZip(string Zip)
        {
            ProcessEventHandler("Zip files to be copied " + Zip);

            string ZipName = Path.GetFileNameWithoutExtension(Zip);
            string[] ZipParts = ZipName.Split('_');
            if (ZipParts.Length == 3)
            {
                string JID = ZipParts[0];
                string AID = ZipParts[2].Replace(JID,"");

                string connString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;

                DatabaseLayer.OPSDB OPSDBObj = new DatabaseLayer.OPSDB(connString);
                OPSDetail OPS = OPSDBObj.GetOPSDetails(JID);
                string Clnt = OPS.Client;

                ProcessEventHandler("Client" + Clnt);
                ProcessEventHandler("JID"    + JID);
                ProcessEventHandler("AID"    + AID);

                EVDatasetProcess EVObj = new EVDatasetProcess(Clnt, JID, AID, ConfigDetails.TDXPSGangtokFMSFolder);

                EVObj.ProcessNotification += ProcessEventHandler;
                EVObj.ErrorNotification   += ProcessErrorHandler;

                EVObj.StartProcess(Zip);
            }
        }
    }
}
