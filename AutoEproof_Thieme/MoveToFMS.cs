using System;
using System.Data.SqlClient;
using ProcessNotification;
using System.Net;
using MsgRcvr;
using System.Configuration;
using DatabaseLayer;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoEproof
{
    class MoveToFMS : MessageEventArgs
    {
        readonly string NtwrkLoc = string.Empty;

        string _FMSMNT = string.Empty;

        string _FMSXML = string.Empty;
        string _FMSPDF = string.Empty;
        string _FMSQPDF = string.Empty;
        string _FMSSrc = string.Empty;

        string _NtwrkLocFMSXML = string.Empty;
        string _NtwrkLocFMSPDF = string.Empty;
        string _NtwrkLocFMSQPDF = string.Empty;
        string _NtwrkLocFMSSrc = string.Empty;

        MNTInfo _MNT = null;
        public MoveToFMS(string FMSMNT)
        {
            NtwrkLoc = "C:\\FMS\\";
            _FMSMNT = FMSMNT;

            string MNTFolder = Path.GetFileName(_FMSMNT);

            _MNT = new MNTInfo(MNTFolder);

        }

        public void CreateMNTFolder()
        {
            string XMLFileName = _MNT.JID + _MNT.AID + ".xml";
            string PDFFileName = _MNT.AID + ".pdf";

            _FMSXML = _FMSMNT + "\\" + XMLFileName;
            _FMSPDF = _FMSMNT + "\\" + PDFFileName;

            string NtwrkMNTFolder = NtwrkLoc + _MNT.MNTFolder;

            _NtwrkLocFMSXML = NtwrkMNTFolder + "\\" + XMLFileName;
            _NtwrkLocFMSPDF = NtwrkMNTFolder + "\\" + PDFFileName;

            if (Directory.Exists(NtwrkMNTFolder))
                Directory.Delete(NtwrkMNTFolder, true);


            Directory.CreateDirectory(NtwrkMNTFolder);
            ProcessEventHandler(NtwrkMNTFolder + " folder created successfully..");

            if (File.Exists(_FMSXML))
            {
                File.Copy(_FMSXML, _NtwrkLocFMSXML);
                ProcessEventHandler(_NtwrkLocFMSXML + " file copied successfully..");
            }
            else
            {
                _FMSXML = _FMSMNT + "\\tx1.xml";
                if (File.Exists(_FMSXML))
                {
                    File.Copy(_FMSXML, _NtwrkLocFMSXML);
                    ProcessEventHandler(_NtwrkLocFMSXML + " file copied successfully..");
                }
                else
                {
                    _FMSXML = _FMSMNT + "\\" + _MNT.AID + ".xml";
                    if (File.Exists(_FMSXML))
                    {
                        File.Copy(_FMSXML, _NtwrkLocFMSXML);
                        ProcessEventHandler(_NtwrkLocFMSXML + " file copied successfully..");
                    }
                }
            }
            if (File.Exists(_FMSPDF))
            {
                File.Copy(_FMSPDF, _NtwrkLocFMSPDF);
                string GabsPDFFileName = _FMSPDF.Replace(".pdf", "c.pdf");
                string QPDFFileName = _FMSPDF.Replace(".pdf", "Q.pdf");
                if (File.Exists(GabsPDFFileName))
                {
                    string GabsPDFFileNameCopyTo = _NtwrkLocFMSPDF.Replace(".pdf", "c.pdf");
                    File.Copy(GabsPDFFileName, GabsPDFFileNameCopyTo);
                }
                else
                    ProcessEventHandler(GabsPDFFileName + " file not exist.");
                if (File.Exists(QPDFFileName))
                {
                    string QPDFFileNameCopyTo = _NtwrkLocFMSPDF.Replace(".pdf", "Q.pdf");
                    File.Copy(QPDFFileName, QPDFFileNameCopyTo);
                }
                else
                    ProcessEventHandler(QPDFFileName + " file not exist.");

                ProcessEventHandler(_NtwrkLocFMSXML + " file copied successfully..");
            }


        }



        private string GetIP()
        {
            string strHostName = string.Empty;

            strHostName = Dns.GetHostName();
            Console.WriteLine("Local Machine's Host Name: " + strHostName);
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName);
            IPAddress[] addr = ipEntry.AddressList;

            return addr[0].ToString();
        }
    }


    class FilesFromTDXPS : MessageEventArgs
    {
        // public event NotifyMsg ProcessNotification;

        string _MNTPath = string.Empty;
        MNTInfo _MNT = null;
        FMSStructure _FMSStructureObj = null;
        string GTKFMS = ConfigDetails.TDXPSGangtokFMSFolder;
        public FilesFromTDXPS(string MNTPath, FMSStructure FMSStructureObj)
        {
            _MNTPath = MNTPath;

            string MNTFolder = Path.GetFileName(MNTPath);
            _MNT = new MNTInfo(MNTFolder);
            _FMSStructureObj = FMSStructureObj;
        }
        public FilesFromTDXPS(string MNTPath)
        {
            _MNTPath = MNTPath;

            string MNTFolder = Path.GetFileName(MNTPath);
            _MNT = new MNTInfo(MNTFolder);

        }

        public bool GetFilesForRvsPagination(string CopToLoc)
        {
            bool Rslt = false;
            try
            {

                string ReviseFile = "AIDc.3d#AIDQ.pdf#AID.3d#AID.3f#JIDAID*.eps#JIDAID*.tif#tx1.xml#AID.pdf";
                Rslt = GetFilesForPagination(ReviseFile, CopToLoc);
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }

            return Rslt;
        }
        public bool GetFilesForIssPagination(string CopToLoc)
        {
            bool Rslt = false;
            try
            {
                string IssueFile = "AIDc.3d#AID.3d#AIDc.pdf#AIDQ.pdf#AID.pdf#AID.3f#tx1.xml#JIDAID*.eps#JIDAID*.tif#";
                Rslt = GetFilesForPagination(IssueFile, CopToLoc);

                string[] VOLISS = Path.GetFileName(CopToLoc).Split('_');
                string sPage = "0";
                string ePage = "0";
                DateTime EV = new DateTime();


                string _OPSConStr = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString;
                string SqlStr = "SELECt [STARTPAGE],[ENDPAGE],[EVDATE] FROM [OPS].[dbo].[IssueArticleInfo] where  jidaid='JIDAID'".Replace("JIDAID", _MNT.JID + _MNT.AID);

                try
                {
                    SqlDataReader Dr = SqlHelper.ExecuteReader(_OPSConStr, System.Data.CommandType.Text, SqlStr);
                    Dr.Read();
                    if (Dr.HasRows)
                    {
                        sPage = Dr[0].ToString();
                        ePage = Dr[1].ToString();
                        string EVDate = "0";

                        if (Dr[2] != null)
                        {
                            EVDate = Dr[2].ToString();
                            if (!string.IsNullOrEmpty(EVDate))
                            {
                                string[] Arr = EVDate.Split('_');
                                if (Arr.Length > 2)
                                {
                                    EV = new DateTime(Int32.Parse(Arr[0]), Int32.Parse(Arr[1]), Int32.Parse(Arr[2]));
                                }
                            }
                        }
                    }
                    Dr.Close();
                    Dr.Dispose();
                }
                catch (SqlException ex)
                {
                    ProcessErrorHandler(ex);
                }
                string PagStr = "<pagination><stage>S300</stage><vol>" + VOLISS[1] + "</vol><issue>" + VOLISS[2] + "</issue><startP>" + sPage + "</startP><endP>" + ePage + "</endP><effect-cover-date>2016</effect-cover-date></pagination>";

                //if (EV.Year > 2014)
                //{

                //    string s = "<date-online day=\"strDay\" month=\"strMnth\" year=\"strYear\"/><effect-cover-date>2016</effect-cover-date>".Replace("strDay", EV.Day.ToString().PadLeft(2, '0')).Replace("strMnth", EV.Month.ToString()).Replace("strYear", EV.Year.ToString());

                //    PagStr = PagStr.Replace("</pagination>", s + "</pagination>");
                //}
                string PaginationXML = CopToLoc + "\\" + _MNT.MNTFolder + "\\pagination.xml";
                File.WriteAllText(PaginationXML, PagStr);

            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }
            return Rslt;
        }
        public bool GetFilesForS280(string CopToLoc)
        {
            bool Rslt = false;
            try
            {
                string IssueFile = "#AIDc.pdf#AIDQ.pdf#AID.pdf#final.pdf#tx1.xml#JIDAID*.eps#JIDAID*.tif";
                Rslt = GetFilesForS280(IssueFile, CopToLoc);
                GetFolderForS280("equation", CopToLoc);
                GetFolderForS280("sup", CopToLoc);
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
            }
            return Rslt;
        }
        private bool GetFilesForS280(string RequiredFileList, string CopToLoc)
        {
            bool Rslt = false;

            RequiredFileList = RequiredFileList.Replace("JID", _MNT.JID);
            RequiredFileList = RequiredFileList.Replace("AID", _MNT.AID);

            string[] RequiredFiles = RequiredFileList.Split('#');
            string MNTFolder = CopToLoc + "\\" + (_MNT.JID + _MNT.AID).ToUpper();


            if (Directory.Exists(MNTFolder))
                Directory.Delete(MNTFolder, true);

            Directory.CreateDirectory(MNTFolder);

            ProcessEventHandler(MNTFolder + " folder created successfully..");

            foreach (string RequiredFile in RequiredFiles)
            {
                if (RequiredFile.Contains("*"))
                    Rslt = CopyMultipleFiles(RequiredFile, MNTFolder);
                else
                    Rslt = CopyFile(RequiredFile, MNTFolder);
            }


            if (MNTFolder.Contains("S280"))
            {
                string AIDpdf = MNTFolder + "\\" + _MNT.AID + ".pdf";
                string AIDxml = MNTFolder + "\\tx1.xml";
                if (File.Exists(AIDpdf))
                {
                    string newAIDpdf = MNTFolder + "\\" + _MNT.JID.ToLower() + _MNT.AID.ToLower() + ".pdf";

                    if (File.Exists(newAIDpdf))
                    {
                        File.Delete(newAIDpdf);
                    }
                    File.Move(AIDpdf, newAIDpdf);
                }
                if (File.Exists(AIDxml))
                {
                    string newAIDxml = MNTFolder + "\\" + _MNT.JID.ToLower() + _MNT.AID.ToLower() + ".xml";

                    if (File.Exists(newAIDxml))
                    {
                        File.Delete(newAIDxml);
                    }
                    File.Move(AIDxml, newAIDxml);
                }
            }
            return Rslt;
        }
        private bool GetFilesForPagination(string RequiredFileList, string CopToLoc)
        {
            bool Rslt = false;

            RequiredFileList = RequiredFileList.Replace("JID", _MNT.JID);
            RequiredFileList = RequiredFileList.Replace("AID", _MNT.AID);

            string[] RequiredFiles = RequiredFileList.Split('#');
            string MNTFolder = CopToLoc + "\\" + _MNT.MNTFolder;


            if (Directory.Exists(MNTFolder))
                Directory.Delete(MNTFolder, true);

            Directory.CreateDirectory(MNTFolder);

            ProcessEventHandler(MNTFolder + " folder created successfully..");

            foreach (string RequiredFile in RequiredFiles)
            {
                if (RequiredFile.Contains("*"))
                    Rslt = CopyMultipleFiles(RequiredFile, MNTFolder);
                else
                    Rslt = CopyFile(RequiredFile, MNTFolder);
            }


            if (MNTFolder.Contains("S280"))
            {
                string AIDpdf = MNTFolder + "\\" + _MNT.AID + ".pdf";
                string AIDxml = MNTFolder + "\\tx1.xml";
                if (File.Exists(AIDpdf))
                {
                    string newAIDpdf = MNTFolder + "\\" + _MNT.JID.ToLower() + _MNT.AID.ToLower() + ".pdf";

                    if (File.Exists(newAIDpdf))
                    {
                        File.Delete(newAIDpdf);
                    }
                    File.Move(AIDpdf, newAIDpdf);
                }
                if (File.Exists(AIDxml))
                {
                    string newAIDxml = MNTFolder + "\\" + _MNT.JID.ToLower() + _MNT.AID.ToLower() + ".xml";

                    if (File.Exists(newAIDxml))
                    {
                        File.Delete(newAIDxml);
                    }
                    File.Move(AIDxml, newAIDxml);
                }
            }
            return Rslt;
        }

        private bool GetFolderForS280(string FldrName, string CopToLoc)
        {
            bool Rslt = true;

            try
            {
                string MNTFolder = CopToLoc + "\\" + _MNT.JID + _MNT.AID;
                ProcessEventHandler(MNTFolder + " folder created successfully..");
                string[] DIRList = Directory.GetDirectories(_MNTPath, FldrName, SearchOption.AllDirectories);
                if (DIRList.Length > 0)
                {
                    string CopyDIRFrom = DIRList[0];
                    if (Directory.Exists(CopyDIRFrom))
                    {
                        string CopyDIRTo = MNTFolder + "\\" + Path.GetFileName(CopyDIRFrom);
                        Program.Copy(CopyDIRFrom, CopyDIRTo);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcessErrorHandler(ex);
                Rslt = false;
            }
            return Rslt;
        }

        private bool CopyMultipleFiles(string SrchPtrn, string CopyToLoc)
        {
            bool Rslt = false;
            string[] Files = null;
            if (Directory.Exists(_MNTPath))
                Files = Directory.GetFiles(_MNTPath, SrchPtrn);
            if (Files != null)
            {
                if (Directory.Exists(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Text\\") && Files.Length == 0)
                    Files = Directory.GetFiles(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Text\\", SrchPtrn);
                if (Directory.Exists(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Output\\") && Files.Length == 0)
                    Files = Directory.GetFiles(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Output\\", SrchPtrn);
                if (Directory.Exists(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\ART\\") && Files.Length == 0)
                    Files = Directory.GetFiles(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\ART\\", SrchPtrn);
            }
            else
            {
                if (Directory.Exists(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Text\\"))
                    Files = Directory.GetFiles(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Text\\", SrchPtrn);
                if (Directory.Exists(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Output\\") && Files.Length == 0)
                    Files = Directory.GetFiles(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Output\\", SrchPtrn);
                if (Directory.Exists(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\ART\\") && Files.Length == 0)
                    Files = Directory.GetFiles(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\ART\\", SrchPtrn);
            }
            if (Files.Length == 0)
            {
                Rslt = true;
            }
            foreach (string fl in Files)
            {
                string NewFileName = CopyToLoc + "\\" + Path.GetFileName(fl);
                ProcessEventHandler(fl + " is being file copied..");
                try
                {
                    if (File.Exists(fl))
                        File.Copy(fl, NewFileName, true);
                    Rslt = true;
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                    Rslt = false;
                }
                ProcessEventHandler(fl + " file copied successfully..");
            }


            return Rslt;
        }
        private bool CopyFile(string FileName, string CopyToLoc)
        {
            try
            {
                string CopyFrom = _MNTPath + "\\" + FileName;
                string CopyTo = CopyToLoc + "\\" + FileName;
                if (FileName.EndsWith("final.pdf"))
                    CopyTo = CopyToLoc + "\\" + _MNT.JID.ToLower() + _MNT.AID + "_am.pdf";


                if (File.Exists(CopyFrom))
                { }
                else
                {
                    if (FileName.EndsWith("pdf") && _FMSStructureObj != null)
                        CopyFrom = _FMSStructureObj.OutFolder + "\\" + FileName;
                }



                if (File.Exists(CopyFrom))
                {
                    ProcessEventHandler(CopyFrom + " is being file copied..");
                    File.Copy(CopyFrom, CopyTo, true);
                    ProcessEventHandler(CopyFrom + " file copied successfully..");
                }
                else
                {
                    if (File.Exists(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Text\\" + FileName))
                    {
                        ProcessEventHandler(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Text\\" + FileName + " is being file copied..");
                        File.Copy(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Text\\" + FileName, CopyTo, true);
                        ProcessEventHandler(GTKFMS + "\\" + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Text\\" + FileName + " file copied successfully..");
                    }

                    if (File.Exists(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Output\\" + FileName))
                    {
                        ProcessEventHandler(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Output\\" + FileName + " is being file copied..");
                        File.Copy(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Output\\" + FileName, CopyTo, true);
                        ProcessEventHandler(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\Output\\" + FileName + " file copied successfully..");
                    }

                    if (File.Exists(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\ART\\" + FileName))
                    {
                        ProcessEventHandler(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\ART\\" + FileName + " is being file copied..");
                        File.Copy(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\ART\\" + FileName, CopyTo, true);
                        ProcessEventHandler(GTKFMS + _MNT.Client + "\\JOURNAL\\" + _MNT.JID + "\\" + _MNT.AID + "\\ART\\" + FileName + " file copied successfully..");
                    }
                }
            }
            catch (Exception ex)
            {

                ProcessErrorHandler(ex);
                return false;
            }
            return true;
        }
    }

}

