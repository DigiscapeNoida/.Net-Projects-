using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data;

public partial class ExtractIsbn : System.Web.UI.Page
{
    int count = 0;

    DataTable dt;
    string Book_series_title;
    string JID;
    string AID;
    string ISSN;
    string Volume;
    string Volume_Issue_PII;
    string Volume_Title;
    string Volume_Subtitle;
    string Volume_Editors;
    string ISBN;
    string Expected_year_publ;
    string Year_of_registration;
    string Stage;
    string cno;
    string PII;
    string DOI;
    string BookId;
    string Job_Type;
    static int cnt = 1;
    static int cnt1 = 1;
    bool VTC = false;
    //----------------------------
    string PIIIssn;
    string PIIIsbn;
    string PIIYear;
    string PIIAid;
    int ClcNum;
    int[] WF = new int[15];
    int[] WFEX = new int[18];
    int i;
    string Tissn;
    string Prefix;

    
}
       
 
