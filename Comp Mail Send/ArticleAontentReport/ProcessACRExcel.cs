using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArticleAontentReport
{
    public delegate void NotifyMsg(string NotificationMsg);
    public delegate void NotifyErrMsg(Exception Ex);

    public class ProcessACRExcel
    {
        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;


        string _InputPath = string.Empty;
        public ProcessACRExcel(string InputPath)
        {
            _InputPath = InputPath;
        }

        public void StartProcess()
        {
            ProcessMessage("Start to process Article Content Report..");
            string ProcessedPath = _InputPath + "\\Processed";

            ProcessMessage("Processed Path :: " + ProcessedPath);

            if (!Directory.Exists(ProcessedPath))
            {
                Directory.CreateDirectory(ProcessedPath);
            }
            string[] ExlFiles = Directory.GetFiles(_InputPath, "*.xlsx");
            List<ACRDetail> ArticleACRDetailList = new List<ACRDetail>();
            foreach (string ExlFile in ExlFiles)
            {
                ProcessMessage("Excel file to be Processed :: " + ExlFile);

                ProcessACR ProcessACROBJ = new ProcessACR(ExlFile);
                ProcessACROBJ.ProcessNotification += new NotifyMsg(ProcessACROBJ_ProcessNotification);
                ProcessACROBJ.ErrorNotification += new NotifyErrMsg(ProcessACROBJ_ErrorNotification);
                ProcessACROBJ.StartProcss();
                ArticleACRDetailList.AddRange(ProcessACROBJ.ArticleACRDetailList);
                string ProcessedFile = ProcessedPath + "\\" + Path.GetFileName(ExlFile);
                if (File.Exists(ProcessedFile))
                {
                    File.Delete(ProcessedFile);
                }

                ProcessMessage("Moved file from :");
                ProcessMessage(ExlFile + " to " + ProcessedFile);
                File.Move(ExlFile, ProcessedFile);
            }

            ProcessMessage("Start ArticleACRDetailList to datatable");

            DataTable Dt = ConvertToDataTable(ArticleACRDetailList);
            Dt.TableName = "ACR";

            ProcessMessage("Start datatable to xml");
            string result;
            using (StringWriter sw = new StringWriter())
            {
                Dt.WriteXml(sw);
                result = sw.ToString();
            }
            result = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>" + result;

            ProcessMessage("Start to execute usp_InsertCorAuthrEmailUsingACR procedure..");
            ExecuteSP(result, "usp_InsertCorAuthrEmailUsingACR");
            ProcessMessage("usp_InsertCorAuthrEmailUsingACR procedure exceute successfully..");
        }

        void ProcessACROBJ_ErrorNotification(Exception Ex)
        {
            ErrorMessage(Ex);
        }

        void ProcessACROBJ_ProcessNotification(string NotificationMsg)
        {
            ProcessMessage(NotificationMsg);
        }
        DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        private void ExecuteSP(string strXML, string SPName)
        {
            SqlCommand SqlCom = new SqlCommand();
            SqlConnection SqlCon = new SqlConnection();


            SqlCon.ConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
            SqlCon.Open();

            try
            {

                if (SqlCon.State == ConnectionState.Open)
                {

                    ProcessMessage("Sql connection open successfully....");
                    SqlCom.Connection = SqlCon;
                    SqlCom.CommandText = SPName;
                    SqlCom.CommandType = CommandType.StoredProcedure;
                    SqlCom.Parameters.Clear();
                    SqlCom.Parameters.AddWithValue("@strXML", strXML);
                    SqlCom.CommandTimeout = 220;

                    ProcessMessage("Start to execute ExecuteNonQuery....");
                    SqlCom.ExecuteNonQuery();
                    ProcessMessage("End ExecuteNonQuery....");
                }
            }
            catch (SqlException Ex)
            {
                Console.WriteLine(Ex.Message);
                ErrorMessage(Ex);
                
            }
            finally
            {
                SqlCon.Close();
                SqlCon.Dispose();
            }
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
            }
        }
    }
    

    

}
