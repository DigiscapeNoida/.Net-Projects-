using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TD.Data;
using System.IO;
using System.Web.Services;
using System.Drawing;

namespace LexisNexis
{
    public partial class EncyclopediasLanding : System.Web.UI.Page
    {
         
        public static string _sortDirection;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SESSION.LOGGED_USER] != null)
            {
                Session[SESSION.LOGGED_PRODSITE] = "RV";
                hidDivId.Value = Session[SESSION.LOGGED_USER].ToString();
                if (Session[SESSION.LOGGED_ROLE].ToString() == "LN")
                {
                 // comment on 15 dec 
                   // btnremove.Visible = true;
                }
                if (Session[SESSION.LOGGED_ROLE].ToString() == "TDM" || Session[SESSION.LOGGED_ROLE].ToString() == "TDN")
                {
                    ddltask.Visible = false;
                    lblTask.Visible = false;
                }

                // 31 aug remove
                lblname.Text = Session[SESSION.LOGGED_USER_NAME].ToString();
                /////

                if (!Page.IsPostBack)
                {
                    BindGrid();
                    /*
                    bindcheckboxjid();
                    bindjid();
                    bindproduct();
                    if (Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString() == "")
                    {
                        BindGrid();
                    }
                    else
                    {
                        BindGridMemory(Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString());
                       // ddlreview.Items.FindByValue(Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString().Trim()).Selected = true;
                        foreach (ListItem chk in CheckBoxList1.Items)
                        {
                            string[] arr = Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString().Trim().Split(',');
                            for (int i = 0; i < arr.Length; i++)
                            {
                                string tmpval = arr[i].ToString().Replace("'", "");
                                if (chk.Value == tmpval)
                                {
                                    chk.Selected = true;
                                }
                            }
                           
                           
                        }
                    }
                   // BindGrid();
                  
                    // 31 aug remove
                    bindtask();
                    ddlProduct.SelectedValue = "RV";

                    */
                }
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            if (Session[SESSION.LOGGED_USER] != null)
            {
                Session[SESSION.LOGGED_PRODSITE] = "RV";
                BindGrid();
                /*
                if (Session["searchqryJournal"] != null)
                {
                    BindsearchGrid();
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        if (Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString() == "")
                        {
                            BindGrid();
                        }
                        else
                        {
                            BindGridMemory(Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString());
                            Session[SESSION.LOGGED_PREVIOUS_REDACTION] = "";
                        }
                    }
                }
                */
            }
        }
        private void BindsearchGrid()
        {
            string strQuery = Session["searchqryJournal"].ToString();
            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();

            OrderDAOJournal objProductDAO = new OrderDAOJournal();

            IList<OrderViewJournal> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 5; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
            else
            {
               
                btnremove.Visible = false;
            }
            Session["searchqryJournal"] = null;
        }
        public string SortDireaction
        {
            get
            {
                if (ViewState["SortDireaction"] == null)
                    return string.Empty;
                else
                    return ViewState["SortDireaction"].ToString();
            }
            set
            {
                ViewState["SortDireaction"] = value;
            }
        }
        private void bindjid()
        {
           
            ddlreview.Items.Clear();
            //SqlParameter[] paramlist = new SqlParameter[1];
            //paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getJournalInfo);
            ddlreview.DataSource = ds;
            ddlreview.DataTextField = "Journal_name";
            ddlreview.DataValueField = "JID";
            ddlreview.DataBind();
            ddlreview.Items.Insert(0, new ListItem("----------", "-1"));
        }
        private void bindcheckboxjid()
        {
            // ddlreview.Items.Clear();
            //SqlParameter[] paramlist = new SqlParameter[1];
            //paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getJournalInfo);
            CheckBoxList1.DataSource = ds;
            CheckBoxList1.DataTextField = "journal_name";
            CheckBoxList1.DataValueField = "JID";
            CheckBoxList1.DataBind();

        }
        private void bindproduct()
        {
            ddlProduct.Items.Clear();
            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getproduct, paramlist);
            ddlProduct.DataSource = ds;
            ddlProduct.DataTextField = "proddesc";
            ddlProduct.DataValueField = "prodid";
            ddlProduct.DataBind();
           // ddlProduct.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        // 31 aug remove
        private void bindtask()
        {
            ddltask.Items.Clear();
            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.gettask, paramlist);
            ddltask.DataSource = ds;
            ddltask.DataTextField = "taskdesc";
            ddltask.DataValueField = "taskid";
            ddltask.DataBind();
            ddltask.Items.Insert(0, new ListItem("----------", "-1"));
        }
        private string ArrayToString(IEnumerable<string> strValues)
        {
            string strValue = string.Empty;
            foreach (string strVal in strValues)
                strValue = strValue + "'" + strVal + "', ";
            if (strValue.Length > 0)
                strValue = "(" + strValue.Substring(0, strValue.Length - 2) + ")";
            
            return strValue;
        }
        private void BindGridMemory(string collection)
        {
           
        
            //string strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid AND a.JID in(" + collection + ")";
            string strQuery = "select a.*,l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid AND a.JID in(" + collection + ")";
            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();

            if (hndSelectedValue.Value.Trim().Length > 0)
            {
                SelectedDataCollection objSelectedDataCollection = new SelectedDataCollection();
                objSelectedDataCollection = objJavaScriptSerializer.Deserialize<SelectedDataCollection>(hndSelectedValue.Value);

                foreach (SelectedData selectedData in objSelectedDataCollection.DataCollection)
                {
                    if (selectedData.SelectedValue.Count<string>() > 0)
                    {
                        if (selectedData.ColumnName == "DUEDATE")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "IN_DATE")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "Delivery_Date")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "Articleid")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "a." + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                        }
                        else if (selectedData.ColumnName == "fullname")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "l.firstname+' '+l.lastname" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                        }
                    }
                }
            }
            else
            {
                strQuery = strQuery + " AND A.Active<> 'No' ";
            }

            OrderDAOJournal objProductDAO = new OrderDAOJournal();

            IList<OrderViewJournal> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 5; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
            else
            {

                btnremove.Visible = false;
            }

        }
        private void BindGrid()
        {

            string queryval = "";
            string strQuery = "";
            /*
            foreach (ListItem chk in CheckBoxList1.Items)
            {

                if (chk.Selected == true)
                {
                    if (queryval == "")
                    {
                        queryval = "'" + chk.Value + "'";
                    }
                    else
                    {
                        queryval = queryval + "," + "'" + chk.Value + "'";
                    }
                }
            }

            //string strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate";//SELECT * FROM ENCYCLOPEDIA
          //  string strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(duedate) as duedate from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.duedate = b.duedate WHERE  a.userid=l.userid";//SELECT * FROM ENCYCLOPEDIA
            string strQuery = "";
            if (queryval != "")
            {
                // strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and JID in(" + queryval + ")";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid and JID in(" + queryval + ")";
            }
            else
            {
                // strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid";
            }
            */


            strQuery = "select a.*,l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid and stage='En préparation'";
            
            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();

            if (hndSelectedValue.Value.Trim().Length > 0)
            {
                SelectedDataCollection objSelectedDataCollection = new SelectedDataCollection();
                objSelectedDataCollection = objJavaScriptSerializer.Deserialize<SelectedDataCollection>(hndSelectedValue.Value);

                foreach (SelectedData selectedData in objSelectedDataCollection.DataCollection)
                {
                    if (selectedData.SelectedValue.Count<string>() > 0)
                    {
                        if (selectedData.ColumnName == "DUEDATE")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No' order by Articleid";
                        }
                        else if (selectedData.ColumnName == "IN_DATE")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No' order by Articleid";
                        }
                        else if (selectedData.ColumnName == "Delivery_Date")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No' order by Articleid";
                        }
                        else if (selectedData.ColumnName == "Articleid")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "a."+selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                        }
                        else if (selectedData.ColumnName == "fullname")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "l.firstname+' '+l.lastname" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No' order by Articleid";
                        }
                        else
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                        }
                    }
                }
            }
            else
            {
                strQuery = strQuery + " AND A.Active<> 'No' order by Articleid";
            }

            OrderDAOJournal objProductDAO = new OrderDAOJournal();

            IList<OrderViewJournal> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());
           
            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 5; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
            else
            {
              
                btnremove.Visible = false;
            }

        }
      
        protected void Refresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void grdViewOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dataTable = new DataTable();

            string queryval = "";
            foreach (ListItem chk in CheckBoxList1.Items)
            {

                if (chk.Selected == true)
                {
                    if (queryval == "")
                    {
                        queryval = "'" + chk.Value + "'";
                    }
                    else
                    {
                        queryval = queryval + "," + "'" + chk.Value + "'";
                    }
                }
            }
            string strQuery = "";
            /*
            if (queryval != "")
            {





                strQuery = "select  a.Articleid,a.JID,a.AID,a.Stage,a.ArticleTitle,a.ArticleType,a.Schedule_Issue,a.PIT,a.Doc_Head,a.ISSN,a.PII,a.DOI,a.TAT," +
                "a.CE_Required,a.pagecount,a.wordcount,a.charactercount, CONVERT(VARCHAR(10), a.received_Date, 103) + ' '  + convert(VARCHAR(8), a.received_Date, 14) as received_Date," +
                 "CONVERT(VARCHAR(10), a.revised_date, 103) + ' '  + convert(VARCHAR(8), a.revised_date, 14) as revised_date," +
                "CONVERT(VARCHAR(10), a.In_date, 103) + ' '  + convert(VARCHAR(8), a.In_date, 14) as in_date," +
                "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate," +
                "CONVERT(VARCHAR(10), a.delivery_date, 103) + ' '  + convert(VARCHAR(8), a.delivery_date, 14) as delivery_date," +
                "CONVERT(VARCHAR(10), a.reviseddelivery_date, 103) + ' '  + convert(VARCHAR(8), a.reviseddelivery_date, 14) as reviseddelivery_date," +
                "a.Iteration,CONVERT(VARCHAR(10), a.Itemcomplete_date, 103) + ' '  + convert(VARCHAR(8), a.Itemcomplete_date, 14) as Itemcomplete_date," +
                "a.physicle_figs,a.bw_figs,a.Web_color_figs,a.color_figs,a.Authorname,a.email,a.status,a.no_of_loops,a.volume,a.issue,a.editor_code,a.comments,a.td_comments," +
                "a.expire_status,a.publishing_year,a.publishing_number,a.filename,a.tdfilename,a.userid,a.articlerid" +
                ",l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid  and a.jid=j.jid  and JID in(" + queryval + ")";
                // ",l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid  and JID in(" + queryval + ")";
            }
            else
            {
                strQuery = "select  a.Articleid,a.JID,a.AID,a.Stage,a.ArticleTitle,a.ArticleType,a.Schedule_Issue,a.PIT,a.Doc_Head,a.ISSN,a.PII,a.DOI,a.TAT," +
               "a.CE_Required,a.pagecount,a.wordcount,a.charactercount, CONVERT(VARCHAR(10), a.received_Date, 103) + ' '  + convert(VARCHAR(8), a.received_Date, 14) as received_Date," +
                "CONVERT(VARCHAR(10), a.revised_date, 103) + ' '  + convert(VARCHAR(8), a.revised_date, 14) as revised_date," +
               "CONVERT(VARCHAR(10), a.In_date, 103) + ' '  + convert(VARCHAR(8), a.In_date, 14) as in_date," +
               "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate," +
               "CONVERT(VARCHAR(10), a.delivery_date, 103) + ' '  + convert(VARCHAR(8), a.delivery_date, 14) as delivery_date," +
               "CONVERT(VARCHAR(10), a.reviseddelivery_date, 103) + ' '  + convert(VARCHAR(8), a.reviseddelivery_date, 14) as reviseddelivery_date," +
               "a.Iteration,CONVERT(VARCHAR(10), a.Itemcomplete_date, 103) + ' '  + convert(VARCHAR(8), a.Itemcomplete_date, 14) as Itemcomplete_date," +
               "a.physicle_figs,a.bw_figs,a.Web_color_figs,a.color_figs,a.Authorname,a.email,a.status,a.no_of_loops,a.volume,a.issue,a.editor_code,a.comments,a.td_comments," +
               "a.expire_status,a.publishing_year,a.publishing_number,a.filename,a.tdfilename,a.userid,a.articlerid" +
               ",l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid";
                //",l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid";
            }
            */
            strQuery = "select  a.Articleid,a.JID,a.AID,a.Stage,a.ArticleTitle,a.ArticleType,a.Schedule_Issue,a.PIT,a.Doc_Head,a.ISSN,a.PII,a.DOI,a.TAT," +
             "a.CE_Required,a.pagecount,a.wordcount,a.charactercount, CONVERT(VARCHAR(10), a.received_Date, 103) + ' '  + convert(VARCHAR(8), a.received_Date, 14) as received_Date," +
              "CONVERT(VARCHAR(10), a.revised_date, 103) + ' '  + convert(VARCHAR(8), a.revised_date, 14) as revised_date," +
             "CONVERT(VARCHAR(10), a.In_date, 103) + ' '  + convert(VARCHAR(8), a.In_date, 14) as in_date," +
             "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate," +
             "CONVERT(VARCHAR(10), a.delivery_date, 103) + ' '  + convert(VARCHAR(8), a.delivery_date, 14) as delivery_date," +
             "CONVERT(VARCHAR(10), a.reviseddelivery_date, 103) + ' '  + convert(VARCHAR(8), a.reviseddelivery_date, 14) as reviseddelivery_date," +
             "a.Iteration,CONVERT(VARCHAR(10), a.Itemcomplete_date, 103) + ' '  + convert(VARCHAR(8), a.Itemcomplete_date, 14) as Itemcomplete_date," +
             "a.physicle_figs,a.bw_figs,a.Web_color_figs,a.color_figs,a.Authorname,a.email,a.status,a.no_of_loops,a.volume,a.issue,a.editor_code,a.comments,a.td_comments," +
             "a.expire_status,a.publishing_year,a.publishing_number,a.filename,a.tdfilename,a.userid,a.articlerid" +
             ",l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid and a.stage='En préparation'";
          
            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();

            if (hndSelectedValue.Value.Trim().Length > 0)
            {
                SelectedDataCollection objSelectedDataCollection = new SelectedDataCollection();
                objSelectedDataCollection = objJavaScriptSerializer.Deserialize<SelectedDataCollection>(hndSelectedValue.Value);

                foreach (SelectedData selectedData in objSelectedDataCollection.DataCollection)
                {
                    if (selectedData.SelectedValue.Count<string>() > 0)
                    {
                        if (selectedData.ColumnName == "DUEDATE")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "IN_DATE")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "Delivery_Date")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "Articleid")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "a." + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                        }
                        else if (selectedData.ColumnName == "fullname")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "l.firstname+' '+l.lastname" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                        }
                    }
                }
            }
            else
            {
                strQuery = strQuery + " and a.Active<> 'No'";// order by Articleid
            }
            bool datsts = false;
            if (GridViewSortExpression != string.Empty)
            {
                if (GridViewSortExpression.ToString() == "IN_DATE" || GridViewSortExpression.ToString() == "DUEDATE" || GridViewSortExpression.ToString() == "Delivery_Date")
                {
                    string sort = ViewState["SortDireaction"].ToString();// GetSortDirection();
                    strQuery = strQuery + " Order by " + "a." + GridViewSortExpression.ToString() + " " + sort;
                    datsts = true;
                }
                else
                {
                    strQuery = strQuery + " order by Articleid";
                }
            }
            else
            {
                strQuery = strQuery + " order by Articleid";
            }
            SqlConnection connection = null;


            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());
            SqlCommand command = new SqlCommand();
            command.CommandText = strQuery;
            command.Connection = connection;

            command.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dataTable);

            connection.Close();

            if (datsts == false)
            {
                grdViewOrders.DataSource = SortDataTablepageing(dataTable, false);
            }
            else
            {
                grdViewOrders.DataSource = dataTable;
            }
        //    grdViewOrders.DataSource = SortDataTable(dataTable, true);

            grdViewOrders.PageIndex = e.NewPageIndex;
            grdViewOrders.DataBind();

            for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 5; intRowIndex++)
            {
                TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                tblCell.CssClass = "filter";
                tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
            }
            /*
            grdViewOrders.PageIndex = e.NewPageIndex;
            if (ViewState["myDataSet"] != null)
            {
                DataView dataView = new DataView((DataTable)ViewState["myDataSet"]);
                if ((string)ViewState["SortDir"] == "ASC" || String.IsNullOrEmpty((string)ViewState["SortDir"]))
                {
                    dataView.Sort = "ASC";
                   
                }
                else if ((string)ViewState["SortDir"] == "DESC")
                {
                    dataView.Sort =  "DESC";
                    
                }
                grdViewOrders.DataSource = dataView;
                grdViewOrders.DataBind();
            }
            else
            {
                BindGrid();
            }
            */
        }

        protected void grdViewOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = new DataTable();

            string queryval = "";
            foreach (ListItem chk in CheckBoxList1.Items)
            {

                if (chk.Selected == true)
                {
                    if (queryval == "")
                    {
                        queryval = "'" + chk.Value + "'";
                    }
                    else
                    {
                        queryval = queryval + "," + "'" + chk.Value + "'";
                    }
                }
            }
            string strQuery = "";
            /*
            if (queryval != "")
            {

               

 

                strQuery = "select  a.Articleid,a.JID,a.AID,a.Stage,a.ArticleTitle,a.ArticleType,a.Schedule_Issue,a.PIT,a.Doc_Head,a.ISSN,a.PII,a.DOI,a.TAT,"+
                "a.CE_Required,a.pagecount,a.wordcount,a.charactercount, CONVERT(VARCHAR(10), a.received_Date, 103) + ' '  + convert(VARCHAR(8), a.received_Date, 14) as received_Date,"+
                 "CONVERT(VARCHAR(10), a.revised_date, 103) + ' '  + convert(VARCHAR(8), a.revised_date, 14) as revised_date,"+
                "CONVERT(VARCHAR(10), a.In_date, 103) + ' '  + convert(VARCHAR(8), a.In_date, 14) as in_date,"+
                "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate,"+
                "CONVERT(VARCHAR(10), a.delivery_date, 103) + ' '  + convert(VARCHAR(8), a.delivery_date, 14) as delivery_date,"+
                "CONVERT(VARCHAR(10), a.reviseddelivery_date, 103) + ' '  + convert(VARCHAR(8), a.reviseddelivery_date, 14) as reviseddelivery_date,"+
                "a.Iteration,CONVERT(VARCHAR(10), a.Itemcomplete_date, 103) + ' '  + convert(VARCHAR(8), a.Itemcomplete_date, 14) as Itemcomplete_date,"+
                "a.physicle_figs,a.bw_figs,a.Web_color_figs,a.color_figs,a.Authorname,a.email,a.status,a.no_of_loops,a.volume,a.issue,a.editor_code,a.comments,a.td_comments,"+
                "a.expire_status,a.publishing_year,a.publishing_number,a.filename,a.tdfilename,a.userid,a.articlerid"+
                ",l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid and JID in(" + queryval + ")";
                //",l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid  and JID in(" + queryval + ")";
            }
            else
            {
                 strQuery = "select  a.Articleid,a.JID,a.AID,a.Stage,a.ArticleTitle,a.ArticleType,a.Schedule_Issue,a.PIT,a.Doc_Head,a.ISSN,a.PII,a.DOI,a.TAT,"+
                "a.CE_Required,a.pagecount,a.wordcount,a.charactercount, CONVERT(VARCHAR(10), a.received_Date, 103) + ' '  + convert(VARCHAR(8), a.received_Date, 14) as received_Date,"+
                 "CONVERT(VARCHAR(10), a.revised_date, 103) + ' '  + convert(VARCHAR(8), a.revised_date, 14) as revised_date,"+
                "CONVERT(VARCHAR(10), a.In_date, 103) + ' '  + convert(VARCHAR(8), a.In_date, 14) as in_date,"+
                "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate,"+
                "CONVERT(VARCHAR(10), a.delivery_date, 103) + ' '  + convert(VARCHAR(8), a.delivery_date, 14) as delivery_date,"+
                "CONVERT(VARCHAR(10), a.reviseddelivery_date, 103) + ' '  + convert(VARCHAR(8), a.reviseddelivery_date, 14) as reviseddelivery_date,"+
                "a.Iteration,CONVERT(VARCHAR(10), a.Itemcomplete_date, 103) + ' '  + convert(VARCHAR(8), a.Itemcomplete_date, 14) as Itemcomplete_date,"+
                "a.physicle_figs,a.bw_figs,a.Web_color_figs,a.color_figs,a.Authorname,a.email,a.status,a.no_of_loops,a.volume,a.issue,a.editor_code,a.comments,a.td_comments,"+
                "a.expire_status,a.publishing_year,a.publishing_number,a.filename,a.tdfilename,a.userid,a.articlerid"+
                ",l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid";
                //",l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid";
            }
            */
            strQuery = "select  a.Articleid,a.JID,a.AID,a.Stage,a.ArticleTitle,a.ArticleType,a.Schedule_Issue,a.PIT,a.Doc_Head,a.ISSN,a.PII,a.DOI,a.TAT," +
                "a.CE_Required,a.pagecount,a.wordcount,a.charactercount, CONVERT(VARCHAR(10), a.received_Date, 103) + ' '  + convert(VARCHAR(8), a.received_Date, 14) as received_Date," +
                 "CONVERT(VARCHAR(10), a.revised_date, 103) + ' '  + convert(VARCHAR(8), a.revised_date, 14) as revised_date," +
                "CONVERT(VARCHAR(10), a.In_date, 103) + ' '  + convert(VARCHAR(8), a.In_date, 14) as in_date," +
                "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate," +
                "CONVERT(VARCHAR(10), a.delivery_date, 103) + ' '  + convert(VARCHAR(8), a.delivery_date, 14) as delivery_date," +
                "CONVERT(VARCHAR(10), a.reviseddelivery_date, 103) + ' '  + convert(VARCHAR(8), a.reviseddelivery_date, 14) as reviseddelivery_date," +
                "a.Iteration,CONVERT(VARCHAR(10), a.Itemcomplete_date, 103) + ' '  + convert(VARCHAR(8), a.Itemcomplete_date, 14) as Itemcomplete_date," +
                "a.physicle_figs,a.bw_figs,a.Web_color_figs,a.color_figs,a.Authorname,a.email,a.status,a.no_of_loops,a.volume,a.issue,a.editor_code,a.comments,a.td_comments," +
                "a.expire_status,a.publishing_year,a.publishing_number,a.filename,a.tdfilename,a.userid,a.articlerid" +
                ",l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid and a.stage='En préparation' ";

            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();

            if (hndSelectedValue.Value.Trim().Length > 0)
            {
                SelectedDataCollection objSelectedDataCollection = new SelectedDataCollection();
                objSelectedDataCollection = objJavaScriptSerializer.Deserialize<SelectedDataCollection>(hndSelectedValue.Value);

                foreach (SelectedData selectedData in objSelectedDataCollection.DataCollection)
                {
                    if (selectedData.SelectedValue.Count<string>() > 0)
                    {
                        if (selectedData.ColumnName == "DUEDATE")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a."  + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "IN_DATE")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "Delivery_Date")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "Articleid")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "a." + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                        }
                          else if (selectedData.ColumnName == "fullname")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "l.firstname+' '+l.lastname" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                        }
                    }
                }
            }
            else
            {
                strQuery = strQuery + " and a.Active<> 'No' ";
            }
            bool datsts = false;
            if (e.SortExpression.ToString() == "IN_DATE" || e.SortExpression.ToString() == "DUEDATE" || e.SortExpression.ToString() == "Delivery_Date")
            {
              string sort=  GetSortDirection();
                strQuery = strQuery + " Order by " + "a."+e.SortExpression.ToString()+" "+sort;
                datsts = true;
            }

            SqlConnection connection = null;


            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());
            SqlCommand command = new SqlCommand();
            command.CommandText = strQuery;
            command.Connection = connection;

            command.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dataTable);

            connection.Close();

           

            

           // string SortDireaction = e.SortDirection.ToString();
          //  SetSortDirection(SortDireaction);
            if (dataTable != null)
            {


                GridViewSortExpression = e.SortExpression;

                //Gets the Pageindex of the GridView.
                int iPageIndex = grdViewOrders.PageIndex;
                if (datsts == false)
                {
                    grdViewOrders.DataSource = SortDataTable(dataTable, false);
                }
                else
                {
                    grdViewOrders.DataSource = dataTable;
                }
                grdViewOrders.DataBind();
                grdViewOrders.PageIndex = iPageIndex;

                /*
                //Sort the data.
                dataTable.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                DataView dataView = new DataView(dataTable);
                if ((string)ViewState["SortDir"] == "ASC" || String.IsNullOrEmpty((string)ViewState["SortDir"]))
                {
                    dataView.Sort = e.SortExpression + " ASC";
                    ViewState["SortDir"] = "DESC";
                }
                else if ((string)ViewState["SortDir"] == "DESC")
                {
                    dataView.Sort = e.SortExpression + " DESC";
                    ViewState["SortDir"] = "ASC";
                }
                int iPageIndex = grdViewOrders.PageIndex;
                grdViewOrders.DataSource = dataView;
                grdViewOrders.DataBind();
                grdViewOrders.PageIndex = iPageIndex;
                SortDireaction = _sortDirection;
               
               ViewState["myDataSet"] = dataView.Table;
                */
            }

            for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 5; intRowIndex++)
            {
                TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                tblCell.CssClass = "filter";
                tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
            }

        }
        //Gets or Sets the GridView SortDirection Property
        private string GridViewSortDirection
        {
              get
            {
                if (ViewState["SortDireaction"] == null)
                    return string.Empty;
                else
                    return ViewState["SortDireaction"].ToString();
            }
            set
            {
                ViewState["SortDireaction"] = value;
            }

            /*
            get
            {
                return ViewState["SortDirection"] as string ?? "ASC";
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
            */
        }
        //Gets or Sets the GridView SortExpression Property
        private string GridViewSortExpression
        {


            get
            {
                return ViewState["SortExpression"] as string ?? string.Empty;
            }
            set
            {
                ViewState["SortExpression"] = value;
            }
        }

        //Toggles between the Direction of the Sorting
        private string GetSortDirection()
        {
            if (GridViewSortDirection == "ASC")
            {
                GridViewSortDirection = "DESC";
            }
            else
            {
                GridViewSortDirection = "ASC";
            }

            /*
            switch (GridViewSortDirection)
            {
                case "ASC":
                    GridViewSortDirection = "DESC";
                    break;
                case "DESC":
                    GridViewSortDirection = "ASC";
                    break;
            }
            */

            return GridViewSortDirection;
        }
        protected DataView SortDataTable(DataTable myDataTable, bool isPageIndexChanging)
        {
            if (myDataTable != null)
            {
                DataView myDataView = new DataView(myDataTable);
                if (GridViewSortExpression != string.Empty)
                {
                    if (isPageIndexChanging)
                    {
                        myDataView.Sort = string.Format("{0} {1}",
                        GridViewSortExpression, GridViewSortDirection);
                    }
                    else
                    {
                        myDataView.Sort = string.Format("{0:dd-MMM-yyyy}",
                        GridViewSortExpression, GetSortDirection());
                    }
                }
                return myDataView;
            }
            else
            {

                return new DataView();
            }
        }
        protected DataView SortDataTablepageing(DataTable myDataTable, bool isPageIndexChanging)
        {
            if (myDataTable != null)
            {
                DataView myDataView = new DataView(myDataTable);
                if (GridViewSortExpression != string.Empty)
                {
                    if (isPageIndexChanging)
                    {
                        myDataView.Sort = string.Format("{0} {1}",
                        GridViewSortExpression, ViewState["SortDireaction"].ToString());
                    }
                    else
                    {
                        myDataView.Sort = string.Format("{0:dd-MMM-yyyy}",
                        GridViewSortExpression, ViewState["SortDireaction"].ToString());
                    }
                }
                return myDataView;
            }
            else
            {

                return new DataView();
            }
        }

        protected void SetSortDirection(string sortDirection)
        {
            if (sortDirection == "ASC")
            {
                _sortDirection = "DESC";
            }
            else
            {
                _sortDirection = "ASC";
            }
        }

       
        
      
      
        protected void btnremove_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                int cnt = 0;
                bool status = false;
                foreach (GridViewRow row in grdViewOrders.Rows)
                {
                    CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
                    if (chkCalendarId.Checked)
                    {
                        status = true;
                        string liCalendarId = (row.FindControl("litID") as Literal).Text;

                        SqlParameter[] paramlist = new SqlParameter[2];
                        paramlist[0] = new SqlParameter("articleid", liCalendarId);
                        paramlist[1] = new SqlParameter("InDate", Common.GetDayLightTime());
                        int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.removeJournalItem, paramlist);
                        if (rowresult > 0)
                        {
                            cnt++;
                            //Success mail
                            SqlParameter[] paramlist1 = new SqlParameter[1];
                            paramlist1[0] = new SqlParameter("articleid", liCalendarId);
                            DataSet set = DataAccess.ExecuteDataSetSP(SPNames.getemaildatajournal, paramlist1);
                            if (set.Tables[0].Rows.Count > 0)
                            {

                                string strTitle = set.Tables[0].Rows[0]["ArticleTitle"].ToString();
                                string strDuedate = set.Tables[0].Rows[0]["DUEDATE"].ToString();
                                string strComments = set.Tables[0].Rows[0]["COMMENTS"].ToString();

                                string strTo = Session[SESSION.LOGGED_USER].ToString();// System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                                string strLT = Session[SESSION.LOGGED_ROLE].ToString();

                                //    string strStage = "Article in process";

                                string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";
                              

                                string strCC = set.Tables[0].Rows[0]["Email"].ToString();
                                // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                                // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                                string strFile = Server.MapPath("App_Data\\MAILS\\LN_DELETE\\") + "J.html";

                                if (File.Exists(strFile))
                                {
                                    StreamReader sr = new StreamReader(strFile);
                                    string FileC = sr.ReadToEnd();
                                    sr.Close();
                                    string strBody = FileC;
                                    strBody = strBody.Replace("[ILT]", strTitle);
                                    strBody = strBody.Replace("[DTAD]", strDuedate);
                                    if (strComments == "")
                                    {
                                        strBody = strBody.Replace("[IACE]", "aucun commentaire");
                                    }
                                    else
                                    {
                                        strBody = strBody.Replace("[IACE]", strComments);
                                    }
                                    strBody = strBody.Replace("[IHT]", strLink);

                                    string strSubject = "Léonard – Intervention extérieure annulée pour le document : « " + strTitle + " »";
                                    Common cmn = new Common();
                                    Common.SendEmail(strTo, strCC, strSubject, strBody);
                                    // Utility.NumberToEnglish.email();
                                }

                            }

                        }
                        if (DataAccess.isDBConnectionFail == true)
                        {
                            //  Errlbl.Visible = true;
                            return;
                        }
                    }
                }
                if (cnt > 0)
                {

                    string message = " Entrée(s) supprimée(s) avec succès";
                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                  "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
                  "</script>");
                    BindGrid();
                }
                else
                {
                    string message = "Merci de faire une sélection";
                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                  "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
                  "</script>");
                }
            }
          
        }
        // 31 aug remove
      protected void btnExportExcel_Click(object sender, EventArgs e)
        {
          //  Export_Excel("exportpage");
            ExportToExcel("exportpage");
        }
        protected void btnExportExcelAll_Click(object sender, EventArgs e)
        {
           // Export_Excel("exportall");
            ExportToExcel("exportall");
        }
        protected void ExportToExcel(string exportwhat)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Leonard.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grdViewOrders.Columns[1].Visible = true;

                //To Export all pages
                if (exportwhat == "exportall")
                {
                    grdViewOrders.AllowPaging = false;

                }
                else if (exportwhat == "exportpage")
                {
                    grdViewOrders.AllowPaging = true;
                }
               // grdViewOrders.AllowPaging = false;
                this.BindGrid();
              
                grdViewOrders.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grdViewOrders.HeaderRow.Cells)
                {
                    cell.BackColor = grdViewOrders.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdViewOrders.Rows)
                {
                    
                    ImageButton lnkvalidate = (ImageButton)row.FindControl("img1");
                    string ggg = lnkvalidate.CommandArgument;
                    row.Cells[15].Text = ggg;
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdViewOrders.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdViewOrders.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                      
                    }
                    row.Cells[1].Visible = true;
                    row.Cells[18].Visible = false;
                    row.Cells[19].Visible = false;
                }

                grdViewOrders.RenderControl(hw);

              
                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                byte[] BOM = { 0xEF, 0xBB, 0xBF };
                Response.BinaryWrite(BOM);
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
                grdViewOrders.Columns[1].Visible = false;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void Export_Excel(string exportwhat)
        {

            #region Exporting Data to Excel sheet
            string attachment = "attachment; filename=Maquettes.xls";
            Response.ClearContent();
            if (exportwhat == "exportall")
            {
                grdViewOrders.AllowPaging = false;

            }
            else if (exportwhat == "exportpage")
            {
                grdViewOrders.AllowPaging = true;
            }
            BindGrid();
           
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grdViewOrders.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            #endregion
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            /*
            SqlParameter[] sqlparam = new SqlParameter[2];
            sqlparam[0] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
            sqlparam[1] = new SqlParameter("dashboard", "RV");
            int result = DataAccess.ExecuteNonQuerySP(SPNames.updateloginfordashboard, sqlparam);
            */

            SqlParameter[] sqlparam = new SqlParameter[4];
            sqlparam[0] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
            sqlparam[1] = new SqlParameter("dashboard", "RV");
            sqlparam[2] = new SqlParameter("collections", "");


            string queryval = "";
            foreach (ListItem chk in CheckBoxList1.Items)
            {

                if (chk.Selected == true)
                {
                    if (queryval == "")
                    {
                        queryval = "'" + chk.Value + "'";
                    }
                    else
                    {
                        queryval = queryval + "," + "'" + chk.Value + "'";
                    }
                }
            }
            if (queryval == "")
            {
                sqlparam[3] = new SqlParameter("redaction", "");
            }
            else
            {
                sqlparam[3] = new SqlParameter("redaction", queryval);
            }
          
            /*
            if (ddlreview.SelectedItem.Text == "----------")
            {
                sqlparam[3] = new SqlParameter("redaction", "");
            }
            else
            {
                sqlparam[3] = new SqlParameter("redaction", ddlreview.SelectedValue.ToString());
            }
            */
            int result = DataAccess.ExecuteNonQuerySP(SPNames.updateloginfordashboard_New, sqlparam);
            Session[SESSION.LOGGED_USER] = null;
            Response.Redirect("Default.aspx");

        }
        protected void grdViewOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnk = (LinkButton)row.FindControl("lnkDownload");
                string filename = lnk.Text;
                string id = lnk.CommandArgument;
                string Expath = ConfigurationSettings.AppSettings["UploadFilePath"];
                if (filename != "")
                {
                    string filepath = Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN\\" + id + "\\" + filename;
                    string path = filepath;//Server.MapPath();
                    if (File.Exists(path))
                    {
                        byte[] bts = System.IO.File.ReadAllBytes(path);
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.AddHeader("Content-Type", "Application/octet-stream");
                        Response.AddHeader("Content-Length", bts.Length.ToString());

                        Response.AddHeader("Content-Disposition", "attachment;   filename=\"" + filename +"\"");

                        Response.BinaryWrite(bts);

                        Response.Flush();

                        Response.End();
                    }
                    else
                    {
                        string message = "file not found";
                        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                          "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
                          "</script>");
                    }
                }
            }
            if (e.CommandName == "TDDownload")
            {

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnk = (LinkButton)row.FindControl("lnkTDDownload");
                string filename = lnk.Text;
                string id = lnk.CommandArgument;
                string Expath = ConfigurationSettings.AppSettings["UploadFilePath"];
                if (filename != "")
                {
                    string filepath = Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD\\" + id + "\\" + filename;
                    string path = filepath;//Server.MapPath();
                    if (File.Exists(path))
                    {
                        byte[] bts = System.IO.File.ReadAllBytes(path);
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.AddHeader("Content-Type", "Application/octet-stream");
                        Response.AddHeader("Content-Length", bts.Length.ToString());

                        Response.AddHeader("Content-Disposition", "attachment;   filename=\"" + filename +"\"");

                        Response.BinaryWrite(bts);

                        Response.Flush();

                        Response.End();
                    }
                    else
                    {
                        string message = "file not found";
                        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                          "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
                          "</script>");
                    }
                }
            }
            if (e.CommandName == "comment")
            {
                string ttttt = e.CommandArgument.ToString().Replace("'", "\'").Replace("\r\n","\\n");//.Replace("\r\n", "")
                int sop = ttttt.IndexOf("'", 0);
                while (sop != -1)
                {
                    ttttt = ttttt.Insert(sop , "\\");
                    sop = ttttt.IndexOf("'", sop+3);
                }
                
               // Response.Write(@"<script language='javascript'>alert('"+e.CommandArgument.ToString()+"')</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + ttttt + "' );", true);
            }
            /*
            if (e.CommandName == "rework")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnk = (LinkButton)row.FindControl("lnkcancel");
                string filename = lnk.Text;
                string id = lnk.CommandArgument;


                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
               "window.open('EncyclopediaReviseEntry.aspx?EID=" + id + "','','width=800,height=700,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
               "</script>");
            }

            
            if (e.CommandName == "Validate")
            {


                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnk = (LinkButton)row.FindControl("lnkvalidate");
                string filename = lnk.Text;
                string id = lnk.CommandArgument;
                       
                            string liCalendarId = (row.FindControl("litID") as Literal).Text;

                            SqlParameter[] paramlist = new SqlParameter[2];
                            paramlist[0] = new SqlParameter("eid", id);
                            paramlist[1] = new SqlParameter("stage", "Article Completed");
                            int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.AllocteEncycloItem, paramlist);
                           
                            if (DataAccess.isDBConnectionFail == true)
                            {
                                //Errlbl.Visible = true;
                                return;
                            }
                        
                  
               


               // BindGrid();
            }
            */

            if (e.CommandName == "Upload")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnk = (LinkButton)row.FindControl("lnktdupload");
                string filename = lnk.Text;
                string id = lnk.CommandArgument;


                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
               "window.open('TDUploadFileJournal.aspx?ARTICLERID=" + id + "','','width=800,height=700,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
               "</script>");
            }
        }

        protected void deleteFile(object sender, EventArgs e)
        {
            #region Expiring Advert Details through Business Layer
            int cnt = 0;
            bool status = false;

            LinkButton lnkdelete = (LinkButton)sender;

            string id = lnkdelete.CommandArgument;

          
              
                   

                    SqlParameter[] paramlist = new SqlParameter[2];
                    paramlist[0] = new SqlParameter("articleid", id);
                    paramlist[1] = new SqlParameter("InDate", Common.GetDayLightTime());
                    int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.removeJournalItem, paramlist);
                    if (rowresult > 0)
                    {
                       
                        //Success mail
                        SqlParameter[] paramlist1 = new SqlParameter[1];
                        paramlist1[0] = new SqlParameter("articleid", id);
                        DataSet set = DataAccess.ExecuteDataSetSP(SPNames.getemaildatajournal, paramlist1);
                        if (set.Tables[0].Rows.Count > 0)
                        {

                            string strTitle = set.Tables[0].Rows[0]["ArticleTitle"].ToString();
                            string strDuedate = set.Tables[0].Rows[0]["DUEDATE"].ToString();
                            string strComments = set.Tables[0].Rows[0]["COMMENTS"].ToString();

                            string strTo = Session[SESSION.LOGGED_USER].ToString();// System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                            string strLT = Session[SESSION.LOGGED_ROLE].ToString();

                            //    string strStage = "Article in process";

                            string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                            string strCC = set.Tables[0].Rows[0]["Email"].ToString();
                            // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                            // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                            string strFile = Server.MapPath("App_Data\\MAILS\\LN_DELETE\\") + "J.html";

                            if (File.Exists(strFile))
                            {
                                StreamReader sr = new StreamReader(strFile);
                                string FileC = sr.ReadToEnd();
                                sr.Close();
                                string strBody = FileC;
                                strBody = strBody.Replace("[ILT]", strTitle);
                                strBody = strBody.Replace("[DTAD]", strDuedate);
                                if (strComments == "")
                                {
                                    strBody = strBody.Replace("[IACE]", "aucun commentaire");
                                }
                                else
                                {
                                    strBody = strBody.Replace("[IACE]", strComments);
                                }
                                strBody = strBody.Replace("[IHT]", strLink);

                                string strSubject = "Léonard – Intervention extérieure annulée pour le document : « " + strTitle + " »";
                                Common cmn = new Common();
                               Common.SendEmail(strTo, strCC, strSubject, strBody);
                                // Utility.NumberToEnglish.email();
                            }

                        }

                    }
                    if (DataAccess.isDBConnectionFail == true)
                    {
                        //  Errlbl.Visible = true;
                        return;
                    }
                
           
         
            BindGrid();

            #endregion
        }
        protected void ValidateFile(object sender, EventArgs e)
        {
            #region Expiring Advert Details through Business Layer
            LinkButton lnkValidate = (LinkButton)sender;
           
            string id = lnkValidate.CommandArgument;
            SqlParameter[] paramlist = new SqlParameter[3];
            paramlist[0] = new SqlParameter("articleid", id);
            paramlist[1] = new SqlParameter("stage", "Archivé");
            paramlist[2] = new SqlParameter("InDate", Common.GetDayLightTime());
            int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.completejournalItem, paramlist);

            if (DataAccess.isDBConnectionFail == true)
            {
                //Errlbl.Visible = true;
                return;
            }
            BindGrid();      
                  
            #endregion
        }
        protected void CancelFile(object sender, EventArgs e)
        {
            LinkButton lnkCancel = (LinkButton)sender;
        
            string id = lnkCancel.CommandArgument;


            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
           "window.open('JournalReviseEntry1.aspx?ARTICLEID=" + id + "','','width=800,height=700,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
           "</script>");

            BindGrid();
        }
        protected void UpdateFile(object sender, EventArgs e)
        {
            LinkButton lnkCancel = (LinkButton)sender;

            string id = lnkCancel.CommandArgument;


            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
           "window.open('EncyclopediaUpdate.aspx?EID=" + id + "','','width=800,height=700,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
           "</script>");

            BindGrid();
        }

        protected void grdViewOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow i in grdViewOrders.Rows)
            {
                // raushan 15 dec 16
                /*
                //Retrieve the state of the CheckBox
                CheckBox cb1 = (CheckBox)i.FindControl("chk");
                Literal lituserid = (Literal)i.FindControl("userid");
                if (Session[SESSION.LOGGED_ROLE].ToString() == "LN")
                {
                    // string stage = e.Row.Cells[8].Text;
                    if (cb1.ToolTip == "En préparation" && lituserid.Text == Session[SESSION.LOGGED_USER].ToString()) // En attente prod //!=|| cb1.ToolTip.ToUpper() == "ARTICLE SUBMITTED BY TD"
                    {
                        cb1.Enabled = true;
                      //  LinkButton lnkedit = (LinkButton)i.FindControl("lnkedit");
                     //   lnkedit.Visible = true;
                    }
                    else
                    {
                        cb1.Enabled = false;
                    }
                    if ((cb1.ToolTip == "Contenu préparé" || cb1.ToolTip == "Contenu corrigé") && lituserid.Text == Session[SESSION.LOGGED_USER].ToString())
                    {
                        LinkButton lnkvalidate = (LinkButton)i.FindControl("lnkvalidate");
                        lnkvalidate.Visible = true;
                        LinkButton lnkcancel = (LinkButton)i.FindControl("lnkcancel");
                        lnkcancel.Visible = true;
                    }
                }

                */
                Literal stageid = (Literal)i.FindControl("stageid");
                Literal litID = (Literal)i.FindControl("litID1");
                Literal lituserid = (Literal)i.FindControl("userid1");
                if (Session[SESSION.LOGGED_ROLE].ToString() == "LN")
                {
                    if (stageid.Text == "En préparation" && lituserid.Text == Session[SESSION.LOGGED_USER].ToString()) // En attente prod //!=|| cb1.ToolTip.ToUpper() == "ARTICLE SUBMITTED BY TD"
                    {

                        LinkButton lnkdelete = (LinkButton)i.FindControl("lnkdelete");
                        lnkdelete.Visible = true;
                    }
                    else
                    {
                        LinkButton lnkdelete = (LinkButton)i.FindControl("lnkdelete");
                        lnkdelete.Visible = false;
                    }
                    if ((stageid.Text == "Contenu préparé"|| stageid.Text == "Contenu corrigé") && lituserid.Text == Session[SESSION.LOGGED_USER].ToString())
                    {
                        LinkButton lnkvalidate = (LinkButton)i.FindControl("lnkvalidate");
                        lnkvalidate.Visible = true;
                        LinkButton lnkcancel = (LinkButton)i.FindControl("lnkcancel");
                        lnkcancel.Visible = true;
                    }
                }


                if (Session[SESSION.LOGGED_ROLE].ToString() == "TDM" || Session[SESSION.LOGGED_ROLE].ToString() == "TDN")
                {
                    if (stageid.Text == "En préparation" || stageid.Text == "En correction")//!=
                    {
                        LinkButton lnkvalidate = (LinkButton)i.FindControl("lnktdupload");
                        lnkvalidate.Visible = true;
                    }
                    else
                    {
                        LinkButton lnkvalidate = (LinkButton)i.FindControl("lnktdupload");
                        lnkvalidate.Visible = false;
                    }
                   

                }
                ImageButton img = (ImageButton)i.FindControl("img1");
                if (img.CommandArgument != "" && img.CommandArgument != null)
                {
                    img.Visible = true;
                }
                else
                {
                    img.Visible = false;
                }

            }
        }
        //protected void lnktdupload_Click(object sender, EventArgs e)
        //{
        //    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
        //       "window.open('TDUploadFile.aspx?EID=" + id + "','','width=750,height=800,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
        //       "</script>");
        //}

        // 31 aug remove
      protected void ddltask_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddltask.SelectedItem.Text == "Déclarer un article")
            {
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                "window.open('JournalEntry.aspx','','width=750,height=800,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
                "</script>");
            }
           
        }



      protected void ddlreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strQuery = "";
            if (ddlreview.SelectedItem.Text != "----------")
            {
                // strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and JID='" + ddlreview.SelectedValue.ToString() + "' and a.Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid and JID='" + ddlreview.SelectedValue.ToString() + "' and a.Active<> 'No'";
               
            }
            else if (ddlreview.SelectedItem.Text == "----------")
            {
                // strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid  and a.Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid and a.Active<> 'No'";
            }
           

            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();
            hndSelectedValue.Value = "";

            OrderDAOJournal objProductDAO = new OrderDAOJournal();

            IList<OrderViewJournal> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 5; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
        }
       
       
        protected void btnLookfor_Click(object sender, EventArgs e)
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
               "window.open('JournalSearch.aspx','','width=1035,height=650,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
               "</script>");
        }
        protected void btnremovefilter_Click(object sender, EventArgs e)
        {

            //string strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.Active <> 'No' order by Articleid";
            string strQuery = "select a.*,l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid and a.Active <> 'No' order by Articleid";
            hndSelectedValue.Value = "";

            OrderDAOJournal objProductDAO = new OrderDAOJournal();

            IList<OrderViewJournal> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();
            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 5; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }

            foreach (ListItem chk in CheckBoxList1.Items)
            {

                if (chk.Selected == true)
                {
                    chk.Selected = false;
                }
            }

            // // 31 aug remove
            ddltask.SelectedIndex = -1;
            ddlreview.SelectedIndex = -1;
           
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProduct.SelectedValue == "EC")
            {
                Session[SESSION.LOGGED_PRODSITE] = "EC";

                Response.Redirect("EncyclopediasLanding.aspx");
         
            }
            else if (ddlProduct.SelectedValue == "DS")
            {
                Session[SESSION.LOGGED_PRODSITE] = "DS";
                Response.Redirect("DossierLanding1.aspx");
            }
            else if (ddlProduct.SelectedValue == "FS")
            {
                Session[SESSION.LOGGED_PRODSITE] = "FS";
                Response.Redirect("FicheLanding.aspx");

                /*
                string message = "Développement en cours";
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                  "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
                  "</script>");
                */
            }
            else if (ddlProduct.SelectedValue == "RV")
            {
                Session[SESSION.LOGGED_PRODSITE] = "RV";
                Response.Redirect("JournalLanding.aspx");
            }
        }


        [WebMethod(EnableSession = true)]
        public static string RegisterUser(string userid, string password, string newpassword)
        {

            //int uid = Convert.ToInt16(userid);
            SqlParameter[] paramList = new SqlParameter[3];
            paramList[0] = new SqlParameter("uiid", userid);// SqlDbType.VarChar, 50);//, userid
            //paramList[0].Value = userid;
            paramList[1] = new SqlParameter("oldpass", password);// SqlDbType.VarChar, 50);//, password
            //paramList[1].Value = password;
            paramList[2] = new SqlParameter("newpass", newpassword);// SqlDbType.VarChar, 50);//, newpassword
            //paramList[2].Value = newpassword;
            int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.changepassword, paramList);

            if (rowAffected > 0)
            {
                return "Mot de passe modifié";
            }
            else
            {
                return "ancien mot de passe incorrect";
            }



        }
        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string queryval = "";
            foreach (ListItem chk in CheckBoxList1.Items)
            {

                if (chk.Selected == true)
                {
                    if (queryval == "")
                    {
                        queryval = "'"+chk.Value+"'";
                    }
                    else
                    {
                        queryval = queryval + "," + "'"+chk.Value+"'";
                    }
                }
            }
            string strQuery = "";

            if (queryval != "")
            {
                //strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and JID in(" + queryval + ") and a.Active<> 'No' order by Articleid";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid and JID in(" + queryval + ") and a.Active<> 'No' order by Articleid";
            }
            else
            {
                //strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and  a.Active<> 'No' order by Articleid";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname,j.journal_Name from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(iteration) as iteration from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.iteration = b.iteration WHERE  a.userid=l.userid and a.jid=j.jid and  a.Active<> 'No' order by Articleid";
            }
               
           
           


            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();
            hndSelectedValue.Value = "";

            OrderDAOJournal objProductDAO = new OrderDAOJournal();

            IList<OrderViewJournal> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 5; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
}
}