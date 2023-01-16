using System;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using FMSIntegrateUsingEOOLink;

//using ExcelRead;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using System.Configuration;

using OpenPop.Pop3;
using OpenPop.Mime;
using System.Linq;

namespace FMSIntegrateUsingEOOLink
{
    //public void NotifyMsg(string NotificationMsg);
    //public void NotifyErrMsg(Exception Ex);

    class Program
    {
        public class Email
        {
            public Email()
            {
                this.Attachments = new List<Attachment>();
            }
            public int MessageNumber { get; set; }
            public string From { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public DateTime DateSent { get; set; }
            public List<Attachment> Attachments { get; set; }
        }
        static void Main(string[] args)
        {
            

            Console.Title = "EOO Link";
            Console.WriteLine("eProofMailsProcess");

            string ExeName = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (System.Diagnostics.Process.GetProcessesByName(ExeName).Length > 1)
            {
                return;
            }


            StaticInfo.WriteLogMsg.AppendLog("Process Start");
            EOOLinkProcess EOOLinkProcessOBJ = new EOOLinkProcess();
            //EOOLinkProcessOBJ.ProcessNotification += new NotifyMsg(EOOLinkProcessOBJ_ProcessNotification);
            //EOOLinkProcessOBJ.ErrorNotification += new NotifyErrMsg(EOOLinkProcessOBJ_ErrorNotification);
            Console.WriteLine("EOOLinkProcessOBJ.StartProcessOnlyDownload()");
            EOOLinkProcessOBJ.StartProcessOnlyDownload();
            StaticInfo.WriteLogMsg.AppendLog("Process END");


            //if (args.Length > 0)
            //{
            //    eFirstThieme eFirstThiemeObj = new eFirstThieme();
            //    eFirstThiemeObj.ProcessNotification += EOOLinkProcessOBJ_ProcessNotification;
            //    eFirstThiemeObj.ErrorNotification += EOOLinkProcessOBJ_ErrorNotification;
            //    eFirstThiemeObj.StartProcess();     ///////////Process Thieme Eoff-Print 

                

            ////}

            //    ArticleAontentReport.ProcessACRExcel ACRExcelOBJ = new ArticleAontentReport.ProcessACRExcel(ConfigDetails.ACRDownloadPath);
            //    ACRExcelOBJ.ErrorNotification += new ArticleAontentReport.NotifyErrMsg(EOOLinkProcessOBJ_ErrorNotification);
            //    ACRExcelOBJ.ProcessNotification += new ArticleAontentReport.NotifyMsg(EOOLinkProcessOBJ_ProcessNotification);
            //    ACRExcelOBJ.StartProcess();

            //    StaticInfo.WriteLogMsg.WriteLogFileInDate();
          
        }

        

        //static void EOOLinkProcessOBJ_ErrorNotification(Exception Ex)
        //{
        //    StaticInfo.WriteLogMsg.AppendLog("*****************Error Details***********************");
        //    Console.WriteLine(Ex.Message);
        //    StaticInfo.WriteLogMsg.AppendLog(Ex.Message);
        //    StaticInfo.WriteLogMsg.AppendLog(Ex.StackTrace);
        //    StaticInfo.WriteLogMsg.AppendLog(Ex.Source);
        //    StaticInfo.WriteLogMsg.AppendLog("*****************************************************");
        //}

        static void EOOLinkProcessOBJ_ProcessNotification(string NotificationMsg)
        {
            Console.WriteLine(NotificationMsg);
            StaticInfo.WriteLogMsg.AppendLog(NotificationMsg);
        }
        //static void Test()
        //{
        //    ArticleAontentReport.ProcessACRExcel ACRExcelOBJ = new ArticleAontentReport.ProcessACRExcel(ConfigDetails.ACRDownloadPath);
        //    ACRExcelOBJ.ErrorNotification += new ArticleAontentReport.NotifyErrMsg(EOOLinkProcessOBJ_ErrorNotification);
        //    ACRExcelOBJ.ProcessNotification += new ArticleAontentReport.NotifyMsg(EOOLinkProcessOBJ_ProcessNotification);
        //    ACRExcelOBJ.StartProcess();

        //    string s = "Data Source=10.10.23.62;Initial Catalog=GTK_1P1PLIVE;Persist Security Info=True;User ID=sa;Password=p@ssw0rd";
        //    SqlParameter [] para = new SqlParameter [1];

        //    para [0] = new SqlParameter ("@Stage", null);
        //    SqlHelper.ExecuteReader(s, "usp_GetPlanner", para);
        //}

        //static void InsertJKSMail()
        //{
        //    string Year = "2014";
        //    string JID= "SRCCM";
        //    string Stage= "FIZ";

        //    string XmlPath = @"\\wip\Thieme_3B2\For_Final_Thieme\online\FIZ2012-13\Uploaded\FIZ2014\SRCCM\srccm35(3)";
        //    string []XMLS = Directory.GetFiles(XmlPath, "*.xml", SearchOption.AllDirectories);

        //    foreach(string XML in XMLS)
        //    {
        //        Temp Obj = new Temp(JID, XML, Stage,Year);
        //        Obj.GetArticleDteail();
        //        Obj.InsertEfirstDetail();
        //    }
        //}
    }
}
