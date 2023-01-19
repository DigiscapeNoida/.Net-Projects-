using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TD.Data;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;



namespace Contrast4ElsBooks
{
    public class DatasetCreation
    {

        int Counter = 0;
        private string strMarker;
        string FolderPath;
        string strSPath;
        string strTPath;
        string[] BInfo;
        string[] AInfo;
        string sISBN;
        string strGXML;
        string strGAsset;
        string strGPDF;
        string glFlow;
        bool boolPDFLess;
        string DSubType;
        private void FillTemp()
        {
            Counter = 0;
            string strMarker = string.Empty;
            FolderPath = string.Empty;
            strSPath = string.Empty;
            strTPath = string.Empty;
            BInfo = null;
            AInfo = null;
            sISBN = string.Empty;
            strGXML = string.Empty;
            strGAsset = string.Empty;
            strGPDF = string.Empty;            
            DSubType = string.Empty;
        } 

        public DatasetCreation()
        {
        }

        public DatasetCreation(string Marker)
        {
            strSPath = Path.GetDirectoryName(Marker);// System.Configuration.ConfigurationSettings.AppSettings["SourcePath"];
            strTPath = GlobalConfig.oGlobalConfig.TARGET;// System.Configuration.ConfigurationSettings.AppSettings["TargetPath"];
            FolderPath = "";
            sISBN = Path.GetFileName(Marker);
            glFlow = "";
            DSubType = "";
            boolPDFLess = CheckPDFLess(sISBN);
            

            //boolPDFLess = true;
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public bool CreateDataset(out string Error, out string pii, out string isbn)
        {
            try
            {
                if (!(Directory.Exists(strSPath)))
                {
                    Error = "Error: Directory does not exists!!!!";
                    pii = "";
                    isbn = strSPath;
                    return false;
                }

                if (GetStructueInformation(out Error) == false)
                {
                    pii = "";
                    isbn = strSPath;
                    return false;
                }
                DirectoryInfo[] DInfo = new DirectoryInfo(FolderPath).GetDirectories();
                if (DInfo.Length >= 0)
                {
                    for (int i = 0; i < DInfo.Length; i++)
                    {
                        string ConPath = DInfo[i].FullName;
                        //if ((File.Exists(ConPath + "\\PDF\\main.pdf")) && (File.Exists(ConPath + "\\XML\\main.xml")))
                        //// Condition changed for PC 2.0  07 of Feb-2017  edited by Kshitij
                        if ((File.Exists(ConPath + "\\XML\\main.xml")))
                        {
                            Counter = Convert.ToInt32(DataAccess.ExecuteScalarSP(SPNames.getid));
                            Counter++;

                            string Round = "R1";
                            string strCounter = Counter.ToString();
                            while (strCounter.Length < 8)
                            {
                                strCounter = "0" + strCounter;
                            }
                            string ThomNum = "TOMBK" + strCounter; //GetTombk
                            StreamReader sr = new StreamReader(ConPath + "\\XML\\main.xml");
                            string FCon = sr.ReadToEnd();
                            sr.Close();
                            string strInfo = GetInformation(FCon, "info");
                            string strISBN = GetInformation(strInfo, "ce:isbn");
                            string strPII = GetInformation(strInfo, "ce:pii");
                            string strDOI = GetInformation(strInfo, "ce:doi");
                            // for docsubtype
                            string strDocsubtype = "";
                            if (!strPII.Contains(strISBN))
                            {
                                Error = "Error: ISBN and PII Mismatch!!!!";
                                isbn = strISBN;
                                pii = strPII;
                                return false;
                            }


                            int docsop = FCon.IndexOf("docsubtype=\"", 0, StringComparison.OrdinalIgnoreCase);
                            if (docsop != -1)
                            {
                                int edocsop = FCon.IndexOf("\"", docsop + "docsubtype=\"".Length);
                                if (edocsop != -1)
                                {
                                    strDocsubtype = FCon.Substring(docsop + "docsubtype=\"".Length, edocsop - docsop - "docsubtype=\"".Length);
                                    DSubType = strDocsubtype;
                                }
                            }

                            /*
                            int dsop = FCon.IndexOf("<chapter ", 0, StringComparison.OrdinalIgnoreCase);
                            if (dsop != -1)
                            {
                                int edsop = FCon.IndexOf(">", dsop);
                                string chapval = FCon.Substring(dsop, edsop - dsop + 1);
                                int docsop = chapval.IndexOf("docsubtype=\"", 0, StringComparison.OrdinalIgnoreCase);
                                if (docsop != -1)
                                {
                                    int edocsop = chapval.IndexOf("\"", docsop + "docsubtype=\"".Length);
                                    if (edocsop != -1)
                                    {
                                        strDocsubtype = chapval.Substring(docsop + "docsubtype=\"".Length, edocsop - docsop - "docsubtype=\"".Length);
                                    }
                                }
                            }
                            */

                              if (strISBN.IndexOf("<") != -1)
                            {
                                strISBN = strISBN.Substring(0, strISBN.IndexOf("<"));
                            }

                            if (strPII.IndexOf("<") != -1)
                            {
                                strPII = strPII.Substring(0, strPII.IndexOf("<"));
                            }

                            if (strDOI.IndexOf("<") != -1)
                            {
                                strDOI = strDOI.Substring(0, strDOI.IndexOf("<"));
                            }

                            string strMainPath = (strTPath + "\\" + ThomNum);
                            Directory.CreateDirectory(strMainPath);
                            if (!Directory.Exists(strMainPath))
                                continue;
                            string strItemPath = strMainPath + "\\" + strISBN.Replace("-", "");
                            Directory.CreateDirectory(strItemPath);
                            if (!Directory.Exists(strItemPath))
                                continue;

                            string strChapPath = strItemPath + "\\" + strPII.Replace("-", "").Replace(".", "");
                            Directory.CreateDirectory(strChapPath);
                            if (!Directory.Exists(strChapPath))
                                continue;


                            SqlParameter[] sqlparam = new SqlParameter[2];
                            sqlparam[0] = new SqlParameter("isbn", strISBN);
                            sqlparam[1] = new SqlParameter("pii", strPII);
                            DataSet ds = DataAccess.ExecuteDataSetSP(SPNames.getstatus, sqlparam);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string sts = ds.Tables[0].Rows[0]["RSTATUS"].ToString();
                                if (sts.Trim().ToLower() == "ROUND2")
                                {
                                    Round = "ROUND2";
                                }
                            }

                            //// Condition changed for PC 2.0  07 of Feb-2017  edited by Kshitij
                            //// PDF should not be copied for PC-2.0                            

                            if (boolPDFLess == false)
                            {
                                File.Copy(ConPath + "\\PDF\\main.pdf", strChapPath + "\\main.pdf", true);
                            }
                            else
                            {
                                if (strDocsubtype != "chp" && strDocsubtype != "itr" && strDocsubtype != "idx")
                             //   if (strDocsubtype != "chp" && strDocsubtype != "itr") //Ignoring Index
                                {
                                    File.Copy(ConPath + "\\PDF\\main.pdf", strChapPath + "\\main.pdf", true);
                                }
                            }

                            File.Copy(ConPath + "\\XML\\main.xml", strChapPath + "\\main.xml", true);

                            if (File.Exists(strChapPath + "\\main.xml"))
                            {
                                GlobalConfig.oGlobalConfig.Convert_UTF8toENT(strChapPath + "\\main.xml");                               
                                Check_Queries(strChapPath + "\\main.xml");
                            }

                            //Strippins Check

                         

                            string chaptertitle = ChapTitle(strChapPath + "\\main.xml");
                            string lbl = ChapLbl(strChapPath + "\\main.xml");
                            if (lbl.Trim() == "")
                            {
                                lbl = chaptertitle;
                            }

                            if (chaptertitle.Trim() == "")
                            {
                                Error = "Title does not exist in main.xml";
                                pii = strPII;
                                isbn = strISBN;
                                return false;
                            }

                            string strFileContInfo = "";
                            strFileContInfo = strFileContInfo + strGXML;
                            //Artwork    

                            if (Directory.Exists(ConPath + "\\art"))
                            {
                                string strArtPath = strChapPath + "\\main.assets";
                                Directory.CreateDirectory(strArtPath);
                                if (!Directory.Exists(strArtPath))
                                {
                                    continue;
                                }
                                FileInfo[] FInfo = new DirectoryInfo(ConPath + "\\art").GetFiles();

                                for (int j = 0; j < FInfo.Length; j++)
                                {
                                    if (FInfo[j].Name.ToLower().Contains("thum"))
                                        continue;
                                    File.Copy(FInfo[j].FullName, strArtPath + "\\" + FInfo[j].Name, true);

                                    string strAsset = strGAsset.Replace("[FILE]", FInfo[j].Name);
                                    if (FInfo[j].Name.EndsWith(".mp4"))
                                        strAsset = strAsset.Replace("[ASSETTYPE]", "VIDEO");
                                    else if (FInfo[j].Name.EndsWith(".jpg") || FInfo[j].Name.EndsWith(".jpeg"))
                                        strAsset = strAsset.Replace("[ASSETTYPE]", "IMAGE-CAP");
                                    else
                                        strAsset = strAsset.Replace("[ASSETTYPE]", "APPLICATION");

                                    strFileContInfo = strFileContInfo + strAsset;
                                }
                            }

                            strFileContInfo = strFileContInfo.Replace("[PATH]", strISBN.Replace("-", "") + "/" + strPII.Replace("-", "").Replace(".", ""));


                            //// Condition changed for PC 2.0  07 of Feb-2017  edited by Kshitij
                            //// Stripins not required for PC-2.0



                            //Stripins
                           // if (boolPDFLess == false)
                           // {
                                if (Directory.Exists(ConPath + "\\stripin"))
                                {
                                    string strArtPath = strChapPath + "\\main.stripin";
                                    Directory.CreateDirectory(strArtPath);
                                    if (!Directory.Exists(strArtPath))
                                        continue;

                                    FileInfo[] FInfo = new DirectoryInfo(ConPath + "\\stripin").GetFiles();

                                    for (int j = 0; j < FInfo.Length; j++)
                                    {
                                        File.Copy(FInfo[j].FullName, strArtPath + "\\" + FInfo[j].Name, true);
                                        //strFileContInfo = strFileContInfo + strGAsset.Replace("[FILE]", FInfo[j].Name).Replace("/main.assets/", "/main.stripin/"); ;
                                    }
                                }
                                else if (Directory.Exists(ConPath + "\\stripins"))
                                {
                                    string strArtPath = strChapPath + "\\main.stripin";
                                    Directory.CreateDirectory(strArtPath);
                                    if (!Directory.Exists(strArtPath))
                                        continue;

                                    FileInfo[] FInfo = new DirectoryInfo(ConPath + "\\stripins").GetFiles();

                                    for (int j = 0; j < FInfo.Length; j++)
                                    {
                                        File.Copy(FInfo[j].FullName, strArtPath + "\\" + FInfo[j].Name, true);
                                        //strFileContInfo = strFileContInfo + strGAsset.Replace("[FILE]", FInfo[j].Name).Replace("/main.assets/", "/main.stripin/");
                                    }
                                }

                            //}

                            /*

                            // Stripins
                            if (Directory.Exists(ConPath + "\\stripins"))
                            {
                                string strArtPath = strChapPath + "\\main.stripin";
                                Directory.CreateDirectory(strArtPath);
                                if (!Directory.Exists(strArtPath))
                                    continue;

                                FileInfo[] FInfo = new DirectoryInfo(ConPath + "\\stripins").GetFiles();
                                for (int j = 0; j < FInfo.Length; j++)
                                {
                                    File.Copy(FInfo[j].FullName, strArtPath + "\\" + FInfo[j].Name, true);
                                    //strFileContInfo = strFileContInfo + strGAsset.Replace("[FILE]", FInfo[j].Name);
                                }
                            }
                            
                            */

                            if (boolPDFLess == false)                                    
                            {
                                strFileContInfo = "<ml>" + strFileContInfo + "</ml>" + strGPDF;
                            }
                            else
                            {
                                if (strDocsubtype != "chp" && strDocsubtype != "itr" && strDocsubtype != "idx")
                                {                                                              
                                    strFileContInfo = "<ml>" + strFileContInfo + "</ml>" + strGPDF;
                                }  
                            }






                           
                            strFileContInfo = strFileContInfo.Replace("[PATH]", strISBN.Replace("-", "") + "/" + strPII.Replace("-", "").Replace(".", ""));

                            if (strFileContInfo.IndexOf("<ml>") == -1)
                                strFileContInfo = "<files-info><ml>" + strFileContInfo + "</ml></files-info>";
                            else
                                strFileContInfo = "<files-info>" + strFileContInfo + "</files-info>";

                            string root = System.AppDomain.CurrentDomain.BaseDirectory;
                            StreamReader srDI = new StreamReader(root + "\\DatassetInfo.txt");
                            string strDatasetCon = srDI.ReadToEnd();
                            srDI.Close();

                            string strPC = "";

                            StreamReader srFI = new StreamReader(root + "\\FContInfo.txt");
                            string strFI = srFI.ReadToEnd();
                            srFI.Close();

                            string Tomail = "";
                            string AditionalAuthCcmail = "";
                            string Authorname = "";
                            string Editorname = "";
                            // string PMNAME = "";
                            string EditorEmail = "";
                            string AditionalEDCcmail = "";
                            string ElsPMEmail = "";
                            string ELSPMNAME = "";
                            string ThomsonPMMail = "";
                            string ThomsonPMname = "";
                            string BranchType = "";
                            string FtpDetails = "";
                            string Laung = "";
                            int AuthDueday = 0;
                            int EDDueday = 0;

                            for (int j = 0; j < BInfo.Length; j++)
                            {

                                if (BInfo[j].ToLower().StartsWith(DInfo[i].Name.ToLower()))
                                {
                                    string[] TempArr = BInfo[j].Split(',');
                                    if (TempArr.Length == 16 && glFlow == "EDF")
                                    {
                                        StreamReader srPI = new StreamReader(root + "\\PCInfoE.txt");
                                        strPC = srPI.ReadToEnd();
                                        srPI.Close();
                                        strPC = strPC.Replace("[AN]", TempArr[1]);
                                        strPC = strPC.Replace("[AE]", TempArr[2]);
                                        strPC = strPC.Replace("[EN]", TempArr[3]);
                                        strPC = strPC.Replace("[EE]", TempArr[4]);

                                        Authorname = TempArr[1].ToString();
                                        Tomail = TempArr[2].ToString();
                                        Editorname = TempArr[3].ToString();
                                        EditorEmail = TempArr[4].ToString();
                                        ElsPMEmail = TempArr[5].ToString();
                                        ELSPMNAME = TempArr[6].ToString();
                                        AditionalAuthCcmail = TempArr[7].ToString();
                                        AditionalEDCcmail = TempArr[8].ToString();
                                        ThomsonPMMail = TempArr[9].ToString();
                                        ThomsonPMname = TempArr[10].ToString();
                                        AuthDueday = Convert.ToInt16(TempArr[11]);
                                        EDDueday = Convert.ToInt16(TempArr[12]);
                                        BranchType = TempArr[13].ToString();
                                        FtpDetails = TempArr[14].ToString();
                                        Laung = TempArr[15].ToString();

                                        if (Authorname == "" || Tomail == "" || Editorname == "" || EditorEmail == "" || ElsPMEmail == "" || ELSPMNAME == "" ||
                                            ThomsonPMMail == "" || ThomsonPMname == "" || AuthDueday == 0 || EDDueday == 0 || BranchType == "" || FtpDetails == "" || Laung == "")// || IsValidEmail(Tomail)
                                            //|| IsValidEmail(EditorEmail) || IsValidEmail(ElsPMEmail) || IsValidEmail(ThomsonPMMail))
                                        {

                                            string Valv = "";
                                            Valv = Valv + "Authorname:" + Authorname + "\n";
                                            Valv = Valv + "Tomail:" + Tomail + "\n";
                                            Valv = Valv + "Editorname:" + Editorname + "\n";
                                            Valv = Valv + "EditorEmail:" + EditorEmail + "\n";
                                            Valv = Valv + "ElsPMEmail:" + ElsPMEmail + "\n";
                                            Valv = Valv + "ELSPMNAME:" + ELSPMNAME + "\n";
                                            Valv = Valv + "ThomsonPMMail:" + ThomsonPMMail + "\n";
                                            Valv = Valv + "ThomsonPMname:" + ThomsonPMname + "\n";
                                            Valv = Valv + "AuthDueday:" + AuthDueday + "\n";
                                            Valv = Valv + "EDDueday:" + EDDueday + "\n";
                                            Valv = Valv + "BranchType:" + BranchType + "\n";
                                            Valv = Valv + "FtpDetails:" + FtpDetails + "\n";
                                            Valv = Valv + "Laung:" + Laung + "\n";

                                            Error = "Error: Wrong values in Editor's template used!!!" + "---------------------------------\n\n" + Valv + "---------------------------------\n\n";
                                            pii = strPII;
                                            isbn = strISBN;
                                            return false;
                                        }


                                    }
                                    else if (TempArr.Length == 12 && glFlow == "AUF")
                                    {
                                        StreamReader srPI = new StreamReader(root + "\\PCInfoA.txt");
                                        strPC = srPI.ReadToEnd();
                                        srPI.Close();
                                        strPC = strPC.Replace("[AN]", TempArr[1]);
                                        strPC = strPC.Replace("[AE]", TempArr[2]);

                                        /* Authorname = TempArr[1].ToString();
                                          Tomail = TempArr[2].ToString();
                                          Ccmail = TempArr[3].ToString();
                                          PMNAME = TempArr[4].ToString();
                                          Duedate = Convert.ToDateTime(TempArr[5].ToString());
                                          //EDDuedate = Convert.ToDateTime("");
                                        */

                                        Authorname = TempArr[1].ToString();
                                        Tomail = TempArr[2].ToString();
                                        Editorname = "";
                                        EditorEmail = "";
                                        ElsPMEmail = TempArr[3].ToString();
                                        ELSPMNAME = TempArr[4].ToString();
                                        AditionalAuthCcmail = TempArr[5].ToString();
                                        AditionalEDCcmail = "";
                                        ThomsonPMMail = TempArr[6].ToString();
                                        ThomsonPMname = TempArr[7].ToString();
                                        AuthDueday = Convert.ToInt32(TempArr[8]);
                                        BranchType = TempArr[9].ToString();
                                        FtpDetails = TempArr[10].ToString();
                                        Laung = TempArr[11].ToString();

                                        if (BranchType == "" || Authorname == "" || Tomail == "" || ElsPMEmail == "" || ELSPMNAME == "" || ThomsonPMMail == "" || ThomsonPMname == "" || AuthDueday == 0 || FtpDetails == "" || Laung == "")// || IsValidEmail(Tomail)
                                            //|| IsValidEmail(EditorEmail) || IsValidEmail(ElsPMEmail) || IsValidEmail(ThomsonPMMail))
                                        {
                                            Error = "Error: Wrong values in Author's template!!!!";
                                            pii = strPII;
                                            isbn = strISBN;
                                            return false;
                                        }

                                    }
                                    else
                                    {
                                        if (TempArr.Length == 5 && glFlow == "PM")
                                        {
                                            StreamReader srPI = new StreamReader(root + "\\PCInfoPM.txt");
                                            strPC = srPI.ReadToEnd();
                                            srPI.Close();
                                            strPC = strPC.Replace("[PM]", TempArr[1]);
                                            strPC = strPC.Replace("[PME]", TempArr[2]);

                                            /* Authorname = TempArr[1];
                                             Tomail = TempArr[2];
                                             AditionalAuthCcmail = TempArr[3];
                                             PMNAME = TempArr[4].ToString();
                                             Round = "R2";
                                             Duedate = Convert.ToDateTime(TempArr[5].ToString());                                                
                                             */
                                            Authorname = "";
                                            Tomail = "";
                                            Editorname = "";
                                            EditorEmail = "";
                                            ElsPMEmail = TempArr[2].ToString();
                                            ELSPMNAME = TempArr[1].ToString();
                                            AditionalAuthCcmail = "";
                                            AditionalEDCcmail = "";
                                            ThomsonPMMail = TempArr[3].ToString();
                                            ThomsonPMname = TempArr[4].ToString();
                                            if (ElsPMEmail == "" || ELSPMNAME == "" || ThomsonPMMail == "" || ThomsonPMname == "")
                                            {
                                                Error = "Error: Wrong values in PM's template used for Round2 !!!!";
                                                pii = strPII;
                                                isbn = strISBN;
                                                return false;
                                            }
                                            Round = "R2";
                                        }
                                        else
                                        {
                                            Error = "Error: Wrong template is used  !!!";
                                            pii = strPII;
                                            isbn = strISBN;
                                            return false;
                                        }
                                    }
                                    break;
                                }
                            }

                            //Datset.xml                           

                            string strDate = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss+05:30");
                            strDatasetCon = strDatasetCon.Replace("[DATASETID]", ThomNum);
                            strDatasetCon = strDatasetCon.Replace("[TIMESTAMP]", strDate);
                            strDatasetCon = strDatasetCon.Replace("[PII]", strPII);
                            strDatasetCon = strDatasetCon.Replace("[DOI]", strDOI);
                            strDatasetCon = strDatasetCon.Replace("[ISBN]", strISBN);
                            strDatasetCon = strDatasetCon.Replace("[FILEINFO]", strFileContInfo);
                            strDatasetCon = strDatasetCon.Replace("[PCINFO]", strPC);
                           
                            //PIT Updation

                            string TempPIT = System.Configuration.ConfigurationManager.AppSettings["PitInfo"];
                            string []ArrPIT = TempPIT.Split(';');
                            for (int j=0;j<ArrPIT.Length;j++)
                            {
                                if (ArrPIT[j].StartsWith(GlobalConfig._strPIT + "|"))
                                {
                                    string []TempArr = ArrPIT[j].Split('|');
                                    strDatasetCon.Replace("<PIT>CHP</PIT>", "<PIT>" + TempArr[0]+"</PIT>");
                                    strDatasetCon.Replace("[PITINFO]", TempArr[1]);
                                    break;
                                }

                            }


                            
                            StreamWriter sw = new StreamWriter(strMainPath + "\\dataset.xml");



                            sw.Write(strDatasetCon);
                            sw.Close();

                            SqlParameter[] sqlinputparm = new SqlParameter[25];
                            sqlinputparm[0] = new SqlParameter("id", Counter);
                            sqlinputparm[1] = new SqlParameter("isbn", strISBN);
                            sqlinputparm[2] = new SqlParameter("pii", strPII);
                            sqlinputparm[3] = new SqlParameter("status", "success");
                            sqlinputparm[4] = new SqlParameter("tombk", ThomNum);
                            sqlinputparm[5] = new SqlParameter("MailTo", Tomail);
                            sqlinputparm[6] = new SqlParameter("MailCC", AditionalAuthCcmail);
                            sqlinputparm[7] = new SqlParameter("AuthorName", Authorname);
                            sqlinputparm[8] = new SqlParameter("EditorsName", Editorname);
                            sqlinputparm[9] = new SqlParameter("EditorsMail", EditorEmail); 
                            sqlinputparm[10] = new SqlParameter("PM", ELSPMNAME);
                            sqlinputparm[11] = new SqlParameter("DUEDAYS", AuthDueday);
                            sqlinputparm[12] = new SqlParameter("Flow", glFlow);
                            sqlinputparm[13] = new SqlParameter("ChapTitle", chaptertitle);
                            sqlinputparm[14] = new SqlParameter("FRound", Round);
                            sqlinputparm[15] = new SqlParameter("EDDUEDAYS", EDDueday);
                            sqlinputparm[16] = new SqlParameter("ChpLbl", lbl);
                            sqlinputparm[17] = new SqlParameter("BCCMail", ThomsonPMMail);
                            sqlinputparm[18] = new SqlParameter("ElsPMName", ThomsonPMname);
                            sqlinputparm[19] = new SqlParameter("PMMAIL", ElsPMEmail);
                            sqlinputparm[20] = new SqlParameter("AdditionalEDEmail", AditionalEDCcmail);
                            sqlinputparm[21] = new SqlParameter("docsubtype", strDocsubtype);
                            sqlinputparm[22] = new SqlParameter("BranchType", BranchType);
                            sqlinputparm[23] = new SqlParameter("FtpDetails", FtpDetails);
                            sqlinputparm[24] = new SqlParameter("Laung", Laung);
                            int res = DataAccess.ExecuteNonQuerySP(SPNames.insertinfo, sqlinputparm);
                        }
                        else
                        {
                            Error = "Error: main.xml does not exists";
                            pii = "";
                            isbn = ConPath;
                            return false;
                        }
                    }
                }

                pii = "";
                isbn = FolderPath;
                return true;
            }
            catch (Exception exe)
            {
                Error = exe.Message.ToString();
                pii = "";
                isbn = "";
                return false;
            }
        }

        private void Check_Queries(string strFilePath)
      
        {

            try
            {
                string strFileC = "";

                StreamReader sr = new StreamReader(strFilePath);
                strFileC = sr.ReadToEnd();
                sr.Close();
                /*
                <!--<query id="Q1" type="Keyterm/Keywords:Confirm correctness">
                S/B
                <!--<query id="q1" >
                <!--<query id="q2" >  etc.
                */

                string pattern = "(<!--<query)(.*?)(>)";

                MatchCollection MC = Regex.Matches(strFileC, pattern);

                foreach (Match m in MC)
                {
                    Console.WriteLine(m.Value);

                    string TempCStr = m.Value;
                    string TempNStr = TempCStr.Replace("id=\"Q", "id=\"q");
                    if (TempNStr.IndexOf(" type=") != -1)
                    {
                        TempNStr = TempNStr.Substring(0, TempNStr.IndexOf(" type=")) + ">";
                    }

                    strFileC = strFileC.Replace(TempCStr, TempNStr);
                }
                File.Delete(strFilePath);
                StreamWriter sw = new StreamWriter(strFilePath);
                sw.Write(strFileC);
                sw.Close();

            }
            catch (Exception Ex)
            { }


        }

        private string Check_Strippins(string strFilePath)
        {

            StreamReader sr = new StreamReader(@"C:\Users\57916\Desktop\main.xml");
            string strFileC = sr.ReadToEnd();
            sr.Close();
            /*
            <!--<query id="Q1" type="Keyterm/Keywords:Confirm correctness">
            S/B
            <!--<query id="q1" >
            <!--<query id="q2" >  etc.
            */

            string pattern = "(<ce:formula)(.*?)(</ce:formula>)";

            MatchCollection MC = Regex.Matches(strFileC, pattern);

            foreach (Match m in MC)
            {
                Console.WriteLine(m.Value);               
            }

            return "";




        }

        private bool DocSubtype(string content)
        {
            try
            {
                int start = content.IndexOf("docsubtype=\"", StringComparison.OrdinalIgnoreCase);
                if (start != -1)
                {
                    int end = content.IndexOf("\"", start + "docsubtype=\"".Length);
                    if (end != -1)
                    {
                            string doctype = content.Substring(start + "docsubtype=\"".Length, end - start - "docsubtype=\"".Length);
                        if (doctype.Trim().ToUpper() == "CHP" || doctype.Trim().ToUpper() == "ITR")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
        string ChapLbl(string mainxml)
        {
            string lbl = "";
            try
            {
                string main = File.ReadAllText(mainxml);
                if (!(DocSubtype(main)))
                {
                    int idx = main.IndexOf("<ce:title");
                    if (idx != -1)
                    {
                        int idxcls = main.LastIndexOf("</", idx);
                        if (idxcls != -1)
                        {
                            string inter = main.Substring(idxcls, idx - idxcls).Trim().ToUpper();
                            if (inter == "</CE:LABEL>")
                            {
                                int strt = main.LastIndexOf("<ce:label>", idxcls);
                                if (strt != -1)
                                {
                                    lbl = main.Substring(strt + "<ce:label>".Length, idxcls - strt - "<ce:label>".Length);
                                    int comment = lbl.IndexOf("<!--");
                                    while (comment != -1)
                                    {
                                        int end = lbl.IndexOf("-->", comment);
                                        lbl = lbl.Remove(comment, end - comment + 3);
                                        comment = lbl.IndexOf("<!--");
                                    }

                                    int value;
                                    if (int.TryParse(lbl, out value))
                                    {
                                        lbl = "Chapter " + value;
                                    }
                                }
                            }
                        }
                    }
                }
                return lbl;
            }
            catch (Exception)
            {
                return "";
            }
        }


        string ChapTitle(string mainxml)
        {
            string title = string.Empty;
            try
            {

                string main = File.ReadAllText(mainxml);
                int idx = main.IndexOf("<ce:title");
                if (idx != -1)
                {
                    int idxcls = main.IndexOf(">", idx);
                    if (idxcls != -1)
                    {
                        int cls = main.IndexOf("</ce:title>", idxcls);
                        if (cls != -1)
                        {
                            title = main.Substring(idxcls + 1, cls - idxcls - 1);

                            int comment = title.IndexOf("<!--");
                            while (comment != -1)
                            {
                                int end = title.IndexOf("-->", comment);
                                title = title.Remove(comment, end - comment + 3);
                                comment = title.IndexOf("<!--");
                            }
                        }
                    }
                }
                return title;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string GetInformation(string content, string tag)
        {
            string RetVal = "";

            int spos = content.IndexOf("<" + tag + ">");
            int epos = content.IndexOf("</" + tag + ">");

            if (spos != -1 && epos != -1 && spos < epos)
             {
                RetVal = content.Substring(spos + tag.Length + 2, epos - spos - tag.Length - 2);
                }
             return RetVal;
          }
        public bool GetStructueInformation(out string Error)
        {

            try
            {

                FolderPath = strSPath + "\\" + sISBN;
                if (!Directory.Exists(FolderPath))
                {
                    Error = "Invalid ISBN or Files not available for processing at \"" + FolderPath + "\"";
                    Console.WriteLine("Invalid ISBN or Files not available for processing at \"" + FolderPath + "\"");
                    return false;
                }
                else
                {
                    glFlow = "";
                    //Get PC Author/Editor Information
                    string meta = FolderPath + "\\AU_" + sISBN + ".csv";

                    if (!File.Exists(meta))
                    {
                        meta = FolderPath + "\\ED_" + sISBN + ".csv";
                        if (!File.Exists(meta))
                        {
                            meta = FolderPath + "\\PM_" + sISBN + ".csv";
                            if (!File.Exists(meta))
                            {
                                Error = "Metadata file not available at \"" + FolderPath + "\\" + sISBN + "\"";
                                Console.WriteLine("Metadata file not available at \"" + FolderPath + "\\" + sISBN + "\"");
                                return false;
                            }
                            else
                            {
                                glFlow = "PM";
                            }
                        }
                        else
                        {
                            glFlow = "EDF";
                        }
                    }
                    else
                    {
                        glFlow = "AUF";
                    }

                    //// edited by kshitij to validate the xml and csv in the isbn folder
                    //// if validation is returned false then do not execute the program any further.
                    if (!ValidateFolderStructure(FolderPath, sISBN))
                    {
                        Error = "Validation did not get cleared, mail sent to the dataset admin";
                        return false;
                    }

                    //StreamReader sr = new StreamReader(FolderPath + "\\" + sISBN);
                    StreamReader sr = new StreamReader(meta);
                    string FCon = sr.ReadToEnd();
                    sr.Close();

                    string[] SArr = new string[4];
                    SArr[0] = "\r\n";
                    SArr[1] = "\n\r";
                    SArr[2] = "\n";
                    SArr[3] = "\r";
                    BInfo = FCon.Split(SArr, StringSplitOptions.RemoveEmptyEntries);

                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    StreamReader srFI = new StreamReader(path + "\\" + "FContInfo.txt");
                    string strFI = srFI.ReadToEnd();
                    srFI.Close();

                    AInfo = strFI.Split(SArr, StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < AInfo.Length; j++)
                    {
                        if (AInfo[j].StartsWith("XML"))
                        {
                            strGXML = AInfo[j].Substring(4);
                        }
                        else if (AInfo[j].StartsWith("PDF"))
                        {
                            if (boolPDFLess == false)
                                strGPDF = AInfo[j].Substring(4);
                            else
                            {
                                if (DSubType == "chp" || DSubType == "itr" || DSubType == "idx")
                                    strGPDF = "";
                                else
                                    strGPDF = AInfo[j].Substring(4);
                                    
                            }
                        }
                        else if (AInfo[j].StartsWith("ASSET"))
                        {
                            strGAsset = AInfo[j].Substring(6);
                        }
                    }
                    Error = "";
                    return true;
                }
            }
            catch (Exception exe)
            {
                Error = exe.Message.ToString();
                return false;
            }
        }
        /// <summary>
        /// vlaidates the folder and xml are same as in the csv file that in the root folder
        /// </summary>
        /// <param name="folderPath">The isbn folder name </param>
        /// <returns></returns>
        private bool ValidateFolderStructure(string folderPath, string isbn)
        {
            //// check if the directory given exists
            
            try
             {
                if (Directory.Exists(folderPath))
                {
                    //// check if the csv file exists and read it .
                    string[] dir = Directory.GetFiles(folderPath, "*" + isbn + "*.csv");
                    if (dir.Count() > 0)
                    {
                        StreamReader reader = new StreamReader(File.OpenRead(Path.Combine(folderPath.Trim(), dir[0].Trim())));
                        List<string> allChapterIdinExcel = new List<string>();

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');
                            allChapterIdinExcel.Add(values[0].Trim());
                        }

                        reader.Close();
                        
                        reader.Dispose();
                        //// allchapters is the list of all the csv files in the folder remove the chpaterId
                        //// [0] is the name chapterId 
                        //// check the number of folder in the isbn folder

                        //if (!(Directory.GetDirectories(folderPath).Count() <= allChapterId.Count - 1))
                        //    throw new Exception();

                        //// getting all the folders in the foldername
                        List<string> allFolders = new List<string>();
                        allFolders.AddRange(Directory.GetDirectories(folderPath, "*", SearchOption.TopDirectoryOnly));

                        //// Now search if this folder exists in the fucking excel
                        List<string> unmatchedChapterId = new List<string>();

                        foreach ( string innerFolder  in allFolders)
                        {
                            string chapterName = innerFolder.Remove(0, innerFolder.LastIndexOf("\\") + 1).Trim();

                            if (allChapterIdinExcel.Contains(chapterName))
                            {
                                if (!string.IsNullOrWhiteSpace(ValidateXML(folderPath, chapterName)))
                                    unmatchedChapterId.Add(chapterName.Trim());
                            }
                            else
                                unmatchedChapterId.Add(chapterName.Trim());

                          }


                        //// id unmathedChapter is not null then send the mail to dataset admin

                        if (unmatchedChapterId.Count > 0)
                        {
                            string tomail = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["DataSetAdminMail"]);
                            if (!string.IsNullOrWhiteSpace(tomail))
                            {
                                string umchpId = string.Join("<br> <br>" + "Chapter ID ", unmatchedChapterId.ToArray());
                                string sub = "Failure: [" + isbn + "] Folder structure validation failed";
                                string body = "Dear ALL,<br/><br/>ISBN " + isbn + ": Validation Failed for below Chapter IDs <br/> <br/>" + umchpId + "<br>";
                                // footer
                                body += "_____________________________________________________________________________________________________________________";

                                body += "<br> <br> " + "This is an autogenerated email, Please do not respond.";
                                string cc = (string.IsNullOrWhiteSpace(GlobalConfig.oGlobalConfig.ERRCC)) ? GlobalConfig.oGlobalConfig.ERRCC : string.Empty;

                                Mailing mail = new Mailing();
                                if (!mail.SendMail(tomail, sub, body, cc, string.Empty, "elsbook.validation@thomsondigital.com", string.Empty))
                                    throw new Exception();
                            }

                            throw new Exception();
                        }

                    }
                    else throw new Exception();
                }
                else throw new Exception();

                //// end of try

                return true;

            }
            catch (Exception Ex)
            {
                MoveINIFile(folderPath, isbn);
                return false;
            }
            finally {
               
                
            }
        }

        private bool MoveINIFile(string folderPath, string isbn)
        {
            try
            {
                string iniPath = folderPath.Replace(isbn, string.Empty);
                

                //// validtion fail folder
                string validationFolder = Path.Combine(iniPath, "ValidationFail");
                if (!Directory.Exists(validationFolder))
                    Directory.CreateDirectory(validationFolder);

                iniPath = Path.Combine(iniPath, "Marker", isbn + ".ini");
                //string destination = Path.Combine(iniPath, "ValidationFail");
                

                File.Move(iniPath, Path.Combine(validationFolder, isbn+".ini" ));
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string ValidateXML(string xmlFolderPath, string chapterId)
        {
            
            try
            {
                if (!Directory.Exists(Path.Combine(xmlFolderPath, chapterId)))
                    throw new Exception();

                if (!Directory.Exists(Path.Combine(xmlFolderPath, chapterId, "xml")))
                    throw new Exception();

                if (!File.Exists(Path.Combine(xmlFolderPath, chapterId, "xml", "main.xml")))
                    throw new Exception();

                //// now we have to read the xml file 
                //// reading file but can't parse ce: tags
                //// XDocument doc = XDocument.Load(Path.Combine(xmlFolderPath, chapterId ,"xml", "main.xml"));
                //// string chapterid = doc.Descendants("chapter").Where(r => r.Attribute("docsubtype").Value.Trim().Contains("chp")).Select(r => r.Attribute("id").Value).FirstOrDefault();


                XmlTextReader myTextReader = new XmlTextReader(Path.Combine(xmlFolderPath, chapterId, "xml", "main.xml"));
                myTextReader.XmlResolver = null;
                myTextReader.DtdProcessing = DtdProcessing.Ignore;
                myTextReader.WhitespaceHandling = WhitespaceHandling.None;

                GlobalConfig._strPIT = "";

                string chpId = string.Empty;
                while (myTextReader.Read())
                {
                    if (myTextReader.LocalName == "chapter"
                        && myTextReader.HasAttributes
                        && myTextReader.GetAttribute("docsubtype").ToString() == "chp")
                    {
                        GlobalConfig._strPIT = "CHP";
                        chpId = myTextReader.GetAttribute("id").ToString();
                        break;
                    }
                    else if ((myTextReader.LocalName == "fb-non-chapter" || myTextReader.LocalName == "index")
                        && myTextReader.HasAttributes
                        && myTextReader.GetAttribute("docsubtype").ToString() == "idx")
                    {
                        GlobalConfig._strPIT = "IDX";
                        chpId = myTextReader.GetAttribute("id").ToString();

                        break;
                    }
                    else if (myTextReader.LocalName == "introduction"
                        && myTextReader.HasAttributes
                        && myTextReader.GetAttribute("docsubtype").ToString() == "itr")
                    {
                        GlobalConfig._strPIT = "ITR";
                        chpId = myTextReader.GetAttribute("id").ToString();
                        break;
                    }
                    else if (myTextReader.LocalName == "fb-non-chapter"
                        && myTextReader.HasAttributes
                        && myTextReader.GetAttribute("docsubtype").ToString() == "bib")
                    {
                        GlobalConfig._strPIT = "BIB";
                        chpId = myTextReader.GetAttribute("id").ToString();
                        break;
                    }
                    else if (myTextReader.LocalName == "fb-non-chapter"
                        && myTextReader.HasAttributes
                        && myTextReader.GetAttribute("docsubtype").ToString() == "app")
                    {
                        GlobalConfig._strPIT = "APP";
                        chpId = myTextReader.GetAttribute("id").ToString();
                        break;
                    }
                    else if (myTextReader.LocalName == "fb-non-chapter"
                      && myTextReader.HasAttributes
                      && myTextReader.GetAttribute("docsubtype").ToString() == "ctr")
                    {
                        GlobalConfig._strPIT = "CTR";
                        chpId = myTextReader.GetAttribute("id").ToString();
                        break;
                    }
                }

                myTextReader.Close();
                myTextReader.Dispose();
                //// Check if both chapterId are equal.
                if (!chapterId.Equals(chpId, StringComparison.InvariantCultureIgnoreCase))
                    return chapterId;
                else
                    return string.Empty;

            }
            catch
            {
                return chapterId;
            }
            finally {
                //
                
            }

        }

        public bool CheckPDFLess(string ISBN)
        {
            if (ISBN == null)
            {
                return false;
            }
            if (ISBN.Length != 13)
            {
                return false;
            }

            string OPath = System.Configuration.ConfigurationSettings.AppSettings["OrdePath"] + "\\" + ISBN;

            if (Directory.Exists(OPath))
            {
                DirectoryInfo[] DInfo;
                DInfo = new DirectoryInfo(OPath).GetDirectories("*TYPESET*");
                if (DInfo.Length == 0)
                {
                    DInfo = new DirectoryInfo(OPath).GetDirectories("*FULL-SERVICE-ORDER*");
                }
             
                if (DInfo.Length > 0)
                {
                    FileInfo[] FInfo = new DirectoryInfo(DInfo[0].FullName + "\\Current_order").GetFiles("kup*.xml");
                    if (FInfo.Length > 0)
                    {
                        string FileC = System.IO.File.ReadAllText(FInfo[0].FullName);
                        string OrderTemplate = GetInformation(FileC, "text-design-type");
                        string strTemplate = System.Configuration.ConfigurationSettings.AppSettings["Template"];

                        string[] TemplateArr = strTemplate.Split(',');

                        for (int j = 0; j < TemplateArr.Length; j++)
                        {
                            if (TemplateArr[j] == null)
                                continue;
                            else if (TemplateArr[j].Length == 0)
                                continue;
                            else if (OrderTemplate.IndexOf(TemplateArr[j]) != -1)
                            {
                                return true;
                            }                                
                        }
                    }
                }

            }
            else
                return false;
            return false;       

        }

    }
}
