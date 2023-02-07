using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Fiche" in code, svc and config file together.
namespace LexisNexis
{

    public class Fiche : IFiche
    {
        public FilterValueSet[] GetDistinctValue(string strColumnName)
        {
            SqlConnection connection = null;

            try
            {

                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());

                string strSQL = string.Empty;
                switch (strColumnName.ToLower()) // This is the column name in the GridView defined
                {
                    case "fid":
                        strSQL = @"SELECT distinct FID, FID FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "indate":
                        strSQL = @"SELECT distinct INDATE, CONVERT(VARCHAR(11), INDATE, 106) AS [INDATE] FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "duedate":
                        strSQL = @"SELECT distinct duedate, CONVERT(VARCHAR(11), duedate, 106) AS [duedate] FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "delivered_date":
                        strSQL = @"SELECT distinct DELIVERED_DATE, CONVERT(VARCHAR(11), DELIVERED_DATE, 106) AS [DELIVERED_DATE] FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "ftitle":
                        strSQL = @"SELECT distinct ftitle, ftitle FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "folio":
                        strSQL = @"SELECT distinct folio, folio FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "damandtype":
                        strSQL = @"SELECT distinct Damandtype, Damandtype FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "duration":
                        strSQL = @"SELECT distinct duration, duration FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "iteration":
                        strSQL = @"SELECT distinct iteration, iteration FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "pagecount":
                        strSQL = @"SELECT distinct pagecount, pagecount FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "stage":
                        strSQL = @"SELECT distinct Stage, Stage FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "tat":
                        strSQL = @"SELECT distinct tat, tat FROM fiches where Active<>'No' Order By 1";
                        break;
                    case "fullname":
                        strSQL = @"SELECT distinct(l.firstname+' '+l.lastname) as fullname, (l.firstname+' '+l.lastname) as fullname  FROM fiches n , login l where n.userid=l.userid and  n.Active<>'No' Order By 1";
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

                /*
                while (dataReader.Read())
                {
                    filterValueList.Add(new FilterValueSet
                    {
                        Id = dataReader[0].ToString(),
                        Value = dataReader[1].ToString()
                    });
                }
                */
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
