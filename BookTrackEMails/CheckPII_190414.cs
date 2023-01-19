using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Text;
public class CheckPII
{
    string PPMOrderPath = "";
    XmlDocument XD = new XmlDocument();
    XmlNodeList NLIST = null;
    string PPMISBN = "";
    string pii = "";
    string booktitle = "";
    string booksubtitle = "";
    string bookeditors = "";
    List<string> lst=new List<string>();
    public CheckPII()
	{
        PPMOrderPath = System.Configuration.ConfigurationManager.AppSettings["PPMPIIPATH"].ToString();  
	}
    public string Checkpii_ppm(string PPMISBN)
    {
        try
        {
            PPMISBN = PPMISBN.Replace("-", "");
            string[] Dinfo = Directory.GetDirectories(PPMOrderPath);
            for (int i = 0; i < Dinfo.Length; i++)
            {
                if (Directory.Exists(PPMOrderPath + "\\" + PPMISBN))
                {
                    string[] Fname = Directory.GetFiles(PPMOrderPath + "\\" + PPMISBN + "\\TYPESET-ORDER\\Current_order", "*.xml");
                    for (int j = 0; j < Fname.Length; j++)
                    {
                        string filename = Path.GetFileName(Fname[j]).ToLower();

                        if (filename.StartsWith("kup") == true)
                        {
                            File.Copy(Fname[j], "C:\\ppm.xml", true);
                        }

                    }
                    if (File.Exists("C:\\ppm.xml"))
                    {
                        pii = getpii(PPMISBN, "C:\\ppm.xml");
                        File.Delete("C:\\ppm.xml");
                        return pii;
                    }
                }
            }
            return "";
        }
        catch(Exception ex)
        {
            return "";
        }
       
    }
    public string getpii(string isbn, string fpath)
    {
        string tpii = "";
        string tbt = "";
        string tbst = "";
        string tbedt = "";
        string timp = "";
        
        string tissn = "";   
        string tvol = "";    
        string tsertit = ""; 
        string tJid = "";
        string tdiv="";

        string year = "";
        string editor = "";
        string trimsize = "";
        string color = "";
        string OWNER="INC";


        XD = new XmlDocument();

        try
        {
            XD.XmlResolver = null;
            XD.Load(fpath);
            NLIST = XD.GetElementsByTagName("pii");
            for (int i = 0; i < NLIST.Count; i++)
            {
                tpii = NLIST[0].InnerText;

            }
            NLIST = XD.GetElementsByTagName("book-title");
            for (int i = 0; i < NLIST.Count; i++)
            {
                tbt = NLIST[0].InnerText;

            }
            NLIST = XD.GetElementsByTagName("subtitle");
            for (int i = 0; i < NLIST.Count; i++)
            {
                tbst = NLIST[0].InnerText;

            }
            NLIST = XD.GetElementsByTagName("originator");
            lst.Clear();
            for (int i = 0; i < NLIST.Count; i++)
            {
                string attrVal = NLIST[i].Attributes["sort-order"].Value;
                lst.Add(attrVal);
                //tbedt = NLIST[0].InnerText;
            }
            string maxattrval = lst.Max();
            NLIST = XD.GetElementsByTagName("last-name");
            lst.Clear();
            for (int i = 0; i < NLIST.Count; i++)
            {
                //string attrVal = NLIST[i].Attributes["sort-order"].Value;
                //lst.Add(attrVal);
                tbedt = NLIST[NLIST.Count-1].InnerText;
                
            }

            NLIST = XD.GetElementsByTagName("imprint");
            for (int i = 0; i < NLIST.Count; i++)
            {
                timp = NLIST[0].InnerText;

            }

            /*      New fields add           */
            try
            {
                NLIST = XD.GetElementsByTagName("issn");
                for (int i = 0; i < NLIST.Count; i++)
                {
                    tissn = NLIST[0].InnerText;

                }
                NLIST = XD.GetElementsByTagName("jid");
                for (int i = 0; i < NLIST.Count; i++)
                {
                    tJid = NLIST[0].InnerText;
                }

                NLIST = XD.GetElementsByTagName("series-title");
                for (int i = 0; i < NLIST.Count; i++)
                {
                    tsertit = NLIST[0].InnerText;
                }
                NLIST = XD.GetElementsByTagName("volume-number");
                for (int i = 0; i < NLIST.Count; i++)
                {
                    tvol = NLIST[0].InnerText;
                }

                NLIST = XD.GetElementsByTagName("division");
                for (int i = 0; i < NLIST.Count; i++)
                {
                    tdiv = NLIST[0].InnerText;
                }

                NLIST = XD.GetElementsByTagName("edition-no");
                for (int i = 0; i < NLIST.Count; i++)
                {
                   editor  = NLIST[0].InnerText;
                }
                NLIST = XD.GetElementsByTagName("interior-colors");
                for (int i = 0; i < NLIST.Count; i++)
                {
                    color = NLIST[0].InnerText;
                }
                NLIST = XD.GetElementsByTagName("format");
                for (int i = 0; i < NLIST.Count; i++)
                {
                    trimsize = NLIST[0].InnerText;
                }
                NLIST = XD.GetElementsByTagName("copyright-yr");
                for (int i = 0; i < NLIST.Count; i++)
                {
                    year = NLIST[0].InnerText;
                }

                NLIST = XD.GetElementsByTagName("ownership");
                for (int i = 0; i < NLIST.Count; i++)
                {
                    OWNER = NLIST[0].InnerText;
                }
 
            }
            catch(Exception ree){            
            }

            return tpii + "_" + tbt + "_" + tbst + "_" + tbedt + "_" + timp + "_" + tissn + "_" + tJid + "_" + tsertit + "_" + tvol + "_" + tdiv +            "_" +  year + "_" + editor + "_" + trimsize + "_" +  color  + "_"  + OWNER  ;
        }
        catch (Exception ex)
        {
            return "";
        }
    }
}
