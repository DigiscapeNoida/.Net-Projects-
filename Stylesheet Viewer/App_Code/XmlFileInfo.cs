using System;
using System.IO.Compression;
using System.Web.Configuration;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Text;
using System.Data;
using System.Configuration;
using Microsoft.XmlDiffPatch;
/// <summary>
/// Summary description for FileInfo
/// </summary>
    
    public class XmlFileInfo
    {
        System.Collections.ArrayList _Log = new System.Collections.ArrayList();
        string _DirectoryPath = "";
        string _FileExtension = "";
        string _ZipFilePath   = "";
        string _TargetFolderPath ="";
        string _BackupFolderPath ="";
           int _XmlFilesCount = 0;
           int _TextFilesCount = 0;
           int _TotalFilesCount = 0;

        public int TotalFilesCount
        {
            get
            {
                return _TotalFilesCount;
            }
        }
        public int XmlFilesCount
        {
            get
            {
                return _XmlFilesCount;
            }
        }
        public int TextFilesCount
        {
            get
            {
                return _TextFilesCount;
            }
        }
        public System.Collections.ArrayList  Log
        {
            get { return _Log; }
        }

        public string BackupFolderPath
        {
            set
            {
                _BackupFolderPath = value;
            }
            get
            {
                return _BackupFolderPath;
            }
       }

        public string TargetFolderPath
        {
            set 
            {
                _TargetFolderPath = value;
            }
            get
            {
                return _TargetFolderPath;
            }
        }

        public XmlFileInfo(string ZipFilePath, string Targetfolderpath, string oldfolderpath) 
       {
           _ZipFilePath = ZipFilePath;
           _TargetFolderPath = Targetfolderpath;
           _BackupFolderPath = oldfolderpath;
       }
        private void Extract(string zipFileName, string destinationPath)
        {

            ZipFile.ExtractToDirectory(zipFileName, destinationPath);
           
               /*
            FileInfo fInf           = new FileInfo(zipFileName);
            ZipFile zipfile         = new ZipFile(zipFileName);
            List<ZipEntry> zipFiles = GetZipFiles(zipfile);

            foreach (ZipEntry zipFile in zipFiles)
            {
                if (!zipFile.isDirectory())
                {
                    InputStream s = zipfile.getInputStream(zipFile);
                    try
                    {
                        Directory.CreateDirectory(destinationPath + "\\" + Path.GetDirectoryName(zipFile.getName()));
                        FileOutputStream dest = new FileOutputStream(Path.Combine(destinationPath + "\\" + Path.GetDirectoryName(zipFile.getName()), Path.GetFileName(zipFile.getName())));
                        try
                        {
                            int len = 0;
                            sbyte[] buffer = new sbyte[8000];
                            while ((len = s.read(buffer)) >= 0)
                            {
                                dest.write(buffer, 0, len);
                            }
                        }
                        finally
                        {
                            dest.close();
                        }
                    }
                    finally
                    {
                        s.close();
                    }
                }
            }
            */
        }

       

        private int GetNextFileNo(out int MinNo, out int MaxNo)
        {
            List<int> File_No = new List<int>();
            //string[] FileLIst = Directory.GetFiles(_DirectoryPath, "*" + _FileExtension);
            string[] FileLIst = Directory.GetFiles(_DirectoryPath, "*.xml" );
            if (FileLIst.Length == 0)
            {
                MinNo = 0;
                MaxNo = 0;
                return 0;
            }
            else
            {
                int SeqNo = 0;
                MatchCollection MatchCol;
                foreach (string fName in FileLIst)
                {
                    MatchCol = Regex.Matches(fName, "[0-9]{1,}");
                    if (MatchCol.Count > 0)
                    {
                        SeqNo = Int32.Parse(MatchCol[MatchCol.Count-1].Value);
                        File_No.Add(SeqNo);
                    }
                }
                File_No.Sort();
                MinNo = File_No[0];
                File_No.Reverse();
            }

            MaxNo = File_No[0];
            File_No[0]++;
            return File_No[0];
        }

        private string GetFileName(string fName, out int MinNo, out int MaxNo)
        {

            return Path.GetFileNameWithoutExtension(fName) + "_" + GetNextFileNo(out MinNo,out MaxNo).ToString() + _FileExtension;
        }

       public bool SaveFileInZip()
       { 
           if (!System.IO.File.Exists(_ZipFilePath ))
               return false;
           if (!Directory.Exists(_TargetFolderPath))
               return false;


           string DesPath = GetTodayDir() +"\\"+ DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond;

           Extract(_ZipFilePath,DesPath);
           
           _TotalFilesCount = Directory.GetFiles(DesPath,"*.*",SearchOption.AllDirectories).Length;
           string [] FL     = Directory.GetFiles(DesPath,"*.xml",SearchOption.AllDirectories);

           foreach (string XmlFile in FL)
           {
               System.IO.File.WriteAllText(XmlFile, System.IO.File.ReadAllText(XmlFile).Replace("&amp;#x0026;#x", "&#x").Replace("&amp;#x", "&#x").Replace("&amp;amp;", "&amp;"));
           }

           _XmlFilesCount = FL.Length;
           string JID="";
           string NewFileName = "";
           string PrvusFileName = "";
           int MinNo,MaxNo;

           foreach(string fn in FL)
           {
               if (Path.GetFileNameWithoutExtension(fn).IndexOf("-jss")!=-1)
               {
                    JID = Path.GetFileNameWithoutExtension(fn).Replace("-jss","");
                   _DirectoryPath = _TargetFolderPath + "\\" + JID;
                   _FileExtension = ".xml";

                   if (!Directory.Exists(_DirectoryPath))Directory.CreateDirectory(_DirectoryPath);

                //NewFileName   = _DirectoryPath  + "\\" + GetFileName(Path.GetFileName(fn),out MinNo,out MaxNo);
                //PrvusFileName = _DirectoryPath  + "\\" +Path.GetFileNameWithoutExtension(fn) + "_" + MaxNo.ToString() + _FileExtension;
                NewFileName = _DirectoryPath + "\\" + Path.GetFileName(fn);
                //PrvusFileName = _DirectoryPath + "\\" + Path.GetFileNameWithoutExtension(fn) + "_" + MaxNo.ToString() + _FileExtension;
                try
                   {
                       //if (MinNo != 0 && MaxNo != 0)
                       //{
                       //    if (CheckXml(fn, PrvusFileName))
                       //    {
                       //       //_Log.Add(new LogDetail(Path.GetFileName(fn), "Identical", "Identical"));
                       //          //Copy2FMS(fn, JID);
                       //         _Log.Add(new LogDetail(Path.GetFileName(fn), "Identical", "Success"));
                       //    }
                       //    else
                       //    {
                       //         System.IO.File.Copy(fn, NewFileName, true);
                       //         Copy2FMS(fn, JID);
                       //        _Log.Add(new LogDetail(Path.GetFileName(fn), "Success", "Success"));
                       //    }
                       //}
                       //else
                       //{
                             System.IO.File.Copy(fn, NewFileName, true);
                             //Copy2FMS(fn, JID);
                            _Log.Add(new LogDetail(Path.GetFileName(fn), "Success", "Success"));
                       //}
                   }
				   catch(Exception ex)
                   {
                       //_Log.Add(new LogDetail(Path.GetFileName(fn), "Failed", "Failed"));
                       _Log.Add(new LogDetail(ex.Message, "Failed", "Failed"));
                   }
                   //while (Directory.GetFiles(_DirectoryPath, "*.xml").Length > 4)
                   //{
                   //    string[] bckupfillename = Directory.GetFiles( _DirectoryPath ,"*_" + MinNo.ToString() + ".xml");
                   //    if (bckupfillename.Length > 0)
                   //    {
                   //        string _bckupfillename = _BackupFolderPath + "\\" + Path.GetFileName(bckupfillename[0]);
                   //        if (System.IO.File.Exists(_bckupfillename) ) System.IO.File.Delete(_bckupfillename);
                   //        System.IO.File.Move(bckupfillename[0], _bckupfillename);
                   //    }
                   //    MinNo++;
                   //}
                }
           }

           // FL =  Directory.GetFiles(DesPath,"*.html",SearchOption.AllDirectories);
           //_TextFilesCount = FL.Length;
           //foreach (string fn in FL)
           //{
           //    JID = Path.GetFileNameWithoutExtension(fn).Replace("-diff", "");
           //    _DirectoryPath = _TargetFolderPath + "\\" + JID;
           //    _FileExtension = ".html";
           //    //NewFileName = _DirectoryPath + "\\" + GetFileName(Path.GetFileName(fn) ,out MinNo,out MaxNo);
           //     GetNextFileNo(out MinNo, out MaxNo).ToString();
           //     NewFileName = _DirectoryPath + "\\" + Path.GetFileNameWithoutExtension(fn) + "_" + MaxNo.ToString() + _FileExtension;

           //    try
           //    {
           //        if (!System.IO.File.Exists(NewFileName))
           //        {
           //            System.IO.File.Copy(fn, NewFileName);
           //            _Log.Add(new LogDetail(Path.GetFileName(fn), "Success","None"));
           //        }
           //    }
           //    catch
           //    {
           //         _Log.Add(new LogDetail(Path.GetFileName(fn),"Failed","None"));
           //    }
               

           //    while (Directory.GetFiles(_DirectoryPath, "*.html").Length > 4)
           //    {
           //        string[] bckupfillename = Directory.GetFiles(_DirectoryPath, "*_" + MinNo.ToString() + ".html");
           //        if (bckupfillename.Length > 0)
           //        {
           //            string _bckupfillename = _BackupFolderPath + "\\" + Path.GetFileName(bckupfillename[0]);
           //           if (System.IO.File.Exists(_bckupfillename)) System.IO.File.Delete(_bckupfillename);
           //            System.IO.File.Move(bckupfillename[0], _bckupfillename);
           //        }
           //        MinNo++;
           //    }
           //}
           try
           {
               Directory.Delete(DesPath, true);
           }
           catch (Exception ex)
           {
               DeleteDirectory(DesPath);
           }
           return true;
       }
       private bool Copy2FMS(string SrcPath, string JID)
       {
           try
           {
               string FMSPath         = WebConfigurationManager.AppSettings["FMSFolderPath"];
               string TDXPSPath       = WebConfigurationManager.AppSettings["TDXPSFolderPath"];
               string GANGTDXPSPath = WebConfigurationManager.AppSettings["GANGTDXPSFolderPath"];
               
               if (FMSPath != null)
               {
                   string FMSUsername = WebConfigurationManager.AppSettings["FMSUsername"];
                   string FMSPassword = WebConfigurationManager.AppSettings["FMSPassword"];
                   
                   if (Directory.Exists(FMSPath))
                   {
                       GloVar.ConnectSystem(FMSPath, FMSUsername, FMSPassword);
                   }
                   string TrgtPath      = FMSPath + "\\" + JID;
                   if (Directory.Exists(TrgtPath))
                   {
                       TrgtPath = TrgtPath + "\\" + JID + "-jss.xml";
                       System.IO.File.Copy(SrcPath, TrgtPath,true);
                   }
                   else
                   {
                       Directory.CreateDirectory(TrgtPath);
                       TrgtPath = TrgtPath + "\\" + JID + "-jss.xml";
                       System.IO.File.Copy(SrcPath, TrgtPath, true);
                   }

                   string TDXPSTrgtPath = TDXPSPath + "\\" + JID;
                   if (!Directory.Exists(TDXPSTrgtPath))
                       Directory.CreateDirectory(TDXPSTrgtPath);

                   if (Directory.Exists(TDXPSTrgtPath))
                   {
                       TDXPSTrgtPath = TDXPSTrgtPath + "\\" + JID + "-jss.xml";
                       System.IO.File.Copy(SrcPath, TDXPSTrgtPath, true);
                   }


                   string GANGTDXPSTrgtPath = GANGTDXPSPath + "\\" + JID;
                   if (!Directory.Exists(GANGTDXPSTrgtPath))
                       Directory.CreateDirectory(GANGTDXPSTrgtPath);

                   if (Directory.Exists(GANGTDXPSTrgtPath))
                   {
                       GANGTDXPSTrgtPath = GANGTDXPSTrgtPath + "\\" + JID + "-jss.xml";
                       System.IO.File.Copy(SrcPath, GANGTDXPSTrgtPath, true);
                   }


                   return true;
               }
               else
               {
                   return false;
               }
           }
           catch
           {
               return false;
           }
       }
        private void DeleteDirectory(string DirectoryPath)
        {
            if (Directory.Exists(DirectoryPath))
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = "cmd";
                myProcess.StartInfo.Arguments = " " + @"/c RMDIR " + "\"" + DirectoryPath + "\"" + @" /s /q";
                myProcess.StartInfo.RedirectStandardOutput = true;
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myProcess.Start();
                myProcess.WaitForExit();
            }
        }

        private bool CheckXml(string sourceXmlFile,string changedXmlFile)
        {
            XmlDiffOptions options = XmlDiffOptions.None;
            options |= XmlDiffOptions.IgnoreChildOrder;
            options |= XmlDiffOptions.IgnoreComments;
            options |= XmlDiffOptions.IgnorePI;
            options |= XmlDiffOptions.IgnoreWhitespace;
            options |= XmlDiffOptions.IgnoreNamespaces;
            options |= XmlDiffOptions.IgnorePrefixes;
            options |= XmlDiffOptions.IgnoreXmlDecl;
            options |= XmlDiffOptions.IgnoreDtd;
            bool bFragment = true;


            MemoryStream diffgram = new MemoryStream();
            XmlTextWriter diffgramWriter = new XmlTextWriter(new StreamWriter(diffgram));
            Microsoft.XmlDiffPatch.XmlDiff xmlDiff = new Microsoft.XmlDiffPatch.XmlDiff(options);

            bool bIdentical = false;
            string TempPath = "C:\\Temp1\\";
            string NewTemp = TempPath + Guid.NewGuid().ToString();
            string OldTemp = TempPath + Guid.NewGuid().ToString();

            try
            {
                System.IO.File.Copy(changedXmlFile, NewTemp);
                System.IO.File.Copy(sourceXmlFile, OldTemp);

                bIdentical = xmlDiff.Compare(OldTemp, NewTemp, bFragment, diffgramWriter);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                System.IO.File.Delete(NewTemp);
                System.IO.File.Delete(OldTemp);
            }
            if (bIdentical)
                return true;
            else
                return false;
        }

        private string GetTodayDir()
        {
            if (!Directory.Exists("c:\\temp"))
            {
                Directory.CreateDirectory("c:\\temp");
            }
            string ToDayDir = "c:\\Temp1\\" + DateTime.Now.ToShortDateString().Replace("/", "-");
            if (!Directory.Exists(ToDayDir))
            {
                string[] OldDir = Directory.GetDirectories("c:\\temp");
                foreach (string ForDltdDir in OldDir)
                {
                    Directory.Delete(ForDltdDir, true);
                }
                Directory.CreateDirectory(ToDayDir);
            }
            return ToDayDir;
        }
    }



