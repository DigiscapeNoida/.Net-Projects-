using System;
using System.Web.Configuration;
using System.IO;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for Class1
/// </summary>
public class JIDList
{
    StringCollection _JIDList= new StringCollection ();


    public StringCollection JID
    {
        get {
            return _JIDList;
        }
    }
    public JIDList()
	{
       try
       {
           string TargetFolderPath = WebConfigurationManager.AppSettings["TargetFolderPath"];
           if (TargetFolderPath != null)
           {
               if (Directory.Exists(TargetFolderPath))
               {
                   String[] DL = Directory.GetDirectories(TargetFolderPath);
                   foreach (string DN in DL)
                       _JIDList.Add(Path.GetFileName(DN));
               }
               else
               {
                   _JIDList.Add("Not exist");
               }
           }
       }
       catch (Exception ex)
       {
           _JIDList.Add(ex.Message);
       }

        
	}
}
