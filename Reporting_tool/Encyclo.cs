using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;

namespace report
{
    internal class Encyclo
    {
        static Log log = Log.GetInstance();
        private Encyclo()
        {
        }
        private static Encyclo obj;
        private static readonly object mylockobject = new object();
        public static Encyclo GetInstance()
        {
            if (obj == null)
            {
                lock (mylockobject)
                {
                    if (obj == null)
                    {
                        obj = new Encyclo();
                    }
                }
            }
            return obj;
        }
        public void ProcessEncyclo(DataTable tbl, string action, string file)
        {
            string success = ConfigurationSettings.AppSettings["success"];
            string fail = ConfigurationSettings.AppSettings["fail"];
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                string _collection_Title = tbl.Rows[i]["titre de la collection"].ToString();
                string _folio = tbl.Rows[i]["folio du (fasc ou la fiche)"].ToString();
                string _itm_type = tbl.Rows[i]["type de l'item"].ToString();
                string _itemdtd = tbl.Rows[i]["dtd de l'item"].ToString();
                string _itemstate = tbl.Rows[i]["etat de l'item"].ToString();
                string _title = tbl.Rows[i]["titre"].ToString();
                string _reference = tbl.Rows[i]["référence"].ToString();
                string _nature = tbl.Rows[i]["nature de la demande"].ToString();
                string _datprovision = tbl.Rows[i]["date et heure de mise à disposition dans extranet pour le prestataire"].ToString();
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
                    if (IsInsert(_collection_Title, _title, _reference))
                    {
                        SqlParameter[] paramlista = new SqlParameter[26];
                        paramlista[0] = new SqlParameter("ctitle", _collection_Title);
                        paramlista[1] = new SqlParameter("folio", _folio);
                        paramlista[2] = new SqlParameter("itemtype", _itm_type);
                        paramlista[3] = new SqlParameter("itemdtd", _itemdtd);
                        paramlista[4] = new SqlParameter("itemstate", _itemstate);
                        paramlista[5] = new SqlParameter("title", _title);
                        paramlista[6] = new SqlParameter("reference", _reference);
                        paramlista[7] = new SqlParameter("nature", _nature);
                        paramlista[8] = new SqlParameter("dateandtime", _datprovision);
                        paramlista[9] = new SqlParameter("nbiteration", _iteration);
                        paramlista[10] = new SqlParameter("enddate", _enddate);
                        paramlista[11] = new SqlParameter("applicant", _applicant);
                        paramlista[12] = new SqlParameter("pagebefore", _pagebefore);
                        paramlista[13] = new SqlParameter("signbefore", _signbefore);
                        paramlista[14] = new SqlParameter("pageafter", _pageafter);
                        paramlista[15] = new SqlParameter("signafter", _signafter);
                        paramlista[16] = new SqlParameter("statedeligation", _statedeligation);
                        paramlista[17] = new SqlParameter("comment", _comment);
                        paramlista[18] = new SqlParameter("status", "OPEN");
                        DataTable tbl1 = GetRecords(_collection_Title, _title, _reference);
                        if (tbl1.Rows.Count > 0)
                        {
                            paramlista[19] = new SqlParameter("stage", "REVISED");
                        }
                        else
                        {
                            paramlista[19] = new SqlParameter("stage", "FRESH");
                        }
                        paramlista[20] = new SqlParameter("modifiedby", _modifiedby);
                        paramlista[21] = new SqlParameter("reviceiteration", tbl1.Rows.Count.ToString());
                        paramlista[22] = new SqlParameter("duedate", _duedate);
                        DateTime e1;
                        DateTime.TryParseExact(_datprovision, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out e1);
                        if (e1.ToString() == "01-01-0001 00:00:00")
                        {
                            paramlista[23] = new SqlParameter("ordate", DBNull.Value);
                        }
                        else
                        {
                            paramlista[23] = new SqlParameter("ordate", e1);
                        }
                        DateTime e2;
                        DateTime.TryParseExact(_enddate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out e2);
                        if (e2.ToString() == "01-01-0001 00:00:00")
                        {
                            paramlista[24] = new SqlParameter("crdate", DBNull.Value);
                        }
                        else
                        {
                            paramlista[24] = new SqlParameter("crdate", e2);
                        }
                        paramlista[25] = new SqlParameter("deadline", _deadline);
                        Db db = Db.GetInstance();
                        string ins = db.InsertUpdateData("usp_insertrecord", paramlista);
                        if (ins == "1")
                        {
                            log.Generatelog("Record inserted. File : "+Path.GetFileName(file));
                            File.Move(file, success + "\\" + Path.GetFileName(file));
                        }
                        else
                        {
                            log.Generatelog("Record not inserted. File : "+ Path.GetFileName(file));
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
                    string encid = GetLastRecordId(_collection_Title, _title, _reference);
                    if (encid != "0")
                    {
                        Db db = Db.GetInstance();
                        if (action == "cancel")
                        {
                            string update_data = db.AddUpdateData("update LexisNexis_Encyclopedia set Status='CANCELLED', Cancellation_Date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where Encyc_Id='" + encid + "'");
                            if (update_data == "1")
                            {
                                log.Generatelog("Cancel record updated. File : " + Path.GetFileName(file));
                                File.Move(file, success + "\\" + Path.GetFileName(file));
                            }
                            else
                            {
                                log.Generatelog("Error found while updating record. File : " + Path.GetFileName(file));
                                File.Move(file, fail + "\\" + Path.GetFileName(file));
                            }
                        }
                        if (action == "refuse")
                        {
                            string update_data = db.AddUpdateData("update LexisNexis_Encyclopedia set Status='REJECTED' where Encyc_Id='" + encid + "'");
                            if (update_data == "1")
                            {
                                log.Generatelog("Refused record updated. File : " + Path.GetFileName(file));
                                File.Move(file, success + "\\" + Path.GetFileName(file));
                            }
                            else
                            {
                                log.Generatelog("Error found while updating record. FIle : " + Path.GetFileName(file));
                                File.Move(file, fail + "\\" + Path.GetFileName(file));
                            }
                        }
                        if (action == "delivered")
                        {
                            SqlParameter[] paramlistb = new SqlParameter[8];
                            paramlistb[0] = new SqlParameter("enid", encid);
                            paramlistb[1] = new SqlParameter("nbiteration", _iteration);
                            DateTime s2;
                            DateTime.TryParseExact(_enddate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out s2);
                            if (s2.ToString() == "01-01-0001 00:00:00")
                            {
                                paramlistb[2] = new SqlParameter("crdate", DBNull.Value);
                            }
                            else
                            {
                                paramlistb[2] = new SqlParameter("crdate", s2);
                            }
                            paramlistb[3] = new SqlParameter("enddate", _enddate);
                            paramlistb[4] = new SqlParameter("pageafter", _pageafter);
                            paramlistb[5] = new SqlParameter("signafter", _signafter);
                            paramlistb[6] = new SqlParameter("comment", _comment);
                            paramlistb[7] = new SqlParameter("status", "CLOSED");
                            string update_data = db.InsertUpdateData("usp_updaterecord", paramlistb);
                            if (update_data == "1")
                            {
                                log.Generatelog("Delivered record updated. File : " + Path.GetFileName(file));
                                File.Move(file, success + "\\" + Path.GetFileName(file));
                            }
                            else
                            {
                                log.Generatelog("Error found while updating record. File : " + Path.GetFileName(file));
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
        public DataTable GetRecords(string ctt, string tt, string reff)
        {
            DataTable dt = new DataTable();
            SqlParameter[] sqpmc = new SqlParameter[3];
            sqpmc[0] = new SqlParameter("ctitle", ctt);
            sqpmc[1] = new SqlParameter("title", tt);
            sqpmc[2] = new SqlParameter("reference", reff);
            Db db = Db.GetInstance();
            dt = db.GetData("usp_getrecord", sqpmc);
            return dt;
        }
        public string GetLastRecordId(string ctt, string tt, string reff)
        {
            string ret = "0";
            DataTable dt = new DataTable();
            SqlParameter[] sqpmd = new SqlParameter[3];
            sqpmd[0] = new SqlParameter("ctitle", ctt);
            sqpmd[1] = new SqlParameter("title", tt);
            sqpmd[2] = new SqlParameter("reference", reff);
            Db db = Db.GetInstance();
            dt = db.GetData("usp_getlastrecord", sqpmd);
            if (dt.Rows.Count > 0)
            {
                ret = dt.Rows[0][0].ToString();
            }
            return ret;
        }
        public bool IsInsert(string ctt, string tt, string reff)
        {
            bool ret = true;
            DataTable dt = new DataTable();
            SqlParameter[] sqpme = new SqlParameter[3];
            sqpme[0] = new SqlParameter("ctitle", ctt);
            sqpme[1] = new SqlParameter("title", tt);
            sqpme[2] = new SqlParameter("reference", reff);
            Db db = Db.GetInstance();
            dt = db.GetData("usp_getlastrecord", sqpme);
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
