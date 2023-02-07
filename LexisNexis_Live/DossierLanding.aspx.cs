using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using LexisNexis;

namespace LexisNexis
{
    public partial class DossierLanding : System.Web.UI.Page
    {
        public static string _sortDirection;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        protected void grdViewOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdViewOrders.PageIndex = e.NewPageIndex;
            BindGrid();
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
        private void BindGrid()
        {
            string strQuery = "SELECT Encyc_id,Openreceiveddate,Collection_title,folio,item_type,itemdtd,title FROM lexisNexis_Encyclopedia";
            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();

            if (hndSelectedValue.Value.Trim().Length > 0)
            {
                SelectedDataCollection objSelectedDataCollection = new SelectedDataCollection();
                objSelectedDataCollection = objJavaScriptSerializer.Deserialize<SelectedDataCollection>(hndSelectedValue.Value);

                foreach (SelectedData selectedData in objSelectedDataCollection.DataCollection)
                {
                    if (selectedData.SelectedValue.Count<string>() > 0)
                        strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                }
            }

            OrderDAO objProductDAO = new OrderDAO();

            IList<OrderView> orderViewList = objProductDAO.GetOrders(strQuery);
            if (orderViewList.Count == 0)
                orderViewList.Add(new OrderView());

            grdViewOrders.DataSource = orderViewList;
            grdViewOrders.DataBind();

            for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 1; intRowIndex++)
            {
                TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                tblCell.CssClass = "filter";
                tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
            }
        }
        protected void grdViewOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = new DataTable();
            OrderDAO objProductDAO = new OrderDAO();
            string strQuery = "SELECT Encyc_id,Openreceiveddate,Collection_title,folio,item_type,itemdtd,title FROM lexisNexis_Encyclopedia";
            string strShipName = string.Empty, strShipCity = string.Empty;
            var objJavaScriptSerializer = new JavaScriptSerializer();

            if (hndSelectedValue.Value.Trim().Length > 0)
            {
                SelectedDataCollection objSelectedDataCollection = new SelectedDataCollection();
                objSelectedDataCollection = objJavaScriptSerializer.Deserialize<SelectedDataCollection>(hndSelectedValue.Value);

                foreach (SelectedData selectedData in objSelectedDataCollection.DataCollection)
                {
                    if (selectedData.SelectedValue.Count<string>() > 0)
                        strQuery = strQuery + ((strQuery.Contains(" WHERE ") == true) ? " AND " : " WHERE ") + selectedData.ColumnName + " IN " + ArrayToString(selectedData.SelectedValue);
                }
            }
            SqlConnection connection = null;


            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand command = new SqlCommand();
            command.CommandText = strQuery;
            command.Connection = connection;

            command.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dataTable);

            connection.Close();




            string SortDireaction = e.SortDirection.ToString();
            SetSortDirection(SortDireaction);
            if (dataTable != null)
            {
                //Sort the data.
                dataTable.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                grdViewOrders.DataSource = dataTable;
                grdViewOrders.DataBind();
                SortDireaction = _sortDirection;
            }

            for (int intRowIndex = 0; intRowIndex < grdViewOrders.Columns.Count - 1; intRowIndex++)
            {
                TableCell tblCell = ((TableCell)grdViewOrders.HeaderRow.Cells[intRowIndex]);
                tblCell.CssClass = "filter";
                tblCell.Attributes.Add("alt", grdViewOrders.Columns[intRowIndex].SortExpression);
            }

        }
        protected void Refresh_Click(object sender, EventArgs e)
        {
            BindGrid();
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
    }
}