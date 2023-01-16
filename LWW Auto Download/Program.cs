using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;
using ProcessNotification;

namespace LWWAutoIntegrate
{

    class ArticleInfo1
    {
        public string Client { get; set; }
        public string Stage { get; set; }
        public string ServerIP { get; set; }
        public string Remarks { get; set; }
    }

    public class Program : MessageEventArgs
    {
        static WriteLog _WriteLogObj = null;
        public static void Main(string[] args)
        {
            
            Console.Title = "LWW AutoDownload";
     
            if (args.Length > 0)
            {
                if (args[0].ToUpper().Equals("OPENACCESS"))
                {
                    string CsvEXELoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                   _WriteLogObj = new WriteLog(CsvEXELoc+"\\CSVLog");
                    OpenAccess OpenOBj = new OpenAccess(15);
                    OpenOBj.ProcessNotification += ProcessNotification;
                    OpenOBj.ErrorNotification += ErrorNotification;
                    OpenOBj.Process();

                    _WriteLogObj.WriteLogFileInDate();

                    return;
                }
               
            }


            string ExeName = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            if (System.Diagnostics.Process.GetProcessesByName(ExeName).Length > 1)
            {
                return;
            }

            _WriteLogObj = new WriteLog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ "\\DwnloadLog");
           
            try
            {
                ProcessOne();
            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
            }
            _WriteLogObj.WriteLogFileInDate();


        }
        static void ProcessOne()
        {
            FtptToPrcsInput ProcsOne = new FtptToPrcsInput();
            ProcsOne.ProcessNotification += ProcessNotification;
            ProcsOne.ErrorNotification += ErrorNotification;
            ProcsOne.StartProcess();
        }
     
        static void ErrorNotification(Exception Ex)
        {
            _WriteLogObj.AppendLog("---------Start Error detail------ ");
            _WriteLogObj.AppendLog(Ex);
            _WriteLogObj.AppendLog("---------End Error detail------ ");
        }
        static void ProcessNotification(string NotificationMsg)
        {
            Console.WriteLine(NotificationMsg);
            _WriteLogObj.AppendLog(NotificationMsg);
        }
    }


    public static class SerializeClass
    {
        public static void SerializeObject(this List<string> list, string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<string>));
            using (var stream = File.OpenWrite(fileName))
            {
                serializer.Serialize(stream, list);

            }
        }

        public static string SerializeToXML<T>(List<T> source)
        {
            string xml = "";
            foreach (var item in source)
            {
                if (xml == "")
                {
                    xml = item.ToString();
                }
                else
                {
                    xml = xml + "|" + item.ToString();
                }
            }
            string[] abc=xml.Split('|');
            StringBuilder SerializeXML = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            XmlWriter _XmlWriter = XmlWriter.Create(SerializeXML, settings);
            var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(_XmlWriter, abc.ToString(), emptyNs);
            _XmlWriter.Flush();
            _XmlWriter.Close();
            string s = SerializeXML.ToString().Replace("#$#", "&");
            return s;
        }

        public static void Deserialize(this List<string> list, string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<string>));
            using (var stream = File.OpenRead(fileName))
            {
                var other = (List<string>)(serializer.Deserialize(stream));
                list.Clear();
                list.AddRange(other);
            }
        }
    }
}


