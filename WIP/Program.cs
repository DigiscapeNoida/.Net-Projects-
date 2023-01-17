using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Configuration;

namespace WIP_New
{
    internal class Program
    {
        static Log log = new Log();
        static bool del = false;
        static void Main(string[] args)
        {
            string inpath = System.Windows.Forms.Application.StartupPath + "\\input";
            string outpath = System.Windows.Forms.Application.StartupPath + "\\output\\";
            string failpath = System.Windows.Forms.Application.StartupPath + "\\fail\\";
            string successpath = System.Windows.Forms.Application.StartupPath + "\\success\\";
            string[] fls = Directory.GetFiles(inpath, "*.xlsx", SearchOption.TopDirectoryOnly);
            string conn = "";
            if (fls.Length > 0)
            {
                log.Generatelog("===========================================================================================");
                log.Generatelog("Processing file " + Path.GetFileName(fls[0]));
                System.Data.DataTable dt = new System.Data.DataTable();
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fls[0] + ";Extended Properties='Excel 12.0 XML;HDR=YES';";
                using (OleDbConnection con = new OleDbConnection(conn))
                {
                    try
                    {
                        OleDbDataAdapter oleAdpt1 = new OleDbDataAdapter("select * from [WIP$]", con); //here we read data from sheet1  
                        oleAdpt1.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        log.Generatelog("Error found " + Path.GetFileName(fls[0]) + " " + ex.Message.ToString());
                        del = true;
                    }
                }
                if (dt.Rows.Count > 0)
                {
                Loop:
                    Console.WriteLine("Please enter report generation date (dd-mm-yyyy) : ");
                    string rpt_date = Console.ReadLine();
                    DateTime rptdt;
                    try
                    {
                        rptdt = DateTime.Parse(rpt_date);
                    }
                    catch
                    {
                        Console.WriteLine("Date format is not correct.");
                        goto Loop;
                    }
                Loop1:
                    Console.WriteLine("Please enter exchange rate : ");
                    string rt = Console.ReadLine();
                    decimal ex_rate;
                    try
                    {
                        decimal f = Convert.ToDecimal(rt);
                        ex_rate = Math.Round(f, 2);
                    }
                    catch
                    {
                        Console.WriteLine("Exchange rate is not valid : ");
                        goto Loop1;
                    }
                    //Killexl();
                    Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Cells[1, 1] = "S. No.";
                    xlWorkSheet.Cells[1, 2] = "PROJ CODE";
                    xlWorkSheet.Cells[1, 3] = "PROJECT NAME";
                    xlWorkSheet.Cells[1, 4] = "CUST- SEND DATE";
                    xlWorkSheet.Cells[1, 5] = "STATUS";
                    xlWorkSheet.Cells[1, 6] = "REAL PAGES/Illustration/Figure/Darts";
                    xlWorkSheet.Cells[1, 7] = "STAGE";
                    xlWorkSheet.Cells[1, 8] = "CLIENT-WISE";
                    xlWorkSheet.Cells[1, 9] = "SEGMENT";
                    xlWorkSheet.Cells[1, 10] = "AGEING (In Days)";
                    xlWorkSheet.Cells[1, 11] = "AGEING PERIOD";
                    xlWorkSheet.Cells[1, 12] = "RATE/PAGE OR APPROX BILLING AMT.";
                    xlWorkSheet.Cells[1, 13] = "CURRENCY";
                    xlWorkSheet.Cells[1, 14] = "EXCHANGE RATE";
                    xlWorkSheet.Cells[1, 15] = "REALISATION VALUE (Rs.)";
                    xlWorkSheet.Cells[1, 16] = "VALUE AT COST (Rs.)";
                    xlWorkSheet.Cells[1, 17] = "LOWER OF THE TWO (Rs.)";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString().Trim().Length > 0)
                        {
                            int ts = 0;
                            try
                            {
                                DateTime dt1 = DateTime.Parse(dt.Rows[i][6].ToString());
                                ts = (rptdt - dt1).Days;
                            }
                            catch (Exception ex)
                            {
                                int p = i + 2;
                                log.Generatelog("Error found in cell G"+ p + " : " + ex.Message);
                                ts = 0;
                                del = true;
                            }
                            for (int j = 1; j < 18; j++)
                            {
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[i + 2, j] = dt.Rows[i][6];
                                }
                                else if (j == 5)
                                {
                                    int q=i+2;
                                    string q1 = "F" + q;
                                    xlWorkSheet.Cells[i + 2, j] = Task_status(dt.Rows[i][5].ToString(), q1);
                                }
                                else if (j == 6)
                                {
                                    xlWorkSheet.Cells[i + 2, j] = dt.Rows[i][4];
                                }
                                else if (j == 7)
                                {
                                    xlWorkSheet.Cells[i + 2, j] = dt.Rows[i][5];
                                }
                                else if (j == 8)
                                {
                                    xlWorkSheet.Cells[i + 2, j] = dt.Rows[i][9];
                                }
                                else if (j == 9)
                                {
                                    xlWorkSheet.Cells[i + 2, j] = "Elsevier books";
                                }
                                else if (j == 10)
                                {
                                    if (ts == 0)
                                    {
                                        xlWorkSheet.Cells[i + 2, j] = "";
                                    }
                                    else
                                    {
                                        xlWorkSheet.Cells[i + 2, j] = ts.ToString();
                                    }
                                }
                                else if (j == 11)
                                {
                                    if (ts == 0)
                                    {
                                        xlWorkSheet.Cells[i + 2, j] = "";
                                    }
                                    if (ts > 0 && ts <= 90)
                                    {
                                        xlWorkSheet.Cells[i + 2, j] = "0-90 Days";
                                    }
                                    if (ts >= 91 && ts <= 180)
                                    {
                                        xlWorkSheet.Cells[i + 2, j] = "91-180 Days";
                                    }
                                    if (ts >= 181 && ts <= 365)
                                    {
                                        xlWorkSheet.Cells[i + 2, j] = "181-365 Days";
                                    }
                                    if (ts > 365)
                                    {
                                        xlWorkSheet.Cells[i + 2, j] = "Above 365 Days";
                                    }
                                }
                                else if (j == 12)
                                {
                                    try
                                    {
                                        xlWorkSheet.Cells[i + 2, j] = Cost(dt.Rows[i][4].ToString(), dt.Rows[i][10].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        xlWorkSheet.Cells[i + 2, j] = "";
                                        int p=i + 2;
                                        log.Generatelog("Error in page/rate (cell E"+p+"/cell K"+p+") : " + ex.Message);
                                        del = true;
                                    }
                                }
                                else if (j == 13)
                                {
                                    xlWorkSheet.Cells[i + 2, j] = "USD";
                                }
                                else if (j == 14)
                                {
                                    xlWorkSheet.Cells[i + 2, j] = ex_rate;
                                }
                                else if (j == 15)
                                {
                                    xlWorkSheet.Cells[i + 2, j] = Cost1(dt.Rows[i][4].ToString(), dt.Rows[i][10].ToString(), ex_rate.ToString());
                                }
                                else if (j == 16)
                                {
                                    xlWorkSheet.Cells[i + 2, j] = Cost2(dt.Rows[i][4].ToString(), dt.Rows[i][10].ToString(), ex_rate.ToString());
                                }
                                else if (j == 17)
                                {
                                    xlWorkSheet.Cells[i + 2, j] = Cost2(dt.Rows[i][4].ToString(), dt.Rows[i][10].ToString(), ex_rate.ToString());
                                }
                                else
                                {
                                    xlWorkSheet.Cells[i + 2, j] = dt.Rows[i][j - 1];
                                }
                            }
                        }
                    }
                    string f1 = "";
                    if (del == true)
                    {
                        f1 = outpath + Path.GetFileNameWithoutExtension(fls[0]) + "_error.xlsx";
                        try
                        {
                            File.Move(fls[0], failpath + Path.GetFileName(fls[0]));
                            log.Generatelog("File " + Path.GetFileName(fls[0]) + " moved in fail directory.");
                        }
                        catch { }
                    }
                    else
                    {
                        f1 = outpath + Path.GetFileNameWithoutExtension(fls[0]) + "_ok.xlsx";
                        try
                        {
                            File.Move(fls[0], successpath + Path.GetFileName(fls[0]));
                            log.Generatelog("File " + Path.GetFileName(fls[0]) + " moved in success directory.");
                        }
                        catch { }
                    }
                    if (File.Exists(f1))
                    {
                        File.Delete(f1);
                    }
                    try
                    {
                        xlWorkBook.SaveCopyAs(f1);
                        if (f1.EndsWith("_ok.xlsx"))
                        {
                            log.Generatelog("Output file " + Path.GetFileName(f1) + " created without error.");
                        }
                        if (f1.EndsWith("_error.xlsx"))
                        {
                            log.Generatelog("Output file " + Path.GetFileName(f1) + " created with error(s).");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Generatelog("Error found in creating output : " + ex.Message);
                    }
                }
            }
        }
        static void Killexl()
        {
            System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process p in localByName)
            {
                try
                {
                    p.Kill();
                }
                catch
                {
                    continue;
                }
            }
        }
        static string Task_status(string a, string b)
        {
            string ret = "";
            try
            {
                string[] lns = File.ReadAllLines(System.Windows.Forms.Application.StartupPath + "\\stage.txt");
                foreach (string l in lns)
                {
                    if (a.Trim().ToLower() == l.Split('|')[0].Trim().ToLower())
                    {
                        ret = l.Split('|')[1].Trim();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Generatelog("Error found in stage.txt file : " + ex.Message);
            }
            if (ret == "")
            {
                log.Generatelog("Error found in cell "+b+". Stage \"" + a + "\" not defined.");
                del = true;
                Thread.Sleep(300);
            }
            return ret;
        }
        static string Cost(string page, string rate)
        {
            string ret1 = "";
            //try
            //{
                decimal a = Convert.ToDecimal(page);
                decimal b = Convert.ToDecimal(rate);
                decimal c = Math.Round(a * b, 2);
                ret1 = c.ToString();
            //}
            //catch (Exception ex)
            //{
                //log.Generatelog("Error in page/rate " + page + "/" + rate + " : " + ex.Message);
                //del = true;
            //}
            return ret1;
        }
        static string Cost1(string page, string rate, string cur)
        {
            string ret1 = "";
            try
            {
                decimal a = Convert.ToDecimal(page);
                decimal b = Convert.ToDecimal(rate);
                decimal d = Convert.ToDecimal(cur);
                decimal c = Math.Round(a * b * d, 2);
                ret1 = c.ToString();
            }
            catch
            {
                // log.Generatelog("Error : " + ex.Message);
                del = true;
            }
            return ret1;
        }
        static string Cost2(string page, string rate, string cur)
        {
            string ret1 = "";
            try
            {
                decimal a = Convert.ToDecimal(page);
                decimal b = Convert.ToDecimal(rate);
                decimal d = Convert.ToDecimal(cur);
                decimal c = Math.Round(a * b * d * 80 / 100, 2);
                ret1 = c.ToString();
            }
            catch
            {
                //log.Generatelog("Error : " + ex.Message);
                del = true;
            }
            return ret1;
        }
    }
}
