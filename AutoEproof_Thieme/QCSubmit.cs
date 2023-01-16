using System;
using System.Data.SqlClient;
using System.Configuration;
using ProcessNotification;
using MsgRcvr;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoEproof
{
      class QCSubmit : MessageEventArgs
    {

        public void StartProcess(string QCOutFolder)
        {
            string[] MNTFolders = Directory.GetDirectories(QCOutFolder);

            foreach (string MNTFolder in MNTFolders)
            {
                ProcessEventHandler(MNTFolder + " to be QCOut MNTFolder");
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

            string CopyToMNTFolder = ConfigDetails.FinalQC + "\\" + MNTObj.MNTFolder;
            if (Directory.Exists(CopyToMNTFolder))
            {
                Directory.Delete(CopyToMNTFolder, true);
            }
            string CleanProofPdf = @"\\" + ConfigDetails.TDXPSGangtokIP + @"\temp\files\MNT_CLIENT_JOURNAL_JID_AID_110\clean_proof.pdf".Replace("CLIENT", MNTObj.Client).Replace("JID", MNTObj.JID).Replace("AID", MNTObj.AID);
            if (File.Exists(CleanProofPdf))
            {
                File.Delete(CleanProofPdf);
            }
            string AIDPdf = @"\\" + ConfigDetails.TDXPSGangtokIP + @"\temp\files\MNT_CLIENT_JOURNAL_JID_AID_110\AID.pdf".Replace("CLIENT", MNTObj.Client).Replace("JID", MNTObj.JID).Replace("AID", MNTObj.AID);
            if (File.Exists(AIDPdf))
            {
                File.Delete(AIDPdf);
            }

            if (!string.IsNullOrEmpty(MNTObj.JID) && !string.IsNullOrEmpty(MNTObj.AID))
            {
                string _3dPath  = MNTFolder + "\\" + MNTObj.AID + ".3d";
                string _PdfPath = MNTFolder + "\\" + MNTObj.AID + "_OUT.xml";

                string _PgCountLog = MNTFolder + "\\PgCount.log";

                if (File.Exists(_3dPath) && File.Exists(_PdfPath) && File.Exists(_PgCountLog))
                {
                    ProcessEventHandler("CopyToTemp Process start.");
                    if (CopyToTemp(MNTFolder, MNTObj, "*.3d"))
                    {
                        if (CopyToTemp(MNTFolder, MNTObj, "*.xml"))
                        {
                            if (CopyToTemp(MNTFolder, MNTObj, "PgCount.xml"))
                                PrcsMoveToQC(MNTFolder, MNTObj);
                        }

                    }
                    ProcessEventHandler("CopyToTemp Process finished.");
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
                if (Program.SendMsgToGang(MNTObj.Client, MNTObj.JID, MNTObj.AID, "QCSUBMIT"))
                {
                    ProcessEventHandler("Message send successfully for QCSUBMIT.");

                    Directory.Delete(MNTFolder, true);
                    InsertMessageCleanProof(MNTObj);
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

                    WritePagXML(TempMNTFolder);

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
        private bool WritePagXML(string TempMNTFolder)
        {
            string PagXML = TempMNTFolder + "\\pagination.xml";
            bool Rslt = false;

            if (File.Exists(PagXML))
            {
                string str = File.ReadAllText(PagXML);

                try
                {
                    if (str.Contains("S280"))
                    {
                        File.WriteAllText(PagXML, str.Replace("S280","S275"));
                    }
                    Rslt = true;
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                }
            }
            return Rslt;
        }
        private bool MoveToQC(MNTInfo MNTObj)
        {
            bool Rslt = false;
            string MNT = Path.GetFileNameWithoutExtension(MNTObj.MNTFolder);
            string GangDBConnectionString = ConfigurationManager.ConnectionStrings["GangDBConnectionString"].ConnectionString;
            string SqlQry = "update ARTICLEDETAILS set PSE_STAGEID='ST1037' where ITEMCODE='" + MNT + "'";


            try
            {

                SqlHelper.ExecuteNonQuery(GangDBConnectionString, System.Data.CommandType.Text, SqlQry);
                Rslt = true;
                ProcessEventHandler("article move to PS_CREATION::" + MNTObj.MNTFolder);
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

        private bool InsertMessageCleanProof(MNTInfo MNTObj)
        {
            bool Rslt = false;
            string _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString; 
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@JIDAID", MNTObj.JID+MNTObj.AID);
                SqlHelper.ExecuteNonQuery(_OPSConStr, System.Data.CommandType.StoredProcedure, "[usp_InsertToFINALQC]",param);
                Rslt = true;
                ProcessEventHandler("Insert message for cleanproof " + MNTObj.MNTFolder);

            }
            catch (SqlException ex)
            {
                ProcessErrorHandler(ex);
                ProcessEventHandler("Article failes to cleanproof message::" + MNTObj.MNTFolder);
            }
            return Rslt;

        }
    }
}
