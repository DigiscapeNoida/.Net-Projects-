#pragma checksum "D:\WinCVS\ThimeXMLORDER\EMCOrder.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D213D834E6B688C2315DF0064DAB1ADD043BBBF9"

#line 1 "D:\WinCVS\ThimeXMLORDER\EMCOrder.aspx.cs"
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class EMCOrder : System.Web.UI.Page
{
    Orders.JIDInfo     JIDInfoOBJ   = new Orders.JIDInfo();
    Orders.EMCXmlOrder XmlOrderOBJ  = new Orders.EMCXmlOrder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Inialize();
        }
    }
    protected void CmdXmlGenerate_Click(object sender, EventArgs e)
    {
            bool Result = false;

            XmlOrderOBJ.ProdSite     = "EMC-SITE";

            XmlOrderOBJ.Client       = "EMC";

            XmlOrderOBJ.Aid          = txtAid.Text;

            XmlOrderOBJ.AppelBiblio  = ConvertComboValue(cmbAppelBiblio.Text);
 
            XmlOrderOBJ.AppelIcono   = ConvertComboValue(cmbAppelIcono.Text);

            XmlOrderOBJ.ArbreDeci    = ConvertComboValue(cmbArbreDeci.Text);

            XmlOrderOBJ.ArticleType  = txtArticleType.Text;

            XmlOrderOBJ.Chaptre      = txtChaptre.Text;

            XmlOrderOBJ.ColorModel   = txtColorModel.Text;

            XmlOrderOBJ.Doi          = txtDoi.Text;

            XmlOrderOBJ.Fasnumero    = txtFasnumero.Text;

            XmlOrderOBJ.IconoOk      = ConvertComboValue(cmbIconoOk.Text);

            XmlOrderOBJ.Isbn         = txtIsbn.Text;

            XmlOrderOBJ.Issn         = txtIssn.Text;

            XmlOrderOBJ.ItemSubtitle = txtItemSubtitle.Text;

            XmlOrderOBJ.ItemTitle    = txtItemTitle.Text;

            XmlOrderOBJ.Lblvide      = txtLblvide.Text;

            XmlOrderOBJ.MajAnne      = txtMajAnne.Text;

            XmlOrderOBJ.MajCote      = txtMajCote.Text;
                 
            XmlOrderOBJ.MajNo        = txtMajNo.Text;

            XmlOrderOBJ.McEn         = ConvertComboValue(cmbMcEn.Text);

            XmlOrderOBJ.McFr         = ConvertComboValue(cmbMcFr.Text);

            XmlOrderOBJ.NbArbreIntractif = txtNbArbreIntractif.Text;

            XmlOrderOBJ.NbArbrePapier    =  txtNbArbrePapier.Text;

            XmlOrderOBJ.NbAutoeval       = txtNbAutoeval.Text;

            XmlOrderOBJ.NbBiblio         = txtNbBiblio.Text;

            XmlOrderOBJ.NbBiblioSs       = txtNbBiblioSs.Text;

            XmlOrderOBJ.NbClinique       = txtNbClinique.Text;

            XmlOrderOBJ.NbDessin         = txtNbDessin.Text;

            XmlOrderOBJ.NbDocLegaux      = txtNbDocLegaux.Text;

            XmlOrderOBJ.NbEncadreT1      = txtNbEncadreT1.Text;

            XmlOrderOBJ.NbFichePatient   = txtNbFichePatient.Text;

            XmlOrderOBJ.NbFicheTech      = txtNbFicheTech.Text;

            XmlOrderOBJ.NbFig            = txtNbFig.Text;

            XmlOrderOBJ.NbIconoSup       = txtNbIconoSup.Text;
  
            XmlOrderOBJ.NbPagesCommande  = txtNbPagesCommande.Text;

            XmlOrderOBJ.NbPagesEstimate  = txtNbPagesEstimate.Text;

            XmlOrderOBJ.NbPhoto          = txtNbPhoto.Text;   

            XmlOrderOBJ.NbQuotidien      = txtNbQuotidien.Text;

            XmlOrderOBJ.NbSavoirPlus     = txtNbSavoirPlus.Text;

            XmlOrderOBJ.NbTableau        = txtNbTableau.Text;

            XmlOrderOBJ.Nbvide           = txtNbvide.Text;

            XmlOrderOBJ.NbVideo          = txtNbVideo.Text;

            XmlOrderOBJ.Pii              = txtPii.Text;

            XmlOrderOBJ.ResumeEn         = ConvertComboValue(cmbResumeEn.Text);

            XmlOrderOBJ.ResumeFr         = ConvertComboValue(cmbResumeFr.Text);

            XmlOrderOBJ.Stage            = cmbStage.Text;

            XmlOrderOBJ.Subchaptre       = txtSubchaptre.Text; 

            XmlOrderOBJ.TitreEn          = ConvertComboValue(cmbTitreEn.Text);

            XmlOrderOBJ.Tracode          = cmbTracode.Text;

            XmlOrderOBJ.JID              = cmbTracode.Text;

            XmlOrderOBJ.TraiteTitle      = txtTraiteTitle.Text;

            XmlOrderOBJ.Trajid           = txtTrajid.Text;

            XmlOrderOBJ.TraRoot          = txtTraRoot.Text;
    
            XmlOrderOBJ.TypesettingModelFormat   = txtTypesettingModelFormat.Text;
            XmlOrderOBJ.Vol                      = txtVol.Text;

            XmlOrderOBJ.PrincAuthorNom  = txtPrincAuthorNom.Text;
            XmlOrderOBJ.PrincAuthorPnom = txtPrincAuthorPnom.Text;
            XmlOrderOBJ.PrincAuthorAff  = txtPrincAuthorAff.Text;

            XmlOrderOBJ.SecondAuthorNom  = txtSecondAuthorNom.Text;
            XmlOrderOBJ.SecondAuthorPnom = txtSecondAuthorPnom.Text;
            XmlOrderOBJ.SecondAuthorAff  = txtSecondAuthorAff.Text;

            XmlOrderOBJ.CorrAuthorPhone  = txtCorrAuthorPhone.Text;
            XmlOrderOBJ.CorrAuthorFAX    = txtCorrAuthorFax.Text;
            XmlOrderOBJ.CorrAuthorEmail  = txtCorrAuthorEmail.Text;


            if (cmbStage.Text.Equals("S100"))
                XmlOrderOBJ.InternalDuedate = AddDaysNoWeekends(5);
            else if (cmbStage.Text.Equals("S200"))
                XmlOrderOBJ.InternalDuedate = AddDaysNoWeekends(2);

            string XMLFilePath = XmlOrderOBJ.CreateXMLOrder();

            if (File.Exists(XMLFilePath))
            {
                string ZipFile = XMLFilePath.Replace(".xml", ".zip");
                if (File.Exists(ZipFile))
                {
                    File.Delete(ZipFile);
                }
                flUpload.SaveAs(ZipFile);
                FileInfo zipFl = new FileInfo(ZipFile);
                if (flUpload.PostedFile.ContentLength == zipFl.Length)
                {
                    string FMSXMLFile = Orders.ConfigDetails.EMCFMSPath.TrimEnd(new char[] { '\\' }) + "\\" + Path.GetFileName(XMLFilePath);
                    File.Copy(XMLFilePath, FMSXMLFile);

                    string FMSZipFile = Orders.ConfigDetails.EMCFMSPath.TrimEnd(new char[] { '\\' }) + "\\" + Path.GetFileName(ZipFile);
                    File.Copy(ZipFile, FMSZipFile);
                    Result = true;
                }
            }
            if (Result)
            {
                UnInitialize();
                string alertScript = "<script language=JavaScript>";
                alertScript += "alert('--- Order Created successfully---')</script" + "> ";
                this.RegisterClientScriptBlock("alert", alertScript);
            }
    }
    protected void Inialize()
    {
        //*****************\\
        cmbTracode.Items.Clear();
        JIDInfoOBJ.GetTRACode();
        cmbTracode.DataSource = JIDInfoOBJ.JID;
        cmbTracode.DataBind();
        //*****************\\
    }
    protected void cmbTracode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Orders.TRACodeInfo TRACodeInfoOBj = JIDInfoOBJ.GetTRACodeInfo(cmbTracode.Text);

        if (!TRACodeInfoOBj.ISSN.Equals(""))
        {
            txtIssn.Text    = TRACodeInfoOBj.ISSN;
            txtIssn.Enabled = false;
        }
        else
        {
            txtIssn.Enabled = true;
        }

        if (!TRACodeInfoOBj.ISBN.Equals(""))
        {
            txtIsbn.Text = TRACodeInfoOBj.ISBN;
            txtIsbn.Enabled = false;
        }
        else
        {
            txtIsbn.Enabled = true;
        }


        if (!TRACodeInfoOBj.ColorModel.Equals(""))
        {
            txtColorModel.Text = TRACodeInfoOBj.ColorModel;
            txtColorModel.Enabled = false;
        }
        else
        {
            txtColorModel.Enabled = true;
        }



        if (!TRACodeInfoOBj.TRATitle.Equals(""))
        {
            txtTraiteTitle.Text = TRACodeInfoOBj.TRATitle;
            txtTraiteTitle.Enabled = false;
        }
        else
        {
            txtTraiteTitle.Enabled = true;
        }

        if (!TRACodeInfoOBj.TypeSettingModelFormat.Equals(""))
        {
            txtTypesettingModelFormat.Text = TRACodeInfoOBj.TypeSettingModelFormat;
            txtTypesettingModelFormat.Enabled = false;
        }
        else
        {
            txtTypesettingModelFormat.Enabled = true;
        }

        if (!TRACodeInfoOBj.TRAJID.Equals(""))
        {
            txtTrajid.Text = TRACodeInfoOBj.TRAJID;
            txtTrajid.Enabled = false;
        }
        else
        { 
            txtTrajid.Enabled = true;
        }
        }
    private string ConvertComboValue(string Txt)
    {
        string Result = "";

        if (Txt.Equals("OK"))
            Result = "O";
        else if (Txt.Equals("KO"))
            Result = "N";

        return Result;

    }
    private string ConvertValue(string Txt)
    {
        string Result = "";
        if (Txt.Equals("O"))
            Result = "OK";
        else if (Txt.Equals("N"))
            Result = "KO";
        else
            Result = "-Select-";

        return Result;
    }
    public DateTime AddDaysNoWeekends(int Days)
    {
        DateTime dt = DateTime.Today;
        //dt=dt.AddDays(-1); //////////////////// To exclude current date
        int DayIcr = 0;
        while (true)
        {
            dt = dt.AddDays(1);
            if (dt.DayOfWeek == DayOfWeek.Saturday
                || dt.DayOfWeek == DayOfWeek.Sunday
                || dt.ToShortDateString() == "1/26/2012" //Republic Day
                || dt.ToShortDateString() == "3/9/2012" //Annual Day
                || dt.ToShortDateString() == "3/11/2009" //Holi
                || dt.ToShortDateString() == "8/5/2009" //Rakshabandhan
                || dt.ToShortDateString() == "8/15/2012" //Independence Day
                || dt.ToShortDateString() == "10/6/2011" //Dussehra
                || dt.ToShortDateString() == "10/2/2011" //Mahatma Gandhi's Birthday
                || dt.ToShortDateString() == "10/26/2011" //Diwali
                || dt.ToShortDateString() == "11/10/2011" //Guru Nanak’s Birthday
                || dt.ToShortDateString() == "12/25/2011" //Christmas Day
                || dt.ToShortDateString() == "12/31/2011") //Christmas Day
            {
            }
            else
                DayIcr++;

            if (DayIcr == Days) break;
        }
        return dt;
    }
    public void UnInitialize()
    { 
            txtAid.Text          ="";

            cmbAppelBiblio.Text  = "-Select-";
            cmbAppelIcono.Text   = "-Select-";
            cmbArbreDeci.Text    = "-Select-";
            cmbMcEn.Text         = "-Select-";
            cmbMcFr.Text         = "-Select-";
            cmbIconoOk.Text      = "-Select-";
            cmbResumeEn.Text     = "-Select-";
            cmbResumeFr.Text     = "-Select-";

            txtArticleType.Text  ="";
            txtChaptre.Text      ="";
            txtColorModel.Text   ="";
            txtDoi.Text          ="";
            txtFasnumero.Text    ="";
            txtIsbn.Text         ="";
            txtIssn.Text         ="";
            txtItemSubtitle.Text ="";
            txtItemTitle.Text    ="";
            txtLblvide.Text      ="";
            txtMajAnne.Text      ="";
            txtMajCote.Text      ="";
            txtMajNo.Text        ="";

            

            txtNbArbreIntractif.Text ="";
            txtNbArbrePapier.Text    ="";
            txtNbAutoeval.Text       ="";
            txtNbBiblio.Text         ="";
            txtNbBiblioSs.Text       ="";
            txtNbClinique.Text       ="";
            txtNbDessin.Text         ="";
            txtNbDocLegaux.Text      ="";
            txtNbEncadreT1.Text      ="";
            txtNbFichePatient.Text   ="";
            txtNbFicheTech.Text      ="";
            txtNbFig.Text            ="";
            txtNbIconoSup.Text       ="";
            txtNbPagesCommande.Text  ="";
            txtNbPagesEstimate.Text  ="";
            txtNbPhoto.Text          ="";
            txtNbQuotidien.Text      ="";
            txtNbSavoirPlus.Text     ="";
            txtNbTableau.Text        ="";
            txtNbvide.Text           ="";
            txtNbVideo.Text          ="";
            txtPii.Text              ="";

            txtSubchaptre.Text       ="";
            txtTraiteTitle.Text      ="";
            txtTrajid.Text           ="";
            txtTraRoot.Text                ="";
            txtTypesettingModelFormat.Text ="";
            txtVol.Text                    ="";
            txtPrincAuthorNom.Text         ="";
            txtPrincAuthorPnom.Text        ="";
            txtPrincAuthorAff.Text         ="";
            txtSecondAuthorNom.Text        ="";
            txtSecondAuthorPnom.Text       ="";
            txtSecondAuthorAff.Text        ="";
            txtCorrAuthorPhone.Text        ="";
            txtCorrAuthorFax.Text          ="";
            txtCorrAuthorEmail.Text        ="";
    }
    protected void txtAid_TextChanged(object sender, EventArgs e)
    {
        if (!cmbStage.SelectedItem.Text.Equals("S100"))
        {
            XmlOrderOBJ.JID = cmbTracode.Text;
            XmlOrderOBJ.Aid = txtAid.Text;
            XmlOrderOBJ.Stage = cmbStage.SelectedItem.Text;
            XmlOrderOBJ.Stage = cmbStage.Items[cmbStage.SelectedIndex - 1].Text;
            XmlOrderOBJ.Client ="EMC";
            XmlOrderOBJ.ProdSite = "EMC-SITE";
            if (XmlOrderOBJ.RevisedXMLOrder())
            {
              cmbAppelBiblio.Text = ConvertValue(XmlOrderOBJ.AppelBiblio);
              cmbAppelIcono.Text  = ConvertValue(XmlOrderOBJ.AppelIcono);
              cmbArbreDeci.Text   = ConvertValue(XmlOrderOBJ.ArbreDeci);
              cmbIconoOk.Text     = ConvertValue(XmlOrderOBJ.IconoOk);
              cmbMcEn.Text        = ConvertValue(XmlOrderOBJ.McEn);
              cmbMcFr.Text        = ConvertValue(XmlOrderOBJ.McFr);
              cmbResumeEn.Text    = ConvertValue(XmlOrderOBJ.ResumeEn);
              cmbResumeFr.Text    = ConvertValue(XmlOrderOBJ.ResumeFr);
              cmbTitreEn.Text     = ConvertValue(XmlOrderOBJ.TitreEn);

              txtArticleType.Text = XmlOrderOBJ.ArticleType;
              txtChaptre.Text     = XmlOrderOBJ.Chaptre;
              txtColorModel.Text  = XmlOrderOBJ.ColorModel;
              txtDoi.Text         = XmlOrderOBJ.Doi;
              txtFasnumero.Text   = XmlOrderOBJ.Fasnumero;
              
              txtIsbn.Text        = XmlOrderOBJ.Isbn;
              txtIssn.Text        = XmlOrderOBJ.Issn;

              txtItemSubtitle.Text = XmlOrderOBJ.ItemSubtitle;
              txtItemTitle.Text    = XmlOrderOBJ.ItemTitle;

              txtLblvide.Text      = XmlOrderOBJ.Lblvide;
              txtMajAnne.Text      = XmlOrderOBJ.MajAnne;
              txtMajCote.Text      = XmlOrderOBJ.MajCote;

              txtMajNo.Text        = XmlOrderOBJ.MajNo;

              txtNbArbreIntractif.Text = XmlOrderOBJ.NbArbreIntractif;
               txtNbArbrePapier.Text   = XmlOrderOBJ.NbArbrePapier;

              txtNbAutoeval.Text       = XmlOrderOBJ.NbAutoeval;
              txtNbBiblio.Text         = XmlOrderOBJ.NbBiblio;
              txtNbBiblioSs.Text       = XmlOrderOBJ.NbBiblioSs;
              txtNbClinique.Text       = XmlOrderOBJ.NbClinique;
              txtNbDessin.Text         = XmlOrderOBJ.NbDessin;
              txtNbDocLegaux.Text      = XmlOrderOBJ.NbDocLegaux;
              txtNbEncadreT1.Text      = XmlOrderOBJ.NbEncadreT1;
              txtNbEncadreT2.Text      = XmlOrderOBJ.NbEncadreT2;
              txtNbFichePatient.Text   = XmlOrderOBJ.NbFichePatient;
              txtNbFicheTech.Text      = XmlOrderOBJ.NbFicheTech;
              txtNbFig.Text            = XmlOrderOBJ.NbFig;
              txtNbIconoSup.Text       = XmlOrderOBJ.NbIconoSup;
              txtNbPagesCommande.Text  = XmlOrderOBJ.NbPagesCommande;
              txtNbPagesEstimate.Text  = XmlOrderOBJ.NbPagesEstimate;
              txtNbPhoto.Text          = XmlOrderOBJ.NbPhoto;
              txtNbQuotidien.Text      = XmlOrderOBJ.NbQuotidien;
              txtNbSavoirPlus.Text     = XmlOrderOBJ.NbSavoirPlus;
              txtNbTableau.Text        = XmlOrderOBJ.NbTableau;
              txtNbvide.Text           = XmlOrderOBJ.Nbvide;
              txtNbVideo.Text          = XmlOrderOBJ.NbVideo;
              txtPii.Text              = XmlOrderOBJ.Pii;

              
              cmbStage.Text            = XmlOrderOBJ.Stage;
              txtSubchaptre.Text       = XmlOrderOBJ.Subchaptre;
              
              cmbTracode.Text          = XmlOrderOBJ.Tracode;
              cmbTracode.Text          = XmlOrderOBJ.JID;
              txtTraiteTitle.Text      = XmlOrderOBJ.TraiteTitle;
              txtTrajid.Text           = XmlOrderOBJ.Trajid;
              txtTraRoot.Text          = XmlOrderOBJ.TraRoot;

              txtTypesettingModelFormat.Text = XmlOrderOBJ.TypesettingModelFormat;

              txtVol.Text               = XmlOrderOBJ.Vol;
              txtPrincAuthorNom.Text    = XmlOrderOBJ.PrincAuthorNom;
              txtPrincAuthorPnom.Text   = XmlOrderOBJ.PrincAuthorPnom;
              txtPrincAuthorAff.Text    = XmlOrderOBJ.PrincAuthorAff;
              txtSecondAuthorNom.Text   = XmlOrderOBJ.SecondAuthorNom;
              txtSecondAuthorPnom.Text  = XmlOrderOBJ.SecondAuthorPnom;
              txtSecondAuthorAff.Text   = XmlOrderOBJ.SecondAuthorAff;
              txtCorrAuthorPhone.Text   = XmlOrderOBJ.CorrAuthorPhone;
              txtCorrAuthorFax.Text     = XmlOrderOBJ.CorrAuthorFAX;
              txtCorrAuthorEmail.Text   = XmlOrderOBJ.CorrAuthorEmail;
            }
        }
    }
    protected void cmbStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbStage.Text.Equals("S100", StringComparison.OrdinalIgnoreCase) == false)
            txtAid.AutoPostBack = true;
        else
            txtAid.AutoPostBack = false;
    }
}



#line default
#line hidden
