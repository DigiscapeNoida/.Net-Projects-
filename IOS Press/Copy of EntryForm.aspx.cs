using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.IO;

public partial class EntryForm : System.Web.UI.Page
{
    OleDbConnection objCon;
    OleDbCommand objCmnd;
    OleDbDataReader Dr;
    string strConnectionString = "";
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        strConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DatabasePath"];
        if (IsPostBack == false)
        {
            
            StreamReader sr = new StreamReader(Server.MapPath("~")  + "\\" +"JID.ini");
            string strCont = sr.ReadToEnd();
            sr.Close();

            string[] sp = new string[4];

            sp[0] = "\n\r";
            sp[1] = "\r\n";
            sp[2] = "\r";
            sp[3] = "\n";

            string[] Arr = strCont.Split(sp, StringSplitOptions.RemoveEmptyEntries);

            cmbJID.Items.Clear();
            for (int i = 0; i < Arr.Length; i++)
            {
                cmbJID.Items.Add(Arr[i]);
            }
        }
        
    }
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox9_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox6_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cmdUpdate_Click(object sender, EventArgs e)
    {

        string strJID = cmbJID.Text.Trim();
        string strAID = txtAID.Text.Trim();
        string strVolIss = txtVolIss.Text.Trim();       
        string strReceipt = txtReceiptDt.Text.Trim();
        string strJIDDesc = txtJIDDescription.Text.Trim();
        string strTitle = txtTitle.Text.Trim();
        string strAuthors = txtAuthors.Text.Trim();
        string strTypesetPages = txtTypesetPages.Text.Trim();
        string strSR = txtSR.Text.Trim();
        string strtoEditor =  txttoEditor.Text.Trim();        
        string strtoAuthors = txttoAuthors.Text.Trim();
        string strtoEditorial = txttoEditorial.Text.Trim();
        string strfromAuthors =  txtfromAuthors.Text.Trim();
        string strfromEditor =  txtfromEditor.Text.Trim();
        string strfromEditorial = txtfromEditorial.Text.Trim();        
        string strCompilationDt = txtCompilationDt.Text.Trim();
        string strCRCDt = txtCRCDt.Text.Trim();
        string strPrintConfirmationDT = txtPrintConfirmationDt.Text.Trim();
        string strPrintDtIOS = txtPrintDtIOS.Text.Trim();
        string strThomsonRemarks = txtThomsonRemarks.Text.Trim();
        string strTo = txtTo.Text.Trim();
        string strCC = txtCC.Text.Trim();
        string strBCC = txtBCC.Text.Trim();
        string strRDate = txtReminderDate.Text.Trim();
        string strOAO = txtOAO.Text.Trim();

        //////////////////
        


        string SqlSt = "";
        string SqlSt1 = "";
        string strJournalID = CheckForExistence(strJID, strAID);
        if (strJournalID.Trim().Length > 0)
        {
            SqlSt = "Update IOSJournal set VolIss = '" + strVolIss + "', title = '" + strTitle +
                "', JIDDesc = '" + strJIDDesc + "'";             

            SqlSt1 = "Update IOSJournalDetails set Authors = '" + strAuthors + "', " +
                "TSPages = '" + strTypesetPages + "'," +
                "AuthorReq = '" + strSR + "'," +
                "TsRemarks = '" + strThomsonRemarks + "' ";


            string TempS;
                TempS = "";
                if(strReceipt.Length >0)
                {
                    TempS = TempS + "DtReceipt = '" + strReceipt + "'" ;
                }

                if (strtoAuthors.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",DtProofsSent_Author = '" + strtoAuthors + "'";
                    else
                        TempS = "DtProofsSent_Author = '" + strtoAuthors + "'";
                }

                if (strfromAuthors.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",DtCorrectionsRec_Author = '" + strfromAuthors + "'";
                    else
                        TempS = "DtCorrectionsRec_Author = '" + strfromAuthors + "'";
                }

                if (strtoEditor.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",DtProofsSent_Editor = '" + strtoEditor + "'";
                    else
                        TempS = "DtProofsSent_Editor = '" + strtoEditor + "'";
                }

                if (strfromEditor.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",DtCorrectionsRec_Editor = '" + strfromEditor + "'";
                    else
                        TempS = "DtCorrectionsRec_Editor = '" + strfromEditor + "'";
                }

                if (strtoEditorial.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",DtProofsSent_Editorial = '" + strtoEditorial + "'";
                    else
                        TempS = "DtProofsSent_Editorial = '" + strtoEditorial + "'";
                }

                if (strfromEditorial.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",DtCorrectionsRec_Editorial = '" + strfromEditorial + "'";
                    else
                        TempS = "DtCorrectionsRec_Editorial = '" + strfromEditorial + "'";
                }             

                if (strCompilationDt.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",IssComDt = '" + strCompilationDt + "'";
                    else
                        TempS = "IssComDt = '" + strCompilationDt + "'";
                }              

                if (strCRCDt.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",CRCDeliveryDt = '" + strCRCDt + "'";
                    else
                        TempS = "CRCDeliveryDt = '" + strCRCDt + "'";
                }              

                if (strPrintConfirmationDT.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",PrintPDFDT = '" + strPrintConfirmationDT + "'";
                    else
                        TempS = "PrintPDFDT = '" + strPrintConfirmationDT + "'";
                }

                if (strPrintDtIOS.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",PrintPDF2IOSDt = '" + strPrintDtIOS + "'";
                    else
                        TempS = "PrintPDF2IOSDt = '" + strPrintDtIOS + "'";
                }

                string TempS1 = "";
                if (strTo.Length > 0)
                {
                    if (TempS1.Length > 0)
                        TempS1 = TempS1 + ",EmailTo = '" + strTo + "'";
                    else
                        TempS1 = "EmailTo = '" + strTo + "'";
                }

                if (strCC.Length > 0)
                {
                    if (TempS1.Length > 0)
                        TempS1 = TempS1 + ",EmailCC = '" + strCC + "'";
                    else
                        TempS1 = "EmailCC = '" + strCC + "'";
                }

                if (strBCC.Length > 0)
                {
                    if (TempS1.Length > 0)
                        TempS1 = TempS1 + ",EmailBCC = '" + strBCC + "'";
                    else
                        TempS1 = "EmailBCC = '" + strBCC + "'";
                }

                if (strRDate.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",ReminderDate = '" + strRDate + "'";
                    else
                        TempS = "ReminderDate = '" + strRDate + "'";
                }

                if (strOAO.Length > 0)
                {
                    if (TempS.Length > 0)
                        TempS = TempS + ",OAO = '" + strOAO + "'";
                    else
                        TempS = "OAO = '" + strOAO + "'";
                }


                if (TempS.Length > 0)
                {
                    SqlSt1 = SqlSt1 + "," + TempS;
                }

                if (TempS1.Length > 0)
                {
                    SqlSt = SqlSt + "," + TempS1;
                }



                SqlSt = SqlSt + " where JournalID = '" + strJournalID + "'";
                
                SqlSt1 = SqlSt1 + " where JournalID = '" + strJournalID + "'";
        }
        else 
        {
            strJournalID = GetUniqueValue();
            string strSeqID = "1";
            SqlSt = "Insert into IOSJournal values ('"+strJournalID+
                        "','" + strJID +
                        "','" + strJIDDesc +
                        "','" + strAID +
                        "','" + strTitle +
                        "','" + strVolIss +
                        "','" + strTo +
                        "','" + strCC +
                        "','" + strBCC +
                        "','No','')";

            SqlSt1 = "Insert into IOSJournalDetails values (";

            string strF = "";
            string strV = "";

            if (strJournalID.Length > 0)
            {
                strF = "SeqID, JournalID";
                strV = "'" + strJournalID + "','" + strJournalID + "'";
            }

            if (strReceipt.Length > 0)
            {
                strF = strF + ",DtReceipt";                
                strV = strV + ",'" + strReceipt + "'";
            }
            

            if (strAuthors.Length > 0)
            {
                strF = strF + ",Authors";
                strV = strV + ",'" + strAuthors + "'";
            }

            if (strtoAuthors.Length > 0)
            {
                strF = strF + ",DtProofsSent_Author";
                strV = strV + ",'" + strtoAuthors + "'";
            }

            if (strfromAuthors.Length > 0)
            {
                strF = strF + ",DtCorrectionsRec_Author";
                strV = strV + ",'" + strfromAuthors + "'";
            }

            if (strtoEditor.Length > 0)
            {
                strF = strF + ",DtProofsSent_Editor";
                strV = strV + ",'" + strtoEditor + "'";
            }
            if (strfromEditor.Length > 0)
            {
                strF = strF + ",DtCorrectionsRec_Editor";
                strV = strV + ",'" + strfromEditor + "'";
            }
            if (strtoEditorial.Length > 0)
            {
                strF = strF + ",DtProofsSent_Editorial";
                strV = strV + ",'" + strtoEditorial + "'";
            }
            if (strfromEditorial.Length > 0)
            {
                strF = strF + ",DtCorrectionsRec_Editorial";
                strV = strV + ",'" + strfromEditorial + "'";
            }
            if (strTypesetPages.Length > 0)
            {
                strF = strF + ",TSPages";
                strV = strV + ",'" + strTypesetPages + "'";
            }
            if (strSR.Length > 0)
            {
                strF = strF + ",AuthorReq";
                strV = strV + ",'" + strSR + "'";
            }
            if (strCompilationDt.Length > 0)
            {
                strF = strF + ",IssComDt";
                strV = strV + ",'" + strCompilationDt + "'";
            }
            if (strCRCDt.Length > 0)
            {
                strF = strF + ",CRCDeliveryDt";
                strV = strV + ",'" + strCRCDt + "'";
            }
            if (strPrintConfirmationDT.Length > 0)
            {
                strF = strF + ",PrintPDFDT";
                strV = strV + ",'" + strPrintConfirmationDT + "'";
            }
            if (strPrintDtIOS.Length > 0)
            {
                strF = strF + ",PrintPDF2IOSDt";
                strV = strV + ",'" + strPrintDtIOS + "'";
            }
            if (strThomsonRemarks.Length > 0)
            {
                strF = strF + ",TsRemarks";
                strV = strV + ",'" + strThomsonRemarks + "'";
            }

            if (strRDate.Length > 0)
            {
                strF = strF + ",ReminderDate";
                strV = strV + ",'" + strRDate + "'";
            }

            if (strOAO.Length > 0)
            {
                strF = strF + ",OAO";
                strV = strV + ",'" + strOAO + "'";
            }


/*            if (strTo.Length > 0)
            {
                strF = strF + ",EmailTo";
                strV = strV + ",'" + strTo + "'";
            }

           
            if (strTo.Length > 0)
            {
                strF = strF + ",EmailCC";
                strV = strV + ",'" + strCC + "'";
            }

            if (strBCC.Length > 0)
            {
                strF = strF + ",EmailBCC";
                strV = strV + ",'" + strBCC + "'";
            }

            */
          


            SqlSt1 = "Insert into IOSJournalDetails ("+ strF+ ") values (" + strV+ ")";

            
            /*
       Select JID, AID, VolIss, title, DtReceipt, Authors, 
       TSPages,DtProofsSent_Author, DtCorrectionsRec_Author,AuthorReq,  
       DtProofsSent_Editor,DtCorrectionsRec_Editor, DtProofsSent_Editorial, 
       DtCorrectionsRec_Editorial, IssComDt, CRCDeliveryDt, PrintPDFDT, 
       PrintPDF2IOSDt, TsRemarks from IOSJournal a, IOSJournalDetails b 
        where a.JournalID = b.JournalID";
        */                      
                
        }



        objCon = new OleDbConnection(strConnectionString);
        objCon.Open();             
       
        objCmnd = new OleDbCommand(SqlSt, objCon);
        int cnt = objCmnd.ExecuteNonQuery();
        if (cnt == 0)
        {
            // No rows update
        }
        else
        { 
            // Rows updated
        }

        objCmnd = new OleDbCommand(SqlSt1, objCon);
        int cnt1 = objCmnd.ExecuteNonQuery();
        objCon.Close();

        AlertMessage("Updation task completed successfully");

        
    }

    public string CheckForExistence(string strJID, string strAID)
    {

        objCon = new OleDbConnection(strConnectionString);
        objCon.Open();

        string SqlSt;

        SqlSt = "Select JournalID from IOSJournal where JID ='" + strJID + "' and AID='" + strAID + "'";

        objCmnd = new OleDbCommand(SqlSt, objCon);
        Dr = objCmnd.ExecuteReader();

        string strJournalID = "";
        while (Dr.Read())
        {
            strJournalID = Dr[0].ToString().Trim();
        }

        objCon.Close();
        return strJournalID;      
    }

    public string CheckForExistence_(string strJID, string strAID)
    {

        objCon = new OleDbConnection(strConnectionString);
        objCon.Open();

        string SqlSt;

        SqlSt = "Select JournalID from  IOSJournal where JID ='" + strJID + "' and AID='" + strAID + "'";

        objCmnd = new OleDbCommand(SqlSt, objCon);
        Dr = objCmnd.ExecuteReader();

        string strJournalID = "";
        while (Dr.Read())
        {
            strJournalID = Dr[0].ToString().Trim();
        }
        objCon.Close();
        return strJournalID;       
    }



    public string GetUniqueValue()
    {

        objCon = new OleDbConnection(strConnectionString);
        objCon.Open();

        string strDT = "";
        int y = System.DateTime.Today.Year;
        int m = System.DateTime.Today.Month;

        string strJID = "";
        string strAID = "";

        strDT = "J" + y.ToString().Trim();
        if (m < 10)
        {
            strDT = strDT + "0" + m.ToString().Trim();
        }
        else
        {
            strDT = strDT + m.ToString().Trim();
        }

        string SqlSt;
        SqlSt = "Select max(JournalID) from  IOSJournal where JournalID like'" + strDT + "%'";

        objCmnd = new OleDbCommand(SqlSt, objCon);
        Dr = objCmnd.ExecuteReader();

        string strJournalID = "";
        while (Dr.Read())
        {
            strJournalID = Dr[0].ToString().Trim();
        }
        objCon.Close();

        if (strJournalID.Length == 12)
        {
            string s = strJournalID.Replace(strDT, "");
            //Check whether number or not

            double num = Convert.ToDouble(s);
            num++;

            if (num < 10)
            {
                strJournalID = strDT + "0000" + num.ToString().Trim();
            }
            else if (num < 100)
            {
                strJournalID = strDT + "000" + num.ToString().Trim();
            }
            else if (num < 1000)
            {
                strJournalID = strDT + "00" + num.ToString().Trim();
            }
            else if (num < 10000)
            {
                strJournalID = strDT + "0" + num.ToString().Trim();
            }
            else if (num < 100000)
            {
                strJournalID = strDT + num.ToString().Trim();
            }
            else
            {
                strJournalID = strDT + num.ToString().Trim();
            }
        }
        else
        {
            strJournalID = strDT + "00001";
        }
        return strJournalID;       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ClearFields("All");
    }
    protected void txtfromEditorial_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtThomsonRemarks_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtVolIss_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cmdView_Click(object sender, EventArgs e)
    {
        string strJID = cmbJID.Text.Trim();
        if (strJID.Equals("Select JID"))
        {
            strJID = "";
        }

        string strAID = txtAID.Text.Trim();

        if ((strJID.Length == 0))
        {
            if (strAID.Length == 0)
            {
                ClearFields("All");
                AlertMessage("Please select JID and AID!");
                return;
            }
            else
            {
                ClearFields("AID");
                AlertMessage("AID \"" + strAID + "\" is specified, but JID is missing, Please check!");
                return;
            }
        }
        else
        {
            if (strAID.Length == 0)
            {
                ClearFields("JID");
                AlertMessage("JID \"" + strJID + "\" is specified, but AID is missing, Please check!");
                return;
            }
            else
            {
                ClearFields("JID,AID");
            }

        }

        objCon = new OleDbConnection(strConnectionString);       

        string SqlSt = "";
        SqlSt = "Select JID, AID, VolIss, title, DtReceipt, Authors, TSPages,DtProofsSent_Author, DtCorrectionsRec_Author,AuthorReq,  DtProofsSent_Editor,DtCorrectionsRec_Editor, DtProofsSent_Editorial, DtCorrectionsRec_Editorial, IssComDt, CRCDeliveryDt, PrintPDFDT, PrintPDF2IOSDt, TsRemarks, JIDDesc, EmailTo, EmailCC, EmailBCC, ReminderDate, OAO from IOSJournal a, IOSJournalDetails b where a.JournalId = b.JournalID and JID = '" + strJID+ "' and AID = '" + strAID+ "'";
        objCon.Open();
        objCmnd = new OleDbCommand(SqlSt, objCon);
        Dr = objCmnd.ExecuteReader();

        int RecCnt = 0;
        while (Dr.Read())
        {
            RecCnt++;
            txtVolIss.Text = Dr[2].ToString();
            txtTitle.Text = Dr[3].ToString();
            txtReceiptDt.Text = Dr[4].ToString();
            txtAuthors.Text = Dr[5].ToString();
            txtTypesetPages.Text=  Dr[6].ToString();
            txttoAuthors.Text = Dr[7].ToString();
            txtfromAuthors.Text = Dr[8].ToString();
            txtSR.Text = Dr[9].ToString();
            txttoEditor.Text = Dr[10].ToString();
            txtfromEditor.Text = Dr[11].ToString();
            txttoEditorial.Text = Dr[12].ToString();
            txtfromEditorial.Text =  Dr[13].ToString();
            txtCompilationDt.Text = Dr[14].ToString();
            txtCRCDt.Text = Dr[15].ToString();
            txtPrintConfirmationDt.Text = Dr[16].ToString();
            lbl1.Text = Dr[17].ToString();
            txtThomsonRemarks.Text = Dr[18].ToString();
            txtJIDDescription.Text = Dr[19].ToString();
            txtTo.Text = Dr[20].ToString();
            txtCC.Text = Dr[21].ToString();
            txtBCC.Text = Dr[22].ToString();
            txtReminderDate.Text = Dr[23].ToString();
            txtOAO.Text = Dr[23].ToString();
        }
        
        objCon.Close();
        if (RecCnt == 0)
        {

            ///////////

            
            SqlSt = "Select JID, AID, VolIss, title, JIDDesc, EmailTo, EmailCC, EmailBCC from IOSJournal a where JID = '" + strJID + "' and AID = '" + strAID + "'";
            objCon.Open();
            objCmnd = new OleDbCommand(SqlSt, objCon);
            Dr = objCmnd.ExecuteReader();
            RecCnt = 0;
            while (Dr.Read())
            {
                RecCnt++;
                txtVolIss.Text = Dr[2].ToString();
                txtTitle.Text = Dr[3].ToString();
                txtJIDDescription.Text = Dr[4].ToString();
                txtTo.Text = Dr[5].ToString();
                txtCC.Text = Dr[6].ToString();
                txtBCC.Text = Dr[7].ToString();
                //txtReceiptDt.Text = Dr[4].ToString();
                //txtAuthors.Text = Dr[5].ToString();
                //txtTypesetPages.Text = Dr[6].ToString();
                //txttoAuthors.Text = Dr[7].ToString();
                //txtfromAuthors.Text = Dr[8].ToString();
                //txtSR.Text = Dr[9].ToString();
                //txttoEditor.Text = Dr[10].ToString();
                //txtfromEditor.Text = Dr[11].ToString();
                //txttoEditorial.Text = Dr[12].ToString();
                //txtfromEditorial.Text = Dr[13].ToString();
                //txtCompilationDt.Text = Dr[14].ToString();
                //txtCRCDt.Text = Dr[15].ToString();
                //txtPrintConfirmationDt.Text = Dr[16].ToString();
                //lbl1.Text = Dr[17].ToString();
                //txtThomsonRemarks.Text = Dr[18].ToString();
                
            }

            objCon.Close();

            if (RecCnt == 0)
            {
                ///////////
                AlertMessage("Information not found for JID: \"" + strJID + "\" AID: \"" + strAID + "\", Please check!");
                ClearFields("JID,AID");
            }

        }

    }

    public void AlertMessage(string strMsg)
    {
        Page.RegisterStartupScript("ScrollToBottom", "<script language='javascript'>" + System.Environment.NewLine +
"alert('"+ strMsg+"');" + System.Environment.NewLine +
"</script>");
    
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cmdReports_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void cmdAdd_Click(object sender, EventArgs e)
    {
        ClearFields("All");
    }

    public void ClearFields(string ClearType)
    {
        if (ClearType.Equals("All"))
        {
            cmbJID.Text = "Select JID";
            txtAID.Text = "";
        }
        else
        {
            if (ClearType.IndexOf("JID") == -1)
            {
                cmbJID.Text = "Select JID";
            }
            if (ClearType.IndexOf("AID") == -1)
            {
                txtAID.Text = "";
            }
        }


        txtAuthors.Text = "";
        
        txtVolIss.Text = "";
        txtTitle.Text = "";
        txtReceiptDt.Text = "";
        txtAuthors.Text = "";
        txtTypesetPages.Text = "";
        txttoAuthors.Text = "";
        txtfromAuthors.Text = "";
        txtSR.Text = "";
        txttoEditor.Text = "";
        txtfromEditor.Text = "";
        txttoEditorial.Text = "";
        txtfromEditorial.Text = "";
        txtCompilationDt.Text = "";
        txtCRCDt.Text = "";
        txtPrintConfirmationDt.Text = "";
        lbl1.Text = "";
        txtThomsonRemarks.Text = "";
        txtJIDDescription.Text = "";
        txtTo.Text = "";
        txtCC.Text = "";
        txtBCC.Text = "";
        txtOAO.Text = "";


    }
    protected void cmdLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
    protected void txtCRCDt_TextChanged(object sender, EventArgs e)
    {

    }
}
