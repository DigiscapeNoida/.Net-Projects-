using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for JIDDETAILS
/// </summary>
public class JIDDETAILS
{
    string _JID             = "";
    string  _JOURNALTITLE    = "";
    string _PRODUCTIONSITE  = "";
    string _CUSTOMER        = "";
    int         _SNO = 0;


    public int SNO
    {
        get { return _SNO; }
        set { _SNO = value; }
    }

    public string JID
    {
        get{return _JID;}
        set { _JID = value; }
    }

    public string JOURNALTITLE
    {
        get { return _JOURNALTITLE; }
        set { _JOURNALTITLE = value; }
    }

    public string PRODUCTIONSITE
    {
        get { return _PRODUCTIONSITE; }
        set { _PRODUCTIONSITE = value; }
    }

    public string CUSTOMER
    {
        get { return _CUSTOMER; }
        set { _CUSTOMER = value; }
    }

    public JIDDETAILS(int SN, string JID,string JOURNALTITLE,string PRODUCTIONSITE,string CUSTOMER)
    {
        _SNO = SN;
           _JID             = JID;
           _JOURNALTITLE    = JOURNALTITLE;
           _PRODUCTIONSITE  = PRODUCTIONSITE;
           _CUSTOMER        = CUSTOMER;
    }
	public JIDDETAILS()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
