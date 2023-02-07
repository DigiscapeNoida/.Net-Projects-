using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using TD.Data;

public partial class MasterContent : System.Web.UI.MasterPage
{
    private ResourceManager rm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[SESSION.LOGGED_USER] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack)
        {
            CultureInfo ci;
            string lang = "";
            if (Session["lang"] != null)
            {
                lang = Session["lang"].ToString();
            }
            else
            {
                //lang = "en-US";
                Session["lang"] = "fr-FR";
                lang = "fr-FR";
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            rm = new ResourceManager("Resources.String", Assembly.Load("App_GlobalResources"));
            ci = Thread.CurrentThread.CurrentCulture;
            LoadData(ci);
           // bindtask();
            //lblname.Text = Session[SESSION.LOGGED_USER_NAME].ToString();
        }
       
    }

    public void LoadData(CultureInfo ci)
    {

        LinkButton lnkdos = (LinkButton)MainContent.FindControl("lnkdosier");
        LinkButton lnkencyclo = (LinkButton)MainContent.FindControl("lnkencyclo");
        LinkButton lnkfiche = (LinkButton)MainContent.FindControl("lnkfiche");
        LinkButton lnkjournal = (LinkButton)MainContent.FindControl("lnkjournal");
        
        if (lnkdos != null)
        {
            lnkdos.Text = rm.GetString("Dossiers", ci);
        }
        if (lnkencyclo != null)
        {
            lnkencyclo.Text = rm.GetString("Encyclopedia", ci);
        }
        if (lnkfiche != null)
        {
            lnkfiche.Text = rm.GetString("Fiches", ci);
        }
        if (lnkjournal != null)
        {
            lnkjournal.Text = rm.GetString("Journals", ci);
        }

        //if (Session[SESSION.LOGGED_PRODSITE].ToString() != null && Session[SESSION.LOGGED_PRODSITE].ToString() != "" && Session[SESSION.LOGGED_PRODSITE] !=null)
        //{

        Button btnRefresh = (Button)MainContent.FindControl("btnRefresh");
        if (btnRefresh != null)
        {
            btnRefresh.Text = rm.GetString("Refresh", ci);
        }
            Button btnSubmit = (Button)MainContent.FindControl("btnSubmit");
            Button btnExportExcel = (Button)MainContent.FindControl("btnExportExcel");
            Button btnExportExcelAll = (Button)MainContent.FindControl("btnExportExcelAll");
            Button btnLookfor = (Button)MainContent.FindControl("btnLookfor");
            Button btnLogout = (Button)MainContent.FindControl("btnLogout");
            Button btnsendprod = (Button)MainContent.FindControl("btnsendprod");
            Button btnremove = (Button)MainContent.FindControl("btnremove");
            Button btnremovefilter = (Button)MainContent.FindControl("btnremovefilter");

            Label lblTask = (Label)MainContent.FindControl("lblTask");
            Label lblUserName = (Label)MainContent.FindControl("lblUserName");
            Label lblChooseProduct = (Label)MainContent.FindControl("lblChooseProduct");
            Label lblDeclination = (Label)MainContent.FindControl("lblDeclination");
            Label lbllblWriting = (Label)MainContent.FindControl("lblWriting");

            Label lblcollection = (Label)MainContent.FindControl("lblcollection");

            if (btnremovefilter != null)
            {
                btnremovefilter.Text = rm.GetString("Remove_all_filters", ci);
            }


            GridView gv_ViewOrders = (GridView)MainContent.FindControl("grdViewOrders");
            if (btnSubmit != null)
            {
                btnSubmit.Text = rm.GetString("Insert", ci);
            }
            if (btnExportExcel != null)
            {
                btnExportExcel.Text = rm.GetString("export_this_page", ci);
            }
            if (btnExportExcelAll != null)
            {
                btnExportExcelAll.Text = rm.GetString("Export_All", ci);
            }
            if (btnLookfor != null)
            {
                btnLookfor.Text = rm.GetString("Look_For", ci);
            }
            if (btnLogout != null)
            {
                btnLogout.Text = rm.GetString("Log_Out", ci);
            }
            if (btnsendprod != null)
            {
                btnsendprod.Text = rm.GetString("Send_prod", ci);
            }
            if (btnremove != null)
            {
                btnremove.Text = rm.GetString("Remove", ci);
            }
            if (lblUserName != null)
            {
                lblUserName.Text = rm.GetString("connected_Username", ci);
            }
            if (lblTask != null)
            {
                lblTask.Text = rm.GetString("Task", ci);
            }
            if (lblheading != null)
            {
                lblheading.Text = rm.GetString("Online_Production_Tracking", ci);
            }
            if (lblheading2 != null)
            {
                lblheading2.Text = rm.GetString("System", ci);
            }
            if (txtCopyRight != null)
            {
                txtCopyRight.Text = rm.GetString("Copyright", ci);
            }
            if (lblcopyRight1 != null)
            {
                lblcopyRight1.Text = rm.GetString("All_rights_reserved", ci);
            }
            if (lbllblWriting != null)
            {
                lbllblWriting.Text = rm.GetString("Category", ci);
            }
            Button btnComplete = (Button)MainContent.FindControl("btnComplete");
            if (btnComplete != null)
            {
                btnComplete.Text = rm.GetString("Complete", ci);
            }

            if (lblDeclination != null)
            {
                lblDeclination.Text = rm.GetString("Declination", ci);
            }
            Button btnSearch = (Button)MainContent.FindControl("btnLookfor");
            if (btnSearch != null)
            {
                btnSearch.Text = rm.GetString("Look_For", ci);
            }
            Button btnlncomplete = (Button)MainContent.FindControl("btnlncomplete");
            Button btntdcomplete = (Button)MainContent.FindControl("btntdcomplete");
            if (btnlncomplete != null)
            {
                btnlncomplete.Text = rm.GetString("Complete", ci);
            }
            if (btntdcomplete != null)
            {
                btntdcomplete.Text = rm.GetString("Complete", ci);
            }
            Label lblChoseProduct = (Label)MainContent.FindControl("lblChoseProduct");
            if (lblChooseProduct != null)
            {
                lblChooseProduct.Text = rm.GetString("choose_your_product", ci);
            }
            Label lblChoseProductencyclo = (Label)MainContent.FindControl("lblChoseProductencyclo");
            if (lblChoseProductencyclo != null)
            {
                lblChoseProductencyclo.Text = rm.GetString("choose_your_product", ci);
            }
            if (lblcollection != null)
            {
                lblcollection.Text = rm.GetString("collection", ci);
            }
            
            Button btnsearch = (Button)MainContent.FindControl("btnSearch");
        
            if (btnSearch != null)
            {
                btnSearch.Text = rm.GetString("Search", ci);
            }
            if (btnsearch != null)
            {
                btnsearch.Text = rm.GetString("Search", ci);
            }

            Label lblHeadingsearch = (Label)MainContent.FindControl("lblHeadingsearch");
            if (lblHeadingsearch != null)
            {
                lblHeadingsearch.Text = rm.GetString("Search_dossier", ci);
            }
            Label lblHeadingsearche = (Label)MainContent.FindControl("lblHeadingsearche");
            if (lblHeadingsearche != null)
            {
                lblHeadingsearche.Text = rm.GetString("Search_Encyclopedia", ci);
            }
            Label lblHeadingsearchjournal = (Label)MainContent.FindControl("lblHeadingsearchjournal");
            if (lblHeadingsearchjournal != null)
            {
                lblHeadingsearchjournal.Text = rm.GetString("Search_Journal", ci);
            }
            if (Session[SESSION.LOGGED_PRODSITE].ToString() == "DS")
            {

                if (gv_ViewOrders != null)
                {

                    gv_ViewOrders.Columns[0].HeaderText = rm.GetString("ID", ci);
                    gv_ViewOrders.Columns[1].HeaderText = rm.GetString("Declination", ci);
                    gv_ViewOrders.Columns[2].HeaderText = rm.GetString("Folder_Title", ci);

                    gv_ViewOrders.Columns[3].HeaderText = rm.GetString("Journal_Author", ci);

                    gv_ViewOrders.Columns[4].HeaderText = rm.GetString("Demand_Type", ci);
                    gv_ViewOrders.Columns[5].HeaderText = rm.GetString("Duration", ci);
                    gv_ViewOrders.Columns[6].HeaderText = rm.GetString("Iteration", ci);

                    gv_ViewOrders.Columns[7].HeaderText = rm.GetString("In_Date", ci);

                    gv_ViewOrders.Columns[8].HeaderText = rm.GetString("Return_DateJournal", ci);//Return_Date

                    gv_ViewOrders.Columns[9].HeaderText = rm.GetString("Complete_date", ci);

                    gv_ViewOrders.Columns[10].HeaderText = rm.GetString("Page_Count", ci);
                    gv_ViewOrders.Columns[11].HeaderText = rm.GetString("Stage", ci);
                    gv_ViewOrders.Columns[12].HeaderText = rm.GetString("Name_of_requester", ci);
                    gv_ViewOrders.Columns[13].HeaderText = rm.GetString("Comments", ci);
                    gv_ViewOrders.Columns[14].HeaderText = rm.GetString("LN_Attachments", ci);


                   
                   
                    
                }
                // gv_ViewOrders.Columns[10].HeaderText = rm.GetString("Send_prod", ci);

                // for remove all filter and search
               
               
            }
            if (Session[SESSION.LOGGED_PRODSITE].ToString() == "EC")
            {
                if (gv_ViewOrders != null)
                {

                    gv_ViewOrders.Columns[0].HeaderText = rm.GetString("ID", ci);
                    gv_ViewOrders.Columns[1].HeaderText = rm.GetString("Title_Fesc", ci);
                    gv_ViewOrders.Columns[2].HeaderText = rm.GetString("No_Folio", ci);
                    gv_ViewOrders.Columns[3].HeaderText = rm.GetString("Nature_of_demand", ci);
                    gv_ViewOrders.Columns[4].HeaderText = rm.GetString("Time_limit", ci);
                    gv_ViewOrders.Columns[5].HeaderText = rm.GetString("Iteration", ci);
                    gv_ViewOrders.Columns[6].HeaderText = rm.GetString("Return_Date", ci);
                    gv_ViewOrders.Columns[7].HeaderText = rm.GetString("Number_of_Pages", ci);
                    gv_ViewOrders.Columns[8].HeaderText = rm.GetString("Status", ci);
                    gv_ViewOrders.Columns[9].HeaderText = rm.GetString("Name_of_requester", ci);
                    gv_ViewOrders.Columns[10].HeaderText = rm.GetString("Comments", ci);
                    gv_ViewOrders.Columns[11].HeaderText = rm.GetString("LN_Attachments", ci);
                    gv_ViewOrders.Columns[12].HeaderText = rm.GetString("TD_Attachments", ci);
                 //   gv_ViewOrders.Columns[14].HeaderText = rm.GetString("Task", ci);
                }
               
            }
            if (Session[SESSION.LOGGED_PRODSITE].ToString() == "RV")
            {
                if (gv_ViewOrders != null)
                {

                    gv_ViewOrders.Columns[0].HeaderText = rm.GetString("ID", ci);
                   // gv_ViewOrders.Columns[1].HeaderText = rm.GetString("JID", ci);
                    gv_ViewOrders.Columns[2].HeaderText = rm.GetString("AID", ci);
                    gv_ViewOrders.Columns[3].HeaderText = rm.GetString("Journal_Article_Title", ci);
                    gv_ViewOrders.Columns[4].HeaderText = rm.GetString("Journal_Author", ci);
                    gv_ViewOrders.Columns[5].HeaderText = rm.GetString("Journal_article_type", ci);
                    gv_ViewOrders.Columns[6].HeaderText = rm.GetString("Journal_pubnum", ci);
                    gv_ViewOrders.Columns[7].HeaderText = rm.GetString("Journal_delai", ci);
                    gv_ViewOrders.Columns[8].HeaderText = rm.GetString("Iteration", ci);
                    gv_ViewOrders.Columns[9].HeaderText = rm.GetString("In_Date", ci);
                    gv_ViewOrders.Columns[10].HeaderText = rm.GetString("Return_DateJournal", ci);
                    gv_ViewOrders.Columns[11].HeaderText = rm.GetString("Complete_date", ci);
                    gv_ViewOrders.Columns[12].HeaderText = rm.GetString("Number_of_Pages", ci);					
                    gv_ViewOrders.Columns[13].HeaderText = rm.GetString("Status", ci);
                    gv_ViewOrders.Columns[14].HeaderText = rm.GetString("Name_of_requester", ci);
                    // add and modify on 10 mar 17
                    gv_ViewOrders.Columns[15].HeaderText = rm.GetString("Work_to_be_Done", ci);
					gv_ViewOrders.Columns[16].HeaderText = rm.GetString("Character_count", ci);
                    gv_ViewOrders.Columns[17].HeaderText = rm.GetString("Comments", ci);
                    gv_ViewOrders.Columns[18].HeaderText = rm.GetString("LN_Attachments", ci);
                    gv_ViewOrders.Columns[19].HeaderText = rm.GetString("TD_Attachments", ci);
                    gv_ViewOrders.Columns[20].HeaderText = rm.GetString("Task", ci);
                  
                }

            }
            if (Session[SESSION.LOGGED_PRODSITE].ToString() == "FS")
            {
                if (gv_ViewOrders != null)
                {
                    gv_ViewOrders.Columns[0].HeaderText = rm.GetString("ID", ci);
                    gv_ViewOrders.Columns[1].HeaderText = rm.GetString("Title_Fesc", ci);
                    gv_ViewOrders.Columns[2].HeaderText = rm.GetString("No_Folio", ci);
                    gv_ViewOrders.Columns[3].HeaderText = rm.GetString("Nature_of_demand", ci);
                    gv_ViewOrders.Columns[4].HeaderText = rm.GetString("Time_limit", ci);
                    gv_ViewOrders.Columns[5].HeaderText = rm.GetString("Iteration", ci);

                    gv_ViewOrders.Columns[6].HeaderText = rm.GetString("In_Date", ci);
                    gv_ViewOrders.Columns[7].HeaderText = rm.GetString("Return_DateJournal", ci);
                    gv_ViewOrders.Columns[8].HeaderText = rm.GetString("Complete_date", ci);

                   
                    gv_ViewOrders.Columns[9].HeaderText = rm.GetString("Number_of_Pages", ci);
                    gv_ViewOrders.Columns[10].HeaderText = rm.GetString("Status", ci);
                    gv_ViewOrders.Columns[11].HeaderText = rm.GetString("Name_of_requester", ci);
                    gv_ViewOrders.Columns[12].HeaderText = rm.GetString("Comments", ci);

                    gv_ViewOrders.Columns[19].HeaderText = rm.GetString("LN_Attachments", ci);
                    gv_ViewOrders.Columns[20].HeaderText = rm.GetString("TD_Attachments", ci);
                  //  gv_ViewOrders.Columns[20].HeaderText = rm.GetString("LN_Attachments", ci);
                 //   gv_ViewOrders.Columns[21].HeaderText = rm.GetString("TD_Attachments", ci);
                    gv_ViewOrders.Columns[23].HeaderText = rm.GetString("Task", ci);
                }
            }

      //  }
        // for entry page

            Label lblLoadaFileefiche = (Label)MainContent.FindControl("lblLoadaFileefiche");
            if (lblLoadaFileefiche != null)
            {
                lblLoadaFileefiche.Text = rm.GetString("Load_Fiche_File", ci);
            }

          
                Button btnSend = (Button)MainContent.FindControl("btnSend");
                Button btnCancel = (Button)MainContent.FindControl("btnCancel");



                Label lblCategory = (Label)MainContent.FindControl("lblCategory");
                Label lblHeading = (Label)MainContent.FindControl("lblHeading");
                //Label lblDeclination = (Label)MainContent.FindControl("lblDeclination");
                Label lblFolderTitle = (Label)MainContent.FindControl("lblFolderTitle");
                Label lblAuthor = (Label)MainContent.FindControl("lblAuthor");
                Label lblmailnotification = (Label)MainContent.FindControl("lblmailnotification");
                Label lblComment = (Label)MainContent.FindControl("lblComment");
                Label lblLoadafile = (Label)MainContent.FindControl("lblLoadafile");
                if (btnSend != null)
                {
                    btnSend.Text = rm.GetString("To_send", ci);
                }
                if (btnCancel != null)
                {
                    btnCancel.Text = rm.GetString("Cancel", ci);
                }

                if (lblCategory != null)
                {
                    lblCategory.Text = rm.GetString("Category", ci);
                }
                if (lblHeading != null)
                {
                    lblHeading.Text = rm.GetString("Log_folder", ci);
                }
                if (lblDeclination != null)
                {
                    lblDeclination.Text = rm.GetString("Declination", ci);
                }
                if (lblFolderTitle != null)
                {
                    lblFolderTitle.Text = rm.GetString("Folder_Title", ci);
                }
                if (lblAuthor != null)
                {
                    lblAuthor.Text = rm.GetString("Author", ci);
                }
                if (lblmailnotification != null)
                {
                    lblmailnotification.Text = rm.GetString("Additional_mail_notification", ci);
                }
                if (lblComment != null)
                {
                    lblComment.Text = rm.GetString("Comment", ci);
                }
                if (lblLoadafile != null)
                {
                    lblLoadafile.Text = rm.GetString("Load_a_fileDosier", ci);
                }

                Label lblCategorye = (Label)MainContent.FindControl("lblCategorye");
                Label lblHeadinge = (Label)MainContent.FindControl("lblHeadinge");
                //Label lblDeclination = (Label)MainContent.FindControl("lblDeclination");
                Label lblheading2e = (Label)MainContent.FindControl("lblheading2e");
                Label lblcollectione = (Label)MainContent.FindControl("lblcollectione");
                Label lblTitlefesce = (Label)MainContent.FindControl("lblTitlefesce");
                Label lblTypeofiteme = (Label)MainContent.FindControl("lblTypeofiteme");
                Label lblDTDiteme = (Label)MainContent.FindControl("lblDTDiteme");
                Label lblnewFolioe = (Label)MainContent.FindControl("lblnewFolioe");
                Label lblReferencee = (Label)MainContent.FindControl("lblReferencee");
                Label lblNatureofdemande = (Label)MainContent.FindControl("lblNatureofdemande");
                Label lblDelaibacke = (Label)MainContent.FindControl("lblDelaibacke");
                Label lblNameofapplicante = (Label)MainContent.FindControl("lblNameofapplicante");
                Label lblNotificationforsupe = (Label)MainContent.FindControl("lblNotificationforsupe");
                Label lblDueDatee = (Label)MainContent.FindControl("lblDueDatee");
                Label lblCommente = (Label)MainContent.FindControl("lblCommente");
                Label lblLoadaFilee = (Label)MainContent.FindControl("lblLoadaFilee");


                if (lblCategorye != null)
                {
                    lblCategorye.Text = rm.GetString("Category", ci);
                }
                if (lblHeadinge != null)
                {
                    lblHeadinge.Text = rm.GetString("Log_a_booklet_encyclo", ci);
                }
                if (lblheading2e != null)
                {
                    lblheading2e.Text = rm.GetString("Encyclopedia", ci);
                }
                if (lblcollectione != null)
                {
                    lblcollectione.Text = rm.GetString("collection", ci);
                }
                if (lblTitlefesce != null)
                {
                    lblTitlefesce.Text = rm.GetString("Title_Fesc", ci);
                }
                if (lblTypeofiteme != null)
                {
                    lblTypeofiteme.Text = rm.GetString("Type_of_item", ci);
                }
                if (lblDTDiteme != null)
                {
                    lblDTDiteme.Text = rm.GetString("DTD_item", ci);
                }
                if (lblnewFolioe != null)
                {
                    lblnewFolioe.Text = rm.GetString("new_Folio", ci);
                }
                if (lblReferencee != null)
                {
                    lblReferencee.Text = rm.GetString("Reference", ci);
                }
                if (lblNatureofdemande != null)
                {

                    lblNatureofdemande.Text = rm.GetString("Nature_of_demand", ci);
                }
                if (lblDelaibacke != null)
                {
                    lblDelaibacke.Text = rm.GetString("Delai_back", ci);
                }
                if (lblNameofapplicante != null)
                {
                    lblNameofapplicante.Text = rm.GetString("Name_of_applicant", ci);
                }
                if (lblNotificationforsupe != null)
                {
                    lblNotificationforsupe.Text = rm.GetString("Notification_for_sup", ci);
                }
                if (lblDueDatee != null)
                {
                    lblDueDatee.Text = rm.GetString("Due_Date", ci);
                }
                if (lblCommente != null)
                {
                    lblCommente.Text = rm.GetString("Comment", ci);
                }
                if (lblLoadaFilee != null)
                {
                    lblLoadaFilee.Text = rm.GetString("Load_a_file", ci);
                }

                Label lblHeadinger = (Label)MainContent.FindControl("lblHeadinger");
                Label lblheading2er = (Label)MainContent.FindControl("lblheading2er");
              //  Label lblIder = (Label)MainContent.FindControl("lblIder");
                Label lblTitlefescer = (Label)MainContent.FindControl("lblTitlefescer");
                Label lblCommenter = (Label)MainContent.FindControl("lblCommenter");
                Label lblLoadafileer = (Label)MainContent.FindControl("lblLoadafileer");

                if (lblHeadinger != null)
                {
                    lblHeadinger.Text = rm.GetString("Log_correction", ci);
                }
                if (lblheading2er != null)
                {
                    lblheading2er.Text = rm.GetString("Encyclopedia", ci);
                }
                //if (lblIder != null)
                //{
                //    lblIder.Text = rm.GetString("Leonardo_id", ci);
                //}
                if (lblTitlefescer != null)
                {
                    lblTitlefescer.Text = rm.GetString("Title_Fesc", ci);
                }
                if (lblCommenter != null)
                {
                    lblCommenter.Text = rm.GetString("Comment", ci);
                }
                if (lblLoadafileer != null)
                {
                    lblLoadafileer.Text = rm.GetString("Load_a_file", ci);
                }

                Label lblHeadingtd = (Label)MainContent.FindControl("lblHeadingtd");
                Label lblheading2td = (Label)MainContent.FindControl("lblheading2td");
                Label lblIdtd = (Label)MainContent.FindControl("lblIdtd");
                Label lblTitlefesctd = (Label)MainContent.FindControl("lblTitlefesctd");
                Label lblCommenttd = (Label)MainContent.FindControl("lblCommenttd");
                Label lblLoadafiletd = (Label)MainContent.FindControl("lblLoadafiletd");
                if (lblHeadingtd != null)
                {
                    lblHeadingtd.Text = rm.GetString("Upload_Article", ci);
                }
                if (lblheading2td != null)
                {
                    lblheading2td.Text = rm.GetString("Encyclopedia", ci);
                }
                if (lblIdtd != null)
                {
                    lblIdtd.Text = rm.GetString("Leonardo_id", ci);
                }
                if (lblTitlefesctd != null)
                {
                    lblTitlefesctd.Text = rm.GetString("Title_Fesc", ci);
                }
                if (lblCommenttd != null)
                {
                    lblCommenttd.Text = rm.GetString("Comment", ci);
                }
                if (lblLoadafiletd != null)
                {
                    lblLoadafiletd.Text = rm.GetString("Load_a_file", ci);
                }

        // for dosier login
         // Button btnSend = (Button)MainContent.FindControl("btnSend");
                Label lblLoadafileDosier = (Label)MainContent.FindControl("lblLoadafileDosier");
                if (lblLoadafileDosier != null)
                {
                    lblLoadafileDosier.Text = rm.GetString("Load_a_File_dosier", ci);
                }

                Label lblstagedossiersearch = (Label)MainContent.FindControl("lblstagedossiersearch");
                if (lblstagedossiersearch != null)
                {
                    lblstagedossiersearch.Text = rm.GetString("Stage", ci);
                }

                Label lbldosierpublication = (Label)MainContent.FindControl("lbldosierpublication");
                Label lbldossierdelai = (Label)MainContent.FindControl("lbldossierdelai");
                Label lbldosierdateheure = (Label)MainContent.FindControl("lbldosierdateheure");
                if (lbldosierpublication != null)
                {
                    lbldosierpublication.Text = rm.GetString("Dossier_Publication_Date", ci);
                }
                if (lbldossierdelai != null)
                {
                    lbldossierdelai.Text = rm.GetString("Dossier_delai", ci);
                }
                if (lbldosierdateheure != null)
                {
                    lbldosierdateheure.Text = rm.GetString("Dossier_heuredate", ci);
                }


                Label lbldossierdeliverydateto = (Label)MainContent.FindControl("lbldossierdeliverydateto");
                Label lbldossierdeliverydatefrom = (Label)MainContent.FindControl("lbldossierdeliverydatefrom");
                Label lbldossierlogindatefrom = (Label)MainContent.FindControl("lbldossierlogindatefrom");
                Label lbldossierlogindateto = (Label)MainContent.FindControl("lbldossierlogindateto");
                if (lbldossierdeliverydateto != null)
                {
                    lbldossierdeliverydateto.Text = rm.GetString("Delivery_To_date", ci);
                }
                if (lbldossierdeliverydatefrom != null)
                {
                    lbldossierdeliverydatefrom.Text = rm.GetString("Delivery_From_date", ci);
                }
                if (lbldossierlogindatefrom != null)
                {
                    lbldossierlogindatefrom.Text = rm.GetString("Login_From_date", ci);
                }
                if (lbldossierlogindateto != null)
                {
                    lbldossierlogindateto.Text = rm.GetString("Login_To_date", ci);
                }
                Label Dossiervalidationmessage = (Label)MainContent.FindControl("Dossiervalidationmessage");
                if (Dossiervalidationmessage != null)
                {
                    Dossiervalidationmessage.Text = rm.GetString("Dossier_Validatemsg", ci);
                }

        // for journal serach
                Label lbljournallogindatefrom = (Label)MainContent.FindControl("lbljournallogindatefrom");
                Label lbljournallogindateto = (Label)MainContent.FindControl("lbljournallogindateto");
                Label lbljournaldeliverydatefrom = (Label)MainContent.FindControl("lbljournaldeliverydatefrom");
                Label lbljournaldeliverydateto = (Label)MainContent.FindControl("lbljournaldeliverydateto");
                Label lbljournalcompletedatefrom = (Label)MainContent.FindControl("lbljournalcompletedatefrom");
                Label lbljournalcompletedateto = (Label)MainContent.FindControl("lbljournalcompletedateto");
              
               if (lbljournallogindatefrom != null)
                {
                    lbljournallogindatefrom.Text = rm.GetString("Journal_LoginDate_From", ci);
                }
                if (lbljournallogindateto != null)
                {
                    lbljournallogindateto.Text = rm.GetString("Journal_LoginDate_To", ci);
                }
                if (lbljournaldeliverydatefrom != null)
                {
                    lbljournaldeliverydatefrom.Text = rm.GetString("Journal_deliverydate_from", ci);
                }
                if (lbljournaldeliverydateto != null)
                {
                    lbljournaldeliverydateto.Text = rm.GetString("Journal_deliverydate_To", ci);
                }
                if (lbljournalcompletedatefrom != null)
                {
                    lbljournalcompletedatefrom.Text = rm.GetString("Journal_ActualDeliverydate_from", ci);
                }
                if (lbljournalcompletedateto != null)
                {
                    lbljournalcompletedateto.Text = rm.GetString("Journal_ActualDeliverydate_to", ci);
                }
                Label lbljournalstage = (Label)MainContent.FindControl("lbljournalstage");
                if (lbljournalstage != null)
                {
                    lbljournalstage.Text = rm.GetString("Status", ci);
                }
        // for change password and forget password


                Label dossierchangepwdhead = (Label)MainContent.FindControl("dossierchangepwdhead");
                Label dossieroldpwd = (Label)MainContent.FindControl("dossieroldpwd");
                Label lblnewpwd = (Label)MainContent.FindControl("lblnewpwd");
                Label lblconfirmnewpwd = (Label)MainContent.FindControl("lblconfirmnewpwd");
          Label changepassword = (Label)MainContent.FindControl("changepassword");
          if (dossierchangepwdhead != null)
          {
              dossierchangepwdhead.Text = rm.GetString("Change_Password", ci);
          }
          if (dossieroldpwd != null)
          {
              dossieroldpwd.Text = rm.GetString("Old_Password", ci);
          }
          if (lblnewpwd != null)
          {
              lblnewpwd.Text = rm.GetString("New_Password", ci);
          }

          if (lblconfirmnewpwd != null)
          {
              lblconfirmnewpwd.Text = rm.GetString("Confirm_New_Password", ci);
          }
          if (changepassword != null)
          {
              changepassword.Text = rm.GetString("Change_Password", ci);
          }

        //  Label mainresetpwd = (Label)MainContent.FindControl("mainresetpwd");
         // Label lblresetuid = (Label)MainContent.FindControl("lblresetuid");
        //  Button btnresetpassword = (Button)MainContent.FindControl("btnresetpassword");
          Button btnchangepassword = (Button)MainContent.FindControl("btnchangepassword");
        /*
          if (mainresetpwd != null)
          {
              mainresetpwd.Text = rm.GetString("PleaseInsertValidloginIDforResetpassword", ci);
          }
          if (lblresetuid != null)
          {
              lblresetuid.Text = rm.GetString("login_id", ci);
          }
          if (btnresetpassword != null)
          {
              btnresetpassword.Text = rm.GetString("submit", ci);
          }*/
          if (btnchangepassword != null)
          {
              btnchangepassword.Text = rm.GetString("submit", ci);
          }

        // add comment in dossier
          Label lbldossiercommenthead = (Label)MainContent.FindControl("lbldossiercommenthead");
          Label lbldossiercomment = (Label)MainContent.FindControl("lbldossiercomment");
          Button btnaddcomment = (Button)MainContent.FindControl("btnaddcomment");
          if (lbldossiercommenthead != null)
          {
              lbldossiercommenthead.Text = rm.GetString("Insert_Comment", ci);
          }
          if (lbldossiercomment != null)
          {
              lbldossiercomment.Text = rm.GetString("Comment", ci);
          }
          if (btnaddcomment != null)
          {
              btnaddcomment.Text = rm.GetString("submit", ci);
          }

        // for encyclo entry
          Label lblLoadaFileeEncyclo = (Label)MainContent.FindControl("lblLoadaFileeEncyclo");
          Label lblencyclodateheure = (Label)MainContent.FindControl("lblencyclodateheure");
          if (lblLoadaFileeEncyclo != null)
          {
              lblLoadaFileeEncyclo.Text = rm.GetString("Encyclo_LoadFile", ci);
          }
          if (lblencyclodateheure != null)
          {
              lblencyclodateheure.Text = rm.GetString("Encylo_heuredate", ci);
          }

          Label lblsearchuser = (Label)MainContent.FindControl("lblsearchuser");
          if (lblsearchuser != null)
          {
              lblsearchuser.Text = rm.GetString("Name_of_requester", ci);
          }

        // for journals
          Label lblHeadingtdJournal = (Label)MainContent.FindControl("lblHeadingtdJournal");
          if (lblHeadingtdJournal != null)
          {
              lblHeadingtdJournal.Text = rm.GetString("Journal_Heading", ci);
          }
          Label lblHeadingrv = (Label)MainContent.FindControl("lblHeadingrv");
          if (lblHeadingrv != null)
          {
              lblHeadingrv.Text = rm.GetString("Journal_Heading", ci);
          }
          Label lblHeadingRevisedrv = (Label)MainContent.FindControl("lblHeadingRevisedrv");
          if (lblHeadingRevisedrv != null)
          {
              lblHeadingRevisedrv.Text = rm.GetString("Journal_Revised_Heading", ci);
          }

          Label lblHeadingjournal = (Label)MainContent.FindControl("lblHeadingjournal");
          Label lblreview = (Label)MainContent.FindControl("lblreview");
          Label lbljournalarticletitle = (Label)MainContent.FindControl("lbljournalarticletitle");
          Label lbljournalAuthor = (Label)MainContent.FindControl("lbljournalAuthor");
          Label lbljournalartciletype = (Label)MainContent.FindControl("lbljournalartciletype");
          Label lbljournalpubnum = (Label)MainContent.FindControl("lbljournalpubnum");
          Label lblDelaijournal = (Label)MainContent.FindControl("lblDelaijournal");
          Label lblJournaldateheure = (Label)MainContent.FindControl("lblJournaldateheure");
          Label lblNotificationforsupejournal = (Label)MainContent.FindControl("lblNotificationforsupejournal");
          Label lblCommentejournal = (Label)MainContent.FindControl("lblCommentejournal");
          Label lblLoadaFilejournal = (Label)MainContent.FindControl("lblLoadaFilejournal");
          Label lbljournaltobedone = (Label)MainContent.FindControl("lbljournaltobedone");
          Button btnSendJournal = (Button)MainContent.FindControl("btnSendJournal");
          
        if(lblHeadingjournal !=null)
        {
            lblHeadingjournal.Text = rm.GetString("Journal_Heading", ci);
        }
        if (lblreview != null)
        {
            lblreview.Text = rm.GetString("Revue", ci);
        }
        if (lbljournalarticletitle != null)
        {
            lbljournalarticletitle.Text = rm.GetString("Journal_Article_Title", ci);
        }
        if (lbljournalAuthor != null)
        {
            lbljournalAuthor.Text = rm.GetString("Journal_Author", ci);
        }
        if (lbljournalartciletype != null)
        {
            lbljournalartciletype.Text = rm.GetString("Journal_article_type", ci);
        }
        if (lbljournalpubnum != null)
        {
            lbljournalpubnum.Text = rm.GetString("Journal_pubnum", ci);
        }
        if (lblDelaijournal != null)
        {
            lblDelaijournal.Text = rm.GetString("Journal_delai", ci);
        }
        if (lblJournaldateheure != null)
        {
            lblJournaldateheure.Text = rm.GetString("Journal_heuredate", ci);
        }
        if (lblNotificationforsupejournal != null)
        {
            lblNotificationforsupejournal.Text = rm.GetString("Journal_notification", ci);
        }
        if (lblCommentejournal != null)
        {
            lblCommentejournal.Text = rm.GetString("Journal_Comment", ci);
        }
        if (lblLoadaFilejournal != null)
        {
            lblLoadaFilejournal.Text = rm.GetString("Journal_fileload", ci);
        }
        // for add work to be done
        if (lbljournaltobedone != null)
        {
            lbljournaltobedone.Text = rm.GetString("Work_to_be_Done", ci);
        }
        /////
        if (btnSendJournal != null)
        {
            btnSendJournal.Text = rm.GetString("Journal_submit", ci);
        }



        // for admin user module

        Label lblUserID = (Label)MainContent.FindControl("lblUserID");
        Label lblfname = (Label)MainContent.FindControl("lblfname");
        Label lbllname = (Label)MainContent.FindControl("lbllname");
        Label lblprodsite = (Label)MainContent.FindControl("lblprodsite");
        Button btnUserCreate = (Button)MainContent.FindControl("btnUserCreate");
        Button btnUserCancel = (Button)MainContent.FindControl("btnUserCancel");
        Label lblcreateusermenu = (Label)MainContent.FindControl("lblcreateusermenu");
        if (lblcreateusermenu != null)
        {
            lblcreateusermenu.Text = rm.GetString("Create_User", ci);
        }
        if (lblUserID != null)
        {
            lblUserID.Text = rm.GetString("User_ID", ci);
        }
        if (lblfname != null)
        {
            lblfname.Text = rm.GetString("First_name", ci);
        }
        if (lbllname != null)
        {
            lbllname.Text = rm.GetString("Last_name", ci);
        }
        if (lblprodsite != null)
        {
            lblprodsite.Text = rm.GetString("Production_site", ci);
        }
        if (btnUserCreate != null)
        {
            btnUserCreate.Text = rm.GetString("Create_User", ci);
        }
        if (btnUserCancel != null)
        {
            btnUserCancel.Text = rm.GetString("User_cancel", ci);
        }
        if (btnUserCancel != null)
        {
            btnUserCancel.Text = rm.GetString("User_cancel", ci);
        }
        GridView grdUser = (GridView)MainContent.FindControl("grdUser");
        if (Session[SESSION.LOGGED_ROLE].ToString() == "LAD" || Session[SESSION.LOGGED_ROLE].ToString() == "TAD")
        {
            if (grdUser != null)
            {

                grdUser.Columns[0].HeaderText = rm.GetString("User_ID", ci);
                grdUser.Columns[1].HeaderText = rm.GetString("First_name", ci);
                grdUser.Columns[2].HeaderText = rm.GetString("Last_name", ci);
                grdUser.Columns[3].HeaderText = rm.GetString("User_Role", ci);
                grdUser.Columns[4].HeaderText = rm.GetString("Production_site", ci);
               // Button Deleteimg = (Button)MainContent.FindControl("Deleteimg"); 
               //Button btndelete = (Button)grdUser.FindControl("Deleteimg");
               // Deleteimg.Text = rm.GetString("User_Delete", ci);
               
            }

        }


        // for admin

        Button BtnProducts = (Button)MainContent.FindControl("BtnProducts");
        if (BtnProducts != null)
        {
            BtnProducts.Text = rm.GetString("Products", ci);
        }
        Button BtnUsers = (Button)MainContent.FindControl("BtnUsers");
        if (BtnUsers != null)
        {
            BtnUsers.Text = rm.GetString("Create_User", ci);
        }

        Button Btn_Cancel = (Button)MainContent.FindControl("Btn_Cancel");
        if (Btn_Cancel != null)
        {
            Btn_Cancel.Text = rm.GetString("Cancel", ci);
        }
        Button Btn_Delete = (Button)MainContent.FindControl("Btn_Delete");
        if (Btn_Delete != null)
        {
            Btn_Delete.Text = rm.GetString("Delete_Item", ci);
        }

        Button Btn_Save = (Button)MainContent.FindControl("Btn_Save");
        if (Btn_Save != null)
        {
            Btn_Save.Text = rm.GetString("To_send", ci);
        }
        Button Btn_Close = (Button)MainContent.FindControl("Btn_Close");
        if (Btn_Close != null)
        {
            Btn_Close.Text = rm.GetString("Cancel", ci);
        }

        Label lblAdmindossierupdate = (Label)MainContent.FindControl("lblAdmindossierupdate");
        if (lblAdmindossierupdate != null)
        {
            lblAdmindossierupdate.Text = rm.GetString("Log_folder", ci);
        }

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        CultureInfo ci;
        rm = new ResourceManager("Resources.String", Assembly.Load("App_GlobalResources"));
        ci = Thread.CurrentThread.CurrentCulture;
        Session["lang"] = "en-US";
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        LoadData(Thread.CurrentThread.CurrentCulture);
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        CultureInfo ci;
        rm = new ResourceManager("Resources.String", Assembly.Load("App_GlobalResources"));
        ci = Thread.CurrentThread.CurrentCulture;
        Session["lang"] = "fr-FR";
        Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
        LoadData(Thread.CurrentThread.CurrentCulture);
    }
    protected void ddltask_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
  
}
