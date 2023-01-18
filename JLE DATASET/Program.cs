using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.Drawing  ;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.AccessControl;
using System.Management;
using System.Management.Instrumentation;
using System.Threading;
using System.Xml;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

using NLMXML;

namespace JLEDATASET
{
    class Program
    {

        public static StringBuilder LogStr         = new StringBuilder("");
        public static StringBuilder IssuePubmedXML = new StringBuilder("");

        public static string ProcessPath = "";
        public static string JID   = "";
        public static string AID   = "";
        public static string VOL   = "";
        public static string ISSUE = "";

        public static string ISSUEMonth = "";
        public static string ISSUEYear  = "";

        public static string[] AIDList;
        public static bool     SpecialISSUE = false;
        public static bool     PubMedIssue  = false;
        public static bool     MedraOnly    = false;

        public static bool     PubMedIssueWithoutReplace  = false;
        //public static bool     qPubMedIssueWithoutReplace = false;

        public static string[] SpringerJID;
        public static string[] FreeArticle;
        public static string[]  FreeJID;
        public static string[] mEDRAJID;
        public static string[] PubmedNotRequired;

        static void Main(string[] args)
        {
            //CreateTar(@"D:\Input\JLE\TEST\lib-ito-2-2-reguliere.tar.gz", @"D:\Input\JLE\TEST\lib-ito-2-2-reguliere");
            if (NLMXML.Program.CheckFolderExistence())
            {
                NLMXML.Xml2NLM.GetAllMappingTextFile();
                GetMaaping();
            }
            else
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
                return;
            }

           
            if (args.Length == 2)
            {
                if (args[1].Equals("EPS2JPG", StringComparison.OrdinalIgnoreCase))
                {
                    EPS2JPG(args[0]);
                    return;
                }
                else if (args[1].Equals("PubMedIssueWithoutReplace", StringComparison.OrdinalIgnoreCase))
                {
                    PubMedIssueWithoutReplace = true;
                }
                else if (args[1].Equals("Medra", StringComparison.OrdinalIgnoreCase))
                {
                    MedraOnly = true;
                }
                else
                {
                    PubMedIssueWithoutReplace = true;
                }

                if (args[0].ToLower().IndexOf("issue") == -1)
                {
                    RecursiveProcess(args[0]);
                }
                else
                {
                    PubMedIssue = true;
                    MainProcess(args[0]);
                }
            }
            else if (args.Length==1)
            {
                if (args[0].ToLower().IndexOf("issue") == -1)
                {
                    RecursiveProcess(args[0]);
                }
                else
                {
                  /////////No need to check pubmed issue for special issue
                  //if (args[0].IndexOf("_SI",StringComparison.OrdinalIgnoreCase)==-1)
                        PubMedIssue = true;
                        MainProcess(args[0]);
                }
            }

            else
            {
                return;
            }
            // Crossref code starts

            //CreateCrossrefXML(args[0]);

        }

        private static void CreateNLM(string InputFolderName)
        {
            //if (NLMXML.Program.CheckFolderExistence())
            //{
            //    NLMXML.Xml2NLM.GetAllMappingTextFile();
            //    GetMaaping();
            //}
            //string[] FL = Directory.GetFiles(InputFolderName, "*.xml", SearchOption.AllDirectories);
            ///////////////No need to create nxml file in case of special issue
            //Console.WriteLine("Process start to create NXML file.");
            //for (int i = 0; i < FL.Length; i++)
            //{
            //    Console.WriteLine("Input Xml file :" + FL[i]);
            //    NLMXML.Program.ProcessStart(FL[i]);
            //}
            //Console.WriteLine("Process end to create NXML file.");
        }
        static public bool IsNumeric(string Expression)
        {

            if (Expression.Equals(""))
                return false;
            else if (Regex.Match(Expression, "[^0-9]").Value.Equals(""))
                return true;
            else
                return false;

            bool isNum = false;

            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        static void GetMaaping()
        {
            string ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string PubmedID = ExeLoc + "\\PubmedID.txt";
            if (File.Exists(PubmedID))
            {
                string[] PMID = File.ReadAllLines(PubmedID);
                foreach (string PID in PMID)
                {
                    if (PID.IndexOf("|") != -1)
                    {
                        string[] JIDAIDPMID = PID.Split('|');
                        
                        if (!PubmedInfo.PubMedID.ContainsKey(JIDAIDPMID[0]))
                        {
                            if (JIDAIDPMID[1].Equals(""))
                                PubmedInfo.PubMedID.Add(JIDAIDPMID[0], "");
                            else
                                PubmedInfo.PubMedID.Add(JIDAIDPMID[0], JIDAIDPMID[1]);
                        }
                        
                    }
                    else if (!PID.Equals(""))
                    {
                        PubmedInfo.PubMedID.Add(PID, "");
                    }
                    else
                    {
                    }
                }

            }
            if (File.Exists(ExeLoc + "\\mEDRAJID.txt"))
            {
                string mEDRAFile = ExeLoc + "\\mEDRAJID.txt";
                File.WriteAllText(mEDRAFile, File.ReadAllText(mEDRAFile).ToUpper());
                mEDRAJID = File.ReadAllLines(mEDRAFile);
            }

            if (File.Exists(ExeLoc + "\\FreeArticle.txt"))
                FreeArticle = File.ReadAllLines(ExeLoc + "\\FreeArticle.txt");

            if (File.Exists(ExeLoc + "\\SpringerJID.txt"))
                SpringerJID = File.ReadAllLines(ExeLoc + "\\SpringerJID.txt");


            
            if (File.Exists(ExeLoc + "\\FREEJID.txt"))
                FreeJID = File.ReadAllLines(ExeLoc + "\\FREEJID.txt");
            

            if (File.Exists(ExeLoc + "\\PubmedNotRequired.txt"))
            {
                File.WriteAllText(ExeLoc + "\\PubmedNotRequired.txt",File.ReadAllText(ExeLoc + "\\PubmedNotRequired.txt").Replace(" ",""));
                PubmedNotRequired = File.ReadAllLines(ExeLoc + "\\PubmedNotRequired.txt");
            }
        }
        static private void RecursiveProcess(string  ProcessDirPath)
        {
            
             string[]SubDir = Directory.GetDirectories(ProcessDirPath);
            if (SubDir.Length == 0)
            {
                MainProcess(ProcessDirPath);
                return;
            }
            foreach (string Dr in SubDir)
            {
                if (Directory.GetDirectories(Dr).Length == 0)
                {
                    MainProcess(Dr);
                }
                else
                {
                    RecursiveProcess(Dr);
                }
            }

        }
        static void MainProcess(string PrcsPath)
        {
            string   TempProcessPath;
            ProcessPath = PrcsPath;

            Console.WriteLine("Process start to check space.");
            if (!CheckDriveSize(ProcessPath))
            {
                return;
            }
            Console.WriteLine("Proccess result: OK." );

            Console.WriteLine("Process start to check input.");
            if (InputValidation(ProcessPath) && CheckImageSize (ProcessPath))
            {
                Console.WriteLine("Proccess result: OK.");

                Console.WriteLine("Process start to create temp folder.");
                CopyInputToTemp(ProcessPath, out TempProcessPath);
                Console.WriteLine("Proccess result: OK.");

                //////////Delete order file\\\\\\\\\\\\\
                string[] FL = Directory.GetFiles(TempProcessPath, "*issue*.xml", SearchOption.TopDirectoryOnly);
                if (FL.Length == 1)
                {
                    File.Delete(FL[0]);
                }
                //////////End\\\\\\\\\\\\\\\\\\

                /////////////////To create springer xml for EPD journal\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                
                ///////////////////////Issue information ///////////////////////////////////
                IssueDetails.InputPath = TempProcessPath;
                IssueDetails.JID    = JID;
                IssueDetails.Volume = VOL;
                IssueDetails.ISSUE  = ISSUE;
                IssueDetails.Month  = ISSUEMonth;
                IssueDetails.Year   = ISSUEYear;
                //////////////////////////////////////////////////////////
                //if (  JID.Equals("EJD",StringComparison.OrdinalIgnoreCase))
                if (Array.IndexOf(SpringerJID, JID.ToUpper()) != -1)
                {
                    IssueDetails.IssueArticleCount = Directory.GetFiles(TempProcessPath, "*.pdf", SearchOption.AllDirectories).Length.ToString();

                    if (!IssueDetails.ISSUE.Equals(""))
                    {
                        SpringerXml SpringerXmlObj = new SpringerXml(PrcsPath);
                        SpringerXmlObj.CreateSpringerXml();
                    }
                   // return;
                }

                if (!IssueDetails.ISSUE.Equals(""))
                {
                    bool Result = false;
                    if (Array.IndexOf(mEDRAJID, JID.ToUpper()) != -1)
                    {
                        Console.WriteLine("Process start to create mEDRA xml file.");
                        Medra MedraOBJ = new Medra(TempProcessPath, PrcsPath);
                        MedraOBJ.WriteMedraXML();
                        MedraOBJ.WriteCrossrefXML();
                        Console.WriteLine("Process end to create mEDRA xml file.");
                        Result = true;
                    }
                    if (MedraOnly )
                    {
                        if (Result == false)
                        {
                            Console.WriteLine( JID +  " does not exist in mEDRAJID file.");
                        }
                        return;
                    }
                    
                }
                /////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

                Console.WriteLine("Process start to create NXML file.");
                //Console.WriteLine("Process start to create html file.");
                ProcessStart(TempProcessPath);
                Console.WriteLine("Proccess result: OK.");

                //////////Start Delete xml file\\\\\\\\\\\\\
                FL = Directory.GetFiles(TempProcessPath, "*.xml", SearchOption.AllDirectories);
                foreach (string xmlFile in FL)
                {
                    if (!Path.GetFileName(xmlFile).Equals("index.xml"))
                        File.Delete(xmlFile);
                }
                //////////End\\\\\\\\\\\\\


                Console.WriteLine("Process start to create tar file.");
                CreateTarFile(TempProcessPath, ProcessPath);
                Console.WriteLine("Proccess result: OK.");

                if (Program.IssuePubmedXML.Length > 100)
                {
                    string PubmedFileName = Path.GetDirectoryName(ProcessPath) + "\\" + JID.ToLower() + "_" + Program.VOL + "_" + Program.ISSUE + ".xml";

                    string IssueXML = "";
                    IssueXML ="<?xml version=\"1.0\"?>" +
                    "<!DOCTYPE ArticleSet PUBLIC \"-//NLM//DTD PubMed 2.0//EN\" \"PubMed.dtd\">" +
                     "<ArticleSet>" + Environment.NewLine + Program.IssuePubmedXML.ToString() + Environment.NewLine + "</ArticleSet>";
                    File.WriteAllText(PubmedFileName, IssueXML);

                    PubmedInfo PubmedObj = new PubmedInfo();
                    if (!PubmedObj.PubMedXmlParsing(PubmedFileName))
                    {
                        string PubmedErrFileName = PubmedFileName.Replace(".xml", ".txt");
                        if (File.Exists(PubmedErrFileName))
                        {
                            File.Delete(PubmedErrFileName);
                        }
                        File.Move(PubmedFileName, PubmedErrFileName);
                    }
                }
            }
            else
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        static private bool ProcessStart(string InputFolderName)
        {
            string[] FL = Directory.GetFiles(InputFolderName, "*.xml", SearchOption.AllDirectories);

            foreach (string XML in FL)
            { 
                string  FileStr = File.ReadAllText(XML);
                FileStr = FileStr.Replace("<td:inline-figure><td:link locator=\"Picto_DVD\"/></td:inline-figure>", "");
                FileStr = FileStr.Replace("<td:link locator=\"Picto_DVD\"/>", "");
                FileStr = FileStr.Replace("<td:inline-figure></td:inline-figure>", "");

                FileStr = FileStr.Replace("<!--<RunningTitle&gt;", "<RunningTitle>");
                FileStr = FileStr.Replace("&lt;/RunningTitle>-->", "</RunningTitle>");

                //if (FileStr.IndexOf("DVD")!=-1)
                    File.WriteAllText(XML, FileStr);
            }
            
            /////////////No need to create nxml file in case of special issue
            if (SpecialISSUE == false)
            {
                Console.WriteLine("Process start to create NXML file.");
                for (int i = 0; i < FL.Length; i++)
                {
                    Console.WriteLine("Input Xml file :" + FL[i]);
                    //NLMXML.Program.ProcessStart(FL[i]);
                }
                Console.WriteLine("Process end to create NXML file.");
            }
            /////////////No need to create nxml file in case of special issue\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            Array.Sort(FL);

            if (FL.Length == 0)
            {
                Console.WriteLine("Article's xml file missing");
                Console.WriteLine("Please check this path:");
                Console.WriteLine(ProcessPath);
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            XmlInfo XmlInfoObj = new XmlInfo();
            for (int i = 0; i < FL.Length; i++)
            {
                Console.WriteLine("Process start to create HTML file.");
                Console.WriteLine("Input Xml file :" + FL[i]);
                XmlInfoObj.FilePath = FL[i];
                XmlInfoObj.IsChangedDtdPath = true;
                XmlInfoObj.ChangedDtdPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TD_JLE_Journal_v1.0.dtd";
                if (XmlInfoObj.LoadXml())
                {
                    Console.WriteLine("Please wait conversion is going on.....");

                    XmlInfoObj.MakeXHtml();

                    
                    //////////////////////////////If special issue then overwrite the html file
                    string HtmlFileName = Path.GetDirectoryName(FL[i]) + "\\index.htm";
              
                    

                    if (SpecialISSUE)// || ParaCount==1) 
                    {
                        StringBuilder HTMLStr= new StringBuilder ("");
                        HTMLStr.AppendLine("<html>");
                        HTMLStr.AppendLine("<head></head>");
                        HTMLStr.AppendLine("<body>");
                        HTMLStr.AppendLine("<p>Voir version pdf</p>");
                        HTMLStr.AppendLine("</body>");
                        HTMLStr.AppendLine("</html>");
                        File.WriteAllText(HtmlFileName, HTMLStr.ToString());
                    }

                    Console.WriteLine("File converted successfully.");


                    /////////////////////include jid aid with jpg file name..
                    string[] jpgFL = Directory.GetFiles(Path.GetDirectoryName(FL[i]), "*.jpg", SearchOption.TopDirectoryOnly);
                    for (int j = 0; j < jpgFL.Length; j++)
                    {
                      //File.Move(jpgFL[j], Path.GetDirectoryName(jpgFL[j]) + "\\jle" + JID.ToLower() + AID.ToLower() + "-" + Path.GetFileName(jpgFL[j]));
                        if (!Path.GetFileName(jpgFL[j]).StartsWith(JID.ToLower()) && (!Path.GetFileName(jpgFL[j]).StartsWith("JLE".ToLower())))
                             File.Move(jpgFL[j], Path.GetDirectoryName(jpgFL[j]) + "\\jle" + JID.ToLower() + AID.ToLower()  + Path.GetFileName(jpgFL[j]));
                        else
                             File.Move(jpgFL[j], Path.GetDirectoryName(jpgFL[j]) + "\\jle" + JID.ToLower() + AID.ToLower() + Path.GetFileName(jpgFL[j]));
                    }


                    jpgFL = Directory.GetFiles(Path.GetDirectoryName(FL[i]), "*.gif", SearchOption.TopDirectoryOnly);
                    for (int j = 0; j < jpgFL.Length; j++)
                    {
                        if (!Path.GetFileName(jpgFL[j]).StartsWith(JID.ToLower()) && (!Path.GetFileName(jpgFL[j]).StartsWith("JLE".ToLower())))
                            File.Move(jpgFL[j], Path.GetDirectoryName(jpgFL[j]) + "\\jle" + JID.ToLower() + AID.ToLower() + Path.GetFileName(jpgFL[j]));
                        else
                            File.Move(jpgFL[j], Path.GetDirectoryName(jpgFL[j]) + "\\jle" + JID.ToLower() + AID.ToLower() + Path.GetFileName(jpgFL[j]));
                    }



                    string[] PDFFL = Directory.GetFiles(Path.GetDirectoryName(FL[i]), "*.pdf", SearchOption.TopDirectoryOnly);
                    if (PDFFL.Length == 0)
                    {
                        Console.WriteLine("Article's pdf file missing");
                        Console.WriteLine("Please check this path:");
                        Console.WriteLine(ProcessPath);

                        Console.WriteLine("Press any key to exit.");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }

                    string[] XMLFL = Directory.GetFiles(Path.GetDirectoryName(FL[i]), "tx1.xml", SearchOption.TopDirectoryOnly);
                    if (XMLFL.Length == 0)
                    {
                        Console.WriteLine("Article's xml file missing");
                        Console.WriteLine("Please check this path:");
                        Console.WriteLine(ProcessPath);

                        Console.WriteLine("Press any key to exit.");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }


                    /////////////No need to check nxml file in case of special issue
                    if (SpecialISSUE == false)
                    {
                        string[] NXMLFL = Directory.GetFiles(Path.GetDirectoryName(FL[i]), "*.nxml", SearchOption.TopDirectoryOnly);
                        if (NXMLFL.Length == 0)
                        {
                            Console.WriteLine("Article's nxml file missing");
                            Console.WriteLine("Please check this path:");
                            Console.WriteLine(ProcessPath);

                            Console.WriteLine("Press any key to exit.");
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }

                    string[] ErrFL = Directory.GetFiles(Path.GetDirectoryName(FL[i]), "*.err", SearchOption.TopDirectoryOnly);
                    if (ErrFL.Length> 0)
                    {
                        Console.WriteLine("Article has error log file...");
                        Console.WriteLine("Please check this path:");
                        Console.WriteLine(ProcessPath);

                        Console.WriteLine("Press any key to exit.");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }

                    if (PDFFL.Length > 1)
                    {
                        Console.WriteLine("More than one Article's pdf file exist..");
                        Console.WriteLine("Please check this path:");
                        Console.WriteLine(ProcessPath);

                        Console.WriteLine("Press any key to exit.");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Xml file could not be loaded..");
                    Console.WriteLine("File conversion: Failed.");
                }
                
            }

           
            return true;

        }
        static private void CreateTarFile(string TempProcessPath, string ProcessPath)  
        {
            //./lib[journal_code][volume_number][issue_number][issue_type]/[page_start][page_end][type_of_document][order_of_article_in_page] /
             //////////Open  when  new datsaet live
            string[] HtmlFiles = Directory.GetFiles(TempProcessPath, "*.htm", SearchOption.AllDirectories);
            string[] IndexFiles= Directory.GetFiles(TempProcessPath, "index.xml", SearchOption.AllDirectories);

            foreach (string  xmlFile in HtmlFiles)
            {
                    File.Delete(xmlFile);
            }
            foreach (string xmlFile in IndexFiles)
            {
                File.Delete(xmlFile);
            }
            

            string TarFilePath = Path.GetDirectoryName(ProcessPath) + "\\" + Path.GetFileName(TempProcessPath) + ".tar.gz";
            CreateTar(TarFilePath, TempProcessPath);
            Directory.Delete(TempProcessPath,true);
        }

        /// <summary>
        /// Creates a GZipped Tar file from a source directory
        /// </summary>
        /// <param name="outputTarFilename">Output .tar.gz file</param>
        /// <param name="sourceDirectory">Input directory containing files to be added to GZipped tar archive</param>
        

        static private void CreateTar(string outputTarFilename, string sourceDirectory)
        {
                FileStream fs = new FileStream(outputTarFilename, FileMode.Create, FileAccess.Write, FileShare.None);
                Stream gzipStream = new GZipOutputStream(fs);
                TarArchive tarArchive = TarArchive.CreateOutputTarArchive(gzipStream);
                tarArchive.RootPath = Path.GetDirectoryName(sourceDirectory).Replace('\\','/');
                AddDirectoryFilesToTar(tarArchive, sourceDirectory, true);
                tarArchive.Close();
                fs.Close();
        }

        /// <summary>
        /// Recursively adds folders and files to archive
        /// </summary>
        /// <param name="tarArchive"></param>
        /// <param name="sourceDirectory"></param>
        /// <param name="recurse"></param>

        static private void AddDirectoryFilesToTar(TarArchive tarArchive, string sourceDirectory, bool recurse)
        {
            // Recursively add sub-folders
            //tarArchive.RootPath = sourceDirectory;
            if (recurse)
            {
                tarArchive.ApplyUserInfoOverrides = false;
                            string[] directories = Directory.GetDirectories(sourceDirectory);
                            foreach (string directory in directories)
                                AddDirectoryFilesToTar(tarArchive, directory, recurse);
            }
         // Add files
            string[] filenames = Directory.GetFiles(sourceDirectory);
            foreach (string filename in filenames)
            {
                TarEntry tarEntry = TarEntry.CreateEntryFromFile(filename);
                tarEntry.SetNames("","");
                tarArchive.WriteEntry(tarEntry, true);
                tarArchive.ApplyUserInfoOverrides = false;
            }
        }
        static private bool InputValidation (string InputFolderName)
        {
            /////////////If input folder has only single folder that's means to create single article dataset instead of issue.
            if (Directory.GetDirectories(InputFolderName).Length >= 1 && InputFolderName.IndexOf("issue",StringComparison.OrdinalIgnoreCase)!=-1)
            {
                //if (!CheckOrderFile(InputFolderName))
                //{
                //    Console.WriteLine("Press any key to exit.");
                //    Console.ReadLine();
                //    return false;
                //}
                if (!GetVolIssueFromPath(InputFolderName))
                {
                    return false;
                }

                ////////////NO need to check special issue
                //if (InputFolderName.IndexOf("_SI",StringComparison.OrdinalIgnoreCase) == -1)
                //{
                    if (!CheckGifFile(InputFolderName))
                    {
                        Console.WriteLine("Press any key to exit.");
                        Console.ReadLine();
                        return false;
                    }
                //}
            }
            else
            {
                /////////////Get JID AID from input xml file...........
                Xml2HTML.AOPArticle = true;
                GetJIDAID(InputFolderName);
            }

            if (MedraOnly)
            {
                return true;
            }

            ChekFolderSequence ChkSeqObj = new ChekFolderSequence();
            ChkSeqObj.DirPath = InputFolderName;
            ChkSeqObj.CheckFolderSeq();
            ChkSeqObj.CheckImageInXML();

            if (ChkSeqObj.ProcessLog.Length > 0)
            {
                Console.WriteLine(ChkSeqObj.ProcessLog);
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
                return true;
            }
            else
            {
                return true;
            }
        }
        static private bool GetJIDAID (string InputFolderName)      
        {
            string[] FL = Directory.GetFiles(InputFolderName, "*.xml", SearchOption.AllDirectories);
            if (FL.Length != 1) return false;

            string XmlFilePath = FL[0];
            string tempXMlPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\temp.xml";

            string ss = "xmlns=\"http://www.thomsondigital.com/xml/ja/dtd\" xmlns:td=\"http://www.thomsondigital.com/xml/common/dtd\"  xmlns:xlink=\"http://www.w3.org/1999/xlink\"  xmlns:tp=\"http://www.thomsondigital.com/xml/common/struct-bib/dtd\" ";


            

            StringBuilder xmlStr = new StringBuilder(File.ReadAllText(XmlFilePath));

            if (xmlStr.ToString().IndexOf("<article xmlns:ce") == -1)
            {
                xmlStr.Replace("<article", "<article " + ss);
                xmlStr.Replace("<sa:", "<");
                xmlStr.Replace("</sa:", "</");
                File.WriteAllText(XmlFilePath, xmlStr.ToString());
            }

            xmlStr.Replace("&", "#$#");
            xmlStr.Replace("given-name><ce:", "given-name> <ce:");

            xmlStr.Replace("<jid>PNV</jid>", "<jid>GPN</jid>");
            xmlStr.Replace("<jid>pnv</jid>", "<jid>GPN</jid>");

            xmlStr.Replace("<jid>MTE</jid>", "<jid>MTG</jid>");
            xmlStr.Replace("<jid>mte</jid>", "<jid>MTG</jid>");

            int ePos = xmlStr.ToString().IndexOf(".dtd");
            if (ePos > 0)
            {
                int sPos = xmlStr.ToString().LastIndexOf("\"", ePos);
                if (sPos > 0)
                {
                    ePos = ePos + 4;
                    sPos++;
                    string str = xmlStr.ToString().Substring(sPos, ePos - sPos);
                    if (!str.Equals(""))
                    {
                        string ChangedDtdPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\TD_JLE_Journal_v1.0.dtd";
                        xmlStr.Replace(str, ChangedDtdPath);
                    }
                }
            }
            File.WriteAllText(tempXMlPath, xmlStr.ToString());

            XmlDocument MyXmlDocument        = new XmlDocument();
            XmlReaderSettings ReaderSettings = new XmlReaderSettings();
            ReaderSettings.XmlResolver = new XmlUrlResolver();
            ReaderSettings.DtdProcessing = DtdProcessing.Parse;
            //ReaderSettings.ValidationType = ValidationType.DTD;
            ReaderSettings.IgnoreComments = false;
            ReaderSettings.IgnoreWhitespace = false;
             //ReaderSettings.ProhibitDtd       = false;
            XmlReader Reader                 = XmlReader.Create(tempXMlPath,ReaderSettings);
            try
            {
                MyXmlDocument.PreserveWhitespace = true;
                MyXmlDocument.XmlResolver = null;
                MyXmlDocument.Load(Reader);
                MyXmlDocument.InnerXml = MyXmlDocument.InnerXml.Replace("\r", "");

                XmlNode Node = MyXmlDocument.GetElementsByTagName("aid")[0];

                if (Node != null)
                {
                    AID = Node.InnerText;
                }

                Node = MyXmlDocument.GetElementsByTagName("jid")[0];

                if (Node != null)
                {
                    JID = Node.InnerText;
                }


            }
            catch (XmlException ex)
            {
                Console.WriteLine("Error message :" + ex.Message);
                return false;
            }
            finally
            {
                Reader.Close();
                File.Delete(tempXMlPath);
            }
            return true;
        }
        static private bool CheckOrderFile  (string InputFolderName)
        {
            string []FL= Directory.GetFiles(InputFolderName, "*.xml", SearchOption.TopDirectoryOnly);
            if (FL.Length == 1)
            {
                GetVolIssue(FL[0]);
                return true;
            }
            else if (FL.Length == 0)
            {
                Console.WriteLine("Order xml file does not exist.");
                return false;
            }
            else if (FL.Length > 1)
            {
                Console.WriteLine("More than one xml file exist on root.");
                Console.WriteLine("Only order's xml file should be exist on root.");
                return false;
            }
            return true;
        }
        static private bool CheckGifFile(string InputFolderName)
        {
            string[] FL = Directory.GetFiles(InputFolderName, "*.png", SearchOption.TopDirectoryOnly);
            if (FL.Length == 1)
            {
                //GetVolIssue(FL[0]);                           sommaire_parution.gif
                File.Move(FL[0],Path.GetDirectoryName(FL[0])+"\\sommaire_parution.png");
                return true;
            }
            else if (FL.Length == 0)
            {
                Console.WriteLine("Cover page's png file does not exist.");
                Console.WriteLine("Please press Y to continue.");
                if (Console.ReadKey().KeyChar == 'Y')
                    return true;
                else
                    return false;
            } 
            else if (FL.Length > 1)
            {
                Console.WriteLine("More than one png file exist on root.");
                Console.WriteLine("Only Cover page's png file should be exist on root.");
                return false;
            }
            return true;
        }
        static private bool GetVolIssueFromPath(string InputPath)   
        {
            bool Result = true;

            InputPath = Path.GetFileName(InputPath);
            if (!InputPath.StartsWith("issue", StringComparison.OrdinalIgnoreCase))
                Result = false;

            string[] StrPart = InputPath.Split('_');
            if (StrPart.Length != 4)
                Result = false;
            else
            {
                JID   = StrPart[1];
                VOL   = StrPart[2];
                ISSUE = StrPart[3];

                if (ISSUE.IndexOf("(") == -1)
                    Result = false;
                if (ISSUE.IndexOf(")") == -1)
                    Result = false;
            }
            Xml2HTML.AOPArticle = false;

            if (Result == false)
            {
                Console.WriteLine("Folder name must be follow below squence");
                Console.WriteLine("ISSUE_JID_VOLNO_ISSUENO(Month-Year)");
                return false;
            }
            else
            {
                ////////////Please do not change execution sequence
                  string MMYY   = ISSUE.Substring(ISSUE.IndexOf('(')).TrimEnd(')');
                string[] MMYYYY = MMYY.Split('-');
                ISSUEYear                 = MMYYYY[MMYYYY.Length - 1];
                MMYYYY[MMYYYY.Length - 1] = "";
                ISSUEMonth = string.Join("-", MMYYYY).Trim(new char[]{'-' , '('}); 
                ISSUE = ISSUE.Substring(0,ISSUE.IndexOf('('));
                return true;
            }
        }
        static private bool GetVolIssue(string XmlFilePath)    
        { 
            string tempXMlPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\temp.xml";
           
                StringBuilder xmlStr = new StringBuilder(File.ReadAllText(XmlFilePath));
                xmlStr.Replace("&", "#$#");
                xmlStr.Replace("<jid>PNV</jid>", "<jid>GPN</jid>");
                xmlStr.Replace("<jid>pnv</jid>", "<jid>GPN</jid>");

                xmlStr.Replace("<jid>MTE</jid>", "<jid>MTG</jid>");
                xmlStr.Replace("<jid>mte</jid>", "<jid>MTG</jid>");


                    int ePos = xmlStr.ToString().IndexOf(".dtd");
                    if (ePos > 0)
                    {
                        int sPos = xmlStr.ToString().LastIndexOf("\"", ePos);
                        if (sPos > 0)
                        {
                            ePos = ePos + 4;
                            sPos++;
                            string str = xmlStr.ToString().Substring(sPos, ePos - sPos);
                            if (!str.Equals(""))
                            {
                                xmlStr.Replace(str, "");
                            }
                        }
                    }
            File.WriteAllText(tempXMlPath, xmlStr.ToString());
            XmlReader Reader = XmlReader.Create(tempXMlPath);
            try
            {
                XmlDocument MyXmlDocument = new XmlDocument();
                MyXmlDocument.PreserveWhitespace = true;
                MyXmlDocument.Load(Reader);
                MyXmlDocument.InnerXml = MyXmlDocument.InnerXml.Replace("\r", "");

                XmlNode Node= MyXmlDocument.DocumentElement.SelectSingleNode("//vol-from");

                if (Node != null)
                {
                    VOL = Node.InnerText;
                }

                Node = MyXmlDocument.SelectSingleNode("//iss-from");

                if (Node != null)
                {
                    ISSUE = Node.InnerText;
                }

                Node = MyXmlDocument.SelectSingleNode("//jid");

                if (Node != null)
                {
                    JID = Node.InnerText;
                }

                
            }
            catch (XmlException ex)
            {
                Console.WriteLine("Error message :" + ex.Data);
                return false;
            }
            finally
            {
                Reader.Close();
                File.Delete(tempXMlPath);
            }
            return true;
        }
        static private bool CheckDriveSize(string ChkPath)        
        {

            //////////////Get input directory size
            long DirSize = 0;
            string[] FL = Directory.GetFiles(ChkPath, "*.*", SearchOption.AllDirectories);
            foreach (string InnerFile in FL)
            {
                DirSize += new FileInfo(InnerFile).Length;
            }
            DirSize = (DirSize / 1024) / 1024;

            //////////////Get drive size in which input directory exist..
            string DrvName         = Path.GetPathRoot(ChkPath);
            DriveInfo DriveInfoObj = new DriveInfo(DrvName);
            //////////////Convert byte to KB and then into MB;
            long MB = (DriveInfoObj.TotalFreeSpace/1024)/1024;

            if (MB > DirSize * 2) return true;
            else
            {
                Console.WriteLine("Due to lack of space in drive '" + DrvName + "' Process can't be executed.");
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
                return false;
            }
        }
        static private void CopyInputToTemp (string InputFolderName , out string Dst)
        {
            //libbdc9511reguliere.tar.gz
            //lib[journal_code]­[volume_number]­[issue_number]­[issue_type].tar.gz
            //lib-bdc-96-9-reguliere
            string[] FL;
            string DestPath = "";
            FileInfo fInfo;

            if (Directory.GetDirectories(InputFolderName).Length < 2 && InputFolderName.IndexOf("issue",StringComparison.OrdinalIgnoreCase)==-1)
            {
                ///////////////For single article
                Dst = Path.GetPathRoot(InputFolderName) + "\\aop-" + JID + "-" + AID;
                Dst = Dst.ToLower();
                if (Directory.Exists(Dst))
                {
                    Directory.Delete(Dst, true);
                    Directory.CreateDirectory(Dst);
                }
                else
                    Directory.CreateDirectory(Dst);

                FL = Directory.GetFiles(InputFolderName, "*.*", SearchOption.AllDirectories); ///////////FileList
                foreach (string fname in FL)
                {
                    fInfo = new FileInfo(fname);


                    if ( fInfo.Name.IndexOf("DVD",StringComparison.OrdinalIgnoreCase) != -1)
                    {
                    }
                    else if (fInfo.Attributes.ToString().IndexOf("Hidden") == -1 && ".xml#.pdf#.jpg#.gif#.png".IndexOf(fInfo.Extension) != -1)
                    {
                        DestPath = Dst + "\\" + Path.GetFileName(fname);
                        File.Copy(fname, DestPath);
                    }
                    else
                        Console.WriteLine("Hidden File :" + fname);
                }
            }
            else
            {
                ///////////////For Issue
                ISSUE = ISSUE.ToUpper();


                if (ISSUE.StartsWith("NS"))
                {
                    string ISS = ISSUE.Replace("NS", "");
                    Dst = Path.GetPathRoot(InputFolderName) + "\\lib-" + JID + "-" + VOL + "-" + ISS + "-num_special";
                }
                else if (ISSUE.StartsWith("SI"))
                {
                    SpecialISSUE = true;
                    ISSUE = ISSUE.Replace("SI", "");
                    Dst = Path.GetPathRoot(InputFolderName) + "\\lib-" + JID + "-" + VOL + "-" + ISSUE + "-hors_serie";
                }
                else if (ISSUE.StartsWith("S"))
                {
                    string ISSUEInPath = ISSUE.Replace("S", "");

                    Dst = Path.GetPathRoot(InputFolderName) + "\\lib-" + JID + "-" + VOL + "-" + ISSUEInPath + "-supplement";
                }
                else if (ISSUE.Contains("-"))
                {
                    Dst = "";
                    string[] SplitIssue = ISSUE.Split('-');
                    if (SplitIssue.Length > 0)
                    {
                        Dst = Path.GetPathRoot(InputFolderName) + "\\lib-" + JID + "-" + VOL + "-" + SplitIssue[0] + "-num_double";
                    }

                }
                else
                    Dst = Path.GetPathRoot(InputFolderName) + "\\lib-" + JID + "-" + VOL + "-" + ISSUE + "-reguliere";

                //////////////////////////////////////For pubmed xml for issue
                PubMedIssue = true;
                //////////////////////////////////////For pubmed xml for issue

                Dst = Dst.ToLower();
                if (Directory.Exists(Dst))
                    Directory.Delete(Dst, true);
                else
                    Directory.CreateDirectory(Dst);

                
                string[] SubDL = Directory.GetDirectories(InputFolderName); ///////////SubDirectorylist
                foreach (string SubDir in SubDL)
                {
                    string[] Arr = Path.GetFileName(SubDir).Split('-');
                    int PgNo;
                    if (Arr.Length > 0 && Int32.TryParse(Arr[0],out PgNo) && Int32.TryParse(Arr[1],out PgNo))
                    {
                    }
                    else
                    {
                        string SubDirName = SubDir.Substring(  SubDir.LastIndexOf('\\')+1);
                        string NewDirName = Path.GetDirectoryName(SubDir) + "\\" + SubDirName.Replace("-", "&hyphen;");
                        if (!SubDir.Equals(NewDirName, StringComparison.OrdinalIgnoreCase))
                            Directory.Move(SubDir, NewDirName);
                    }
                }

                SubDL = Directory.GetDirectories(InputFolderName,"*-*",SearchOption.AllDirectories); ///////////SubDirectorylist

                string RefinePageRange = "";
                string[] PageRange = null;
                foreach (string SubDir in SubDL)
                {
                    PageRange = Path.GetFileName(SubDir).Split('-');
                    //RefinePageRange = PageRange[0].TrimStart('0').PadLeft(4, '0') + "-" + PageRange[1].TrimStart('0').PadLeft(4, '0');
                    RefinePageRange = PageRange[0].TrimStart('0') + "-" + PageRange[1].TrimStart('0'); 
                    if( PageRange.Length==2)
                        DestPath = Dst + "\\" + RefinePageRange + "-Article-1";
                    else if (PageRange.Length == 3)
                        DestPath = Dst + "\\" + RefinePageRange + "-Article-2";

                    copyDirectory(SubDir, DestPath);
                }
                FL = Directory.GetFiles(InputFolderName); ///////////FileList
                foreach (string fname in FL)
                {
                    fInfo = new FileInfo(fname);

                    if (fInfo.Name.IndexOf("DVD", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                    }
                    else if (fInfo.Attributes.ToString().IndexOf("Hidden") == -1 && ".xml#.pdf#.jpg#.gif#.png".IndexOf(fInfo.Extension) != -1)
                    {
                        DestPath = Dst + "\\" + Path.GetFileName(fname);
                        File.Copy(fname, DestPath);
                    }
                    else
                    {
                        Console.WriteLine("Hidden File :" + fname);
                    }
                }
            }

            ///////////Getting all pdf file list in folders
            FL = Directory.GetFiles(Dst, "*.pdf",SearchOption.AllDirectories);
            /////////////////////********Sort to getting aid list according to page sequence******************\\\\\\\\\\\\\\\\\\\
            Array.Sort(FL);
            AIDList = new string[FL.Length];
            /////////////////////********To store aid list according to page sequence******************\\\\\\\\\\\\\\\\\\\

            /////////////////////All pdf files must be index.pdf
            for (int i = 0; i < FL.Length; i++)
            {
                AIDList[i] = Path.GetFileNameWithoutExtension(FL[i]);
                File.Move(FL[i], Path.GetDirectoryName(FL[i])+"\\index.pdf");
            }
        }

        static private void copyDirectory   (string Src, string Dst)
        {
            

            String[] Files;
            if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
                Dst += Path.DirectorySeparatorChar;

            if (!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
            Files = Directory.GetFileSystemEntries(Src);
            foreach (string Element in Files)
            {
                // Sub directories
                if (Directory.Exists(Element))
                    copyDirectory(Element, Dst + Path.GetFileName(Element));
                // Files in directory
                else
                {
                    
                    FileInfo fInfo = new FileInfo(Element);
                    if (fInfo.Name.IndexOf("DVD", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                    }
                    else if (fInfo.Attributes.ToString().IndexOf("Hidden") == -1 && ".xml#.pdf#.jpg#.gif#.png".IndexOf(fInfo.Extension)!=-1)
                        File.Copy(Element, Dst + Path.GetFileName(Element), true);
                    else
                        Console.WriteLine("Hidden File :" + Element);
                }
            }
        }
        static private void SetFileAttribute(string dirPath)        
        {
            DirectoryInfo     myDirectoryInfo     = new DirectoryInfo(dirPath);
            DirectorySecurity myDirectorySecurity = myDirectoryInfo.GetAccessControl();

            FileSystemAccessRule FSAR = new FileSystemAccessRule("", FileSystemRights.FullControl, AccessControlType.Allow);
            
            //myDirectorySecurity.AddAccessRule(new FileSystemAccessRule("User",FileSystemRights.Read,PropagationFlags.None ,AccessControlType.Allow ));
            myDirectoryInfo.SetAccessControl(myDirectorySecurity);
        }
        static private bool CheckImageSize(string InputPath)        
        {
            bool ErrorFound    = false;
            string[] JpgFiles  = Directory.GetFiles(InputPath,"*.jpg");
            Bitmap   BitmapOBJ = null;

            foreach (string JpgFile in JpgFiles)
            {
                BitmapOBJ = new Bitmap(JpgFile);
                if (Path.GetFileNameWithoutExtension(JpgFile).StartsWith("gr", StringComparison.OrdinalIgnoreCase))
                {
                    if (BitmapOBJ.Width > 700)
                    {
                        Console.WriteLine("Error: Jpg's file size must not be exceed 700 pixels.");
                        Console.WriteLine("Check file name : " + JpgFile);
                        Console.WriteLine("Width : "           + BitmapOBJ.Width);
                        ErrorFound = true;
                    }
                }
                else if (Path.GetFileNameWithoutExtension(JpgFile).StartsWith("fx", StringComparison.OrdinalIgnoreCase))
                {
                    if (BitmapOBJ.Width == 26 && BitmapOBJ.Height == 26) { }
                    else if (BitmapOBJ.Width <800) { }
                    else
                    {
                        Console.WriteLine("Error: FX's file size must not be exceed 26 pixels.");
                        Console.WriteLine("Check file name : " + JpgFile);
                        Console.WriteLine("Width : "           + BitmapOBJ.Width);
                        ErrorFound = true;
                    }
                }
            }

            string[] gifFiles = Directory.GetFiles(InputPath, "*.png");
            if (gifFiles.Length == 1 && ! Path.GetFileNameWithoutExtension(gifFiles[0]).StartsWith("si"))
            {
                BitmapOBJ = new Bitmap(gifFiles[0]);
                if (BitmapOBJ.Width > 1000)
                {
                    Console.WriteLine("Error: png's file width must not be exceed 1000 pixels.");
                    Console.WriteLine("Check file name : " + gifFiles[0]);
                    Console.WriteLine("Width : " + BitmapOBJ.Width);
                    if (BitmapOBJ.Width == 300) { }
                    else
                        ErrorFound = true;
                }
                if (BitmapOBJ.Height > 1000)
                {
                    Console.WriteLine("Error: png's file height must not be exceed 1000 pixels.");
                    Console.WriteLine("Check file name : " + gifFiles[0]);
                    Console.WriteLine("Height : "          + BitmapOBJ.Height);
                    ErrorFound = true;
                }

            }

            if (ErrorFound)
                return false;
            else
                return true;

        }
        static private void ConvertTiff2JPG()                       
        { 
            string []FL= Directory.GetFiles( @"C:\Test");
            foreach (string filename in FL)
            {
                Image bm = Image.FromFile(filename);
                bm.Save( "c:\\2.png", ImageFormat.Png);
           }
        }
        static public  string Pubmed(string term)                   
        {
            //string term = "21382788"; //"Skin cancers and other cutaneous diseases in renal transplant recipients: a single Italian center observational study";

                string PubMed_XML = "";
                string responseFromServer = "";
            try
            {
                //ServicePointManager.MaxServicePointIdleTime = 1000;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                WebRequest request = WebRequest.Create("https://www.ebi.ac.uk/europepmc/webservices/rest/search?query="+term+"&format=xml");
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                var res = response.GetResponseStream();

                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    Console.WriteLine(responseFromServer);
                    reader.Close();
                }

                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(responseFromServer);

                //XmlNodeList resNodeList = doc.GetElementsByTagName("title");
                //if (resNodeList.Count > 0)
                //{
                //    foreach (XmlNode node in resNodeList)
                //    {
                //        if (!node.InnerText.Contains(term))
                //        {
                //            //delete tag which has not this title

                //        }
                //        else
                //        {
                //            PubMed_XML = node.ParentNode.ParentNode.InnerXml;
                //        }
                //    }
                //}
                PubMed_XML = responseFromServer;
                return PubMed_XML;

                
                //string url = @"https://pubmed.ncbi.nlm.nih.gov/";  /* This is PubMed site from wher we are extrecting and matching citaions */
                //WebClient HttpClient = new WebClient();
                //HttpClient.BaseAddress = "";
                //HttpClient.QueryString.Add("term", term);
                //HttpClient.QueryString.Add("report", "XML");
                //HttpClient.QueryString.Add("format", "text");
                //HttpClient.Encoding = Encoding.UTF8;
                ////HttpClient.Timeout =
                //PubMed_XML = HttpClient.DownloadString(url);
                //PubMed_XML = PubMed_XML.Replace("&lt;", "<").Replace("&gt;", ">");


                //var HttpClient = new WebClientEx();
                //HttpClient.Timeout = 9000000; // Daft timeout period
                // HttpClient.BaseAddress = "";
                // HttpClient.QueryString.Add("term", term);
                // HttpClient.QueryString.Add("report", "XML");
                // HttpClient.QueryString.Add("format", "text");
                // HttpClient.Encoding = Encoding.UTF8;
                // PubMed_XML = HttpClient.DownloadString(url);
                // PubMed_XML = PubMed_XML.Replace("&lt;", "<").Replace("&gt;", ">");
                // return PubMed_XML;
            }
            catch (Exception ex)
            {
                return PubMed_XML;
            }
        }

        public class WebClientEx : WebClient
        {
            public int Timeout { get; set; }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address);
                request.Timeout = Timeout;
                return request;
            }
        }

        static private void EPS2JPG(string InputPath)
        {
            string []EpsFiles = Directory.GetFiles(InputPath,"*.eps",SearchOption.AllDirectories);
            try
            {
                string ExeLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string EPS2JPGPath = ExeLoc + "\\eps2jpg.bat";
                Process myProcess  = new Process();
                myProcess.StartInfo.FileName = EPS2JPGPath;
                
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

                string JPGFile = string.Empty;
                for (int i=0; i<EpsFiles.Length;i++)
                {
                    JPGFile = Path.GetDirectoryName(EpsFiles[i]) + "\\" + Path.GetFileNameWithoutExtension(EpsFiles[i]) + ".jpg";
                    myProcess.StartInfo.Arguments = "\"" + EpsFiles[i]+ "\" \"" + JPGFile+ "\"";
                    myProcess.Start();
                    myProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + Environment.NewLine);
            }
        }

        static void CreateCrossrefXML(string inputPath)
        {
            // Read medra xml
            XmlTextWriter textWriter;


            string FileName = "CrossRef_" + IssueDetails.JID + "_" + IssueDetails.Volume + "_" + IssueDetails.ISSUE + ".xml";
            //JID_VOLUME_ISSUE
            string CrossXml = Path.GetDirectoryName(inputPath) + "\\" + FileName;
            textWriter = new XmlTextWriter(CrossXml, Encoding.UTF8);

            textWriter.Indentation = 1;
            textWriter.IndentChar = '\t';
            textWriter.Formatting = Formatting.Indented;


            textWriter.WriteStartDocument();

            textWriter.WriteStartElement("ONIXDOISerialArticleWorkRegistrationMessage");
            textWriter.WriteAttributeString("xmlns", "http://www.editeur.org/onix/DOIMetadata/1.1");
            textWriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            textWriter.WriteAttributeString("xsi:schemaLocation", "http://www.editeur.org/onix/DOIMetadata/1.1 http://www.medra.org/schema/onix/DOIMetadata/1.1/ONIX_DOIMetadata_1.1.xsd");
            //textWriter.WriteAttributeString("xsi:noNamespaceSchemaLocation", "ONIX_DOIMetadata_1.1.xsd");
            string CurrentDate = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0');
            textWriter.WriteStartElement("Header");
            textWriter.WriteElementString("FromCompany", "Thomson Digital (Mauritius) ltd");
            textWriter.WriteElementString("FromEmail", "veema.mohun@thomsondigital.com");
            textWriter.WriteElementString("ToCompany", "Crossref");
            textWriter.WriteElementString("SentDate", CurrentDate);
            textWriter.WriteEndElement();

            //string[] XmlFiles = Directory.GetFiles(_InPutPath, "*.xml", SearchOption.AllDirectories);
            //Array.Sort(XmlFiles);

            //foreach (string XMLFile in XmlFiles)
            //{
            //    Console.WriteLine("Process xml file :" + XMLFile);
            //    WriteDOISerialArticleWork(XMLFile);
            //}

            textWriter.WriteEndDocument();

            textWriter.Flush();
            textWriter.Close();

            //FinalFilteration(MedraXml);

            //Console.WriteLine("Validation start of mEDRA xml.");
            //WriteErrorLog(MedraXml);

        }
    }

    public class TiffImageConverter
    {
     #region Methods

        #region ConvertTo
        /// <summary>Converts the specified image into a specified format with the specified codec and parameters.</summary>
        /// <param name="imgStream">The stream of the image to convert</param>
        /// <param name="codecInfo">The codec info to use</param>
        /// <param name="encoderParams">The encoder params to use</param>
        /// <returns>An Image with formatted data if successful; otherwise null</returns>
        public static Image ConvertTo(Stream imgStream, ImageCodecInfo encoder, EncoderParameters encoderParams)
        {
            Image retVal = null;
            Stream retStream = new MemoryStream();

            using (Image img = Image.FromStream(imgStream, false, false))
            {
                img.Save(retStream, encoder, encoderParams);
                retStream.Flush();
            }
            retVal = Image.FromStream(retStream, true, true);
            retStream.Close();
            return retVal;
        }
        #endregion ConvertTo

        #region ConvertTo
        /// <summary>Converts the specified image into a specified format</summary>
        /// <param name="imgStream">The stream of the image to convert</param>
        /// <param name="format">The desired image format.</param>
        /// <returns>An Image with formated data if successful; otherwise null</returns>
        public static Image ConvertTo(Stream imgStream, ImageFormat format)
        {
            Image retVal = null;
            Stream retStream = new MemoryStream();


            using (Image img = Image.FromStream(imgStream, true, true))
            {
                img.Save(retStream, format);
                retStream.Flush();
            }
            retVal = Image.FromStream(retStream, true, true);
            retStream.Close();
            return retVal;
        }
        #endregion ConvertTo

        #region ConvertToTIFF
        /// <summary>Converts the specified image into a TIFF format</summary>
        /// <param name="fileName">The file path of the image to convert</param>
        /// <returns>An Image with TIFF data if successful; otherwise null</returns>
        public static Image ConvertToTIFF(string fileName, bool useLZW)
        {
            Image retVal = null;
            using (FileStream fs = File.OpenRead(fileName))
            {
                retVal = ConvertToTIFF(fs, useLZW);
                fs.Close();
            }

            return retVal;
        }
        #endregion ConvertToTIFF

        #region ConvertToTIFF
        /// <summary>               Converts the specified image into a TIFF format      </summary>
        /// <param name="imgStream">The stream of the image to convert                   </param>
        /// <param name="useLZW">   True to compress                                     </param>
        /// <returns>               An Image with TIFF data if successful; otherwise null</returns>
        public static Image ConvertToTIFF(Stream imgStream, bool useLZW)
        {
            if (useLZW)
            {
                ImageCodecInfo encoder = null;
                EncoderParameters encoderParams = null;
                GetLZWParams(out  encoder, out encoderParams);

                return ConvertTo(imgStream, encoder, encoderParams);
            }
            else { return ConvertTo(imgStream, ImageFormat.Tiff); }
        }
        #endregion ConvertToTIFF

        #region GetEncoderInfo
        /// <summary>Gets a codec by mimetype.</summary>
        /// <param name="mimeType">The mime type to search for.</param>
        /// <returns>An <see cref="T:ImageCodecInfo" /> object, or null.</returns>
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo retVal = null;
            string encMimeType = string.Empty;
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            for (int j = 0; j < encoders.Length; ++j)
            {
                encMimeType = encoders[j].MimeType;
                if (!string.IsNullOrEmpty(encMimeType) &&
                    encMimeType.Equals(mimeType, StringComparison.OrdinalIgnoreCase))
                {
                    retVal = encoders[j];
                    break;
                }
            }
            return retVal;
        }
        #endregion GetEncoderInfo

        #region GetLZWParams
        /// <summary>Gets the codec and encoder parameters for LZW TIFF compression.</summary>
        /// <param name="codecInfo">The <see cref="T:ImageCodecInfo" /> for TIFF.</param>
        /// <param name="encoderParams">The <see cref="T:EncoderParameters" /> for LZW compression.</param>
        private static void GetLZWParams(out ImageCodecInfo codecInfo, out EncoderParameters encoderParams)
        {
            System.Drawing.Imaging.Encoder encoder =System.Drawing.Imaging.Encoder.Compression;
            EncoderParameter encoderParam = new EncoderParameter(encoder, (long)EncoderValue.CompressionLZW);
            codecInfo              = GetEncoderInfo("image/tiff");
            encoderParams          = new EncoderParameters(1);
            encoderParams.Param[0] = encoderParam;
        }
        #endregion GetLZWParams

     #endregion Methods
    }
}



