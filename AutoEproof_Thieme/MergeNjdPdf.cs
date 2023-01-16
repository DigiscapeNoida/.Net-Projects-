using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using ProcessNotification;

namespace AutoEproof
{

    public class MergeNjdPdf : MessageEventArgs
    {
        string _InputPDF = string.Empty;
        string InputPDF = string.Empty;

        public bool var = true;

        int pdfPages = 0;

        public MergeNjdPdf()
        {
        }
        public int PDFCount
        {
            get { return pdfPages; }
        }

        public bool StartProcess(string JID, string AID, string File1, string File2, string Stage)
        {
            bool result = false;
            if (Stage == "S100" || Stage == "S100Resupply")
            {
                try
                {
                    ProcessEventHandler(File1 + " for: " + JID + ", " + AID);
                    ProcessEventHandler(File2);
                    InputPDF = File1;
                    string[] JIDList = null;
                    string JIDListPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\RollOutJournalsInNewDesign.ini";
                    if (File.Exists(JIDListPath))
                    {
                        JIDList = File.ReadAllLines(JIDListPath);
                        List<string> a = JIDList.Where(x => x.Contains(JID)).ToList();

                        if (a.Count > 0)
                        {
                            int aidVal = Convert.ToInt32(a[0].Split('#')[1]);
                            if (Convert.ToInt32(AID) >= aidVal)
                            {
                                PdfReader reader = new PdfReader(File1);

                                int PageCount = reader.NumberOfPages;
                                for (int i = PageCount; i > (PageCount - 2); i--)
                                {
                                    string PageText = PdfTextExtractor.GetTextFromPage(reader, i);
                                    if (PageText.Contains("AUTHOR QUERY FORM"))
                                    {
                                        if (File.Exists(File2))
                                        {
                                            MergePDF(new string[2] { File1, File2 }, i);
                                            DeleteFile(File2);
                                        }
                                    }
                                    else
                                    {
                                        //LogWrite
                                    }
                                }
                                reader.Close();
                                reader = null;
                                DeleteFile(InputPDF);

                                MoveFile(InputPDF.Replace(".pdf", "_1.pdf"), InputPDF);
                                result = true;
                            }

                        }
                        else
                        {
                            result = false;
                        }


                        //else
                        //JID/AID not found in file
                    }
                    else
                    {
                        return result = false;
                        //RollOutJournalsInNewDesign.ini file is missing
                    }


                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                }
            }
            return result;
        }

        private static int GetPdfPageCount(string InputPDF)
        {
            PdfReader reader = null;
            try
            {
                reader = new PdfReader(InputPDF);
            }
            catch (Exception ex)
            {

            }
            int PageCount = reader.NumberOfPages;

            reader.Close();

            return PageCount;

        }


        private void MergePDF(string[] sSrcFile, int i)
        {
            string MergePDFPath = InputPDF.Replace(".pdf", "_1.pdf");
            MergeFiles(MergePDFPath, sSrcFile, i);
            if (File.Exists(MergePDFPath))
            {
                //DeleteFile(InputPDF);
                //MoveFile(MergePDFPath, InputPDF);                       
            }
        }

        private void DeleteFile(string FilePath)
        {
            while (true)
            {
                try
                {
                    File.Delete(FilePath);
                    break;
                }
                catch (Exception ex)
                {
                    // break;
                }
            }
        }
        private void MoveFile(string SourcePath, string TargetPath)
        {
            while (true)
            {
                try
                {
                    File.Move(SourcePath, TargetPath);
                    break;
                }
                catch (Exception ex)
                {
                    //break;
                }
            }
        }

        private bool MergeFiles(string DstFile, string[] SrcFiles, int i)
        {

            ////////////To test if already pdf has guide line page
            if (SrcFiles.Length > 1)
            {
                PdfReader Reader1 = new PdfReader(SrcFiles[1]);
                string FirstPage = System.Text.Encoding.Default.GetString(Reader1.GetPageContent(1));
                Reader1.Close();
                Reader1 = null;
            }

            bool bSucess = true;

            if (File.Exists(DstFile))
                File.Delete(DstFile);

            try
            {
                PdfReader Reader = new PdfReader(SrcFiles[0]);
                Document PdfDocument = new Document(Reader.GetPageSizeWithRotation(1));
                PdfCopy Writer = new PdfCopy(PdfDocument, new FileStream(DstFile, FileMode.Create));

                for (int X = 0; X < SrcFiles.Length; X++)
                {
                    if (File.Exists(SrcFiles[X]))
                    {
                        Reader = new PdfReader(SrcFiles[X]);
                        Reader.ConsolidateNamedDestinations();
                        int PdfPages = Reader.NumberOfPages;
                        PdfDocument.Open();
                        renameFields(Reader.AcroFields);

                        PdfImportedPage PdfPage;
                        for (int Y = 1; Y <= PdfPages; Y++)
                        {
                            if (Y == i)
                            {
                                PdfImportedPage PdfPage2;
                                PdfReader Reader2 = new PdfReader(SrcFiles[X + 1]);
                                Document PdfDocument2 = new Document(Reader2.GetPageSizeWithRotation(1));
                                Reader2.ConsolidateNamedDestinations();
                                int PdfPages2 = Reader2.NumberOfPages;
                                for (int Z = 1; Z <= PdfPages2; Z++)
                                {
                                    PdfPage2 = Writer.GetImportedPage(Reader2, Z);
                                    Writer.AddPage(PdfPage2);
                                }
                                X++;
                                PdfDocument2.Close();
                                PdfDocument2.Dispose();
                                Reader2.Close();
                                Reader2 = null;
                            }
                            PdfPage = Writer.GetImportedPage(Reader, Y);
                            Writer.AddPage(PdfPage);
                        }
                    }
                }

                Writer.CopyAcroForm(Reader);
                PdfDocument.Close();
                Writer.Close();
                Reader.Close();
                Reader = null;
                Writer.Dispose();
                PdfDocument.Dispose();


            }
            catch (Exception e)
            {
                bSucess = false;
            }

            return bSucess;
        }

        private void renameFields(AcroFields fields)
        {
            IDictionary<string, AcroFields.Item> Dfields = fields.Fields;
            foreach (KeyValuePair<string, AcroFields.Item> FN in Dfields)
            {
                fields.RenameField(FN.Value.ToString(), "TD_" + FN);
            }

        }


    }
}
