using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PPM_TRACKING_SYSTEM
{
    class GlobalFunc
    {
        public static void LogFunc(string strLog)
        {
            try
            {
                if (!Directory.Exists("Log"))
                {
                    Directory.CreateDirectory("Log");
                }

                string FName = DateTime.Now.Day.ToString().Trim() + "_" + DateTime.Now.Month.ToString().Trim() + "_" + DateTime.Now.Year.ToString().Trim();
                FName = FName + ".txt";              

                StreamWriter sw;

                if (!File.Exists("Log\\" + FName))
                {
                    sw = new StreamWriter("Log\\" + FName);
                }
                else
                {
                    sw = new StreamWriter("Log\\" + FName,true);
                }
                sw.WriteLine(strLog);
               sw.Close();         
            }
            catch (Exception Ex)
            { 
                
            }


        }

    }
}
