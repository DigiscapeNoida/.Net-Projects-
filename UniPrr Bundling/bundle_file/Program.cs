using System;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace bundle_file
{
	class Program
	{
		static Log log = new Log();
		static bool isok = true;
		static void Main(string[] args)
		{
			try
			{
				killword();
				GC.Collect();
				bundle1 p1 = new bundle1(args[0]);
				Thread bd = new Thread(new ParameterizedThreadStart(bundling));
				bd.Start(p1);				
			}
			catch
			{
			}
		}
		static void bundling(object bundledir)
		{
			bundle1 data = (bundle1)bundledir;
			try
			{
				if (Directory.Exists(System.Configuration.ConfigurationSettings.AppSettings["in_path"] + "\\" + data.dir_name))
				{
					log.Generatelog("Processing directory " + data.dir_name);
					bundle p = new bundle(data.dir_name);
					var tsk_b = Task.Run(() => _bundling(p));
					tsk_b.Wait();
					p = null;
					GC.Collect();
				}
				else
				{
					log.Generatelog("Directory not found " + System.Configuration.ConfigurationSettings.AppSettings["in_path"] + "\\" + data.dir_name);
					log.Errorlog("Directory not found " + System.Configuration.ConfigurationSettings.AppSettings["in_path"] + "\\" + data.dir_name);
					isok = false;
				}
			}
			catch (Exception ex)
			{
				log.Generatelog("Error found " + ex.Message.ToString());
				log.Errorlog("Error found in directory " + data.dir_name + ". " + ex.Message.ToString());
				isok = false;
			}
			if (isok == true)
			{
				log.Generatelog("Task completed successfully for " + data.dir_name);
				log.Generatelog("Deleting directory " + System.Configuration.ConfigurationSettings.AppSettings["in_path"] + "\\" + data.dir_name);
				try
				{
					Directory.Delete(System.Configuration.ConfigurationSettings.AppSettings["in_path"] + "\\" + data.dir_name, true);
					Directory.Delete(System.Configuration.ConfigurationSettings.AppSettings["out_path"] + "\\" + data.dir_name + "\\input", true);
				}
				catch
				{
				}
			}
			else
			{
				log.Generatelog("Task failed for " + data.dir_name);
				log.Errorlog("Moving directory to fail location.");
				string today = System.Configuration.ConfigurationSettings.AppSettings["fail_dir"] + "\\" + DateTime.Now.Date.ToString("dd-MM-yyyy");
				if (!Directory.Exists(today))
				{
					Directory.CreateDirectory(today);
				}
				
				if (Directory.Exists(today + "\\" + data.dir_name))
				{
					try
					{
						Directory.Delete(today + "\\" + data.dir_name, true);
					}
					catch
					{
					}
				}
				try
				{
					Directory.CreateDirectory(today + "\\" + data.dir_name);
					foreach (string dirPath in Directory.GetDirectories(System.Configuration.ConfigurationSettings.AppSettings["in_path"] + "\\" + data.dir_name, "*", SearchOption.AllDirectories))
					{
						Directory.CreateDirectory(dirPath.Replace(System.Configuration.ConfigurationSettings.AppSettings["in_path"] + "\\" + data.dir_name, today + "\\" + data.dir_name));
					}
					foreach (string newPath in Directory.GetFiles(System.Configuration.ConfigurationSettings.AppSettings["in_path"] + "\\" + data.dir_name, "*.*", SearchOption.AllDirectories))
					{
						File.Copy(newPath, newPath.Replace(System.Configuration.ConfigurationSettings.AppSettings["in_path"] + "\\" + data.dir_name,
today + "\\" + data.dir_name), true);
					}
					Directory.Delete(System.Configuration.ConfigurationSettings.AppSettings["in_path"] + "\\" + data.dir_name, true);
					if (Directory.Exists(System.Configuration.ConfigurationSettings.AppSettings["out_path"] + "\\" + data.dir_name))
					{
						Directory.Delete(System.Configuration.ConfigurationSettings.AppSettings["out_path"] + "\\" + data.dir_name, true);
					}
				}
				catch
				{
				}
			}
		}
		static void _bundling(object bundledata)
		{
			bundle data = (bundle)bundledata;
			if (filecheck(data.sourcePath) == true)
			{
				if (Directory.Exists(data.targetPath))
				{
					try
					{
						Directory.Delete(data.targetPath, true);
					}
					catch (Exception ex)
					{
						log.Generatelog("Error found in deleting directory " + data.targetPath + ". " + ex.Message.ToString());
						log.Errorlog("Error found in deleting directory " + data.targetPath + ". " + ex.Message.ToString());
						isok = false;
					}
				}
				if (isok == true)
				{
					try
					{
						Directory.CreateDirectory(data.targetPath);
						foreach (string dirPath in Directory.GetDirectories(data.sourcePath, "*", SearchOption.AllDirectories))
						{
							Directory.CreateDirectory(dirPath.Replace(data.sourcePath, data.targetPath));
						}
						foreach (string newPath in Directory.GetFiles(data.sourcePath, "*.*", SearchOption.AllDirectories))
						{
							File.Copy(newPath, newPath.Replace(data.sourcePath, data.targetPath), true);
						}
					}
					catch (Exception ex)
					{
						log.Generatelog("Error found in " + data.targetPath + " " + ex.Message.ToString());
						log.Errorlog("Error found in " + data.targetPath + " " + ex.Message.ToString());
						isok = false;
					}
					if (isok == true)
					{
						string[] files = File.ReadAllLines(data.targetPath + "\\input\\order.txt");
						try
						{
							var tsk1 = Task.Run(() => Doctodocx(data.targetPath + "\\input"));
							tsk1.Wait();
						}
						catch (Exception ex)
						{
							log.Generatelog("Error found in converting doc to docx " + Path.GetFileNameWithoutExtension(data.targetPath) + ". " + ex.Message.ToString());
							log.Errorlog("Error found in converting doc to docx " + Path.GetFileNameWithoutExtension(data.targetPath) + ". " + ex.Message.ToString());
							isok = false;
						}
                        try
                        {
                            var tskuri = Task.Run(() => FixUri(data.targetPath + "\\input"));
                            tskuri.Wait();
                        }
                        catch
                        {                            
                        }
                        if (isok == true)
						{
							string pap = "";
							if (files.Count() > 1)
							{
								for (int j = 0; j < files.Count(); j++)
								{
									if (files[j].Trim().ToLower().EndsWith(".docx"))
									{
										if (pap == "")
										{
											pap = data.targetPath + "\\input\\" + files[j].Trim().ToLower();
										}
										else
										{
											pap = pap + "|" + data.targetPath + "\\input\\" + files[j].Trim().ToLower();
										}
									}
									else if (files[j].Trim().ToLower().EndsWith(".doc"))
									{
										if (pap == "")
										{
											pap = data.targetPath + "\\input\\" + files[j].Trim().ToLower().Replace(".doc", ".docx");
										}
										else
										{
											pap = pap + "|" + data.targetPath + "\\input\\" + files[j].Trim().ToLower().Replace(".doc", ".docx");
										}
									}
									else
									{
										try
										{
											var tsk2 = Task.Run(() => Add_image(data.targetPath + "\\input\\" + Path.GetFileNameWithoutExtension(data.targetPath + "\\input\\" + files[j].Trim().ToLower()) + ".docx", data.targetPath + "\\input\\" + files[j].Trim()));
											tsk2.Wait();
										}
										catch (Exception ex)
										{
											if (ex.Message.ToString().Contains("This is not a valid file name") == false)
											{
												log.Generatelog("Error found in adding image " + Path.GetFileNameWithoutExtension(data.targetPath) + ". " + ex.Message.ToString());
												log.Errorlog("Error found in adding image " + Path.GetFileNameWithoutExtension(data.targetPath) + ". " + ex.Message.ToString());
												isok = false;
											}
										}
										if (isok == true)
										{
											if (pap == "")
											{
												pap = data.targetPath + "\\input\\" + Path.GetFileNameWithoutExtension(data.targetPath + "\\input\\" + files[j].Trim().ToLower()) + ".docx";
											}
											else
											{
												pap = pap + "|" + data.targetPath + "\\input\\" + Path.GetFileNameWithoutExtension(data.targetPath + "\\input\\" + files[j].Trim().ToLower()) + ".docx";
											}
										}
									}
								}
							}
							if (isok == true)
							{
								if (Directory.Exists(data.targetPath + "\\output"))
								{
									try
									{
										Directory.Delete(data.targetPath + "\\output", true);
									}
									catch (Exception ex)
									{
										log.Generatelog("Error found in deleting directory " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output. " + ex.Message.ToString());
										log.Errorlog("Error found in deleting directory " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output. " + ex.Message.ToString());
										isok = false;
									}
								}
								if (isok == true)
								{
									Directory.CreateDirectory(data.targetPath + "\\output");
									if (pap != "")
									{
										string[] outfile = pap.Split('|');
										if (outfile.Count() > 1)
										{
											log.Generatelog("Creating bundle files : " + data.targetPath + "\\output");
											try
											{
												if (outfile.Count() > 2)
												{
													string ofile = data.targetPath + "\\output\\thomtest.docx";
													string f_file = "";
													for (int i = 1; i < outfile.Count(); i++)
													{
														if (i == 1)
														{
															f_file = outfile[i];
														}
														else
														{
															f_file = ofile;
														}
														if ((i + 1) < outfile.Count())
														{
															mergedocx(f_file, outfile[i + 1], ofile);
														}
													}
													log.Generatelog("Adding line numbers : " + data.targetPath);
													var tsk7 = Task.Run(() => Addlno(ofile));
													tsk7.Wait();
													File.Copy(ofile, data.targetPath + "\\output\\Bundled.docx", true);
													log.Generatelog("Bundle file created : " + data.targetPath + "\\output\\Bundled.docx");
													log.Generatelog("Removing line numbers from first file : " + data.targetPath);
													remlno(outfile[0]);
													var tsk3 = Task.Run(() => mergedocx1(outfile[0], ofile, data.targetPath + "\\output\\BundledPdfForReviewer.docx"));
													tsk3.Wait();
													log.Generatelog("Bundle file created : " + data.targetPath + "\\output\\BundledPdfForReviewer.docx");
													File.Delete(ofile);
												}
												if (outfile.Count() == 2)
												{
													log.Generatelog("Adding line numbers : " + data.targetPath);
													var tsk77 = Task.Run(() => Addlno(outfile[1]));
													tsk77.Wait();
													File.Copy(outfile[1], data.targetPath + "\\output\\Bundled.docx", true);
													log.Generatelog("Bundle file created : " + data.targetPath + "\\output\\Bundled.docx");
													log.Generatelog("Removing line numbers from first file : " + data.targetPath);
													remlno(outfile[0]);
													var tsk4 = Task.Run(() => mergedocx1(outfile[0], outfile[1], data.targetPath + "\\output\\BundledPdfForReviewer.docx"));
													tsk4.Wait();
													log.Generatelog("Bundle file created : " + data.targetPath + "\\output\\BundledPdfForReviewer.docx");
												}
											}
											catch (Exception ex)
											{
												log.Generatelog("Error found in bundling process : " + Path.GetFileNameWithoutExtension(data.targetPath) + " " + ex.Message.ToString());
												log.Errorlog("Error found in bundling process : " + Path.GetFileNameWithoutExtension(data.targetPath) + " " + ex.Message.ToString());
												isok = false;
											}
											if (File.Exists(data.targetPath + "\\output\\BundledPdfForReviewer.docx"))
											{
												log.Generatelog("Optimizing images in bundle file " + data.targetPath + "\\output\\BundledPdfForReviewer.docx");
												try
												{
													var tsk5 = Task.Run(() => Setimage(data.targetPath + "\\output\\BundledPdfForReviewer.docx"));
													tsk5.Wait();
												}
												catch (Exception ex)
												{
													log.Generatelog("Error found in image optimization " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\BundledPdfForReviewer.docx " + ex.Message.ToString());
													log.Errorlog("Error found in image optimization " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\BundledPdfForReviewer.docx " + ex.Message.ToString());
												}
												log.Generatelog("Optimizing tables in bundle file " + data.targetPath + "\\output\\BundledPdfForReviewer.docx");
												try
												{
													var tsk6 = Task.Run(() => Settable(data.targetPath + "\\output\\BundledPdfForReviewer.docx"));
													tsk6.Wait();
												}
												catch (Exception ex)
												{
													log.Generatelog("Error found in table optimization " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\BundledPdfForReviewer.docx " + ex.Message.ToString());
													log.Errorlog("Error found in table optimization " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\BundledPdfForReviewer.docx " + ex.Message.ToString());
												}
												//log.Generatelog("Adding line numbers in bundle file " + data.targetPath + "\\output\\BundledPdfForReviewer.docx");
												//try
												//{
												//	//var tsk7 = Task.Run(() => Addlno(data.targetPath + "\\output\\BundledPdfForReviewer.docx"));
												//	//tsk7.Wait();
												//}
												//catch (Exception ex)
												//{
												//	log.Generatelog("Error found in adding line numbers " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\BundledPdfForReviewer.docx. " + ex.Message.ToString());
												//	log.Errorlog("Error found in adding line numbers " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\BundledPdfForReviewer.docx. " + ex.Message.ToString());
												//	isok = false;
												//}
												log.Generatelog("Creating PDF file " + data.targetPath + "\\output\\Bundled.pdf");
												try
												{
													var tsk8 = Task.Run(() => Convert(data.targetPath + "\\output\\BundledPdfForReviewer.docx", data.targetPath + "\\output\\Bundled.pdf", Word.WdSaveFormat.wdFormatPDF));
													tsk8.Wait();
												}
												catch (Exception ex)
												{
													log.Generatelog("Error found in creating PDF " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\Bundled.pdf. " + ex.Message.ToString());
													log.Errorlog("Error found in creating PDF " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\Bundled.pdf. " + ex.Message.ToString());
													isok = false;
												}
											}
											if (File.Exists(data.targetPath + "\\output\\Bundled.docx"))
											{
												log.Generatelog("Optimizing images in bundle file " + data.targetPath + "\\output\\Bundled.docx");
												try
												{
													var tsk9 = Task.Run(() => Setimage(data.targetPath + "\\output\\Bundled.docx"));
													tsk9.Wait();
												}
												catch (Exception ex)
												{
													log.Generatelog("Error found in image optimization " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\Bundled.docx " + ex.Message.ToString());
													log.Errorlog("Error found in image optimization " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\Bundled.docx " + ex.Message.ToString());
												}
												log.Generatelog("Optimizing tables in bundle file " + data.targetPath + "\\output\\Bundled.docx");
												try
												{
													var tsk10 = Task.Run(() => Settable(data.targetPath + "\\output\\Bundled.docx"));
													tsk10.Wait();
												}
												catch (Exception ex)
												{
													log.Generatelog("Error found in table optimization " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\Bundled.docx " + ex.Message.ToString());
													log.Errorlog("Error found in table optimization " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\Bundled.docx " + ex.Message.ToString());
												}
												//log.Generatelog("Adding line numbers in bundle file " + data.targetPath + "\\output\\Bundled.docx");
												//try
												//{
												//	//var tsk11 = Task.Run(() => Addlno(data.targetPath + "\\output\\Bundled.docx"));
												//	//tsk11.Wait();
												//}
												//catch (Exception ex)
												//{
												//	log.Generatelog("Error found in adding line numbers " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\Bundled.docx. " + ex.Message.ToString());
												//	log.Errorlog("Error found in adding line numbers " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\Bundled.docx. " + ex.Message.ToString());
												//	isok = false;
												//}
												log.Generatelog("Creating PDF file " + data.targetPath + "\\output\\BundledPdfForReviewer.pdf");
												try
												{
													var tsk12 = Task.Run(() => Convert(data.targetPath + "\\output\\Bundled.docx", data.targetPath + "\\output\\BundledPdfForReviewer.pdf", Word.WdSaveFormat.wdFormatPDF));
													tsk12.Wait();
												}
												catch (Exception ex)
												{
													log.Generatelog("Error found in creating PDF " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\BundledPdfForReviewer.pdf. " + ex.Message.ToString());
													log.Errorlog("Error found in creating PDF " + Path.GetFileNameWithoutExtension(data.targetPath) + "\\output\\BundledPdfForReviewer.pdf. " + ex.Message.ToString());
													isok = false;
												}
											}
										}
										else
										{
											log.Generatelog("Minimum 2 files required in bundling process.");
											log.Errorlog("Minimum 2 files required in bundling process.");
											isok = false;
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				isok = false;
			}
		}
		static bool filecheck(string dirpath)
		{
			bool ret = true;
			if (File.Exists(dirpath + "\\input\\order.txt"))
			{
				string tu = "";
				string[] files = File.ReadAllLines(dirpath + "\\input\\order.txt");
				foreach (string fl in files)
				{
					if (fl.Trim().Length > 0)
					{
						if (File.Exists(dirpath + "\\input\\" + fl.Trim()))
						{
							if (tu == "")
							{
								tu = fl.Trim();
							}
							else
							{
								tu = tu + "\n" + fl.Trim();
							}
						}
						else
						{
							log.Generatelog("File not found. " + dirpath + "\\input\\" + fl.Trim());
							log.Errorlog("File not found. " + dirpath + "\\input\\" + fl.Trim());
							ret = false;
							break;
						}
					}
				}
				if (ret == true)
				{
					if (tu != "")
					{
						File.Delete(dirpath + "\\input\\order.txt");
						File.WriteAllText(dirpath + "\\input\\order.txt", tu);
					}
					else
					{
						log.Generatelog("Empty file. " + dirpath + "\\input\\order.txt");
						log.Errorlog("Empty file. " + dirpath + "\\input\\order.txt");
						ret = false;
					}
				}
			}
			else
			{
				log.Generatelog("File not found. " + dirpath + "\\input\\order.txt");
				log.Errorlog("File not found. " + dirpath + "\\input\\order.txt");
				ret = false;
			}
			return ret;
		}
		static void Doctodocx(string directorypath)
		{
			Word._Application application = new Word.Application();
			application.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
			object missing = Missing.Value;
			object fileformat = Word.WdSaveFormat.wdFormatXMLDocument;
			DirectoryInfo directory = new DirectoryInfo(directorypath);
			foreach (FileInfo file in directory.GetFiles("*.doc", SearchOption.AllDirectories))
			{
				if (file.Extension.ToLower() == ".doc")
				{
					if (!File.Exists(file.FullName.ToLower().Replace(".doc", ".docx")))
					{
						log.Generatelog("Converting doc to docx " + file.FullName.ToString());
						object filename = file.FullName;
						object newfilename = file.FullName.ToLower().Replace(".doc", ".docx");
						Word._Document document = application.Documents.Open(ref filename,
							ref missing, ref missing, ref missing, ref missing, ref missing,
							ref missing, ref missing, ref missing, ref missing, ref missing,
							ref missing, ref missing, ref missing, ref missing, ref missing);
						document.Convert();
						document.SaveAs(ref newfilename, ref fileformat, ref missing, ref missing,
							ref missing, ref missing, ref missing, ref missing,
							ref missing, ref missing, ref missing, ref missing,
							ref missing, ref missing, ref missing, ref missing);
						//Thread.Sleep(500);
						document.Close(ref missing, ref missing, ref missing);
						document = null;
					}
				}
			}
			application.Quit(ref missing, ref missing, ref missing);
			application = null;
		}
        static void FixUri(string directorypath)
        {
            
            DirectoryInfo directory = new DirectoryInfo(directorypath);
            foreach (FileInfo file in directory.GetFiles("*.docx", SearchOption.AllDirectories))
            {
                Word.Application applicationObject = new Word.Application();
                object missing = Type.Missing;
                object fileName = file.FullName;
                object False = false;
                applicationObject.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
                Word.Document documentObject = applicationObject.Documents.Open(
                ref fileName, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref False, ref missing, ref missing,
                ref missing, ref missing);
            repeat:
                for (int i = 1; i <= documentObject.Hyperlinks.Count; i++)
                {
                    object index = (object)i;
                    Word.Hyperlink link = documentObject.Hyperlinks.get_Item(ref index);
                    link.Delete();
                }
                if (documentObject.Hyperlinks.Count > 0)
                {
                    goto repeat;
                }
                documentObject.Save();
                applicationObject.Quit(ref missing, ref missing, ref missing);
            }
        }
        static void Add_image(string docpath, string imgpath)
		{
			if (File.Exists(docpath))
			{
				File.Delete(docpath);
			}
			using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(docpath, WordprocessingDocumentType.Document))
			{
				MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
				mainPart.Document = new Document();
				Body body = mainPart.Document.AppendChild(new Body());
				Paragraph para = body.AppendChild(new Paragraph());
				Run run = para.AppendChild(new Run());
				run.AppendChild(new Text(""));
			}
			Word.Application ap = new Word.Application();
			ap.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
			Word.Document document = ap.Documents.Open(docpath);
			try
			{
				object StartPos1 = 0;
				object Endpos1 = 1;
				Word.Range rng = document.Range(ref StartPos1, ref Endpos1);
				object NewEndPos1 = rng.StoryLength - 1;
				rng = document.Range(ref NewEndPos1, ref NewEndPos1);
				rng.Select();
				document.InlineShapes.AddPicture(imgpath);
				//Thread.Sleep(500);
				document.Close();
				ap.Quit();
				ap = null;
			}
			catch (Exception ex)
			{
				document.Close();
				ap.Quit();
				ap = null;
			}
		}
		public static void mergedocx(string first_file, string second_file, string out_file)
		{
			object missing = System.Reflection.Missing.Value;
			Microsoft.Office.Interop.Word.Application WordApp = new Microsoft.Office.Interop.Word.Application();
			Microsoft.Office.Interop.Word.Document adoc = WordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
			adoc.Sections.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
			object start = 0;
			object end = 0;
			Microsoft.Office.Interop.Word.Range rng = adoc.Range(ref start, ref missing);
			rng.InsertFile(first_file, ref missing, ref missing, ref missing, ref missing);
			//Thread.Sleep(200);
			start = WordApp.ActiveDocument.Content.End - 1;
			Microsoft.Office.Interop.Word.Range rng1 = adoc.Range(ref start, ref missing);
			rng1.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
			rng1.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);
			rng1.InsertFile(second_file, ref missing, ref missing, ref missing, ref missing);
			//Thread.Sleep(500);
			start = WordApp.ActiveDocument.Content.End - 1;
			if (File.Exists(out_file))
			{
				File.Delete(out_file);
				//Thread.Sleep(300);
			}
			adoc.SaveAs2(out_file);
			//Thread.Sleep(500);
			adoc.Close();
			//Thread.Sleep(500);
			WordApp.Quit();
			WordApp = null;
		}
		public static void mergedocx1(string first_file, string second_file, string out_file)
		{
			object missing = System.Reflection.Missing.Value;
			Microsoft.Office.Interop.Word.Application WordApp = new Microsoft.Office.Interop.Word.Application();
			Microsoft.Office.Interop.Word.Document adoc = WordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);
			adoc.Sections.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
			object start = 0;
			object end = 0;
			Microsoft.Office.Interop.Word.Range rng = adoc.Range(ref start, ref missing);
			rng.InsertFile(first_file, ref missing, ref missing, ref missing, ref missing);
			//Thread.Sleep(200);

			start = WordApp.ActiveDocument.Content.End - 1;
			Microsoft.Office.Interop.Word.Range rng1 = adoc.Range(ref start, ref missing);
			rng1.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd);
			//rng1.InsertBreak(Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak);			
			rng1.InsertFile(second_file, ref missing, ref missing, ref missing, ref missing);
			for (int i = 1; i <= adoc.Sections.Count; i++)
			{
				try
				{
					adoc.Sections[i].PageSetup.LineNumbering.Active = -1;
					adoc.Sections[i].PageSetup.LineNumbering.RestartMode = Word.WdNumberingRule.wdRestartContinuous;
				}
				catch
				{
					continue;
				}
			}
			//Thread.Sleep(500);
			start = WordApp.ActiveDocument.Content.End - 1;
			if (File.Exists(out_file))
			{
				File.Delete(out_file);
				//Thread.Sleep(300);
			}
			adoc.SaveAs2(out_file);
			//Thread.Sleep(500);
			adoc.Close();
			//Thread.Sleep(500);
			WordApp.Quit();
			WordApp = null;
		}
		static void Settable(string filepath)
		{
			WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(filepath, true);
			Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
			string ppp = "";
			string ppp1 = "";
			foreach (var para in body)
			{
				if (para.LocalName == "tbl")
				{
					try
					{
						if (para.Elements<TableProperties>().Count() == 0)
						{
							para.PrependChild<TableProperties>(new TableProperties());
						}
						ppp = para.InnerXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "");
						ppp1 = ppp;
						foreach (var run in para)
						{
							if (run.LocalName == "tblPr")
							{
								string tr = run.OuterXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "");
								string tr3 = "";
								string tr4 = "";
								string tr5 = tr;
								string tr6 = "";
								string tr7 = "";
								string tr8 = "";
								string tr9 = "";
								string tr10 = "";
								if (tr.Contains("<w:tblpPr"))
								{
									string tr1 = tr.Substring(tr.IndexOf("<w:tblpPr"));
									string tr2 = tr1.Substring(0, tr1.IndexOf(">") + 1);
									if (tr2.Contains("w:tblpXSpec="))
									{
										tr3 = tr2.Substring(tr2.IndexOf("w:tblpXSpec=") + 13);
										tr4 = tr3.Substring(0, tr3.IndexOf("\""));
									}
									else
									{
										tr5 = tr5.Replace("<w:tblpPr", "<w:tblpPr w:tblpXSpec=\"center\"");
									}
									if (tr4 != "")
									{
										tr5 = tr5.Replace(tr4, "center");
									}
								}
								else
								{
									tr5 = tr5.Replace("</w:tblPr>", "<w:tblpPr w:leftFromText=\"180\" w:rightFromText=\"180\" w:vertAnchor=\"text\" w:tblpXSpec=\"center\" w:tblpY=\"1\" /></w:tblPr>");
								}
								if (tr.Contains("<w:tblOverlap") == false)
								{
									tr5 = tr5.Replace("</w:tblPr>", "<w:tblOverlap w:val=\"never\" /></w:tblPr>");
								}
								if (tr.Contains("<w:tblLayout") == false)
								{
									tr5 = tr5.Replace("</w:tblPr>", "<w:tblLayout w:type=\"fixed\" /></w:tblPr>");
								}
								if (tr.Contains("<w:tblLook") == false)
								{
									tr5 = tr5.Replace("</w:tblPr>", "<w:tblLook w:val=\"04a0\" /></w:tblPr>");
								}
								if (tr.Contains("<w:tblW"))
								{
									if (tr5.Contains("w:w="))
									{
										tr6 = tr5.Substring(tr5.IndexOf("w:w=") + 5);
										tr7 = tr6.Substring(0, tr6.IndexOf("\""));
									}
									if (tr5.Contains("w:type="))
									{
										tr8 = tr5.Substring(tr5.IndexOf("w:type=") + 8);
										tr9 = tr8.Substring(0, tr8.IndexOf("\""));
									}
									if (tr9 == "dxa")
									{
										if (System.Convert.ToInt32(tr7) > 8930)
										{
											tr10 = "8930";
										}
									}
									if (tr9 == "pct")
									{
										if (System.Convert.ToInt32(tr7) > 5000)
										{
											tr10 = "5000";
										}
									}
									if (tr10 != "")
									{
										tr5 = tr5.Replace(tr7, tr10);
									}
									tr5 = Regex.Replace(tr5, "(<w:tblW w:w=\")(.*?)(\" w:type=\"auto\" \\/>)", "(<w:tblW w:w=\"8930\" w:type=\"dxa\" />)");
								}
								ppp = ppp.Replace(tr, tr5);
							}
						}
						para.InnerXml = ppp;
					}
					catch
					{
						para.InnerXml = ppp1;
						continue;
					}
				}
			}
			wordprocessingDocument.Close();
		}
		static void Setimage(string filepath)
		{
			WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(filepath, true);
			Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
			string ppp = "";
			foreach (var para in body)
			{
				if (para.LocalName == "p")
				{
					try
					{
						ppp = para.InnerXml.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "");
						foreach (var run in para)
						{
							if (run.LocalName == "r")
							{
								foreach (var txt in run)
								{
									if (txt.LocalName == "pict")
									{
										foreach (var txt2 in txt)
										{
											if (txt2.LocalName == "shape")
											{
												string a1 = txt2.OuterXml;
												decimal w1 = 0;
												decimal h1 = 0;
												string repwidth1 = "";
												string repmargin = "";
												string repheight1 = "";
												if (a1.Contains("style="))
												{
													string b1 = a1.Substring(a1.IndexOf("style=") + 7);
													string b2 = b1.Substring(0, b1.IndexOf("\""));
													string[] st = b2.Split(';');
													foreach (string s in st)
													{
														if (s.Contains("margin-left:"))
														{
															repmargin = s;
														}
														if (s.Contains("width:"))
														{
															repwidth1 = s;
															string p1 = s.Split(':')[1];
															string w = Regex.Match(p1, @"(\d*\.)?\d+").Value;
															w1 = System.Convert.ToDecimal(w);
														}
														if (s.Contains("height:"))
														{
															repheight1 = s;
															string p1 = s.Split(':')[1];
															string w = Regex.Match(p1, @"(\d*\.)?\d+").Value;
															h1 = System.Convert.ToDecimal(w);
														}
													}
												}
												string a_rep = "";
												if (w1 > 450)
												{
													decimal h2 = Math.Round((h1 / w1) * 450, 2);
													a_rep = a1.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace(repwidth1, "width:450pt").Replace(repheight1, "height:" + h2 + "pt");
													ppp = ppp.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace(a1, a_rep);
												}
												else
												{
													decimal m1 = Math.Round((450 - w1) / 2, 2);
													a_rep = a1.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace(repmargin, "margin-left:" + m1 + "pt");
												}
												a_rep = a_rep.Replace("text-align:left;", "").Replace(";text-align:left", "").Replace("left:0;", "").Replace(";left:0", "");
												ppp = ppp.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace(a1, a_rep);
											}
										}
									}
									if (txt.LocalName == "object")
									{
										foreach (var txt2 in txt)
										{
											if (txt2.LocalName == "shape")
											{
												string a1 = txt2.OuterXml;
												decimal w1 = 0;
												decimal h1 = 0;
												string repmargin = "";
												string repwidth = "";
												string repheight = "";
												if (a1.Contains("style="))
												{
													string b1 = a1.Substring(a1.IndexOf("style=") + 7);
													string b2 = b1.Substring(0, b1.IndexOf("\""));
													string[] st = b2.Split(';');
													foreach (string s in st)
													{
														if (s.Contains("margin-left:"))
														{
															repmargin = s;
														}
														if (s.Contains("width:"))
														{
															repwidth = s;
															string p1 = s.Split(':')[1];
															string w = Regex.Match(p1, @"(\d*\.)?\d+").Value;
															w1 = System.Convert.ToDecimal(w);
														}
														if (s.Contains("height:"))
														{
															repheight = s;
															string p1 = s.Split(':')[1];
															string w = Regex.Match(p1, @"(\d*\.)?\d+").Value;
															h1 = System.Convert.ToDecimal(w);
														}
													}
												}
												if (w1 > 0 && h1 > 0)
												{
													string a_rep = "";
													if (w1 > 450)
													{
														decimal h2 = Math.Round((h1 / w1) * 450, 2);
														a_rep = a1.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace(repwidth, "width:450pt").Replace(repheight, "height:" + h2 + "pt").Replace(repmargin, "margin-left:0pt");
													}
													else
													{
														decimal m1 = Math.Round((450 - w1) / 2, 2);
														a_rep = a1.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace(repmargin, "margin-left:" + m1 + "pt");
													}
													a_rep = a_rep.Replace("text-align:left;", "").Replace(";text-align:left", "").Replace("left:0;", "").Replace(";left:0", "");
													ppp = ppp.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace(a1, a_rep);
												}
											}
										}
									}
									if (txt.LocalName == "drawing")
									{
										string dr = txt.InnerXml;
										decimal cx = 0;
										decimal cy = 0;
										if (dr.Contains("<wp:extent cx="))
										{
											string dr1 = dr.Substring(dr.IndexOf("<wp:extent cx=") + 15);
											cx = System.Convert.ToInt32(dr1.Substring(0, dr1.IndexOf("\"")).Trim());
											string dr2 = dr1.Substring(dr1.IndexOf("cy=") + 4);
											cy = System.Convert.ToInt32(dr2.Substring(0, dr2.IndexOf("\"")).Trim());
											string rep_a = dr.Substring(dr.IndexOf("<wp:extent cx="));
											string rep1 = rep_a.Substring(0, rep_a.IndexOf(">") + 1);
											string rep_b = dr.Substring(dr.IndexOf("<a:ext cx="));
											string rep2 = rep_b.Substring(0, rep_b.IndexOf(">") + 1);
											string dist = dr.Substring(dr.IndexOf("distL=") + 7);
											string repdist = "distL=\"" + dist.Substring(0, dist.IndexOf("\"") + 1);
											string repnew1 = rep1;
											string repnew2 = "<a:ext cx=\"" + cx + "\" cy=\"" + cy + "\" />";
											string repdistnew = "";
											string dr3 = dr;
											if (cx > 5274310)
											{
												int cy_new = System.Convert.ToInt32((cy / cx) * 5274310);
												repnew1 = "<wp:extent cx=\"" + 5274310 + "\" cy=\"" + cy_new + "\" />";
												repnew2 = "<a:ext cx=\"" + 5274310 + "\" cy=\"" + cy_new + "\" />";
												repdistnew = "distL=\"0\"";
												dr3 = dr.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace(rep1, repnew1).Replace(rep2, repnew2).Replace(repdist, repdistnew);
											}
											if (dr3 != dr)
											{
												ppp = ppp.Replace(" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\"", "").Replace(dr, dr3);
											}
										}
									}
								}
							}
						}
						para.InnerXml = ppp;
					}
					catch
					{
					}
				}
			}
			wordprocessingDocument.Close();
			//Thread.Sleep(500);
		}
		static void Convert(string input, string output, Word.WdSaveFormat format)
		{
			Word.Application oWord = new Word.Application
			{
				Visible = false,
				DisplayAlerts = Word.WdAlertLevel.wdAlertsNone
			};
			object oMissing = System.Reflection.Missing.Value;
			object isVisible = true;
			object readOnly = true;
			object oInput = input;
			object oOutput = output;
			object oFormat = format;
			Word.Document oDoc = oWord.Documents.Open(
				ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing,
				ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
				ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing
				);
			oDoc.Activate();
			oDoc.SaveAs(ref oOutput, ref oFormat, ref oMissing, ref oMissing,
				ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
				ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
				);
			//Thread.Sleep(500);
			oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
			oWord = null;
		}
		static void Addlno(string filepath)
		{
			Word.Application ap = new Word.Application
			{
				Visible = false,
				DisplayAlerts = Word.WdAlertLevel.wdAlertsNone
			};
			Word.Document document = ap.Documents.Open(filepath);
			if (ap.ActiveDocument.Sections.Count > 0)
			{
				for (int i = 1; i <= ap.ActiveDocument.Sections.Count; i++)
				{
					try
					{
						ap.ActiveDocument.Sections[i].PageSetup.LineNumbering.Active = -1;
						ap.ActiveDocument.Sections[i].PageSetup.LineNumbering.RestartMode = Word.WdNumberingRule.wdRestartContinuous;
					}
					catch
					{
						continue;
					}
				}
			}
			document.Close();
			//Thread.Sleep(500);
			ap.Quit();
			ap = null;
		}
		static void killword()
		{
			System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcessesByName("winword");
			foreach (System.Diagnostics.Process p in localByName)
			{
				if ((DateTime.Now - p.StartTime).Minutes > 2)
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
		}
		static void remlno(string filepath)
		{
			WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(filepath, true);
			Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
			foreach (var para in body)
			{
				if (para.LocalName == "p")
				{
					if (para.Elements<ParagraphProperties>().Count() == 0)
					{
						para.PrependChild<ParagraphProperties>(new ParagraphProperties());
					}
				}
				if (para.InnerXml.Contains("w:suppressLineNumbers") == false)
				{
					para.InnerXml = para.InnerXml.Replace("<w:pPr />", "<w:pPr></w:pPr>");
					para.InnerXml = para.InnerXml.Replace("<w:pPr/>", "<w:pPr></w:pPr>");
					para.InnerXml = para.InnerXml.Replace("</w:pPr>", "<w:suppressLineNumbers /></w:pPr>");
				}
			}
			body.InnerXml = body.InnerXml + "<w:p><w:pPr><w:suppressLineNumbers /></w:pPr><w:r><w:br w:type=\"page\" /></w:r></w:p>";
			foreach (Paragraph P in wordprocessingDocument.MainDocumentPart.Document.Descendants<Paragraph>().ToList())
			{
				if (P.InnerText.Trim() == "" && P.InnerXml.Contains("w:suppressLineNumbers") == false && P.LocalName == "p")
				{
					P.Remove();
				}
			}
			wordprocessingDocument.Close();
		}
	}
	public class bundle1
	{
		public string dir_name { get; set; }
		public string threadname { get; set; }
		public bundle1(string spath)
		{
			this.dir_name = spath;
			this.threadname = spath;
		}
	}
	public class bundle
	{
		public string sourcePath { get; set; }
		public string targetPath { get; set; }
		public string dirname { get; set; }
		public bundle(string strin)
		{
			this.sourcePath = System.Configuration.ConfigurationSettings.AppSettings["in_path"] + "\\" + strin;
			this.targetPath = System.Configuration.ConfigurationSettings.AppSettings["out_path"] + "\\" + strin;
			this.dirname = strin;
		}
	}
}
