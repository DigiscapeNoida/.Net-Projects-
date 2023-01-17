using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace move_jss
{
    internal class Program
    {
        static Log log = new Log();
        static void Main(string[] args)
        {
            while (true)
            {
                string source = ConfigurationSettings.AppSettings["source"].ToString();
                string target = ConfigurationSettings.AppSettings["target"].ToString();
                string[] dirs = Directory.GetDirectories(source, "*.*", SearchOption.TopDirectoryOnly);
                log.Generatelog("============================================================");
                log.Generatelog("Total JIDs : " + dirs.Length.ToString());
                log.Generatelog("Copying files.");
                foreach (string dir in dirs)
                {
                    try
                    {
                        string[] files = Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly);
                        if (files.Length > 0)
                        {
                            string jid = Path.GetFileNameWithoutExtension(dir);
                            if (!Directory.Exists(target + jid))
                            {
                                try
                                {
                                    Directory.CreateDirectory(target + jid);
                                    log.Generatelog("New directory created : " + target + jid);
                                }
                                catch (Exception ex)
                                {
                                    log.Generatelog("Error found : " + ex.Message);
                                }
                            }
                            foreach (string file in files)
                            {
                                try
                                {
                                    File.Copy(file, target + jid + "\\" + Path.GetFileName(file), true);
                                }
                                catch (Exception ex)
                                {
                                    log.Generatelog("Error found : " + ex.Message);
                                    continue;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Generatelog("Error found : " + ex.Message);
                        continue;
                    }
                }
                log.Generatelog("JSS Updated.");
                log.Generatelog("Waiting for 20 min.");
                Thread.Sleep(1200000);
            }
        }
    }
}
