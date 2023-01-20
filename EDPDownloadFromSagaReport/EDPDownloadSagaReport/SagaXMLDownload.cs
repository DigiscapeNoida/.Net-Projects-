using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using EDPCommonDetails;
using EDPSagaLog;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using iTextSharp.text.pdf;
using System.Threading;

namespace EDPDownloadSagaReport
{
	class SagaXMLDownload
	{
		string FtpURL = System.Configuration.ConfigurationSettings.AppSettings["FtpURL"];
		string _UserName = System.Configuration.ConfigurationSettings.AppSettings["UID"];
		string _Password = System.Configuration.ConfigurationSettings.AppSettings["PWD"];

		public static OleDbConnection OledbConn;
		public static List<string> GetDataFromCsv = new List<string>();
		public static string ITEMCODE = "";
		StreamWriter sw;

		public static List<string> listB = new List<string>();

		System.Timers.Timer oTimer = null;
		double interval = 15 * 60 * 1000;
		//double interval = 200000;

		public static string strFile = "";

		public void Process_Initiate()
		{
			//private void oTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
			//{
			string AIDJID = string.Empty;
			try
			{
				//string[] lines = System.IO.File.ReadAllLines(@"D:\JurnalList.txt"); // has to change to orignal before deployment
				//Jitender 2018-06-08
				string[] lines = System.IO.File.ReadAllLines(System.Configuration.ConfigurationSettings.AppSettings["JIDList"]); // has to change to orignal before deployment

				//string[] lines = System.IO.File.ReadAllLines(@"Z:\JurnalList.txt"); // has to change to orignal before deployment
				string FinalContent = "";
				foreach (string val in lines)
				{

					string[] parameter = val.Split('/');
					//string[] strAllStage = { "wait_author_proof_file", "author_proof_being_corrected_by_typesetter", "wait_final_proof_file", "final_proof_being_corrected_by_typesetter", "efirst_files_preparation", "wait_final_pdf_preparation", "intermediate_proof_awaiting_validation" };
					string strS = System.Configuration.ConfigurationSettings.AppSettings["Stages"];
					string[] strAllStage = strS.Split(',');

					foreach (string onestage in strAllStage)
					{
						Console.WriteLine(val + ":" + onestage);
						;
						try
						{
                            //string url = "https://saga.edpsciences.org/api/1.0/'" + parameter[0] + "'/list/bystatus/status/'" + onestage + "'/_sk/9gnNeUQVfjL0wk8%2B%2B8r2ZcjFCYuuE88TK276QxPMQgKfXrk%2BbeLsL8tlYCSUPOtD22t3n7dvDzVOwlhEbRS3QA%3D%3D";
                            string url = "https://saga.edpsciences.org/api/1.0/'" + parameter[0] + "'/list/bystatus/status/'" + onestage + "'/_sk/9gnNeUQVfjL0wk8%2B%2B8r2ZcjFCYuuE88TK276QxPMQgLLo%2FbNCrLtcFtAfCwh7%2F608CGr3mY1t01ruwuzIMJN3w%3D%3D";
                            url = url.Replace("'", string.Empty);
							WebRequest request = HttpWebRequest.Create(url);
							WebResponse response = request.GetResponse();
							StreamReader reader = new StreamReader(response.GetResponseStream());
							string strXmltext = reader.ReadToEnd();
							XmlTextReader myTextReader = new XmlTextReader(reader);
							//   myTextReader.XmlResolver = null;
							//  myTextReader.DtdProcessing = DtdProcessing.Ignore;
							// myTextReader.WhitespaceHandling = WhitespaceHandling.None;


							string strJID = parameter[0];
							string strStage = onestage;
							string strContent = GetArrInfo(strXmltext);

							FinalContent = FinalContent + "<FC>" + strJID + "<S>" + strStage + "<S>" + strContent;

							//



							//


						}
						catch (Exception ex)
						{
							LogHelper.WriteLog(ex.Message + " " + AIDJID, LogType.Error);
						}
					}
				}

				GenerateCSVFile(FinalContent);
				Missing_Items();

			}
			catch (Exception ex)
			{
				LogHelper.WriteLog(ex.Message + " " + AIDJID, LogType.Error);
			}
		}

		public bool Download(string DownloadFilePath, string FTPFileURL)
		{

			FtpWebRequest reqFTP;
			try
			{
                //filePath = <<The full path where the file is to be created.>>, 
                //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>				
                //if (FTPFileURL== "ftp://ftp.edpsaga.org/from_edp\\bsgf\\bsgf210005.zip" || FTPFileURL== "ftp://ftp.edpsaga.org/from_edp\\ocl\\ocl210062.zip")
                //{ }
				reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPFileURL));
				reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
				reqFTP.UseBinary = true;
				reqFTP.Credentials = new NetworkCredential(_UserName, _Password);
				reqFTP.Timeout = -1;
				//reqFTP.KeepAlive = true;
				try
				{
					FtpWebResponse response1 = (FtpWebResponse)reqFTP.GetResponse();
				}
				catch (WebException ex)
				{
					FtpWebResponse response1 = (FtpWebResponse)ex.Response;
					if (response1.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
					{
						return false;
					}
				}
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				Stream ftpStream = response.GetResponseStream();
				long cl = response.ContentLength;
				int bufferSize = 2048;
				int readCount;
				byte[] buffer = new byte[bufferSize];
				readCount = ftpStream.Read(buffer, 0, bufferSize);
				FileStream outputStream = new FileStream(DownloadFilePath, FileMode.Create);
				while (readCount > 0)
				{
					outputStream.Write(buffer, 0, readCount);
					readCount = ftpStream.Read(buffer, 0, bufferSize);
				}

				ftpStream.Close();
				ftpStream = null;
				outputStream.Close();
				outputStream = null;
				response.Close();
				response = null;

				FileInfo Fi = new FileInfo(DownloadFilePath);
				if (Fi.Length == 0)
				{
					return false;
				}
				//ProcessEventHandler("Finish Download");

				return true;
			}
			catch (Exception ex)
			{
				// ProcessErrorHandler(ex);
				//_ErrorMsg = ex.Message;
				//System. MessageBox.Show(ex.Message);
			}
			return false;
		}

		public void GenerateCSVFile(string FinalContent)
		{
			try
			{

				strFile = System.Configuration.ConfigurationSettings.AppSettings["ReportPath"];
				strFile = strFile + "EDP_Report_" + System.DateTime.Now.ToString().Replace(" ", "-").Replace("/", "-").Replace(":", "-") + ".csv";


				string strStageValue = System.Configuration.ConfigurationSettings.AppSettings["Display_Stages"];


				//sw = new StreamWriter(strFile);

				//sw.WriteLine("List of Aricles:" + System.DateTime.Now.ToString());
				//sw.WriteLine("JID,Stage,AID,Status_Date,Manuscript Pages,Pdf Pages,Figures");

				string[] SplitOpt = new string[1];
				SplitOpt[0] = "<FC>";

				string[] Arr = FinalContent.Split(SplitOpt, StringSplitOptions.RemoveEmptyEntries);
				DataTable dt = new DataTable();
				dt.Columns.Add(new DataColumn("c1"));
				dt.Columns.Add(new DataColumn("c2"));
				dt.Columns.Add(new DataColumn("c3"));
				dt.Columns.Add(new DataColumn("c4"));
				dt.Columns.Add(new DataColumn("c5"));
				dt.Columns.Add(new DataColumn("c6"));
				dt.Columns.Add(new DataColumn("c7"));
				for (int i = 0; i < Arr.Length; i++)
				{
					string[] SplitOpt1 = new string[1];
					SplitOpt1[0] = "<S>";

					string[] ItemS = Arr[i].Split(SplitOpt1, StringSplitOptions.RemoveEmptyEntries);

					if (ItemS.Length == 3)
					{
						string[] SplitOpt2 = new string[1];
						SplitOpt2[0] = "<A>";
						string[] ItemA = ItemS[2].Split(SplitOpt2, StringSplitOptions.RemoveEmptyEntries);

						for (int j = 0; j < ItemA.Length; j++)
						{
							string strAID, strStatusDate, strmpc, strppc, strfc;
							strAID = strStatusDate = strmpc = strppc = strfc = "";
							GetItem(ItemA[j], out strAID, out strStatusDate, out strmpc, out strppc, out strfc);

							//condition

							if (strppc == "")
							{
								if (Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\temp"))
								{
									Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\temp", true);
								}
								if (Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\location"))
								{
									Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\location", true);
								}
								if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\temp"))
								{
									Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\temp");
								}

								if (Download(System.Windows.Forms.Application.StartupPath + "\\temp" + "\\" + strAID + ".zip", FtpURL + "\\" + ItemS[0] + "\\" + strAID + ".zip"))
								{
									//unzip

									unzip(System.Windows.Forms.Application.StartupPath + "\\temp" + "\\" + strAID + ".zip");


									string[] locationtocheck = Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "\\location" + "\\" + strAID);

									//check pdf
									DirectoryInfo d = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\location" + "\\" + strAID);
									FileInfo[] pdfFiles = d.GetFiles("*.pdf");
									FileInfo[] docFiles = d.GetFiles("*.doc");
									FileInfo[] docxFiles = d.GetFiles("*.docx");

									//page count of max pdf
									//if pdf not exist

									if (pdfFiles.Count() > 0)
									{
										List<int> termsList = new List<int>();
										int count;
										foreach (var file in locationtocheck)
										{
											if (file.ToString().EndsWith(".pdf"))
											{
												count = pdfpagecount(file);
												termsList.Add(count);
											}
										}

										strppc = termsList.Max().ToString();
									}

									//check docx
									//page count of max doc

									else if (docFiles.Count() > 0 || docxFiles.Count() > 0)
									{
										List<int> termsList = new List<int>();
										int count;
										foreach (string file in locationtocheck)
										{
											if (file.ToString().EndsWith(".docx"))
											{
												count = docpagecount(file);
												termsList.Add(count);
											}
										}
										foreach (string file in locationtocheck)
										{
											if (file.ToString().EndsWith(".doc"))
											{
												count = docpagecount(file);
												termsList.Add(count);
											}
										}
										strppc = termsList.Max().ToString();

										//Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\temp", true);
										//Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\location", true);
									}									
								}
								//page count = strppc
								//delete temp directory
							}
							//condition
							string strS = ItemS[1];

							if (ItemS[1] == "wait_author_proof_file")       //S100
								strS = "proof in preparation";
							else if (ItemS[1] == "author_proof_being_corrected_by_typesetter")  //S100 Resupply
								strS = "proof being corrected by the typesetter";
							else if (ItemS[1] == "wait_final_proof_file")   //S200
								strS = "intermediate proof in preparation";
							else if (ItemS[1] == "final_proof_being_corrected_by_typesetter") // S200 Resupply
								strS = "intermediate proof being corrected by the typesetter";
							else if (ItemS[1] == "wait_final_pdf_preparation")  //S275
								strS = "final version beeing prepared by the typesetter";
							else if (ItemS[1] == "efirst_files_preparation")  //S250
								strS = "e-first files being prepared by the typesetter";
							DataRow dr = dt.NewRow();
							dr["c1"] = ItemS[0];
							dr["c2"] = strS;
							dr["c3"] = strAID;
							dr["c4"] = strStatusDate;
							dr["c5"] = strmpc;
							dr["c6"] = strppc;
							dr["c7"] = strfc;
							dt.Rows.Add(dr);
							//sw.WriteLine(ItemS[0] + "," + strS + "," + strAID + ",'" + strStatusDate + ",'" + strmpc + ",'" + strppc + ",'" + strfc);
						}
					}
				}
				sw = new StreamWriter(strFile);
				sw.WriteLine("List of Aricles:" + System.DateTime.Now.ToString());
				sw.WriteLine("JID,Stage,AID,Status_Date,Manuscript Pages,Pdf Pages,Figures");
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					sw.WriteLine(dt.Rows[i]["c1"].ToString() + "," + dt.Rows[i]["c2"].ToString() + "," + dt.Rows[i]["c3"].ToString() + ",'" + dt.Rows[i]["c4"].ToString() + ",'" + dt.Rows[i]["c5"].ToString() + ",'" + dt.Rows[i]["c6"].ToString() + ",'" + dt.Rows[i]["c7"].ToString());
				}
				sw.WriteLine("List" + System.DateTime.Now.ToString());
				sw.WriteLine();
				sw.Close();
			}
			catch (Exception Ex)
			{ Console.WriteLine(Ex.Message.ToString());}
		}

		public static int docpagecount(string filepath)
		{
			Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
			Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(filepath);
			Microsoft.Office.Interop.Word.WdStatistic stat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;
			object missing = System.Reflection.Missing.Value;
			int num = doc.ComputeStatistics(stat, ref missing);
			doc.Close();
			doc = null;
			app = null;

			return num;
		}

		public static int pdfpagecount(string filepath)
		{
			//pdf page count
			string ppath = filepath;
			PdfReader pdfReader = new PdfReader(ppath);
			int numberOfPages = pdfReader.NumberOfPages;

			pdfReader.Close();
			return numberOfPages;
		}

		public static void unzip(string filepath)
		{
			System.IO.Compression.ZipFile.ExtractToDirectory(filepath, @"location\");
		}

		public void GetItem(string article, out string strAID, out string strSD, out string mpc, out string ppc, out string fc)
		{
			strAID = "";
			strSD = "";
			mpc = ppc = fc = "";

			try
			{


				int spos = 0;
				int epos = 0;

				/* <article pub-id-type="arxiv" doi="10.1051/0004-6361/201731667" reference="aa31667-17">
                    <count count-type="manuscript-page-count" count="41"/><fig-count count="41"/>
                    <table-count count="3"/>
                    <page-count count="41"/>
                 <status-date>2018-10-03 18:10:23.943088</status-date></article>*/


				spos = article.IndexOf("reference");
				if (spos != -1)
					spos = spos + 11;
				epos = article.IndexOf("\"", spos + 1);
				strAID = article.Substring(spos, epos - spos);

				spos = article.IndexOf("manuscript-page-count");
				if (spos != -1)
				{
					spos = article.IndexOf("count=\"", spos + 1);
					epos = article.IndexOf("\"", spos + 7);
					spos = spos + 7;
					mpc = article.Substring(spos, epos - spos);
				}

				spos = article.IndexOf("fig-count");
				if (spos != -1)
				{
					spos = article.IndexOf("count=\"", spos + 1);
					epos = article.IndexOf("\"", spos + 7);
					spos = spos + 7;
					fc = article.Substring(spos, epos - spos);
				}

				spos = article.IndexOf("<page-count");
				if (spos != -1)
				{
					spos = article.IndexOf("count=\"", spos + 1);
					epos = article.IndexOf("\"", spos + 7);
					spos = spos + 7;
					ppc = article.Substring(spos, epos - spos);
				}







				spos = article.IndexOf("<status-date>");
				if (spos != -1)
					spos = spos + 13;
				epos = article.IndexOf(">", spos + 1);
				string strSD1 = article.Substring(spos, epos - spos - 14);

				//manoj
				string[] arr = strSD1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
				strSD = arr[0];

			}
			catch (Exception Ex)
			{ }
		}


		public string GetArrInfo(string strArr)
		{

			try
			{
				string TempS = "";

				int sPos = 0, ePos = -1;

				sPos = strArr.IndexOf("<article ");

				while (sPos != -1)
				{
					ePos = strArr.IndexOf("</article>", sPos);
					if (ePos == -1)
						break;

					if (ePos < sPos)
						break;

					TempS = TempS + "<A>" + strArr.Substring(sPos, ePos - sPos + 10);

					sPos = ePos;
					sPos = strArr.IndexOf("<article ", sPos + 1);
				}

				return TempS;

			}
			catch (Exception Ex)
			{ return ""; }
		}


		static void Missing_Items()
		{

			try
			{

				StreamReader sr = new StreamReader("EDPPaginationType.txt");
				string FileC = sr.ReadToEnd();
				sr.Close();

				string strItems = "";
				string TempString = "";

				//string newstrFile = strFile;
				using (var reader = new StreamReader(strFile))
				{
					while (!reader.EndOfStream)
					{
						string line = reader.ReadLine();
						string[] SplitStr = line.Split(',');


						//MNTFOLDER= "MNT_EDP_JOURNAL_"+ +"_110";
						//GetDataFromCsv.Add(line);

						if ((line.Contains(",")) && (line.Contains("AID") == false) && (line.Contains("List of Aricles") == false))
						{
							if (line.StartsWith("mi"))
							{
								Console.WriteLine("AP");
							}

							string[] SplitStr1 = line.Split(',');
							string str = SplitStr1[2].Replace(SplitStr1[0], SplitStr1[0] + "_");

							if (str.IndexOf("_") == -1)
								str = str.Replace("ap", "ap_");

							if (str.IndexOf("_") == -1)
								str = str.Replace("pv", "epjpv_");

							if (str.IndexOf("_") == -1)
								str = str.Replace("b", "epjb_");

							if (str.IndexOf("_") == -1)
								str = str.Replace("d", "epjd_");

							if (str.IndexOf("_") == -1)
								str = str.Replace("h", "epjh_");

							if (str.IndexOf("_") == -1)
								str = str.Replace("mi", "mi_");

							if (str.IndexOf("_") == -1)
								str = str.Replace("mt", "mt_");

							strItems = strItems + "," + "'MNT_EDP_JOURNAL_" + str.ToUpper() + "_110'";

							TempString = TempString + "," + strItems + "|" + SplitStr1[1];

							//validatedata(line);
						}


					}
				}

				strItems = strItems.ToUpper();

				if (strItems.StartsWith(","))
					strItems = strItems.Substring(1);
				string ListValues = validatedata1(strItems, "").ToUpper();

				string NewItemsText = "";
				int nicnt = 0;

				string[] LV = ListValues.Split(',');

				string[] srcItems = strItems.Split(',');

				for (int i = 0; i < srcItems.Length; i++)
				{
					string s = srcItems[i].Replace("'", "");

					if (ListValues.Contains(s) == false)
					{
						string[] strA = srcItems[i].Split('_');

						string sjid = strA[3];

						string strPP = "";
						if (FileC.IndexOf("\n" + sjid) != -1)
						{
							int sspos = FileC.IndexOf("\n" + sjid);
							if (sspos != -1)
							{
								int eepos = FileC.IndexOf("\n", sspos + 1);
								if (eepos == -1)
								{
									strPP = FileC.Substring(sspos + sjid.Length + 1).Replace(",", "").Replace("\r", ""); ;
								}
								else
								{
									strPP = FileC.Substring(sspos + sjid.Length + 1, eepos - sspos - sjid.Length - 1).Replace(",", "").Replace("\r", "");
								}
								//  strSt = TempString.Substring(TempString.IndexOf(srcItems[i]) + TempString.Length,
							}
						}


						NewItemsText = NewItemsText + "*" + srcItems[i] + "," + strPP; nicnt++;
					}
				}


				if (strItems.StartsWith(","))
					strItems = strItems.Substring(1);
				ListValues = validatedata1(strItems, "Delivery").ToUpper();

				string RevisedItemsText = "";
				int ricnt = 0;

				srcItems = strItems.Split(',');

				for (int i = 0; i < srcItems.Length; i++)
				{
					string s = srcItems[i].Replace("'", "");
					if (ListValues.Contains(s) == true)
					{
						string strSt = "";
						if (TempString.IndexOf(srcItems[i]) != -1)
						{
							int sspos = TempString.IndexOf(srcItems[i]);
							if (sspos != -1)
							{
								int eepos = TempString.IndexOf(",", sspos + 1);
								if (eepos == -1)
								{
									strSt = TempString.Substring(sspos + srcItems[i].Length + 1);
								}
								else
								{
									strSt = TempString.Substring(sspos + srcItems[i].Length + 1, eepos - sspos - srcItems[i].Length - 1);
								}
								//  strSt = TempString.Substring(TempString.IndexOf(srcItems[i]) + TempString.Length,
							}
						}

						string[] strA = srcItems[i].Split('_');

						string sjid = strA[3];

						string strPP = "";
						if (FileC.IndexOf("\n" + sjid) != -1)
						{
							int sspos = FileC.IndexOf("\n" + sjid);
							if (sspos != -1)
							{
								int eepos = FileC.IndexOf("\n", sspos + 1);
								if (eepos == -1)
								{
									strPP = FileC.Substring(sspos + sjid.Length + 1).Replace(",", "").Replace("\r", "");
								}
								else
								{
									strPP = FileC.Substring(sspos + sjid.Length + 1, eepos - sspos - sjid.Length - 1).Replace(",", "").Replace("\r", ""); ;
								}
								//  strSt = TempString.Substring(TempString.IndexOf(srcItems[i]) + TempString.Length,
							}
						}

						RevisedItemsText = RevisedItemsText + "*" + srcItems[i] + "," + strSt + "," + strPP; ricnt++;
					}
				}

				StreamWriter sw = new StreamWriter(strFile, true);

				string strRP = System.Configuration.ConfigurationSettings.AppSettings["ReportPath"];

				StreamWriter sw1 = new StreamWriter(strRP + "NewList.txt");

				sw.WriteLine("\n\n\n");
				sw.WriteLine("New Items Not Integrated");
				sw.WriteLine("Total Items: " + nicnt);
				sw.WriteLine(NewItemsText.Replace("*", "\n"));

				sw1.WriteLine(NewItemsText.Replace("*", "\n"));

				sw.WriteLine("\n\n\n");
				sw.WriteLine("Revised Items Not Integrated");
				sw.WriteLine("Total Items: " + ricnt);
				sw.WriteLine(RevisedItemsText.Replace("*", "\n"));
				sw1.WriteLine(RevisedItemsText.Replace("*", "\n"));
				sw.Close();
				sw1.Close();

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.Read();
			}

			//   CreateCsvFile();

		}
		private static bool CreateCsvFile()
		{
			try
			{
				string CsvFilesDir = AppDomain.CurrentDomain.BaseDirectory + "CsvFiles";
				string CSVFile = CsvFilesDir + "\\" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".csv";

				StringBuilder CsvStr = new StringBuilder();

				if (!(Directory.Exists(CsvFilesDir)))
				{
					Directory.CreateDirectory(CsvFilesDir);
				}


				if (listB.Count > 0)
				{
					CsvStr.AppendLine("JID,Stage,AID,Status_Date");
					foreach (string item in listB)
					{


						string[] str = item.Split(',');

						CsvStr.AppendLine(str[0] + "," + str[1].Replace("\r", "") + "," + str[2] + "," + str[3].Replace("\r", ""));
					}

					if (!string.IsNullOrEmpty(CSVFile))
					{
						if (File.Exists(CSVFile))
						{
							File.Delete(CSVFile);
						}
						File.AppendAllText(CSVFile, CsvStr.ToString());
					}


				}
				else
				{

				}
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static string validatedata1(string DataFromCsv, string strDelivery)
		{

			string strRetVal = "";
			try
			{
				// string[] SplitStr = DataFromCsv.Split(',');

				//                string str = SplitStr[2].Replace(SplitStr[0], SplitStr[0] + "_");
				//              ITEMCODE = "MNT_EDP_JOURNAL_" + str + "_110";
				string Connection = System.Configuration.ConfigurationSettings.AppSettings["EDPConnectionString1"];
				SqlConnection con = new SqlConnection(Connection);

				string query = "select itemcode from ARTICLEDETAILS where ITEMCODE in (" + DataFromCsv.ToUpper() + ")";
				if (strDelivery.Length > 0)
				{
					query = query + " and pse_stageid = 'ST1019'";
				}
				SqlCommand cmd = new SqlCommand(query, con);
				con.Open();
				try
				{
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					// this will query your database and return the result to your datatable
					DataTable dataTable = new DataTable();
					da.Fill(dataTable);

					for (int x = 0; x < dataTable.Rows.Count; x++)
					{
						strRetVal = strRetVal + "," + dataTable.Rows[x][0];
					}
					/*
                        if (dataTable.Rows.Count > 0)
                        {
                            listB.Add(DataFromCsv);
                        }*/
					con.Close();
					da.Dispose();
					return strRetVal;
				}
				catch (Exception ex)
				{
					return "";
				}
			}
			catch (Exception ex)
			{
				return "";
			}
		}
	}
}
