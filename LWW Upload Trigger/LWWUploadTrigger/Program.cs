using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LWWUploadTrigger
{
    class Program
    {
        static void Main(string[] args)
        {
           //check folder for file
            // if file exists copy at our location and insert db
            Process obj = new Process();
            obj.CheckFile();
        }

        
    }
}
