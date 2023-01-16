using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FMSIntegrateUsingEOOLink
{
    class PDFProcess
    {
        List<PdfObject> PdfObjects = new List<PdfObject>();
        string _InputPDF = string.Empty;
        int _PAGES = 0;
        int _Images = 0;


        public int Pages
        {
            get {return _PAGES; }
        }

        public int Images
        {
            get {return _Images; }
        }

        public PDFProcess(string InputPDF)
        { 
            _InputPDF = InputPDF;
            CountImagesAndPages();
        }

        private void CountImagesAndPages()
        {
            int LastPageCount = 0;
            //ExtractImagesFromPDF(_InputPDF, "c:\\1\\");
            // NOTE:  This will only get the first image it finds per page.
            PdfReader pdf = new PdfReader(_InputPDF);
            RandomAccessFileOrArray raf = new iTextSharp.text.pdf.RandomAccessFileOrArray(_InputPDF);
            _PAGES= pdf.NumberOfPages;
            try
            {
                // for (int pageNumber =2; pageNumber <= pdf.NumberOfPages; pageNumber++)
                for (int pageNumber = pdf.NumberOfPages; pageNumber > 0; pageNumber--)
                {
                    PdfDictionary pg = pdf.GetPageN(pageNumber);
                    // recursively search pages, forms and groups for images.
                    PdfObjects.Clear();
                    FindImageInPDFDictionary(pg);
                    if (PdfObjects.Count < 3)
                    {
                        _Images = _Images + PdfObjects.Count;
                    }
                    LastPageCount++;
                    if (LastPageCount >2 && PdfObjects.Count == 0)
                    {
                        break;
                    }
                }
            }
            catch
            {
                //throw;
            }
            finally
            {
                pdf.Close();
                raf.Close();
            }
            //ExtractImagesFromPDF(_InputPDF, "c:\\1\\");
        }
        private void ExtractImagesFromPDF(string sourcePdf, string outputPath)
        {
            // NOTE:  This will only get the first image it finds per page.
            PdfReader pdf = new PdfReader(sourcePdf);
            RandomAccessFileOrArray raf = new iTextSharp.text.pdf.RandomAccessFileOrArray(sourcePdf);
            int Count = 1;
            try
            {
                for (int pageNumber = 1; pageNumber <= pdf.NumberOfPages; pageNumber++)
                {
                    PdfDictionary pg = pdf.GetPageN(pageNumber);
                    // recursively search pages, forms and groups for images.
                    //PdfObject obj = FindImageInPDFDictionary(pg);
                    PdfObjects.Clear();
                    FindImageInPDFDictionary(pg);
                    foreach (PdfObject obj in PdfObjects)
                    {
                        int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        PdfObject pdfObj = pdf.GetPdfObject(XrefIndex);
                        PdfStream pdfStrem = (PdfStream)pdfObj;
                        byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)pdfStrem);
                        
                        if ((bytes != null))
                        {
                            try
                            {
                                System.IO.Stream memStream = new System.IO.MemoryStream(bytes);
                                
                                
                                    memStream.Position = 0;
                                    memStream.Seek(0, SeekOrigin.Begin);

                                    System.Drawing.Image img = System.Drawing.Image.FromStream(memStream, true, true);
                                    // must save the file while stream is open.
                                    if (!Directory.Exists(outputPath))
                                        Directory.CreateDirectory(outputPath);

                                    //string path = Path.Combine(outputPath, String.Format(@"{0}.jpg", pageNumber));
                                    string path = outputPath + "\\" + Count + ".jpg";
                                    Count++;
                                    System.Drawing.Imaging.EncoderParameters parms = new System.Drawing.Imaging.EncoderParameters(1);
                                    parms.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 0);
                                    System.Drawing.Imaging.ImageCodecInfo jpegEncoder = (System.Drawing.Imaging.ImageCodecInfo)ImageCodecInfo.GetImageEncoders().GetValue(1);
                                    img.Save(path, jpegEncoder, parms);

                                    memStream.Close();
                                
                            }
                            catch
                            { }
                        }
                    }
                }
            }
            catch
            {
                //throw;
            }
            finally
            {
                pdf.Close();
                raf.Close();
            }
        }


        
        private void FindImageInPDFDictionary(PdfDictionary pg)
        {
            PdfDictionary res = (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));
            PdfDictionary xobj = (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));

            if (xobj != null)
            {
                foreach (PdfName name in xobj.Keys)
                {
                    PdfObject obj = xobj.Get(name);
                    if (obj.IsIndirect())
                    {
                        PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);
                        PdfName type = (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));

                        //image at the root of the pdf
                        if (PdfName.IMAGE.Equals(type))
                        {
                            PdfObjects.Add(obj);
                        }// image inside a form
                        else if (PdfName.FORM.Equals(type))
                        {
                            FindImageInPDFDictionary(tg);
                        } //image inside a group
                        else if (PdfName.GROUP.Equals(type))
                        {
                            FindImageInPDFDictionary(tg);
                        }
                    }
                }
            }
        }
    }
}
