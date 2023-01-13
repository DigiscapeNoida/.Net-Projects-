using System;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Configuration;
using System.IO;

namespace report
{
    internal class Synth
    {
        static Log log = Log.GetInstance();
        private Synth()
        {
        }
        private static Synth obj;
        private static readonly object mylockobject = new object();
        public static Synth GetInstance()
        {
            if (obj == null)
            {
                lock (mylockobject)
                {
                    if (obj == null)
                    {
                        obj = new Synth();
                    }
                }
            }
            return obj;
        }
        public void ProcessSynth(DataTable tbl, string action, string file)
        {
            string success = ConfigurationSettings.AppSettings["success"];
            string fail = ConfigurationSettings.AppSettings["fail"];
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                string _title = tbl.Rows[i]["titre  du fasc de synthèse"].ToString();
                string _kid = tbl.Rows[i]["kid"].ToString();
                string _collection = tbl.Rows[i]["collection"].ToString();
                string _state = tbl.Rows[i]["etat du fasc de synthèse"].ToString();
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
                string _statedeligation = tbl.Rows[i]["etat de délégation"].ToString();
                string _comment = tbl.Rows[i]["commentaire"].ToString();
                string _modifiedby = tbl.Rows[i]["modifié par"].ToString();
                string _duedate = tbl.Rows[i]["date de retour souhaitée"].ToString();
                if (action == "insert")
                {
                    if (IsInsert(_title, _kid))
                    {
                        SqlParameter[] paramlist = new SqlParameter[23];
                        paramlist[0] = new SqlParameter("title", _title);
                        paramlist[1] = new SqlParameter("kid", _kid);
                        paramlist[2] = new SqlParameter("collection", _collection);
                        paramlist[3] = new SqlParameter("state", _state);
                        paramlist[4] = new SqlParameter("nature", _nature);
                        paramlist[5] = new SqlParameter("dateandtime", _dateandtime);
                        paramlist[6] = new SqlParameter("deadline", _deadline);
                        paramlist[7] = new SqlParameter("nbiteration", _iteration);
                        paramlist[8] = new SqlParameter("enddate", _enddate);
                        paramlist[9] = new SqlParameter("applicant", _applicant);
                        paramlist[10] = new SqlParameter("pagebefore", _pagebefore);
                        paramlist[11] = new SqlParameter("signbefore", _signbefore);
                        paramlist[12] = new SqlParameter("pageafter", _pageafter);
                        paramlist[13] = new SqlParameter("signafter", _signafter);
                        paramlist[14] = new SqlParameter("statedeligation", _statedeligation);
                        paramlist[15] = new SqlParameter("comment", _comment);
                        paramlist[16] = new SqlParameter("status", "OPEN");
                        DataTable tbl1 = GetRecords(_title, _kid);
                        if (tbl1.Rows.Count > 0)
                        {
                            paramlist[17] = new SqlParameter("stage", "REVISED");
                        }
                        else
                        {
                            paramlist[17] = new SqlParameter("stage", "FRESH");
                        }
                        paramlist[18] = new SqlParameter("modifiedby", _modifiedby);
                        paramlist[19] = new SqlParameter("reviceiteration", tbl1.Rows.Count.ToString());
                        paramlist[20] = new SqlParameter("duedate", _duedate);
                        DateTime e1;
                        DateTime.TryParseExact(_dateandtime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out e1);
                        if (e1.ToString() == "01-01-0001 00:00:00")
                        {
                            paramlist[21] = new SqlParameter("ordate", DBNull.Value);
                        }
                        else
                        {
                            paramlist[21] = new SqlParameter("ordate", e1);
                        }
                        DateTime e2;
                        DateTime.TryParseExact(_enddate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out e2);
                        if (e2.ToString() == "01-01-0001 00:00:00")
                        {
                            paramlist[22] = new SqlParameter("crdate", DBNull.Value);
                        }
                        else
                        {
                            paramlist[22] = new SqlParameter("crdate", e2);
                        }
                        Db db = Db.GetInstance();
                        string ins = db.InsertUpdateData("usp_insertrecord_synth", paramlist);
                        if (ins == "1")
                        {
                            log.Generatelog("Synthesis Record inserted. FIle : " + Path.GetFileName(file));
                            File.Move(file, success + "\\" + Path.GetFileName(file));
                        }
                        else
                        {
                            log.Generatelog("Synthesis Record not inserted. File : " + Path.GetFileName(file));
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
                    string encid = GetLastRecordId(_title, _kid);
                    if (encid != "0")
                    {
                        Db db = Db.GetInstance();
                        if (action == "cancel")
                        {
                            string update_data = db.AddUpdateData("update LexisNexis_Synthesis set Status='CANCELLED', Cancellation_Date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where Sync_id='" + encid + "'");
                            if (update_data == "1")
                            {
                                log.Generatelog("Synthesis record updated. FIle : " + Path.GetFileName(file));
                                File.Move(file, success + "\\" + Path.GetFileName(file));
                            }
                            else
                            {
                                log.Generatelog("Error found while updating Synthesis Record. File : " + Path.GetFileName(file));
                                File.Move(file, fail + "\\" + Path.GetFileName(file));
                            }
                        }
                        if (action == "refuse")
                        {
                            string update_data = db.AddUpdateData("update LexisNexis_Synthesis set Status='REJECTED' where Sync_id='" + encid + "'");
                            if (update_data == "1")
                            {
                                log.Generatelog("Refused Synthesis Record updated. File : " + Path.GetFileName(file));
                                File.Move(file, success + "\\" + Path.GetFileName(file));
                            }
                            else
                            {
                                log.Generatelog("Error found while updating Synthesis Record. File : " + Path.GetFileName(file));
                                File.Move(file, fail + "\\" + Path.GetFileName(file));
                            }
                        }
                        if (action == "delivered")
                        {
                            SqlParameter[] paramlist1 = new SqlParameter[8];
                            paramlist1[0] = new SqlParameter("enid", encid);
                            paramlist1[1] = new SqlParameter("nbiteration", _iteration);
                            paramlist1[2] = new SqlParameter("enddate", _enddate);
                            paramlist1[3] = new SqlParameter("pageafter", _pageafter);
                            paramlist1[4] = new SqlParameter("signafter", _signafter);
                            paramlist1[5] = new SqlParameter("comment", _comment);
                            paramlist1[6] = new SqlParameter("status", "CLOSED");
                            DateTime s2;
                            DateTime.TryParseExact(_enddate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out s2);
                            if (s2.ToString() == "01-01-0001 00:00:00")
                            {
                                paramlist1[7] = new SqlParameter("crdate", DBNull.Value);
                            }
                            else
                            {
                                paramlist1[7] = new SqlParameter("crdate", s2);
                            }
                            string update_data = db.InsertUpdateData("usp_updaterecord_synth", paramlist1);
                            if (update_data == "1")
                            {
                                log.Generatelog("Synthesis delivered record updated. File : " + Path.GetFileName(file));
                                File.Move(file, success + "\\" + Path.GetFileName(file));
                            }
                            else
                            {
                                log.Generatelog("Error found while updating Synthesis Record. FIle : " + Path.GetFileName(file));
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
        public DataTable GetRecords(string title1, string kidd)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqpm = new SqlParameter[2];
            sqpm[0] = new SqlParameter("title", title1);
            sqpm[1] = new SqlParameter("kid", kidd);
            Db db = Db.GetInstance();
            dt = db.GetData("usp_getrecord_synth", sqpm);
            return dt;
        }
        public string GetLastRecordId(string title1, string kidd)
        {
            string ret = "0";
            DataTable dt = new DataTable();
            SqlParameter[] sqpm1 = new SqlParameter[2];
            sqpm1[0] = new SqlParameter("title", title1);
            sqpm1[1] = new SqlParameter("kid", kidd);
            Db db = Db.GetInstance();
            dt = db.GetData("usp_getlastrecord_synth", sqpm1);
            if (dt.Rows.Count > 0)
            {
                ret = dt.Rows[0][0].ToString();
            }
            return ret;
        }
        public bool IsInsert(string title1, string kidd)
        {
            bool ret = true;
            DataTable dt = new DataTable();
            SqlParameter[] sqpm2 = new SqlParameter[2];
            sqpm2[0] = new SqlParameter("title", title1);
            sqpm2[1] = new SqlParameter("kid", kidd);
            Db db = Db.GetInstance();
            dt = db.GetData("usp_getlastrecord_synth", sqpm2);
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
