using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FMSIntegrateUsingEOOLink
{
    public delegate void NotifyMsg(string NotificationMsg);
    public delegate void NotifyErrMsg(Exception Ex);

    public class eFirstThieme
    {
        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;

        public void StartProcess()
        {
            ProcessMessage("Getting InProgres rows from ThiemeOffPrint table");
            DataSet OffPrintOB = ThiemeDataProcess.GetThiemeOffPrint();
            if (OffPrintOB.Tables[0].Rows.Count > 0)
            {
                ProcessMessage("InProgres rows ::" + OffPrintOB.Tables[0].Rows.Count.ToString());

                for (int i = 0; i < OffPrintOB.Tables[0].Rows.Count; i++)
                {
                    ThiemeOffPrint objThiemeOffPrint = new ThiemeOffPrint();
                    if (OffPrintOB.Tables[0].Rows[i]["AID"] != DBNull.Value)
                        objThiemeOffPrint.AID = OffPrintOB.Tables[0].Rows[i]["AID"].ToString();
                    if (OffPrintOB.Tables[0].Rows[i]["DOI"] != DBNull.Value)
                        objThiemeOffPrint.DOI = OffPrintOB.Tables[0].Rows[i]["DOI"].ToString();
                    if (OffPrintOB.Tables[0].Rows[i]["JID"] != DBNull.Value)
                        objThiemeOffPrint.JID = OffPrintOB.Tables[0].Rows[i]["JID"].ToString();
                    if (OffPrintOB.Tables[0].Rows[i]["MailSubjectLine"] != DBNull.Value)
                        objThiemeOffPrint.MailSubjectLine = OffPrintOB.Tables[0].Rows[i]["MailSubjectLine"].ToString();
                    if (OffPrintOB.Tables[0].Rows[i]["SNO"] != DBNull.Value)
                        objThiemeOffPrint.SNO = Convert.ToInt32(OffPrintOB.Tables[0].Rows[i]["SNO"]);
                    if (OffPrintOB.Tables[0].Rows[i]["STAGE"] != DBNull.Value)
                        objThiemeOffPrint.STAGE = OffPrintOB.Tables[0].Rows[i]["STAGE"].ToString();
                    if (OffPrintOB.Tables[0].Rows[i]["STATUS"] != DBNull.Value)
                        objThiemeOffPrint.STATUS = OffPrintOB.Tables[0].Rows[i]["STATUS"].ToString();
                    if (OffPrintOB.Tables[0].Rows[i]["Volume"] != DBNull.Value)
                        objThiemeOffPrint.Volume = OffPrintOB.Tables[0].Rows[i]["Volume"].ToString();
                    if (OffPrintOB.Tables[0].Rows[i]["Year"] != DBNull.Value)
                        objThiemeOffPrint.Year = OffPrintOB.Tables[0].Rows[i]["Year"].ToString();
                    ProcessMessage("Creating eFirstProcess Object..");
                    eFirstProcess_2 eFirstProcess_2Obj = new eFirstProcess_2(objThiemeOffPrint);
                    eFirstProcess_2Obj.ProcessNotification += ProcessMessage;
                    eFirstProcess_2Obj.ErrorNotification += ErrorMessage;
                    ProcessMessage("Start to process ");
                    eFirstProcess_2Obj.StartProcess();

                }
            }
            //-------------Comment as suggest by suresh sir
            string NotFoudFile = "c:\\NotFoudFile.log";
            if (File.Exists(NotFoudFile))
            {
                string[] NotFoudDOI = File.ReadAllLines(NotFoudFile);
                if (NotFoudFile.Length > 0)
                {
                    MailDetail MailDetailObj = new MailDetail();
                    MailDetailObj.MailFrom = "eproof@thomsondigital.com";
                    MailDetailObj.MailTo = "thieme.j@thomsondigital.com";
                    MailDetailObj.MailCC = "";
                    MailDetailObj.MailBCC = "";//"uditpandit21@gmail.com";
                    MailDetailObj.MailBody = GetNotFoundMailBody(NotFoudDOI);

                    MailDetailObj.MailSubject = "Pdf files missing in WIP to be used in eFirst application.";

                    eMailProcess eMailProcessObj = new eMailProcess();
                    eMailProcessObj.SendMailInternal(MailDetailObj);
                }
            }
        }
        private string GetNotFoundMailBody(string [] Lines)
        {
            StringBuilder MailBody = new StringBuilder();
            MailBody.AppendLine("Dear Thomson Digital,");
            MailBody.AppendLine();
            MailBody.AppendLine("Below DOI is not found to send eFirst or FIZ, Please make sure to place pdf files to be used in eFirst application.");
            MailBody.AppendLine();
            MailBody.AppendLine();

            foreach (string Line in Lines)
            {
                MailBody.AppendLine(Line);
            }

            MailBody.AppendLine();

            MailBody.AppendLine();
            MailBody.AppendLine("Thanks,");
            MailBody.AppendLine("Autogenerated mail by Auto E-First System.");

            return MailBody.ToString();
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

                string NotFoudFile = "c:\\NotFoudFile.log";
                File.AppendAllText(NotFoudFile, Ex.Message);
            }
        }
    }
}
