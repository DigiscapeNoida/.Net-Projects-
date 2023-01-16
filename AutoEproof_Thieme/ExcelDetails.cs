using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Text;
using ProcessNotification;
namespace AutoEproof
{
    class ExcelDetails
    {
        #region Private Variable
        private StringCollection _ExcelSheets   = new StringCollection();
        private string           _ExcelFilePath = string.Empty;
        private string           _ConStr        = string.Empty;

        #endregion

        #region Public Property

        public ExcelDetails(string ExcelFilePath)
        {
            _ExcelFilePath  = ExcelFilePath;
            _ConStr         = GetConnectionString();
        }
        #endregion

        #region Public Method

        public string GetDataAsXML( )
        {
            StringBuilder XML = new StringBuilder("");
            try
            {
                DataTable data = new DataTable();
                GetExcelSheetName();
                using (OleDbConnection connection = new OleDbConnection(_ConStr))
                {
                    foreach (string ExcelSheet in _ExcelSheets)
                    {
                        try
                        {
                            using (OleDbDataAdapter adapter = new OleDbDataAdapter(GetSelectString(ExcelSheet ), connection))
                            {
                                adapter.Fill(data);
                                data.TableName = "EmailDetail";
                            }

                            StringWriter strm = new StringWriter();
                            data.WriteXml(strm);
                            XML.Append(strm.ToString()); ////////////Do'nt change this.
                            break;
                        }
                        catch (Exception ex)
                        {
                            //AppLog.LogInfo(ex);
                        }
                    }
                }
                XML.Replace("</DocumentElement><DocumentElement>", string.Empty);
            }
            catch (Exception ex)
            {
                //AppLog.LogInfo(ex);
            }

            return XML.ToString();
        }
        public string GetDataAsXML(string JIDAID)
        {
            StringBuilder XML = new StringBuilder("");
            try
            {
                DataTable data = new DataTable();
                GetExcelSheetName();
                using (OleDbConnection connection = new OleDbConnection(_ConStr))
                {
                    foreach (string ExcelSheet in _ExcelSheets)
                    {
                        try
                        {
                            using (OleDbDataAdapter adapter = new OleDbDataAdapter(GetSelectString(ExcelSheet,JIDAID), connection))
                            {
                                adapter.Fill(data);
                                data.TableName = "EmailDetail";

                                /*
                                DataRow [] DR =data.Select("JIDAID = '" + JIDAID + "'");

                                if (DR.Length > 0)
                                {
                                    foreach (object obj in DR[0].ItemArray)
                                    {
                                        XML.Append(obj.ToString() + '\t');
                                    }
                                }
                                */
                             }

                            //XML.ToString();
                            //break;
                            StringWriter strm = new StringWriter();
                            data.WriteXml(strm);
                            XML.Append(strm.ToString()); ////////////Do'nt change this.
                            break;
                        }
                        catch (Exception ex)
                        {
                            //AppLog.LogInfo(ex);
                        }
                    }
                }
                XML.Replace("</DocumentElement><DocumentElement>", string.Empty);
            }
            catch (Exception ex)
            {
                //AppLog.LogInfo(ex);
            }

            return XML.ToString();
        }
        #endregion

        #region Private Method
        private string GetConnectionString()
        {
            // Note: the Types array exactly matches the entries in openFileDialog1.Filter
            string[] Types = {
                    "Excel 12.0 Xml",   // For Excel 2007 XML (*.xlsx)
                    "Excel 12.0",       // For Excel 2007 Binary (*.xlsb)
                    "Excel 12.0 Macro", // For Excel 2007 Macro-enabled (*.xlsm)
                    "Excel 8.0",        // For Excel 97/2000/2003 (*.xls)
                    "Excel 5.0" };      // For Excel 5.0/95 (*.xls)

            // Note: openFileDialog1.FilterIndex was saved into textBoxFilename.Tag
            //string Type = Types[(int)textBoxFilename.Tag - 1];

            string Type = Types[1];

            // True if the first row in the Excel data is a header (used for column names, not data)
            bool Header = true;

            // True if columns containing different data types are treated as text
            //  (note that columns containing only integer types are still treated as integer, etc)
            bool TreatIntermixedAsText = true;

            // Build the actual connection string
            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();

            // Name of the Excel worksheet to open
            builder.DataSource = _ExcelFilePath;

            if (Type == "Excel 5.0" || Type == "Excel 8.0")
                builder.Provider = "Microsoft.Jet.OLEDB.4.0";
            else
                builder.Provider = "Microsoft.ACE.OLEDB.12.0";

            builder["Extended Properties"] = Type +
            ";hdr=" + (Header ? "Yes" : "No") +
            ";IMEX=" + (TreatIntermixedAsText ? "1" : "0") + ";";


            //builder["Extended Properties"] = Type;
            //builder.ConnectionString += ";HDR=" + (Header ? "Yes" : "No") + ";IMEX=" + (TreatIntermixedAsText ? "1" : "0");

            //  The "ACE" provider requires either Office 2007 or the following redistributable:
            //  Office 2007 Data Connectivity Components:
            //  http://www.microsoft.com/downloads/details.aspx?familyid=7554F536-8C28-4598-9B72-EF94E038C891&displaylang=en

            // The "ACE" provider can be used for older types (e.g., Excel 8.0) as well.
            //  The connection strings used for Excel files are not clearly documented; see the following links for more information:
            //  Excel 2007 on ConnectionStrings.com:
            //    http://www.connectionstrings.com/excel-2007
            //  Excel on ConnectionStrings.com:
            //    http://www.connectionstrings.com/excel
            //  Microsoft OLE DB Provider for Microsoft Jet on MSDN:
            //    http://msdn.microsoft.com/en-us/library/ms810660.aspx
            //  KB247412 Methods for transferring data to Excel from Visual Basic:
            //    http://support.microsoft.com/kb/247412
            //  KB278973 ExcelADO demonstrates how to use ADO to read and write data in Excel workbooks:
            //    http://support.microsoft.com/kb/278973
            //  KB306023 How to transfer data to an Excel workbook by using Visual C# 2005 or Visual C# .NET:
            //    http://support.microsoft.com/kb/306023
            //  KB306572 How to query and display excel data by using ASP.NET, ADO.NET, and Visual C# .NET:
            //    http://support.microsoft.com/kb/306572
            //  KB316934 How to use ADO.NET to retrieve and modify records in an Excel workbook with Visual Basic .NET:
            //    http://support.microsoft.com/kb/316934

            return builder.ConnectionString;
            //return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\P.J.0iui\\Downloads\\Google_Hotel_Finder_List.xlsx;Extended Properties=\"Excel 12.0;hdr=yes;IMEX=1;\"";
        }
        private void GetExcelSheetName()
        {
            string TableName = string.Empty;
            try
            {
                using (OleDbConnection connection = new OleDbConnection(_ConStr))
                {
                    connection.Open();
                    using (DataTable tables = connection.GetSchema("Tables"))
                    {
                        foreach (DataRow row in tables.Rows)
                        {
                            TableName = (string)row["TABLE_NAME"];
                            if (TableName.EndsWith("$") || TableName.EndsWith("$'"))
                            {
                                _ExcelSheets.Add(TableName);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // Display any errors

            }
        }

        /// <summary>
        /// Returns the connection string needed for connecting to the specified file and type
        /// </summary>
        /// 
        /// UswChain	UswPid
        /// 

        private string GetSelectString(string ExcelSheet)
        {
            string SelectStr = "SELECT JIDAID,Name,MailID,Title FROM  [" + ExcelSheet + "]";
            return SelectStr;
        }
        private string GetSelectString(string ExcelSheet, string JIDAID)
        {
            string SelectStr = "SELECT JIDAID,Name,MailID,Title FROM [" + ExcelSheet + "] where JIDAID = '" + JIDAID + "'";
            return SelectStr;
        }
        #endregion

    }
}
