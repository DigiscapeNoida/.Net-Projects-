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
using System.Resources;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Web.Services;
using System.Drawing;

namespace LexisNexis
{
    public partial class DossierLanding1 : System.Web.UI.Page
    {
     
        public static string _sortDirection;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SESSION.LOGGED_USER] != null)
            {
                hidDivId.Value=Session[SESSION.LOGGED_USER].ToString();

                if (Session[SESSION.LOGGED_ROLE].ToString() == "LN")
                {
                   // btnsendprod.Visible = true;
                  // 15 dec 
                   // btnremove.Visible = true;
                }
                if (Session[SESSION.LOGGED_ROLE].ToString() == "TDM" || Session[SESSION.LOGGED_ROLE].ToString() == "TDN")
                {
                    btnComplete.Visible = true;
                    ddltask.Visible = false;
                    lblTask.Visible = false;
                }
                lblname.Text = Session[SESSION.LOGGED_USER_NAME].ToString();
                if (!Page.IsPostBack)
                {
                    bindInputtype();
                    if (Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString() == "")
                    {
                        if (Session["searchqry"] == null)
                        {
                            BindGrid();
                        }
                    }
                    else
                    {
                        BindGridMemory(Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString());
                        ddlwriting.Items.FindByText(Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString().Trim()).Selected = true;

                    }
                    // Session["ppp"] = null;
                   // BindGrid();

                  
                    // for memory
                   
                    bindproduct();
                    bindtask();
                    binddec();

                }


            }
        }
      


        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            if (Session[SESSION.LOGGED_USER] != null)
            {
                if (Session["searchqry"] != null)
                {
                    BindSearch();
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
            }
        }

      

        private void BindSearch()
        {
            string strQuery = Session["searchqry"].ToString();
            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();

            OrderDAONews objProductDAO = new OrderDAONews();

            IList<OrderViewNews> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewNews());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 4; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
            else
            {
                btnsendprod.Visible = false;
                btnremove.Visible = false;
            }
          //  Session["searchqry"] = null;
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
       
        private void bindInputtype()
        {
            ddlwriting.Items.Clear();
            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getinputcategory, paramlist);
            ddlwriting.DataSource = ds;
            ddlwriting.DataTextField = "catdesc";
            ddlwriting.DataValueField = "catid";
            ddlwriting.DataBind();
            ddlwriting.Items.Insert(0, new ListItem("----------", "-1"));
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
          //  ddlProduct.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        private void binddec()
        {
            ddlCollection.Items.Clear();
            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.Declination, paramlist);
            ddlCollection.DataSource = ds;
            ddlCollection.DataTextField = "decdesc";
            ddlCollection.DataValueField = "decid";
            ddlCollection.DataBind();
            ddlCollection.Items.Insert(0, new ListItem("----------", "-1"));
            ddlCollection.Items.Insert(1, new ListItem("Tous les thèmes", "Tous les thèmes"));
        }
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
        private void BindGridMemory(string redaction)
        {
            //  string strQuery = "SELECT DID,DECLINATION,INDATE,DUEDATE,CTITLE,DEMANDTYPE,DURATION,ITERATION,PAGECOUNT,STAGE,filename,remarks,userid FROM NEWS";
            string strQuery = "select n.*,l.firstname+' '+l.lastname as fullname from news n , login l WHERE n.userid=l.userid AND category='"+redaction+"'";
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
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND n.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "fullname")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "l.firstname+' '+l.lastname" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND n.Active<> 'No'";
                        }
                        else
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue) + "AND n.Active<> 'No'";
                        }
                    }
                }
            }
            else
            {
                //strQuery = strQuery + " where Active<> 'No' ";
                strQuery = strQuery + " and n.Active<> 'No' ";
            }

            OrderDAONews objProductDAO = new OrderDAONews();

            IList<OrderViewNews> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewNews());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 4; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
            else
            {
                btnsendprod.Visible = false;
                btnremove.Visible = false;
            }
            Session["searchqry"] = null;
        }
        private void BindGrid()
        {
            string strQuery = "";
             if (ddlwriting.SelectedItem.Text != "----------")
            {
               
                strQuery = " select n.*,l.firstname+' '+l.lastname as fullname from news n , login l WHERE n.userid=l.userid and n.category='" + ddlwriting.SelectedItem.Text + "' ";
            }
			else
            {
               // strQuery = "SELECT * FROM NEWS where  Active<> 'No'";
                strQuery = " select n.*,l.firstname+' '+l.lastname as fullname from news n , login l WHERE n.userid=l.userid";
            }


         
         //   string strQuery = "select n.*,l.firstname+' '+l.lastname as fullname from news n , login l WHERE n.userid=l.userid";
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
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") +"cast ("+ selectedData.ColumnName +" as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND n.Active<> 'No'";
                        }
                            else if(selectedData.ColumnName=="fullname")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "l.firstname+' '+l.lastname"  + " IN " + ArrayToString(selectedData.SelectedValue) + "AND n.Active<> 'No'";
                        }
                        else
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue) + "AND n.Active<> 'No'";
                        }
                    }
                }
            }
            else
            {
                //strQuery = strQuery + " where Active<> 'No' ";
                strQuery = strQuery + " and n.Active<> 'No' ";
            }

            OrderDAONews objProductDAO = new OrderDAONews();

            IList<OrderViewNews> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewNews());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 4; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
            else
            {
                btnsendprod.Visible = false;
                btnremove.Visible = false;
            }
            Session["searchqry"] = null;
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void grdViewOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dataTable = new DataTable();
            OrderDAO objProductDAO = new OrderDAO();
            string strQuery = "";
            if (Session["searchqry"] != null)
            {

                strQuery = Session["searchqry"].ToString();
            }
            else
            {
                if (ddlwriting.SelectedItem.Text != "----------")
                {

                    strQuery = "select n.did,n.declination,n.ctitle,n.demandtype,n.duration,n.iteration," +
                "CONVERT(VARCHAR(10), n.Indate, 103) + ' '  + convert(VARCHAR(8), n.Indate, 14) as indate," +
                "CONVERT(VARCHAR(10), n.duedate, 103) + ' '  + convert(VARCHAR(8), n.duedate, 14) as duedate," +
                "CONVERT(VARCHAR(10), n.allocation_date, 103) + ' '  + convert(VARCHAR(8), n.allocation_date, 14) as allocation_date," +
                "CONVERT(VARCHAR(10), n.revised_duedate, 103) + ' '  + convert(VARCHAR(8), n.revised_duedate, 14) as revised_duedate," +
                "CONVERT(VARCHAR(10), n.delivered_date, 103) + ' '  + convert(VARCHAR(8), n.delivered_date, 14) as delivered_date," +
                "n.pagecount,n.stage,n.remarks,n.author,n.authormail,n.filename,n.category,n.wordcount,n.userid," +
                "CONVERT(VARCHAR(10), n.publicationdate, 103) + ' '  + convert(VARCHAR(8), n.publicationdate, 14) as publicationdate," +
                "CONVERT(VARCHAR(10), n.deletiondate, 103) + ' '  + convert(VARCHAR(8), n.deletiondate, 14) as deletiondate" +
                ",l.firstname+' '+l.lastname as fullname from news n , login l WHERE n.userid=l.userid and n.category='" + ddlwriting.SelectedItem.Text + "' ";
                }
                else
                {

                    strQuery = "select n.did,n.declination,n.ctitle,n.demandtype,n.duration,n.iteration," +
                "CONVERT(VARCHAR(10), n.Indate, 103) + ' '  + convert(VARCHAR(8), n.Indate, 14) as indate," +
                "CONVERT(VARCHAR(10), n.duedate, 103) + ' '  + convert(VARCHAR(8), n.duedate, 14) as duedate," +
                "CONVERT(VARCHAR(10), n.allocation_date, 103) + ' '  + convert(VARCHAR(8), n.allocation_date, 14) as allocation_date," +
                "CONVERT(VARCHAR(10), n.revised_duedate, 103) + ' '  + convert(VARCHAR(8), n.revised_duedate, 14) as revised_duedate," +
                "CONVERT(VARCHAR(10), n.delivered_date, 103) + ' '  + convert(VARCHAR(8), n.delivered_date, 14) as delivered_date," +
                "n.pagecount,n.stage,n.remarks,n.author,n.authormail,n.filename,n.category,n.wordcount,n.userid," +
                "CONVERT(VARCHAR(10), n.publicationdate, 103) + ' '  + convert(VARCHAR(8), n.publicationdate, 14) as publicationdate," +
                "CONVERT(VARCHAR(10), n.deletiondate, 103) + ' '  + convert(VARCHAR(8), n.deletiondate, 14) as deletiondate" +
                ",l.firstname+' '+l.lastname as fullname from news n , login l WHERE n.userid=l.userid";
                }

            }
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
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND n.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "fullname")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "l.firstname+' '+l.lastname" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND n.Active<> 'No'";
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
                strQuery = strQuery + " and n.Active<> 'No' ";
            }
            bool datsts = false;
            if (GridViewSortExpression != string.Empty)
            {
                if (GridViewSortExpression.ToString() == "INDATE" || GridViewSortExpression.ToString() == "DUEDATE" || GridViewSortExpression.ToString() == "Delivered_Date")
                {
                    string sort = ViewState["SortDireaction"].ToString();// GetSortDirection();
                    strQuery = strQuery + " Order by " + "n." + GridViewSortExpression.ToString() + " " + sort;
                    datsts = true;
                }
                else
                {
                   // strQuery = strQuery + " order by Articleid";
                }
            }
            else
            {
               // strQuery = strQuery + " order by Articleid";
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
           // grdViewOrders.DataSource = SortDataTable(dataTable, true);
            grdViewOrders.PageIndex = e.NewPageIndex;
            grdViewOrders.DataBind();

            for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 4; intRowIndex++)
            {
                TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                tblCell.CssClass = "filter";
                tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
            }
            //BindGrid();
        }

        protected void grdViewOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = new DataTable();
            OrderDAO objProductDAO = new OrderDAO();
            string strQuery = "";
            if (Session["searchqry"] != null)
            {

                strQuery = Session["searchqry"].ToString();
            }
            else
            {
                if (ddlwriting.SelectedItem.Text != "----------")
                {

                    strQuery = "select n.did,n.declination,n.ctitle,n.demandtype,n.duration,n.iteration," +
                "CONVERT(VARCHAR(10), n.Indate, 103) + ' '  + convert(VARCHAR(8), n.Indate, 14) as indate," +
                "CONVERT(VARCHAR(10), n.duedate, 103) + ' '  + convert(VARCHAR(8), n.duedate, 14) as duedate," +
                "CONVERT(VARCHAR(10), n.allocation_date, 103) + ' '  + convert(VARCHAR(8), n.allocation_date, 14) as allocation_date," +
                "CONVERT(VARCHAR(10), n.revised_duedate, 103) + ' '  + convert(VARCHAR(8), n.revised_duedate, 14) as revised_duedate," +
                "CONVERT(VARCHAR(10), n.delivered_date, 103) + ' '  + convert(VARCHAR(8), n.delivered_date, 14) as delivered_date," +
                "n.pagecount,n.stage,n.remarks,n.author,n.authormail,n.filename,n.category,n.wordcount,n.userid," +
                "CONVERT(VARCHAR(10), n.publicationdate, 103) + ' '  + convert(VARCHAR(8), n.publicationdate, 14) as publicationdate," +
                "CONVERT(VARCHAR(10), n.deletiondate, 103) + ' '  + convert(VARCHAR(8), n.deletiondate, 14) as deletiondate" +
                ",l.firstname+' '+l.lastname as fullname from news n , login l WHERE n.userid=l.userid and n.category='" + ddlwriting.SelectedItem.Text + "' ";
                }
                else
                {

                    strQuery = "select n.did,n.declination,n.ctitle,n.demandtype,n.duration,n.iteration," +
                "CONVERT(VARCHAR(10), n.Indate, 103) + ' '  + convert(VARCHAR(8), n.Indate, 14) as indate," +
                "CONVERT(VARCHAR(10), n.duedate, 103) + ' '  + convert(VARCHAR(8), n.duedate, 14) as duedate," +
                "CONVERT(VARCHAR(10), n.allocation_date, 103) + ' '  + convert(VARCHAR(8), n.allocation_date, 14) as allocation_date," +
                "CONVERT(VARCHAR(10), n.revised_duedate, 103) + ' '  + convert(VARCHAR(8), n.revised_duedate, 14) as revised_duedate," +
                "CONVERT(VARCHAR(10), n.delivered_date, 103) + ' '  + convert(VARCHAR(8), n.delivered_date, 14) as delivered_date," +
                "n.pagecount,n.stage,n.remarks,n.author,n.authormail,n.filename,n.category,n.wordcount,n.userid," +
                "CONVERT(VARCHAR(10), n.publicationdate, 103) + ' '  + convert(VARCHAR(8), n.publicationdate, 14) as publicationdate," +
                "CONVERT(VARCHAR(10), n.deletiondate, 103) + ' '  + convert(VARCHAR(8), n.deletiondate, 14) as deletiondate" +
                ",l.firstname+' '+l.lastname as fullname from news n , login l WHERE n.userid=l.userid";
                }
            }
       
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
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND n.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "fullname")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "l.firstname+' '+l.lastname" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND n.Active<> 'No'";
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
                strQuery = strQuery + " and n.Active<> 'No' ";
            }
            bool datsts = false;
            if (e.SortExpression.ToString() == "INDATE" || e.SortExpression.ToString() == "DUEDATE" || e.SortExpression.ToString() == "Delivered_Date")
            {
                string sort = GetSortDirection();
                strQuery = strQuery + " Order by " + "n." + e.SortExpression.ToString() + " " + sort;
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
            SetSortDirection(SortDireaction);
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
                //grdViewOrders.DataSource = SortDataTable(dataTable, false);
                grdViewOrders.DataBind();
                grdViewOrders.PageIndex = iPageIndex;
                /*
                //Sort the data.
                dataTable.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                grdViewOrders.DataSource = dataTable;
                grdViewOrders.DataBind();
                SortDireaction = _sortDirection;
                 */
            }

            for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 4; intRowIndex++)
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
                        myDataView.Sort = string.Format("{0} {1}",
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
                        myDataView.Sort = string.Format("{0} {1}",
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddltask.SelectedItem.Text == "Loguer un dossier")
            {
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                "window.open('DossierEntry.aspx','','width=750,height=700,top=50,left=100,scrollbars=yes');" + System.Environment.NewLine +
                "</script>");
            }
            //else if (cmbtask.SelectedItem.Value == "Login new advert")
            //{
            //    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            //    "window.open('../Integration/Advert_Integration.aspx','','width=800,height=700,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
            //    "</script>");
            //}
        }


        protected void btnsendprod_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (GridViewRow row in grdViewOrders.Rows)
            {
                CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
                if (chkCalendarId.Checked)
                {
                    
                    string liCalendarId = (row.FindControl("litID") as Literal).Text;
                    
                    SqlParameter[] paramlist = new SqlParameter[3];
                    paramlist[0] = new SqlParameter("did", liCalendarId);
                    paramlist[1] = new SqlParameter("stage", "Dossier envoyé à Thomson");
                    paramlist[2] = new SqlParameter("InDate", Common.GetDayLightTime());
                    int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.AllocteDossierItem, paramlist);
                    if (rowresult > 0)
                    {
                        cnt++;

                        //Success mail
                        SqlParameter[] paramlist1 = new SqlParameter[1];
                        paramlist1[0] = new SqlParameter("did", liCalendarId);
                        DataSet set = DataAccess.ExecuteDataSetSP(SPNames.getemaildata, paramlist1);
                        if (set.Tables[0].Rows.Count > 0)
                        {

                            string strTitle = set.Tables[0].Rows[0]["ctitle"].ToString();
                            string strDuedate = set.Tables[0].Rows[0]["duedate"].ToString();
                            string strComments=set.Tables[0].Rows[0]["remarks"].ToString();

                            string strTo = Session[SESSION.LOGGED_USER].ToString(); //System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                           // string strLT = Session[SESSION.LOGGED_ROLE].ToString();
                           
                            //    string strStage = "Article in process";

                            string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                            string strCC = set.Tables[0].Rows[0]["authormail"].ToString();// System.Configuration.ConfigurationSettings.AppSettings["strCC"];
                           // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                            // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                            string strFile =Server.MapPath("App_Data\\MAILS\\LN_SEND_NEW_JOB\\")+"D.html";

                            if (File.Exists(strFile))
                            {
                                StreamReader sr = new StreamReader(strFile);
                                string FileC = sr.ReadToEnd();
                                sr.Close();
                                string strBody = FileC;
                                strBody = strBody.Replace("[ILT]", strTitle);
                                strBody = strBody.Replace("[DTAD]", strDuedate);
                                strBody = strBody.Replace("[IAC]", strComments);
                                strBody = strBody.Replace("[IHA]", strLink);

                                string strSubject = "Léonard – Demande d’intervention sur le document : \"" + strTitle + "\"";
                                Common cmn = new Common();
                                Common.SendEmail(strTo, strCC, strSubject, strBody);
                                // Utility.NumberToEnglish.email();
                            }

                        }



                    }
                    if (DataAccess.isDBConnectionFail == true)
                    {
                        
                        //Fail Mail
                        //Errlbl.Visible = true;
                        return;
                    }
                    else
                    { 
                        


                    }
                }
            }
            if (cnt > 0)
            {
                string message = "Fichier(s) envoyé(s) en prod avec succès";
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

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (GridViewRow row in grdViewOrders.Rows)
            {
                CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
                if (chkCalendarId.Checked)
                {

                    string liCalendarId = (row.FindControl("litID") as Literal).Text;

                    SqlParameter[] paramlist = new SqlParameter[3];
                    paramlist[0] = new SqlParameter("did", liCalendarId);
                    paramlist[1] = new SqlParameter("stage", "Livré sur Back Office");
                    paramlist[2] = new SqlParameter("InDate", Common.GetDayLightTime());
                    int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.AllocteDossierItem, paramlist);
                    if (rowresult > 0)
                    {
                        cnt++;

                        //Success mail
                        SqlParameter[] paramlist1 = new SqlParameter[1];
                        paramlist1[0] = new SqlParameter("did", liCalendarId);
                        DataSet set = DataAccess.ExecuteDataSetSP(SPNames.getemaildata, paramlist1);
                        if (set.Tables[0].Rows.Count > 0)
                        {

                            string strTitle = set.Tables[0].Rows[0]["ctitle"].ToString();
                            string strDuedate = set.Tables[0].Rows[0]["duedate"].ToString();
                            string strComments = set.Tables[0].Rows[0]["TD_Remarks"].ToString();

                            string strTo = set.Tables[0].Rows[0]["userid"].ToString();  //Session[SESSION.LOGGED_USER].ToString();// System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                           // string strLT = Session[SESSION.LOGGED_ROLE].ToString();

                            //    string strStage = "Article in process";

                            string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                            string strCC = set.Tables[0].Rows[0]["authormail"].ToString();// System.Configuration.ConfigurationSettings.AppSettings["strCC"];
                            // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                            // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                            string strFile = Server.MapPath("App_Data\\MAILS\\THOMSON_TASK_COMPLETE\\") + "D.html";

                            if (File.Exists(strFile))
                            {
                                StreamReader sr = new StreamReader(strFile);
                                string FileC = sr.ReadToEnd();
                                sr.Close();
                                string strBody = FileC;
                                strBody = strBody.Replace("[ILID]", strTitle);// liCalendarId
                               // strBody = strBody.Replace("[DAT]", strDuedate);
                                if (strComments == "")
                                {
                                    strBody = strBody.Replace("[IACE]", "aucun commentaire");  
                                }
                                else
                                {
                                    strBody = strBody.Replace("[IACE]", strComments);
                                }
                                strBody = strBody.Replace("[IHT]", strLink);

                                string strSubject = "Léonard – Intervention extérieure terminée pour le document : « " + strTitle + " »"; 

                                Common cmn = new Common();
                                Common.SendEmail(strTo, strCC, strSubject, strBody);
                                // Utility.NumberToEnglish.email();
                            }

                        }

                    }
                    if (DataAccess.isDBConnectionFail == true)
                    {
                        //Errlbl.Visible = true;
                        return;
                    }
                }
            }
            if (cnt > 0)
            {
                string message = "Item Achevée";
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
              "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
              "</script>");
            }
            else
            {
                string message = "Merci de faire une sélection";
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
              "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
              "</script>");
            }


            BindGrid();
        }

        protected void deleteFile(object sender, EventArgs e)
        {
            LinkButton lnkdelete = (LinkButton)sender;

            string id = lnkdelete.CommandArgument;


            SqlParameter[] paramlist = new SqlParameter[2];
            paramlist[0] = new SqlParameter("did", id);
            paramlist[1] = new SqlParameter("InDate", Common.GetDayLightTime());
            int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.removeDossierItem, paramlist);
            if (rowresult > 0)
            {
               
                //Success mail
                SqlParameter[] paramlist1 = new SqlParameter[1];
                paramlist1[0] = new SqlParameter("did", id);
                DataSet set = DataAccess.ExecuteDataSetSP(SPNames.getemaildata, paramlist1);
                if (set.Tables[0].Rows.Count > 0)
                {

                    string strTitle = set.Tables[0].Rows[0]["ctitle"].ToString();
                    string strDuedate = set.Tables[0].Rows[0]["duedate"].ToString();
                    string strComments = set.Tables[0].Rows[0]["remarks"].ToString();

                    string strTo = System.Configuration.ConfigurationSettings.AppSettings["strThomsonTo"];//Session[SESSION.LOGGED_USER].ToString();//
                    //  string strLT = Session[SESSION.LOGGED_ROLE].ToString();

                    //    string strStage = "Article in process";

                    string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                    string strCC = System.Configuration.ConfigurationSettings.AppSettings["strThomsonCC"];// set.Tables[0].Rows[0]["authormail"].ToString(); //
                    // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                    // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                    string strFile = Server.MapPath("App_Data\\MAILS\\LN_DELETE\\") + "D.html";

                    if (File.Exists(strFile))
                    {
                        StreamReader sr = new StreamReader(strFile);
                        string FileC = sr.ReadToEnd();
                        sr.Close();
                        string strBody = FileC;
                        strBody = strBody.Replace("[ILT]", strTitle);
                        strBody = strBody.Replace("[DAT]", strDuedate);
                        if (strComments == "")
                        {
                            strBody = strBody.Replace("[IACE]", "aucun commentaire");
                        }
                        else
                        {
                            strBody = strBody.Replace("[IACE]", strComments);
                        }
                        strBody = strBody.Replace("[IHT]", strLink);

                        string strSubject = "Léonard – Intervention extérieure annulée pour le document   : « " + strTitle + " »";

                        Common cmn = new Common();
                        Common.SendEmail(strTo, strCC, strSubject, strBody);
                        // Utility.NumberToEnglish.email();
                    }

                }

            }
            if (DataAccess.isDBConnectionFail == true)
            {
                //lblmessage.Visible = true;
                return;
            }
            BindGrid();
        }
        protected void btnremove_Click(object sender, EventArgs e)
        {
             string confirmValue = Request.Form["confirm_value"];
             if (confirmValue == "Yes")
             {

                 int cnt = 0;
                 foreach (GridViewRow row in grdViewOrders.Rows)
                 {
                     CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
                     if (chkCalendarId.Checked)
                     {

                         string liCalendarId = (row.FindControl("litID") as Literal).Text;

                         SqlParameter[] paramlist = new SqlParameter[2];
                         paramlist[0] = new SqlParameter("did", liCalendarId);
                         paramlist[1] = new SqlParameter("InDate", Common.GetDayLightTime());
                         int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.removeDossierItem, paramlist);
                         if (rowresult > 0)
                         {
                             cnt++;
                             //Success mail
                             SqlParameter[] paramlist1 = new SqlParameter[1];
                             paramlist1[0] = new SqlParameter("did", liCalendarId);
                             DataSet set = DataAccess.ExecuteDataSetSP(SPNames.getemaildata, paramlist1);
                             if (set.Tables[0].Rows.Count > 0)
                             {

                                 string strTitle = set.Tables[0].Rows[0]["ctitle"].ToString();
                                 string strDuedate = set.Tables[0].Rows[0]["duedate"].ToString();
                                 string strComments = set.Tables[0].Rows[0]["remarks"].ToString();

                                 string strTo = System.Configuration.ConfigurationSettings.AppSettings["strThomsonTo"];//Session[SESSION.LOGGED_USER].ToString();//
                               //  string strLT = Session[SESSION.LOGGED_ROLE].ToString();

                                 //    string strStage = "Article in process";

                                 string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                                 string strCC = System.Configuration.ConfigurationSettings.AppSettings["strThomsonCC"];// set.Tables[0].Rows[0]["authormail"].ToString(); //
                                 // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                                 // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                                 string strFile = Server.MapPath("App_Data\\MAILS\\LN_DELETE\\") + "D.html";

                                 if (File.Exists(strFile))
                                 {
                                     StreamReader sr = new StreamReader(strFile);
                                     string FileC = sr.ReadToEnd();
                                     sr.Close();
                                     string strBody = FileC;
                                     strBody = strBody.Replace("[ILT]", strTitle);
                                     strBody = strBody.Replace("[DAT]", strDuedate);
                                     if (strComments == "")
                                     {
                                         strBody = strBody.Replace("[IACE]", "aucun commentaire");
                                     }
                                     else
                                     {
                                         strBody = strBody.Replace("[IACE]", strComments);
                                     }
                                     strBody = strBody.Replace("[IHT]", strLink);

                                     string strSubject = "Léonard – Intervention extérieure annulée pour le document   : « " + strTitle + " »";

                                     Common cmn = new Common();
                                     Common.SendEmail(strTo, strCC, strSubject, strBody);
                                     // Utility.NumberToEnglish.email();
                                 }

                             }

                         }
                         if (DataAccess.isDBConnectionFail == true)
                         {
                             //lblmessage.Visible = true;
                             return;
                         }
                     }
                 }
                 if (cnt > 0)
                 {

                     string message = "Entrée(s) supprimée(s) avec succès";
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
          //  BindGrid();
        }
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {



          //  Export_Excel("exportpage");
            ExportToExcel("exportpage");


        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnExportExcelAll_Click(object sender, EventArgs e)
        {
          //  Export_Excel("exportall");
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
                grdViewOrders.Columns[grdViewOrders.Columns.Count - 1].Visible = false;
                grdViewOrders.Columns[grdViewOrders.Columns.Count - 2].Visible = false;
                grdViewOrders.Columns[grdViewOrders.Columns.Count - 3].Visible = false;
                grdViewOrders.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grdViewOrders.HeaderRow.Cells)
                {
                    cell.BackColor = grdViewOrders.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdViewOrders.Rows)
                {
                    ImageButton lnkvalidate = (ImageButton)row.FindControl("img1");
                    string ggg = lnkvalidate.CommandArgument;
                    row.Cells[10].Text = ggg;
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
                grdViewOrders.Columns[grdViewOrders.Columns.Count - 1].Visible = true;
                grdViewOrders.Columns[grdViewOrders.Columns.Count - 2].Visible = true;
                grdViewOrders.Columns[grdViewOrders.Columns.Count - 3].Visible = true;
            }
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
            //grdViewOrders.DataSource = Session["ppp"];
            //grdViewOrders.DataBind();
            //foreach (GridViewRow i in grdViewOrders.Rows)
            //{
            //    CheckBox cb1 = (CheckBox)i.FindControl("chk");
            //    cb1.Visible = false;
            //}

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
        
        protected void grdViewOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
             {
            //    string id = e.CommandArgument.ToString();
          
            //Response.Clear();
            //Response.ContentType = "application/octet-stream";
            //Response.AppendHeader("Content-Disposition", "filename=" + e.CommandSource.ToString());
            //Response.TransmitFile(Server.MapPath(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString()+"\\") + e.CommandArgument);
            //Response.End();
                 GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnk=(LinkButton)row.FindControl("lnkDownload");
                string filename = lnk.Text;
            string id = lnk.CommandArgument;
            string Expath = ConfigurationSettings.AppSettings["UploadFilePath"];
            if (filename != "")
            {
                string filepath = Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\" + id + "\\" + filename;
                string path = filepath;//Server.MapPath();
                byte[] bts = System.IO.File.ReadAllBytes(path);
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("Content-Type", "Application/octet-stream");
                Response.AddHeader("Content-Length", bts.Length.ToString());

                Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);

                Response.BinaryWrite(bts);

                Response.Flush();

                Response.End();
            }
            }
            if (e.CommandName == "comment")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('"+e.CommandArgument +  " ');", true);  
            }
        }
        protected void grdViewOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Session[SESSION.LOGGED_ROLE].ToString() == "LN")
            {
                grdViewOrders.Columns[16].Visible = false;//13
            }
            foreach (GridViewRow i in grdViewOrders.Rows)
            {
                //Retrieve the state of the CheckBox
                CheckBox cb1 = (CheckBox)i.FindControl("chk");
                Literal lituserid = (Literal)i.FindControl("userid");
                
                LinkButton lnk = (LinkButton)i.FindControl("lnkDownload");

                LinkButton lnkdelete = (LinkButton)i.FindControl("lnkdelete");

                if (Session[SESSION.LOGGED_ROLE].ToString() == "LN")
                {
                    if (cb1.ToolTip == "Dossier envoyé à Thomson" && lituserid.Text==Session[SESSION.LOGGED_USER].ToString())
                    {
                       // cb1.Enabled = true;
                        lnkdelete.Visible = true;
                    }
                    else
                    {
                      //  cb1.Enabled = false;
                        lnkdelete.Visible = false;
                        lnk.Enabled = false;
                    }
                    /*
                    // string stage = e.Row.Cells[8].Text;
                    if (cb1.ToolTip == "En attente prod")//!=
                    {
                        cb1.Enabled = true;
                        LinkButton lnkedit = (LinkButton)i.FindControl("lnkedit");
                        lnkedit.Visible = true;
                      
                        
                        // lnk.Enabled = false;
                    }
                    else
                    {
                        cb1.Enabled = false;
                    }
                    if (cb1.ToolTip == "En préparation" || cb1.ToolTip=="Livré sur Back Office")//!=
                    {
                        lnk.Enabled = false;
                    }
                    */

                }
                if (Session[SESSION.LOGGED_ROLE].ToString() == "TDM" || Session[SESSION.LOGGED_ROLE].ToString() == "TDN")
                {
                    if (cb1.ToolTip == "Dossier envoyé à Thomson")//!=
                    {
                       // cb1.Enabled = true;
                        cb1.Visible = true;
                    }
                    else
                    {
                        lnk.Enabled = false;
                       // cb1.Enabled = false;
                        cb1.Visible = false;
                    }
                    LinkButton lnkcomment = (LinkButton)i.FindControl("lnkcomment1111");
                    if (cb1.ToolTip == "Livré sur Back Office" || cb1.ToolTip == "Annulé")
                    {
                        lnkcomment.Visible = false;
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
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            /*
            SqlParameter[] sqlparam = new SqlParameter[2];
            sqlparam[0] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
            sqlparam[1] = new SqlParameter("dashboard", "DS");
            int result=DataAccess.ExecuteNonQuerySP(SPNames.updateloginfordashboard, sqlparam);
            */
            SqlParameter[] sqlparam = new SqlParameter[4];
            sqlparam[0] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
            sqlparam[1] = new SqlParameter("dashboard", "DS");
            sqlparam[2] = new SqlParameter("collections", "");
            if (ddlwriting.SelectedItem.Text == "----------")
            {
                sqlparam[3] = new SqlParameter("redaction", "");
            }
            else
            {
                sqlparam[3] = new SqlParameter("redaction", ddlwriting.SelectedItem.Text);
            }

            int result = DataAccess.ExecuteNonQuerySP(SPNames.updateloginfordashboard_New, sqlparam);
            

            Session[SESSION.LOGGED_USER] = null;
            Response.Redirect("Default.aspx");

        }
        protected void ddltask_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddltask.SelectedItem.Text == "Déclarer un dossier")//Loguer un dossier
            {
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
               "window.open('DossierEntry.aspx','','width=750,height=800,top=50,left=100,scrollbars=yes');" + System.Environment.NewLine +
               "</script>");
            }
            if (ddltask.SelectedItem.Text == "Envoyer en prod")
            {
                SqlParameter[] paramlist = new SqlParameter[1];
                paramlist[0] = new SqlParameter("stage", "En attente prod");

                string strQuery = "SELECT DID,DECLINATION,INDATE,DUEDATE,CTITLE,DEMANDTYPE,DURATION,ITERATION,PAGECOUNT,STAGE,filename,remarks,userid FROM NEWS where stage='En attente prod' and Active<> 'No'";
                string strShipName = string.Empty, strShipCity = string.Empty;
                var objJavaScriptSerializer = new JavaScriptSerializer();
                hndSelectedValue.Value="";

                /*
                if (hndSelectedValue.Value.Trim().Length > 0)
                {
                    SelectedDataCollection objSelectedDataCollection = new SelectedDataCollection();
                    objSelectedDataCollection = objJavaScriptSerializer.Deserialize<SelectedDataCollection>(hndSelectedValue.Value);

                    foreach (SelectedData selectedData in objSelectedDataCollection.DataCollection)
                    {
                        if (selectedData.SelectedValue.Count<string>() > 0)
                        {
                            if (selectedData.ColumnName == "INDATE")
                            {
                                strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND Active<> 'No'";
                            }
                            else
                            {
                                strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue) + "AND Active<> 'No'";
                            }
                        }
                    }
                }
                else
                {
                    strQuery = strQuery + " where Active<> 'No' ";
                }
                */
                OrderDAONews objProductDAO = new OrderDAONews();

                IList<OrderViewNews> orderViewList = objProductDAO.GetOrders(strQuery);
                //if (orderViewList.Count == 0)
                //    orderViewList.Add(new OrderViewNews());

                grdViewOrders.DataSource = orderViewList;
                grdViewOrders.DataBind();
                // Session["ppp"] = orderViewList;
                if (grdViewOrders.Rows.Count > 0)
                {
                    for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 4; intRowIndex++)
                    {
                        TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                        tblCell.CssClass = "filter";
                        tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                    }
                }


            }
        }
        protected void ddlCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strQuery = "";
            if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
                if (ddlCollection.SelectedItem.Text == "Tous les thèmes")
                {
                    strQuery = "SELECT DID,DECLINATION,INDATE,DUEDATE,CTITLE,DEMANDTYPE,DURATION,ITERATION,PAGECOUNT,STAGE,filename,remarks,userid FROM NEWS where  category='" + ddlwriting.SelectedItem.Text + "' and Active<> 'No'";
                }
                else
                {
                    strQuery = "SELECT DID,DECLINATION,INDATE,DUEDATE,CTITLE,DEMANDTYPE,DURATION,ITERATION,PAGECOUNT,STAGE,filename,remarks,userid FROM NEWS where DECLINATION='" + ddlCollection.SelectedItem.Text + "' and category='" + ddlwriting.SelectedItem.Text + "' and Active<> 'No'";
                }
            }
            else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
                strQuery = "SELECT DID,DECLINATION,INDATE,DUEDATE,CTITLE,DEMANDTYPE,DURATION,ITERATION,PAGECOUNT,STAGE,filename,remarks,userid FROM NEWS where category='" + ddlwriting.SelectedItem.Text + "' and Active<> 'No'";
            }
            else if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
                if (ddlCollection.SelectedItem.Text == "Tous les thèmes")
                {
                    strQuery = "SELECT DID,DECLINATION,INDATE,DUEDATE,CTITLE,DEMANDTYPE,DURATION,ITERATION,PAGECOUNT,STAGE,filename,remarks,userid FROM NEWS where Active<> 'No'";
                }
                else
                {
                    strQuery = "SELECT DID,DECLINATION,INDATE,DUEDATE,CTITLE,DEMANDTYPE,DURATION,ITERATION,PAGECOUNT,STAGE,filename,remarks,userid FROM NEWS where DECLINATION='" + ddlCollection.SelectedItem.Text + "' and Active<> 'No'";
                }
            }
			  else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
                strQuery = "SELECT * FROM NEWS where  Active<> 'No'";
            }
            
          
            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();
            hndSelectedValue.Value = "";

            OrderDAONews objProductDAO = new OrderDAONews();

            IList<OrderViewNews> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewNews());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();
            // Session["ppp"] = orderViewList;
            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 4; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
        }
        protected void ddlwriting_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strQuery = "";
            if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
               // strQuery = "SELECT DID,DECLINATION,INDATE,DUEDATE,CTITLE,DEMANDTYPE,DURATION,ITERATION,PAGECOUNT,STAGE,filename,remarks,userid FROM NEWS where DECLINATION='" + ddlCollection.SelectedItem.Text + "' and category='" + ddlwriting.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = "select n.*,l.firstname+' '+l.lastname as fullname from news n , login l where n.userid=l.userid and n.DECLINATION='" + ddlCollection.SelectedItem.Text + "' and n.category='" + ddlwriting.SelectedItem.Text + "' and n.Active<> 'No'";
            }
            else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
               // strQuery = "SELECT DID,DECLINATION,INDATE,DUEDATE,CTITLE,DEMANDTYPE,DURATION,ITERATION,PAGECOUNT,STAGE,filename,remarks,userid FROM NEWS where category='" + ddlwriting.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = " select n.*,l.firstname+' '+l.lastname as fullname from news n , login l where n.userid=l.userid and n.category='" + ddlwriting.SelectedItem.Text + "' and n.Active<> 'No'";
            }
            else if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
                //strQuery = "SELECT DID,DECLINATION,INDATE,DUEDATE,CTITLE,DEMANDTYPE,DURATION,ITERATION,PAGECOUNT,STAGE,filename,remarks,userid FROM NEWS where DECLINATION='" + ddlCollection.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = " select n.*,l.firstname+' '+l.lastname as fullname from news n , login l where n.userid=l.userid and n.DECLINATION='" + ddlCollection.SelectedItem.Text + "' and n.Active<> 'No'";
            }
			else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
               // strQuery = "SELECT * FROM NEWS where  Active<> 'No'";
                strQuery = " select n.*,l.firstname+' '+l.lastname as fullname from news n , login l where n.userid=l.userid and  n.Active<> 'No'";
            }
            hndSelectedValue.Value = "";
            /*
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
                        if (selectedData.ColumnName == "INDATE")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND Active<> 'No'";
                        }
                        else
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue) + "AND Active<> 'No'";
                        }
                    }
                }
            }
            else
            {
              //  strQuery = strQuery + " where Active<> 'No' ";
            }
            */
            OrderDAONews objProductDAO = new OrderDAONews();

            IList<OrderViewNews> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewNews());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();
            // Session["ppp"] = orderViewList;
            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 4; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
        }

        protected void ddlstage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strQuery="";
            if (ddlstage.SelectedItem.Text == "Show All Item")
            {
                strQuery = "SELECT * FROM NEWS where Active <> 'No'";
            }
            else if (ddlstage.SelectedItem.Text == "Article in process")
            {
                strQuery = "SELECT * FROM NEWS where Stage='" + ddlstage.SelectedItem.Text + "' and Active <> 'No'";
            }
            else if (ddlstage.SelectedItem.Text == "Article Completed")
            {
                strQuery = "SELECT * FROM NEWS where Stage='" + ddlstage.SelectedItem.Text + "' and Active <> 'No'";
            }
            else if (ddlstage.SelectedItem.Text == "Send for Production")
            {
                strQuery = "SELECT * FROM NEWS where Stage='" + ddlstage.SelectedItem.Text + "' and Active <> 'No'";
            }

            hndSelectedValue.Value = "";
           
            OrderDAONews objProductDAO = new OrderDAONews();

            IList<OrderViewNews> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewNews());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 4; intRowIndex++)
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
              "window.open('DossierSearch.aspx','','width=1035,height=550,top=50,left=100,scrollbars=yes');" + System.Environment.NewLine +
              "</script>");
            //string message = "Fonction non disponible pour le moment";
            //Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            //  "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
            //  "</script>");
        }
        protected void btnremovefilter_Click(object sender, EventArgs e)
        {
           string strQuery ="select n.*,l.firstname+' '+l.lastname as fullname from news n , login l where n.userid=l.userid and  n.Active <> 'No'";//SELECT * FROM NEWS where Active <> 'No'";
            hndSelectedValue.Value = "";

            OrderDAONews objProductDAO = new OrderDAONews();

            IList<OrderViewNews> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewNews());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 4; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
            ddltask.SelectedIndex = -1;
            ddlCollection.SelectedIndex = -1;
            ddlwriting.SelectedIndex = -1;
            Session["searchqry"] = null;
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["searchqry"] = null;
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

                
                //string message = "Développement en cours";
                //Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                //  "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
                //  "</script>");
                
            }
            else if (ddlProduct.SelectedValue == "RV")
            {
                Session[SESSION.LOGGED_PRODSITE] = "RV";
                Response.Redirect("JournalLanding.aspx");
            }
        }
        protected void editfile(object sender, EventArgs e)
        {
            LinkButton lnkCancel = (LinkButton)sender;

            string id = lnkCancel.CommandArgument;


            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
           "window.open('DossierUpdate.aspx?DID=" + id + "','','width=750,height=800,top=50,left=100,scrollbars=yes');" + System.Environment.NewLine +
           "</script>");

           // BindGrid();
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
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

        [WebMethod(EnableSession = true)]
        public static string InsertComment(string comment, string did)
        {
            
            //int uid = Convert.ToInt16(userid);
            SqlParameter[] paramList = new SqlParameter[2];
            paramList[0] = new SqlParameter("tdremark", comment);// SqlDbType.VarChar, 50);//, userid
            //paramList[0].Value = userid;
            paramList[1] = new SqlParameter("did", did);// SqlDbType.VarChar, 50);//, password
           
            int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.updatedossiertdComment, paramList);

            if (rowAffected > 0)
            {
                return "Commentaire ajouté";
            }
            else
            {
                return "error";
            }

        }
       


        [WebMethod(EnableSession = true)]
        public static string getComment(string did)
        {
            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("did", did);
            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getdossiertdcomment, paramlist);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["TD_Remarks"].ToString();
            }
            else
            {
                return "";
            }
        }

}
}