using System;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Configuration;
using System.IO;

namespace report
{
    internal class Fp
    {
        static Log log = Log.GetInstance();
        private Fp()
        {
        }
        private static Fp obj;
        private static readonly object mylockobject = new object();
        public static Fp GetInstance()
        {
            if (obj == null)
            {
                lock (mylockobject)
                {
                    if (obj == null)
                    {
                        obj = new Fp();
                    }
                }
            }
            return obj;
        }
        public void ProcessFp(DataTable tbl, string action, string file)
        {
            string success = ConfigurationSettings.AppSettings["success"];
            string fail = ConfigurationSettings.AppSettings["fail"];
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                string _factsheet = tbl.Rows[i]["titre de la fiche pratique"].ToString();
                string _kid = tbl.Rows[i]["kid"].ToString();
                string _record_type = tbl.Rows[i]["type de la fiche"].ToString();
                string _writing = tbl.Rows[i]["rédaction"].ToString();
                string _factsheetstate = tbl.Rows[i]["Etat de la fiche pratique"].ToString();
                string _nature = tbl.Rows[i]["nature de la demande"].ToString();
                string _dateandtime = tbl.Rows[i]["date et heure de mise à disposition dans extranet pour le prestataire"].ToString();
                string _deadline = tbl.Rows[i]["délai de retour"].ToString();
                string _iteration = tbl.Rows[i]["nb d’itération prestataire"].ToString();
                string _enddate = tbl.Rows[i]["date et heure de fin de traitement"].ToString();
                string _applicant = tbl.Rows[i]["nom du demandeur (édito)"].ToString();
                string _pagebefore = tbl.Rows[i]["nb page avant"].ToString();
                string _signbefore = tbl.Rows[i]["nb signes avant"].ToString();
                string _pageafter = tbl.Rows[i]["nb page après"].ToString();
                string _signafter = tbl.Rows[i]["nb signes après"].ToString();
                string _statedeligation = tbl.Rows[i]["Etat de délégation"].ToString();
                string _comment = tbl.Rows[i]["Commentaire"].ToString();
                string _modifiedby = tbl.Rows[i]["Modifié par"].ToString();
                string _duedate = tbl.Rows[i]["Date de retour souhaitée"].ToString();
                if (action == "insert")
                {
                    if (IsInsert(_factsheet, _kid))
                    {
                        SqlParameter[] paramlistk = new SqlParameter[24];
                        paramlistk[0] = new SqlParameter("fact", _factsheet);
                        paramlistk[1] = new SqlParameter("kid", _kid);
                        paramlistk[2] = new SqlParameter("itemtype", _record_type);
                        paramlistk[3] = new SqlParameter("redaction", _writing);
                        paramlistk[4] = new SqlParameter("itemstate", _factsheetstate);
                        paramlistk[5] = new SqlParameter("nature", _nature);
                        paramlistk[6] = new SqlParameter("dateandtime", _dateandtime);
                        paramlistk[7] = new SqlParameter("deadline", _deadline);
                        paramlistk[8] = new SqlParameter("nbiteration", _iteration);
                        paramlistk[9] = new SqlParameter("enddate", _enddate);
                        paramlistk[10] = new SqlParameter("applicant", _applicant);
                        paramlistk[11] = new SqlParameter("pagebefore", _pagebefore);
                        paramlistk[12] = new SqlParameter("signbefore", _signbefore);
                        paramlistk[13] = new SqlParameter("pageafter", _pageafter);
                        paramlistk[14] = new SqlParameter("signafter", _signafter);
                        paramlistk[15] = new SqlParameter("statedeligation", _statedeligation);
                        paramlistk[16] = new SqlParameter("comment", _comment);
                        paramlistk[17] = new SqlParameter("status", "OPEN");
                        DataTable tbl1 = GetRecords(_factsheet, _kid);
                        if (tbl1.Rows.Count > 0)
                        {
                            paramlistk[18] = new SqlParameter("stage", "REVISED");
                        }
                        else
                        {
                            paramlistk[18] = new SqlParameter("stage", "FRESH");
                        }
                        paramlistk[19] = new SqlParameter("modifiedby", _modifiedby);
                        paramlistk[20] = new SqlParameter("reviceiteration", tbl1.Rows.Count.ToString());
                        paramlistk[21] = new SqlParameter("duedate", _duedate);
                        DateTime e1;
                        DateTime.TryParseExact(_dateandtime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out e1);
                        if (e1.ToString() == "01-01-0001 00:00:00")
                        {
                            paramlistk[22] = new SqlParameter("ordate", DBNull.Value);
                        }
                        else
                        {
                            paramlistk[22] = new SqlParameter("ordate", e1);
                        }
                        DateTime e2;
                        DateTime.TryParseExact(_enddate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out e2);
                        if (e2.ToString() == "01-01-0001 00:00:00")
                        {
                            paramlistk[23] = new SqlParameter("crdate", DBNull.Value);
                        }
                        else
                        {
                            paramlistk[23] = new SqlParameter("crdate", e2);
                        }
                        Db db = Db.GetInstance();
                        string ins = db.InsertUpdateData("usp_insertrecord_fich", paramlistk);
                        if (ins == "1")
                        {
                            log.Generatelog("FP Record inserted. File : " + Path.GetFileName(file));
                            File.Move(file, success + "\\" + Path.GetFileName(file));
                        }
                        else
                        {
                            log.Generatelog("FP Record not inserted. File : " + Path.GetFileName(file));
                            File.Move(file, fail + "\\" + Path.GetFileName(file));
                        }
                    }
                    else
                    {
                        log.Generatelog("Can not insert record because the status of last inserted record is 'OPEN'. File : " + Path.GetFileName(file));
                        File.Move(file, fail + "\\" + Path.GetFileName(file));
                    }
                }
                else
                {
                    string encid = GetLastRecordId(_factsheet, _kid);
                    if (encid != "0")
                    {
                        Db db = Db.GetInstance();
                        if (action == "cancel")
                        {
                            string update_data = db.AddUpdateData("update LexisNexis_Fiches set Status='CANCELLED', Cancellation_Date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where Fact_Id='" + encid + "'");
                            if (update_data == "1")
                            {
                                log.Generatelog("FP record updated. File" + Path.GetFileName(file));
                                File.Move(file, success + "\\" + Path.GetFileName(file));
                            }
                            else
                            {
                                log.Generatelog("Error found while updating FP Record. File : " + Path.GetFileName(file));
                                File.Move(file, fail + "\\" + Path.GetFileName(file));
                            }
                        }
                        if (action == "refuse")
                        {
                            string update_data = db.AddUpdateData("update LexisNexis_Fiches set Status='REJECTED' where Fact_Id='" + encid + "'");
                            if (update_data == "1")
                            {
                                log.Generatelog("Refused FP Record updated. File : " + Path.GetFileName(file));
                                File.Move(file, success + "\\" + Path.GetFileName(file));
                            }
                            else
                            {
                                log.Generatelog("Error found while updating FP Record. File : " + Path.GetFileName(file));
                                File.Move(file, fail + "\\" + Path.GetFileName(file));
                            }
                        }
                        if (action == "delivered")
                        {
                            SqlParameter[] paramlistl = new SqlParameter[8];
                            paramlistl[0] = new SqlParameter("enid", encid);
                            paramlistl[1] = new SqlParameter("nbiteration", _iteration);
                            DateTime s2;
                            DateTime.TryParseExact(_enddate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out s2);
                            if (s2.ToString() == "01-01-0001 00:00:00")
                            {
                                paramlistl[2] = new SqlParameter("crdate", DBNull.Value);
                            }
                            else
                            {
                                paramlistl[2] = new SqlParameter("crdate", s2);
                            }
                            paramlistl[3] = new SqlParameter("enddate", _enddate);
                            paramlistl[4] = new SqlParameter("pageafter", _pageafter);
                            paramlistl[5] = new SqlParameter("signafter", _signafter);
                            paramlistl[6] = new SqlParameter("comment", _comment);
                            paramlistl[7] = new SqlParameter("status", "CLOSED");
                            string update_data = db.InsertUpdateData("usp_updaterecord_fich", paramlistl);
                            if (update_data == "1")
                            {
                                log.Generatelog("Delivered record updated. File : " + Path.GetFileName(file));
                                File.Move(file, success + "\\" + Path.GetFileName(file));
                            }
                            else
                            {
                                log.Generatelog("Error found while updating FP Record. File : " + Path.GetFileName(file));
                                File.Move(file, fail + "\\" + Path.GetFileName(file));
                            }
                        }
                    }
                    else
                    {
                        log.Generatelog("Record not found. File : " + Path.GetFileName(file));
                        File.Move(file, fail + "\\" + Path.GetFileName(file));
                    }
                }
            }
        }
        public DataTable GetRecords(string factsheet, string kidd)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqpmm = new SqlParameter[2];
            sqpmm[0] = new SqlParameter("fact", factsheet);
            sqpmm[1] = new SqlParameter("kid", kidd);
            Db db = Db.GetInstance();
            dt = db.GetData("usp_getrecord_fich", sqpmm);
            return dt;
        }
        public string GetLastRecordId(string factsheet, string kidd)
        {
            string ret = "0";
            DataTable dt = new DataTable();
            SqlParameter[] sqpmn = new SqlParameter[2];
            sqpmn[0] = new SqlParameter("fact", factsheet);
            sqpmn[1] = new SqlParameter("kid", kidd);
            Db db = Db.GetInstance();
            dt = db.GetData("usp_getlastrecord_fich", sqpmn);
            if (dt.Rows.Count > 0)
            {
                ret = dt.Rows[0][0].ToString();
            }
            return ret;
        }
        public bool IsInsert(string factsheet, string kidd)
        {
            bool ret = true;
            DataTable dt = new DataTable();
            SqlParameter[] sqpmo = new SqlParameter[2];
            sqpmo[0] = new SqlParameter("fact", factsheet);
            sqpmo[1] = new SqlParameter("kid", kidd);
            Db db = Db.GetInstance();
            dt = db.GetData("usp_getlastrecord_fich", sqpmo);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Status"].ToString() == "OPEN")
                {
                    ret = false;
                }
            }
            return ret;
        }
    }
}
