using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using PPM_TRACKING_SYSTEM.Classes.XmlOperation;

namespace PPM_TRACKING_SYSTEM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         string strText = System.Configuration.ConfigurationSettings.AppSettings["FlowTypeActivity"];
         if (strText == "PM")
             Application.Run(new DataForm());
         else if (strText == "AUTO")
             Application.Run(new PPM());
         else
             MessageBox.Show("Please check the Config file, it should be either \"PM\" or \"AUTO\"");
         //Application.Run(new UploadForm());
        // Application.Run(new DataForm());
        }
        
    }
}
