using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Data;
using System.Windows.Forms;

namespace Doc2OpenXml
{
    class Program
    {
        static string final_xml_path;
        static string final_xml_name;
        static string file_name;
        static bool delxml;
        static void Main(string[] args)
        {
            delxml = false;
            string extractPath = System.Configuration.ConfigurationSettings.AppSettings["docxfile"].ToString();
            string output = System.Configuration.ConfigurationSettings.AppSettings["output"].ToString();
            if (File.Exists(output + "errorlog.txt"))
            {
                File.Delete(output + "errorlog.txt");
            }
            else
            {
                // File.Create(output+"errorlog.txt");
            }
            WriteLog writeLog = new WriteLog(System.Configuration.ConfigurationSettings.AppSettings["logfile"].ToString());
            try
            {



                writeLog.GetDateLogPath();
                DocxToOpenXML(args[0]);
                Console.WriteLine("Please wait for 10 SEC");
                System.Threading.Thread.Sleep(1000);
                string finalfileName = Path.GetFileNameWithoutExtension(args[0]);
                file_name = finalfileName;
                string metapath = output + finalfileName.Replace(" ", "") + "\\";
                MetadataXmlMove(extractPath, metapath); //anita 13_oct_2020

                if (Directory.Exists(output + "\\Process3"))
                    Directory.Delete(output + "\\Process3", true);
                if (Directory.Exists(output + "\\Process4"))
                    Directory.Delete(output + "\\Process4", true);
                if (Directory.Exists(output + "\\Process5"))
                    Directory.Delete(output + "\\Process5", true);
                if (Directory.Exists(output + "\\Process6"))
                    Directory.Delete(output + "\\Process6", true);
                if (Directory.Exists(output + "\\Process7"))
                    Directory.Delete(output + "\\Process7", true);

                //if (!File.Exists(metapath + "article.dtd"))
                //    File.Copy(output + "article.dtd", metapath + "article.dtd");
                //if (!File.Exists(metapath + "revue.dtd"))
                //    File.Copy(output + "revue.dtd", metapath + "revue.dtd");

                string articledtd = System.Configuration.ConfigurationSettings.AppSettings["ArticleDTD"].ToString();
                string reveuedtd = System.Configuration.ConfigurationSettings.AppSettings["ReveueDTD"].ToString();
                //File.Copy();
                string[] getfile1 = Directory.GetFiles(metapath);
                foreach (string file1 in getfile1)
                {
                    string fname = Path.GetFileName(file1);
                    string fnameonly = Path.GetFileNameWithoutExtension(file1);
                    if (fname.EndsWith(".xml"))
                    {

                        StreamReader sr1 = new StreamReader(file1);
                        string FCon1 = sr1.ReadToEnd();
                        sr1.Close();
                        FCon1 = FCon1.Replace("article.dtd", articledtd);
                        FCon1 = FCon1.Replace("revue.dtd", reveuedtd);

                        StreamWriter sw1 = new StreamWriter(file1);
                        sw1.Write(FCon1);
                        sw1.Close();

                        Parser(metapath + fname);
                    }
                }
                AddComments(args[0], output + "errorlog.txt");
                if (delxml == true)//client requirement but not delivered 
                {
                    //Directory.Delete(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", "") + "\\", true);
                }
                else
                {
                    //sommdossier(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", ""));
                    //movefile();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("" + ex.Message);
                writeLog.AppendLog("exception" + ex.Message);

            }
        }
        public static void Parser(string final_xml1)
        {
            WriteLog writeLog = new WriteLog(System.Configuration.ConfigurationSettings.AppSettings["logfile"].ToString());
            // Set the validation settings.
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.XmlResolver = new XmlUrlResolver();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
            settings.IgnoreWhitespace = true;
            File.AppendAllText(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + "errorlog.txt", "file_name|" + final_xml1 + Environment.NewLine);
            // Create the XmlReader object.
            if (!string.IsNullOrWhiteSpace(final_xml1))
            {

                XmlReader reader = XmlReader.Create(final_xml1, settings);
                // Parse the file.
                try
                {

                    while (reader.Read()) ;
                    {
                        final_xml_name = reader.Name;
                    }
                    if (reader.ReadState != ReadState.Closed)
                    {
                        reader.Close();
                    }
                    writeLog.AppendLog(final_xml1 + "File Parsed Successfully");
                }
                catch (Exception ex)
                {
                    writeLog.AppendLog(final_xml1 + " " + ex.Message.ToString());
                    if (reader.ReadState != ReadState.Closed)
                    {
                        reader.Close();
                    }
                    delxml = true;
                }
            }
            else
            {
                Console.WriteLine("final xml cannot found");
                writeLog.AppendLog("final xml cannot found");
            }
        }
        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            string outputpath = System.Configuration.ConfigurationSettings.AppSettings["output"].ToString();
            WriteLog log = new WriteLog(System.Configuration.ConfigurationSettings.AppSettings["logfile"].ToString());
            string asd = final_xml_name;

            Console.WriteLine("Validation Error: {0}", e.Message);
            log.AppendLog("Validation Error:" + e.Message + "\t Line Number: " + (e.Exception).LineNumber + "\t Line Position: " + (e.Exception).LinePosition);
            File.AppendAllText(outputpath + "errorlog.txt", "Validation Error Location:\t" + (e.Exception).LineNumber + ":" + (e.Exception).LinePosition + "\t" + e.Message + Environment.NewLine);
        }
        public static void DocxToOpenXML(string path)
        {
            string extractPath = System.Configuration.ConfigurationSettings.AppSettings["docxfile"].ToString();
            string xlsfile1 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile1"].ToString();
            string xlsfile2 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile2"].ToString();
            string xlsfile3 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile3"].ToString();//anita5_oct_2020
            string xlsfile4 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile4"].ToString();//anita6_oct_2020
            string xlsfile5 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile5"].ToString();
            string xlsfile6 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile6"].ToString();
            string xlsfile7 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile7"].ToString();//anita 14_may_2021
            string xlsfileMergeXML = System.Configuration.ConfigurationSettings.AppSettings["xlsfileMergeXML"].ToString();//anita6_oct_2020
            string xlsfile1Metadata = System.Configuration.ConfigurationSettings.AppSettings["xlsfile1Metadata"].ToString();//anita13_oct_2020
            string xlsfile2Metadata = System.Configuration.ConfigurationSettings.AppSettings["xlsfile2Metadata"].ToString();//anita13_oct_2020
            string output = System.Configuration.ConfigurationSettings.AppSettings["output"].ToString();
            WriteLog log = new WriteLog((System.Configuration.ConfigurationSettings.AppSettings["logfile"].ToString()));
            try
            {
                if (!File.Exists(path.Replace(".docx", ".zip")))
                    File.Copy(path, Path.ChangeExtension(path, ".zip"));
                System.Threading.Thread.Sleep(1000);
                string zipPath = path.Replace(".docx", ".zip");
                string fileName = Path.GetFileNameWithoutExtension(zipPath);
                if (Directory.Exists(extractPath))
                {
                    var dir = new DirectoryInfo(extractPath);
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);
                    //Directory.Delete(extractPath); 
                    System.Threading.Thread.Sleep(500);
                }
                System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
                System.Threading.Thread.Sleep(1000);
                File.Delete(zipPath);
                Console.WriteLine("File exctract successfully");
                log.AppendLog("Step 1: File exctract successfully()");
                process2(extractPath + @"word\document.xml", xlsfile1);
                Console.WriteLine("process 2 completed");
                log.AppendLog("Step 2 :process 2 completed  ");
                string[] getfile = Directory.GetFiles(extractPath);
                string process3outputpath = @output + "process3\\";
                string process4outputpath = @output + "process4\\";
                string process5outputpath = @output + "process5\\";
                string process6outputpath = @output + "process6\\";
                string process7outputpath = @output + "process7\\";
                if (Directory.Exists(process3outputpath))
                {
                    var dir = new DirectoryInfo(process3outputpath);
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);

                }
                if (Directory.Exists(process4outputpath))
                {
                    var dir = new DirectoryInfo(process4outputpath);
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);

                }
                if (Directory.Exists(process5outputpath))
                {
                    var dir = new DirectoryInfo(process5outputpath);
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);

                }
                if (Directory.Exists(process6outputpath))
                {
                    var dir = new DirectoryInfo(process6outputpath);
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);

                }
                if (Directory.Exists(process7outputpath))
                {
                    var dir = new DirectoryInfo(process7outputpath);
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);

                }

                foreach (string file in getfile)
                {
                    string fname = Path.GetFileName(file);
                    string fnameonly = Path.GetFileNameWithoutExtension(file);
                    if (fname.EndsWith(".xml") && fname != "[Content_Types].xml" && !fname.ToLower().Contains("metadata"))
                    {
                        //if (fname.ToLower().Contains("metadata"))
                        final_xml_path = process3outputpath + fnameonly + "_final.xml";
                        process3(file, xlsfile2, final_xml_path);

                        Console.WriteLine("Step 3: process 3 completed file created :" + final_xml_path);
                        log.AppendLog("Step 3: process 3 completed file created :" + final_xml_path);
                    }
                    else if (fname == "[Content_Types].xml" && getfile.Count() < 2)
                    {
                        Console.WriteLine("Step 3: no xml found for process 3");
                        log.AppendLog("Step 3: no xml found for process 3");
                    }
                }

                //anita5_oct_2020
                if (Directory.Exists(process3outputpath))
                {
                    string[] getfile1 = Directory.GetFiles(process3outputpath);
                    foreach (string file1 in getfile1)
                    {
                        string fname = Path.GetFileName(file1);
                        string fnameonly = Path.GetFileNameWithoutExtension(file1);
                        if (fname.EndsWith(".xml"))
                        {
                            final_xml_path = process4outputpath + fnameonly + ".xml";
                            process4(file1, xlsfile3, final_xml_path);

                            Console.WriteLine("Step 4: process 4 completed file created :" + final_xml_path);
                            log.AppendLog("Step 4: process 4 completed file created :" + final_xml_path);
                        }
                        else
                        {
                            Console.WriteLine("Step 4: no xml found for process 4 at");
                            log.AppendLog("Step 4: no xml found for process ");
                        }
                    }
                }


                if (Directory.Exists(process4outputpath))
                {
                    string[] getfile2 = Directory.GetFiles(process4outputpath);
                    foreach (string file2 in getfile2)
                    {
                        string fname = Path.GetFileName(file2);
                        string fnameonly = Path.GetFileNameWithoutExtension(file2);
                        if (fname.EndsWith(".xml"))
                        {
                            final_xml_path = process5outputpath + fnameonly + ".xml";
                            if (File.Exists(final_xml_path))
                            {

                                File.Delete(final_xml_path);
                            }
                            process5(file2, xlsfile4, final_xml_path);

                            Console.WriteLine("Step 5: process 5 completed file created :" + final_xml_path);
                            log.AppendLog("Step 5: process 5 completed file created :" + final_xml_path);
                        }
                        else
                        {
                            Console.WriteLine("Step 5: no xml found for process 5");
                            log.AppendLog("Step 5: no xml found for process ");
                        }
                    }
                }
                if (Directory.Exists(process5outputpath))
                {
                    string[] getfile2 = Directory.GetFiles(process5outputpath);
                    foreach (string file2 in getfile2)
                    {
                        string fname = Path.GetFileName(file2);
                        string fnameonly = Path.GetFileNameWithoutExtension(file2);
                        if (fname.EndsWith(".xml"))
                        {
                            final_xml_path = process6outputpath + fnameonly + ".xml";
                            if (File.Exists(final_xml_path))
                            {

                                File.Delete(final_xml_path);
                            }
                            process6(file2, xlsfile5, final_xml_path);

                            Console.WriteLine("Step 6: process 6 completed file created :" + final_xml_path);
                            log.AppendLog("Step 6: process 6 completed file created :" + final_xml_path);
                        }
                        else
                        {
                            Console.WriteLine("Step 6: no xml found for process 5");
                            log.AppendLog("Step 6: no xml found for process ");
                        }
                    }
                }
                if (Directory.Exists(process6outputpath))
                {
                    string[] getfile2 = Directory.GetFiles(process6outputpath);
                    foreach (string file2 in getfile2)
                    {
                        string fname = Path.GetFileName(file2);
                        string fnameonly = Path.GetFileNameWithoutExtension(file2);
                        if (fname.EndsWith(".xml"))
                        {
                            final_xml_path = process7outputpath + fnameonly + ".xml";
                            if (File.Exists(final_xml_path))
                            {

                                File.Delete(final_xml_path);
                            }
                            process7(file2, xlsfile6, final_xml_path);

                            Console.WriteLine("Step 7: process 7 completed file created :" + final_xml_path);
                            log.AppendLog("Step 7: process 7 completed file created :" + final_xml_path);
                        }
                        else
                        {
                            Console.WriteLine("Step 7: no xml found for process 6");
                            log.AppendLog("Step 7: no xml found for process ");
                        }
                    }
                }
                string[] oldfiles = Directory.GetFiles(output);
                foreach (string file1 in oldfiles)
                {
                    string fname = Path.GetFileName(file1);
                    string fnameonly = Path.GetFileNameWithoutExtension(file1);
                    if (fname.EndsWith(".xml"))
                    {
                        File.Delete(fname);
                    }
                }
                if (Directory.Exists(process7outputpath))
                {
                    string[] getfile3 = Directory.GetFiles(process7outputpath);
                    int count = getfile3.Count();

                    File.AppendAllText(output + "errorlog.txt", "Total XML count = " + count + Environment.NewLine);
                    string fl = "";
                    foreach (string file3 in getfile3)
                    {
                        string fname = Path.GetFileName(file3);
                        string fnameonly = Path.GetFileNameWithoutExtension(file3);
                        if (fname.EndsWith(".xml"))
                        {
                            final_xml_path = output + fnameonly + ".xml";
                            string process8outputxml = output + fileName.Replace(" ", "");
                            if (File.Exists(final_xml_path))
                            {

                                File.Delete(final_xml_path);
                            }
                            if (!File.Exists(process8outputxml))
                            {
                                Directory.CreateDirectory(process8outputxml);
                            }
                            process8outputxml = process8outputxml + "\\" + fnameonly + ".xml";
                            process8(file3, xlsfile7, process8outputxml);
                            fl = process8outputxml;
                            Console.WriteLine("Step 8: process 8 completed file created :" + process8outputxml);
                            log.AppendLog("Step 8: process 8 completed file created :" + process8outputxml);
                        }
                        else
                        {
                            Console.WriteLine("Step 8: no xml found for process 7");
                            log.AppendLog("Step 8: no xml found for process 7");
                        }
                    }
                    //string fl1 = fl.Replace("final", "Merge");
                    //process8(fl, xlsfileMergeXML, fl1);
                }

                // read the file here 
                string XMLPATH = System.Configuration.ConfigurationSettings.AppSettings["XMLPATH"].ToString();
                //string TagPATH = System.Configuration.ConfigurationSettings.AppSettings["TagPATH"].ToString();
                string xmlString = File.ReadAllText(XMLPATH);
                string[] newarr = xmlString.Split(new string[] { "Métadonnées" }, StringSplitOptions.None);
                for (int i = 1; i < newarr.Count(); i++)
                {


                    if (!newarr[i].ToString().Contains("NumArticle"))
                    {
                        //File.AppendAllText(output + "errorlog.txt", "NumArticle Tag Missing" + Environment.NewLine);//client doesnot wants this error
                        // insert log here
                    }
                }


                // process1ForMetadata(, xlsfile1Metadata);

                //anita5_oct_2020



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                Console.Read();
                log.AppendLog("Step: ERROR MESSAGE: " + ex.ToString());
            }

        }

        public static void process2(string document_xml, string word2xml_xsl)
        {
            Process proc = null;
            string sPath = null;
            proc = new Process();
            string Transformexe = System.Configuration.ConfigurationSettings.AppSettings["Transformexe"].ToString();
            proc.StartInfo.WorkingDirectory = Transformexe;
            proc.StartInfo.FileName = Transformexe + "Transform.exe";
            proc.StartInfo.Arguments = @"-s:" + document_xml + " -xsl:" + word2xml_xsl;// +" -o:" + temp_xml;    

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }

        //anita5_oct_2020
        public static void process4(string xml, string word2xml_03_xsl, string XML)
        {
            //if (File.Exists(xml))
            //{
            //    File.Delete(xml);
            //}
            Process proc = null;
            string sPath = null;
            proc = new Process();
            string Transformexe = System.Configuration.ConfigurationSettings.AppSettings["Transformexe"].ToString();
            proc.StartInfo.WorkingDirectory = Transformexe;
            proc.StartInfo.FileName = Transformexe + "Transform.exe";
            proc.StartInfo.Arguments = @"-s:" + xml + " -xsl:" + word2xml_03_xsl + " -o:" + XML;

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
        public static void process5(string XML, string word2xml_04_xsl, string final)
        {
            //if (File.Exists(xml))
            //{
            //    File.Delete(xml);
            //}
            Process proc = null;
            string sPath = null;
            proc = new Process();
            string Transformexe = System.Configuration.ConfigurationSettings.AppSettings["Transformexe"].ToString();
            proc.StartInfo.WorkingDirectory = Transformexe;
            proc.StartInfo.FileName = Transformexe + "Transform.exe";
            proc.StartInfo.Arguments = @"-s:" + XML + " -xsl:" + word2xml_04_xsl + " -o:" + final;

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
        public static void process6(string XMl, string word2xml_05_xsl, string final)
        {
            //if (File.Exists(xml))
            //{
            //    File.Delete(xml);
            //}
            Process proc = null;
            string sPath = null;
            proc = new Process();
            string Transformexe = System.Configuration.ConfigurationSettings.AppSettings["Transformexe"].ToString();
            proc.StartInfo.WorkingDirectory = Transformexe;
            proc.StartInfo.FileName = Transformexe + "Transform.exe";
            proc.StartInfo.Arguments = @"-s:" + XMl + " -xsl:" + word2xml_05_xsl + " -o:" + final;

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
        public static void process7(string XML, string word2xml_06_xsl, string final)
        {
            //if (File.Exists(xml))
            //{
            //    File.Delete(xml);
            //}
            Process proc = null;
            string sPath = null;
            proc = new Process();
            string Transformexe = System.Configuration.ConfigurationSettings.AppSettings["Transformexe"].ToString();
            proc.StartInfo.WorkingDirectory = Transformexe;
            proc.StartInfo.FileName = Transformexe + "Transform.exe";
            proc.StartInfo.Arguments = @"-s:" + XML + " -xsl:" + word2xml_06_xsl + " -o:" + final;

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
        public static void process8(string XML, string word2xml_07_xsl, string final)
        {
            //if (File.Exists(xml))
            //{
            //    File.Delete(xml);
            //}
            Process proc = null;
            string sPath = null;
            proc = new Process();
            string Transformexe = System.Configuration.ConfigurationSettings.AppSettings["Transformexe"].ToString();
            proc.StartInfo.WorkingDirectory = Transformexe;
            proc.StartInfo.FileName = Transformexe + "Transform.exe";
            proc.StartInfo.Arguments = @"-s:" + XML + " -xsl:" + word2xml_07_xsl + " -o:" + final;

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }

        //anita5_oct_2020
        public static void process3(string temp_xml, string word2xml_02_xsl, string xml)
        {
            if (File.Exists(xml))
            {
                File.Delete(xml);
            }
            Process proc = null;
            string sPath = null;
            proc = new Process();
            string Transformexe = System.Configuration.ConfigurationSettings.AppSettings["Transformexe"].ToString();
            proc.StartInfo.WorkingDirectory = Transformexe;
            proc.StartInfo.FileName = Transformexe + "Transform.exe";
            proc.StartInfo.Arguments = @"-s:" + temp_xml + " -xsl:" + word2xml_02_xsl + " -o:" + xml;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
        public static void MetadataXmlMove(string From, string To)
        {
            string extractPath = System.Configuration.ConfigurationSettings.AppSettings["docxfile"].ToString();
            string xlsfile1Metadata = System.Configuration.ConfigurationSettings.AppSettings["xlsfile1Metadata"].ToString();
            string xlsfile2Metadata = System.Configuration.ConfigurationSettings.AppSettings["xlsfile2Metadata"].ToString();
            WriteLog writeLog = new WriteLog(System.Configuration.ConfigurationSettings.AppSettings["logfile"].ToString());
            string[] getfile3 = Directory.GetFiles(From);
            foreach (string file3 in getfile3)
            {
                string fname = Path.GetFileName(file3);
                string fnameonly = Path.GetFileNameWithoutExtension(file3);
                if (fname.EndsWith(".xml"))
                {

                    if (fname.ToLower().Contains("metadata"))
                    {
                        process1ForMetadata(file3, xlsfile1Metadata);
                        process2ForMetadata(file3, xlsfile2Metadata);
                        File.Copy(file3, To + fname, true);
                    }

                }
                else
                {
                    Console.WriteLine("metaxml not found ");
                    writeLog.AppendLog(" metaxml not found");
                }
            }
        }
        public static void process1ForMetadata(string Metadata_xml, string word2xml_meta_01)
        {
            Process proc = null;
            string sPath = null;
            proc = new Process();
            string Transformexe = System.Configuration.ConfigurationSettings.AppSettings["Transformexe"].ToString();
            proc.StartInfo.WorkingDirectory = Transformexe;
            proc.StartInfo.FileName = Transformexe + "Transform.exe";
            proc.StartInfo.Arguments = @"-s:" + Metadata_xml + " -xsl:" + word2xml_meta_01 + " -o:" + Metadata_xml;

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
        public static void process2ForMetadata(string Metadata_xml, string word2xml_meta_02)
        {
            Process proc = null;
            string sPath = null;
            proc = new Process();
            string Transformexe = System.Configuration.ConfigurationSettings.AppSettings["Transformexe"].ToString();
            proc.StartInfo.WorkingDirectory = Transformexe;
            proc.StartInfo.FileName = Transformexe + "Transform.exe";
            proc.StartInfo.Arguments = @"-s:" + Metadata_xml + " -xsl:" + word2xml_meta_02 + " -o:" + Metadata_xml;

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
        public static void AddComments(string docx, string errlog)
        {
            try
            {
                //string filecopy = docx.Replace(".docx", "_copy.docx");
                //File.Copy(docx, filecopy, true);
                DataTable dt = ConvertToDataTable(errlog);
                WordprocessingDocument document = WordprocessingDocument.Open(docx, true);
                Body body = document.MainDocumentPart.Document.Body;
                string xl = "";
                foreach (var para1 in body)
                {
                    if (para1.InnerXml.Contains("Métadonnées"))
                    {
                        if (xl == "")
                        {
                            xl = body.InnerXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").IndexOf(para1.OuterXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "")).ToString();
                        }
                        else
                        {
                            xl = xl + "," + body.InnerXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").IndexOf(para1.OuterXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "")).ToString();
                        }
                    }
                }
                string[] b = xl.Split(',');
                string m_c = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        if (dt.Rows[i]["f_name"].ToString() == dt.Rows[i - 1]["f_name"].ToString() && dt.Rows[i]["f_name"].ToString().ToLower().Contains("metadata") == false)
                        {
                            dt.Rows[i]["meta"] = dt.Rows[i - 1]["meta"].ToString();
                        }
                        else
                        {
                            if (dt.Rows[i]["f_name"].ToString().ToLower().Contains("metadata"))
                            {
                                if (m_c.Contains(dt.Rows[i]["txt"].ToString() + dt.Rows[i]["err"].ToString()))
                                {
                                    dt.Rows[i]["meta"] = "0";
                                }
                                else
                                {
                                    dt.Rows[i]["meta"] = "1";
                                    if (m_c == "")
                                    {
                                        m_c = dt.Rows[i]["txt"].ToString() + dt.Rows[i]["err"].ToString();
                                    }
                                    else
                                    {
                                        m_c = m_c + "|" + dt.Rows[i]["txt"].ToString() + dt.Rows[i]["err"].ToString();
                                    }
                                }
                            }
                            else
                            {
                                b = b.Skip(1).ToArray();
                                dt.Rows[i]["meta"] = b[0].ToString();
                            }
                        }
                    }
                    if (i == 0)
                    {
                        dt.Rows[i]["meta"] = b[0].ToString();
                    }
                }
                Comments comments = null;
                string id = "0";
                if (document.MainDocumentPart.GetPartsOfType<WordprocessingCommentsPart>().Count() > 0)
                {
                    comments = document.MainDocumentPart.WordprocessingCommentsPart.Comments;
                    if (comments.HasChildren)
                    {
                        id = comments.Descendants<Comment>().Select(e => e.Id.Value).Max();
                    }
                }
                else
                {
                    WordprocessingCommentsPart commentPart = document.MainDocumentPart.AddNewPart<WordprocessingCommentsPart>();
                    commentPart.Comments = new Comments();
                    comments = commentPart.Comments;
                }
                WriteLog writeLog = new WriteLog(System.Configuration.ConfigurationSettings.AppSettings["logfile"].ToString());
                writeLog.AppendLog("==============================Docx file error(S)==============================");
                if (dt.Rows.Count > 0)
                {
                    int loop = 0;
                    int loop1 = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["txt"].ToString().Length > 0)
                        {

                            foreach (var para1 in body)
                            {
                                if (para1.InnerText.Contains(dt.Rows[i]["txt"].ToString()) && dt.Rows[i]["err"].ToString().Contains("The element 'chfer' has invalid child element 'div1'") == false)
                                {
                                    if (dt.Rows[i]["f_name"].ToString().ToLower().Contains("metadata"))
                                    {
                                        //if (para1.InnerText == dt.Rows[i]["txt"].ToString() && dt.Rows[i]["meta"].ToString()=="1")
                                        if (dt.Rows[i]["meta"].ToString() == "0" || dt.Rows[i]["meta"].ToString() == "1")
                                        {
                                            id = (Convert.ToInt32(id) + 1).ToString();
                                            string er1 = dt.Rows[i]["err"].ToString();
                                            if (dt.Rows[i]["err"].ToString().ToLower().Contains("has invalid child element 'nb-bl'"))
                                            {
                                                delxml = true;
                                                er1 = "nb-bl are prohibited for this type of article.";
                                            }
                                            if (dt.Rows[i]["err"].ToString().ToLower().Contains("has invalid child element 'pnchr'"))
                                            {
                                                delxml = true;
                                                er1 = "Invalid child element 'pnchr'";
                                            }
                                            Paragraph p = new Paragraph(new Run(new Text(er1)));
                                            Comment cmt =
                                                new Comment()
                                                {
                                                    Id = id,
                                                    Author = "Thomson Digital",
                                                    Initials = "TD",
                                                    Date = DateTime.Now
                                                };
                                            cmt.AppendChild(p);
                                            comments.AppendChild(cmt);
                                            comments.Save();
                                            para1.InsertAt(new CommentReference() { Id = id }, 0);
                                            para1.InsertAt(new CommentRangeEnd() { Id = id }, 0);
                                            para1.InsertAt(new CommentRangeStart() { Id = id }, 0);
                                            writeLog.AppendLog(p.InnerText + " near " + "\"" + dt.Rows[i]["txt"].ToString() + "\"" + " in metadata.");

                                        }
                                    }
                                    else
                                    {
                                        if (body.InnerXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").IndexOf(para1.OuterXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "")) >= Convert.ToInt32(dt.Rows[i]["meta"]) + (loop1 * 93))
                                        {
                                            string er1 = dt.Rows[i]["err"].ToString();
                                            id = (Convert.ToInt32(id) + 1).ToString();                                            
                                            if (dt.Rows[i]["err"].ToString().ToLower().Contains("has invalid child element 'nb-bl'"))
                                            {
                                                delxml = true;
                                                er1 = "nb-bl are prohibited for this type of article.";
                                            }
                                            if (dt.Rows[i]["err"].ToString().ToLower().Contains("has invalid child element 'pnchr'"))
                                            {
                                                delxml = true;
                                                er1 = "Invalid child element 'pnchr'";
                                            }
                                            Paragraph p = new Paragraph(new Run(new Text(er1)));
                                            Comment cmt =
                                                new Comment()
                                                {
                                                    Id = id,
                                                    Author = "Thomson Digital",
                                                    Initials = "TD",
                                                    Date = DateTime.Now
                                                };
                                            cmt.AppendChild(p);
                                            comments.AppendChild(cmt);
                                            comments.Save();
                                            para1.InsertAt(new CommentReference() { Id = id }, 0);
                                            para1.InsertAt(new CommentRangeEnd() { Id = id }, 0);
                                            para1.InsertAt(new CommentRangeStart() { Id = id }, 0);
                                            writeLog.AppendLog(p.InnerText + " near " + "\"" + dt.Rows[i]["txt"].ToString() + "\"" + ".");
                                            //delxml = true;
                                            loop = loop + 1;
                                            if (i < dt.Rows.Count - 1)
                                            {
                                                if (dt.Rows[i]["f_name"].ToString() != dt.Rows[i + 1]["f_name"].ToString())
                                                {
                                                    loop1 = loop;
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                string atitle = "";
                foreach (var para1 in body)
                {
                    if (para1.LocalName == "p")
                    {
                        try
                        {
                            if (para1.Elements<ParagraphProperties>().Count() == 0)
                            {
                                para1.PrependChild<ParagraphProperties>(new ParagraphProperties());
                            }
                            ParagraphProperties pPr = para1.Elements<ParagraphProperties>().First();
                            if (pPr.ParagraphStyleId != null)
                            {
                                if (pPr.ParagraphStyleId.Val.ToString() == "NumArticle")
                                {
                                    if (atitle == "")
                                    {
                                        atitle = para1.InnerText.ToString().Trim();
                                    }
                                    else
                                    {
                                        if (atitle.Contains(para1.InnerText.ToString().Trim()) == false)
                                        {
                                            atitle = atitle + "|" + para1.InnerText.ToString().Trim();
                                        }
                                        else
                                        {
                                            id = (Convert.ToInt32(id) + 1).ToString();
                                            Paragraph p = new Paragraph(new Run(new Text("Repeated article " + para1.InnerText.ToString().Trim())));
                                            delxml = true;
                                            Comment cmt =
                                                new Comment()
                                                {
                                                    Id = id,
                                                    Author = "Thomson Digital",
                                                    Initials = "TD",
                                                    Date = DateTime.Now
                                                };
                                            cmt.AppendChild(p);
                                            comments.AppendChild(cmt);
                                            comments.Save();
                                            para1.InsertAt(new CommentReference() { Id = id }, 0);
                                            para1.InsertAt(new CommentRangeEnd() { Id = id }, 0);
                                            para1.InsertAt(new CommentRangeStart() { Id = id }, 0);
                                            writeLog.AppendLog(p.InnerText);
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                if (check_resume(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", "")) == true)
                {
                    string resume = "";
                    string[] nbl = Directory.GetFiles(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", ""));
                    foreach (string fl in nbl)
                    {
                        if (fl.EndsWith("final.xml"))
                        {
                            string[] lines = File.ReadAllLines(fl);
                            string txt = "";
                            foreach (string ln in lines)
                            {
                                txt = txt + ln;
                            }
                            MatchCollection m1 = Regex.Matches(txt, @"(<resume><p><al>)(.*?)(<\/al><\/p><\/resume>)");
                            foreach (Match m in m1)
                            {
                                string ad = replace_symbol(m.Groups[2].ToString());
                                ad = Regex.Replace(ad, @"(<)(.*?)(>)", "");
                                ad = ad.Replace("&nbsp;", " ");
                                if (resume == "")
                                {
                                    resume = ad;
                                }
                                else
                                {
                                    if (resume.Contains(ad) == false)
                                    {
                                        resume = resume + "|" + ad;
                                    }
                                }
                            }
                        }
                    }
                    if (resume != "")
                    {
                        string[] nb_bl = resume.Split('|');
                        foreach (string nb in nb_bl)
                        {
                            foreach (var para1 in body)
                            {
                                if (para1.LocalName == "p" && para1.InnerXml.Contains(nb))
                                {
                                    try
                                    {
                                        id = (Convert.ToInt32(id) + 1).ToString();
                                        Paragraph p = new Paragraph(new Run(new Text("'Resume' is not allowed in this article. SO, XML files will not be available.")));
                                        delxml = true;
                                        Comment cmt =
                                            new Comment()
                                            {
                                                Id = id,
                                                Author = "Thomson Digital",
                                                Initials = "TD",
                                                Date = DateTime.Now
                                            };
                                        cmt.AppendChild(p);
                                        comments.AppendChild(cmt);
                                        comments.Save();
                                        para1.InsertAt(new CommentReference() { Id = id }, 0);
                                        para1.InsertAt(new CommentRangeEnd() { Id = id }, 0);
                                        para1.InsertAt(new CommentRangeStart() { Id = id }, 0);
                                        writeLog.AppendLog(p.InnerText);
                                        delxml = true;
                                        break;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
                if (check_nbbl(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", "")) == true)
                {
                    string nbbl = "";
                    string[] nbl = Directory.GetFiles(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", ""));
                    foreach(string fl in nbl)
                    {
                        if(fl.EndsWith("final.xml"))
                        {
                            string[] lines = File.ReadAllLines(fl);
                            string txt = "";
                            foreach(string ln in lines)
                            {
                                txt = txt + ln;
                            }
                            MatchCollection m1 = Regex.Matches(txt, @"(<nb-bl [^><]+><al>)(.*?)(<\/al><\/nb-bl>)");
                            foreach(Match m in m1)
                            {
                                string ad = replace_symbol(m.Groups[2].ToString());
                                ad = Regex.Replace(ad,@"(<)(.*?)(>)", "");
                                ad = ad.Replace("&nbsp;", " ");
                                if (nbbl == "")
                                {
                                    nbbl = ad;
                                }
                                else
                                {
                                    if(nbbl.Contains(ad)==false)
                                    {
                                    nbbl = nbbl + "|" + ad;
                                    }
                                }
                            }
                        }
                    }
                    if(nbbl!="")
                    {
                        string[] nb_bl = nbbl.Split('|');
                        foreach(string nb in nb_bl)
                        {
                            foreach (var para1 in body)
                            {
                                
                                if (para1.LocalName == "p" && para1.InnerText.Contains(nb))
                                {
                                    try
                                    {
                                        id = (Convert.ToInt32(id) + 1).ToString();
                                        Paragraph p = new Paragraph(new Run(new Text("'nb-bl' is not allowed in this article. So, XML files will not be available.")));
                                        delxml = true;
                                        Comment cmt =
                                            new Comment()
                                            {
                                                Id = id,
                                                Author = "Thomson Digital",
                                                Initials = "TD",
                                                Date = DateTime.Now
                                            };
                                        cmt.AppendChild(p);
                                        comments.AppendChild(cmt);
                                        comments.Save();
                                        para1.InsertAt(new CommentReference() { Id = id }, 0);
                                        para1.InsertAt(new CommentRangeEnd() { Id = id }, 0);
                                        para1.InsertAt(new CommentRangeStart() { Id = id }, 0);
                                        writeLog.AppendLog(p.InnerText);
                                        delxml = true;
                                        break;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }                                       
                }
                //"NumArticle Tag Missing." not required.
                //string XMLPATH = System.Configuration.ConfigurationSettings.AppSettings["XMLPATH"].ToString();
                //string xmlString = File.ReadAllText(XMLPATH);
                //string[] newarr = xmlString.Split(new string[] { "Métadonnées" }, StringSplitOptions.None);				
                //for (int i = 1; i < newarr.Count(); i++)
                //{
                //    if (!newarr[i].ToString().Contains("NumArticle"))
                //    {
                //        int w = 0;
                //        int p1 = 0;
                //        foreach (var para1 in body)
                //        {
                //            if (para1.InnerText.Contains("Métadonnées"))
                //            {
                //                w = w + 1;
                //                p1 = 0;
                //            }
                //            else
                //            {
                //                p1 = p1 + 1;
                //            }
                //            if (i==w && p1 == 4)
                //            {
                //                id = (Convert.ToInt32(id) + 1).ToString();
                //                Paragraph p = new Paragraph(new Run(new Text("NumArticle Tag Missing.")));
                //                Comment cmt =
                //                    new Comment()
                //                    {
                //                        Id = id,
                //                        Author = "Thomson Digital",
                //                        Initials = "TD",
                //                        Date = DateTime.Now
                //                    };
                //                cmt.AppendChild(p);
                //                comments.AppendChild(cmt);
                //                comments.Save();
                //                para1.InsertAt(new CommentReference() { Id = id }, 0);
                //                para1.InsertAt(new CommentRangeEnd() { Id = id }, 0);
                //                para1.InsertAt(new CommentRangeStart() { Id = id }, 0);
                //                writeLog.AppendLog(p.InnerText);
                //                delxml = true;
                //            }
                //        }
                //    }							
                //}
                writeLog.AppendLog("==============================Docx file error(E)==============================");
                document.Close();
            }
            catch(Exception ex)
            {

            }
        }
        public static string sommdossier_part(string a, string outpath)
        {
            string ret = "";
            try
            {
                Match m = Regex.Match(a, @"(\[\[artid\]\])(.*?)(\[\[\/artid\]\])");
                if (File.Exists(outpath + "\\" + m.Groups[2].ToString() + "_final.xml"))
                {
                    string[] ftxt = File.ReadAllLines(outpath + "\\" + m.Groups[2].ToString() + "_final.xml");
                    string ftxt1 = "";
                    foreach(string l in ftxt)
                    {
                        ftxt1 = ftxt1 + l;
                    }
                    ret = "<ligne artid=\"" + m.Groups[2].ToString() + "\"" + "></ligne>";
                    Match num=Regex.Match(ftxt1, @"(<num>)(.*?)(<\/num>)");
                    ret = ret.Replace("</ligne>", "<champ type=\"numele\">" + num.Groups[2].ToString() +"</champ></ligne>");
                    string a1 = ftxt1.Substring(ftxt1.IndexOf("<tit>")+ 5);
                    string a2 = a1.Substring(a1.IndexOf("<al>") + 4);
                    string a3 = a2.Substring(0, a2.IndexOf("</al>"));
                    ret = ret.Replace("</ligne>", "<champ type=\"titreArticle\">" + a3 + "</champ></ligne>");
                    MatchCollection aut = Regex.Matches(ftxt1, @"(<auteur>)(.*?)(<\/auteur>)");
                    if(aut.Count > 0)
                    {
                        foreach(Match au in aut)
                        {
                            Match fn = Regex.Match(au.ToString(), @"(<prenom>)(.*?)(<\/prenom>)");
                            ret = ret.Replace("</ligne>", "<champ type=\"prenomAuteur\">" + fn.Groups[2].ToString().Trim() +"</champ></ligne>");
                            Match sn = Regex.Match(au.ToString(), @"(<nom>)(.*?)(<\/nom>)");
                            ret = ret.Replace("</ligne>", "<champ type=\"nomAuteur\">" + sn.Groups[2].ToString().Trim() + "</champ></ligne>");
                        }
                    }
                    ret = ret.Replace("</ligne>", "<champ type=\"rpublimpl\">article " + num.Groups[2].ToString().Trim() + "</champ></ligne>");
                    ret = ret.Replace("</ligne>", "<champ type=\"numpage\"></champ></ligne>");
                }
            }
            catch
            {
                ret = "";
            }
            return ret;
        }
        public static void sommdossier(string dirpath)
        {
            string[] fnl_xml = Directory.GetFiles(dirpath);
            string txt1 = "";
            foreach(string fxml in fnl_xml)
            {
                if(fxml.EndsWith("final.xml"))
                {
                    string txt = File.ReadAllText(fxml);
                    MatchCollection matchCollection = Regex.Matches(txt, @"(<sommdossier>)(.*?)(<\/sommdossier>)", RegexOptions.IgnoreCase);
                    if (matchCollection.Count > 0)
                    {
                        foreach (Match mt in matchCollection)
                        {
                            txt1 = mt.Groups[2].ToString();
                            break;
                        }
                        if(txt1!="")
                        {
                            MatchCollection matchCollection1 = Regex.Matches(txt1, @"(\[\[ligne\]\])(.*?)(\[\[\/ligne\]\])", RegexOptions.IgnoreCase);
                            string txt2 = "";
                            if(matchCollection1.Count>0)
                            {                            
                                foreach(Match m1 in matchCollection1)
                                {
                                    if (txt2 == "")
                                    {
                                        txt2 = sommdossier_part(m1.Groups[2].ToString(), dirpath);
                                    }
                                    else
                                    {
                                        txt2 = txt2 + sommdossier_part(m1.Groups[2].ToString(), dirpath);
                                    }
                                }
                            }
                            if(txt2!="")
                            {
                                txt = txt.Replace(txt1, txt2);
                                try
                                {
                                    File.WriteAllText(fxml, txt);
                                }
                                catch
                                {
                                }
                            }
                        }
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        public static bool check_resume(string dirpath)
        {
            bool ret = false;
            string[] fnl_xml = Directory.GetFiles(dirpath);
            foreach (string fxml in fnl_xml)
            {
                if (fxml.EndsWith("final.xml"))
                {
                    try
                    {
                        string txt = File.ReadAllText(fxml);
                        if (txt.Contains("type=\"apercu\"") && txt.Contains("<resume>"))
                        {
                            ret = true;
                            break;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return ret;
        }
        public static bool check_nbbl(string dirpath)
        {
            bool ret = false;
            string[] fnl_xml = Directory.GetFiles(dirpath);
            foreach (string fxml in fnl_xml)
            {
                if (fxml.EndsWith("final.xml"))
                {
                    try
                    {
                        string txt = File.ReadAllText(fxml);
                        if (txt.Contains("type=\"tnc\"") && txt.Contains("</nb-bl>"))
                        {
                            ret = true;
                            break;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return ret;
        }
        public static DataTable ConvertToDataTable(string errorlog)
        {

            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("ln"));
            tbl.Columns.Add(new DataColumn("pos"));
            tbl.Columns.Add(new DataColumn("err"));
            tbl.Columns.Add(new DataColumn("txt"));
            tbl.Columns.Add(new DataColumn("f_name"));
            //tbl.Columns.Add(new DataColumn("sl"));
            tbl.Columns.Add(new DataColumn("meta"));
            string[] lines = System.IO.File.ReadAllLines(errorlog);
            string sep = "\t";
            int total_lines = lines.Count();
            string fn = "";
            int art = 0;
            for (int i = 0; i < total_lines; i++)
            {
                if (lines[i].StartsWith("file_name|"))
                {
                    fn = lines[i].Split('|')[1].ToString();
                    art = art + 1;
                }
                if (lines[i].StartsWith("Validation Error Location"))
                {
                    string[] splitContent = lines[i].Split(sep.ToCharArray());
                    string[] st = splitContent[1].Split(':');
                    DataRow dr = tbl.NewRow();
                    dr["ln"] = st[0].ToString();
                    dr["pos"] = st[1].ToString();
                    dr["err"] = splitContent[2].ToString();
                    dr["f_name"] = fn;
                    tbl.Rows.Add(dr);
                }
            }
            string wd = "";
            try
            {
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    string[] lines1 = System.IO.File.ReadAllLines(tbl.Rows[i]["f_name"].ToString());
                    bool b = true;
                    int ct = 0;
                    while (b)
                    {
                        if (ct == 0)
                        {
                            wd = lines1[Convert.ToInt32(tbl.Rows[i][0]) - 1].Substring(Convert.ToInt32(tbl.Rows[i][1]) - 1);
                        }
                        else
                        {
                            if ((Convert.ToInt32(tbl.Rows[i][0]) - 1 + ct) < lines1.Count())
                            {
                                wd = lines1[Convert.ToInt32(tbl.Rows[i][0]) - 1 + ct];
                            }
                            else
                            {
                                tbl.Rows[i]["txt"] = "";
                                b = false;
                            }
                        }
                        if (wd.Length > 0)
                        {
                            string p = wd.Replace("><", "|");
                            if (p.Length > 0)
                            {
                                p = p.Substring(p.IndexOf(">") + 1);
                                if (p.Length > 0)
                                {
                                    p = p.Substring(0, p.IndexOf("<"));
                                    p = replace_symbol(p).Trim();
                                    tbl.Rows[i]["txt"] = p;
                                    b = false;
                                }
                            }
                        }
                        ct = ct + 1;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return tbl;
        }
        public static void movefile()
        {

            string output = System.Configuration.ConfigurationSettings.AppSettings["output"].ToString();

            string metapath = output + file_name.Replace(" ", "") + "\\";
            string jidval = "";
            string yearval = "";
            string mwval = "";
            string filename = "";

            string path = System.Configuration.ConfigurationSettings.AppSettings["pathmaking"].ToString();

            XmlDocument xmlDoc = new XmlDocument();
            //string myXML = metapath + "\\"+metapath+"_final.xml";
            // string myXML = metapath + file_name+"_final.xml";
            string[] final_xml = Directory.GetFiles(metapath);
            foreach (var myXML in final_xml)
            {
                if (myXML.EndsWith("_final.xml"))
                {

                    xmlDoc.Load(myXML);
                    break;
                }
            }
            var year = xmlDoc.GetElementsByTagName("daterev");
            for (int i = 0; i < year.Count; i++)
            {
                yearval = year[i].Attributes["annee"].Value;
            }

            var mw = xmlDoc.GetElementsByTagName("revnumr");
            for (int i = 0; i < mw.Count; i++)
            {
                mwval = mw[i].InnerText;
            }
            var jid = xmlDoc.GetElementsByTagName("nomrev");
            for (int i = 0; i < jid.Count; i++)
            {
                jidval = jid[i].InnerText;
            }
            if (yearval != "" && mwval != "" && jidval != "")
            {
                filename = jidval + yearval + mwval + "_DC";
            }
            string mwpath = getmw(jidval);
            //string [] collectfiles =  Directory.GetFiles(metapath);
            if (!Directory.Exists(path + mwpath))
            {
                Directory.CreateDirectory(path + mwpath);
            }
            if (!Directory.Exists(path + mwpath + "\\" + jidval))
            {
                Directory.CreateDirectory(path + mwpath + "\\" + jidval);
            }
            if (!Directory.Exists(path + mwpath + "\\" + jidval + "\\" + yearval))
            {
                Directory.CreateDirectory(path + mwpath + "\\" + jidval + "\\" + yearval);
            }
            if (!Directory.Exists(path + mwpath + "\\" + jidval + "\\" + yearval + "\\" + filename))
            {
                Directory.CreateDirectory(path + mwpath + "\\" + jidval + "\\" + yearval + "\\" + filename);
            }
            string fullpath = path + mwpath + "\\" + jidval + "\\" + yearval + "\\" + filename + "\\";
            foreach (var f in final_xml)
            {

                File.Copy(f, fullpath + Path.GetFileName(f), true);


            }
            Directory.Delete(metapath, true);



        }
        public static string replace_symbol(string a)
        {
            if (File.Exists(Application.StartupPath + "\\entity.txt"))
            {
                string[] lines = File.ReadAllLines(Application.StartupPath + "\\entity.txt");
                foreach (string ln in lines)
                {
                    string[] rp = ln.Split(' ');
                    if (a.Contains(rp[1]))
                    {
                        a = a.Replace(rp[1], rp[0]);
                    }
                }
            }
            return a;
        }
        public static string getmw(string jid)
        {
            string output = "";
            string mwtxt = System.Configuration.ConfigurationSettings.AppSettings["weeklymonthly"].ToString();
            string[] lines = System.IO.File.ReadAllLines(mwtxt);


            foreach (string line in lines)
            {
                if (line.StartsWith(jid))
                {
                    output = line.Split(' ')[1].ToString();
                    break;
                }
            }
            return output;
        }
    }
}


