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
    class CleanProof : MessageEventArgs
    {
        bool _IsSuccess = false;
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

        public CleanProof()
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

        public void ProcessArticles()
        {
            try
            {
                foreach (MessageDetail MsgDtl in _MsgList)
                {

                    MNTInfo MNT = new MNTInfo(MsgDtl.Client, MsgDtl.JID, MsgDtl.AID, MsgDtl.Stage);
                    MNT.Status = MsgDtl.Status;
                    if (MNT.Status.Contains("FinalQC"))
                    {
                        if (CopyCleanproof(MNT))
                        {
                            _OPSDBObj.UpdateRvsIssMessageDetal(MsgDtl.MsgID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }
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

        private void InProcessMessageDetail()
        {

            _MsgList = _OPSDBObj.GetInprocessMessageDetail();


        }

    }


}

