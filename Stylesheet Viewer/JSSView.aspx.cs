using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing  ;
using System.Web.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class JSSView : System.Web.UI.Page
{
    string SaveZipFilePath = "";
    XmlDiff XmlDiffObj = new XmlDiff();
    protected int widestData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Server.Transfer("Default.aspx");
        }
        //MenuItem.Visible = false;
        if (!Page.IsPostBack)
        {
            ProductionSiteDropDownList.Items.Add("-Select-");
            ProductionSiteDropDownList.Items.Add("All");
            string ConStr = WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(ConStr);
            SqlCommand cmd    = new SqlCommand();
            SqlDataReader dr;
            try
            {
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    cmd.Connection = con;
                    cmd.CommandText = @"Select distinct Productionsite from elsstylesheet";
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.FieldCount == 1)
                        {
                            while (dr.Read())
                            {

                                ProductionSiteDropDownList.Items.Add(dr[0].ToString());
                            }
                        }
                    }
                    else
                    {
                        //Response.Write("Msg(\"Data Not Inserted Successfully\")");
                    }
                }
            }
            catch (Exception ex)
            {
                //Response.Write("Some Error Found" + ex.Message);
            }
            finally
            {
                con.Close();
            }
            JournalCodeDropDownList.Items.Clear();
            JournalCodeDropDownList.Items.Add("________");
            JournalCodeDropDownList.AutoPostBack = false;

            ViewDropDownList.Items.Clear();
            ViewDropDownList.Items.Add("________");
            ViewDropDownList.AutoPostBack = false;

            /////////////////////Diable Menu
            DisableMenuBar(false);
            /////////////////////Diable Menu

            MultiView1.ActiveViewIndex = MultiView1.Views.Count - 1;
            if (Session["userid"] == null)
            {
                UpdtPrdctnSite.Enabled = false;
                Upld.Enabled = false;
                UpdtPrdctnSite.OnClientClick = "";
            }
            else if (Session["userid"].Equals("admin"))
            {
                UpdtPrdctnSite.Enabled = true;
                Upld.Enabled = true;
            }
            else
            {
                UpdtPrdctnSite.Enabled = false;
                Upld.Enabled           = false;
                UpdtPrdctnSite.OnClientClick = "";
            }
            ViewDropDownList.Attributes.Add("onClick", "ResetDD();");
            ViewDropDownList.Attributes.Add("onChange", "if (CheckViewList()){__doPostBack('ctl00$ContentPlaceHolder1$ViewDropDownList','');}");
            //UpdtPrdctnSite.Attributes.Add("onClick", "CheckConfirm();");
            //DropDownList1.Attributes.Add("onChange", "if (CheckConfirm()){__doPostBack('ctl00$ContentPlaceHolder1$UpdtPrdctnSite','');}");
        }
        SaveAsExcel.Visible = false;
    }
    private void  DIsplayDifferenceView()
    {
        //menuRow.Visible = false;
        if (JournalCodeDropDownList.Text.Equals("-Select-"))
        {
            return;
        }
        string TargetFolderPath = WebConfigurationManager.AppSettings["TargetFolderPath"];
        TargetFolderPath = TargetFolderPath + "\\" + JournalCodeDropDownList.Text;
        
        XmlDiffObj.XmlCompare(TargetFolderPath);

        Repeater1.DataSource = XmlDiffObj.BaseDataList;
        Repeater2.DataSource = XmlDiffObj.SOList;
        Repeater3.DataSource = XmlDiffObj.PITNodeList;
        Repeater4.DataSource = XmlDiffObj.SECHeadList;
        Repeater5.DataSource = XmlDiffObj.S100List;
        Repeater6.DataSource = XmlDiffObj.S200List;
        Repeater7.DataSource = XmlDiffObj.P100List;
        Repeater8.DataSource = XmlDiffObj.S300List;
        Repeater9.DataSource = XmlDiffObj.PrintList;
        Repeater10.DataSource = XmlDiffObj.DisPatchList;
        Repeater14.DataSource = XmlDiffObj.StandardTextList;//by munesh
        Repeater11.DataSource = XmlDiffObj.OtherInstList;
        Repeater12.DataSource = XmlDiffObj.EditiorList;
        Repeater13.DataSource = XmlDiffObj.CUList;//added by Rahul

        Repeater1.DataBind();
        Repeater2.DataBind();
        Repeater3.DataBind();
        Repeater4.DataBind();
        Repeater5.DataBind();
        Repeater6.DataBind();
        Repeater7.DataBind();
        Repeater8.DataBind();
        Repeater9.DataBind();
        Repeater10.DataBind();
        Repeater11.DataBind();
        Repeater12.DataBind();
        Repeater13.DataBind();
        Repeater14.DataBind();
        JIDName.Visible = true;
        JIDName.Text = JournalCodeDropDownList.Text + "(Stylesheet Difference)" ;

        /////////////To describe version no of column
        StringCollection Head = new StringCollection();
        Head.Add("");
        Head.Add("Latest");
        Head.Add("Previous");
        Head.Add("First Previous");
        Head.Add("Second Previous");
       
        if (VerColumn != null)
        {
            int ColCount = XmlDiffObj.XmlCount;
            ColCount =  75 / ColCount;
            double frstCol = 25 / 1;
            TableHeaderRow TH = new TableHeaderRow();
            TH.Controls.Add(new TableHeaderCell());

            TH.Cells[0].Width = Unit.Percentage(frstCol);
            TH.Cells[0].Text = "Stylesheet Version ";
            TH.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            TH.Cells[0].Font.Size = FontUnit.Point(15);
            TH.Cells[0].BackColor = Color.SkyBlue;
            TH.Cells[0].Font.Bold = true;
            TH.Cells[0].ForeColor = Color.Brown;
            for (int i = 1; i <= XmlDiffObj.XmlCount; i++)
            {
                TH.Controls.Add(new TableCell());
                TH.Cells[i].Font.Bold = true;
                TH.Cells[i].ForeColor = Color.Brown;
                TH.Cells[i].Width = Unit.Percentage(ColCount);
                TH.Cells[i].Text = Head[i];
                TH.Cells[i].BackColor = Color.SkyBlue;
                TH.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                TH.Cells[i].Font.Size = FontUnit.Point(15);
            }
            VerColumn.Controls.Add(TH);
        }
        MultiView1.ActiveViewIndex = 1;
    }
    protected void NrmlView_Click(object sender, EventArgs e)
    {
        DIsplayNormalView();
    }

    private void DIsplayNormalView()
    {

        xml1.Visible = true;
        this.ifm.Visible = false;

        JIDName.Visible = false;
        NrmlJIDName.Visible = false;

        MultiView1.ActiveViewIndex = 0;

        string xmlFile = WebConfigurationManager.AppSettings["TargetFolderPath"];
        xmlFile = xmlFile + "\\" + JournalCodeDropDownList.Text;
        //////////////////******Return if jid folder does not exist**********\\\\\\\\\\\\\\\\\\\\\\\
        if (!Directory.Exists(xmlFile)) return;

        string[] FL = Directory.GetFiles(xmlFile,"*.xml");
        //Array.Reverse(FL);
		if (FL.Length>1)
	        FL=MyReverse(FL,".xml");

        if (!Directory.Exists("C:\\Temp1")) Directory.CreateDirectory("C:\\Temp1");

        string StoreXmlFileInTemp = "C:\\Temp1\\" + Path.GetFileName(FL[0]);

        File.Copy(FL[0], StoreXmlFileInTemp, true);
        StringBuilder XmlStr = new StringBuilder (File.ReadAllText(StoreXmlFileInTemp));
        XmlStr.Replace( "&","&amp;");
        XmlStr.Replace( "&amp;amp;","&amp;");
        XmlStr.Replace(       "&amp;#x","&#x");
		XmlStr.Replace("<p><![CDATA[<p><![CDATA[", "<p><![CDATA[");
        XmlStr.Replace("]]></p>]]></p>", "]]></p>");

        XmlStr.Replace("<journalTitle><![CDATA[", "<journalTitle>");
        XmlStr.Replace("]]></journalTitle>", "</journalTitle>");
        
        
        File.WriteAllText(StoreXmlFileInTemp, XmlStr.ToString());
        xml1.DocumentSource = StoreXmlFileInTemp;

		

			/*if (xml1.Document.InnerXml.IndexOf("<pit>") != -1)
			{
				string OldJSSFile = Server.MapPath("~/PrsrvOldJSS.xsl");
				if (File.Exists(OldJSSFile))
					xml1.TransformSource = OldJSSFile;
			}*/
		
        //xml1.DocumentSource = FL[0];
    }
    private void DIsplayDiffView()
    {
        JIDName.Visible = false;
        NrmlJIDName.Visible = false;

        
        MultiView1.ActiveViewIndex = 0;

        string xmlFile = WebConfigurationManager.AppSettings["TargetFolderPath"];
        xmlFile = xmlFile + "\\" + JournalCodeDropDownList.Text;
        //////////////////******Return if jid folder does not exist**********\\\\\\\\\\\\\\\\\\\\\\\
        if (!Directory.Exists(xmlFile)) return;

        string[] FL = Directory.GetFiles(xmlFile, "*.html", SearchOption.TopDirectoryOnly);
        //Array.Reverse(FL);

        if (FL.Length > 0)
        {
            //FL = MyReverse(FL, ".html");

            if (!Directory.Exists("C:\\Temp")) Directory.CreateDirectory("C:\\Temp");

            string StoreXmlFileInTemp = "C:\\Temp\\" + Path.GetFileName(FL[0]);

            File.Copy(FL[0], StoreXmlFileInTemp, true);

            HtmlGenericControl div1 = new HtmlGenericControl("div");
            div1.InnerHtml = File.ReadAllText(StoreXmlFileInTemp);
            ifm.Controls.Add(div1);
            ifm.Visible = true;
        }
      
    }

    private string[] MyReverse(string[] FileLIst, string ext)
    {
        string[] RevFileList = new string[FileLIst.Length];
        List<int> File_No = new List<int>();
        if (FileLIst.Length > 0)
        {
            int SeqNo = 0;
            MatchCollection MatchCol;
            foreach (string fName in FileLIst)
            {
                MatchCol = Regex.Matches(fName, "[0-9]{1,}");
                if (MatchCol.Count > 0)
                {
                    SeqNo = Int32.Parse(MatchCol[MatchCol.Count - 1].Value);
                    File_No.Add(SeqNo);
                }
            }

            File_No.Sort();
            File_No.Reverse();
            int SNo = 0;
            foreach (int SN in File_No)
            {
                foreach (string fName in FileLIst)
                {
                    if (fName.EndsWith("_" + SN + ext))
                    {
                        RevFileList.SetValue(fName, SNo);
                        SNo++;
                        break;
                    }
                }
            }
        }
        return RevFileList;
    }

    protected void Repeater1_ItemCreated(Object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            XmlData xdata = (XmlData)e.Item.DataItem;
            if (xdata != null)
            //if (xdata.ColumnHead.Equals(""))
            if (xdata.PIT && "docheadExpired+sectionExpired".IndexOf(xdata.getColumnHead())!=-1)
            {
                Control thing1 = e.Item.FindControl("thing1");
                Control thing2 = e.Item.FindControl("thing2");
                thing1.Visible = true;
                thing2.Visible = false;
            }
            else
            {
                     Control thing1 = e.Item.FindControl("thing1");
                     TableRow TR = new TableRow ();

                     int ColCount = XmlDiffObj.XmlCount ;
                     if (ColCount==1)  ColCount=75;
                     else if (ColCount == 2) ColCount = 38;
                     else if (ColCount == 3) ColCount = 25;
                     else if (ColCount == 4) ColCount = 19;

                     if (xdata.Column1.Equals("") && xdata.Column2.Equals("") && xdata.Column3.Equals("") && xdata.Column4.Equals("") && xdata.Column5.Equals(""))
                     {
                         TR.Controls.Add(new TableCell());
                         if (xdata.OnlyHead == true)
                         {
                             TR.Cells[0].Text = "<b>" + xdata.ColumnHead + "</b>";
                             TR.Cells[0].ForeColor = Color.Brown;
                         }
                         else
                         {
                             TR.Cells[0].Text = "<b>" + xdata.ColumnHead + "</b>";
                             TR.Cells[0].BackColor = Color.SkyBlue;
                             TR.Cells[0].ForeColor = Color.Black;

                             if ( ColCount == 75)
                                 TR.Cells[0].Width = Unit.Percentage(25);
                             else if (ColCount == 38)
                                 TR.Cells[0].Width = Unit.Percentage(24);
                             else if (ColCount == 25)
                                 TR.Cells[0].Width = Unit.Percentage(25);
                             else if (ColCount == 19)
                                  TR.Cells[0].Width = Unit.Percentage(24); 
                         }
                         thing1.Controls.Add(TR);
                     }
                     else
                         if (!xdata.ColumnHead.Equals(""))
                         {
                             TR.Controls.Add(new TableCell());
                             TR.Cells[0].Text = "<b>" + xdata.ColumnHead + "</b>";
                             if (ColCount == 75)
                                 TR.Cells[0].Width = Unit.Percentage(25);
                             else if (ColCount == 38)
                                 TR.Cells[0].Width = Unit.Percentage(24);
                             else if (ColCount == 25)
                                 TR.Cells[0].Width = Unit.Percentage(25);
                             else if (ColCount == 19)
                                 TR.Cells[0].Width = Unit.Percentage(24); 

                             TR.Cells[0].BackColor = Color.SkyBlue;
                             TR.Cells[0].ForeColor = Color.Black;
                         }
                         else
                         {
                             TR.Controls.Add(new TableCell());
                         }

                     if (XmlDiffObj.XmlCount >= 1)
                          {
                              TR.Controls.Add(new TableCell());
                              TR.Cells[1].Text = xdata.Column1;
                              TR.Cells[1].Width = Unit.Percentage(ColCount);
                          }

                     if (XmlDiffObj.XmlCount >= 2)
                          {
                              TR.Controls.Add(new TableCell());
                              TR.Cells[2].Text = xdata.Column2;
                              TR.Cells[2].Width = Unit.Percentage(ColCount);
                          }

                     if (XmlDiffObj.XmlCount >= 3)
                          {
                              TR.Controls.Add(new TableCell());
                              TR.Cells[3].Text = xdata.Column3;
                              TR.Cells[3].Width = Unit.Percentage(ColCount);
                          }
                     if (XmlDiffObj.XmlCount >= 4)
                          {
                              TR.Controls.Add(new TableCell());
                              TR.Cells[4].Text = xdata.Column4;
                              TR.Cells[4].Width = Unit.Percentage(ColCount);
                          }
                     if (XmlDiffObj.XmlCount >= 5)
                          {
                              TR.Controls.Add(new TableCell());
                              TR.Cells[5].Text = xdata.Column5;
                              TR.Cells[5].Width = Unit.Percentage(ColCount);
                          }
                        
                      if (XmlDiffObj.XmlCount==5)
                          if (!xdata.Column5.Replace("<br/>","").Equals(xdata.Column4.Replace("<br/>","")))
                          {
                              TR.Cells[4].ForeColor = Color.Red;
                              TR.Cells[4].BackColor = Color.Yellow;
                          }

                      if (XmlDiffObj.XmlCount >= 4)
                          if (!xdata.Column4.Replace("<br/>","").Equals(xdata.Column3.Replace("<br/>","")))
                          {
                              TR.Cells[3].ForeColor = Color.Red;
                              TR.Cells[3].BackColor = Color.Yellow;
                          }

                      if (XmlDiffObj.XmlCount >= 3)
                          if (!xdata.Column3.Replace("<br/>", "").Equals(xdata.Column2.Replace("<br/>", "")))
                          {
                              TR.Cells[2].ForeColor = Color.Red;
                              TR.Cells[2].BackColor = Color.Yellow;
                          }

                      if (XmlDiffObj.XmlCount >= 2)
                          if (!xdata.Column2.Replace("<br/>", "").Equals(xdata.Column1.Replace("<br/>", "")))
                          {
                              TR.Cells[1].ForeColor = Color.Red;
                              TR.Cells[1].BackColor = Color.Yellow;
                          }


                      thing1.Controls.Add(TR);
                      if (xdata.PIT)
                      {
                          thing1.Visible = false;
                      }
            }
            
        }
    }
    protected string SpanValue
    {

        get
        {
            int colspan = XmlDiffObj.XmlCount + 1;
            return colspan.ToString();
        }
    }
    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
       // Response.Buffer = true;
      // Response.Cache.SetCacheability(HttpCacheability.NoCache)
     //   Response.ExpiresAbsolute = DateTime.Now().AddDays(-1)
     //  Response.Expires = -1500;
      //  Response.Cache.SetCacheability(HttpCacheability.NoCache);
       // Response.Cache.SetExpires(DateTime.Now); 
        Response.Cookies.Clear();
      //  Response.Cache.SetNoStore();
        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;
        Response.Redirect("Default.aspx");
        
    }
    protected void ProcessSection(object sender, EventArgs e)
    {
        string chkText = ((LinkButton)sender).Text.ToLower();
        if (chkText == "c&amp;u")
            chkText = "cu";
        switch (chkText)
        {
            case "basedata":
                {
                    ProcessPart("baseData");
                    break;
                }
            case "main":
                {
                    ProcessPart("baseData");
                    break;
                }
                // Case "cu"  is added by Rahul
            case "cu":
                {
                    ProcessPart("cu");
                    break;
                }
                //add by munesh
            case "standardText":
                {
                    ProcessPart("standardText");
                    break;
                }
            case "s0":
                {
                    ProcessPart("s0");
                    break;
                }
            case "pit":
                {
                    ProcessPart("pit");
                    break;
                }
            case "s100":
                {
                    ProcessPart("s100");
                    break;
                }
            case "s200":
                {
                    ProcessPart("s200");
                    break;
                }
            case "p100":
                {
                    ProcessPart("p100");
                    break;
                }
            case "s300":
                {
                    ProcessPart("s300");
                    break;
                }

            case "print":
                {
                    ProcessPart("print");
                    break;
                }
            case "despatch":
                {
                    ProcessPart("despatch");
                    break;
                }
            case "dispatch":
                {
                    ProcessPart("despatch");
                    break;
                }
            case "otherinstructions":
                {
                    ProcessPart("otherInstructions");
                    break;
                }
            case "other information":
            {
                ProcessPart("otherInstructions");
                break;
            }
            case "editors":
                {
                    ProcessPart("editors");
                    break;
                }
            case "editor":
                {
                    ProcessPart("editors");
                    break;
                }
        }
    }
    private void ProcessPart(string SectionName)
    {
        //ViewDropDownList.Text = "-Select-";
        NrmlJIDName.Visible        = true;
        if (SectionName.ToUpper().Equals("BASEDATA"))
        {
            NrmlJIDName.Text = JournalCodeDropDownList.Text + " (MAIN Details)";
        }
        else
            NrmlJIDName.Text = JournalCodeDropDownList.Text + " (" + SectionName.ToUpper() + " Details)";

        MultiView1.ActiveViewIndex = 0;
        string xmlFile = WebConfigurationManager.AppSettings["TargetFolderPath"];
        xmlFile = xmlFile + "\\" + JournalCodeDropDownList.Text;
        if (!Directory.Exists(xmlFile)) return ;
        string[] FL = Directory.GetFiles(xmlFile,"*.xml");
        FL=MyReverse(FL,".xml");
        //Array.Reverse(FL);
        Xml xmlObj = new Xml();
        xmlObj.DocumentSource = FL[0];
        string str = xmlObj.Document.GetElementsByTagName(SectionName)[0].OuterXml;
        xmlObj.Document.DocumentElement.InnerXml = str;
        xml1.DocumentContent = xmlObj.Document.OuterXml;
        xml1.TransformSource = Server.MapPath("JSS.xsl");
        
    }
    protected void Upld_Click(object sender, EventArgs e)
    {

        if (Session["userid"] == null)
        {

            //ClientScript.RegisterStartupScript(typeof(string), "Success", "alert('You are not authorized to upload.');", true);
        }
        else if (Session["userid"].Equals("admin"))
        {
            //Response.Redirect("upload/upload.aspx");
            MultiView1.ActiveViewIndex = MultiView1.Views.Count - 2;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "Success", "alert('You are not authorized to upload.');", true);
        }

    }
    protected void UpdtPrdctnSite_Click(object sender, EventArgs e)
    {

        if (Session["userid"] == null)
        {
            //ClientScript.RegisterStartupScript(typeof(string), "Success", "alert('You are not authorized to upload.');", true);
        }
        else if (Session["userid"].Equals("admin"))
        {

            JIDName.Visible = false;
            string InputPath = WebConfigurationManager.AppSettings["TargetFolderPath"].ToString();
            GloVar.UpdateJID(InputPath);
            ClientScript.RegisterStartupScript(typeof(string), "Success", "alert('Production site has been updated successfully.');", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "Success", "alert('You are not authorized to update production site.');", true);
        }

    }
    protected void ProductionSiteDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
            NrmlJIDName.Visible = false;
            
            ////////////////////Menu Disable
            DisableMenuBar(false);
            ////////////////////Menu Disable

            JournalCodeDropDownList.Items.Clear();
            ViewDropDownList.Items.Clear();
            ViewDropDownList.Items.Add("________");
            ViewDropDownList.AutoPostBack = false;

            string sqlStr = "select JID,JOURNALTITLE,PRODUCTIONSITE from elsstylesheet";
            string ConStr = WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            DataSet DS    = new DataSet ();

            //if (Cache["DS"] == null)
           // {
                SqlDataAdapter DAdapter = new SqlDataAdapter(sqlStr, ConStr);
                DAdapter.Fill(DS);
               // Cache.Insert("DS", DS,  null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 0));
           // }
           // else
           // {
               // DS = (DataSet)Cache.Get("DS");
           // }

            DataView dv = new DataView(DS.Tables[0]);
            if (!ProductionSiteDropDownList.Text.Equals("All"))
            {
                dv.RowFilter = "PRODUCTIONSITE='" + ProductionSiteDropDownList.Text + "'";
            }
            dv.Sort = "JID";

            JournalCodeDropDownList.DataSource = dv;
            JournalCodeDropDownList.DataValueField = "JID";
            JournalCodeDropDownList.DataBind();
            JournalCodeDropDownList.Items.Insert(0, "-Select-");
            JournalCodeDropDownList.AutoPostBack = true;
            MultiView1.ActiveViewIndex = MultiView1.Views.Count - 1;
}
    protected void ViewDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ViewDropDownList.Text.Equals("Normal View"))
        {
            DisableMenuBar(false);
            NrmlJIDName.Visible = false;
            DIsplayNormalView();
        }
        else if (ViewDropDownList.Text.Equals("Difference View"))
        {
            DisableMenuBar(false);
            DIsplayDiffView();
            //DIsplayDifferenceView();
        }
        else if (ViewDropDownList.Text.Equals("Custom View"))
        {
            DisableMenuBar(true);
            MultiView1.ActiveViewIndex = MultiView1.Views.Count - 1;
        }

    }
    protected void JournalCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

        NrmlJIDName.Visible = false;
        MultiView1.ActiveViewIndex = 0;

        ////////////////////////menuDisable
        DisableMenuBar(false);
        ////////////////////////

        
        ViewDropDownList.Items.Clear();
        ViewDropDownList.Items.Add("-Select-");
        ViewDropDownList.Items.Add("Normal View");
        ViewDropDownList.Items.Add("Difference View");
        //ViewDropDownList.Items.Add("Custom View");
        
        MultiView1.ActiveViewIndex = MultiView1.Views.Count - 1;
    }
    protected void JurnalLst_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 2;
        ////////////////////////menuDisable
        DisableMenuBar(false);
        ////////////////////////
        //menuRow.Disabled = true;
        
        ViewDropDownList.Items.Clear();
        ViewDropDownList.Items.Add("________");
        if (JournalCodeDropDownList.Items.Count > 1)
        {
            JournalCodeDropDownList.Text = "-Select-";
        }
        //ViewDropDownList.Items.Add("-Select-");
        //ViewDropDownList.Items.Add("Normal View");
        //ViewDropDownList.Items.Add("Difference View");

        
        
        string sqlStr = "select distinct JID,JOURNALTITLE 'JOURNAL TITLE',PRODUCTIONSITE 'PRODUCTION SITE' from elsstylesheet";
        //string sqlStr = "select JID,JOURNALTITLE,PRODUCTIONSITE from elsstylesheet";
        string ConStr = WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        DataSet DS = new DataSet();
        //SqlDataAdapter DAdapter = new SqlDataAdapter(sqlStr, ConStr);
        //DAdapter.Fill(DS);

       // if (Cache["DS1"] == null)
        {
            SqlDataAdapter DAdapter = new SqlDataAdapter(sqlStr, ConStr);
            DAdapter.Fill(DS);
            //Cache.Insert("DS1", DS, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 10, 0));

        }
        //else
        {
            //DS = (DataSet)Cache.Get("DS1");
        }
        
        JIDDetailDatagrid.DataSource = DS.Tables[0];
        JIDDetailDatagrid.DataBind();
        SaveAsExcel.Visible = true;
        //JIDDetailDatagrid.Columns[0].HeaderText = "JID";
        //JIDDetailDatagrid.Columns[1].HeaderText = "Journal Title";
        //JIDDetailDatagrid.Columns[2].HeaderText = "Production Site";
        
    }
    
    protected void JIDDetailDatagrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        //string sqlStr = "select JID,JOURNALTITLE,PRODUCTIONSITE from elsstylesheet";
        string sqlStr = "select JID,JOURNALTITLE 'JOURNAL TITLE',PRODUCTIONSITE 'PRODUCTION SITE' from elsstylesheet";
        string ConStr = WebConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        DataSet DS = new DataSet();
        //SqlDataAdapter DAdapter = new SqlDataAdapter(sqlStr, ConStr);
        

        if (Cache["DS1"] == null)
        {
            SqlDataAdapter DAdapter = new SqlDataAdapter(sqlStr, ConStr);
            DAdapter.Fill(DS);
            Cache.Insert("DS", DS, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 10, 0));
        }
        else
        {
            DS = (DataSet)Cache.Get("DS1");
        }
        DataView DV = new DataView(DS.Tables[0]);

        if (ViewState["SelectedValue"] == null)
        {
            ViewState["SelectedValue"] = e.SortDirection.ToString();
            DV.Sort = e.SortExpression;
        }
        else
        {
            DV.Sort = e.SortExpression + " DESC";
            ViewState.Remove("SelectedValue");
        }
        JIDDetailDatagrid.DataSource = DV;
        JIDDetailDatagrid.DataBind();

        SaveAsExcel.Visible = true;
    }
    protected void HomeLinkButton_Click(object sender, EventArgs e)
    {
        Server.Transfer("JSSView.aspx");
    }
    protected void JIDDetailDatagrid_DataBound(object sender, EventArgs e)
    {
        GridView GridViewobj = (GridView)sender;
        if (GridViewobj.Columns.Count ==4)
        {
            GridViewobj.Columns[0].ItemStyle.Width = Unit.Percentage(8);
            GridViewobj.Columns[1].ItemStyle.Width = Unit.Percentage(10);
            GridViewobj.Columns[2].ItemStyle.Width = Unit.Percentage(70);
            GridViewobj.Columns[3].ItemStyle.Width = Unit.Percentage(12);
        }
        //GridViewobj.HeaderRow.Cells[0].Text = "S.NO.";
        //GridViewobj.HeaderRow.Cells[1].Text = "JID";
        //GridViewobj.HeaderRow.Cells[2].Text = "Journal Title";
        //GridViewobj.HeaderRow.Cells[3].Text = "Production Site";
        //GridViewobj.AllowSorting = true;        
    }

    private void DisableMenuBar(bool boolValue)
    {
        MAINHyperLink.Enabled = boolValue;
        PITHyperLink.Enabled = boolValue;
        S0HyperLink.Enabled = boolValue;
        S100HyperLink.Enabled = boolValue;
        S200HyperLink.Enabled = boolValue;
        S300HyperLink.Enabled = boolValue;
        DispatchHyperLink.Enabled = boolValue;
        EditorHyperLink.Enabled = boolValue;
        OthrInfoHyperLink.Enabled = boolValue;
        PrintHyperLink.Enabled = boolValue;
        P100HyperLink.Enabled = boolValue;
        CUHyperLink.Enabled = boolValue;
        
    }
     protected void ProcessSaveAsExcel(object sender, EventArgs e)
     {
         PrepareGridViewForExport(JIDDetailDatagrid);
         string attachment = "attachment; filename=Report.xls";
         string style = @"<style> .text { mso-number-format:\@; } </style> ";
         Response.ClearContent();
         Response.AddHeader("content-disposition", attachment);
         Response.ContentType = "application/ms-excel";
         Response.ContentEncoding = System.Text.Encoding.UTF8;
         StringWriter sw = new StringWriter();
         HtmlTextWriter htw = new HtmlTextWriter(sw);
         JIDDetailDatagrid.RenderControl(htw);
         Response.Write(style);
         Response.Write(sw.ToString());
         Response.End();
    }
     public override void VerifyRenderingInServerForm(Control control)
    {



     }
     private void PrepareGridViewForExport(Control gv)
     {

         LinkButton lb = new LinkButton();

         Literal l = new Literal();

         string name = String.Empty;

         for (int i = 0; i < gv.Controls.Count; i++)
         {

             if (gv.Controls[i].GetType() == typeof(LinkButton))
             {

                 l.Text = (gv.Controls[i] as LinkButton).Text;

                 gv.Controls.Remove(gv.Controls[i]);

                 gv.Controls.AddAt(i, l);

             }

             else if (gv.Controls[i].GetType() == typeof(DropDownList))
             {

                 l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;

                 gv.Controls.Remove(gv.Controls[i]);

                 gv.Controls.AddAt(i, l);

             }

             else if (gv.Controls[i].GetType() == typeof(CheckBox))
             {

                 l.Text = (gv.Controls[i] as CheckBox).Checked ? "True" : "False";

                 gv.Controls.Remove(gv.Controls[i]);

                 gv.Controls.AddAt(i, l);

             }

             if (gv.Controls[i].HasControls())
             {

                 PrepareGridViewForExport(gv.Controls[i]);

             }

         }

     }

     protected void Button1_Click(object sender, EventArgs e)
     {
         string DesPath = "c:\\Temp1\\";

         string TargetFolderPath = WebConfigurationManager.AppSettings["TargetFolderPath"];
         string OldFolderPath = WebConfigurationManager.AppSettings["OldFolderPath"];
         SaveZipFilePath = "c:\\Temp1\\" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond + ".zip"; ;

         if (!FileUpload1.FileName.Equals(""))
         {
             if (!Directory.Exists(DesPath))
             {
                 Directory.CreateDirectory(DesPath);
             }
             //SaveZipFilePath = DesPath + "\\" + FileUpload1.FileName;
             FileUpload1.SaveAs(SaveZipFilePath);
             FileInfo finfo = new FileInfo(SaveZipFilePath);
             if (FileUpload1.PostedFile.ContentLength == finfo.Length)
             {
                 LogDetail.SeqCount = 0;
                 XmlFileInfo obj = new XmlFileInfo(SaveZipFilePath, TargetFolderPath, OldFolderPath);

                 try
                 {
                     if (obj.SaveFileInZip())
                     {
                         LblUploadedFileStatus.Text = "Uploaded file status";
                         LblFileName.Text = "File Name:";
                         LblTotalFiles.Text = "Total files in zip file:";
                         LblXMLFILES.Text = "No. of xml files:";
                         LblTXTFILES.Text = "No. of text files:";

                         LblFileNameValue.Text = FileUpload1.PostedFile.FileName;
                         LblTotalFilesValue.Text = obj.TotalFilesCount.ToString();
                         LblXMLFILESValue.Text = obj.XmlFilesCount.ToString();

                         if (obj.TotalFilesCount != obj.XmlFilesCount)
                         {
                             LblXMLFILESValue.ForeColor = Color.Red;
                             LblXMLFILESValue.Font.Bold = true;
                         }
                         LblTXTFILESValue.Text = obj.TextFilesCount.ToString();


                         LogStatusRepeater.Visible = true;
                         LogStatusRepeater.DataSource = obj.Log;
                         LogStatusRepeater.DataBind();

                         string InputPath = WebConfigurationManager.AppSettings["TargetFolderPath"].ToString();
                         GloVar.UpdateJID(InputPath);
                     }
                     else
                     {
                         Response.Write("Failed :: ");
                     }
                     FileUpload1.Dispose();
                     File.Delete(SaveZipFilePath);
                 }
                 catch (Exception ex)
                 {
                    Response.Write(ex.Message);
                 }

             }
         }
     }

     protected void LogStatusRepeater_ItemCreated(Object sender, RepeaterItemEventArgs e)
     {
         if (e.Item.DataItem != null)
         {
             LogDetail LogDetailObj = (LogDetail)e.Item.DataItem;
             if (LogDetailObj.Result.Equals("Identical"))
             {
                 Label lbl = (Label)e.Item.FindControl("statusLabel");
                 lbl.ForeColor = Color.Red;
                 lbl.Font.Bold = true;
             }
         }
     }


     protected void JIDDetailDatagrid_RowDataBound(object sender, GridViewRowEventArgs e)
     {
                System.Data.DataRowView drv;
                drv = (System.Data.DataRowView)e.Row.DataItem;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (drv != null)
                    {
                        String catName = drv[1].ToString();
                        Response.Write(catName + "/");
                        int catNameLen = catName.Length;
                        if (catNameLen > widestData)
                        {
                            widestData = catNameLen;
                            //JIDDetailDatagrid.Columns[1].ItemStyle.Width = widestData * 30;
                            //JIDDetailDatagrid.Columns[2].ItemStyle.Wrap = false;
                        }

                    }
                }

     }
}