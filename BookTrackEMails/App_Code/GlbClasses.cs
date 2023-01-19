using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class GlbClasses
{
    public DataUtility objData = new DataUtility();
    public CheckPII objCpii = new CheckPII();
    public static  int numberredirect;
    public static int numberredirects
    {
        get
    {
        return numberredirect;
    }
        set
        {
            value = numberredirect;
         }
    }
    public  static int index;
    public static int indexs
    {
        get
        {
            return index;
        }
        set
        {
            value = index;
        }
    }
    
	public GlbClasses()
	{
		
	}
}
