using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Journal" in code, svc and config file together.
namespace LexisNexis
{
    public class Journal : IJournal
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
                    case "Articleid":
                        strSQL = @"SELECT distinct Articleid, Articleid FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "DUEDATE":
                        strSQL = @"select a.DUEDATE,CONVERT(VARCHAR(11), a.DUEDATE, 106) AS [DUEDATE]  from Article_Details a inner join (select Articleid,max(DUEDATE) as DUEDATE from Article_Details group by Articleid )b on a.Articleid = b.Articleid and a.DUEDATE = b.DUEDATE where a.Active<>'No' Order By 1";
                                 
                        //  strSQL = @"SELECT distinct DUEDATE, CONVERT(VARCHAR(11), DUEDATE, 106) AS [DUEDATE] FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "IN_DATE":
                        strSQL = @"select a.IN_DATE,CONVERT(VARCHAR(11), a.IN_DATE, 106) AS [IN_DATE]  from Article_Details a inner join (select Articleid,max(IN_DATE) as IN_DATE from Article_Details group by Articleid )b on a.Articleid = b.Articleid and a.IN_DATE = b.IN_DATE where a.Active<>'No' Order By 1";

                        //  strSQL = @"SELECT distinct DUEDATE, CONVERT(VARCHAR(11), DUEDATE, 106) AS [DUEDATE] FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "Delivery_Date":
                      //  strSQL = @"select a.Delivery_Date,CONVERT(VARCHAR(11), a.Delivery_Date, 106) AS [Delivery_Date]  from Article_Details a inner join (select Articleid,max(Delivery_Date) as Delivery_Date from Article_Details group by Articleid )b on a.Articleid = b.Articleid and a.Delivery_Date = b.Delivery_Date where a.Active<>'No' Order By 1";
                        strSQL = @"select a.Delivery_Date,CONVERT(VARCHAR(11), a.Delivery_Date, 106) AS [Delivery_Date]  from Article_Details a inner join (select Articleid,max(Iteration) as iteration from Article_Details group by Articleid )b on a.Articleid = b.Articleid and a.Iteration = b.iteration and a.Delivery_Date is not null where a.Active<>'No' Order By 1";
                        //  strSQL = @"SELECT distinct DUEDATE, CONVERT(VARCHAR(11), DUEDATE, 106) AS [DUEDATE] FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "jid":
                        strSQL = @"SELECT distinct JID, JID FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "AID":
                        strSQL = @"SELECT distinct AID, AID FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "ArticleTitle":
                        strSQL = @"SELECT distinct ArticleTitle, ArticleTitle FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "AuthorName":
                        strSQL = @"SELECT distinct AuthorName, AuthorName FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "ArticleType":
                        strSQL = @"SELECT distinct ArticleType, ArticleType FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "Publishing_Number":
                        strSQL = @"SELECT distinct Publishing_Number, Publishing_Number FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "ITERATION":
                        strSQL = @"SELECT distinct ITERATION, ITERATION FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "PAGECOUNT":
                        strSQL = @"SELECT distinct PAGECOUNT, PAGECOUNT FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "STAGE":
                        strSQL = @"SELECT distinct STAGE, STAGE FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "tat":
                        strSQL = @"SELECT distinct tat, tat FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "userid":
                        strSQL = @"SELECT distinct userid, userid FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "fullname":
                        strSQL = @"SELECT distinct(l.firstname+' '+l.lastname) as fullname, (l.firstname+' '+l.lastname) as fullname  FROM Article_Details n , login l where n.userid=l.userid and  n.Active<>'No' Order By 1";
                        break;
                    case "WorkTobeDone":
                        strSQL = @"SELECT distinct WorkTobeDone, WorkTobeDone FROM Article_Details where Active<>'No' Order By 1";
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
