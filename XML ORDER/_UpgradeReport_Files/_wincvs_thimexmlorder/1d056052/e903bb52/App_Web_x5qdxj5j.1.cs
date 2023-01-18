#pragma checksum "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9AAFCBD65AD6B757D6E6EAADEE8B213AA2361B88"

#line 1 "D:\WinCVS\ThimeXMLORDER\UserControl\UCrDate.ascx.cs"
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class UCrDate: System.Web.UI.UserControl
    {

        //private int _Date;
        //private int _Month;
        //private int _Year;

        public int Date
        {
            get { return int.Parse(DDLDay.Text); }
            set { DDLDay.Text = value.ToString(); }
        }
        public int Month
        {
            get { return int.Parse(DDLMonth.Text); }
            set { DDLMonth.Text = value.ToString(); }
        }
        public int Year
        {
            get { return int.Parse(DDLYear.Text); }
            set { DDLYear.Text = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DDLDay.Items.Add(new ListItem("--"));
            for (int i = 1; i <= 31; i++)
            {
                DDLDay.Items.Add(new ListItem(i.ToString()));
            }
            //String[] months = new String[] { "Jan", "Feb", "March", "April", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };

            DDLMonth.Items.Add(new ListItem("--"));
            for (int i = 1; i <= 15; i++)
            {
                DDLMonth.Items.Add(new ListItem(i.ToString()));
            }

            DDLYear.Items.Add(new ListItem("--"));
            for (int i = 0; i <= 5; i++)
            {
                DDLYear.Items.Add(new ListItem((DateTime.Now.Year - i).ToString()));
            }
        }
    }


#line default
#line hidden
