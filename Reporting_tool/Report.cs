using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace report
{
    internal class Report
    {
        static Log log = Log.GetInstance();
        private Report()
        {
        }
        private static Report obj;
        private static readonly object mylockobject = new object();
        public static Report GetInstance()
        {
            if (obj == null)
            {
                lock (mylockobject)
                {
                    if (obj == null)
                    {
                        obj = new Report();
                    }
                }
            }
            return obj;
        }
        public void CreareReport()
        {
            Db db = Db.GetInstance();
            DataTable dt = db.GetData1("select * from lnreport where mail_sent='0' order by id asc");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string rpt = dt.Rows[i]["report_type"].ToString().Trim().ToLower();
                    if (rpt == "rev")
                    {
                        getReportRev(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][0].ToString());
                    }
                   else if (rpt == "enc")
                    {
                        getReportEnc(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][0].ToString());
                    }
                    else if (rpt == "ficj")
                    {
                        getReportFicj(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][0].ToString());
                    }
                    else if (rpt == "ficf")
                    {
                        getReportFicFp(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][0].ToString());
                    }
                    else if (rpt == "syn")
                    {
                        getReportSyn(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][0].ToString());
                    }
                }
            }
        }
        public void send_mail(string at, string body, string subject)
        {
            MailMessage Email = new MailMessage();
            string[] mailto = ConfigurationManager.AppSettings["mail_to"].Split(',');
            if (mailto.Length == 1)
            {
                Email.To.Add(new MailAddress(mailto[0].Trim()));
            }
            else
            {
                foreach (string m in mailto)
                {
                    Email.To.Add(new MailAddress(m.Trim()));
                }
            }
            string[] mailcc = ConfigurationManager.AppSettings["mail_cc"].Trim().Split(',');
            if (mailcc.Length > 0)
            {
                if (mailcc.Length == 1)
                {
                    Email.CC.Add(new MailAddress(mailcc[0].Trim()));
                }
                else
                {
                    foreach (string n in mailcc)
                    {
                        Email.CC.Add(new MailAddress(n.Trim()));
                    }
                }
            }
            Email.From = new MailAddress(ConfigurationManager.AppSettings["mail_from"]);
            Email.IsBodyHtml = true;
            Email.Body = body;
            Email.Subject = subject + " " + DateTime.Now.ToString();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            SmtpClient eMailClient = new SmtpClient("smtp.office365.com");
            eMailClient.UseDefaultCredentials = false;
            eMailClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["mail_from"], ConfigurationManager.AppSettings["pwd"]);
            eMailClient.Port = 587;
            eMailClient.EnableSsl = true;
            eMailClient.Timeout = 600000;
            Email.Bcc.Add(new MailAddress("deepak.verma@digiscapetech.com"));
            Attachment ach = new Attachment(at);
            Email.Attachments.Add(ach);
            eMailClient.Send(Email);
            log.Generatelog("Mail sent successfully.");
            Email.Attachments.Dispose();
        }
        public void getReportRev(string a, string b, string rid)
        {
            string c = b;
            Killexl();
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            b = DateTime.Parse(b).AddDays(1).ToString().Split(' ')[0];
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("dt_in1", a.Split('-')[2] + a.Split('-')[1] + a.Split('-')[0]);
            param[1] = new SqlParameter("dt_out1", b.Split('-')[2] + b.Split('-')[1] + b.Split('-')[0]);
            Db db = Db.GetInstance();
            DataTable dt1 = db.GetData1("ln_report", param);
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
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 1; j < 28; j++)
                {
                    xlWorkSheet.Cells[i + 2, j] = dt1.Rows[i][j - 1];
                }
            }
            string f1 = ConfigurationSettings.AppSettings["FilePath"] + "LN-Report_Revues_" + a + "_" + c + ".xlsx";
            if (File.Exists(f1))
            {
                File.Delete(f1);
            }
            xlWorkBook.SaveCopyAs(f1);
            Thread.Sleep(2000);
            Killexl();
            log.Generatelog("File created : " + f1);
            send_mail(f1, "Please find report.", "LN Report Revues");
            db.AddUpdateData1("update lnreport set mail_sent='1' where id='" + rid + "'");
            log.Generatelog("Revue report sent : " + Path.GetFileName(f1));
        }
        public void getReportEnc(string a, string b, string rid)
        {
            string c = b;
            Killexl();
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            b = DateTime.Parse(b).AddDays(1).ToString().Split(' ')[0];
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            SqlParameter[] param1 = new SqlParameter[2];
            param1[0] = new SqlParameter("dt_in2", a.Split('-')[2] + a.Split('-')[1] + a.Split('-')[0]);
            param1[1] = new SqlParameter("dt_out2", b.Split('-')[2] + b.Split('-')[1] + b.Split('-')[0]);
            Db db = Db.GetInstance();
            DataTable dt1 = db.GetData("enc_report", param1);
            xlWorkSheet.Cells[1, 1] = "ID";
            xlWorkSheet.Cells[1, 2] = "Titre de la collection";
            xlWorkSheet.Cells[1, 3] = "Folio du (Fasc ou la fiche)";
            xlWorkSheet.Cells[1, 4] = "Type de l'iTem";
            xlWorkSheet.Cells[1, 5] = "DTD de l'Item";
            xlWorkSheet.Cells[1, 6] = "Etat de l'item";
            xlWorkSheet.Cells[1, 7] = "Titre";
            xlWorkSheet.Cells[1, 8] = "Référence";
            xlWorkSheet.Cells[1, 9] = "Nature de la demande";
            xlWorkSheet.Cells[1, 10] = "Date de réception";
            xlWorkSheet.Cells[1, 11] = "Délai de retour";
            xlWorkSheet.Cells[1, 12] = "Nb d’itération prestataire";
            xlWorkSheet.Cells[1, 13] = "Date de fin";
            xlWorkSheet.Cells[1, 14] = "Nom du demandeur (édito)";
            xlWorkSheet.Cells[1, 15] = "Nb Page avant";
            xlWorkSheet.Cells[1, 16] = "Nb Signes avant";
            xlWorkSheet.Cells[1, 17] = "Nb Page après";
            xlWorkSheet.Cells[1, 18] = "Nb Signes après";
            xlWorkSheet.Cells[1, 19] = "Etat de délégation";
            xlWorkSheet.Cells[1, 20] = "Commentaire";
            xlWorkSheet.Cells[1, 21] = "Statut";
            xlWorkSheet.Cells[1, 22] = "Étape du processus";
            xlWorkSheet.Cells[1, 23] = "Modifié par";
            xlWorkSheet.Cells[1, 24] = "Réviser l'itération";
            xlWorkSheet.Cells[1, 25] = "Date de retour souhaitée";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 1; j < 26; j++)
                {
                    xlWorkSheet.Cells[i + 2, j] = dt1.Rows[i][j - 1];
                }
            }
            string f1 = ConfigurationSettings.AppSettings["FilePath"] + "LN-Report_Encyclopedia_" + a + "_" + c + ".xlsx";
            if (File.Exists(f1))
            {
                File.Delete(f1);
            }
            xlWorkBook.SaveCopyAs(f1);
            Thread.Sleep(2000);
            Killexl();
            log.Generatelog("File created : " + f1);
            send_mail(f1, "Please find report.", "LN Report Encyclopedia");
            db.AddUpdateData1("update lnreport set mail_sent='1' where id='" + rid + "'");
            log.Generatelog("Encyclopedia report sent : " + Path.GetFileName(f1));
        }
        public void getReportFicj(string a, string b, string rid)
        {
            string c = b;
            Killexl();
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            b = DateTime.Parse(b).AddDays(1).ToString().Split(' ')[0];
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            SqlParameter[] param2 = new SqlParameter[2];
            param2[0] = new SqlParameter("dt_in4", a.Split('-')[2] + a.Split('-')[1] + a.Split('-')[0]);
            param2[1] = new SqlParameter("dt_out4", b.Split('-')[2] + b.Split('-')[1] + b.Split('-')[0]);
            Db db = Db.GetInstance();
            DataTable dt1 = db.GetData("fichj_report", param2);
            xlWorkSheet.Cells[1, 1] = "ID";
            xlWorkSheet.Cells[1, 2] = "Titre de la collection";
            xlWorkSheet.Cells[1, 3] = "Folio du (Fasc ou la fiche)";
            xlWorkSheet.Cells[1, 4] = "Type de l'iTem";
            xlWorkSheet.Cells[1, 5] = "DTD de l'Item";
            xlWorkSheet.Cells[1, 6] = "Etat de l'item";
            xlWorkSheet.Cells[1, 7] = "Titre";
            xlWorkSheet.Cells[1, 8] = "Référence";
            xlWorkSheet.Cells[1, 9] = "Nature de la demande";
            xlWorkSheet.Cells[1, 10] = "Date de réception";
            xlWorkSheet.Cells[1, 11] = "Délai de retour";
            xlWorkSheet.Cells[1, 12] = "Nb d’itération prestataire";
            xlWorkSheet.Cells[1, 13] = "Date de fin";
            xlWorkSheet.Cells[1, 14] = "Nom du demandeur (édito)";
            xlWorkSheet.Cells[1, 15] = "Nb Page avant";
            xlWorkSheet.Cells[1, 16] = "Nb Signes avant";
            xlWorkSheet.Cells[1, 17] = "Nb Page après";
            xlWorkSheet.Cells[1, 18] = "Nb Signes après";
            xlWorkSheet.Cells[1, 19] = "Etat de délégation";
            xlWorkSheet.Cells[1, 20] = "Commentaire";
            xlWorkSheet.Cells[1, 21] = "Statut";
            xlWorkSheet.Cells[1, 22] = "Étape du processus";
            xlWorkSheet.Cells[1, 23] = "Modifié par";
            xlWorkSheet.Cells[1, 24] = "Réviser l'itération";
            xlWorkSheet.Cells[1, 25] = "Date de retour souhaitée";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 1; j < 26; j++)
                {
                    xlWorkSheet.Cells[i + 2, j] = dt1.Rows[i][j - 1];
                }
            }
            string f1 = ConfigurationSettings.AppSettings["FilePath"] + "LN-Report_Fiches-J_" + a + "_" + c + ".xlsx";
            if (File.Exists(f1))
            {
                File.Delete(f1);
            }
            xlWorkBook.SaveCopyAs(f1);
            Thread.Sleep(2000);
            Killexl();
            log.Generatelog("File created : " + f1);
            send_mail(f1, "Please find report.", "LN Report Fiches-J");
            db.AddUpdateData1("update lnreport set mail_sent='1' where id='" + rid + "'");
            log.Generatelog("Fiches-J report sent : " + Path.GetFileName(f1));
        }
        public void getReportFicFp(string a, string b, string rid)
        {
            string c = b;
            Killexl();
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            b = DateTime.Parse(b).AddDays(1).ToString().Split(' ')[0];
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            SqlParameter[] param3 = new SqlParameter[2];
            param3[0] = new SqlParameter("dt_in3", a.Split('-')[2] + a.Split('-')[1] + a.Split('-')[0]);
            param3[1] = new SqlParameter("dt_out3", b.Split('-')[2] + b.Split('-')[1] + b.Split('-')[0]);
            Db db = Db.GetInstance();
            DataTable dt1 = db.GetData("fichFP_report", param3);
            xlWorkSheet.Cells[1, 1] = "ID";
            xlWorkSheet.Cells[1, 2] = "Titre de la fiche pratique";
            xlWorkSheet.Cells[1, 3] = "KID";
            xlWorkSheet.Cells[1, 4] = "Type de la fiche";
            xlWorkSheet.Cells[1, 5] = "Rédaction";
            xlWorkSheet.Cells[1, 6] = "Etat de la fiche pratique";
            xlWorkSheet.Cells[1, 7] = "Nature de la demande";
            xlWorkSheet.Cells[1, 8] = "Date de réception";
            xlWorkSheet.Cells[1, 9] = "Délai de retour";
            xlWorkSheet.Cells[1, 10] = "Nb d’itération prestataire";
            xlWorkSheet.Cells[1, 11] = "Date de fin";
            xlWorkSheet.Cells[1, 12] = "Nom du demandeur (édito)";
            xlWorkSheet.Cells[1, 13] = "Nb Page avant";
            xlWorkSheet.Cells[1, 14] = "Nb Signes avant";
            xlWorkSheet.Cells[1, 15] = "Nb Page après";
            xlWorkSheet.Cells[1, 16] = "Nb Signes après";
            xlWorkSheet.Cells[1, 17] = "Etat de délégation";
            xlWorkSheet.Cells[1, 18] = "Commentaire";
            xlWorkSheet.Cells[1, 19] = "Statut";
            xlWorkSheet.Cells[1, 20] = "Étape du processus";
            xlWorkSheet.Cells[1, 21] = "Modifié par";
            xlWorkSheet.Cells[1, 22] = "Réviser l'itération";
            xlWorkSheet.Cells[1, 23] = "Date de retour souhaitée";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 1; j < 24; j++)
                {
                    xlWorkSheet.Cells[i + 2, j] = dt1.Rows[i][j - 1];
                }
            }
            string f1 = ConfigurationSettings.AppSettings["FilePath"] + "LN-Report_Fiches-FP_" + a + "_" + c + ".xlsx";
            if (File.Exists(f1))
            {
                File.Delete(f1);
            }
            xlWorkBook.SaveCopyAs(f1);
            Thread.Sleep(2000);
            Killexl();
            log.Generatelog("File created : " + f1);
            send_mail(f1, "Please find report.", "LN Report Fiches-FP");
            db.AddUpdateData1("update lnreport set mail_sent='1' where id='" + rid + "'");
            log.Generatelog("Fiches-FP report sent : " + Path.GetFileName(f1));
        }
        public void getReportSyn(string a, string b, string rid)
        {
            string c = b;
            Killexl();
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            b = DateTime.Parse(b).AddDays(1).ToString().Split(' ')[0];
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            SqlParameter[] param4 = new SqlParameter[2];
            param4[0] = new SqlParameter("dt_in5", a.Split('-')[2] + a.Split('-')[1] + a.Split('-')[0]);
            param4[1] = new SqlParameter("dt_out5", b.Split('-')[2] + b.Split('-')[1] + b.Split('-')[0]);
            Db db = Db.GetInstance();
            DataTable dt1 = db.GetData("syn_report", param4);
            xlWorkSheet.Cells[1, 1] = "ID";
            xlWorkSheet.Cells[1, 2] = "Titre  du Fasc de synthèse";
            xlWorkSheet.Cells[1, 3] = "KID";
            xlWorkSheet.Cells[1, 4] = "Collection";
            xlWorkSheet.Cells[1, 5] = "Etat du Fasc de synthèse";
            xlWorkSheet.Cells[1, 6] = "Nature de la demande";
            xlWorkSheet.Cells[1, 7] = "Date de réception";
            xlWorkSheet.Cells[1, 8] = "Délai de retour";
            xlWorkSheet.Cells[1, 9] = "Nb d’itération prestataire";
            xlWorkSheet.Cells[1, 10] = "Date de fin";
            xlWorkSheet.Cells[1, 11] = "Nom du demandeur (édito)";
            xlWorkSheet.Cells[1, 12] = "Nb Page avant";
            xlWorkSheet.Cells[1, 13] = "Nb Signes avant";
            xlWorkSheet.Cells[1, 14] = "Nb Page après";
            xlWorkSheet.Cells[1, 15] = "Nb Signes après";
            xlWorkSheet.Cells[1, 16] = "Etat de délégation";
            xlWorkSheet.Cells[1, 17] = "Commentaire";
            xlWorkSheet.Cells[1, 18] = "Statut";
            xlWorkSheet.Cells[1, 19] = "Étape du processus";
            xlWorkSheet.Cells[1, 20] = "Modifié par";
            xlWorkSheet.Cells[1, 21] = "Réviser l'itération";
            xlWorkSheet.Cells[1, 22] = "Date de retour souhaitée";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 1; j < 23; j++)
                {
                    xlWorkSheet.Cells[i + 2, j] = dt1.Rows[i][j - 1];
                }
            }
            string f1 = ConfigurationSettings.AppSettings["FilePath"] + "LN-Report_Syntheses_" + a + "_" + c + ".xlsx";
            if (File.Exists(f1))
            {
                File.Delete(f1);
            }
            xlWorkBook.SaveCopyAs(f1);
            Thread.Sleep(2000);
            Killexl();
            log.Generatelog("File created : " + f1);
            send_mail(f1, "Please find report.", "LN Report Syntheses");
            db.AddUpdateData1("update lnreport set mail_sent='1' where id='" + rid + "'");
            log.Generatelog("Syntheses report sent : " + Path.GetFileName(f1));
        }
        static void Killexl()
        {
            System.Diagnostics.Process[] localByName = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process p in localByName)
            {
                try
                {
                    p.Kill();
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
