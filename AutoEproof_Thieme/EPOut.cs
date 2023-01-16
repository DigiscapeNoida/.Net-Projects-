using System;
using System.Configuration;
using ProcessNotification;
using MsgRcvr;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoEproof
{
    class EPOut : MessageEventArgs
    {

        public void StartProcess(string EPOutFolder)
        {
            string[] MNTFolders = Directory.GetDirectories(EPOutFolder);

            foreach (string MNTFolder in MNTFolders)
            {
                ProcessEventHandler(MNTFolder + " to be EPout MNTFolder");
                try
                {
                    ProcessMNT(MNTFolder);
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                }

            }
        }

        private void ProcessMNT(string MNTFolder)
        {
            MNTInfo MNTObj = new MNTInfo(MNTFolder);
            

            if (!string.IsNullOrEmpty(MNTObj.JID) && !string.IsNullOrEmpty(MNTObj.JID))
            {
                string _3dPath  = MNTFolder + "\\" + MNTObj.AID + ".3d";
                string _PdfPath = MNTFolder + "\\" + MNTObj.AID + ".pdf";
                string clean_proof = MNTFolder + "\\" + MNTObj.JID +"_"+ MNTObj.AID + "_clean_proof.pdf";

                if (File.Exists(_3dPath) && File.Exists(_PdfPath))
                {
                    ProcessEventHandler("CopyToTemp Process start.");
                    if (CopyToTemp(MNTFolder, MNTObj, "*.3d"))
                    {
                        if (CopyToTemp(MNTFolder, MNTObj,  "*.pdf"))
                        {
                            PrcsMoveToQC(MNTFolder, MNTObj);
                        }
                    }
                    ProcessEventHandler("CopyToTemp Process finished.");
                }
                else if (File.Exists(clean_proof))
                {
                    PrcsMoveToQC(MNTFolder, MNTObj);
                }
                else
                {
                    ProcessEventHandler("clean_proof or 3d or pdf file does not exist.");
                }
            }
            else
            {
                ProcessEventHandler("3d or pdf file does not exist.");
            }
        }


        private bool CopyFileToTemp(string MNTFolder, MNTInfo MNTObj, string CopyFrom)
        {
            bool Rslt = false;
            string TempMNTFolder = ConfigDetails.TDXPSGangtokTempFolder + "\\" + Path.GetFileName(MNTObj.MNTFolder);

            ProcessEventHandler("TempMNTFolder ::" + TempMNTFolder);
            if (Directory.Exists(TempMNTFolder))
            {

                string CopyTo = TempMNTFolder + "\\" + Path.GetFileName(CopyFrom);
                ProcessEventHandler("File is being copied.");
                ProcessEventHandler("CopyFrom ::" + CopyFrom);
                ProcessEventHandler("CopyTo ::" + CopyTo);
                File.Copy(CopyFrom, CopyTo, true);
                ProcessEventHandler("File has been copied.");

                Rslt= true;
            }
            else
            {
                ProcessEventHandler("TempMNTFolder does not exist::" + TempMNTFolder);
                Directory.CreateDirectory(TempMNTFolder);
                ProcessEventHandler("Created TempMNTFolder::" + TempMNTFolder);
            }
            return Rslt;
        }
        private bool PrcsMoveToQC(string MNTFolder, MNTInfo MNTObj)
        {
            bool Rslt = false;
            if (MoveToQC(MNTObj))
            {
                ProcessEventHandler("Try to message send for author revision.");
                if (Program.SendMsgToGang(MNTObj.Client, MNTObj.JID, MNTObj.AID, "FINALSUBMITQC"))
                {
                    ProcessEventHandler("Message send successfully for author revision .");

                    Directory.Delete(MNTFolder, true);

                    ProcessEventHandler("MNTFolder deleted ::" + MNTFolder);
                    Rslt = true;
                }
                else
                {
                    ProcessEventHandler("Message could not be send.");
                }
            }

            return Rslt;
        }




        private bool CopyToTemp(string MNTFolder, MNTInfo MNTObj, string SrchOptn )
        {
            bool Rslt = false;
            string[] Files = Directory.GetFiles(MNTFolder, SrchOptn);


            string TempMNTFolder = ConfigDetails.TDXPSGangtokTempFolder + "\\" + Path.GetFileName(MNTObj.MNTFolder);
            ProcessEventHandler("TempMNTFolder ::" + TempMNTFolder);
            if (Directory.Exists(TempMNTFolder))
            {
                int Count = 0;
                foreach (string Fl in Files)
                {
                    string CopyFrom = Fl;
                    string CopyTo = TempMNTFolder + "\\" + Path.GetFileName(Fl);


                    ProcessEventHandler("File is being copied.");
                    ProcessEventHandler("CopyFrom ::" + CopyFrom);
                    ProcessEventHandler("CopyTo ::" + CopyTo);


                    File.Copy(CopyFrom, CopyTo, true);

                    ProcessEventHandler("File has been copied.");
                    Count++;
                }

                if (Files.Length == Count)
                {
                           Rslt = true;
                    //if (MoveToQC(MNTObj))
                    //{
                    //    ProcessEventHandler("Try to message send for author revision.");
                    //    if (Program.SendMsgToGang(MNTObj.Client, MNTObj.JID, MNTObj.AID))
                    //    {
                    //        Directory.Delete(MNTFolder, true);

                    //        ProcessEventHandler("MNTFolder deleted ::" + MNTFolder);
                    //        Rslt = true;
                    //    }
                    //    else
                    //    {
                    //        ProcessEventHandler("Message could not be send.");
                    //    }
                    //}
                }
            }
            else
            {
                ProcessEventHandler("TempMNTFolder does not exist::" + TempMNTFolder);
                //Directory.CreateDirectory(TempMNTFolder);
                ProcessEventHandler("Created TempMNTFolder::" + TempMNTFolder);
            }

            return Rslt;
        }
        
        private bool MoveToQC(MNTInfo MNTObj)
        {
            bool Rslt = false;
            string MNT = Path.GetFileNameWithoutExtension(MNTObj.MNTFolder);
            string GangDBConnectionString = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            string SqlQry = "update ARTICLEDETAILS set PSE_STAGEID='ST1053' where ITEMCODE='" + MNT + "'";


            try
            {

                SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.Text, SqlQry);
                Rslt = true;
                ProcessEventHandler("article move to QC::" + MNTObj.MNTFolder);
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
                ProcessEventHandler("Article failes to move QC::" + MNTObj.MNTFolder);
            }

            if (Rslt == false)
            {
                try
                {
                    GangDBConnectionString = ConfigurationManager.ConnectionStrings["AltGangDBConnectionString"].ConnectionString;
                    SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.Text, SqlQry);
                    Rslt = true;
                    ProcessEventHandler("article move to QC::" + MNTObj.MNTFolder);
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                    ProcessEventHandler("Article failes to move QC::" + MNTObj.MNTFolder);
                }
            }
            return Rslt;
        }


        private bool MoveToAuthorRevision(MNTInfo MNTObj)
        {
            bool Rslt = false;
            string MNT = Path.GetFileNameWithoutExtension(MNTObj.MNTFolder);
            string GangDBConnectionString = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            string SqlQry = "update ARTICLEDETAILS set PSE_STAGEID='ST1006' where ITEMCODE='" + MNT + "'";


            try
            {

                SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.Text, SqlQry);
                Rslt = true;
                ProcessEventHandler("article move to AuthorRevision::" + MNTObj.MNTFolder);
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
                ProcessEventHandler("Article failes to move AuthorRevision::" + MNTObj.MNTFolder);
            }

            if (Rslt == false)
            {
                try
                {
                    GangDBConnectionString = ConfigurationManager.ConnectionStrings["AltGangDBConnectionString"].ConnectionString;
                    SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.Text, SqlQry);
                    Rslt = true;
                    ProcessEventHandler("article move to AuthorRevision::" + MNTObj.MNTFolder);
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                    ProcessEventHandler("Article failes to move AuthorRevision::" + MNTObj.MNTFolder);
                }
            }
            return Rslt;
        }
    }
}
