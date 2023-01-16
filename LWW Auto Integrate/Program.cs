using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;
using ProcessNotification;
using System.Net.Mail;
using System.Net;

namespace LWWAutoIntegrate
{
   
   public class Program : MessageEventArgs
    {
        static WriteLog _WriteLogObj = null;

        //public static bool IsZipValid(string path)
        //{
        //    try
        //    {
        //        using (var zipFile = ZipFile.OpenRead(path))
        //        {
        //            var entries = zipFile.Entries;
        //            return true;
        //        }
        //    }
        //    catch (InvalidDataException)
        //    {
        //        return false;
        //    }
        //}
        public static void Main(string[] args)
        {
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.IsReady)
                    {
                        Int64 space = drive.TotalFreeSpace; // /1024*1024 in gb
                        space = space / (1024 * 1024 * 1024);
                        if (space < 10)
                        {

                                string MailTo = "deepak.verma@digiscapetech.com";
                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress("eproof@thomsondigital.com");
                                mail.To.Add(MailTo);
                                //if (MailCC != String.Empty)
                                //    mail.CC.Add(MailCC);
                                mail.Bcc.Add("Rohit.singh@digiscapetech.com");
                                mail.Subject = "Space Problem";
                                mail.Body = "No space on :" + drive.Name.ToString();
                                mail.IsBodyHtml = true;
                            //SmtpClient smtp = new SmtpClient();
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                            SmtpClient eMailClient = new SmtpClient("smtp.office365.com");
                            eMailClient.UseDefaultCredentials = false;
                            eMailClient.Credentials = new System.Net.NetworkCredential("eproof@thomsondigital.com", "Welcome@#$4321");
                            eMailClient.Port = 587;
                            eMailClient.EnableSsl = true;
                            eMailClient.Timeout = 600000;
                            //smtp.Host = "192.168.0.4";
                            eMailClient.Send(mail);

                            if (space < 1)
                            {
                                return;
                            }
                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
                
            


            Console.Title = "LWW AutoIntegrate";

            string ExeName = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(ExeName);
            if (System.Diagnostics.Process.GetProcessesByName(ExeName).Length > 1)
            {
                Console.WriteLine("Return if two instance running");                          
                return;
            }

             string  _EXELoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\IntegrationLog";

            _WriteLogObj = new WriteLog(_EXELoc );
           
            try
            {
                ProcessTwo();
            }
            catch (Exception ex)
            {
                ErrorNotification(ex);
            }
            _WriteLogObj.WriteLogFileInDate();


        }
      
        static void ProcessTwo()
        {
            ProcessTwo ProcsTwo = new ProcessTwo();
            ProcsTwo.ProcessNotification += ProcessNotification;
            ProcsTwo.ErrorNotification += ErrorNotification;
            ProcsTwo.StartProcess();
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

            StringBuilder SerializeXML = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(source.GetType());
            var settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            XmlWriter _XmlWriter = XmlWriter.Create(SerializeXML, settings);
            var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            serializer.Serialize(_XmlWriter, source, emptyNs);
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


