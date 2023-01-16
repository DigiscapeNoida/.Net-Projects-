using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domino;
using System.Collections;
using System.IO;

namespace FMSIntegrateUsingEOOLink
{
    class DownloadAttachements
    {
        NotesDocument Document;        
        public DownloadAttachements(NotesDocument Document)
        {            
            this.Document = Document;
        }
        public void Download(string JidAid)
        {
            //string pAttachment;
            //ArrayList oAlist = new ArrayList();
            //try
            //{
            //  //  string DocId = Document.UniversalID;
            //     string path = System.Configuration.ConfigurationSettings.AppSettings["DownloadPath"].ToString() ;
            //    if (!Directory.Exists(path))
            //    {
            //        Directory.CreateDirectory(path);
            //    }
            //    else
            //    {
            //        Directory.CreateDirectory(path);
            //    }

            //    object[] AllDocItems = (object[])Document.Items;
            //    foreach (object CurItem in AllDocItems)
            //    {
            //        NotesItem nItem = (NotesItem)CurItem;
            //        if (IT_TYPE.ATTACHMENT == nItem.type)
            //        {
            //            pAttachment = ((object[])nItem.Values)[0].ToString();
            //            FileInfo Fi = new FileInfo(pAttachment);
            //            if (Fi.Extension.ToUpper() == ".HTML")
            //            {
            //                Document.GetAttachment(pAttachment).ExtractFile(path + "\\" + JidAid + ".html");
            //             //   string _attachment = path + "\\" + pAttachment;
            //                break;
            //            }
            //        }
            //    }
            //}
            //catch (Exception)
            //{
                
            //}
        }

        
    }
}
