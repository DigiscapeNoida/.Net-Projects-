using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using MyINI;

public partial class CreateEcompSNT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         //CheckExtendedIsbn("9788131262931");
        //txtContactEmail.Text = "sudhi.singh@thomsondigital.com";
        //txtRecipientEmail.Text = "sudhi.singh@thomsondigital.com";
        txtContactEmail.Text = "rohit.singh@digiscapetech.com";
        txtRecipientEmail.Text = "rohit.singh@digiscapetech.com";

    }

    private bool CheckExtendedIsbn(string chkisbn)
    {
        
            int[] ChkEXISBN = new int[15];
            int j;
            int ChkNum;
            chkisbn = chkisbn.Replace("-", "");
            chkisbn = chkisbn.Replace(" ", "");

            ChkEXISBN[0] = 1 * Convert.ToInt32(chkisbn.Substring(0, 1));
            ChkEXISBN[1] = 3 * Convert.ToInt32(chkisbn.Substring(1, 1));
            ChkEXISBN[2] = 1 * Convert.ToInt32(chkisbn.Substring(2, 1));
            ChkEXISBN[3] = 3 * Convert.ToInt32(chkisbn.Substring(3, 1));
            ChkEXISBN[4] = 1 * Convert.ToInt32(chkisbn.Substring(4, 1));
            ChkEXISBN[5] = 3 * Convert.ToInt32(chkisbn.Substring(5, 1));
            ChkEXISBN[6] = 1 * Convert.ToInt32(chkisbn.Substring(6, 1));
            ChkEXISBN[7] = 3 * Convert.ToInt32(chkisbn.Substring(7, 1));
            ChkEXISBN[8] = 1 * Convert.ToInt32(chkisbn.Substring(8, 1));
            ChkEXISBN[9] = 3 * Convert.ToInt32(chkisbn.Substring(9, 1));
            ChkEXISBN[10] = 1 * Convert.ToInt32(chkisbn.Substring(10, 1));
            ChkEXISBN[11] = 3 * Convert.ToInt32(chkisbn.Substring(11, 1));

            ChkNum = 0;
            for (j = 0; j <= 11; j++)
            {
                ChkNum = ChkNum + ChkEXISBN[j];
            }
            ChkNum = ChkNum % 10;
            ChkNum = 10 - ChkNum;
            string c;
            if (ChkNum == 10)
            {
                c = "0";
            }
            else
            {
                c = ChkNum.ToString().Trim();
            }
            if (c == chkisbn.Substring(12, 1).ToUpper())
            {
                return true;
            }
            else
            {
                return false;

            }
        
    }
    protected void btnsubmitClick(Object sender, EventArgs e)
    {
        try
        {
            //string executingLocation = AppDomain.CurrentDomain.BaseDirectory;

            //txtCoverPath.Text = txtCoverPath.Text.Replace(@"\",@"\\");

            
            
            String title = txtTitle.Text.ToString().Trim();
            String isbn = txtISBN.Text.ToString().Trim();
            String contactName = txtContactName.Text.ToString().Trim();
            String contactEmail = txtContactEmail.Text.ToString().Trim();
            String recipientName = txtRecipientName.Text.ToString().Trim();
            String recipientEmail = txtRecipientEmail.Text.ToString().Trim();
            //String coverPath = txtCoverPath.Text.ToString().Trim();
            //String pdfPath = txtPdfPath.Text.ToString().Trim();
            String surName = txtSurName.Text.ToString().Trim();
            String MailBCC = txtMailBCC.Text.ToString().Trim();
            String MailCC = txtMailCC.Text.ToString().Trim();
            String ecopmFolder = ConfigurationSettings.AppSettings["serverFolder"];
            string minisbn = isbn.Replace("-","");

            //string coverPath = AppDomain.CurrentDomain.BaseDirectory;

            //if (!FileUpload1.HasFile)
            //    {
            //    Response.Write("<script>alert('Please upload cover.')</script>");
            //    return;
            //}
            if (!FileUpload2.HasFile)
            {
                Response.Write("<script>alert('Please upload pdf.')</script>");
                return;
            }
            //fileupload control contains a file  
            //    try
            //    {
            //        FileUpload1.SaveAs("E:\\" + FileUpload1.FileName);          // file path where you want to upload  
            //        //Label1.Text = "File Uploaded Sucessfully !! " + FileUpload1.PostedFile.ContentLength + "mb";     // get the size of the uploaded file  
            //    }
            //    catch (Exception ex)
            //    {
            //        //Label1.Text = "File Not Uploaded!!" + ex.Message.ToString();
            //    }
            //else
            //{
            //    Label1.Text = "Please Select File and Upload Again";

            //}



            //string fullcover = coverPath + "\\cover.jpg";

            //Commented by kumar vivek for MRW
            if (!isbn.Contains("-"))
            {
                Response.Write("<script>alert('Please put formatted ISBN.')</script>");
                return;
            }
            
            if (title == "" || isbn == "" || contactName == "" ||
                contactEmail == "" || recipientName == "" || recipientEmail == "" ||
                 surName == "")
            {
                Response.Write("<script>alert('Please fill Data in all mandatory Fields.')</script>");
                return;
            }

            //if (!File.Exists(fullcover))
            //{
            //    Response.Write("<script>alert('cover.jpg is not available at Cover Path" + fullcover + ".')</script>");
            //    return;
            //}

            //if (!File.Exists(pdfPath + "\\" + minisbn + "_WEB.pdf"))
            //{
            //    Response.Write("<script>alert('" + minisbn + "_WEB.pdf is not available at pdf Path.')</script>");
            //    return;
            //}

            if (!Directory.Exists(ecopmFolder))
            {
                Response.Write("<script>alert('172.16.0.179 server not available contact IT team.')</script>");
                return;
            }
            else if (Directory.Exists(ecopmFolder + "\\" + minisbn + "_" + surName))
            {
                Response.Write("<script>alert('Ecomp folder already created. Please delete it.')</script>");
                return;
            }
            else
            {
                Directory.CreateDirectory(ecopmFolder + "\\" + minisbn + "_" + surName);
                FileUpload2.SaveAs(ecopmFolder + "\\" + minisbn + "_" + surName + "\\e" + minisbn + ".pdf");
                FileUpload1.SaveAs(ecopmFolder + "\\" + minisbn + "_" + surName + "\\cover.jpg");

                //File.Copy(coverPath + "\\cover.jpg", ecopmFolder + "\\" + minisbn + "_" + surName + "\\cover.jpg");
                //File.Copy(pdfPath + "\\" + minisbn + "_WEB.pdf", ecopmFolder + "\\" + minisbn + "_" + surName + "\\e" + minisbn + ".pdf");
                // Crete ini file
                String iniFile = ecopmFolder + "\\" + minisbn + "_" + surName + "\\WOBL.ini";
                StreamWriter sw = new StreamWriter(iniFile);
                sw.WriteLine("#EComps System");
                sw.WriteLine("#" + System.DateTime.Today.ToLongDateString() + " " + System.DateTime.Today.ToLongTimeString());
                //string md5Val = GlobalFunction.GenerateMD5(strAuthorSurname);
                string md5Val = "md5Val"; // GlobalFunction.GenerateMD5(strForChecksumFolder);

                sw.WriteLine("md5Value=" + md5Val);
                //strLink = strLink + "/" + strAuthorSurname + "/" + md5Val;
                //strLink = strLink + "/" + strForChecksumFolder + "/" + md5Val;

                string strLink = "https://online.thomsondigital.com/ecompwebsite/Default.aspx?isbn=" + minisbn;
                long fileSize = new FileInfo(ecopmFolder + "\\" + minisbn + "_" + surName + "\\e" + minisbn + ".pdf").Length;

                sw.WriteLine("url=" + strLink);
                sw.WriteLine("Surname=" + surName);
                sw.WriteLine("ChapterID=----");

                sw.WriteLine("fromemail=corrections.esil@elsevier.thomsondigital.com");
                sw.WriteLine("ToEmail=----");
                //GlobalFunction.ConvertToHex("Jitender");
                sw.WriteLine("Receipent=" + ConvertToHex(recipientName));
                sw.WriteLine("Author=" + ConvertToHex(surName));
                sw.WriteLine("Book-Title=" + ConvertToHex(title));
                sw.WriteLine("Chapter-Label=----");
                sw.WriteLine("Chapter-Title=----");
                //Below change done by kumar vivek for MRW
                sw.WriteLine("ISBN=" + isbn);
                //sw.WriteLine("ISBN=978-0-08-102671-7");
                

                sw.WriteLine("Edition=1");
                sw.WriteLine("Date-Posted=" + System.DateTime.Today.ToShortDateString());
                sw.WriteLine("PDF-Path=\\" + ecopmFolder + "\\" + minisbn + "_" + surName + "\\e" + minisbn + ".pdf");
                sw.WriteLine("Cover-Path=\\" + ecopmFolder + "\\" + minisbn + "_" + surName + "\\cover.jpg");
                sw.WriteLine("FileSize=" + fileSize.ToString());
                sw.Close();


                //Send mail
                string executingLocation = AppDomain.CurrentDomain.BaseDirectory;
                string filebody = File.ReadAllText(executingLocation + "\\MailTemplate.htm");

                if (filebody != String.Empty)
                {
                    filebody = filebody.Replace("[BookTitle]",title);
                    filebody = filebody.Replace("[FormattedISBN]", isbn);
                    filebody = filebody.Replace("[ISBN]", minisbn);
                    filebody = filebody.Replace("[Name]", contactName);
                    filebody = filebody.Replace("[EMail]", contactEmail);
                    //filebody = filebody.Replace("[BootkTitle]", Title);
                }

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("eproof@elsevier.thomsondigital.com");
                mail.To.Add(recipientEmail);
                if (MailCC != String.Empty)
                    mail.CC.Add(MailCC);
                if(MailBCC != String.Empty)
                    mail.Bcc.Add(MailBCC);
                mail.Subject = "Your e-Book for \""+ title +"\"";
                mail.Body = filebody;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "65.182.191.169";
                smtp.Send(mail); //Stopped by vivek as mail not required for MRW

                Response.Write("<script>alert('Ecomp has been genrated and Mail sent to respective id.')</script>");
                return;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Some error occured Contact Tech team.')</script>");
            return;
        }


        // submit data in db and create folder and ini file and then send mail


    }

    public string ConvertToHex(string asciiString)
    {
        string hex = "";
        string mystring;
        foreach (char c in asciiString)
        {
            int tmp = c;
            string hexval = String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));

            mystring = "&#x";
            int zeros = 5 - hexval.Length;
            for (int j = 0; j < zeros; j++)
            {
                mystring = mystring + "0";
            }
            mystring = mystring + hexval;
            mystring = mystring + ";";

            hex += mystring;

        }
        return hex;
    }
}