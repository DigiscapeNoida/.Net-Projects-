using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Contrast4ElsBooks
{
   public class WorkerClass:Work
    {
        
        public WorkerClass()
        {
           
        } 
       #region Work Members
       public bool mTask()
        {
           
            string errStatus = string.Empty;
            string excode = "";
            try
            {
               string[] dir = Directory.GetFiles(GlobalConfig.oGlobalConfig.Marker,"*.ini"); 
               foreach (string itms in dir)
               {
                   
                   FileInfo FII = new FileInfo(itms);
                   string directory  = FII.Name.Replace(FII.Extension, "");
                   excode = FII.Name.ToString();
                   directory = GlobalConfig.oGlobalConfig.Source + "\\" + directory;
                   errStatus = directory;
                   DirectoryInfo DI = new DirectoryInfo(directory);
                   string dirname = DI.Name;
                   bool isIsbn = false;
                   Double rslt ;
                   isIsbn = Double.TryParse(dirname, out rslt);

                   if (isIsbn == true)
                   {
                       string error = "";
                       string PII = "";
                       string ISBN = "";
                       DatasetCreation obj = new DatasetCreation(DI.FullName);
                       
                       bool valid = obj.CreateDataset(out error,out PII,out ISBN);
                       if (valid)
                       {
                           GlobalConfig.oGlobalConfig.WriteLog("Tombk generated for :: " + dirname + " at - " + DateTime.Now.ToString("hh-mm-ss tt"));
                       }
                       else
                       {
                           GlobalConfig.oGlobalConfig.Send_Error(error,ISBN,PII,"Failed","");
                       }

                       Console.WriteLine("Checking for Directory: " + GlobalConfig.oGlobalConfig.ProcessPath + "\\" + dirname);
                       if (Directory.Exists(GlobalConfig.oGlobalConfig.ProcessPath + "\\" + dirname))
                       {
                           Console.WriteLine("Deleting Direcotries");
                           var tsk2 = Task.Run(() =>
                              Directory.Delete(GlobalConfig.oGlobalConfig.ProcessPath + "\\" + dirname,true));                              
                           tsk2.Wait();
                           Console.WriteLine("Deletion Completed");
                       }

                       Console.WriteLine("Moving Direcotries/files: Source:" + GlobalConfig.oGlobalConfig.Source + "\\" + dirname);
                       Console.WriteLine("Moving Direcotries/files: Target:" +GlobalConfig.oGlobalConfig.ProcessPath + "\\" + dirname);
                       var tsk = Task.Run(()=>                            
                           Directory.Move(GlobalConfig.oGlobalConfig.Source + "\\" + dirname, GlobalConfig.oGlobalConfig.ProcessPath + "\\" + dirname));
                       tsk.Wait();
                       Console.WriteLine("Copying Completed");
                   }
                   Console.WriteLine("Deleting Marker: " + itms);
                   File.Delete(itms);
                   Console.WriteLine("Deleting Marker: Done");                   
               }
               Console.WriteLine("************* Waiting to get input.");
               return true;
            }
            catch (Exception err)
            {
                GlobalConfig.oGlobalConfig.Send_Error(err.Message.ToString() + " While creating " + errStatus, excode,"","Failed", "");
                return false;                 
            }
        }
        public bool OnError()
        {
            return true;
        }
        public bool OnSuccess()
        {
            return true;
        }
        #endregion
    }
}
