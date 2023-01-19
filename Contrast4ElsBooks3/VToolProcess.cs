using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace Contrast4ElsBooks
{
    class VToolProcess
    {
        public VToolProcess()
        { 
        
        }
        public bool RunVTool(string tombkpath, out string Final, out string vtool)
        {
            try
            {
              

                vtool = "";
                string processbat =  AppDomain.CurrentDomain.BaseDirectory + "\\VTools\\v5.bat";
                string Logpath = AppDomain.CurrentDomain.BaseDirectory + "\\VTools\\VtoolLog";
                              string jar =  AppDomain.CurrentDomain.BaseDirectory + "\\VTools\\Vtool5";
                if (!(Directory.Exists(Logpath)))
                {
                    Final = "Log path for vtool does not exists :: " + Logpath;
                    
                    return false;
                }
                if(!(File.Exists(processbat)))
                {
                    Final = "Vtool batch file does not exists :: " + processbat;
                    return false;
                }   

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = processbat;
               
                DirectoryInfo DI = new DirectoryInfo(tombkpath);
                startInfo.Arguments = "\"" + Logpath + "\\" + DI.Name + "\"" + " " + "\"" + tombkpath + "\"";          
                try
                {  
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }  

                    string logfiles = Logpath + "\\" + DI.Name + ".log.xml";
                    if (File.Exists(logfiles))
                    {
                        string Errortype = string.Empty;
                        if (Check_Result(logfiles, out Errortype))
                        {

                            Final =  Errortype ;
                            return true;
                        }
                        else
                        {
                            vtool = logfiles;
                            Final = Errortype;
                            return false;
                        }

                    }
                    else
                    {
                        Final = "Error: Vtool does not genrate log file ::" + logfiles;
                        return false;
                    }                    
                }
                catch (Exception exp)
                {
                    Final = exp.Message.ToString();
                    return false;
                }                   
            }
            catch (Exception exe) 
            {
                Final = exe.Message.ToString();
                vtool = "";
                return false;                
            }
        }
        public bool Check_Result(string xml,out string nooferror)
        {
            string vtoolError = string.Empty;
            try
            {
                nooferror = "";
                XmlDocument xd = new XmlDocument();
                xd.Load(xml); 

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xd.NameTable);

                nsmgr.AddNamespace("vt", "http://www.elsevier.com/xml/apps/qc/v");
                XmlNodeList Xnodes = xd.SelectNodes("//results/total-errors", nsmgr);
                foreach (XmlNode item in Xnodes)
                {
                    string errors = item.InnerText;
                    if (errors.Trim() == "0")
                    {

                    }
                    else
                    {
                        vtoolError = vtoolError + "Vool Error :: " + errors;
                    }
                }
                nooferror = vtoolError;
                if (string.IsNullOrEmpty(vtoolError))
                {
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            catch (Exception exe)
            {
                nooferror = "Error ::" + exe.Message.ToString();
                return false;
            }
            
        }
    
    }
}
