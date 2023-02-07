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
    public partial class FicheLanding : System.Web.UI.Page
    {
         
        public static string _sortDirection;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SESSION.LOGGED_USER] != null)
            {
                Session[SESSION.LOGGED_PRODSITE] = "FS";
                hidDivId.Value = Session[SESSION.LOGGED_USER].ToString();
                if (Session[SESSION.LOGGED_ROLE].ToString() == "LN")
                {
                  //  btnsendprod.Visible = true;
                    // 17 dec 16
                    btnremoveFiche.Visible = true;

                   // btnlncomplete.Visible = true;
                }
                if (Session[SESSION.LOGGED_ROLE].ToString() == "TDM" || Session[SESSION.LOGGED_ROLE].ToString() == "TDN")
                {
                  //  btntdcomplete.Visible = true;
                    // 31 aug remove
                    openButton.Visible = false;
                    ddltask.Visible = false;
                    lblTask.Visible = false;
                    ///////
                }

                // 31 aug remove
                lblname.Text = Session[SESSION.LOGGED_USER_NAME].ToString();
                /////

                if (!Page.IsPostBack)
                {
                    BindGrid();
                    /*
                    bindcollection();
                    bindInputtype();

                    if (Session[SESSION.LOGGED_PREVIOUS_COLLECTION].ToString() == "" && Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString()=="")
                    {
                        BindGrid();
                    }
                    else
                    {
                        BindGridMemory(Session[SESSION.LOGGED_PREVIOUS_COLLECTION].ToString(),Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString());

                        if (Session[SESSION.LOGGED_PREVIOUS_COLLECTION].ToString() != "")
                        {
                            ddlCollection.Items.FindByText(Session[SESSION.LOGGED_PREVIOUS_COLLECTION].ToString().Trim()).Selected = true;
                        }
                        if (Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString() != "")
                        {
                            ddlwriting.Items.FindByText(Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString().Trim()).Selected = true;
                        }
                    }
                  //  BindGrid();
                  
                    bindproduct();
                    // 31 aug remove
                    bindtask();
                    ddlProduct.SelectedValue = "FS";
                    */
                }
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            if (Session[SESSION.LOGGED_USER] != null)
            {
                Session[SESSION.LOGGED_PRODSITE] = "FS";
                BindGrid();
                /*
                if (Session["searchqryfiche"] != null)
                {
                    BindsearchGrid();
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        if (Session[SESSION.LOGGED_PREVIOUS_COLLECTION].ToString() == "" && Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString() == "")
                        {
                            BindGrid();
                        }
                        else
                        {
                            BindGridMemory(Session[SESSION.LOGGED_PREVIOUS_COLLECTION].ToString(), Session[SESSION.LOGGED_PREVIOUS_REDACTION].ToString());
                            Session[SESSION.LOGGED_PREVIOUS_COLLECTION] = "";
                            Session[SESSION.LOGGED_PREVIOUS_REDACTION] = "";
                        }
                    }
                }
                */
            }
        }
        private void BindsearchGrid()
        {
            string strQuery = Session["searchqryfiche"].ToString();
            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();

            OrderDAOFiche objProductDAO = new OrderDAOFiche();

            IList<OrderViewFiche> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 12; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
            else
            {
                btnsendprod.Visible = false;
                btnremoveFiche.Visible = false;
            }
            Session["searchqryfiche"] = null;
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
        private void bindcollection()
        {
            ddlCollection.Items.Clear();
            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getCollection, paramlist);
            ddlCollection.DataSource = ds;
            ddlCollection.DataTextField = "collection_desc";
            ddlCollection.DataValueField = "collectionid";
            ddlCollection.DataBind();
            ddlCollection.Items.Insert(0, new ListItem("----------", "-1"));
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
        private void BindGridMemory(string collection,string redaction)
        {
            string strQuery = "";
            /*
            //string strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate";//SELECT * FROM ENCYCLOPEDIA
            if (collection != "" && redaction != "")
            {
                 strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate WHERE  a.userid=l.userid AND collection='" + collection + "' AND category='" + redaction + "' ";//SELECT * FROM ENCYCLOPEDIA
            }
            else if (collection == "" && redaction != "")
            {
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate WHERE  a.userid=l.userid AND category='" + redaction + "' ";//SELECT * FROM ENCYCLOPEDIA
            }
            else if (collection != "" && redaction == "")
            {
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate WHERE  a.userid=l.userid AND collection='" + collection + "' ";//SELECT * FROM ENCYCLOPEDIA
            }
            else
            {
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate WHERE  a.userid=l.userid";//SELECT * FROM ENCYCLOPEDIA
            }
            */

            strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, fiches a inner join (select fid,max(duedate) as duedate from fiches group by fid )b on a.fid = b.fid and a.duedate = b.duedate WHERE  a.userid=l.userid";//SELECT * FROM ENCYCLOPEDIA


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
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "FID")
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

            OrderDAOFiche objProductDAO = new OrderDAOFiche();

            IList<OrderViewFiche> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 12; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
            else
            {
                btnsendprod.Visible = false;
                btnremoveFiche.Visible = false;
            }

        }
        private void BindGrid()
        {
            string strQuery = "";
            /*
            if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
                // strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "' and category='" + ddlwriting.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate WHERE COLLECTION='" + ddlCollection.SelectedItem.Text + "' and category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid";
            }
            else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
                //strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where category='" + ddlwriting.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate WHERE category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid";
            }
            else if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
                //  strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate  WHERE COLLECTION='" + ddlCollection.SelectedItem.Text + "'and a.userid=l.userid";
            }
            else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
                // strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate  WHERE  a.userid=l.userid";
            }
            */

            //strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, fiches a inner join (select fid,max(duedate) as duedate from fiches group by fid )b on a.fid = b.fid and a.duedate = b.duedate  WHERE  a.userid=l.userid";
            strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, fiches a inner join (select fid,max(iteration) as iteration from fiches group by fid )b on a.fid = b.fid and a.iteration = b.iteration  WHERE  a.userid=l.userid and a.stage='En attente'";

          
         //   string strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate WHERE  a.userid=l.userid";//SELECT * FROM ENCYCLOPEDIA
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
                            string ss = ArrayToString(selectedData.SelectedValue);
                            if (ss == "('')")
                            {
                                strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " is null " + "AND a.Active<> 'No'";
                            }
                            else
                            {
                                strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                            }
                        }
                        else if (selectedData.ColumnName == "INDATE")
                        {
                             string ss = ArrayToString(selectedData.SelectedValue);
                             if (ss == "('')")
                             {
                                 strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " is null " + "AND a.Active<> 'No'";
                             }
                             else
                             {
                                 strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                             }
                        }
                        else if (selectedData.ColumnName == "DELIVERED_DATE")
                        {
                             string ss = ArrayToString(selectedData.SelectedValue);
                             if (ss == "('')")
                             {
                                 strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " is null " + "AND a.Active<> 'No'";
                             }
                             else
                             {
                                 strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a." + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                             }
                        }
                        else if (selectedData.ColumnName == "FID")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "a." + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                        }
                        else if (selectedData.ColumnName == "fullname")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "l.firstname+' '+l.lastname" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "a." + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                    }
                }
            }
            else
            {
                strQuery = strQuery + " AND A.Active<> 'No' Order by FID Desc";
            }

            OrderDAOFiche objProductDAO = new OrderDAOFiche();

            IList<OrderViewFiche> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());
           
            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 12; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
            else
            {
                btnsendprod.Visible = false;
                btnremoveFiche.Visible = false;
            }

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
            /*
            if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
                strQuery = "select a.eid,a.dtitle,a.folio,a.demandtype,a.collection,a.itemtype,a.dtd,a.applicantname,a.notification,a.comments," +
               "CONVERT(VARCHAR(10), a.Indate, 103) + ' '  + convert(VARCHAR(8), a.Indate, 14) as indate," +
               "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate," +
               "CONVERT(VARCHAR(10), a.allocation_date, 103) + ' '  + convert(VARCHAR(8), a.allocation_date, 14) as allocation_date," +
               "CONVERT(VARCHAR(10), a.revised_duedate, 103) + ' '  + convert(VARCHAR(8), a.revised_duedate, 14) as revised_duedate," +
               "CONVERT(VARCHAR(10), a.delivered_date, 103) + ' '  + convert(VARCHAR(8), a.delivered_date, 14) as delivered_date," +
               "a.iteration,CONVERT(VARCHAR(10), a.returndate, 103) + ' '  + convert(VARCHAR(8), a.returndate, 14) as returndate," +
               "a.pagecount,a.stage,a.tat,a.reference,a.filesname,a.erid,a.category,a.tdcomments,a.tdfilename,a.userid,a.wordcount" +
               ",l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "' and category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid";
                // strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "' and category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid";
            }
            else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
                strQuery = "select a.eid,a.dtitle,a.folio,a.demandtype,a.collection,a.itemtype,a.dtd,a.applicantname,a.notification,a.comments," +
                 "CONVERT(VARCHAR(10), a.Indate, 103) + ' '  + convert(VARCHAR(8), a.Indate, 14) as indate," +
                 "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate," +
                 "CONVERT(VARCHAR(10), a.allocation_date, 103) + ' '  + convert(VARCHAR(8), a.allocation_date, 14) as allocation_date," +
                 "CONVERT(VARCHAR(10), a.revised_duedate, 103) + ' '  + convert(VARCHAR(8), a.revised_duedate, 14) as revised_duedate," +
                 "CONVERT(VARCHAR(10), a.delivered_date, 103) + ' '  + convert(VARCHAR(8), a.delivered_date, 14) as delivered_date," +
                 "a.iteration,CONVERT(VARCHAR(10), a.returndate, 103) + ' '  + convert(VARCHAR(8), a.returndate, 14) as returndate," +
                 "a.pagecount,a.stage,a.tat,a.reference,a.filesname,a.erid,a.category,a.tdcomments,a.tdfilename,a.userid,a.wordcount" +
                 ",l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid";
                //strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid";
            }
            else if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
                strQuery = "select a.eid,a.dtitle,a.folio,a.demandtype,a.collection,a.itemtype,a.dtd,a.applicantname,a.notification,a.comments," +
               "CONVERT(VARCHAR(10), a.Indate, 103) + ' '  + convert(VARCHAR(8), a.Indate, 14) as indate," +
               "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate," +
               "CONVERT(VARCHAR(10), a.allocation_date, 103) + ' '  + convert(VARCHAR(8), a.allocation_date, 14) as allocation_date," +
               "CONVERT(VARCHAR(10), a.revised_duedate, 103) + ' '  + convert(VARCHAR(8), a.revised_duedate, 14) as revised_duedate," +
               "CONVERT(VARCHAR(10), a.delivered_date, 103) + ' '  + convert(VARCHAR(8), a.delivered_date, 14) as delivered_date," +
               "a.iteration,CONVERT(VARCHAR(10), a.returndate, 103) + ' '  + convert(VARCHAR(8), a.returndate, 14) as returndate," +
               "a.pagecount,a.stage,a.tat,a.reference,a.filesname,a.erid,a.category,a.tdcomments,a.tdfilename,a.userid,a.wordcount" +
               ",l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "'and a.userid=l.userid";
                //strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate  where COLLECTION='" + ddlCollection.SelectedItem.Text + "'and a.userid=l.userid";
            }
            else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text == "----------")
            {

                strQuery = "select a.fid,a.ftitle,a.folio,a.demandtype,a.DURATION,a.ITERATION," +
                "CONVERT(VARCHAR(10), a.FTP_DATE, 103) + ' '  + convert(VARCHAR(8), a.FTP_DATE, 14) as FTP_DATE," +
                "CONVERT(VARCHAR(10), a.INDATE, 103) + ' '  + convert(VARCHAR(8), a.INDATE, 14) as INDATE," +
                "CONVERT(VARCHAR(10), a.ALLOCATION_DATE, 103) + ' '  + convert(VARCHAR(8), a.ALLOCATION_DATE, 14) as ALLOCATION_DATE," +
                "CONVERT(VARCHAR(10), a.DUEDATE, 103) + ' '  + convert(VARCHAR(8), a.DUEDATE, 14) as DUEDATE," +
                "CONVERT(VARCHAR(10), a.REVISED_DUEDATE, 103) + ' '  + convert(VARCHAR(8), a.REVISED_DUEDATE, 14) as REVISED_DUEDATE," +
                 "CONVERT(VARCHAR(10), a.DELIVERED_DATE, 103) + ' '  + convert(VARCHAR(8), a.DELIVERED_DATE, 14) as DELIVERED_DATE," +
                "a.PAGECOUNT,a.errorfilename," +
                "a.STAGE,a.comments,a.tat,a.filesname,a.Sgm_Filename,a.userid,a.TDFilename,a.NOTIFICATION,a.tat,a.FRID,a.TDComments,a.combi,a.codecoll,a.nummac,a.artfas,a.numfas" +
                ",l.firstname+' '+l.lastname as fullname from login l, fiches a inner join (select fid,max(ITERATION) as ITERATION from fiches group by fid )b on a.fid = b.fid and a.ITERATION = b.ITERATION WHERE  a.userid=l.userid";
            }
            */
            strQuery = "select a.fid,a.ftitle,a.folio,a.demandtype,a.DURATION,a.ITERATION," +
               "CONVERT(VARCHAR(10), a.FTP_DATE, 103) + ' '  + convert(VARCHAR(8), a.FTP_DATE, 14) as FTP_DATE," +
               "CONVERT(VARCHAR(10), a.INDATE, 103) + ' '  + convert(VARCHAR(8), a.INDATE, 14) as INDATE," +
               "CONVERT(VARCHAR(10), a.ALLOCATION_DATE, 103) + ' '  + convert(VARCHAR(8), a.ALLOCATION_DATE, 14) as ALLOCATION_DATE," +
               "CONVERT(VARCHAR(10), a.DUEDATE, 103) + ' '  + convert(VARCHAR(8), a.DUEDATE, 14) as DUEDATE," +
               "CONVERT(VARCHAR(10), a.REVISED_DUEDATE, 103) + ' '  + convert(VARCHAR(8), a.REVISED_DUEDATE, 14) as REVISED_DUEDATE," +
                "CONVERT(VARCHAR(10), a.DELIVERED_DATE, 103) + ' '  + convert(VARCHAR(8), a.DELIVERED_DATE, 14) as DELIVERED_DATE," +
               "a.PAGECOUNT,a.errorfilename," +
               "a.STAGE,a.comments,a.tat,a.filesname,a.Sgm_Filename,a.userid,a.TDFilename,a.NOTIFICATION,a.tat,a.FRID,a.TDComments,a.combi,a.codecoll,a.nummac,a.artfas,a.numfas" +
               ",l.firstname+' '+l.lastname as fullname from login l, fiches a inner join (select fid,max(ITERATION) as ITERATION from fiches group by fid )b on a.fid = b.fid and a.ITERATION = b.ITERATION WHERE  a.userid=l.userid and a.stage='En attente'";


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
                        else if (selectedData.ColumnName == "EID")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "a." + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                        }
                        else if (selectedData.ColumnName == "fullname")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "l.firstname+' '+l.lastname" + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                        else
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "a." + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue) + "AND a.Active<> 'No'";
                        }
                    }
                }
            }
            else
            {
                strQuery = strQuery + " and a.Active<> 'No'";
            }
            bool datsts = false;
            if (GridViewSortExpression != string.Empty)
            {
                if (GridViewSortExpression.ToString() == "INDATE" || GridViewSortExpression.ToString() == "DUEDATE" || GridViewSortExpression.ToString() == "DELIVERED_DATE")
                {
                    string sort = ViewState["SortDireaction"].ToString();// GetSortDirection();
                    strQuery = strQuery + " Order by " + "a." + GridViewSortExpression.ToString() + " " + sort;
                    datsts = true;
                }
                else
                {
                   // strQuery = strQuery + " order by Articleid";
                }
            }
            else
            {
                strQuery = strQuery + " order by FID Desc";
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

            for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 12; intRowIndex++)
            {
                TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                tblCell.CssClass = "filter";
                tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
            }
            // BindGrid();
        }

        protected void grdViewOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = new DataTable();
            OrderDAO objProductDAO = new OrderDAO();
            string strQuery = "";
            /*
            if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
                strQuery = "select a.eid,a.dtitle,a.folio,a.demandtype,a.collection,a.itemtype,a.dtd,a.applicantname,a.notification,a.comments," +
               "CONVERT(VARCHAR(10), a.Indate, 103) + ' '  + convert(VARCHAR(8), a.Indate, 14) as indate," +
               "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate," +
               "CONVERT(VARCHAR(10), a.allocation_date, 103) + ' '  + convert(VARCHAR(8), a.allocation_date, 14) as allocation_date," +
               "CONVERT(VARCHAR(10), a.revised_duedate, 103) + ' '  + convert(VARCHAR(8), a.revised_duedate, 14) as revised_duedate," +
               "CONVERT(VARCHAR(10), a.delivered_date, 103) + ' '  + convert(VARCHAR(8), a.delivered_date, 14) as delivered_date," +
               "a.iteration,CONVERT(VARCHAR(10), a.returndate, 103) + ' '  + convert(VARCHAR(8), a.returndate, 14) as returndate," +
               "a.pagecount,a.stage,a.tat,a.reference,a.filesname,a.erid,a.category,a.tdcomments,a.tdfilename,a.userid,a.wordcount" +
               ",l.firstname+' '+l.lastname as fullname from login l, fiches a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "' and category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid";
              
            }
            else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
                strQuery = "select a.eid,a.dtitle,a.folio,a.demandtype,a.collection,a.itemtype,a.dtd,a.applicantname,a.notification,a.comments," +
                 "CONVERT(VARCHAR(10), a.Indate, 103) + ' '  + convert(VARCHAR(8), a.Indate, 14) as indate," +
                 "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate," +
                 "CONVERT(VARCHAR(10), a.allocation_date, 103) + ' '  + convert(VARCHAR(8), a.allocation_date, 14) as allocation_date," +
                 "CONVERT(VARCHAR(10), a.revised_duedate, 103) + ' '  + convert(VARCHAR(8), a.revised_duedate, 14) as revised_duedate," +
                 "CONVERT(VARCHAR(10), a.delivered_date, 103) + ' '  + convert(VARCHAR(8), a.delivered_date, 14) as delivered_date," +
                 "a.iteration,CONVERT(VARCHAR(10), a.returndate, 103) + ' '  + convert(VARCHAR(8), a.returndate, 14) as returndate," +
                 "a.pagecount,a.stage,a.tat,a.reference,a.filesname,a.erid,a.category,a.tdcomments,a.tdfilename,a.userid,a.wordcount" +
                 ",l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid";
              
            }
            else if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
                strQuery = "select a.eid,a.dtitle,a.folio,a.demandtype,a.collection,a.itemtype,a.dtd,a.applicantname,a.notification,a.comments," +
               "CONVERT(VARCHAR(10), a.Indate, 103) + ' '  + convert(VARCHAR(8), a.Indate, 14) as indate," +
               "CONVERT(VARCHAR(10), a.duedate, 103) + ' '  + convert(VARCHAR(8), a.duedate, 14) as duedate," +
               "CONVERT(VARCHAR(10), a.allocation_date, 103) + ' '  + convert(VARCHAR(8), a.allocation_date, 14) as allocation_date," +
               "CONVERT(VARCHAR(10), a.revised_duedate, 103) + ' '  + convert(VARCHAR(8), a.revised_duedate, 14) as revised_duedate," +
               "CONVERT(VARCHAR(10), a.delivered_date, 103) + ' '  + convert(VARCHAR(8), a.delivered_date, 14) as delivered_date," +
               "a.iteration,CONVERT(VARCHAR(10), a.returndate, 103) + ' '  + convert(VARCHAR(8), a.returndate, 14) as returndate," +
               "a.pagecount,a.stage,a.tat,a.reference,a.filesname,a.erid,a.category,a.tdcomments,a.tdfilename,a.userid,a.wordcount" +
               ",l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "'and a.userid=l.userid";
              
            }
            else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text == "----------")
            {

                strQuery = "select a.fid,a.ftitle,a.folio,a.demandtype,a.DURATION,a.ITERATION," +
                 "CONVERT(VARCHAR(10), a.FTP_DATE, 103) + ' '  + convert(VARCHAR(8), a.FTP_DATE, 14) as FTP_DATE," +
                 "CONVERT(VARCHAR(10), a.INDATE, 103) + ' '  + convert(VARCHAR(8), a.INDATE, 14) as INDATE," +
                 "CONVERT(VARCHAR(10), a.ALLOCATION_DATE, 103) + ' '  + convert(VARCHAR(8), a.ALLOCATION_DATE, 14) as ALLOCATION_DATE," +
                 "CONVERT(VARCHAR(10), a.DUEDATE, 103) + ' '  + convert(VARCHAR(8), a.DUEDATE, 14) as DUEDATE," +
                 "CONVERT(VARCHAR(10), a.REVISED_DUEDATE, 103) + ' '  + convert(VARCHAR(8), a.REVISED_DUEDATE, 14) as REVISED_DUEDATE," +
                  "CONVERT(VARCHAR(10), a.DELIVERED_DATE, 103) + ' '  + convert(VARCHAR(8), a.DELIVERED_DATE, 14) as DELIVERED_DATE," +
                 "a.PAGECOUNT,a.errorfilename," +
                 "a.STAGE,a.comments,a.tat,a.filesname,a.Sgm_Filename,a.userid,a.TDFilename,a.NOTIFICATION,a.tat,a.FRID,a.TDComments,a.combi,a.codecoll,a.nummac,a.artfas,a.numfas" +
                 ",l.firstname+' '+l.lastname as fullname from login l, fiches a inner join (select fid,max(ITERATION) as ITERATION from fiches group by fid )b on a.fid = b.fid and a.ITERATION = b.ITERATION WHERE  a.userid=l.userid";
            }
            */

            strQuery = "select a.fid,a.ftitle,a.folio,a.demandtype,a.DURATION,a.ITERATION," +
                "CONVERT(VARCHAR(10), a.FTP_DATE, 103) + ' '  + convert(VARCHAR(8), a.FTP_DATE, 14) as FTP_DATE," +
                "CONVERT(VARCHAR(10), a.INDATE, 103) + ' '  + convert(VARCHAR(8), a.INDATE, 14) as INDATE," +
                "CONVERT(VARCHAR(10), a.ALLOCATION_DATE, 103) + ' '  + convert(VARCHAR(8), a.ALLOCATION_DATE, 14) as ALLOCATION_DATE," +
                "CONVERT(VARCHAR(10), a.DUEDATE, 103) + ' '  + convert(VARCHAR(8), a.DUEDATE, 14) as DUEDATE," +
                "CONVERT(VARCHAR(10), a.REVISED_DUEDATE, 103) + ' '  + convert(VARCHAR(8), a.REVISED_DUEDATE, 14) as REVISED_DUEDATE," +
                 "CONVERT(VARCHAR(10), a.DELIVERED_DATE, 103) + ' '  + convert(VARCHAR(8), a.DELIVERED_DATE, 14) as DELIVERED_DATE," +
                "a.PAGECOUNT,a.errorfilename," +
                "a.STAGE,a.comments,a.tat,a.filesname,a.Sgm_Filename,a.userid,a.TDFilename,a.NOTIFICATION,a.tat,a.FRID,a.TDComments,a.combi,a.codecoll,a.nummac,a.artfas,a.numfas" +
                ",l.firstname+' '+l.lastname as fullname from login l, fiches a inner join (select fid,max(ITERATION) as ITERATION from fiches group by fid )b on a.fid = b.fid and a.ITERATION = b.ITERATION WHERE  a.userid=l.userid and a.stage='En attente'";

            
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
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "cast (" + "a."  + selectedData.ColumnName + " as date)" + " IN " + ArrayToString(selectedData.SelectedValue) + " AND a.Active<> 'No'";
                        }
                        else if (selectedData.ColumnName == "EID")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "a." + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                        }
                          else if (selectedData.ColumnName == "fullname")
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "l.firstname+' '+l.lastname" + " IN " + ArrayToString(selectedData.SelectedValue) + " AND a.Active<> 'No'";
                        }
                        else
                        {
                            strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + "a." + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue) + " AND a.Active<> 'No'";
                        }
                    }
                }
            }
            else
            {
                strQuery = strQuery + " and a.Active<> 'No' ";
            }

            bool datsts = false;
            if (e.SortExpression.ToString() == "INDATE" || e.SortExpression.ToString() == "DUEDATE" || e.SortExpression.ToString() == "DELIVERED_DATE")
            {
                string sort = GetSortDirection();
                strQuery = strQuery + " Order by " + "a." + e.SortExpression.ToString() + " " + sort;
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
               // grdViewOrders.DataSource = SortDataTable(dataTable, false);
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

            for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 12; intRowIndex++)
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

        // 31 aug remove
       protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddltask.SelectedItem.Text == "Loguer un fascicule")
            {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            "window.open('EncyclopediasEntry.aspx','','width=750,height=800,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
            "</script>");
            }
            else if (ddltask.SelectedItem.Text == "Envoyer en fasc/dossier en prod")
            {
                //Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                //"window.open('EncyclopediaReviseEntry.aspx','','width=800,height=700,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
                //"</script>");
            }
        }
        
        protected void btnsendprod_Click(object sender, EventArgs e)
        {
           

            int cnt = 0;
            bool chkselectstatus = false;
            foreach (GridViewRow row in grdViewOrders.Rows)
            {
                CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
                if (chkCalendarId.Checked)
                {
                    if (chkCalendarId.ToolTip == "En attente prod")
                    {
                        chkselectstatus = true;
                        string liCalendarId = (row.FindControl("litID") as Literal).Text;

                        SqlParameter[] paramlist = new SqlParameter[3];
                        paramlist[0] = new SqlParameter("eid", liCalendarId);
                        paramlist[1] = new SqlParameter("stage", "En préparation");
                        paramlist[2] = new SqlParameter("InDate", Common.GetDayLightTime());
                        int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.AllocteEncycloItem, paramlist);
                        if (rowresult > 0)
                        {
                            cnt++;

                            //Success mail
                            SqlParameter[] paramlist1 = new SqlParameter[1];
                            paramlist1[0] = new SqlParameter("eid", liCalendarId);
                            DataSet set = DataAccess.ExecuteDataSetSP(SPNames.getemaildataencyclo, paramlist1);
                            if (set.Tables[0].Rows.Count > 0)
                            {

                                string strTitle = set.Tables[0].Rows[0]["DTITLE"].ToString();
                                string strDuedate = set.Tables[0].Rows[0]["DUEDATE"].ToString();
                                string strComments = set.Tables[0].Rows[0]["COMMENTS"].ToString();

                                string strTo = Session[SESSION.LOGGED_USER].ToString();// System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                                string strLT = Session[SESSION.LOGGED_ROLE].ToString();

                                //    string strStage = "Article in process";

                                string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";

                                string strCC = set.Tables[0].Rows[0]["NOTIFICATION"].ToString();
                                // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                                // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                                string strFile = Server.MapPath("App_Data\\MAILS\\LN_SEND_NEW_JOB\\") + "E.html";

                                if (File.Exists(strFile))
                                {
                                    StreamReader sr = new StreamReader(strFile);
                                    string FileC = sr.ReadToEnd();
                                    sr.Close();
                                    string strBody = FileC;
                                    strBody = strBody.Replace("[ILT]", strTitle);
                                    strBody = strBody.Replace("[DTAD]", strDuedate);
                                    strBody = strBody.Replace("[IACE]", strComments);
                                    strBody = strBody.Replace("[IHT]", strLink);

                                    string strSubject = "Léonard – Demande d’intervention sur le document : \"" + strTitle + "\"";
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
        protected void btnlncomplete_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (GridViewRow row in grdViewOrders.Rows)
            {
                CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
                if (chkCalendarId.Checked)
                {
                    if (chkCalendarId.ToolTip == "Article submitted by TD")
                    {
                        string liCalendarId = (row.FindControl("litID") as Literal).Text;

                        SqlParameter[] paramlist = new SqlParameter[3];
                        paramlist[0] = new SqlParameter("eid", liCalendarId);
                        paramlist[1] = new SqlParameter("stage", "Article Completed");
                        paramlist[2] = new SqlParameter("InDate", Common.GetDayLightTime());
                        int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.AllocteEncycloItem, paramlist);
                        if (rowresult > 0)
                        {
                            cnt++;


                        }
                        if (DataAccess.isDBConnectionFail == true)
                        {
                            //Errlbl.Visible = true;
                            return;
                        }
                    }
                    else
                    {
                    }
                }
            }


            BindGrid();
        }
        protected void btntdcomplete_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (GridViewRow row in grdViewOrders.Rows)
            {
                CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
                if (chkCalendarId.Checked)
                {
                    
                    string liCalendarId = (row.FindControl("litID") as Literal).Text;

                    SqlParameter[] paramlist = new SqlParameter[3];
                    paramlist[0] = new SqlParameter("eid", liCalendarId);
                    paramlist[1] = new SqlParameter("stage", "Article submitted by TD");
                    paramlist[2] = new SqlParameter("InDate", Common.GetDayLightTime());
                    int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.AllocteEncycloItem, paramlist);
                    if (rowresult > 0)
                    {
                        cnt++;


                    }
                    if (DataAccess.isDBConnectionFail == true)
                    {
                        //Errlbl.Visible = true;
                        return;
                    }
                }
            }


            BindGrid();
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
                        paramlist[0] = new SqlParameter("Fid", liCalendarId);
                        paramlist[1] = new SqlParameter("InDate", Common.GetDayLightTime());

                        int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.removeFicheItem, paramlist);
                        if (rowresult > 0)
                        {
                            // for file move to ftp
                            string sgmfilename = (row.FindControl("litsgmlfile") as Literal).Text;
                            string sourceftppath = ConfigurationSettings.AppSettings["FTPFilePath"];
                            string sourcefile = sourceftppath + "\\Envoi\\" + sgmfilename;
                            if (File.Exists(sourcefile))
                            {
                                File.Copy(sourcefile, sourceftppath + "\\Annulation\\" + sgmfilename, true);
                            }
                            else
                            {
                                
                            }

                            cnt++;
                            //Success mail
                            SqlParameter[] paramlist1 = new SqlParameter[1];
                            paramlist1[0] = new SqlParameter("fid", liCalendarId);
                            DataSet set = DataAccess.ExecuteDataSetSP(SPNames.getemaildatafiche, paramlist1);
                            if (set.Tables[0].Rows.Count > 0)
                            {

                                string strTitle = set.Tables[0].Rows[0]["FTITLE"].ToString();
                                string strDuedate = set.Tables[0].Rows[0]["DUEDATE"].ToString();
                                string strComments = set.Tables[0].Rows[0]["COMMENTS"].ToString();

                                string strTo = Session[SESSION.LOGGED_USER].ToString();// System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                                string strLT = Session[SESSION.LOGGED_ROLE].ToString();

                                //    string strStage = "Article in process";

                                string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                                string strCC = set.Tables[0].Rows[0]["NOTIFICATION"].ToString();
                                // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                                // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                                string strFile = Server.MapPath("App_Data\\MAILS\\LN_DELETE\\") + "F.html";

                                if (File.Exists(strFile))
                                {
                                    StreamReader sr = new StreamReader(strFile);
                                    string FileC = sr.ReadToEnd();
                                    sr.Close();
                                    string strBody = FileC;
                                    strBody = strBody.Replace("[ILT]", strTitle);
                                    strBody = strBody.Replace("[DTAD]", strDuedate);
                                    strBody = strBody.Replace("[IACE]", strComments);
                                    strBody = strBody.Replace("[IHT]", strLink);

                                    string strSubject = "Léonard – Extranet Éditorial – Intervention extérieure annulée pour le document : « " + strTitle + " »";
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
           // Export_Excel("exportpage");
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
            sqlparam[1] = new SqlParameter("dashboard", "EC");
            int result = DataAccess.ExecuteNonQuerySP(SPNames.updateloginfordashboard, sqlparam);
            */
              
            SqlParameter[] sqlparam = new SqlParameter[4];
            sqlparam[0] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
            sqlparam[1] = new SqlParameter("dashboard", "FS");
           
                sqlparam[2] = new SqlParameter("collections", "");
           
           
           
                sqlparam[3] = new SqlParameter("redaction", "");
           
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

                        Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);

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

                        Response.AddHeader("Content-Disposition", "attachment;   filename=" + filename);

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
            if (e.CommandName == "lnsgm")
            {

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnk = (LinkButton)row.FindControl("lnksgm");
                string filename = lnk.Text;
                string id = lnk.CommandArgument;
                string Expath = ConfigurationSettings.AppSettings["UploadFilePath"];
                if (filename != "")
                {

                    string sourceftppath = ConfigurationSettings.AppSettings["FTPFilePath"];
                    string sourcefile = sourceftppath + "\\Envoi\\" + filename;

                    string filepath = sourcefile;// Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD\\" + id + "\\" + filename;
                    string path = filepath;//Server.MapPath();
                    if (File.Exists(path))
                    {
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
                    else
                    {
                        string message = "file not found";
                        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                          "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
                          "</script>");
                    }
                }
            }
            if (e.CommandName == "Logerror")
            {

                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                LinkButton lnk = (LinkButton)row.FindControl("lnkerror");
                string filename = lnk.Text;
                string id = lnk.CommandArgument;
                string Expath = ConfigurationSettings.AppSettings["UploadFilePath"];
                if (filename != "")
                {

                   
                    string sourceftppath = ConfigurationSettings.AppSettings["FTPFilePath"];
                    string sourcefile = sourceftppath + "\\Logs_LNF\\" + filename;


                    string filepath = sourcefile;// Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\TD\\" + id + "\\" + filename;
                    string path = filepath;//Server.MapPath();
                    if (File.Exists(path))
                    {
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
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + e.CommandArgument + " ');", true);
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
               "window.open('TDUploadFile_Fiches.aspx?FRID=" + id + "','','width=800,height=700,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
               "</script>");
            }
        }
        protected void ValidateFile(object sender, EventArgs e)
        {
            #region Expiring Advert Details through Business Layer
            LinkButton lnkValidate = (LinkButton)sender;
           
            string id = lnkValidate.CommandArgument;
            SqlParameter[] paramlist = new SqlParameter[3];
            paramlist[0] = new SqlParameter("fid", id);
            paramlist[1] = new SqlParameter("stage", "Archivé");
            paramlist[2] = new SqlParameter("InDate", Common.GetDayLightTime());
            int rowresult = DataAccess.ExecuteNonQuerySP(SPNames.AllocteficheItem, paramlist);

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
           "window.open('FicheReviseEntry.aspx?FID=" + id + "','','width=800,height=700,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
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
                //Retrieve the state of the CheckBox
                CheckBox cb1 = (CheckBox)i.FindControl("chk");
                Literal lituserid = (Literal)i.FindControl("userid");
                if (Session[SESSION.LOGGED_ROLE].ToString() == "LN")
                {
                    // string stage = e.Row.Cells[8].Text;
                    if (cb1.ToolTip == "En attente") // En attente prod //!=|| cb1.ToolTip.ToUpper() == "ARTICLE SUBMITTED BY TD"// En préparation//  && lituserid.Text == Session[SESSION.LOGGED_USER].ToString()
                    {
                        cb1.Enabled = true;
                      //  LinkButton lnkedit = (LinkButton)i.FindControl("lnkedit");
                     //   lnkedit.Visible = true;
                    }
                    else
                    {
                        cb1.Enabled = false;
                    }
                    if (cb1.ToolTip == "Contenu préparé" && lituserid.Text == Session[SESSION.LOGGED_USER].ToString())
                    {
                       // LinkButton lnkvalidate = (LinkButton)i.FindControl("lnkvalidate");
                      //  lnkvalidate.Visible = true;


                      //  LinkButton lnkcancel = (LinkButton)i.FindControl("lnkcancel");
                      //  lnkcancel.Visible = true;
                    }
                }



                if (Session[SESSION.LOGGED_ROLE].ToString() == "TDM" || Session[SESSION.LOGGED_ROLE].ToString() == "TDN")
                {
                    cb1.Enabled = false;
                    if (cb1.ToolTip == "En préparation" || cb1.ToolTip == "En correction")//!=
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
            if (ddltask.SelectedItem.Text == "Déclarer un fiches")
            {
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                "window.open('FicheEntry.aspx','','width=750,height=800,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
                "</script>");
            }
            else if (ddltask.SelectedItem.Text == "Envoyer en prod")
            {


                string strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where stage='En attente prod' and Active<> 'No'";//SELECT * FROM ENCYCLOPEDIA
                string strShipName = string.Empty, strShipCity = string.Empty;
                var objJavaScriptSerializer = new JavaScriptSerializer();
                hndSelectedValue.Value = "";

                OrderDAOFiche objProductDAO = new OrderDAOFiche();

                IList<OrderViewFiche> orderViewList = objProductDAO.GetOrders(strQuery);
                //if (orderViewList.Count == 0)
                //    orderViewList.Add(new OrderViewEncylo());

                grdViewOrders.DataSource = orderViewList;
                grdViewOrders.DataBind();

                if (grdViewOrders.Rows.Count > 0)
                {
                    for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 12; intRowIndex++)
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
               // strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "' and category='" + ddlwriting.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "' and category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid and a.Active<> 'No'";
            }
            else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
                //strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where category='" + ddlwriting.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid and a.Active<> 'No'";
            }
            else if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
              //  strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate  where COLLECTION='" + ddlCollection.SelectedItem.Text + "'and a.userid=l.userid and a.Active<> 'No'";
            }
            else if( ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
               // strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate  where  a.userid=l.userid and a.Active<> 'No'";
            }

            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();
            hndSelectedValue.Value = "";

            OrderDAOFiche objProductDAO = new OrderDAOFiche();

            IList<OrderViewFiche> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 12; intRowIndex++)
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
                //strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "' and category='" + ddlwriting.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "' and category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid and a.Active<> 'No'";
            }
            else if (ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text != "----------")
            {
                //strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where category='" + ddlwriting.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where category='" + ddlwriting.SelectedItem.Text + "'and a.userid=l.userid and a.Active<> 'No'";
            }
            else if (ddlCollection.SelectedItem.Text != "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
                //strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where COLLECTION='" + ddlCollection.SelectedItem.Text + "' and Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate  where COLLECTION='" + ddlCollection.SelectedItem.Text + "'and a.userid=l.userid and a.Active<> 'No'";
            }
              else if( ddlCollection.SelectedItem.Text == "----------" && ddlwriting.SelectedItem.Text == "----------")
            {
                //strQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where Active<> 'No'";
                strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate  where  a.userid=l.userid and a.Active<> 'No'";
            }

            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();
            hndSelectedValue.Value = "";

            OrderDAOFiche objProductDAO = new OrderDAOFiche();

            IList<OrderViewFiche> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 12; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
        }
        protected void ddlstage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strQuery = "";
            if (ddlstage.SelectedItem.Text == "Show All Item")
            {
                strQuery = "SELECT * FROM encyclopedia where Active <> 'No'";
            }
            else if (ddlstage.SelectedItem.Text == "Article in process")
            {
                strQuery = "SELECT * FROM encyclopedia where Stage='" + ddlstage.SelectedItem.Text + "' and Active <> 'No'";
            }
            else if (ddlstage.SelectedItem.Text == "Article Completed")
            {
                strQuery = "SELECT * FROM encyclopedia where Stage='" + ddlstage.SelectedItem.Text + "' and Active <> 'No'";
            }
            else if (ddlstage.SelectedItem.Text == "Send for Production")
            {
                strQuery = "SELECT * FROM encyclopedia where Stage='" + ddlstage.SelectedItem.Text + "' and Active <> 'No'";
            }
            else if (ddlstage.SelectedItem.Text.ToUpper() == "ARTICLE DELIVERED")
            {
                strQuery = "SELECT * FROM encyclopedia where Stage='" + ddlstage.SelectedItem.Text + "' and Active <> 'No'";
            }



            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();
            hndSelectedValue.Value = "";

            OrderDAOFiche objProductDAO = new OrderDAOFiche();

            IList<OrderViewFiche> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();
            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 12; intRowIndex++)
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
               "window.open('FicheSearch.aspx','','width=1035,height=600,top=0,left=0,scrollbars=yes');" + System.Environment.NewLine +
               "</script>");
            //string message = "Fonction non disponible pour le moment";
            //Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            //  "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
            //  "</script>");
        }
        protected void btnremovefilter_Click(object sender, EventArgs e)
        {

            string strQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, fiches a inner join (select fid,max(iteration) as iteration from fiches group by fid )b on a.fid = b.fid and a.iteration = b.iteration  WHERE  a.userid=l.userid and a.Active <> 'No'  Order by FID Desc";
              //  "select a.*,l.firstname+' '+l.lastname as fullname from login l, encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where  a.userid=l.userid and a.Active <> 'No'";
            hndSelectedValue.Value = "";

            OrderDAOFiche objProductDAO = new OrderDAOFiche();

            IList<OrderViewFiche> orderViewList = objProductDAO.GetOrders(strQuery);
            //if (orderViewList.Count == 0)
            //    orderViewList.Add(new OrderViewEncylo());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();
            if (grdViewOrders.Rows.Count > 0)
            {
                for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 12; intRowIndex++)
                {
                    TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                    tblCell.CssClass = "filter";
                    tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
                }
            }
            // // 31 aug remove
           // ddltask.SelectedIndex = -1;
            ddlCollection.SelectedIndex = -1;
            ddlwriting.SelectedIndex = -1;
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
                * 
               */
            }
            else if (ddlProduct.SelectedValue == "RV")
            {
                Session[SESSION.LOGGED_PRODSITE] = "RV";
                Response.Redirect("JournalLanding.aspx");
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void openButton_Click(object sender, EventArgs e)
        {
            string filename = "";
            int cnt1 = 0;
            foreach (GridViewRow row in grdViewOrders.Rows)
            {
                CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
                if (chkCalendarId.Checked)
                {
                    string sgmf = (row.FindControl("litsgmlfile") as Literal).Text;
                    if (filename == "")
                    {
                        filename = sgmf;
                    }
                    else
                    {
                        filename = filename + "," + sgmf;
                    }
                    cnt1++;
                }
            }
            lbllistfilename.Text = filename;
            if (cnt1 == 0)
            {
                string message = "Merci de faire une sélection";
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
              "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
              "</script>");
                return;
            }

           /* Response.Redirect("http://localhost:59351/LexisNexis/FicheLanding.aspx#modal-one");*/
           string dddddd= Request.Url.Host.ToString();
            
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
          "window.location.href ='" + "http://"+dddddd+":59351/LexisNexis/FicheLanding.aspx#modal-one" +"';" + System.Environment.NewLine +
          "</script>");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            txtnotification.Text = "";
            txtcomment.Text = "";
            lbllistfilename.Text = "";
            foreach (GridViewRow row in grdViewOrders.Rows)
            {
                CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
                if (chkCalendarId.Checked)
                {
                    chkCalendarId.Checked = false;
                }
            }

        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            //if (!FileUpload1.HasFile)
            //{
            //    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
            //                   "alert('choisir le fiches');</script>");

            //    return;
            //}

            foreach (GridViewRow row in grdViewOrders.Rows)
            {
                CheckBox chkCalendarId = (CheckBox)row.FindControl("chk");// as CheckBox;
                if (chkCalendarId.Checked)
                {
                    string filename = "";

                    // get max value
                    string EID = "";


                    EID = row.Cells[0].Text;
                    string FRID = (row.FindControl("litfrid") as Literal).Text;

                    if (FileUpload1.HasFile)
                    {

                         filename = Path.GetFileName(FileUpload1.FileName);

                        string ext = Path.GetExtension(FileUpload1.FileName);
                        if (ext == ".doc" || ext == ".docx")//ext == ".zip" || ext == ".rar" || ext == ".7zip"
                        {
                            string Expath = ConfigurationSettings.AppSettings["UploadFilePath"];



                            try
                            {
                                if (!Directory.Exists(Expath))
                                {
                                    Directory.CreateDirectory(Expath);
                                }




                                //   filename = EID + "_" + "0" + ext;//"." +



                                // string ASDA=Session[SESSION.LOGGED_USER].ToString();
                                if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString()))
                                {
                                    Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString());
                                }
                                if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN"))
                                {
                                    Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN");
                                }

                                if (!Directory.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID))
                                {
                                    Directory.CreateDirectory(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID);
                                }

                                if (File.Exists(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\" + filename))
                                {
                                    File.Delete(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\" + filename);
                                }

                                FileUpload1.SaveAs(Expath + "\\" + Session[SESSION.LOGGED_PRODSITE].ToString() + "\\LN" + "\\" + EID + "\\" + filename);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else
                        {
                            filename = "";
                        }




                       
                    }

                                // for calculate TAT
                                ////////  DateTime dt = System.DateTime.Now;
                    System.DateTime dt = System.DateTime.Now.Add(new TimeSpan(Common.GetDayLightTime().Subtract(DateTime.Now).Hours, Common.GetDayLightTime().Subtract(DateTime.Now).Minutes, Common.GetDayLightTime().Subtract(DateTime.Now).Seconds));

                                System.DateTime dt1;
                                //dt = dt.AddDays(1); 


                                dt1 = dt.AddDays(1);

                              







                                SqlParameter[] paramList = new SqlParameter[10];
                                paramList[0] = new SqlParameter("FID", EID);
                                paramList[1] = new SqlParameter("NOTIFICATION", txtnotification.Text.Trim());
                                paramList[2] = new SqlParameter("DUEDATE", Convert.ToDateTime(dt1));//txtduedate.Text
                                paramList[3] = new SqlParameter("ITERATION", Convert.ToInt16("0"));
                                paramList[4] = new SqlParameter("STAGE", "En préparation");//En attente prod
                                paramList[5] = new SqlParameter("tat", "Courant");
                                paramList[6] = new SqlParameter("filesname", filename);
                                paramList[7] = new SqlParameter("frid", FRID);
                                paramList[8] = new SqlParameter("userid", Session[SESSION.LOGGED_USER].ToString());
                                paramList[9] = new SqlParameter("comment", txtcomment.Text);
                                int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.updatefiche, paramList);
                                if (rowAffected > 0)
                                {

                                    string strTitle = "";
                                    string strDuedate = dt1.ToString("dd-MMM-yyyy HH:mm:ss");


                                    string strTo = System.Configuration.ConfigurationSettings.AppSettings["strThomsonTo"]; // System.Configuration.ConfigurationSettings.AppSettings["strTo"];
                                    string strLT = System.Configuration.ConfigurationSettings.AppSettings["strThomsonCC"];

                                    //    string strStage = "Article in process";

                                    string strLink = "<a href=\"https://online.thomsondigital.com/LexisNexis_Live/Default.aspx\">https://online.thomsondigital.com/LexisNexis_Live/Default.aspx</a>";


                                    string strCC = txtnotification.Text.Trim();
                                    // string strBCC = System.Configuration.ConfigurationSettings.AppSettings[" strCC"];
                                    // string strSubject = "Léonard – Demande d’intervention sur le document : "Insert Léonard id-Titre du dossier" " 

                                    string strFile = Server.MapPath("App_Data\\MAILS\\LN_SEND_NEW_JOB\\") + "F.html";

                                    if (File.Exists(strFile))
                                    {
                                        StreamReader sr = new StreamReader(strFile);
                                        string FileC = sr.ReadToEnd();
                                        sr.Close();
                                        string strBody = FileC;
                                        strBody = strBody.Replace("[ILTD]", strTitle);
                                        strBody = strBody.Replace("[DTAT]", strDuedate);

                                        strBody = strBody.Replace("[IACE]", "aucun commentaire");

                                        strBody = strBody.Replace("[IHTA]", strLink);

                                        string strSubject = "Léonard – Demande d’intervention sur le document : « " + strTitle + " »";
                                        Common cmn = new Common();
                                        Common.SendEmail(strTo, strCC, strSubject, strBody);
                                        // Utility.NumberToEnglish.email();
                                    }



                                  //  lblmessage.Text = "Item ajouté";

                                    //  btnFinish.Visible = true;
                                }
                                else
                                {
                                   // lblmessage.Text = "error";
                                    return;
                                }


                   //             Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                   //"window.opener.location.href='EncyclopediasLanding.aspx';window.close();" + System.Environment.NewLine +
                   //"</script>");

                      
                   



                }
            }

            BindGrid();
        }



        
   /*     protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.FileUpload1.HasFile)
            {
                this.FileUpload1.SaveAs("c:\\" + this.FileUpload1.FileName);
            }
            else
            {
                //show alert that no file is choosen

            }
        }*/

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
      
}
}