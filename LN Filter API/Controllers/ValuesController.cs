using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public FilterValueSet[] Get(string id)
        {
            //return "value";
            SqlConnection connection = null;

            try
            {

                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());

                string strSQL = string.Empty;
                switch (id) // This is the column name in the GridView defined
                {
                    case "Articleid":
                    //case 1:
                        strSQL = @"SELECT distinct Articleid, Articleid FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "DUEDATE":
                    //case 2:
                        strSQL = @"select CONVERT(VARCHAR(10), a.DUEDATE, 105) as DUEDATE,CONVERT(VARCHAR(11), a.DUEDATE, 106) AS [DUEDATE]  from Article_Details a inner join (select Articleid,max(DUEDATE) as DUEDATE from Article_Details group by Articleid )b on a.Articleid = b.Articleid and a.DUEDATE = b.DUEDATE where a.Active<>'No' Order By a.DUEDATE";

                        //  strSQL = @"SELECT distinct DUEDATE, CONVERT(VARCHAR(11), DUEDATE, 106) AS [DUEDATE] FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "IN_DATE":
                    //case 3:
                        strSQL = @"select CONVERT(VARCHAR(10), a.IN_DATE, 105) as IN_DATE,CONVERT(VARCHAR(11), a.IN_DATE, 106) AS [IN_DATE]  from Article_Details a inner join (select Articleid,max(IN_DATE) as IN_DATE from Article_Details group by Articleid )b on a.Articleid = b.Articleid and a.IN_DATE = b.IN_DATE where a.Active<>'No' Order By a.IN_DATE";

                        //  strSQL = @"SELECT distinct DUEDATE, CONVERT(VARCHAR(11), DUEDATE, 106) AS [DUEDATE] FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "Delivery_Date":
                    //case 4:                       //  strSQL = @"select a.Delivery_Date,CONVERT(VARCHAR(11), a.Delivery_Date, 106) AS [Delivery_Date]  from Article_Details a inner join (select Articleid,max(Delivery_Date) as Delivery_Date from Article_Details group by Articleid )b on a.Articleid = b.Articleid and a.Delivery_Date = b.Delivery_Date where a.Active<>'No' Order By 1";
                        strSQL = @"select CONVERT(VARCHAR(10), a.Delivery_Date, 105) as Delivery_Date,CONVERT(VARCHAR(11), a.Delivery_Date, 106) AS [Delivery_Date]  from Article_Details a inner join (select Articleid,max(Iteration) as iteration from Article_Details group by Articleid )b on a.Articleid = b.Articleid and a.Iteration = b.iteration and a.Delivery_Date is not null where a.Active<>'No' Order By a.Delivery_Date";
                        //  strSQL = @"SELECT distinct DUEDATE, CONVERT(VARCHAR(11), DUEDATE, 106) AS [DUEDATE] FROM encyclopedia where Active<>'No' Order By 1";
                        break;
                    case "jid":
                    //case 5:
                        strSQL = @"SELECT distinct JID, JID FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "AID":
                    //case 6:
                        strSQL = @"SELECT distinct AID, AID FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "ArticleTitle":
                    //case 7:
                        strSQL = @"SELECT distinct ArticleTitle, ArticleTitle FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "AuthorName":
                    //case 8:
                        strSQL = @"SELECT distinct AuthorName, AuthorName FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "ArticleType":
                    //case 9:
                        strSQL = @"SELECT distinct ArticleType, ArticleType FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "Publishing_Number":
                    //case 10:
                        strSQL = @"SELECT distinct Publishing_Number, Publishing_Number FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "ITERATION":
                    //case 11:
                        strSQL = @"SELECT distinct ITERATION, ITERATION FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "PAGECOUNT":
                    //case 12:
                        strSQL = @"SELECT distinct PAGECOUNT, PAGECOUNT FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "STAGE":
                    //case 13:
                        strSQL = @"SELECT distinct STAGE, STAGE FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "tat":
                    //case 14:
                        strSQL = @"SELECT distinct tat, tat FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "userid":
                    //case 15:
                        strSQL = @"SELECT distinct userid, userid FROM Article_Details where Active<>'No' Order By 1";
                        break;
                    case "fullname":
                    //case 16:
                        strSQL = @"SELECT distinct(l.firstname+' '+l.lastname) as fullname, (l.firstname+' '+l.lastname) as fullname  FROM Article_Details n , login l where n.userid=l.userid and  n.Active<>'No' Order By 1";
                        break;
                    case "WorkTobeDone":
                    //case 17:
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
                //return JsonConvert.SerializeObject(filterValueList.ToArray);
                //return jso
                return filterValueList.ToArray();
            }
            catch (Exception ex)
            {
                if (connection != null) connection.Close();

                // Log
                throw ex;
            }
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
    public class FilterValueSet
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
}
