using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutoEproof
{
    public class PDFFormProcess
    {
        private string _PDFInPath     = string.Empty;
        ArticleInfo _ArtObj = null;
        StringBuilder SB = new StringBuilder();

        public PDFFormProcess(string PDFInPath, ArticleInfo ArtObj)
        {
            _PDFInPath  = PDFInPath;
            _ArtObj     = ArtObj;
        }

        #region Set XML values in Article Info
        public string ProcessOnPDF()
        {
            try
            {
                //To Read a PDF Document Form
                PdfReader PdfRdr = new PdfReader(_PDFInPath);
                string PdfPath=_PDFInPath.Replace(".pdf","_1.pdf");

                PdfStamper stamper = new PdfStamper(PdfRdr, new System.IO.FileStream(PdfPath, System.IO.FileMode.Create), '\0', true);
                
                //Assign value to PDF Form
                if (String.IsNullOrEmpty(_ArtObj.JournalTitle))
                    _ArtObj.JournalTitle = " ";
                else if (String.IsNullOrEmpty(_ArtObj.ArticleTitle))
                    _ArtObj.ArticleTitle = " ";
                else if (String.IsNullOrEmpty(_ArtObj.Authors))
                    _ArtObj.Authors = " ";
                else if (String.IsNullOrEmpty(_ArtObj.ContactData))
                    _ArtObj.ContactData = " ";
                else if (String.IsNullOrEmpty(_ArtObj.PEName))
                    _ArtObj.PEName = " ";

                stamper.AcroFields.SetField("Journal", _ArtObj.JournalTitle);
                stamper.AcroFields.SetField("Manuscript", _ArtObj.ArticleTitle);
                stamper.AcroFields.SetField("Manuscript Title", _ArtObj.ArticleTitle);
                stamper.AcroFields.SetField("Manuscript Number", _ArtObj.AID);
                stamper.AcroFields.SetField("Authors", _ArtObj.Authors);
                stamper.AcroFields.SetField("Corresponding authorʼs contact data", _ArtObj.ContactData);

                if (!String.IsNullOrEmpty(_ArtObj.CorEmail))
                { 
                    stamper.AcroFields.SetField("Corresponding authorʼs e-mail address", _ArtObj.CorEmail); 
                    stamper.AcroFields.SetField("Corresponding authorʼs", _ArtObj.CorEmail);
                }
                else { 
                    stamper.AcroFields.SetField("Corresponding authorʼs e-mail address", _ArtObj.AuthorEmail);
                    stamper.AcroFields.SetField("Corresponding authorʼs", _ArtObj.CorEmail);
                }


                stamper.AcroFields.SetField("Contact at the publishers", _ArtObj.PEName);
                stamper.AcroFields.SetField("E-mail address at the publishers", _ArtObj.PEEmail);
                stamper.AcroFields.SetField("E-mail address at \nthe publishers", _ArtObj.PEEmail);


                //stamper.AcroFields.SetField("Interest", "nein");// Ja,nein

                //Select Output PDF Type
                stamper.FormFlattening = false; //Editable PDF Form 
                //stamper.FormFlattening = true; //Non-Editable PDF Form

                stamper.Close();
                PdfRdr.Close();
              
           if (File.Exists(_PDFInPath))
               File.Delete(_PDFInPath);

            if (File.Exists(PdfPath) && !File.Exists(_PDFInPath))
            {
                File.Copy(PdfPath,PdfPath.Replace("_1.pdf",".pdf"));
                File.Delete(PdfPath);
            }
                return "Yes";
            }
            catch (Exception ex)
            {
                return "Article Info should be in proper manner...!!!";
            }
        }


        public string ProcessOnWSBPDF()
        {
            try
            {
                //To Read a PDF Document Form
                PdfReader PdfRdr = new PdfReader(_PDFInPath);
                string PdfPath = _PDFInPath.Replace(".pdf", "_1.pdf");

                PdfStamper stamper = new PdfStamper(PdfRdr, new System.IO.FileStream(PdfPath, System.IO.FileMode.Create), '\0', true);

                //Assign value to PDF Form
                if (String.IsNullOrEmpty(_ArtObj.JournalTitle))
                    _ArtObj.JournalTitle = " ";
                else if (String.IsNullOrEmpty(_ArtObj.ArticleTitle))
                    _ArtObj.ArticleTitle = " ";
                else if (String.IsNullOrEmpty(_ArtObj.Authors))
                    _ArtObj.Authors = " ";
                else if (String.IsNullOrEmpty(_ArtObj.ContactData))
                    _ArtObj.ContactData = " ";
                else if (String.IsNullOrEmpty(_ArtObj.PEName))
                    _ArtObj.PEName = " ";

                stamper.AcroFields.SetField("Article DOI", _ArtObj.AID);
                stamper.AcroFields.SetField("Article Title", _ArtObj.ArticleTitle);
                stamper.AcroFields.SetField("Author List", _ArtObj.Authors);
                stamper.FormFlattening = false; //Editable PDF Form 
                stamper.Close();
                PdfRdr.Close();

                

                if (File.Exists(_PDFInPath))
                    File.Delete(_PDFInPath);

                if (File.Exists(PdfPath) && !File.Exists(_PDFInPath))
                {
                    File.Copy(PdfPath, PdfPath.Replace("_1.pdf", ".pdf"));
                    File.Delete(PdfPath);
                }
                return "Yes";
            }
            catch (Exception ex)
            {
                return "Article Info should be in proper manner...!!!";
            }
        }
        #endregion
    }
}
