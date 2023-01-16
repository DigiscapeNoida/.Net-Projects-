using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WileyMetaData
{
    public delegate void ExceptionHandler(XmlException ex);
    public delegate void XmlChangeHandler(String xmlstr);

    class XmlInfo
    {
        XmlReader         Reader;
        XmlDocument       MyXmlDocument     = new XmlDocument();
        XmlReaderSettings ReaderSettings    = new XmlReaderSettings();

        string processFilePath    = "";
        string XmlFilePath        = "";
        string dtdPath            = "";
        bool isParsingRequired    = false;
        bool preserveSpace        = false;
        bool   _IsChangedDtdPath  = false;
        string _ChangedDtdPath    = "";

        public event ExceptionHandler ExceptionFired;

        public event XmlChangeHandler XmlChanged;

        public XmlInfo()
        {
            MyXmlDocument.NodeChanged += new XmlNodeChangedEventHandler(MyXmlDocument_NodeChanged);
        }

        public XmlDocument xmlDocument
        {
            get { return MyXmlDocument; }
        }

        void MyXmlDocument_NodeChanged(object sender, XmlNodeChangedEventArgs e)
        {
            if (XmlChanged!=null)
            {
                XmlChanged(MyXmlDocument.OuterXml);
            }
        }

        public XmlInfo(string xmlFilePath)
        {
            XmlFilePath = xmlFilePath;
        }

        public string InnerXml
        {
            get 
            {
                if (MyXmlDocument != null)
                    return MyXmlDocument.InnerXml;
                else
                    return "";
            }
            
        }

        public string FilePath
        {
            get{return XmlFilePath;}
            set
            {
                if (value.Equals(""))
                    throw new Exception ("Empty string");
                else if (!File.Exists(value))
                    throw new FileNotFoundException();
                else
                    XmlFilePath= value; 
            }  
        }

        public string ProcessFilePath
        {
            get { return processFilePath; }

        }

        public string DtdPath
        {
            get{return dtdPath;}
            set
            {
                if (value.Equals(""))
                    throw new Exception ("Empty string");
                else if (!File.Exists(dtdPath))
                    throw new FileNotFoundException();
                else
                    dtdPath= value; 
            }  
        }

        public bool   IsParsingRequired
        {
            set
            {
                try
                {
                    isParsingRequired = value;
                }
                catch
                {

                }
            }
            get { return isParsingRequired; }

        }

        public bool   PreserveSpace
        {
            set
            {
                try
                {
                    preserveSpace = value;
                }
                catch
                {

                }
            }
            get { return preserveSpace; }
        }

        private void  SetReaderSettings()
        {
            ReaderSettings.IgnoreComments   = false;
            ReaderSettings.IgnoreWhitespace = false;

            if (dtdPath.Equals(""))
            {
                ReaderSettings.ValidationType = ValidationType.None;
                ReaderSettings.ProhibitDtd = false;
            }
            else
            {
                ReaderSettings.ProhibitDtd = true;
                ReaderSettings.ValidationType = ValidationType.DTD;
            }
            ReaderSettings.XmlResolver = null;
        }

        private bool  LoadXmlFile()
        {
            string tempXMlPath = string.Empty;
            if (AppDomain.CurrentDomain.RelativeSearchPath == null)
                tempXMlPath = AppDomain.CurrentDomain.BaseDirectory + "\\temp.xml";
            else
                tempXMlPath = AppDomain.CurrentDomain.RelativeSearchPath+ "\\temp.xml";

            
            try
            {
                
                
                StringBuilder xmlStr = new StringBuilder(File.ReadAllText(XmlFilePath));

                xmlStr.Replace("&lt;huc&gt;", "<fc>");
                xmlStr.Replace("&lt;/huc&gt;", "</fc>");
                xmlStr.Replace("&lt;hlc&gt;", "<fc>");
                xmlStr.Replace("&lt;/hlc&gt;", "</fc>");
                xmlStr.Replace("&lt;hi&gt;", "<fi>");
                xmlStr.Replace("&lt;/hi&gt;", "</fi>");
                xmlStr.Replace("&lt;fc&gt;", "<fc>");
                xmlStr.Replace("&lt;/fc&gt;", "</fc>");
                xmlStr.Replace("&lt;fr&gt;", "<fr>");
                xmlStr.Replace("&lt;/fr&gt;", "</fr>");
                xmlStr.Replace("&lt;hr&gt;", "<fr>");
                xmlStr.Replace("&lt;/hr&gt;", "</fr>");
                xmlStr.Replace("&lt;fi&gt;", "<fi>");
                xmlStr.Replace("&lt;/fi&gt;", "</fi>");
                xmlStr.Replace("&lt;/Source&gt;", "</Source>");
                xmlStr.Replace("&lt;Source&gt;", "<Source>");

                xmlStr.Replace("&", "#$#");

                xmlStr.Replace("\r", "");

                File.WriteAllText(tempXMlPath, xmlStr.ToString());

                xmlStr = new StringBuilder(InsertDoctype(tempXMlPath));
                File.WriteAllText(tempXMlPath, xmlStr.ToString());

                Reader = XmlReader.Create(tempXMlPath, ReaderSettings);

                MyXmlDocument = new XmlDocument();
                //if (preserveSpace)
                    MyXmlDocument.PreserveWhitespace = true;

                MyXmlDocument.Load(Reader);
                
                
            }
            catch (XmlException ex)
            {
                if (ExceptionFired != null)
                {
                    ExceptionFired(ex);
                }
                //Console.WriteLine("Error in xml file. Please check..");
                //Console.WriteLine("Error :" + ex.Message);
                //Console.ReadLine();
                //Environment.Exit(0);
                return false;
            }
            finally
            {
                Reader.Close();
                File.Delete(tempXMlPath);
            }
            return true;
        }


        private string InsertDoctype(string InputXML)
        {

            /////Read Xml file
            StringBuilder xmlStr = new StringBuilder(File.ReadAllText(InputXML));

            string Doctype = "<!DOCTYPE component PUBLIC \"-//JWS//DTD WileyML 20101020 Vers 3Gv1.0.3//EN\" \"Wileyml3gv103-flat.dtd\">";

            int ePos = xmlStr.ToString().IndexOf(".dtd");
            if (ePos == -1)
            {
                int DoctypePos = xmlStr.ToString().IndexOf("?>");
                DoctypePos = DoctypePos + 2;

                xmlStr.Insert(DoctypePos, Doctype);
                ePos = xmlStr.ToString().IndexOf(".dtd");
            }


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
                    }

                   
                }
            }

            return xmlStr.ToString();

        }
        public  bool  LoadXml()
         {
             try
             {
                 if (!XmlFilePath.Equals(""))
                 {
                     SetReaderSettings();
                     if (LoadXmlFile())
                     {
                         return true;
                     }
                     else
                         return false;
                 }
                 else
                     return false;
             }
             catch (XmlException ex)
             {
                 if (ExceptionFired != null)
                 {
                     ExceptionFired(ex);
                 }
                 return false;
             }
         }
    }
}

