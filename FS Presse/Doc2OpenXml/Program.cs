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
using System.Threading;

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
            if (Directory.Exists(output + Path.GetFileNameWithoutExtension(args[0])))
            {
                Directory.Delete(output + Path.GetFileNameWithoutExtension(args[0]), true);
            }
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
                if (Directory.Exists(output + "Process2A"))
                    Directory.Delete(output + "Process2A", true);
                if (Directory.Exists(output + "Process3"))
                    Directory.Delete(output + "Process3", true);
                if (Directory.Exists(output + "Process4"))
                    Directory.Delete(output + "Process4", true);
                if (Directory.Exists(output + "Process5"))
                    Directory.Delete(output + "Process5", true);
                if (Directory.Exists(output + "Process6"))
                    Directory.Delete(output + "Process6", true);
                if (Directory.Exists(output + "Process7"))
                    Directory.Delete(output + "Process7", true);
                if (Directory.Exists(output + "Process8"))
                    Directory.Delete(output + "Process8", true);//anita29/9/21
                if (Directory.Exists(output + "Process9"))
                    Directory.Delete(output + "Process9", true);
                if (Directory.Exists(output + "TEMP"))
                    Directory.Delete(output + "TEMP", true);//anita29/9/21
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
                    CleanUp(file1);
                    string fname = Path.GetFileName(file1);
                    string fnameonly = Path.GetFileNameWithoutExtension(file1);
                    if (fname.EndsWith(".xml"))
                    // if (fname.EndsWith(".xml") && !fname.ToLower().Contains("metadata"))//metadata xml error not required in errorlog anita26/8/21.
                    {
                        StreamReader sr1 = new StreamReader(file1);
                        string FCon1 = sr1.ReadToEnd();
                        sr1.Close();
                        FCon1 = FCon1.Replace("article.dtd", articledtd);
                        FCon1 = FCon1.Replace("revue.dtd", reveuedtd);

                        StreamWriter sw1 = new StreamWriter(file1);
                        sw1.Write(FCon1);
                        sw1.Close();
                        if (!fname.ToLower().Contains("metadata"))//metadata xml error not required in errorlog anita27/8/21.
                        {
                            Parser(metapath + fname);
                        }
                    }
                }
                AddComments(args[0], output + "errorlog.txt");
                if (delxml == true)//client requirement but not delivered 
                {
                    Directory.Delete(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", "") + "\\", true);

                    File.AppendAllText(output + "errorlog.txt", "Total XML count = 0" + Environment.NewLine);
                }
                //else
                {
                    sommdossier(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", ""));
                    rename(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", ""));
                    f_note(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", ""));
                    string[] getfile9 = Directory.GetFiles(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + Path.GetFileNameWithoutExtension(args[0]));
                    int countxml = 0;
                    foreach (string xl in getfile9)
                    {
                        if (xl.EndsWith("final.xml"))
                        {
                            countxml = countxml + 1;
                        }
                    }
                    File.AppendAllText(output + "errorlog.txt", "Total XML count = " + countxml + Environment.NewLine);
                    // movefile();//anita19_aug21
                    Removefinal();
                    img_rename();
                    Entity_conversion(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", "") + "\\");
                    string mov = movefile();//anita19_aug21
                    if (mov != "")
                    {
                        File.AppendAllText(output + "errorlog.txt", mov + Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("" + ex.Message);
                writeLog.AppendLog("exception" + ex.Message);
            }
            if (System.Configuration.ConfigurationSettings.AppSettings["pro_copy"].ToString() == "client")
            {
                if (File.Exists(output + "errorlog.txt"))
                {
                    File.Delete(output + "errorlog.txt");
                }
                if (Directory.Exists(output + "LogFile"))
                {
                    Directory.Delete(output + "LogFile", true);
                }
            }
        }
        public static void CleanUp(string final_xml1)
        {
            try
            {

                StreamReader sr = new StreamReader(final_xml1);
                string str = sr.ReadToEnd();
                sr.Close();

                //string[] SplitArr = new string[4];
                //SplitArr[0] = "\n\r";
                //SplitArr[1] = "\r\n";
                //SplitArr[2] = "\n";
                //SplitArr[3] = "\r";

                str = str.Replace(">\n\n<", ">\n<");
                //str = str.Replace("\r\r", "\n");
                str = str.Replace(">\n\n<", ">\n<");
                str = str.Replace(">\n\n<", ">\n<");
                //str = str.Replace("\r\r", "\n");
                str = str.Replace(">\n\n<", ">\n<");
                str = str.Replace(" </revnumr>", "</revnumr>");
                str = str.Replace(" </revnumr>", "</revnumr>");
                str = str.Replace(" </emph1>", "</emph1> ");
                str = str.Replace(" </emph1>", "</emph1> ");
                str = str.Replace("</emph1>  ", "</emph1> ");
                str = str.Replace("</emph1>  ", "</emph1> ");
                str = str.Replace(" </emph2>", "</emph2> ");
                str = str.Replace(" </emph2>", "</emph2> ");
                str = str.Replace("</emph2>  ", "</emph2> ");
                str = str.Replace("</emph2>  ", "</emph2> ");
                str = str.Replace(" </emph3>", "</emph3> ");
                str = str.Replace(" </emph3>", "</emph3> ");
                str = str.Replace("</emph3>  ", "</emph3> ");
                str = str.Replace("</emph3>  ", "</emph3> ");

                str = str.Replace(" </al>", "</al>");
                str = str.Replace(" </al>", "</al>");
                str = str.Replace("<al> ", "<al>");
                str = str.Replace("<al> ", "<al>");
                str = str.Replace("\n\n", "\n");
                str = str.Replace("\n\n", "\n");
                str = str.Replace("\n\n", "\n");
                str = str.Replace("\n\n", "\n");
                str = str.Replace("<entry align=\"center center\">", "<entry align=\"center\">");

                //str = str.Replace(" </", "</");
                //str = str.Replace(" </", "</");

                //str = str.Replace("\r\r", "\r");
                StreamWriter sw = new StreamWriter(final_xml1);
                sw.Write(str);
                sw.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
        public static bool nbblignore = false;
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

            StreamReader SR = new StreamReader(final_xml1);
            string sr1 = SR.ReadToEnd();
            SR.Close();

            if (sr1.Contains("<etude "))
                nbblignore = true;
            else
                nbblignore = false;

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

            //Console.WriteLine("Validation Error: {0}", e.Message);
            //added by anita29/6/2021

            if (e.Message.Contains("The 'div1' element is not declared.") || e.Message.Contains("The 'div2' element is not declared.") || e.Message.Contains("The 'div3' element is not declared.") || e.Message.Contains("The element 'titab' has incomplete content. List of possible elements expected: 'al'.") || e.Message.Contains("The element 'titab' cannot contain text. List of possible elements expected: 'al'.") || e.Message.Contains("The element 'chfer' has invalid child element") || e.Message.Contains("The element 'alerte' has invalid child element 'auteur'"))
            {
            }//added by anita29/6/2021
            else
            {
                Console.WriteLine("Validation Error: {0}", e.Message);
                if (nbblignore == true)
                {
                    log.AppendLog("Validation Error:" + e.Message.Replace("nb-bl", "nb bl") + "\t Line Number: " + (e.Exception).LineNumber + "\t Line Position: " + (e.Exception).LinePosition);
                    File.AppendAllText(outputpath + "errorlog.txt", "Validation Error Location:\t" + (e.Exception).LineNumber + ":" + (e.Exception).LinePosition + "\t" + e.Message.Replace("nb-bl", "nb bl") + Environment.NewLine);
                }
                else
                {
                    log.AppendLog("Validation Error:" + e.Message + "\t Line Number: " + (e.Exception).LineNumber + "\t Line Position: " + (e.Exception).LinePosition);
                    File.AppendAllText(outputpath + "errorlog.txt", "Validation Error Location:\t" + (e.Exception).LineNumber + ":" + (e.Exception).LinePosition + "\t" + e.Message + Environment.NewLine);
                }
            }
        }
        public static void DocxToOpenXML(string path)
        {
            string extractPath = System.Configuration.ConfigurationSettings.AppSettings["docxfile"].ToString();
            string xlsfile1 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile1"].ToString();
            string xlsfile1A = System.Configuration.ConfigurationSettings.AppSettings["xlsfile1A"].ToString();
            string xlsfile2 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile2"].ToString();
            string xlsfile3 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile3"].ToString();//anita5_oct_2020
            string xlsfile4 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile4"].ToString();//anita6_oct_2020
            string xlsfile5 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile5"].ToString();
            string xlsfile6 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile6"].ToString();
            string xlsfile7 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile7"].ToString();//anita 14_may_2021
            string xlsfile8 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile8"].ToString();//anita 14_may_2021
            string xlsfile9 = System.Configuration.ConfigurationSettings.AppSettings["xlsfile9"].ToString();
            string xlsfileMergeXML = System.Configuration.ConfigurationSettings.AppSettings["xlsfileMergeXML"].ToString();//anita6_oct_2020
            string xlsfile1Metadata = System.Configuration.ConfigurationSettings.AppSettings["xlsfile1Metadata"].ToString();//anita13_oct_2020
            string xlsfile2Metadata = System.Configuration.ConfigurationSettings.AppSettings["xlsfile2Metadata"].ToString();//anita13_oct_2020
            string output = System.Configuration.ConfigurationSettings.AppSettings["output"].ToString();
            WriteLog log = new WriteLog((System.Configuration.ConfigurationSettings.AppSettings["logfile"].ToString()));
            try
            {
                //=======================================================
                string imgpath = System.Configuration.ConfigurationSettings.AppSettings["img"].ToString();
                if (Directory.Exists(imgpath))
                {
                    Directory.Delete(imgpath, true);
                }
                Directory.CreateDirectory(imgpath);
                string tempdir = DateTime.Now.ToString().Replace("-", "").Replace(" ", "").Replace(":", "");
                tempdir = path.Replace(Path.GetFileName(path), tempdir);
                if (Directory.Exists(tempdir))
                {
                    Directory.Delete(tempdir, true);
                }
                Directory.CreateDirectory(tempdir);
                File.Copy(path, tempdir + "\\" + Path.GetFileName(path), true);
                path = tempdir + "\\" + Path.GetFileName(path);
                ImgProcess imgProcess = new ImgProcess();
                imgProcess.ExtractImage(path, imgpath);
                //=======================================================
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
                //=========================================
                //File.Delete(zipPath);
                Directory.Delete(tempdir, true);
                //=========================================
                Console.WriteLine("File exctract successfully");
                log.AppendLog("Step 1: File exctract successfully()");
                process2(extractPath + @"word\document.xml", xlsfile1);
                Console.WriteLine("process 2 completed");
                log.AppendLog("Step 2 :process 2 completed  ");
                string[] getfile = Directory.GetFiles(extractPath);
                string process2Aoutputpath = @output + "process2A\\";
                string process3outputpath = @output + "process3\\";
                string process4outputpath = @output + "process4\\";
                string process5outputpath = @output + "process5\\";
                string process6outputpath = @output + "process6\\";
                string process7outputpath = @output + "process7\\";
                string process8outputpath = @output + "process8\\";
                string process9outputpath = @output + "process9\\";
                if (Directory.Exists(process2Aoutputpath))
                {
                    var dir = new DirectoryInfo(process2Aoutputpath);
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);

                }
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
                if (Directory.Exists(process8outputpath))
                {
                    var dir = new DirectoryInfo(process8outputpath);
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);

                }

                if (Directory.Exists(process9outputpath))
                {
                    var dir = new DirectoryInfo(process9outputpath);
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
                        final_xml_path = process2Aoutputpath + fnameonly + "_final.xml";
                        process2A(file, xlsfile1A, final_xml_path);

                        Console.WriteLine("Step 2A: process 2A completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                        log.AppendLog("Step 2A: process 2A completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                    }
                    else if (fname == "[Content_Types].xml" && getfile.Count() < 2)
                    {
                        Console.WriteLine("Step 2A: no xml found for process 2A");
                        log.AppendLog("Step 2A: no xml found for process 2A");
                    }
                }

                //anita5_oct_2020
                if (Directory.Exists(process2Aoutputpath))
                {
                    string[] getfile1 = Directory.GetFiles(process2Aoutputpath);
                    foreach (string file1 in getfile1)
                    {
                        string fname = Path.GetFileName(file1);
                        string fnameonly = Path.GetFileNameWithoutExtension(file1);
                        if (fname.EndsWith(".xml"))
                        {
                            final_xml_path = process3outputpath + fnameonly + ".xml";
                            process3(file1, xlsfile2, final_xml_path);

                            Console.WriteLine("Step 3: process 3 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                            log.AppendLog("Step 3: process 3 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                        }
                        else
                        {
                            Console.WriteLine("Step 3: no xml found for process 3 at");
                            log.AppendLog("Step 3: no xml found for process ");
                        }
                    }
                }
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

                            Console.WriteLine("Step 4: process 4 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                            log.AppendLog("Step 4: process 4 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
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

                            Console.WriteLine("Step 5: process 5 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                            log.AppendLog("Step 5: process 5 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
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

                            Console.WriteLine("Step 6: process 6 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                            log.AppendLog("Step 6: process 6 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
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

                            Console.WriteLine("Step 7: process 7 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                            log.AppendLog("Step 7: process 7 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                        }
                        else
                        {
                            Console.WriteLine("Step 7: no xml found for process 6");
                            log.AppendLog("Step 7: no xml found for process ");
                        }
                    }
                }
                if (Directory.Exists(process7outputpath))
                {
                    string[] getfile2 = Directory.GetFiles(process7outputpath);
                    foreach (string file2 in getfile2)
                    {
                        string fname = Path.GetFileName(file2);
                        string fnameonly = Path.GetFileNameWithoutExtension(file2);
                        if (fname.EndsWith(".xml"))
                        {
                            final_xml_path = process8outputpath + fnameonly + ".xml";
                            if (File.Exists(final_xml_path))
                            {

                                File.Delete(final_xml_path);
                            }
                            process8(file2, xlsfile7, final_xml_path);

                            Console.WriteLine("Step 8: process 8 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                            log.AppendLog("Step 8: process 8 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                        }
                        else
                        {
                            Console.WriteLine("Step 8: no xml found for process ");
                            log.AppendLog("Step 8: no xml found for process ");
                        }
                    }
                }
                if (Directory.Exists(process8outputpath))
                {
                    string[] getfile2 = Directory.GetFiles(process8outputpath);
                    foreach (string file2 in getfile2)
                    {
                        string fname = Path.GetFileName(file2);
                        string fnameonly = Path.GetFileNameWithoutExtension(file2);
                        if (fname.EndsWith(".xml"))
                        {
                            final_xml_path = process9outputpath + fnameonly + ".xml";
                            if (File.Exists(final_xml_path))
                            {

                                File.Delete(final_xml_path);
                            }
                            process9(file2, xlsfile8, final_xml_path);

                            Console.WriteLine("Step 9: process 9 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                            log.AppendLog("Step 9: process 9 completed file created :" + final_xml_path.Replace("_final.xml", ".xml"));
                        }
                        else
                        {
                            Console.WriteLine("Step 9: no xml found for process ");
                            log.AppendLog("Step 9: no xml found for process ");
                        }
                    }
                }

                if (Directory.Exists(process9outputpath))
                {
                    string[] getfile3 = Directory.GetFiles(process9outputpath);
                    //int count = getfile3.Count();

                    //File.AppendAllText(output + "errorlog.txt", "Total XML count = " + count + Environment.NewLine);
                    string fl = "";
                    foreach (string file3 in getfile3)
                    {
                        string fname = Path.GetFileName(file3);
                        string fnameonly = Path.GetFileNameWithoutExtension(file3);
                        if (fname.EndsWith(".xml"))
                        {
                            final_xml_path = output + fnameonly + ".xml";
                            string process10outputxml = output + fileName.Replace(" ", "");
                            if (File.Exists(final_xml_path))
                            {

                                File.Delete(final_xml_path);
                            }
                            if (!File.Exists(process10outputxml))
                            {
                                Directory.CreateDirectory(process10outputxml);
                            }
                            process10outputxml = process10outputxml + "\\" + fnameonly + ".xml";
                            process10(file3, xlsfile9, process10outputxml);
                            fl = process10outputxml;
                            Console.WriteLine("Step 10: process 10 completed file created :" + process10outputxml.Replace("_final.xml", ".xml"));
                            log.AppendLog("Step 10: process 10 completed file created :" + process10outputxml.Replace("_final.xml", ".xml"));

                        }
                        else
                        {
                            Console.WriteLine("Step 10: no xml found for process");
                            log.AppendLog("Step 10: no xml found for process");
                        }
                    }
                    //string fl1 = fl.Replace("final", "Merge");
                    //process8(fl, xlsfileMergeXML, fl1);
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
        public static void process9(string XML, string word2xml_08_xsl, string final)
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
            proc.StartInfo.Arguments = @"-s:" + XML + " -xsl:" + word2xml_08_xsl + " -o:" + final;

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
        public static void process10(string XML, string word2xml_09_xsl, string final)
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
            proc.StartInfo.Arguments = @"-s:" + XML + " -xsl:" + word2xml_09_xsl + " -o:" + final;

            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
        //anita5_oct_2020
        public static void process2A(string temp_xml, string word2xml_01A_xsl, string xml)
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
            proc.StartInfo.Arguments = @"-s:" + temp_xml + " -xsl:" + word2xml_01A_xsl + " -o:" + xml;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
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
            string xlsfile0Metadata = System.Configuration.ConfigurationSettings.AppSettings["xlsfile0Metadata"].ToString();
            string xlsfile1Metadata = System.Configuration.ConfigurationSettings.AppSettings["xlsfile1Metadata"].ToString();
            string xlsfile2Metadata = System.Configuration.ConfigurationSettings.AppSettings["xlsfile2Metadata"].ToString();
            string xlsfile3Metadata = System.Configuration.ConfigurationSettings.AppSettings["xlsfile3Metadata"].ToString();
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
                        process0ForMetadata(file3, xlsfile0Metadata);
                        process1ForMetadata(file3, xlsfile1Metadata);
                        process2ForMetadata(file3, xlsfile2Metadata);
                        process3ForMetadata(file3, xlsfile3Metadata);
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
        public static void process0ForMetadata(string Metadata_xml, string word2xml_meta_00)
        {
            Process proc = null;
            string sPath = null;
            proc = new Process();
            string Transformexe = System.Configuration.ConfigurationSettings.AppSettings["Transformexe"].ToString();
            proc.StartInfo.WorkingDirectory = Transformexe;
            proc.StartInfo.FileName = Transformexe + "Transform.exe";
            proc.StartInfo.Arguments = @"-s:" + Metadata_xml + " -xsl:" + word2xml_meta_00 + " -o:" + Metadata_xml;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
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
        public static void process3ForMetadata(string Metadata_xml, string word2xml_meta_03)
        {
            Process proc = null;
            string sPath = null;
            proc = new Process();
            string Transformexe = System.Configuration.ConfigurationSettings.AppSettings["Transformexe"].ToString();
            proc.StartInfo.WorkingDirectory = Transformexe;
            proc.StartInfo.FileName = Transformexe + "Transform.exe";
            proc.StartInfo.Arguments = @"-s:" + Metadata_xml + " -xsl:" + word2xml_meta_03 + " -o:" + Metadata_xml;
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
                            int freq = 0;
                            foreach (var para1 in body)
                            {
                                string pr = Regex.Replace(para1.InnerText, @"\u00a0", " ");
                                if (pr.Contains(dt.Rows[i]["txt"].ToString()))
                                {
                                    freq = freq + 1;
                                    if (freq.ToString() == dt.Rows[i]["freq"].ToString())
                                    {
                                        if (dt.Rows[i]["f_name"].ToString().ToLower().Contains("metadata"))
                                        {
                                            //if (para1.InnerText == dt.Rows[i]["txt"].ToString() && dt.Rows[i]["meta"].ToString()=="1")
                                            if (dt.Rows[i]["meta"].ToString() == "0" || dt.Rows[i]["meta"].ToString() == "1")
                                            {
                                                //id = (Convert.ToInt32(id) + 1).ToString();
                                                string er1 = dt.Rows[i]["err"].ToString();
                                                if (dt.Rows[i]["err"].ToString().ToLower().Contains("has invalid child element 'nb-bl'"))
                                                {
                                                    delxml = true;
                                                    er1 = "Les points clés ne sont pas autorisés pour ce type d'article. De ce fait, le XML ne sera pas généré.";
                                                }
                                                if (dt.Rows[i]["err"].ToString().ToLower().Contains("has invalid child element 'pnchr'"))
                                                {
                                                    delxml = true;
                                                    er1 = "Paragraphe num - Titre noyé n'est pas permis pour ce typed'item. Merci de corriger.";
                                                }

                                                if (para1.LocalName == "tbl")
                                                {
                                                    foreach (var tr in para1)
                                                    {
                                                        if (tr.LocalName == "tr")
                                                        {
                                                            foreach (var tc in tr)
                                                            {
                                                                if (tc.LocalName == "tc")
                                                                {
                                                                    foreach (var td in tc)
                                                                    {
                                                                        string pr1 = Regex.Replace(td.InnerText, @"\u00a0", " ");
                                                                        if (pr1.Contains(dt.Rows[i]["txt"].ToString()))
                                                                        {
                                                                            id = (Convert.ToInt32(id) + 1).ToString();
                                                                            Paragraph p = new Paragraph(new Run(new Text(er1 + "-métadonnées")));
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
                                                                            td.InsertAt(new CommentReference() { Id = id }, 0);
                                                                            td.InsertAt(new CommentRangeEnd() { Id = id }, 0);
                                                                            td.InsertAt(new CommentRangeStart() { Id = id }, 0);
                                                                            Thread.Sleep(500);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    id = (Convert.ToInt32(id) + 1).ToString();
                                                    Paragraph p = new Paragraph(new Run(new Text(er1 + "-métadonnées")));
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
                                                    Thread.Sleep(500);
                                                    writeLog.AppendLog(p.InnerText + " near " + "\"" + dt.Rows[i]["txt"].ToString() + "\"" + " in metadata.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (body.InnerXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").IndexOf(para1.OuterXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "")) >= Convert.ToInt32(dt.Rows[i]["meta"]) + (loop1 * 93))
                                            {
                                                string er1 = dt.Rows[i]["err"].ToString();
                                                //id = (Convert.ToInt32(id) + 1).ToString();
                                                if (dt.Rows[i]["err"].ToString().ToLower().Contains("has invalid child element 'nb-bl'"))
                                                {
                                                    delxml = true;
                                                    er1 = "Les points clés ne sont pas autorisés pour ce type d'article. De ce fait, le XML ne sera pas généré.";
                                                }
                                                if (dt.Rows[i]["err"].ToString().ToLower().Contains("has invalid child element 'pnchr'"))
                                                {
                                                    delxml = true;
                                                    er1 = "Paragraphe num - Titre noyé n'est pas permis pour ce typed'item. Merci de corriger.";
                                                }

                                                if (para1.LocalName == "tbl")
                                                {
                                                    foreach (var tr in para1)
                                                    {
                                                        if (tr.LocalName == "tr")
                                                        {
                                                            foreach (var tc in tr)
                                                            {
                                                                if (tc.LocalName == "tc")
                                                                {
                                                                    foreach (var td in tc)
                                                                    {
                                                                        string pr1 = Regex.Replace(td.InnerText, @"\u00a0", " ");
                                                                        if (pr1.Contains(dt.Rows[i]["txt"].ToString()))
                                                                        {
                                                                            id = (Convert.ToInt32(id) + 1).ToString();
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
                                                                            td.InsertAt(new CommentReference() { Id = id }, 0);
                                                                            td.InsertAt(new CommentRangeEnd() { Id = id }, 0);
                                                                            td.InsertAt(new CommentRangeStart() { Id = id }, 0);
                                                                            Thread.Sleep(500);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    id = (Convert.ToInt32(id) + 1).ToString();
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
                                                    Thread.Sleep(500);
                                                    writeLog.AppendLog(p.InnerText + " near " + "\"" + dt.Rows[i]["txt"].ToString() + "\"" + ".");
                                                }
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
                                            Paragraph p = new Paragraph(new Run(new Text("Article répété " + para1.InnerText.ToString().Trim() + ". Les fichiers XML ne seront pas disponibles.")));
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
                                            Thread.Sleep(500);
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
                            MatchCollection m1 = Regex.Matches(txt, @"(<resume><p>)(.*?)(<\/p><\/resume>)");
                            foreach (Match m in m1)
                            {
                                Match m2 = Regex.Match(m.ToString(), @"(<al>)(.*?)(<\/al>)");
                                string ad = replace_symbol(m2.Groups[2].ToString());
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
                                string pr = Regex.Replace(para1.InnerText, @"\u00a0", " ");
                                if (para1.LocalName == "p" && pr.Contains(nb))
                                {
                                    try
                                    {
                                        id = (Convert.ToInt32(id) + 1).ToString();
                                        Paragraph p = new Paragraph(new Run(new Text("« Resume » n'est pas autorisé dans cet article. DONC, les fichiers XML ne seront pas disponibles.")));
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
                                        Thread.Sleep(500);
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
                //if (check_plan(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", "")) == true)
                //{
                //    string plan = "";
                //    string[] nbl = Directory.GetFiles(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", ""));
                //    foreach (string fl in nbl)
                //    {
                //        if (fl.EndsWith("final.xml"))
                //        {
                //            string[] lines = File.ReadAllLines(fl);
                //            string txt = "";
                //            foreach (string ln in lines)
                //            {
                //                txt = txt + ln;
                //            }
                //            MatchCollection m1 = Regex.Matches(txt, @"(<plan/>)(.*?)");
                //            foreach (Match m in m1)
                //            {
                //                string ad = replace_symbol(m.Groups[2].ToString());
                //                //ad = Regex.Replace(ad, @"(<)(.*?)(>)", "");
                //                ad = ad.Replace("&nbsp;", " ");
                //                if (plan == "")
                //                {
                //                    plan = ad;
                //                }
                //                else
                //                {
                //                    if (plan.Contains(ad) == false)
                //                    {
                //                        plan = plan + "|" + ad;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    if (plan != "")
                //    {
                //        string[] nb_bl = plan.Split('|');
                //        foreach (string nb in nb_bl)
                //        {
                //            string pn = nb;
                //            foreach (var para1 in body)
                //            {
                //                string pr = Regex.Replace(para1.InnerText, @"\u00a0", " ");
                //                //if (nb.Contains("<al>"))
                //                //{
                //                //    pn = Regex.Match(nb, @"(<al>)(.*?)(<\/al>)").Groups[2].ToString();
                //                //}
                //                if (para1.LocalName == "p" && pr.Contains(pn))
                //                {
                //                    try
                //                    {
                //                        id = (Convert.ToInt32(id) + 1).ToString();
                //                        Paragraph p = new Paragraph(new Run(new Text("'Plan' is not allowed in this article. SO, XML files will not be available.")));
                //                        delxml = true;
                //                        Comment cmt =
                //                            new Comment()
                //                            {
                //                                Id = id,
                //                                Author = "Thomson Digital",
                //                                Initials = "TD",
                //                                Date = DateTime.Now
                //                            };
                //                        cmt.AppendChild(p);
                //                        comments.AppendChild(cmt);
                //                        comments.Save();
                //                        para1.InsertAt(new CommentReference() { Id = id }, 0);
                //                        para1.InsertAt(new CommentRangeEnd() { Id = id }, 0);
                //                        para1.InsertAt(new CommentRangeStart() { Id = id }, 0);
                //                        writeLog.AppendLog(p.InnerText);
                //                        delxml = true;
                //                        break;
                //                    }
                //                    catch
                //                    {
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                if (check_nbbl(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", "")) == true)
                {
                    string nbbl = "";
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
                            MatchCollection m1 = Regex.Matches(txt, @"(<nb-bl [^><]+><al>)(.*?)(<\/al><\/nb-bl>)");
                            foreach (Match m in m1)
                            {
                                string ad = replace_symbol(m.Groups[2].ToString());
                                ad = Regex.Replace(ad, @"(<)(.*?)(>)", "");
                                ad = ad.Replace("&nbsp;", " ");
                                if (nbbl == "")
                                {
                                    nbbl = ad;
                                }
                                else
                                {
                                    if (nbbl.Contains(ad) == false)
                                    {
                                        nbbl = nbbl + "|" + ad;
                                    }
                                }
                            }
                        }
                    }
                    if (nbbl != "")
                    {
                        string[] nb_bl = nbbl.Split('|');
                        foreach (string nb in nb_bl)
                        {
                            foreach (var para1 in body)
                            {
                                string pr = Regex.Replace(para1.InnerText, @"\u00a0", " ");
                                if (para1.LocalName == "p" && pr.Contains(nb))
                                {
                                    try
                                    {
                                        id = (Convert.ToInt32(id) + 1).ToString();
                                        Paragraph p = new Paragraph(new Run(new Text("Les points clés ne sont pas autorisés pour ce type d'article. De ce fait, le XML ne sera pas généré.")));
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
                                        Thread.Sleep(500);
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
            catch (Exception ex)
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
                    foreach (string l in ftxt)
                    {
                        ftxt1 = ftxt1 + l;
                    }
                    ret = "<ligne artid=\"" + m.Groups[2].ToString() + "\"" + "></ligne>";
                    Match num = Regex.Match(ftxt1, @"(<num>)(.*?)(<\/num>)");
                    ret = ret.Replace("</ligne>", "<champ type=\"numele\">" + num.Groups[2].ToString() + "</champ></ligne>");
                    string a1 = ftxt1.Substring(ftxt1.IndexOf("<tit>") + 5);
                    string a2 = a1.Substring(a1.IndexOf("<al>") + 4);
                    string a3 = a2.Substring(0, a2.IndexOf("</al>"));
                    ret = ret.Replace("</ligne>", "<champ type=\"titreArticle\">" + a3 + "</champ></ligne>");
                    MatchCollection aut = Regex.Matches(ftxt1, @"(<auteur>)(.*?)(<\/auteur>)");
                    if (aut.Count > 0)
                    {
                        foreach (Match au in aut)
                        {
                            Match fn = Regex.Match(au.ToString(), @"(<prenom>)(.*?)(<\/prenom>)");
                            ret = ret.Replace("</ligne>", "<champ type=\"prenomAuteur\">" + fn.Groups[2].ToString().Trim() + "</champ></ligne>");
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
            foreach (string fxml in fnl_xml)
            {
                if (fxml.EndsWith("final.xml"))
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
                        if (txt1 != "")
                        {
                            MatchCollection matchCollection1 = Regex.Matches(txt1, @"(\[\[ligne\]\])(.*?)(\[\[\/ligne\]\])", RegexOptions.IgnoreCase);
                            string txt2 = "";
                            if (matchCollection1.Count > 0)
                            {
                                foreach (Match m1 in matchCollection1)
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
                            if (txt2 != "")
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
                        //if (txt.Contains("type=\"chronique\"") && txt.Contains("<resume>"))
                        //{
                        //    ret = true;
                        //    break;
                        //}
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
        //public static bool check_plan(string dirpath)
        //{
        //    bool ret = false;
        //    string[] fnl_xml = Directory.GetFiles(dirpath);
        //    foreach (string fxml in fnl_xml)
        //    {
        //        if (fxml.EndsWith("final.xml"))
        //        {
        //            try
        //            {
        //                string txt = File.ReadAllText(fxml);
        //                if (txt.Contains("type=\"chronique\"") && txt.Contains("<plan/>"))
        //                {
        //                    ret = true;
        //                    break;
        //                }
        //            }
        //            catch
        //            {
        //            }
        //        }
        //    }
        //    return ret;
        //}
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
            tbl.Columns.Add(new DataColumn("freq"));
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
                                    p = p.Replace("&nbsp;", " ");
                                    tbl.Rows[i]["txt"] = p;
                                    b = false;
                                }
                            }
                        }
                        ct = ct + 1;
                    }
                }
                string fx = "";
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    int count = 0;
                    string tx = tbl.Rows[i]["txt"].ToString();
                    string er = tbl.Rows[i]["err"].ToString();
                    if (fx.Contains(tx + er) == false)
                    {
                        for (int j = 0; j < tbl.Rows.Count; j++)
                        {
                            if (tbl.Rows[j]["txt"].ToString() + tbl.Rows[j]["err"].ToString() == tx + er)
                            {
                                count = count + 1;
                                tbl.Rows[j]["freq"] = count.ToString();
                            }
                        }
                    }
                    if (i == 0)
                    {
                        fx = tx;
                    }
                    else
                    {
                        if (fx.Contains(tx) == false)
                        {
                            fx = fx + "|" + tx;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return tbl;
        }
        public static string movefile()
        {
            string out1 = "";
            string output = System.Configuration.ConfigurationSettings.AppSettings["output"].ToString();
            string metapath = output + file_name.Replace(" ", "") + "\\";
            string jidval = "";
            string yearval = "";
            string mwval = "";
            string filename = "";
            string pro_copy = System.Configuration.ConfigurationSettings.AppSettings["pro_copy"].ToString();
            string path = "";
            if (pro_copy == "td")
            {
                path = System.Configuration.ConfigurationSettings.AppSettings["td_path"].ToString();
            }
            if (pro_copy == "client")
            {
                path = System.Configuration.ConfigurationSettings.AppSettings["client_path"].ToString();
            }
            XmlDocument xmlDoc = new XmlDocument();
            //string myXML = metapath + "\\"+metapath+"_final.xml";
            // string myXML = metapath + file_name+"_final.xml";
            string[] final_xml = Directory.GetFiles(metapath);
            string txt11 = "";
            foreach (var myXML in final_xml)
            {
                if (!myXML.EndsWith("_metadata.xml"))
                {
                    string[] lines = File.ReadAllLines(myXML);
                    foreach (string ln in lines)
                    {
                        txt11 = txt11 + ln;
                    }
                    //xmlDoc.Load(myXML);
                    break;
                }
            }
            if (txt11 != "")
            {
                string txt2 = "";
                MatchCollection matchCollection11 = Regex.Matches(txt11, @"(<metaart>)(.*?)(<\/metaart>)", RegexOptions.IgnoreCase);
                if (matchCollection11.Count > 0)
                {
                    txt2 = Regex.Match(txt11.ToString(), @"(<metaart>)(.*?)(<\/metaart>)").Groups[2].ToString().Trim();
                    if (txt2 != "")
                    {
                        MatchCollection matchCollection22 = Regex.Matches(txt2, @"(<nomrev>)(.*?)(<\/nomrev>)", RegexOptions.IgnoreCase);
                        if (matchCollection22.Count > 0)
                        {
                            jidval = Regex.Match(txt2, @"(<nomrev>)(.*?)(<\/nomrev>)").Groups[2].ToString().Trim();
                        }
                        MatchCollection matchCollection33 = Regex.Matches(txt2, @"(<revnumr>)(.*?)(<\/revnumr>)", RegexOptions.IgnoreCase);
                        if (matchCollection33.Count > 0)
                        {
                            mwval = Regex.Match(txt2, @"(<revnumr>)(.*?)(<\/revnumr>)").Groups[2].ToString().Trim();
                        }
                        MatchCollection matchCollection44 = Regex.Matches(txt2, "(annee=\")(.*?)(\")", RegexOptions.IgnoreCase);
                        if (matchCollection44.Count > 0)
                        {
                            yearval = Regex.Match(txt2, "(annee=\")(.*?)(\")").Groups[2].ToString().Trim();
                        }
                    }
                }
                //jidval = Regex.Match(txt2, @"(<nomrev>)(.*?)(<\/nomrev>)").Groups[2].ToString().Trim();
                //mwval = Regex.Match(txt2, @"(<revnumr>)(.*?)(<\/revnumr>)").Groups[2].ToString().Trim();
                //yearval = Regex.Match(txt2, "(annee=\")(.*?)(\")").Groups[2].ToString().Trim();
                //var year = xmlDoc.GetElementsByTagName("daterev");
                //for (int i = 0; i < year.Count; i++)
                //{
                //    yearval = year[i].Attributes["annee"].Value;
                //}
                //var mw = xmlDoc.GetElementsByTagName("revnumr");
                //for (int i = 0; i < mw.Count; i++)
                //{
                //    mwval = mw[i].InnerText;
                //}
                //var jid = xmlDoc.GetElementsByTagName("nomrev");
                //for (int i = 0; i < jid.Count; i++)
                //{
                //    jidval = jid[i].InnerText;
                //}
                if (yearval != "" && mwval != "" && jidval != "")
                {
                    //filename = jidval + yearval + mwval + "_DC";
                    filename = jidval + yearval + mwval;
                }
                else
                {
                    out1 = "nomrev/revnumr/annee valeur introuvable. Le fichier ne peut pas être déplacé";
                }
            }
            if (filename != "")
            {
                string mwpath = getmw(jidval);
                // string [] collectfiles =  Directory.GetFiles(metapath);
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
                if (!Directory.Exists(path + mwpath + "\\" + jidval + "\\" + yearval + "\\" + filename + "\\" + "5_Départ copie"))
                {
                    //Directory.Delete(path + mwpath + "\\" + jidval + "\\" + yearval + "\\" + filename + "\\" + "5_Départ copie", true);
                    Directory.CreateDirectory(path + mwpath + "\\" + jidval + "\\" + yearval + "\\" + filename + "\\" + "5_Départ copie");
                }
                //Directory.CreateDirectory(path + mwpath + "\\" + jidval + "\\" + yearval + "\\" + filename + "\\" + "5_Départ copie");
                string fullpath = path + mwpath + "\\" + jidval + "\\" + yearval + "\\" + filename + "\\" + "5_Départ copie" + "\\";
                foreach (var f in final_xml)
                {
                    File.Copy(f, fullpath + Path.GetFileName(f), true);
                }
                Directory.Delete(metapath, true);
                //=================================================
                if (Directory.Exists(fullpath + "img"))
                {
                    Directory.Delete(fullpath + "img");
                }                
                string imgbasepath = System.Configuration.ConfigurationSettings.AppSettings["img"].ToString();
                if (Directory.Exists(imgbasepath))
                {
                    string[] imgs = Directory.GetFiles(imgbasepath, "*.*", SearchOption.TopDirectoryOnly);
                    if (imgs.Length > 0)
                    {
                        Directory.CreateDirectory(fullpath + "img");
                        foreach (string im in imgs)
                        {
                            File.Copy(im, fullpath + "img\\" + Path.GetFileName(im), true);
                        }
                    }
                    Directory.Delete(imgbasepath, true);
                }
                //=================================================
            }
            return out1;
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
        public static void rename(string dirpath)
        {
            string[] fnl_xml = Directory.GetFiles(dirpath);
            foreach (string fxml in fnl_xml)
            {
                if (fxml.EndsWith("metadata.xml"))
                {
                    string[] lines = File.ReadAllLines(fxml);
                    string txt = "";
                    foreach (string ln in lines)
                    {
                        txt = txt + ln;
                    }
                    if (txt.ToLower().Contains("<tit><al>dossier</al></tit>"))
                    {
                        MatchCollection mch = Regex.Matches(txt, "(refart fic=\")(.*?)(\")", RegexOptions.IgnoreCase);
                        string ren = "";
                        try
                        {
                            foreach (Match m in mch)
                            {
                                if (m.Groups[2].ToString().EndsWith(".xml"))
                                {
                                    if (File.Exists(dirpath + "\\" + m.Groups[2].ToString().Replace(".xml", "_final.xml")))
                                    {
                                        string rtext = File.ReadAllText(dirpath + "\\" + m.Groups[2].ToString().Replace(".xml", "_final.xml"));
                                        if (rtext.Contains("type=\"etude\""))
                                        {
                                            Match et = Regex.Match(m.Groups[2].ToString(), @"(\d+)(et)(\d+)", RegexOptions.IgnoreCase);
                                            if (et.Length > 3)
                                            {
                                                string new_name = m.Groups[2].ToString().Replace(et.ToString(), et.ToString().Replace("et", "ed"));
                                                File.Copy(dirpath + "\\" + m.Groups[2].ToString().Replace(".xml", "_final.xml"), dirpath + "\\" + new_name.Replace(".xml", "_final.xml"), true);
                                                if (ren == "")
                                                {
                                                    ren = m.Groups[2].ToString().Replace(".xml", "") + "," + new_name.Replace(".xml", "");
                                                }
                                                else
                                                {
                                                    ren = ren + "#" + m.Groups[2].ToString().Replace(".xml", "") + "," + new_name.Replace(".xml", "");
                                                }
                                                File.Delete(dirpath + "\\" + m.Groups[2].ToString().Replace(".xml", "_final.xml"));
                                            }
                                        }
                                    }
                                }
                            }
                            string[] repl = ren.Split('#');
                            foreach (Match m in mch)
                            {
                                if (File.Exists(dirpath + "\\" + m.Groups[2].ToString().Replace(".xml", "_final.xml")))
                                {
                                    string ftxt = File.ReadAllText(dirpath + "\\" + m.Groups[2].ToString().Replace(".xml", "_final.xml"));
                                    foreach (string rl in repl)
                                    {
                                        ftxt = ftxt.Replace(rl.Split(',')[0], rl.Split(',')[1]);
                                    }
                                    File.Delete(dirpath + "\\" + m.Groups[2].ToString().Replace(".xml", "_final.xml"));
                                    File.WriteAllText(dirpath + "\\" + m.Groups[2].ToString().Replace(".xml", "_final.xml"), ftxt);
                                }
                            }
                            string metatxt = File.ReadAllText(fxml);
                            foreach (string rl in repl)
                            {
                                string rep1 = rl.Split(',')[0].Substring(rl.Split(',')[0].IndexOf("et"));
                                string rep2 = rl.Split(',')[1].Substring(rl.Split(',')[1].IndexOf("ed"));
                                metatxt = metatxt.Replace(rep1, rep2);
                                if (File.Exists(dirpath + "\\" + rl.Split(',')[1].ToString() + "_final.xml"))
                                {
                                    string ftxt1 = File.ReadAllText(dirpath + "\\" + rl.Split(',')[1].ToString() + "_final.xml");
                                    foreach (string r2 in repl)
                                    {
                                        ftxt1 = ftxt1.Replace(r2.Split(',')[0], r2.Split(',')[1]);
                                    }
                                    File.Delete(dirpath + "\\" + rl.Split(',')[1].ToString() + "_final.xml");
                                    File.WriteAllText(dirpath + "\\" + rl.Split(',')[1].ToString() + "_final.xml", ftxt1);
                                }
                            }
                            File.Delete(fxml);
                            File.WriteAllText(fxml, metatxt);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
        public static void f_note(string dirpath)
        {
            string[] fnl_xml = Directory.GetFiles(dirpath);
            foreach (string fxml in fnl_xml)
            {
                if (fxml.EndsWith("metadata.xml") == false)
                {
                    string[] lines = File.ReadAllLines(fxml);
                    string txt = "";
                    foreach (string ln in lines)
                    {
                        if (txt == "")
                        {
                            txt = ln;
                        }
                        else
                        {
                            txt = txt + "\n" + ln;
                        }
                    }
                    //MatchCollection mch = Regex.Matches(txt, "(<fnote id=\")(.*?)(_)(.*?)(\">)", RegexOptions.IgnoreCase);
                    //if (mch.Count > 0)
                    //{
                    //    foreach (Match m in mch)
                    //    {
                    //        if (m.ToString().Contains("_final") == false)
                    //        {
                    //            string rep = m.ToString().Replace(m.Groups[2].ToString(), m.Groups[2].ToString() + "_final");
                    //            txt = txt.Replace(m.ToString(), rep);
                    //            txt = txt.Replace("refid=\"" + m.Groups[2].ToString() + "_" + m.Groups[4].ToString() + "\"", "refid=\"" + m.Groups[2].ToString() + "_final_" + m.Groups[4].ToString() + "\"");
                    //        }
                    //    }
                    //    try
                    //    {
                    //        File.Delete(fxml);
                    //        File.WriteAllText(fxml, txt);
                    //    }
                    //    catch
                    //    {
                    //    }
                    //}
                }
            }
        }
        public static void Removefinal()
        {
            string output = System.Configuration.ConfigurationSettings.AppSettings["output"].ToString();
            string metapath = output + file_name.Replace(" ", "") + "\\";
            XmlDocument xmlDoc = new XmlDocument();
            string[] final_xml = Directory.GetFiles(metapath);
            foreach (var myXML in final_xml)
            {
                if (myXML.EndsWith("_final.xml"))
                {
                    try
                    {
                        string txttt = File.ReadAllText(myXML);
                        txttt = replace_symbol(txttt);
                        MatchCollection mch = Regex.Matches(txttt, "(fic=\")(.*?)(\")");
                        foreach (Match m in mch)
                        {
                            txttt = txttt.Replace(m.ToString(), m.ToString().Replace("_final", ""));
                        }
                        Match mt = Regex.Match(txttt, @"(<\?xml )(.*?)(\?>)");
                        txttt = txttt.Replace(mt.ToString(), mt.ToString() + "\n<!-- Created by LNConversion " + System.Configuration.ConfigurationSettings.AppSettings["version"].ToString() + " -->");
                        File.WriteAllText(myXML.Replace("_final.xml", ".xml"), txttt);
                        File.Delete(myXML);
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void img_rename()
        {
            if (Directory.Exists(System.Configuration.ConfigurationSettings.AppSettings["img"].ToString()))
            {
                string[] imgs = Directory.GetFiles(System.Configuration.ConfigurationSettings.AppSettings["img"].ToString());
                string ipath = System.Configuration.ConfigurationSettings.AppSettings["img"].ToString();
                if (imgs.Length > 0)
                {
                    int count = 0;
                    string file_seq = "";
                    string[] ffls = Directory.GetFiles(System.Configuration.ConfigurationSettings.AppSettings["output"].ToString() + file_name.Replace(" ", ""));
                    int ct = 1;
                    foreach (string f in ffls)
                    {
                        bool p = true;
                        string ctr = "";
                        while (p == true)
                        {
                            if (ct.ToString().Length == 1)
                            {
                                ctr = "0000" + ct.ToString();
                            }
                            if (ct.ToString().Length == 2)
                            {
                                ctr = "000" + ct.ToString();
                            }
                            if (ct.ToString().Length == 3)
                            {
                                ctr = "00" + ct.ToString();
                            }
                            if (ct.ToString().Length == 4)
                            {
                                ctr = "0" + ct.ToString();
                            }
                            if (ct.ToString().Length == 5)
                            {
                                ctr = ct.ToString();
                            }
                            if (f.EndsWith(ctr + ".xml"))
                            {
                                if (file_seq == "")
                                {
                                    file_seq = f;
                                    p = false;
                                }
                                else
                                {
                                    file_seq = file_seq + "|" + f;
                                    p = false;
                                }
                            }
                            if (ct == 99999)
                            {
                                p = false;
                            }
                            ct = ct + 1;
                        }
                    }
                    string[] fls1 = file_seq.Split('|');
                    for (int i = 0; i < fls1.Length; i++)
                    {
                        string ftext = File.ReadAllText(fls1[i]);
                        MatchCollection mch1 = Regex.Matches(ftext, "(<apimag fic=\")(.*?)(.tif\")");
                        for (int j = 0; j < mch1.Count; j++)
                        {
                            if (count < imgs.Length)
                            {
                                count = count + 1;
                                string nd = "";
                                if (count.ToString().Length == 1)
                                {
                                    nd = "00" + count.ToString();
                                }
                                if (count.ToString().Length == 2)
                                {
                                    nd = "0" + count.ToString();
                                }
                                if (count.ToString().Length == 3)
                                {
                                    nd = count.ToString();
                                }
                                File.Copy(ipath + "\\" + file_name + "_" + nd + ".tif", ipath + "\\" + mch1[j].Groups[2].Value + ".tif");
                                File.Delete(ipath + "\\" + file_name + "_" + nd + ".tif");
                                //ftext = ftext.Replace(mch1[j].ToString(), "<apimag fic=\""+file_name+"_"+nd+".tif\"");
                                //count = count + 1;
                            }
                        }
                        //File.Delete(fls1[i]);
                        //File.WriteAllText(fls1[i], ftext);
                    }
                }
            }
        }
        public static void Entity_conversion(string dir_path)
        {
            string[] files = Directory.GetFiles(dir_path);
            string pl_exe = System.Configuration.ConfigurationSettings.AppSettings["entity"].ToString() + "EntityConversion_Setup\\utf8ToEntity.exe";
            foreach (string file in files)
            {
                if (file.EndsWith(".xml"))
                {
                    //File.Copy(file, file.Replace(".xml", ".txt"));
                    Process proc = new Process();
                    proc.StartInfo.WorkingDirectory = dir_path;
                    proc.StartInfo.FileName = pl_exe;
                    proc.StartInfo.Arguments = file;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.Start();
                    proc.WaitForExit();
                    //File.Delete(file);
                }
            }
            //string[] files1 = Directory.GetFiles(dir_path);
            //foreach (string file in files1)
            //{
            //    if (file.EndsWith("_out.xml"))
            //    {
            //        File.Copy(file, file.Replace("_out.xml", ".xml"));
            //        File.Delete(file);
            //    }
            //}
        }
    }
}


