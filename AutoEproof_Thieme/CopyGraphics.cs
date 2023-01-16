using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using ProcessNotification;
using MsgRcvr;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
namespace AutoEproof
{
    class CopyGraphicsToTDXPS : MessageEventArgs
    {

        public void StartProcess(string CopyGraphicsFolder)
        {
            string[] MNTFolders = Directory.GetDirectories(CopyGraphicsFolder, "*110");

            foreach (string MNTFolder in MNTFolders)
            {
                ProcessEventHandler(MNTFolder + " to be EPout MNTFolder");
                ProcessMNT(MNTFolder);
            }
        }

        private void ProcessMNT(string MNTFolder)
        {
            MNTInfo MNTObj = new MNTInfo(MNTFolder);
            string MNT_GRAPHIC = MNTObj.MNTFolder + "_GRAPHIC";
            string MNT_ART = MNT_GRAPHIC + "\\ART";
            string MNT_CMYK = MNT_GRAPHIC + "\\ART\\cmyk";

            if (!Directory.Exists(MNT_GRAPHIC))
                Directory.CreateDirectory(MNT_GRAPHIC);

            if (!Directory.Exists(MNT_ART))
                Directory.CreateDirectory(MNT_ART);

            if (!Directory.Exists(MNT_CMYK))
                Directory.CreateDirectory(MNT_CMYK);


            string[] tif = Directory.GetFiles(MNTFolder, "*.tif");
            string[] eps = Directory.GetFiles(MNTFolder, "*.eps");
            foreach (string t in tif)
            {

         
                string CopyTo = MNT_ART + "\\" + Path.GetFileName(t);
                string CopyToCMYK = MNT_CMYK + "\\" + Path.GetFileName(t);

                File.Copy(t, CopyTo, true);
                File.Copy(t, CopyToCMYK, true);

                ProcessEventHandler(CopyTo);
            }
            foreach (string e in eps)
            {
                string CopyTo = MNT_ART + "\\" + Path.GetFileName(e);
                string CopyToCMYK = MNT_CMYK + "\\" + Path.GetFileName(e);

                File.Copy(e, CopyTo, true);
                File.Copy(e, CopyToCMYK, true);

                ProcessEventHandler(CopyTo);
            }

            if (CopyToTemp(MNTFolder, MNTObj, "*.tif"))
            {
                if (CopyToTemp(MNTFolder, MNTObj, "*.eps"))
                {
                    string ZipPath = MNT_GRAPHIC + ".zip";

                    FastZip Zip = new FastZip();
                    Zip.CreateZip(ZipPath, MNT_GRAPHIC, true, "");


                    ProcessEventHandler("CopyToTemp Process start.");
                    if (CopyFileToTemp(ZipPath))
                    {

                        if (PrcsMoveToQC(MNTFolder, MNTObj))
                        {
                            try
                            {
                                Directory.Delete(MNTFolder, true);
                                Directory.Delete(MNT_GRAPHIC, true);
                                File.Delete(ZipPath);
                            }
                            catch (Exception ex)
                            {
                                ProcessErrorHandler(ex);
                            }
                            ProcessEventHandler("MNTFolder deleted ::" + MNTFolder);
                        }
                    }
                }
            }
            ProcessEventHandler("CopyToTemp Process finished.");
        }




        private bool CopyFileToTemp(string CopyFrom)
        {
            bool Rslt = false;
            string graphicsinput = Path.GetDirectoryName(ConfigDetails.TDXPSGangtokTempFolder) + "\\graphicsinput";

            ProcessEventHandler("graphicsinput ::" + graphicsinput);
            if (Directory.Exists(graphicsinput))
            {
                try
                {
                    string CopyTo = graphicsinput + "\\" + Path.GetFileName(CopyFrom);
                    ProcessEventHandler("File is being copied.");
                    ProcessEventHandler("CopyFrom ::" + CopyFrom);
                    ProcessEventHandler("CopyTo ::" + CopyTo);
                    if (File.Exists(CopyTo))
                    {
                        ProcessEventHandler("File.Delete :: " + CopyTo);
                        File.Delete(CopyTo);
                    }

                    //////Do'nt change it  
                    string GrDir = CopyTo.Replace(".zip", "");

                    if (Directory.Exists(GrDir))
                    {
                        ProcessEventHandler("Folder Delete :: " + GrDir);
                        Directory.Delete(GrDir, true);
                    }
                    File.Copy(CopyFrom, CopyTo, true);
                    ProcessEventHandler("File has been copied.");

                    Rslt = true;
                }
                catch (Exception ex)
                {
                    ProcessErrorHandler(ex);
                }
            }
            else
            {
                ProcessEventHandler("graphicsinput does not exist::" + graphicsinput);
            }
            return Rslt;
        }

        private bool CopyToTemp(string MNTFolder, MNTInfo MNTObj, string SrchOptn)
        {
            bool Rslt = false;
            string[] Files = Directory.GetFiles(MNTFolder, SrchOptn);

            if (Files.Length == 0) 
                return true;

            string TempMNTFolder = ConfigDetails.TDXPSGangtokTempFolder + "\\" + Path.GetFileName(MNTObj.MNTFolder);
            ProcessEventHandler("TempMNTFolder ::" + TempMNTFolder);
            if (Directory.Exists(TempMNTFolder))
            {

                

                int Count = 0;
                foreach (string Fl in Files)
                {
                    string[] ExistingFiles = Directory.GetFiles(TempMNTFolder, Path.GetFileNameWithoutExtension(Fl) + ".*");
                    foreach (string f in ExistingFiles)
                    {
                        File.Delete(f);
                    }

                    string CopyFrom = Fl;
                    string CopyTo = TempMNTFolder + "\\" + Path.GetFileName(Fl);


                    ProcessEventHandler("File is being copied.");
                    ProcessEventHandler("CopyFrom ::" + CopyFrom);
                    ProcessEventHandler("CopyTo ::" + CopyTo);


                    File.Copy(CopyFrom, CopyTo, true);

                    ProcessEventHandler("File has been copied.");
                    Count++;
                }

                if (Files.Length == Count)
                {
                    Rslt = true;
                    //if (MoveToQC(MNTObj))
                    //{
                    //    ProcessEventHandler("Try to message send for author revision.");
                    //    if (Program.SendMsgToGang(MNTObj.Client, MNTObj.JID, MNTObj.AID))
                    //    {
                    //        Directory.Delete(MNTFolder, true);

                    //        ProcessEventHandler("MNTFolder deleted ::" + MNTFolder);
                    //        Rslt = true;
                    //    }
                    //    else
                    //    {
                    //        ProcessEventHandler("Message could not be send.");
                    //    }
                    //}
                }
            }
            else
            {
                ProcessEventHandler("TempMNTFolder does not exist::" + TempMNTFolder);
                //Directory.CreateDirectory(TempMNTFolder);
                ProcessEventHandler("Created TempMNTFolder::" + TempMNTFolder);
            }

            return Rslt;
        }
        private bool PrcsMoveToQC(string MNTFolder, MNTInfo MNTObj)
        {
            bool Rslt = false;
            if (Program.SendMsgToGang(MNTObj.Client, MNTObj.JID, MNTObj.AID, "S200_GRAPHICS"))
            {
                ProcessEventHandler("Message send successfully for S200_GRAPHICS.");

                Rslt = true;
            }
            else
            {
                ProcessEventHandler("Message could not be send.");
            }

            return Rslt;
        }






    }



}
