using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
namespace LexisNexis
{
    public class Service1 : IService1
    {
        public FilterValueSet[] GetDistinctValue(string strColumnName)
        {
            SqlConnection connection = null;

            try
            {

                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());

                string strSQL = string.Empty;
                switch (strColumnName.ToUpper()) // This is the column name in the GridView defined
                {
                    case "DID":
                        strSQL = @"SELECT distinct DID, DID FROM NEWS  where Active<>'No' Order By 1";
                        break;
                    case "AUTHOR":
                        strSQL = @"SELECT distinct AUTHOR, AUTHOR FROM NEWS  where Active<>'No' Order By 1";
                        break;
                    case "DUEDATE":
                        strSQL = @"SELECT distinct DUEDATE, CONVERT(VARCHAR(11), DUEDATE, 106) AS [DUEDATE] FROM NEWS  where Active<>'No' Order By 1";
                        break;
                    case "DECLINATION":
                        strSQL = @"SELECT distinct DECLINATION, DECLINATION FROM NEWS  where Active<>'No' Order By 1";
                        break;
                    case "CTITLE":
                        strSQL = @"SELECT distinct CTITLE, CTITLE FROM NEWS  where Active<>'No' Order By 1";
                        break;
                    case "DEMANDTYPE":
                        strSQL = @"SELECT distinct DEMANDTYPE, DEMANDTYPE FROM NEWS  where Active<>'No' Order By 1";
                        break;
                    case "DURATION":
                        strSQL = @"SELECT distinct DURATION, DURATION FROM NEWS  where Active<>'No' Order By 1";
                        break;
                    case "ITERATION":
                        strSQL = @"SELECT distinct ITERATION, ITERATION FROM NEWS  where Active<>'No' Order By 1";
                        break;
                    case "PAGECOUNT":
                        strSQL = @"SELECT distinct PAGECOUNT, PAGECOUNT FROM NEWS where Active<>'No' Order By 1";
                        break;
                    case "STAGE":
                        strSQL = @"SELECT distinct STAGE, STAGE FROM NEWS where Active<>'No' Order By 1";
                        break;
                    case "USERID":
                        strSQL = @"SELECT distinct userid, userid FROM NEWS where Active<>'No' Order By 1";
                        break;
                    case "FULLNAME":
                        strSQL = @"SELECT distinct(l.firstname+' '+l.lastname) as fullname, (l.firstname+' '+l.lastname) as fullname  FROM news n , login l where n.userid=l.userid and  n.Active<>'No' Order By 1";
                        break;
                    case "INDATE":
                        strSQL = @"SELECT distinct INDATE, CONVERT(VARCHAR(11), INDATE, 106) AS [INDATE] FROM NEWS  where Active<>'No' Order By 1";
                        break;
                    case "DELIVERED_DATE":
                        strSQL = @"SELECT distinct Delivered_Date, CONVERT(VARCHAR(11), Delivered_Date, 106) AS [Delivered_Date] FROM NEWS  where Active<>'No' Order By 1";
                        break;
                }

                SqlCommand command = new SqlCommand();
                command.CommandText = strSQL;
                command.Connection = connection;

                command.Connection.Open();

                SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                
                IList<FilterValueSet> filterValueList = new List<FilterValueSet>();
                int cnt = 0;
                bool sts = false;
                while (dataReader.Read())
                {
                    sts = false;
                    if (cnt == 0)
                    {
                        filterValueList.Add(new FilterValueSet
                        {
                            Id = dataReader[0].ToString(),
                            Value = dataReader[1].ToString()
                        });
                        sts = true;
                    }
                    for (int i = 0; i < filterValueList.Count; i++)
                    {
                        if (filterValueList[i].Value == dataReader[1].ToString())
                        {
                            sts = true;
                            break;
                        }
                    }
                    if (sts == false)
                    {
                        filterValueList.Add(new FilterValueSet
                        {
                            Id = dataReader[0].ToString(),
                            Value = dataReader[1].ToString()
                        });

                    }
                    cnt++;
                }
                connection.Close();

                return filterValueList.ToArray<FilterValueSet>();
            }
            catch (Exception ex)
            {
                if (connection != null) connection.Close();

                // Log
                throw ex;
            }
        }
    }
    public class FilterValueSet
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
}
