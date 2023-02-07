using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Encyclo" in code, svc and config file together.
namespace LexisNexis
{

    public class Encyclo : IEncyclo
    {
        public FilterValueSet[] GetDistinctValue(string strColumnName)
        {
            SqlConnection connection = null;

            try
            {

                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());

                string strSQL = string.Empty;
                switch (strColumnName) // This is the column name in the GridView defined
                {
                    case "EID":
                        strSQL = @"SELECT distinct EID, EID FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "DUEDATE":
                        strSQL = @"select a.DUEDATE,CONVERT(VARCHAR(11), a.DUEDATE, 106) AS [DUEDATE]  from encyclopedia a inner join (select eid,max(DUEDATE) as DUEDATE from encyclopedia group by eid )b on a.eid = b.eid and a.DUEDATE = b.DUEDATE where Active<>'No' Order By 1";
                      //  strSQL = @"SELECT distinct DUEDATE, CONVERT(VARCHAR(11), DUEDATE, 106) AS [DUEDATE] FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "DTITLE":
                        strSQL = @"SELECT distinct DTITLE, DTITLE FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "FOLIO":
                        strSQL = @"SELECT distinct FOLIO, FOLIO FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "DEMANDTYPE":
                        strSQL = @"SELECT distinct DEMANDTYPE, DEMANDTYPE FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "ITERATION":
                        strSQL = @"SELECT distinct ITERATION, ITERATION FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "PAGECOUNT":
                        strSQL = @"SELECT distinct PAGECOUNT, PAGECOUNT FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "STAGE":
                        strSQL = @"SELECT distinct STAGE, STAGE FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "tat":
                        strSQL = @"SELECT distinct tat, tat FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "userid":
                        strSQL = @"SELECT distinct userid, userid FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "fullname":
                        strSQL = @"SELECT distinct(l.firstname+' '+l.lastname) as fullname, (l.firstname+' '+l.lastname) as fullname  FROM encyclopedia n , login l where n.userid=l.userid and  n.Active<>'No' Order By 1";
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
}
