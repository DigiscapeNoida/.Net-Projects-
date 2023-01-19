
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Data;

namespace Contrast4ElsBooks
{
    class WorkerReVtool :Work
    {

        public bool mTask()
        {
            try
            {
               string[] Files = Directory.GetFiles(GlobalConfig.oGlobalConfig.Failedvtool);
               foreach (string  item in Files)
               {
                   FileInfo FI = new FileInfo(item);
                   string tombk = FI.Name.ToString().Replace(FI.Extension, "");
                   string tquery = "update PCDATASETINFO set Vtool = null where TombkNo = '" + tombk + "'";
                   DataAccess.ExecuteNonQuery(tquery);

                   File.Delete(item);
               }
               Console.WriteLine("************* Waiting to revtool.");
               return true;
            }
            catch (Exception EXE)
            {
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
    }
}
