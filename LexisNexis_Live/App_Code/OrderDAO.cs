using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderDAO
/// </summary>
public class OrderDAO
{
    public IList<OrderView> GetOrders()
    {
        return GetOrders(string.Empty);
    }
    public IList<OrderView> GetOrders(string strQuery)
    {
        SqlConnection connection = null;

        try
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            if (strQuery == string.Empty)
                strQuery = @"SELECT Encyc_id,Openreceiveddate,Collection_title,folio,item_type,itemdtd,title FROM lexisNexis_Encyclopedia";

            SqlCommand command = new SqlCommand();
            command.CommandText = strQuery;
            command.Connection = connection;

            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

            IList<OrderView> orderViewList = new List<OrderView>();

            while (dataReader.Read())
            {
                OrderView orderView = new OrderView();
                orderView.Encyc_id = Convert.ToInt32(dataReader["Encyc_id"].ToString());
                orderView.Openreceiveddate = Convert.ToDateTime(dataReader["Openreceiveddate"].ToString());
                orderView.Collection_title = dataReader["Collection_title"].ToString();
                orderView.folio = dataReader["folio"].ToString();
                orderView.item_type = dataReader["item_type"].ToString();//Convert.ToDouble();
                orderView.itemdtd = dataReader["itemdtd"].ToString();
                orderView.title = dataReader["title"].ToString();

                orderViewList.Add(orderView);
            }

            connection.Close();

            //return
            return orderViewList;
        }
        catch (Exception ex)
        {
            if (connection != null) connection.Close();

            // Log
            throw ex;
        }
    }
}
public class OrderDAONews
{
    public IList<OrderViewNews> GetOrders()
    {
        return GetOrders(string.Empty);
    }
    public IList<OrderViewNews> GetOrders(string strQuery)
    {
        SqlConnection connection = null;

        try
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());
            if (strQuery == string.Empty)
                strQuery = @"SELECT DID,DECLINATION,INDATE,DUEDATE,CTITLE,DEMANDTYPE,DURATION,ITERATION,PAGECOUNT,STAGE,filename,remarks,userid FROM NEWS where Active <> 'No'";

            SqlCommand command = new SqlCommand();
            command.CommandText = strQuery;
            command.Connection = connection;

            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

            IList<OrderViewNews> orderViewListNews = new List<OrderViewNews>();

            while (dataReader.Read())
            {
                OrderViewNews orderViewnews = new OrderViewNews();
                orderViewnews.did = dataReader["DID"].ToString();

                orderViewnews.DECLINATION = dataReader["DECLINATION"].ToString();
                if (dataReader["INDATE"].ToString() != "")
                {
                    string indate = Convert.ToDateTime(dataReader["INDATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    orderViewnews.INDATE = indate;// dataReader["INDATE"].ToString();
                }
                else
                {
                    orderViewnews.INDATE =  dataReader["INDATE"].ToString();
                }
                orderViewnews.CTITLE = dataReader["CTITLE"].ToString();
                orderViewnews.DEMANDTYPE = dataReader["DEMANDTYPE"].ToString();//Convert.ToDouble();
                orderViewnews.DURATION = dataReader["DURATION"].ToString();
                orderViewnews.ITERATION = dataReader["ITERATION"].ToString();
                orderViewnews.PAGECOUNT = dataReader["PAGECOUNT"].ToString();
                orderViewnews.STAGE = dataReader["STAGE"].ToString();
                orderViewnews.filename = dataReader["filename"].ToString();
                if (dataReader["DUEDATE"].ToString() != "")
                {
                    string duedate = Convert.ToDateTime(dataReader["DUEDATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    orderViewnews.DUEDATE = duedate;// dataReader["INDATE"].ToString();
                }
                else
                {
                    orderViewnews.DUEDATE = dataReader["DUEDATE"].ToString();
                }
                if (dataReader["DELIVERED_DATE"].ToString() != "")
                {
                    string dlevireddate = Convert.ToDateTime(dataReader["DELIVERED_DATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    orderViewnews.Delivered_DATE = dlevireddate;// dataReader["INDATE"].ToString();
                }
                else
                {
                    orderViewnews.Delivered_DATE = dataReader["DELIVERED_DATE"].ToString();
                }
                orderViewnews.remarks = dataReader["remarks"].ToString();
                orderViewnews.userid = dataReader["userid"].ToString();
                orderViewnews.fullname = dataReader["fullname"].ToString();
                orderViewnews.Author = dataReader["Author"].ToString();
                orderViewListNews.Add(orderViewnews);
            }

            connection.Close();

            //return
            return orderViewListNews;
        }
        catch (Exception ex)
        {
            if (connection != null) connection.Close();

            // Log
            throw ex;
        }
    }
}
public class OrderDAOEncylo
{
    public IList<OrderViewEncylo> GetOrders()
    {
        return GetOrders(string.Empty);
    }
    public IList<OrderViewEncylo> GetOrders(string strQuery)
    {
        SqlConnection connection = null;

        try
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());
            if (strQuery == string.Empty)
                strQuery = @"SELECT * FROM ENCYCLOPEDIA  where Active <> 'No'";

            SqlCommand command = new SqlCommand();
            command.CommandText = strQuery;
            command.Connection = connection;

            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

            IList<OrderViewEncylo> orderViewListencyclo = new List<OrderViewEncylo>();

            while (dataReader.Read())
            {
                OrderViewEncylo orderViewencyclo = new OrderViewEncylo();
                orderViewencyclo.Eid = dataReader["EID"].ToString();

                orderViewencyclo.DTITLE = dataReader["DTITLE"].ToString();
                orderViewencyclo.FOLIO = dataReader["FOLIO"].ToString();
                orderViewencyclo.DEMANDTYPE = dataReader["DEMANDTYPE"].ToString();//Convert.ToDouble();
                orderViewencyclo.ITERATION = dataReader["ITERATION"].ToString();
                if (dataReader["INDATE"].ToString() != "")
                {
                    string indate = Convert.ToDateTime(dataReader["INDATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    orderViewencyclo.INDATE = indate;// dataReader["INDATE"].ToString();
                }
                else
                {
                    orderViewencyclo.INDATE =  dataReader["INDATE"].ToString();
                }
                orderViewencyclo.PAGECOUNT = dataReader["PAGECOUNT"].ToString();
                orderViewencyclo.STAGE = dataReader["STAGE"].ToString();
                orderViewencyclo.filesname = dataReader["filesname"].ToString();
                orderViewencyclo.comments = dataReader["COMMENTS"].ToString();
                orderViewencyclo.erid = dataReader["ERID"].ToString();
                orderViewencyclo.tdfilename = dataReader["TDFileName"].ToString();
                orderViewencyclo.tat = dataReader["tat"].ToString();
                if (dataReader["DUEDATE"].ToString() != "")
                {
                    string duedate = Convert.ToDateTime(dataReader["DUEDATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    orderViewencyclo.DUEDATE = duedate;// dataReader["INDATE"].ToString();
                }
                else
                {
                    orderViewencyclo.DUEDATE = dataReader["DUEDATE"].ToString();
                }
                orderViewencyclo.userid = dataReader["userid"].ToString();
                orderViewencyclo.fullname = dataReader["fullname"].ToString();
               // orderViewencyclo.userid = dataReader["username"].ToString();
                orderViewListencyclo.Add(orderViewencyclo);
            }

            connection.Close();

            //return
            return orderViewListencyclo;
        }
        catch (Exception ex)
        {
            if (connection != null) connection.Close();

            // Log
            throw ex;
        }
    }
}

public class OrderDAOJournal
{
    public IList<OrderViewJournal> GetOrders()
    {
        return GetOrders(string.Empty);
    }
    public IList<OrderViewJournal> GetOrders(string strQuery)
    {
        SqlConnection connection = null;

        try
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());
            if (strQuery == string.Empty)
                strQuery = @"SELECT * FROM Article_Details  where Active <> 'No'";

            SqlCommand command = new SqlCommand();
            command.CommandText = strQuery;
            command.Connection = connection;
            command.CommandTimeout = 100;
            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

            IList<OrderViewJournal> orderViewListenjournal = new List<OrderViewJournal>();

            while (dataReader.Read())
            {
                OrderViewJournal orderViewjournal = new OrderViewJournal();

                orderViewjournal.Articleid = dataReader["ArticleID"].ToString();

                orderViewjournal.jid = dataReader["JID"].ToString();
                orderViewjournal.aid = dataReader["AID"].ToString();
                orderViewjournal.ArticleTitle = dataReader["ArticleTitle"].ToString();//Convert.ToDouble();
                orderViewjournal.AuthorName = dataReader["AuthorName"].ToString();
                orderViewjournal.ArticleType = dataReader["Articletype"].ToString();
                orderViewjournal.Publishing_Number = dataReader["Publishing_Number"].ToString();
                orderViewjournal.tat = dataReader["TAT"].ToString();
                orderViewjournal.iteration = dataReader["Iteration"].ToString();
                if (dataReader["IN_DATE"].ToString() != "")
                {
                    string indate = Convert.ToDateTime(dataReader["IN_DATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    orderViewjournal.IN_DATE = indate;// dataReader["IN_DATE"].ToString();
                }
                else
                {
                   
                    orderViewjournal.IN_DATE =  dataReader["IN_DATE"].ToString();
                }
               
                if (dataReader["duedate"].ToString() != "")
                {
                    string duedate = Convert.ToDateTime(dataReader["duedate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    orderViewjournal.DUEDATE = duedate;// dataReader["duedate"].ToString();
                }
                else
                {
                    orderViewjournal.DUEDATE =  dataReader["duedate"].ToString();
                }
              
                if (dataReader["Delivery_DATE"].ToString() != "")
                {
                    string deldate = Convert.ToDateTime(dataReader["Delivery_DATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    orderViewjournal.Delivery_DATE = deldate;// dataReader["Delivery_DATE"].ToString();
                }
                else
                {
                    orderViewjournal.Delivery_DATE =  dataReader["Delivery_DATE"].ToString();
                }
                
                orderViewjournal.PAGECOUNT = dataReader["pagecount"].ToString();
				orderViewjournal.charactercount = dataReader["charactercount"].ToString();
                orderViewjournal.STAGE = dataReader["STAGE"].ToString();
                orderViewjournal.userid = dataReader["userid"].ToString();
                orderViewjournal.fullname = dataReader["fullname"].ToString();
                orderViewjournal.filename = dataReader["Filename"].ToString();
                orderViewjournal.tdfilename = dataReader["TDFilename"].ToString();
                orderViewjournal.comments = dataReader["Comments"].ToString();
                orderViewjournal.Cancel_comment = dataReader["Cancel_comment"].ToString();
                orderViewjournal.Articlerid = dataReader["ArticleRID"].ToString();
                orderViewjournal.journal_Name = dataReader["journal_Name"].ToString();
                orderViewjournal.WorkTobeDone = dataReader["WorkTobeDone"].ToString();
                orderViewListenjournal.Add(orderViewjournal);
            }

            connection.Close();

            //return
            return orderViewListenjournal;
        }
        catch (Exception ex)
        {
            if (connection != null) connection.Close();

            // Log
            throw ex;
        }
    }
}
public class OrderDAOFiche
{
    public IList<OrderViewFiche> GetOrders()
    {
        return GetOrders(string.Empty);
    }
    public IList<OrderViewFiche> GetOrders(string strQuery)
    {
        SqlConnection connection = null;

        try
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());
            if (strQuery == string.Empty)
                strQuery = @"SELECT * FROM fiches  where Active <> 'No'";

            SqlCommand command = new SqlCommand();
            command.CommandText = strQuery;
            command.Connection = connection;

            command.Connection.Open();
            SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

            IList<OrderViewFiche> orderViewListfiche = new List<OrderViewFiche>();

            while (dataReader.Read())
            {
                OrderViewFiche orderViewfiche = new OrderViewFiche();
                orderViewfiche.Fid = dataReader["FID"].ToString();

                orderViewfiche.Ftitle = dataReader["ftitle"].ToString();
                orderViewfiche.FOLIO = dataReader["FOLIO"].ToString();
                orderViewfiche.DEMANDTYPE = dataReader["DEMANDTYPE"].ToString();//Convert.ToDouble();
                orderViewfiche.ITERATION = dataReader["ITERATION"].ToString();
                orderViewfiche.tat = dataReader["tat"].ToString();
                orderViewfiche.Duration = dataReader["duration"].ToString();
              //  orderViewfiche.INDATE = dataReader["INDATE"].ToString();
                orderViewfiche.PAGECOUNT = dataReader["PAGECOUNT"].ToString();
                orderViewfiche.STAGE = dataReader["STAGE"].ToString();
                orderViewfiche.filesname = dataReader["filesname"].ToString();
                orderViewfiche.DUEDATE = dataReader["DUEDATE"].ToString();
                orderViewfiche.comments = dataReader["COMMENTS"].ToString();
                orderViewfiche.userid = dataReader["userid"].ToString();
                orderViewfiche.fullname = dataReader["fullname"].ToString();
                orderViewfiche.FRid = dataReader["FRID"].ToString();
                orderViewfiche.tdfilename = dataReader["tdfilename"].ToString();

                orderViewfiche.combi = dataReader["combi"].ToString();
                orderViewfiche.codecoll = dataReader["codecoll"].ToString();
                orderViewfiche.nummac = dataReader["nummac"].ToString();
                orderViewfiche.artfas = dataReader["artfas"].ToString();
                orderViewfiche.numfas = dataReader["numfas"].ToString();
                orderViewfiche.Sgm_Filename = dataReader["Sgm_Filename"].ToString();
                orderViewfiche.errorfilename = dataReader["errorfilename"].ToString();
                if (dataReader["INDATE"].ToString() != "")
                {
                    string indate = Convert.ToDateTime(dataReader["INDATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    orderViewfiche.INDATE = indate;// dataReader["IN_DATE"].ToString();
                }
                else
                {

                    orderViewfiche.INDATE = dataReader["INDATE"].ToString();
                }

                if (dataReader["duedate"].ToString() != "")
                {
                    string duedate = Convert.ToDateTime(dataReader["duedate"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    orderViewfiche.DUEDATE = duedate;// dataReader["duedate"].ToString();
                }
                else
                {
                    orderViewfiche.DUEDATE = dataReader["duedate"].ToString();
                }

                if (dataReader["DELIVERED_DATE"].ToString() != "")
                {
                    string deldate = Convert.ToDateTime(dataReader["DELIVERED_DATE"].ToString()).ToString("dd/MM/yyyy HH:mm:ss");
                    orderViewfiche.DELIVERED_DATE = deldate;// dataReader["Delivery_DATE"].ToString();
                }
                else
                {
                    orderViewfiche.DELIVERED_DATE = dataReader["DELIVERED_DATE"].ToString();
                }


                orderViewListfiche.Add(orderViewfiche);
            }

            connection.Close();

            //return
            return orderViewListfiche;
        }
        catch (Exception ex)
        {
            if (connection != null) connection.Close();

            // Log
            throw ex;
        }
    }
}