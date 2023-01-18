using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Word;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using System.Threading;

namespace LNPageCount
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string str = Program.PageCountInput();
                Console.WriteLine(str);
                string str1 = Program.PageCountOutput();
                Console.WriteLine(str1);
                Console.WriteLine("Waiting for 10 min.");
                Thread.Sleep(600000);
            }
        }

        //D:\Udit\AR07BM0002


        static string PageCountInput()
        {
            StringBuilder text = new StringBuilder();
            List<string> list = new List<string>();
            string arID = "";

            //String str = ConfigurationSettings.AppSettings["ConnString"].ToString();
            //config
            String str = System.Configuration.ConfigurationSettings.AppSettings["ConnString"].ToString();
            //string constr = "Data Source=172.16.0.61;Initial Catalog=PTS4LN_TEST;Persist Security Info=True;User ID=sa;Password=p@ssw0rd";
            SqlConnection con = new SqlConnection(str);
            try
            {
                con.Open();
                string sql = "SELECT * FROM Article_Details where (pagecount is NULL OR wordcount is null or Charactercount is null) and Active='yes'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter sqlad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sqlad.Fill(ds);
                con.Close();
                int num = 0;
                int wordcount = 0;
                int lettercount = 0;
                int numout = 0;
                int wordcountout = 0;
                int lettercountout = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        try
                        {
                            arID = ds.Tables[0].Rows[i]["ArticleID"].ToString();
                            WriteToFile("Ärticle ID: " + arID.ToString() + "");
                            Console.WriteLine("Ärticle ID: " + arID.ToString() + "");
                            // config
                            //string pth = @"D:\Udit\" + arID ;
                            string pth = System.Configuration.ConfigurationSettings.AppSettings["path"].ToString() + arID;
                            //string pth1 = System.Configuration.ConfigurationSettings.AppSettings["path1"].ToString() + arID;
                            if (!Directory.Exists(pth))
                            {
                                WriteToFile("FolderNOTFound: " + arID.ToString() + "");
                                Console.WriteLine("FolderNOTFound: " + arID.ToString() + "");
                            }
                            else
                            {
                                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                                object miss = System.Reflection.Missing.Value;
                                //object path = @"D:\Udit\" + item+".docx";
                                string[] asd = Directory.GetFiles(pth);
                                if (asd.Count() > 0)
                                {
                                    for (int count = 0; count < asd.Count(); count++)
                                    {
                                        WriteToFile("FileFound: " + arID.ToString() + "");
                                        string aa = asd[count].ToString();
                                        object path = aa;

                                        string fileName = Path.GetFileName(aa);
                                        FileInfo fi = new FileInfo(fileName);
                                        string ext = fi.Extension.ToLower();
                                        if (ext == ".doc" || ext == ".docx")
                                        {
                                            object readOnly = true;
                                            try
                                            {
                                                Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                                                Microsoft.Office.Interop.Word.WdStatistic stat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;
                                                num = docs.ComputeStatistics(stat, ref miss);

                                                lettercount = docs.ComputeStatistics(WdStatistic.wdStatisticCharactersWithSpaces, ref miss);
                                                wordcount = docs.ComputeStatistics(WdStatistic.wdStatisticWords, ref miss);

                                                docs.Close(false);
                                                UpdateDBInput(arID.ToString(), num, wordcount, lettercount);
                                                Console.WriteLine("Database updated for :- " + arID.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("Document File is Currupted in Folder :- " + arID.ToString());
                                                UpdateDB(arID.ToString(), 0, 0, 0);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("File Extension Not Valid ");
                                        }
                                    }
                                }

                                // UpdateDBInput(arID.ToString(),num,wordcount,lettercount);
                                //SqlConnection conn = new SqlConnection(str);
                                //SqlCommand sqlcmd = new SqlCommand("[UpdatePageCountInput]", conn);
                                //sqlcmd.CommandType = CommandType.StoredProcedure;
                                //sqlcmd.Parameters.AddWithValue("@ArticleID", arID.ToString());
                                //sqlcmd.Parameters.AddWithValue("@PageCount", num.ToString());
                                //sqlcmd.Parameters.AddWithValue("@WordCount", wordcount.ToString());
                                //sqlcmd.Parameters.AddWithValue("@LetterCount", lettercount.ToString());
                                //conn.Open();
                                //sqlcmd.ExecuteNonQuery();
                                //conn.Close();



                                word.Quit(false);



                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                WriteToFile(ex.Message);
            }





            return text.ToString();

        }
        static string PageCountOutput()
        {
            StringBuilder text = new StringBuilder();
            List<string> list = new List<string>();
            string arID = "";


            //config
            String str = System.Configuration.ConfigurationSettings.AppSettings["ConnString"].ToString();
            SqlConnection con = new SqlConnection(str);
            try
            {
                con.Open();
                string sql = "SELECT * FROM Article_Details where (pagecountout is NULL OR wordcountout is null or Charactercountout is null) and Active='yes'";
                SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sqlad.Fill(ds);
                con.Close();
                int numout = 0;
                int wordcountout = 0;
                int lettercountout = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        try
                        {
                            arID = ds.Tables[0].Rows[i]["ArticleID"].ToString();
                            WriteToFile("Output Ärticle ID: " + arID.ToString() + "");
                            Console.WriteLine("Output Ärticle ID: " + arID.ToString() + "");
                            // config
                            //string pth = @"D:\Udit\" + arID ;
                            string pth = System.Configuration.ConfigurationSettings.AppSettings["path1"].ToString() + arID;

                            if (!Directory.Exists(pth))
                            {
                                WriteToFile("Output Folder NOT Found: " + arID.ToString() + "");
                                Console.WriteLine("Output Folder NOT Found: " + arID.ToString() + "");
                            }
                            else
                            {
                                try
                                {
                                    Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                                    object miss = System.Reflection.Missing.Value;
                                    //object path = @"D:\Udit\" + item+".docx";
                                    string[] asd = Directory.GetFiles(pth);
                                    if (asd.Count() > 0)
                                    {
                                        for (int count = 0; count < asd.Count(); count++)
                                        {
                                            WriteToFile("Output File Found: " + arID.ToString() + "");
                                            Console.WriteLine("Output File Found: " + arID.ToString() + "");
                                            string aa = asd[count].ToString();
                                            object path = aa;
                                            string fileName = Path.GetFileName(aa);
                                            FileInfo fi = new FileInfo(fileName);
                                            string ext = fi.Extension;
                                            if (ext == ".doc" || ext == ".docx" || ext == ".DOC" || ext == ".DOCX")
                                            {

                                                object readOnly = true;
                                                try
                                                {
                                                    Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                                                    Microsoft.Office.Interop.Word.WdStatistic stat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;
                                                    numout = docs.ComputeStatistics(stat, ref miss);

                                                    lettercountout = docs.ComputeStatistics(WdStatistic.wdStatisticCharactersWithSpaces, ref miss);
                                                    wordcountout = docs.ComputeStatistics(WdStatistic.wdStatisticWords, ref miss);

                                                    docs.Close(false);
                                                    UpdateDB(arID.ToString(), numout, wordcountout, lettercountout);
                                                }

                                                catch (Exception e)
                                                {
                                                    UpdateDB(arID.ToString(), 0, 0, 0);

                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("File Extension Not Valid");
                                    }

                                    word.Quit(false);
                                }
                                catch (Exception ex)
                                {
                                    WriteToFile(ex.Message.ToString());
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteToFile(ex.Message);
            }

            return text.ToString();
        }
        static void UpdateDB(string ArticleID, int pageCount, int wordCount, int lettercount)
        {
            // call from db
            //String str = "Data Source=172.16.0.61;Initial Catalog=PTS4LN_TEST;Persist Security Info=True;User ID=sa;Password=p@ssw0rd";
            String str = System.Configuration.ConfigurationSettings.AppSettings["ConnString"].ToString();
            //String str = System.Configuration.ConfigurationSettings.AppSettings["ConnString"].ToString();

            SqlConnection conn = new SqlConnection(str);
            SqlCommand sqlcmd = new SqlCommand("[UpdatePageCountOutput]", conn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ArticleID", ArticleID.ToString());
            sqlcmd.Parameters.AddWithValue("@PageCountOut", pageCount.ToString());
            sqlcmd.Parameters.AddWithValue("@WordCountOut", wordCount.ToString());
            sqlcmd.Parameters.AddWithValue("@LetterCountOut", lettercount.ToString());
            conn.Open();
            sqlcmd.ExecuteNonQuery();
            conn.Close();
        }

        static void UpdateDBInput(string ArticleID, int pageCount, int wordCount, int lettercount)
        {
            // call from db
            //String str = "Data Source=172.16.0.61;Initial Catalog=PTS4LN_TEST;Persist Security Info=True;User ID=sa;Password=p@ssw0rd";
            String str = System.Configuration.ConfigurationSettings.AppSettings["ConnString"].ToString();
            //String str = System.Configuration.ConfigurationSettings.AppSettings["ConnString"].ToString();
            SqlConnection conn = new SqlConnection(str);
            SqlCommand sqlcmd = new SqlCommand("[UpdatePageCountInput]", conn);
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.Parameters.AddWithValue("@ArticleID", ArticleID.ToString());
            sqlcmd.Parameters.AddWithValue("@PageCount", pageCount.ToString());
            sqlcmd.Parameters.AddWithValue("@WordCount", wordCount.ToString());
            sqlcmd.Parameters.AddWithValue("@LetterCount", lettercount.ToString());
            conn.Open();
            sqlcmd.ExecuteNonQuery();
            conn.Close();
        }

        private void SaveValue(string ArticleID, int pageCount, int wordCount, int lettercount)
        {
            //String str = System.Configuration.ConfigurationSettings.AppSettings["ConnString"].ToString();

        }
        static void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }





        public string[] GetPagesDoc(object Path)
        {
            List<string> Pages = new List<string>();

            // Get application object
            Microsoft.Office.Interop.Word.Application WordApplication = new Microsoft.Office.Interop.Word.Application();

            // Get document object
            object Miss = System.Reflection.Missing.Value;
            object ReadOnly = false;
            object Visible = false;
            Document Doc = WordApplication.Documents.Open(ref Path, ref Miss, ref ReadOnly, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Miss, ref Visible, ref Miss, ref Miss, ref Miss, ref Miss);

            // Get pages count
            Microsoft.Office.Interop.Word.WdStatistic PagesCountStat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;
            int PagesCount = Doc.ComputeStatistics(PagesCountStat, ref Miss);

            //Get pages
            object What = Microsoft.Office.Interop.Word.WdGoToItem.wdGoToPage;
            object Which = Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToAbsolute;
            object Start;
            object End;
            object CurrentPageNumber;
            object NextPageNumber;

            for (int Index = 1; Index < PagesCount + 1; Index++)
            {
                CurrentPageNumber = (Convert.ToInt32(Index.ToString()));
                NextPageNumber = (Convert.ToInt32((Index + 1).ToString()));

                //Get start position of current page
                Start = WordApplication.Selection.GoTo(ref What, ref Which, ref CurrentPageNumber, ref Miss).Start;

                // Get end position of current page                                
                End = WordApplication.Selection.GoTo(ref What, ref Which, ref NextPageNumber, ref Miss).End;

                // Get text
                if (Convert.ToInt32(Start.ToString()) != Convert.ToInt32(End.ToString()))
                    Pages.Add(Doc.Range(ref Start, ref End).Text);
                else
                    Pages.Add(Doc.Range(ref Start).Text);
            }
            return Pages.ToArray<string>();
        }

        //public int GetPageCount( string path)
        //{
        //    Microsoft.Office.Interop.Word.ApplicationClass WordApp = new Microsoft.Office.Interop.Word.ApplicationClass();

        //    // give any file name of your choice. 
        //    object fileName = path;
        //    object readOnly = false;
        //    object isVisible = true;

        //    //  the way to handle parameters you don't care about in .NET 
        //    object missing = System.Reflection.Missing.Value;

        //    //   Make word visible, so you can see what's happening 
        //    //WordApp.Visible = true; 
        //    //   Open the document that was chosen by the dialog 
        //    Microsoft.Office.Interop.Word.Document aDoc = WordApp.Documents.Open(ref fileName,
        //                            ref missing, ref readOnly, ref missing,
        //                            ref missing, ref missing, ref missing,
        //                            ref missing, ref missing, ref missing,
        //                             ref missing, ref isVisible);

        //    Microsoft.Office.Interop.Word.WdStatistic stat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;
        //    int num = aDoc.ComputeStatistics(stat, ref missing);
        //    System.Console.WriteLine("The number of pages in doc is {0}",
        //                              num);
        //    System.Console.ReadLine(); 
        //}
    }
}
