using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;

public partial class Browse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string order_Path = "";
        order_Path = Session["path"].ToString();
        if (order_Path.IndexOf("TYPESET-ORDER") > -1)
        {
            if (File.Exists(order_Path))
            {
                string order_Path_Temp = "";
                order_Path_Temp = order_Path.Replace(Path.GetFileName(order_Path), Path.GetFileNameWithoutExtension(order_Path) + "_Temp.xml");
                StreamReader sr = new StreamReader(order_Path,System.Text.Encoding.Default);
                string order_text = sr.ReadToEnd();
                order_text = order_text.Replace("ppmorder11.dtd" + (char)34 + ">", "C:\\Inetpub\\wwwroot\\BOOKTRACKEMAILS\\ppmorder11.dtd" + (char)34 + ">" + "<?xml-stylesheet type=" + (char)34 + "text/xsl" + (char)34 + " href=" + (char)34 + "C:\\Inetpub\\wwwroot\\BOOKTRACKEMAILS\\ppmorder.xsl" + (char)34 + "?>");
                order_text = order_text.Replace("ppmorder12.dtd" + (char)34 + ">", "C:\\Inetpub\\wwwroot\\BOOKTRACKEMAILS\\ppmorder12.dtd" + (char)34 + ">" + "<?xml-stylesheet type=" + (char)34 + "text/xsl" + (char)34 + " href=" + (char)34 + "C:\\Inetpub\\wwwroot\\BOOKTRACKEMAILS\\ppmorder.xsl" + (char)34 + "?>");
                sr.Close();
                StreamWriter sw = new StreamWriter(order_Path_Temp,false,System.Text.Encoding.Default);
                sw.WriteLine(order_text);
                sw.Close();
                order_Path = order_Path_Temp;
            }
        }
        XPathDocument doc = new XPathDocument(order_Path);
        XslTransform transform = new XslTransform();
        if (order_Path.IndexOf("TYPESET-ORDER") > -1)
        {
            transform.Load(Server.MapPath("~/App_Data/ppmorder.xsl"));
        }
        else
        {
            transform.Load(Server.MapPath("~/App_Data/order.xsl"));
        }
        transform.Transform(doc, null, Response.Output);
        if (order_Path.IndexOf("TYPESET-ORDER") > -1)
        {
            File.Delete(order_Path);
        }

    }
}
