using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TD.Data;

public partial class EncyclopediasEntry1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindcollection();
            binditemtype();
            bindDTDitem();
            bindnaturedemand();
            bindtat();
            bindcategory();
            txtapplicationname.Text = Session[SESSION.LOGGED_USER_NAME].ToString();
            txtapplicationname.Enabled = false;
            Panel panel1 = (Panel)Page.Master.FindControl("panel1");
            panel1.Visible = false;

             string id = Request.QueryString["EID"].ToString();
             hiddeenval.Text = id;
            SqlParameter[] paramlist = new SqlParameter[1];
            paramlist[0] = new SqlParameter("EID", id);
            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getencycloforedit, paramlist);
          
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcategory.SelectedIndex = ddlcategory.Items.IndexOf(ddlcategory.Items.FindByText(ds.Tables[0].Rows[0]["category"].ToString()));

                ddldemandnature.SelectedIndex = ddldemandnature.Items.IndexOf(ddldemandnature.Items.FindByText(ds.Tables[0].Rows[0]["DEMANDTYPE"].ToString()));
                ddlCollection.SelectedIndex = ddlCollection.Items.IndexOf(ddlCollection.Items.FindByText(ds.Tables[0].Rows[0]["COLLECTION"].ToString()));
                ddlitemtype.SelectedIndex = ddlitemtype.Items.IndexOf(ddlitemtype.Items.FindByText(ds.Tables[0].Rows[0]["ITEMTYPE"].ToString()));
                ddldtditem.SelectedIndex = ddldtditem.Items.IndexOf(ddldtditem.Items.FindByText(ds.Tables[0].Rows[0]["DTD"].ToString()));
                ddldelai.SelectedIndex = ddldelai.Items.IndexOf(ddldelai.Items.FindByText(ds.Tables[0].Rows[0]["tat"].ToString()));

               
              

                txttitle.Text = ds.Tables[0].Rows[0]["DTITLE"].ToString();
                txtfolio.Text = ds.Tables[0].Rows[0]["FOLIO"].ToString();
              
                txtapplicationname.Text = ds.Tables[0].Rows[0]["APPLICANTNAME"].ToString();
                txtsupnotification.Text = ds.Tables[0].Rows[0]["NOTIFICATION"].ToString();
                txtcomment.Text = ds.Tables[0].Rows[0]["COMMENTS"].ToString();

               
                txtreference.Text = ds.Tables[0].Rows[0]["reference"].ToString();
            }
        }
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
    protected void btnSend_Click(object sender, EventArgs e)
    {
        /*
        if (FileUpload1.HasFile)
        {
           
            string filename = Path.GetFileName(FileUpload1.FileName);

            string ext = Path.GetExtension(FileUpload1.FileName);
            if (ext == ".zip" || ext == ".rar" || ext == ".7zip")
            {
                string Expath = ConfigurationSettings.AppSettings["UploadFilePath"];
                */


                try
                {
                    /*
                    if (!Directory.Exists(Expath))
                    {
                        Directory.CreateDirectory(Expath);
                    }

                    // get max value
                    string EID = "";
                    // DateTime dt=DateTime.Now;
                    DataSet idtable = new DataSet();
                    idtable = DataAccess.ExecuteDataSetSP(SPNames.getEncycloid);
                    if (idtable.Tables[0].Rows.Count > 0)
                    {
                        string tidval = "";
                        string tempeid = idtable.Tables[0].Rows[0]["EID"].ToString();
                        if (tempeid != "")
                        {
                            string tt = tempeid.Substring(tempeid.Length - 5, 5);
                            int val = Convert.ToInt32(tt);
                            val = val + 1;
                            if (val.ToString().Length == 1)
                            {
                                tidval = "0000" + val.ToString();
                            }
                            else if (val.ToString().Length == 2)
                            {
                                tidval = "000" + val.ToString();
                            }
                            else if (val.ToString().Length == 3)
                            {
                                tidval = "00" + val.ToString();
                            }
                            else if (val.ToString().Length == 4)
                            {
                                tidval = "0" + val.ToString();
                            }
                            else if (val.ToString().Length == 5)
                            {
                                tidval = val.ToString();
                            }
                            EID = "ENC" + tidval;//dt.Year + dt.Month +
                        }
                        else
                        {
                            EID = "ENC" + "00001";//+ dt.Year + dt.Month
                        }
                    }
                    else
                    {
                        EID = "ENC" + "00001";// + dt.Year + dt.Month
                    }
                    string ERID = "";
                    DataSet eridtable = new DataSet();
                    eridtable = DataAccess.ExecuteDataSetSP(SPNames.getEncycloERID);
                    if (eridtable.Tables[0].Rows.Count > 0)
                    {
                        string tidval = "";
                        string temperid = eridtable.Tables[0].Rows[0]["ERID"].ToString();
                        if (temperid != "")
                        {
                            string tt = temperid.Substring(temperid.Length - 5, 5);
                            int val = Convert.ToInt32(tt);
                            val = val + 1;
                            if (val.ToString().Length == 1)
                            {
                                tidval = "0000" + val.ToString();
                            }
                            else if (val.ToString().Length == 2)
                            {
                                tidval = "000" + val.ToString();
                            }
                            else if (val.ToString().Length == 3)
                            {
                                tidval = "00" + val.ToString();
                            }
                            else if (val.ToString().Length == 4)
                            {
                                tidval = "0" + val.ToString();
                            }
                            else if (val.ToString().Length == 5)
                            {
                                tidval = val.ToString();
                            }
                            ERID = "ERNC" + tidval;//dt.Year + dt.Month +
                        }
                        else
                        {
                            ERID = "ERNC" + "00001";//+ dt.Year + dt.Month
                        }
                    }
                    else
                    {
                        ERID = "ERNC" + "00001";// + dt.Year + dt.Month
                    }


                    filename = EID + "_" + "0" + "." + ext;


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
                    */

                    // for calculate TAT
                    ////////  DateTime dt = System.DateTime.Now;
                    int numofdays = 0;
                    if (ddldelai.SelectedItem.Text == "Urgent")
                    {
                        numofdays = Convert.ToInt16(ddldelai.SelectedValue.ToString());
                    }
                    else if (ddldelai.SelectedItem.Text == "Courant")
                    {
                        numofdays = Convert.ToInt16(ddldelai.SelectedValue.ToString());
                    }
                    else if (ddldelai.SelectedItem.Text == "Étendu")
                    {
                        numofdays = Convert.ToInt16(ddldelai.SelectedValue.ToString());
                    }
                    else
                    {
                        numofdays = 1;
                    }


                    System.DateTime dt = System.DateTime.Now;
                    System.DateTime dt1;

                    int nod = numofdays;
                    int counter = 0;
                    for (int i = 0; i <= nod; i++)
                    {
                        System.DateTime dttemp = dt.AddDays(i);
                        if (dttemp.DayOfWeek == DayOfWeek.Saturday || dttemp.DayOfWeek == DayOfWeek.Sunday || ConfigurationManager.AppSettings["Holiday"].IndexOf(dttemp.Date.ToString("dd/MM/")) != -1)
                        {
                            counter++;
                        }
                    }



                    int TotalDay = nod + counter;

                    dt1 = dt.AddDays(TotalDay);

                    while (true)
                    {
                        if (dt1.DayOfWeek == DayOfWeek.Saturday || dt1.DayOfWeek == DayOfWeek.Sunday || ConfigurationManager.AppSettings["Holiday"].IndexOf(dt1.Date.ToString("dd/MM/")) != -1)
                            dt1 = dt1.AddDays(1);
                        else
                        {
                            break;
                        }
                    }

                    //Console.WriteLine("dt:" + dt.ToString());
                    //Console.WriteLine("Input Day: " + dt.DayOfWeek + "::: Target Date: " + dt1.DayOfWeek);
                    //Console.WriteLine("dt1:" + dt1.ToString()); 





                    /* System.DateTime dt = System.DateTime.Now;

                     System.DateTime dt1;
                     //dt = dt.AddDays(1); 

                     if (dt.DayOfWeek == DayOfWeek.Friday)
                     {
                         numofdays = numofdays + 2;
                         dt1 = dt.AddDays(numofdays);
                     }
                     else if (dt.DayOfWeek == DayOfWeek.Saturday)
                     {
                         numofdays = numofdays + 2;
                         dt1 = dt.AddDays(numofdays);
                     }
                     else if (dt.DayOfWeek == DayOfWeek.Sunday)
                     {
                         numofdays = numofdays + 1;
                         dt1 = dt.AddDays(numofdays);
                     }
                     else
                         dt1 = dt.AddDays(numofdays);*/
                    /////////////////////////////////





                    SqlParameter[] paramList = new SqlParameter[16];
                    paramList[0] = new SqlParameter("EID", hiddeenval.Text);
                    paramList[1] = new SqlParameter("DTITLE", txttitle.Text.Trim());
                    paramList[2] = new SqlParameter("FOLIO", txtfolio.Text.Trim());
                    paramList[3] = new SqlParameter("DEMANDTYPE", ddldemandnature.SelectedItem.Text.Replace("----------", ""));
                    paramList[4] = new SqlParameter("COLLECTION", ddlCollection.SelectedItem.Text.Replace("----------", ""));
                    paramList[5] = new SqlParameter("ITEMTYPE", ddlitemtype.SelectedItem.Text.Replace("----------", ""));
                    paramList[6] = new SqlParameter("DTD", ddldtditem.SelectedItem.Text.Replace("----------", ""));
                    paramList[7] = new SqlParameter("APPLICANTNAME", txtapplicationname.Text.Trim());
                    paramList[8] = new SqlParameter("NOTIFICATION", txtsupnotification.Text.Trim());
                    paramList[9] = new SqlParameter("COMMENTS", txtcomment.Text.Trim());
                    //  paramList[10] = new SqlParameter("INDATE", DateTime.Now);
                    //  paramList[1] = new SqlParameter("ALLOCATION_DATE", "Success");
                    paramList[10] = new SqlParameter("DUEDATE", Convert.ToDateTime(dt1));//txtduedate.Text
                    //  paramList[3] = new SqlParameter("REVISED_DUEDATE", "");
                    //  paramList[1] = new SqlParameter("DELIVERED_DATE", "Success");
                    paramList[11] = new SqlParameter("ITERATION", Convert.ToInt16("0"));
                    // paramList[3] = new SqlParameter("RETURNDATE", "");
                    //  paramList[2] = new SqlParameter("PAGECOUNT", DateTime.Now);
                    paramList[12] = new SqlParameter("STAGE", "En attente prod");
                    paramList[13] = new SqlParameter("tat", ddldelai.SelectedItem.Text.Replace("----------", ""));
                    paramList[14] = new SqlParameter("reference", txtreference.Text.Trim());
                  //  paramList[15] = new SqlParameter("filesname", filename);
                    paramList[15] = new SqlParameter("category", ddlcategory.SelectedItem.Text.Replace("----------", ""));
                   // paramList[17] = new SqlParameter("erid", ERID);
                    int rowAffected = DataAccess.ExecuteNonQuerySP(SPNames.Updateencyclo, paramList);
                    if (rowAffected > 0)
                    {
                        lblmessage.Text = "Item ajouté";

                        //  btnFinish.Visible = true;
                    }
                    else
                    {
                        lblmessage.Text = "error";
                        return;
                    }
                    ddlcategory.SelectedIndex = -1;
                    txttitle.Text = "";
                    txtfolio.Text = "";
                    ddldemandnature.SelectedIndex = -1;
                    ddlCollection.SelectedIndex = -1;
                    ddlitemtype.SelectedIndex = -1;
                    ddldtditem.SelectedIndex = -1;
                    txtapplicationname.Text = "";
                    txtsupnotification.Text = "";
                    txtcomment.Text = "";
                    txtduedate.Text = "";
                    ddldelai.SelectedIndex = -1;
                    txtreference.Text = "";

                    Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
       "window.opener.location.href='EncyclopediasLanding.aspx';window.close();" + System.Environment.NewLine +
       "</script>");

                }
                catch (Exception ex)
                {
                }

/*
            }
            else
            {
                string message = "Merci de charger une pièce jointe valide (format zip)";
                Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
                   "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
                   "</script>");
            }
        }
        else
        {
            string message = "choisir le dossier";
            Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
               "alert(" + "\"" + message + "\"" + ");" + System.Environment.NewLine +
               "</script>");
        }
        */

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAllFields();
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
       "window.close();" + System.Environment.NewLine +
       "</script>");
    }
    protected void ClearAllFields()
    {
        ddlcategory.SelectedIndex = -1;
        txttitle.Text = "";
        txtfolio.Text = "";
        ddldemandnature.SelectedIndex = -1;
        ddlCollection.SelectedIndex = -1;
        ddlitemtype.SelectedIndex = -1;
        ddldtditem.SelectedIndex = -1;
        txtapplicationname.Text = "";
        txtsupnotification.Text = "";
        txtcomment.Text = "";
        txtduedate.Text = "";
        ddldelai.SelectedIndex = -1;
        txtreference.Text = "";
        lblmessage.Text = "";
    }
}