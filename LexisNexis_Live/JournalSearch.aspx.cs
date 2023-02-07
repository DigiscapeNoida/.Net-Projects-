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
            binduser();
            bindjid();
          
            //bindDTDitem();
            //bindnaturedemand();
            //bindtat();
           
            Panel panel1 = (Panel)Page.Master.FindControl("panel1");
            panel1.Visible = false;
        }
    }
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
    private void bindjid()
    {
        ddlreview.Items.Clear();
        //SqlParameter[] paramlist = new SqlParameter[1];
        //paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());
        
        DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getJournalInfo);
        ddlreview.DataSource = ds;
        ddlreview.DataTextField = "journal_name";
        ddlreview.DataValueField = "JID";
        ddlreview.DataBind();
        ddlreview.Items.Insert(0, new ListItem("", "-1"));
        //ddlreview.Items.Insert(0, new ListItem("----------", "-1"));
    }
    //private void bindcollection()
    //{
    //    ddlCollection.Items.Clear();
    //    SqlParameter[] paramlist = new SqlParameter[1];
    //    paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

    //    DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getCollection, paramlist);
    //    ddlCollection.DataSource = ds;
    //    ddlCollection.DataTextField = "collection_desc";
    //    ddlCollection.DataValueField = "collectionid";
    //    ddlCollection.DataBind();
    //    ddlCollection.Items.Insert(0, new ListItem("----------", "-1"));
    //}
    //private void binditemtype()
    //{
    //    ddlitemtype.Items.Clear();
    //    SqlParameter[] paramlist = new SqlParameter[1];
    //    paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

    //    DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.itemtypeEncyclo, paramlist);
    //    ddlitemtype.DataSource = ds;
    //    ddlitemtype.DataTextField = "itemtypedetails";
    //    ddlitemtype.DataValueField = "itemtypeid";
    //    ddlitemtype.DataBind();
    //    ddlitemtype.Items.Insert(0, new ListItem("----------", "-1"));
    //}
    //private void bindDTDitem()
    //{
    //    ddldtditem.Items.Clear();
    //    SqlParameter[] paramlist = new SqlParameter[1];
    //    paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

    //    DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.itemDTDEncyclo, paramlist);
    //    ddldtditem.DataSource = ds;
    //    ddldtditem.DataTextField = "dtd_name";
    //    ddldtditem.DataValueField = "dtdid";
    //    ddldtditem.DataBind();
    //    ddldtditem.Items.Insert(0, new ListItem("----------", "-1"));
    //}
    //private void bindnaturedemand()
    //{
    //    ddldemandnature.Items.Clear();
    //    SqlParameter[] paramlist = new SqlParameter[1];
    //    paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

    //    DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.demandNature, paramlist);
    //    ddldemandnature.DataSource = ds;
    //    ddldemandnature.DataTextField = "demanddesc";
    //    ddldemandnature.DataValueField = "demandid";
    //    ddldemandnature.DataBind();
    //    ddldemandnature.Items.Insert(0, new ListItem("----------", "-1"));
    //}
    //private void bindtat()
    //{
    //    ddldelai.Items.Clear();
    //    SqlParameter[] paramlist = new SqlParameter[1];
    //    paramlist[0] = new SqlParameter("Prodid", Session[SESSION.LOGGED_PRODSITE].ToString());

    //    DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.Tat, paramlist);
    //    ddldelai.DataSource = ds;
    //    ddldelai.DataTextField = "tattype";
    //    ddldelai.DataValueField = "tat";
    //    ddldelai.DataBind();
    //    ddldelai.Items.Insert(0, new ListItem("----------", "-1"));
    //}
    protected void btnSend_Click(object sender, EventArgs e)
    {
       
        string user = ddlusersearch.SelectedValue.ToString();
        if (user == "-1")
        {
            user = "";
        }
        string jid = ddlreview.SelectedValue.ToString();
        if (jid == "-1")
        {
            jid = "";
        }

        string stage = ddlstagesearch.SelectedValue.ToString();
        if (stage == "-1")
        {
            stage = "";
        }
       //string SQLQuery = "select a.* from encyclopedia a inner join (select eid,max(duedate) as duedate from encyclopedia group by eid )b on a.eid = b.eid and a.duedate = b.duedate where Active <> 'No' ";
      string SQLQuery = "select a.*,l.firstname+' '+l.lastname as fullname,j.journal_Name  from login l,journalinfo j, Article_Details a inner join (select ArticleID,max(duedate) as duedate from Article_Details group by ArticleID )b on a.ArticleID = b.ArticleID and a.duedate = b.duedate WHERE  a.userid=l.userid and a.jid=j.jid  and a.Active <> 'No'";

       if (jid != "")
       {
           SQLQuery = SQLQuery + " and a.jid = '" + jid + "'";
       }
       if (user != "")
       {
           SQLQuery = SQLQuery + " and a.userid = '" + user + "'";
       }

       if (stage != "")
       {
           SQLQuery = SQLQuery + " and a.stage = '" + stage + "'";
       }

       if (txtauthor.Text.Trim() != "")
       {
           SQLQuery = SQLQuery + " and a.AuthorName = '" + txtauthor.Text.Trim() + "'";
       }
       if (txtid.Text.Trim() != "")
       {
           SQLQuery = SQLQuery + " and a.ArticleID = '" + txtid.Text.Trim() + "'";
       }
       if (txtarticletitle.Text.Trim() != "")
       {
           SQLQuery = SQLQuery + " and a.ArticleTitle = '" + txtarticletitle.Text.Trim() + "'";
       }
       if (txtarticletype.Text.Trim() != "")
       {
           SQLQuery = SQLQuery + " and a.ArticleType = '" + txtarticletype.Text.Trim() + "'";
       }
       if (txtpublicationnumber.Text.Trim() != "")
       {
           SQLQuery = SQLQuery + " and a.Publishing_Number = '" + txtpublicationnumber.Text.Trim() + "'";
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

           SQLQuery = SQLQuery + " And a.IN_DATE between  '" + Loginfromdate + "'" + " and '" + Logintodate + "'";
       }
        // complete date
       if (txtcompletedatefrom.Text.Trim() != "" && txtcompletedateto.Text.Trim() != "")
       {
           DateTime date = DateTime.ParseExact(txtcompletedatefrom.Text.Trim(), "dd/MM/yyyy", null);
           string completefromdate = date.ToString("MM/dd/yyyy");

           date = DateTime.ParseExact(txtcompletedateto.Text.Trim(), "dd/MM/yyyy", null);
           string completetodate = date.ToString("MM/dd/yyyy");

           SQLQuery = SQLQuery + " And a.Delivery_DATE between  '" + completefromdate + "'" + " and '" + completetodate + "'";
       }
        
       Session["searchqryJournal"] = SQLQuery;
       if (Session[SESSION.LOGGED_ROLE].ToString() == "LAD")
       {
           Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
        "window.opener.location.href='LNJournalLanding.aspx';window.close();" + System.Environment.NewLine +
        "</script>");
       }
       else
       {
           Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
    "window.opener.location.href='JournalLanding.aspx';window.close();" + System.Environment.NewLine +
    "</script>");
       }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Session[SESSION.LOGGED_ROLE].ToString() == "LAD")
        {
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
         "window.opener.location.href='LNJournalLanding.aspx';window.close();" + System.Environment.NewLine +
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