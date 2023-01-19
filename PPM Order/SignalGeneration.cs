using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Xml;
using System.IO;



namespace PPM_TRACKING_SYSTEM
{
    class SignalGeneration
    {
        Boolean isValid;
        string tbMessageID;
        string tbInstNo;
        string tbOrderNo;
        string tbStage;
        string tbTime;
        string ddlStage;   
        string tbPII;
        string tbISBN;

        public int Process_Signal(bool boolArchive, string strISBN, string strStage, out string MsgID)
        {
            string PPMOrderFile = "";
            MsgID = "";
            XmlDocument xDoc = new XmlDocument();
            XmlNodeList xNL;
            string OrderSPath = System.Configuration.ConfigurationSettings.AppSettings["OrderPath"];
            string PPMOrderPath = OrderSPath + "\\" + strISBN.Replace("-", "") + "\\" + strStage + "\\Current_order";
            if (!Directory.Exists(PPMOrderPath))
            {
                //MessageBox.Show("Folder information not available");
                  return 1;
            }

            FileInfo[] FInfo = new DirectoryInfo(PPMOrderPath).GetFiles();
            for (int i = 0; i < FInfo.Length; i++)
            {
                if (FInfo[i].Name.ToLower().StartsWith("kup"))
                {
                    PPMOrderFile = FInfo[i].FullName;
                    break;
                }
            }

            if (PPMOrderFile.Length == 0)
            {
               // MessageBox.Show("PPM Order not available on Server");
                return 2;
            }
            try
            {
                string TempP = @"C:\PPMTS\ProcessPPM";
                if (!Directory.Exists(TempP))
                {
                    Directory.CreateDirectory(TempP);
                }

                TempP = TempP + "\\" + new FileInfo(PPMOrderFile).Name;

                if (!File.Exists(TempP))
                {
                    File.Copy(PPMOrderFile, TempP);
                }

                xDoc.Load(TempP);

                tbMessageID = "";
                tbMessageID = GetMessageID();
                if (tbMessageID.Length == 0)                    
                {
                    return 3;
                }
                else if (tbMessageID == "0")
                {
                    return 4;
                }
                
                xNL = xDoc.GetElementsByTagName("order-no");
                tbOrderNo = xNL[0].InnerXml.ToString();
                xNL = xDoc.GetElementsByTagName("instruction-no");
                if (xNL.Count == 0)
                {
                   // MessageBox.Show("Please check PPM order as instruction no. has not been found.");
                }
                else
                {
                    if (!boolArchive)
                    {
                        tbInstNo = xNL[0].InnerXml.ToString();
                    }
                  
                }
                xNL = xDoc.GetElementsByTagName("stage");
                 if (!boolArchive)
                {
                    tbStage = xNL[0].Attributes[0].Value.ToString();
                }
                else
                {
                    tbStage = "FILES-TO-ARCHIVE";
                }

                xNL = xDoc.GetElementsByTagName("book-info");
                /*if (xNL.Count == 0)
                {
                    tbISBN.Enabled = false;
                    prop.itemInfo = "item-info";
                }
                else
                {
                    prop.itemInfo = "book-info";
                }*/
                
                xNL = xDoc.GetElementsByTagName("pii");
                tbPII = xNL[0].InnerXml.ToString();

                xNL = xDoc.GetElementsByTagName("isbn");
                tbISBN = xNL[0].InnerXml.ToString();
                Create_Signal(boolArchive);
                MsgID = tbMessageID;
                File.Delete(TempP);
                return 0;
            }
            catch (Exception ex)
            {
                File.WriteAllText("c:\\sachin.txt", ex.Message.ToString());
                return 5;
              
            }
        }
        private string GetMessageID()
        {

            string OPath = System.Configuration.ConfigurationSettings.AppSettings["OrderPath"];
            OPath = OPath + System.Configuration.ConfigurationSettings.AppSettings["CounterIni"];
            string counterPath = OPath;

            if (!File.Exists(counterPath))
            {
                return "0";
            }

            string ret = "";
            string temp = "";
            string[] arr;
            int ID;

            try
            {
                temp = File.ReadAllText(counterPath);
                arr = temp.Split(',');
                if (DateTime.Now.ToString("ddMMyyyy") == arr[1])
                {
                    ID = Convert.ToInt32(arr[0]);
                    ID = ID + 1;
                }
                else
                {
                    ID = 1;
                }

                temp = ID.ToString();

                for (int i = 0; i < 6 - temp.Length; i++)
                {
                    ret = ret + "0";
                }
                //temp = DateTime.Now.ToString("ddMMyyyy") + "-" + ret + temp;
                temp = DateTime.Now.ToString("ddMMyyyy") + "-" + temp;
                ret = "THOSG-" + temp;
                temp = ID.ToString();
                temp = temp + "," + DateTime.Now.ToString("ddMMyyyy");
                File.WriteAllText(counterPath, temp);
                return ret;
            }
            catch (Exception ex)
            {
                //WriteLog(ex.Message.ToString());
                ret = "";
                return ret;
            }
        }
        protected void Create_Signal(bool boolArchive)
        {
            string dtdname = ConfigurationSettings.AppSettings["DTDNAME"].ToString();
            string dtdversion = ConfigurationSettings.AppSettings["DTDvesrion"].ToString();
            string signalXML;
            string[] arr, arr1;
            string temp;
                if (!boolArchive)
                {
                    signalXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><!DOCTYPE messages PUBLIC \"-//ES//DTD PPM signal DTD version " + dtdversion + "//EN//XML\" \"";//1.2
                    signalXML = signalXML + "C:/PPMTS/ProcessPPM//" + "" + dtdname + "\"><messages><message id=\"";// ppmsignal12.dtd
                    if (tbMessageID != "")
                    {
                        signalXML = signalXML + tbMessageID + "\">";
                    }
                    else
                    {
                        //MessageBox.Show("No message ID found.");
                    }
                    tbTime = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt");
                    temp = tbTime;
                    arr = temp.Split(' ');

                    arr1 = arr[0].Split("/-".ToCharArray());
                    signalXML = signalXML + "<time day=\"";
                    if (arr1[1].Length == 1)
                    {
                        signalXML = signalXML + "0" + arr1[1] + "\" month=\"";
                    }
                    else
                    {
                        signalXML = signalXML + arr1[1] + "\" month=\"";
                    }
                    if (arr1[0].Length == 1)
                    {
                        signalXML = signalXML + "0" + arr1[0] + "\" year=\"";
                    }
                    else
                    {
                        signalXML = signalXML + arr1[0] + "\" year=\"";
                    }
                    signalXML = signalXML + arr1[2] + "\" hr=\"";

                    arr1 = arr[1].Split(':');

                    if (arr[2] == "PM")
                    {
                        if (Convert.ToInt16(arr1[0]) < 12)
                        {
                            arr1[0] = Convert.ToString(Convert.ToInt16(arr1[0]) + 12);
                        }
                        signalXML = signalXML + arr1[0] + "\" min=\"";
                    }
                    else
                    {
                        if (arr1[0].Length == 1)
                        {
                            signalXML = signalXML + "0" + arr1[0] + "\" min=\"";
                        }
                        else
                        {
                            signalXML = signalXML + arr1[0] + "\" min=\"";
                        }
                    }

                    if (arr1[1].Length == 1)
                    {
                        signalXML = signalXML + "0" + arr1[1] + "\" sec=\"";
                    }
                    else
                    {
                        signalXML = signalXML + arr1[1] + "\" sec=\"";
                    }
                    if (arr1[2].Length == 1)
                    {
                        signalXML = signalXML + "0" + arr1[2] + "\"/><signal id=\"supplier-ready";
                    }
                    else
                    {
                        signalXML = signalXML + arr1[2] + "\"/><signal id=\"supplier-ready";
                    }
                    signalXML = signalXML + ddlStage + "\"/><stage step=\"";
                    signalXML = signalXML + tbStage + "\"/>";
                    if (tbOrderNo.Contains(".") == false)
                    {
                        signalXML = signalXML + "<order-no>" + tbOrderNo + "</order-no>";
                    }
                    else
                    {
                        signalXML = signalXML + "<order-no>" + tbOrderNo.Substring(0, tbOrderNo.IndexOf(".")) + "</order-no>";
                    }

                    if (tbInstNo != "")
                    {
                        signalXML = signalXML + "<instruction-no>" + tbInstNo + "</instruction-no>";
                    }
                    if (tbPII != "" && tbISBN != "")
                    {
                        signalXML = signalXML + "<book-info><pii>" + tbPII + "</pii><isbn>" + tbISBN + "</isbn></book-info>";
                    }
                    else if (tbPII == "" & tbISBN != "")
                    {
                        signalXML = signalXML + "<book-info><isbn>" + tbISBN + "</isbn></book-info>";
                    }
                    else
                    {
                        //RegisterClientScriptBlock("Alert", "<script language='javascript'> alert('ISBN not found.'); </script>");
                    }

                    //  }
                    //else
                    //{
                    //    signalXML = signalXML + "<item-info><pii>" + tbPII + "</pii></item-info>";
                    //}
                    signalXML = signalXML + "<supplier-info>";

                    signalXML = signalXML + "</supplier-info></message></messages>";
                    signalXML = signalXML.Replace("<supplier-info></supplier-info>", "");
                }
                else
                {
                    signalXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><!DOCTYPE messages PUBLIC \"-//ES//DTD PPM signal DTD version " + dtdversion + "//EN//XML\" \"";// 1.2
                    signalXML = signalXML + "C:/PPMTS/ProcessPPM//" + "" + dtdname + "\"><messages><message id=\"";//ppmsignal12.dtd
                    if (tbMessageID != "")
                    {
                        signalXML = signalXML + tbMessageID + "\">";
                    }
                    else
                    {
                        //MessageBox.Show("No message ID found.");
                    }
                    temp = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt"); ;//tbTime;
                    arr = temp.Split(' ');
                    arr1 = arr[0].Split("/-".ToCharArray());
                    signalXML = signalXML + "<time day=\"";
                    if (arr1[1].Length == 1)
                    {
                        signalXML = signalXML + "0" + arr1[1] + "\" month=\"";
                    }
                    else
                    {
                        signalXML = signalXML + arr1[1] + "\" month=\"";
                    }
                    if (arr1[0].Length == 1)
                    {
                        signalXML = signalXML + "0" + arr1[0] + "\" year=\"";
                    }
                    else
                    {
                        signalXML = signalXML + arr1[0] + "\" year=\"";
                    }
                    signalXML = signalXML + arr1[2] + "\" hr=\"";

                    arr1 = arr[1].Split(':');

                    if (arr[2] == "PM")
                    {
                        if (Convert.ToInt16(arr1[0]) < 12)
                        {
                            arr1[0] = Convert.ToString(Convert.ToInt16(arr1[0]) + 12);
                        }
                        signalXML = signalXML + arr1[0] + "\" min=\"";
                    }
                    else
                    {
                        if (arr1[0].Length == 1)
                        {
                            signalXML = signalXML + "0" + arr1[0] + "\" min=\"";
                        }
                        else
                        {
                            signalXML = signalXML + arr1[0] + "\" min=\"";
                        }
                    }

                    if (arr1[1].Length == 1)
                    {
                        signalXML = signalXML + "0" + arr1[1] + "\" sec=\"";
                    }
                    else
                    {
                        signalXML = signalXML + arr1[1] + "\" sec=\"";
                    }
                    if (arr1[2].Length == 1)
                    {
                        signalXML = signalXML + "0" + arr1[2] + "\"/><signal id=\"supplier-ready";
                    }
                    else
                    {
                        signalXML = signalXML + arr1[2] + "\"/><signal id=\"supplier-ready";
                    }
                    signalXML = signalXML + "" + "\"/><stage step=\"FILES-TO-ARCHIVE\"/><order-no>";
                    if (tbOrderNo.Contains(".") == false)
                    {
                        signalXML = signalXML + tbOrderNo + "</order-no>";
                    }
                    else
                    {
                        signalXML = signalXML + tbOrderNo.Substring(0, tbOrderNo.IndexOf(".")) + "</order-no>";
                    }

                    if (tbInstNo != "" && tbInstNo != null)
                    {
                        signalXML = signalXML + "<instruction-no>" + tbInstNo + "</instruction-no>";
                    }

                    //if (prop.itemInfo == "book-info")
                    {
                        if (tbPII != "" && tbISBN != "")
                        {
                            signalXML = signalXML + "<book-info><pii>" + tbPII + "</pii><isbn>" + tbISBN + "</isbn></book-info>";
                        }
                        else if (tbPII == "" & tbISBN != "")
                        {
                            signalXML = signalXML + "<book-info><isbn>" + tbISBN + "</isbn></book-info>";
                        }
                        else
                        {
                            //RegisterClientScriptBlock("Alert", "<script language='javascript'> alert('ISBN not found.'); </script>");
                        }

                    }
                   /* else
                    {
                        signalXML = signalXML + "<item-info><pii>" + tbPII + "</pii></item-info>";
                    }*/
                    /*signalXML = signalXML + "<supplier-info>";
                    if (tbPdfPages != "")
                    {
                        signalXML = signalXML + "<pdf-pages>" + tbPdfPages + "</pdf-pages>";
                    }
                    if (tbSupRemarks != "")
                    {
                        signalXML = signalXML + "<remarks>" + tbSupRemarks + "</remarks>";
                    }
                    */

                    signalXML = signalXML + "</message></messages>";
                    signalXML = signalXML.Replace("<supplier-info></supplier-info>", "");
                }

                //signalXML= signalXML.Replace("ppmsignal10.dtd",Path.GetDirectoryName(Application.ExecutablePath)+"\\ppmsignal10.dtd");
                if (XmlValid(signalXML))
                {
                    //RegisterClientScriptBlock("Alert", "<script language='javascript'> alert('Error while generating signal.'); </script>");
                    //File.Open(Path.GetDirectoryName(Application.ExecutablePath) + "\\error.log", FileMode.Open);                
                }
                else
                {
                        signalXML = signalXML.Replace("C:/PPMTS/ProcessPPM//", "");
                        string serverPath = System.Configuration.ConfigurationSettings.AppSettings["ServerPath"] + "\\" + tbISBN.Replace("-", "") + "\\" + tbStage + "\\Current_Order\\";
                        serverPath = "c:\\PPMTS\\ProcessPPM\\";
                        serverPath = serverPath + tbMessageID + ".xml";
                        File.WriteAllText(serverPath, signalXML);
                }
            }      
        bool XmlValid(string document)
        {
            isValid = false;
            return false; //Time Being
            try
            {
                System.IO.StringReader input = new System.IO.StringReader(document);
                XmlTextReader reader = new XmlTextReader(input);

                XmlValidatingReader settings = new XmlValidatingReader(reader);
                settings.ValidationType = ValidationType.DTD;
                settings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(MyValidationEventHandler);//delegate { xmlValid = false; });

                //XmlReader validator = XmlReader.Create(reader, settings);

                while (settings.Read()) ;
                settings.Close();
               
                if (isValid)
                    Console.WriteLine("Document is valid");
                else
                    Console.WriteLine("Document is invalid");


            }
            catch (Exception ex)
            {
                isValid = true;
                //WriteLog(ex.Message.ToString());            
            }
            return isValid;
        }
        public void MyValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs args)
        {
            isValid = true;
           // lblError.Text = "Validation event: " + args.Message;
        }
        

    }
}
