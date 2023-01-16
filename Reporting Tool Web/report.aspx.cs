using System;
using System.Data;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web;

namespace WebApplication2
{
    public partial class report : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            if (Session["uid"] == null)
            {
                Response.Redirect("logout.aspx");
            }
            else
            {
                if (Session["uid"].ToString() != "leonard")
                {
                    Response.Redirect("login.aspx");
                }
            }
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Label1.Text = Calendar1.SelectedDate.ToString().Split(' ')[0];
            lblerror.Text = null;
        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            Label2.Text = Calendar2.SelectedDate.ToString().Split(' ')[0];
            lblerror.Text = null;
        }
        protected void Reset_Click(object sender, EventArgs e)
        {
            lblerror.Text = null;
        }
        protected void Get_report(string a, string b)
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                lblerror.Text = "Excel is not properly installed!!";
                return;
            }
            b = DateTime.Parse(b).AddDays(1).ToString().Split(' ')[0];
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());
            //SqlCommand cmd = new SqlCommand("Ln_report", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@dt_in", a.Split('-')[2] + a.Split('-')[1] + a.Split('-')[0]);
            //cmd.Parameters.AddWithValue("@dt_out", b.Split('-')[2] + b.Split('-')[1] + b.Split('-')[0]);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //lblerror.Text = "Wait";
            //conn.Open();
            //da.Fill(dt);
            //conn.Close();
            xlWorkSheet.Cells[1, 1] = "ID";
            xlWorkSheet.Cells[1, 2] = "Titre de la revue";
            xlWorkSheet.Cells[1, 3] = "Type";
            xlWorkSheet.Cells[1, 4] = "Titre d'article";
            xlWorkSheet.Cells[1, 5] = "Auteur";
            xlWorkSheet.Cells[1, 6] = "Type d'article";
            xlWorkSheet.Cells[1, 7] = "N° publ.";
            xlWorkSheet.Cells[1, 8] = "Demandeur";
            xlWorkSheet.Cells[1, 9] = "Travail à réaliser";
            xlWorkSheet.Cells[1, 10] = "Délai";
            xlWorkSheet.Cells[1, 11] = "Itération";
            xlWorkSheet.Cells[1, 12] = "Nb Page avant";
            xlWorkSheet.Cells[1, 13] = "Nb Word avant";
            xlWorkSheet.Cells[1, 14] = "Nb Signes avant";
            xlWorkSheet.Cells[1, 15] = "Nb Page après";
            xlWorkSheet.Cells[1, 16] = "Nb Word après";
            xlWorkSheet.Cells[1, 17] = "Nb Signes après";
            xlWorkSheet.Cells[1, 18] = "Statut";
            xlWorkSheet.Cells[1, 19] = "Date denvoi";
            xlWorkSheet.Cells[1, 20] = "Date de retour souhaitée";
            xlWorkSheet.Cells[1, 21] = "Date révisée";
            xlWorkSheet.Cells[1, 22] = "Date de retour réelle";
            xlWorkSheet.Cells[1, 23] = "Retard (J)";
            xlWorkSheet.Cells[1, 24] = "Commentaire";
            xlWorkSheet.Cells[1, 25] = "Remarques";
            xlWorkSheet.Cells[1, 26] = "DepartmentCode";
            xlWorkSheet.Cells[1, 27] = "ProduitCode";
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    for (int j = 1; j < 28; j++)
            //    {
            //        xlWorkSheet.Cells[i + 2, j] = dt.Rows[i][j - 1];
            //    }
            //}
            string f1 = ConfigurationSettings.AppSettings["FilePath1"];
            string f2 = ConfigurationSettings.AppSettings["FilePath2"];
            if (File.Exists(f1))
            {
                File.Delete(f1);
            }
            if (File.Exists(f2))
            {
                File.Delete(f2);
            }
            xlWorkBook.SaveCopyAs(f1);
            Thread.Sleep(5000);
            Killexl();
            xlWorkBook.SaveAs(f2, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            Thread.Sleep(5000);
            Killexl();
            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
            lblerror.Text = null;
            Response.ContentType = "Application/x-msexcel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Ln_Report.xlsx");
            string f3 = Path.GetFileName(f1);
            Response.TransmitFile(Server.MapPath("~/" + f3));
            Response.End();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            lblerror.Text = null;
            if (RadioButtonList1.SelectedValue=="")
            {
                lblerror.Text = "Please select report type.";
                return;
            }
            if (Label1.Text.Trim().Length == 0 || Label2.Text.Trim().Length == 0)
            {
                lblerror.Text = "Please select both dates.";
                return;
            }
            if (DateTime.Parse(Label1.Text.Trim()) > DateTime.Parse(Label2.Text.Trim()))
            {
                lblerror.Text = "Start date can not be greater than end date.";
                return;
            }
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Sql"].ToString());
            try
            {
                string a=Label1.Text.Trim();
                string b=Label2.Text.Trim();               
                SqlDataAdapter da = new SqlDataAdapter("select * from lnreport where from_date='"+a+"' and to_date='"+b+"' and report_type='"+RadioButtonList1.SelectedValue+"'", conn);
                DataTable dt = new DataTable();
                conn.Open();
                da.Fill(dt);
                SqlCommand cmd;
                if (dt.Rows.Count > 0)
                {
                    cmd=new SqlCommand("update lnreport set mail_sent='0', date_updated=getdate() where id='" + dt.Rows[0][0] +"'", conn);
                }
                else
                {
                    cmd = new SqlCommand("insert into lnreport (from_date, to_date, report_type) values ('"+a+"', '"+b+"', '"+RadioButtonList1.SelectedValue+"')", conn);
                }
                cmd.ExecuteNonQuery();
                conn.Close();
                lblerror.Text = "Your request is submitted successfully. Report will be sent on 'zeenat.peyrye@thomsondigital.com' within next 15 min.";
            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message;
            }
            finally
            {
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("report.aspx");
        }
        protected void Killexl()
        {
            System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process p in localByName)
            {
                //if ((DateTime.Now - p.StartTime).Minutes > 2)
                //{
                try
                {
                    p.Kill();
                }
                catch
                {
                    continue;
                }
                // }
            }
        }
    }
}