using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TD.Data;

public partial class EncycloSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            /*
            binduser();
            bindcollection();
          //  binditemtype();
            bindDTDitem();
            bindnaturedemand();
            bindtat();
            bindcategory();
            */
            Panel panel1 = (Panel)Page.Master.FindControl("panel1");
            panel1.Visible = false;
        }
    }
    /*
    private void binduser()
    {
        ddlusersearch.Items.Clear();
        SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("roleid", Session[SESSION.LOGGED_ROLE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getuserbyrole, paramlist);
        ddlusersearch.DataSource = ds;
        ddlusersearch.DataTextField = "fullname";
        ddlusersearch.DataValueField = "userid";
        ddlusersearch.DataBind();
        ddlusersearch.Items.Insert(0, new ListItem("----------", "-1"));
    }
    private void bindcategory()
    {
        ddlcategory.Items.Clear();
        SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getinputcategory, paramlist);
        ddlcategory.DataSource = ds;
        ddlcategory.DataTextField = "catdesc";
        ddlcategory.DataValueField = "catid";
        ddlcategory.DataBind();
        ddlcategory.Items.Insert(0, new ListItem("----------", "-1"));
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
    private void binditemtype()
    {
        ddlitemtype.Items.Clear();
        SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.itemtypeEncyclo, paramlist);
        ddlitemtype.DataSource = ds;
        ddlitemtype.DataTextField = "itemtypedetails";
        ddlitemtype.DataValueField = "itemtypeid";
        ddlitemtype.DataBind();
        ddlitemtype.Items.Insert(0, new ListItem("----------", "-1"));
    }
    private void bindDTDitem()
    {
        ddldtditem.Items.Clear();
        SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.itemDTDEncyclo, paramlist);
        ddldtditem.DataSource = ds;
        ddldtditem.DataTextField = "dtd_name";
        ddldtditem.DataValueField = "dtdid";
        ddldtditem.DataBind();
        ddldtditem.Items.Insert(0, new ListItem("----------", "-1"));
    }
    private void bindnaturedemand()
    {
        ddldemandnature.Items.Clear();
        SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.demandNature, paramlist);
        ddldemandnature.DataSource = ds;
        ddldemandnature.DataTextField = "demanddesc";
        ddldemandnature.DataValueField = "demandid";
        ddldemandnature.DataBind();
        ddldemandnature.Items.Insert(0, new ListItem("----------", "-1"));
    }
    private void bindtat()
    {
        ddldelai.Items.Clear();
        SqlParameter[] paramlist = new SqlParameter[1];
        paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.Tat, paramlist);
        ddldelai.DataSource = ds;
        ddldelai.DataTextField = "tattype";
        ddldelai.DataValueField = "tat";
        ddldelai.DataBind();
        ddldelai.Items.Insert(0, new ListItem("----------", "-1"));
    }
    */

    protected void btnSend_Click(object sender, EventArgs e)
    {
        /*
        string user = ddlusersearch.SelectedValue.ToString();
        if (user == "-1")
        {
            user = "";
        }

        string reduction = ddlcategory.SelectedItem.Text;
        if (reduction == "----------")
        {
            reduction = "";
        }
        string collection = ddlCollection.SelectedItem.Text;
        if (collection == "----------")
        {
            collection = "";
        }
        //string itemtype = ddlitemtype.SelectedItem.Text;
        //if (itemtype == "----------")
        //{
        //    itemtype = "";
        //}
        string dtditem = ddldtditem.SelectedItem.Text;
        if (dtditem == "----------")
        {
            dtditem = "";
        }
        string delai = ddldelai.SelectedItem.Text;
        if (delai == "----------")
        {
            delai = "";
        }
        string demandnature = ddldemandnature.SelectedItem.Text;
        if (demandnature == "----------")
        {
            demandnature = "";
        }
        */
        //string SQLQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where Active <> 'No' ";
        string SQLQuery = "select a.*,l.firstname+' '+l.lastname as fullname from login l, fiches a inner join (select fid,max(iteration) as iteration from fiches group by fid )b on a.fid = b.fid and a.iteration = b.iteration where  a.userid=l.userid and a.Active <> 'No'";
        /*
        if (reduction != "")
        {
            SQLQuery = SQLQuery + " and category = '" + reduction + "'";
        }
        if (collection != "")
        {
            SQLQuery = SQLQuery + " and COLLECTION = '" + collection + "'";
        }
        //if (itemtype != "")
        //{
        //    SQLQuery = SQLQuery + " and ITEMTYPE = '" + itemtype + "'";
        //}
        if (dtditem != "")
        {
            SQLQuery = SQLQuery + " and DTD = '" + dtditem + "'";
        }
        if (delai != "")
        {
            SQLQuery = SQLQuery + " and tat = '" + delai + "'";
        }
        if (demandnature != "")
        {
            SQLQuery = SQLQuery + " and DEMANDTYPE = '" + demandnature + "'";
        }
        if (user != "")
        {
            SQLQuery = SQLQuery + " and a.userid = '" + user + "'";
        }
        */
        if (txttitle.Text.Trim() != "")
        {
            SQLQuery = SQLQuery + " and FTITLE = '" + txttitle.Text.Trim() + "'";
        }
        if (txtfolio.Text.Trim() != "")
        {
            SQLQuery = SQLQuery + " and FOLIO = '" + txtfolio.Text.Trim() + "'";
        }
        if (txtfromduedate.Text.Trim() != "" && txttoduedate.Text.Trim() != "")
        {
            DateTime date = DateTime.ParseExact(txtfromduedate.Text.Trim(), "dd/MM/yyyy", null);
            string Fromdate = date.ToString("MM/dd/yyyy");

            date = DateTime.ParseExact(txttoduedate.Text.Trim(), "dd/MM/yyyy", null);
            string Todate = date.ToString("MM/dd/yyyy");
            SQLQuery = SQLQuery + " And a.DUEDATE between  '" + Fromdate + "'" + " and '" + Todate + "'";
        }
        if (txtlogindatefrom.Text.Trim() != "" && txtlogindateto.Text.Trim() != "")
        {
            DateTime date = DateTime.ParseExact(txtlogindatefrom.Text.Trim(), "dd/MM/yyyy", null);
            string Loginfromdate = date.ToString("MM/dd/yyyy");

            date = DateTime.ParseExact(txtlogindateto.Text.Trim(), "dd/MM/yyyy", null);
            string Logintodate = date.ToString("MM/dd/yyyy");
            SQLQuery = SQLQuery + " And INDATE between  '" + Loginfromdate + "'" + " and '" + Logintodate + "'";
        }

        Session["searchqryfiche"] = SQLQuery;
         if (Session[SESSION.LOGGED_ROLE].ToString() == "LAD")
         {
             Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
          "window.opener.location.href='LNFicheLanding.aspx';window.close();" + System.Environment.NewLine +
          "</script>");
         }
         else
         {
             Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
     "window.opener.location.href='FicheLanding.aspx';window.close();" + System.Environment.NewLine +
     "</script>");
         }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Session[SESSION.LOGGED_ROLE].ToString() == "LAD")
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
         "window.opener.location.href='LNFicheLanding.aspx';window.close();" + System.Environment.NewLine +
         "</script>");
        }
        else
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
          "window.close();" + System.Environment.NewLine +
          "</script>");
        }
    }
}