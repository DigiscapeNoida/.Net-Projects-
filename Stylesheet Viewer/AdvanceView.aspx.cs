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

public partial class AdvanceView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Xml xmlObj =new Xml();
        xmlObj.DocumentSource="~/COMPAG-jss.xml";
        string str = xmlObj.Document.GetElementsByTagName("baseData")[0].OuterXml;
        xmlObj.Document.DocumentElement.InnerXml = str;
        xml1.DocumentContent = xmlObj.Document.OuterXml;
        //PlaceHolder1.Controls.Add(td);

        
  
    }
}
