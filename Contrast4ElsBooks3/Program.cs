using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Contrast4ElsBooks
{
    class Program
    {
        static void Main(string[] args)
        {

            //string OrderTemplate = "A01";
            //string strTemplate = System.Configuration.ConfigurationSettings.AppSettings["Template"];
            //bool asd = false;
            //string[] TemplateArr = strTemplate.Split(',');

            //for (int j = 0; j < TemplateArr.Length; j++)
            //{
            //    if (TemplateArr[j] == null)
            //        continue;
            //    else if (TemplateArr[j].Length == 0)
            //        continue;
            //    else if (OrderTemplate.IndexOf(TemplateArr[j]) != -1)
            //    {
            //        asd = true;
            //    }
            //}




            //            FtpProcess oFtpProcess = new FtpProcess("ftp.elsevierproofcentral.com", "ÏN", "tombk2", "gJ5jaaDws");
            //
            //          oFtpProcess.UploadFileToFTP(@"C:\Users\57916\Desktop\1.zip.zip");

            //FtpProcess oFtpProcess = new FtpProcess("ftp.elsevierproofcentral.com", "ÏN", "tombk2", "gJ5jaaDws");

            //oFtpProcess.UploadFileToFTP(@"C:\Users\57916\Desktop\1.zip.zip");
            //Creating Dataset
    
            //string error, PII, ISBN;

            //DatasetCreation obj = new DatasetCreation(DI.FullName);
            //bool valid = obj.CreateDataset(out error, out PII, out ISBN);


            Console.Title = "Contrast for elsevier book";
            WorkerClass WC = new WorkerClass();
            //WC.mTask();
            BackGroundProccessOne oBGTomCreation = new BackGroundProccessOne(WC, 45000);
            oBGTomCreation.Start();
                   
            //Console.ReadLine();
            //edited by kshitj
            // Console.ReadLine();
            //Commented by kshitij for debugging           
            
            //Running Vtool

            WorkerValidation oWorkerValidation = new WorkerValidation();
            BackGroundProccessOne oBGVtoolValidate = new BackGroundProccessOne(oWorkerValidation, 20000);
            oBGVtoolValidate.Start();


            //Upload on FTP
            UploadWorker oWorkerUpload = new UploadWorker();
            BackGroundProccessOne oBGVUpload = new BackGroundProccessOne(oWorkerUpload, 25000);
            oBGVUpload.Start();


            //Delete info from Database for Rerun Vtool.
            WorkerReVtool WRT = new WorkerReVtool();
            BackGroundProccessOne oBGWRerun = new BackGroundProccessOne(WRT, 45000);
            oBGWRerun.Start();
            
                         
            while (true)
            {
                string Quit = Console.ReadLine();
                if (Quit.Trim() == "QUIT")
                {
                    break;
                }
            }
        }
    }
    public interface Work
    {
        bool mTask();
        bool OnError();
        bool OnSuccess();
    }
    class BackGroundProccessOne
    {
        BackgroundWorker m_oWorker;
        int globlepass = 0;
        int globleFilled = 0;
        Work Iwork = null;
        int period = 2000;
        public BackGroundProccessOne(Work Iwork, int period)
        {
            this.period = period;
            this.Iwork = Iwork;
            m_oWorker = new BackgroundWorker();
            m_oWorker.DoWork += new DoWorkEventHandler(oWorker_DoWork);
            m_oWorker.ProgressChanged += new ProgressChangedEventHandler(oWorker_ProgressChanged);
            m_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(oWorker_RunWorkerCompleted);
            m_oWorker.WorkerReportsProgress = true;
            m_oWorker.WorkerSupportsCancellation = true;
        }
        public bool Start()
        {
            m_oWorker.RunWorkerAsync();
            //// edited by kshitij
            //Console.ReadLine(); 
            return false;
        }
        public bool Stop()
        {
            m_oWorker.CancelAsync();
            return false;
        }
        private void oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Threading.Thread.Sleep(period);
            Console.WriteLine("Failed :: " + globleFilled);
            Start();
        }
        private void oWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState.ToString() == "Completd")
            {
                //globlepass = globlepass + 1;
            }
            else
            {
                globleFilled = globleFilled + 1;
            }
        }
        private void oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Iwork.mTask())
            {
                Iwork.OnSuccess();
                m_oWorker.ReportProgress(100, "Completd");

            }
            else
            {
                Iwork.OnError();
                m_oWorker.ReportProgress(100, "Failed");
            }
        }
    }
}
