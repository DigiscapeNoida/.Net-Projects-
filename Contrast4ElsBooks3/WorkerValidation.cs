using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Data;
using System.Diagnostics;

namespace Contrast4ElsBooks
{
    class WorkerValidation  :Work
    {
        public bool mTask()
        {
            string error = "";
            try
            {                  
               DataSet DSet  = DataAccess.ExecuteDataSetSP(SPNames.getVtooldata);     
               if (DSet != null)
               {
                   foreach (DataRow row in DSet.Tables[0].Rows)
                   {
                       string tombk =  (row["TombkNo"]!= DBNull.Value)?row["TombkNo"].ToString():"";
                       string PII = (row["PII"] != DBNull.Value) ? row["PII"].ToString() : "";
                       if (tombk != "")
                       {
                           string path = GlobalConfig.oGlobalConfig.TARGET.ToString();
                           System.Threading.Thread.Sleep(500);
                           string TOMBKFINAL = path + "\\" + tombk;
                           if (Directory.Exists(TOMBKFINAL))
                           {
                               try
                               {
                                   string[] thumbnil = Directory.GetFiles(TOMBKFINAL, "*.db", SearchOption.AllDirectories);
                                   foreach (string thumb in thumbnil)
                                   {
                                       File.Delete(thumb);
                                   }
                               }
                               catch (Exception)
                               {
                                                                     
                               }                                         
                               System.Threading.Thread.Sleep(400);

                               //Running Strippins

                               try
                               {
                                   string TempPII = PII.Replace("-", "").Replace(".","");
                                   Process ProObj = new Process();
                                   //ProObj.StartInfo.FileName = @"V:\StripCheck.bat";
                                   ProObj.StartInfo.FileName = System.Configuration.ConfigurationSettings.AppSettings["StrippinsBatch"];
                                   ProObj.StartInfo.Arguments = TOMBKFINAL + "\\" + TempPII.Substring(1, 13) + "\\" + TempPII;
                                   ProObj.Start();
                                   ProObj.WaitForExit();

                               }
                               catch (Exception ex)
                               { }

                               //Running Vtool

                               VToolProcess oVToolProcess = new VToolProcess();
                               string attachment = "";                               
                               error = "";
                               bool VtoolValid = oVToolProcess.RunVTool(TOMBKFINAL, out error,out attachment);
                               if (VtoolValid)
                               {
                                   UpdateVtoolStatus(tombk, "DONE");
                               }
                               else
                               {
                                   GlobalConfig.oGlobalConfig.Send_Error(error,tombk,PII,"Failed",attachment);
                                   UpdateVtoolStatus(tombk, "FAIL");
                               }
                           }
                           else
                           {
                               GlobalConfig.oGlobalConfig.Send_Error("Tombk folder does not exists to run vtool :: " + tombk, tombk, PII,"Failed", "");                           
                           }
                       }
                       else
                       {
                           GlobalConfig.oGlobalConfig.WriteLog(" Null tombk row is found :: " + tombk);                                                 
                       }
                   }   
               }
               Console.WriteLine("**** Waiting to run Vtool.");
               return true;
            }
            catch (Exception exe)
            {
                GlobalConfig.oGlobalConfig.WriteLog("Error :" + exe.Message.ToString());
                 //exe.Message.tostring();
              //  GlobalConfig.oGlobalConfig.Send_Error("Error :" + exe.Message.ToString(), tombk, PII, "Failed", "");                           
                return true;              
            }
        }

        public bool OnError()
        {    
            return true;
        }

        public bool OnSuccess()
        {
            return true;
        }
        private bool UpdateVtoolStatus(string mnt, string Status)
        {
            try
            {
                string QL = "update PCDATASETINFO set Vtool = '" + Status + "' where TombkNo='" + mnt + "'";
                DataAccess.ExecuteNonQuery(QL);
                return true;
            }
            catch (Exception exe)
            {
                return true;                
            }
        }
    }
}
