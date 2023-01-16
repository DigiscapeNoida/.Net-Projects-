using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Data;
using iSED;

namespace AutoEproof
{
    class QueryLinking 
    {
        public static iSED.QuickPDF PDF = new QuickPDFClass();
        public static int Pages = 0;
        public static DataTable dt1;
        public static string OutPdfPath = "";
        public QueryLinking()
        {
        }
        public bool Start(string args)
        {
            bool result = false;
            try
            {
                Console.WriteLine("Query Linking Process Started.........");
                if (PDF.Unlocked() == 0)
                    PDF.UnlockKey("313738326707E21A4CE37B9655B94B56");
                dt1 = new DataTable();
                dt1.Columns.Add(new DataColumn("Query", typeof(string)));
                dt1.Columns.Add(new DataColumn("Left", typeof(double)));
                dt1.Columns.Add(new DataColumn("Top", typeof(double)));
                dt1.Columns.Add(new DataColumn("Width", typeof(double)));
                dt1.Columns.Add(new DataColumn("Height", typeof(double)));
                dt1.Columns.Add(new DataColumn("Page", typeof(int)));
                dt1.Columns.Add(new DataColumn("Position", typeof(double)));
                dt1.Columns.Add(new DataColumn("Option", typeof(int)));

                OutPdfPath = args.Replace("_Temp.pdf", ".pdf").Replace("_temp.pdf", ".pdf");

                Console.WriteLine("Process Started.........");
                Get_Data(args);
                Console.WriteLine("Preparing for linking.......");
                Create_AddLinkToPage();
                Pages = 0;
                System.Threading.Thread.Sleep(10000);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                OutPdfPath = string.Empty;
                dt1 = null;
              //  PDF = null;
                Pages = 0;
            }
            return result;
        }
        public void Get_Data(string Input_Path)
        {

            //////if (Directory.Exists(OutPdfPath))
            //////{
            //////    File.Copy(Input_Path, OutPdfPath + Path.GetFileName(Input_Path), true);
            //////}
            //////else
            //////{
            //////    Directory.CreateDirectory(OutPdfPath);
            //////    File.Copy(Input_Path, OutPdfPath + Path.GetFileName(Input_Path), true);
            //////}


            if (File.Exists(Input_Path))
            {
                File.Copy(Input_Path, OutPdfPath, true);
                PDF.LoadFromFile(OutPdfPath);
                if (PDF.LoadFromFile(OutPdfPath) == 0)
                {
                    Console.WriteLine("Unable to read file....");
                }
                Pages = PDF.PageCount();
                ArrayList PDF_Content = new ArrayList();
                Int32 I;
                for (I = 1; I <= Pages; I++)
                {
                    try
                    {
                        string strPage = "";
                        PDF.SelectPage(I);
                        strPage = PDF.GetPageText(3);
                        if (strPage != null)
                        {
                            strPage = strPage.Trim();
                            string[] strLine;
                            strLine = strPage.Split(Environment.NewLine.ToCharArray());
                            Int32 J;
                            for (J = 0; J <= strLine.Length - 1; J++)
                            {
                                if (strLine[J].Trim() != "")
                                {
                                    //strLine[J] = RemoveChar(strLine[J], "\"", true);
                                    //strLine[J] = RemoveChar(strLine[J], "\"", false);
                                    string[] Content_Line = new string[10];
                                    string[] strContent;
                                    strContent = strLine[J].Split(',');

                                    string QueryValue = "";
                                    string QueryTextColor = "";
                                    double QueryTextSize = 0;
                                    double QueryTextLeftPosition = 0;
                                    double QueryTextTopPosition = 0;
                                    double QueryTextRightPosition = 0;
                                    double QueryTextBottomPosition = 0;
                                    //Text Color
                                    QueryTextColor = strContent[1];
                                    //Text Size
                                    // QueryTextSize = Convert.ToDouble(strContent[2]);

                                    bool result = double.TryParse(strContent[2], out QueryTextSize);
                                    if (result)
                                        QueryTextSize = Convert.ToDouble(strContent[2]);
                                    //Text Left Position
                                    result = double.TryParse(strContent[3], out QueryTextLeftPosition);
                                    if (result)
                                        QueryTextLeftPosition = Convert.ToDouble(strContent[3]);
                                    //Text Top Position
                                    result = double.TryParse(strContent[8], out QueryTextTopPosition);
                                    if (result)
                                        QueryTextTopPosition = Convert.ToDouble(strContent[8]);
                                    //Text Right Position
                                    result = double.TryParse(strContent[5], out QueryTextRightPosition);
                                    if (result)
                                        QueryTextRightPosition = Convert.ToDouble(strContent[5]);
                                    //Text Bottom Position
                                    result = double.TryParse(strContent[4], out QueryTextBottomPosition);
                                    if (result)
                                        QueryTextBottomPosition = Convert.ToDouble(strContent[4]);
                                    //Text Value 
                                    if (!string.IsNullOrEmpty(strContent[11].ToString()))
                                    {
                                        QueryValue = strContent[11].ToString();
                                        QueryValue = QueryValue.Replace("\"", "");
                                        QueryValue = QueryValue.Replace("(", "");
                                        QueryValue = QueryValue.Trim();
                                        if (QueryValue.StartsWith("Q") == true || QueryValue.EndsWith("Q") == true)
                                        {
                                            string QueryNumber = "";
                                            QueryNumber = QueryValue.Replace("Q", "").ToString();
                                            if (IsNumeric(QueryNumber) == true && QueryNumber.ToString().Length <= 3)
                                            {
                                                double Left, Top, Width, Height;
                                                Left = QueryTextLeftPosition; //Left
                                                Top = Math.Round(QueryTextTopPosition, 4); //Top
                                                //Top = Math.Round(QueryTextTopPosition + QueryTextRightPosition, 4);
                                                Width = Math.Round(QueryTextRightPosition - QueryTextLeftPosition, 4); //Width
                                                Height = Math.Round(QueryTextTopPosition - QueryTextBottomPosition, 4); //Height
                                                DataRow dr1 = dt1.NewRow();
                                                dr1[0] = QueryValue;
                                                dr1[1] = Left;
                                                dr1[2] = Top;
                                                dr1[3] = Width;
                                                dr1[4] = Height;
                                                dr1[5] = I;
                                                dr1[6] = Top + 70;
                                                dr1[7] = 0;
                                                dt1.Rows.Add(dr1);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {

                        // throw;
                    }   //
                }
            }
            else
            {
                Console.WriteLine("File not found..........");
            }
        }
        private void Create_AddLinkToPage()
        {
            for (int r = 0; r <= dt1.Rows.Count - 1; r++)
            {
                for (int r1 = r + 1; r1 <= dt1.Rows.Count - 1; r1++)
                {
                    if (dt1.Rows[r][0].ToString().Trim() == dt1.Rows[r1][0].ToString().Trim())
                    {
                        PDF.SelectPage(Convert.ToInt32(dt1.Rows[r][5]));
                        PDF.AddLinkToPage(Convert.ToDouble(dt1.Rows[r][1]), Convert.ToDouble(dt1.Rows[r][2]), Convert.ToDouble(dt1.Rows[r][3]), Convert.ToDouble(dt1.Rows[r][4]), Convert.ToInt32(dt1.Rows[r1][5]), Convert.ToDouble(dt1.Rows[r1][6]), Convert.ToInt32(dt1.Rows[r][7]));
                        PDF.SelectPage(Convert.ToInt32(dt1.Rows[r1][5]));
                        PDF.AddLinkToPage(Convert.ToDouble(dt1.Rows[r1][1]), Convert.ToDouble(dt1.Rows[r1][2]), Convert.ToDouble(dt1.Rows[r1][3]), Convert.ToDouble(dt1.Rows[r1][4]), Convert.ToInt32(dt1.Rows[r][5]), Convert.ToDouble(dt1.Rows[r][6]), Convert.ToInt32(dt1.Rows[r1][7]));
                        PDF.SaveToFile(OutPdfPath);
                        Console.WriteLine("For " + dt1.Rows[r][0].ToString().Trim() + " link added successfully.");

                    }
                }
            }
        }
        private static string RemoveChar(string IString, string IChar, bool bPos)
        {
            IString = IString.Trim();
            if (bPos == true)
            {
                if (IString.Substring(0, 1) == IChar)
                {
                    IString = IString.Substring(1, IString.Length - 1);
                }
                else
                {
                    if (IString.Substring(IString.Length - 1, 1) == IChar)
                    {
                        IString = IString.Substring(0, IString.Length);
                    }
                }

            }
            return IString.Trim();
        }
        internal static bool IsNumeric(object ObjectToTest)
        {
            if (ObjectToTest == null)
            {
                return false;

            }
            else
            {
                double OutValue;
                return double.TryParse(ObjectToTest.ToString().Trim(),
                    System.Globalization.NumberStyles.Any,

                    System.Globalization.CultureInfo.CurrentCulture,

                    out OutValue);
            }
        }
    }
}
