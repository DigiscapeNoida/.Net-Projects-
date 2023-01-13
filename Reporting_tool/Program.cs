using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;

namespace report
{
    internal class Program
    {
        static Log log = Log.GetInstance();
        static void Main(string[] args)
        {
            while (true)
            {
                Report report = Report.GetInstance();
                report.CreareReport();
                CsvDownload csvdn = CsvDownload.GetInstance();
                csvdn.GetCsv();
                string input_path = ConfigurationSettings.AppSettings["input"];
                //DirectoryInfo info = new DirectoryInfo(input_path);
                //FileInfo[] filess = info.GetFiles("*.csv", SearchOption.TopDirectoryOnly).OrderBy(p => p.CreationTime).ToArray();
                //string[] files = Directory.GetFiles(input_path, "*.csv", SearchOption.TopDirectoryOnly);
                Db db = Db.GetInstance();
                DataTable dataTable = db.GetData("select file_name from download_files order by creation_date asc");
                //string[] files=filess.ToString();
                //for (int i = 0; i < filess.Length; i++)
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataTable dt = new DataTable();
                    //string file = input_path + "\\" + filess[i].ToString();
                    string file = dataTable.Rows[i][0].ToString();
                    string[] lines = File.ReadAllLines(file);
                    for (int k = 0; k < lines.Length; k++)
                    {
                        if (k == 0)
                        {
                            string[] col = lines[k].Split(';');
                            for (int j = 0; j < col.Length; j++)
                            {
                                if (col[j].Trim().Length > 0)
                                {
                                    dt.Columns.Add(col[j].Trim().ToLower());
                                }
                                else
                                {
                                    dt.Columns.Add("blank" + j);
                                }
                            }
                        }
                        else
                        {
                            string[] col = lines[k].Split(';');
                            if (col.Length > 0)
                            {
                                DataRow dr = dt.NewRow();
                                for (int p = 0; p < col.Length; p++)
                                {
                                    if (col[p].Trim().Length > 0)
                                    {
                                        dr[p] = col[p].Trim().ToLower();
                                    }
                                    else
                                    {
                                        dr[p] = "";
                                    }
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (file.Trim().ToLower().Contains("encyclo_received"))
                        {
                            Encyclo encyclo = Encyclo.GetInstance();
                            encyclo.ProcessEncyclo(dt, "insert", file);
                        }
                        else if (file.Trim().ToLower().Contains("encyclo_cancelled"))
                        {
                            Encyclo encyclo = Encyclo.GetInstance();
                            encyclo.ProcessEncyclo(dt, "cancel", file);
                        }
                        else if (file.Trim().ToLower().Contains("encyclo_refused"))
                        {
                            Encyclo encyclo = Encyclo.GetInstance();
                            encyclo.ProcessEncyclo(dt, "refuse", file);
                        }
                        else if (file.Trim().ToLower().Contains("encyclo_delivered"))
                        {
                            Encyclo encyclo = Encyclo.GetInstance();
                            encyclo.ProcessEncyclo(dt, "delivered", file);
                        }
                        else if (file.Trim().ToLower().Contains("jour_received"))
                        {
                            Fiches fiches = Fiches.GetInstance();
                            fiches.ProcessFiches(dt, "insert", file);
                        }
                        else if (file.Trim().ToLower().Contains("jour_delivered"))
                        {
                            Fiches fiches = Fiches.GetInstance();
                            fiches.ProcessFiches(dt, "delivered", file);
                        }
                        else if (file.Trim().ToLower().Contains("jour_refused"))
                        {
                            Fiches fiches = Fiches.GetInstance();
                            fiches.ProcessFiches(dt, "refuse", file);
                        }
                        else if (file.Trim().ToLower().Contains("jour_cancelled"))
                        {
                            Fiches fiches = Fiches.GetInstance();
                            fiches.ProcessFiches(dt, "cancel", file);
                        }
                        else if (file.Trim().ToLower().Contains("fp_received"))
                        {
                            Fp fp = Fp.GetInstance();
                            fp.ProcessFp(dt, "insert", file);
                        }
                        else if (file.Trim().ToLower().Contains("fp_cancelled"))
                        {
                            Fp fp = Fp.GetInstance();
                            fp.ProcessFp(dt, "cancel", file);
                        }
                        else if (file.Trim().ToLower().Contains("fp_refused"))
                        {
                            Fp fp = Fp.GetInstance();
                            fp.ProcessFp(dt, "refuse", file);
                        }
                        else if (file.Trim().ToLower().Contains("fp_delivered"))
                        {
                            Fp fp = Fp.GetInstance();
                            fp.ProcessFp(dt, "delivered", file);
                        }
                        else if (file.Trim().ToLower().Contains("syntheses_received"))
                        {
                            Synth synth = Synth.GetInstance();
                            synth.ProcessSynth(dt, "insert", file);
                        }
                        else if (file.Trim().ToLower().Contains("syntheses_cancelled"))
                        {
                            Synth synth = Synth.GetInstance();
                            synth.ProcessSynth(dt, "cancel", file);
                        }
                        else if (file.Trim().ToLower().Contains("syntheses_refused"))
                        {
                            Synth synth = Synth.GetInstance();
                            synth.ProcessSynth(dt, "refuse", file);
                        }
                        else if (file.Trim().ToLower().Contains("syntheses_delivered"))
                        {
                            Synth synth = Synth.GetInstance();
                            synth.ProcessSynth(dt, "delivered", file);
                        }
                    }
                }
                ClearFiles();
                //Console.WriteLine("Waiting for 1 min.");
                //Thread.Sleep(60000);
            }
        }
        static void ClearFiles()
        {
            string success = ConfigurationSettings.AppSettings["success"];
            string fail = ConfigurationSettings.AppSettings["fail"];
            string repo = ConfigurationSettings.AppSettings["FilePath"];
            if (Directory.Exists(success))
            {
                string[] fls = Directory.GetFiles(success);
                if (fls.Count() > 0)
                {
                    foreach (string fl in fls)
                    {
                        if (File.GetLastWriteTime(fl) < DateTime.Now.AddDays(-7))
                        {
                            try
                            {
                                File.Delete(fl);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            if (Directory.Exists(fail))
            {
                string[] fls1 = Directory.GetFiles(fail);
                if (fls1.Count() > 0)
                {
                    foreach (string fl1 in fls1)
                    {
                        if (File.GetLastWriteTime(fl1) < DateTime.Now.AddDays(-7))
                        {
                            try
                            {
                                File.Delete(fl1);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            //if (Directory.Exists(repo))
            //{
            //    string[] fls1 = Directory.GetFiles(repo);
            //    if (fls1.Count() > 0)
            //    {
            //        foreach (string fl1 in fls1)
            //        {
            //            if (File.GetLastWriteTime(fl1) < DateTime.Now.AddDays(-60))
            //            {
            //                try
            //                {
            //                    File.Delete(fl1);
            //                }
            //                catch
            //                {
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
}
