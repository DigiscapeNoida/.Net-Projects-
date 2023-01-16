using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.IO;
using ProcessNotification;
namespace LWWAutoIntegrate

{
    struct OpenIds
    {
        public string jid;
        public string aid;
        public string status;
    }
    public class OpenAccess : MessageEventArgs
    {
        int _PrvusDays=1;
        public OpenAccess(int PrvusDays)
        {
            _PrvusDays = PrvusDays;
        }

        public OpenAccess()
        {

        }
        public bool Process()
        {
            try
            {

               
               string CsvFilesDir = AppDomain.CurrentDomain.BaseDirectory + "CsvFiles";
               string CSVFile = CsvFilesDir + "\\" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".csv";

               StringBuilder CsvStr = new StringBuilder();

               if (!(Directory.Exists(CsvFilesDir)))
               {
                   Directory.CreateDirectory(CsvFilesDir);
               }

               ProcessEventHandler("Start to get open access article list.");
               List<OpenIds> lst =   GetData("[Usp_OpenAccessReport]");

               if (lst.Count > 0)
               {
                   CsvStr.AppendLine("JID,AID,OPEN ACCESS");
                   foreach (OpenIds item in lst)
                   {
                       CsvStr.AppendLine(item.jid + "," + item.aid + ",Yes");
                   }

                   if (!string.IsNullOrEmpty(CSVFile))
                   {
                       if (File.Exists(CSVFile))
                       {
                           File.Delete(CSVFile);
                       }
                       File.AppendAllText(CSVFile, CsvStr.ToString());
                   }
                   ProcessEventHandler("Start to send mail"); 
                   sendmail(CSVFile);
                   ProcessEventHandler("Next to send mail"); 
               }
               else
               {
                   ProcessEventHandler("No open access articles found in database in last two days" );
               }
               return true;
            }
            catch (Exception )
            {
                return false;   
            }
        }

       
        private List<OpenIds> GetData(string spName )
        {   
            
            List<OpenIds> lst = new List<OpenIds>();
            string _WIPConnection = ConfigurationManager.ConnectionStrings["WIPConnection"].ConnectionString;
            try
            {
                SqlParameter pInt = new SqlParameter
                {
                    ParameterName = "@Days",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value =_PrvusDays,                    
                    Direction = ParameterDirection.Input,
                };

                ProcessEventHandler("Execute procedure " + spName + "  to get open access article list.");
                DataSet DS = SqlHelper.ExecuteDataset(_WIPConnection, CommandType.StoredProcedure, spName, pInt);

                ProcessEventHandler("Filling list from DS");
                foreach (DataRow Dr in DS.Tables[0].Rows)
                {
                        if (Dr["jid"] != DBNull.Value && Dr["aid"] != DBNull.Value)
                        {
                            OpenIds oOpenIds = new OpenIds();
                            oOpenIds.jid = Dr["jid"].ToString();
                            oOpenIds.aid = Dr["aid"].ToString();
                            lst.Add(oOpenIds);
                        } 
                }
                return lst;
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
                return null;
            }
            finally
            { 
            
            }
        }


        private void sendmail(string CSVFile)
        {

            MailDetail MailDtlObj = new MailDetail();
            MailDtlObj.MailFrom = "eproof@thomsondigital.com";

            string AnnPDF = ConfigurationManager.AppSettings["AnnPDF"];

            MailDtlObj.MailTo = ConfigurationManager.AppSettings["MailTo"];
            MailDtlObj.MailCC = ConfigurationManager.AppSettings["MailCC"];
            MailDtlObj.MailBCC = ConfigurationManager.AppSettings["MailBCC"];
            MailDtlObj.MailAtchmnt.Add(CSVFile);

            MailDtlObj.MailSubject = "Open access articles  list";

            MailDtlObj.MailBody = "Dear All \n\n\nFind the attached open access article list. \n\nWith regards,\nAuto eProof System\n\n\n";

            eMailProcess eMailProcessObj = new eMailProcess();
            eMailProcessObj.ProcessNotification += ProcessEventHandler;
            eMailProcessObj.ErrorNotification += ProcessErrorHandler;


            ProcessEventHandler("Going to send error mail.");
            eMailProcessObj.SendMailInternal(MailDtlObj);
            ProcessEventHandler("Trying to pdf to err file.");

        }
    }
}
