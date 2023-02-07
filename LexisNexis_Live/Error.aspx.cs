using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TD.Data;
using System.IO;
using System.Web.Services;
using System.Drawing;

namespace LexisNexis
{
    public partial class on_Error : System.Web.UI.Page
    {
        
        //public static string _sortDirection;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[SESSION.LOGGED_USER] == null)
            {
                Response.Redirect("Default.aspx");
            }           
        }

        
        protected void btnLogout_Click(object sender, EventArgs e)
        {
           

            Session[SESSION.LOGGED_USER] = null;
            Response.Redirect("Default.aspx");

        }
       
}
}