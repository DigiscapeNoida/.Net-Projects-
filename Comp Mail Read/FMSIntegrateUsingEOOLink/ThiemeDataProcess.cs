using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FMSIntegrateUsingEOOLink
{
    public class ThiemeDataProcess
    {
        public event NotifyMsg ProcessNotification;
        public event NotifyErrMsg ErrorNotification;

        #region Get Autor Details
        public static OPSDetail GetOPSDetails(string _JornalTitle)
        {
            _JornalTitle = _JornalTitle.Replace(" ", "").Replace("-", "").Replace("—", "").ToLower();
            OPSDetail OPDT = new OPSDetail();
            using (var context = DataObjectFactory.CreateContext())
            {
                //List<OPSDetail> OPSRows = context.OPSDetails.Where(x=>x.Client.Equals("Thieme",StringComparison.OrdinalIgnoreCase)).ToList();
 
                //foreach (OPSDetail Row in OPSRows)
                //{
                //    string Jname = Row.Jname.Replace(" ", "").Replace("-", "").Replace("—","").ToLower();
                //    if (Jname == _JornalTitle)
                //    {
                //        OPDT = Row;
                //        break;
                //    }
                //    else if (Jname.Contains(_JornalTitle))
                //    {
                //        OPDT = Row;
                //        break;
                //    }
                //}
                return OPDT;
            }
        }
        #endregion


        public static OPSDetail GetOPSDetailsUsingJID(string JID)
        {
            List<OPSDetail> OPSRows = null;
            //Rohit
            //using (var context = DataObjectFactory.CreateContext())
            //{
            //     OPSRows = context.OPSDetails.Where(x=>x.Client.Equals("Thieme",StringComparison.OrdinalIgnoreCase) && x.Jid==JID).ToList();
            //}

            //if (OPSRows != null)
            //{
            //    return OPSRows[0];
            //}
            return null;
        }


        
        #region Insert into Thieme Off Print Details
        public static string InsertThiemeOffDetails(ThiemeOffPrint _ThOffPrint)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                //Rohit
                //try
                //{
                //    var query = context.ThiemeOffPrints.Where(x => x.JID.Equals(_ThOffPrint.JID) && x.AID.Equals(_ThOffPrint.AID)
                //        && x.MailSubjectLine.Equals(_ThOffPrint.MailSubjectLine) && x.DOI.Equals(_ThOffPrint.DOI));
                //    if (query.Count() == 0)
                //    {
                //        context.AddToThiemeOffPrints(_ThOffPrint);
                //        context.SaveChanges();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    return ex.ToString();
                //}
                return _ThOffPrint.SNO.ToString();
            }
        }
        #endregion

        #region Get Thieme OffPrint Details
        public static DataSet GetThiemeOffPrint()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["OPSConnectionString"].ConnectionString; //System.Configuration.ConfigurationSettings.AppSettings["OPSConnectionString"];
            
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlcmd = new SqlCommand())
                {
                    SqlDataAdapter sqlad = new SqlDataAdapter(sqlcmd);
                    DataSet ds = new DataSet();
                    sqlcmd.CommandText = "GetDetailsToSendComplimentary";
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Connection = conn;
                    sqlad.Fill(ds);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds;

                    }

                    return null;
                }

            }
            //using (var context = DataObjectFactory.CreateContext())
            //{
            //    context.Connection.Open();
            //    List<ThiemeOffPrint> TPrint = new List<ThiemeOffPrint>();// Rohit context.ThiemeOffPrints.Where(x => x.STATUS.Equals("InProgress")).ToList();
            //    return TPrint;
            //}
        }
        #endregion

        #region Change Status of ThiemeOffPrint for Efirst
        public static bool ChangeThiemeOffPrintStatus(ThiemeOffPrint _OffPrint)
        {

            //_OffPrint.s
            using (var context = DataObjectFactory.CreateContext())
            {
                //var query = context.ThiemeOffPrints.Where(x => x.JID.Equals(_OffPrint.JID) && x.AID.Equals(_OffPrint.AID)
                //    && x.MailSubjectLine.Equals(_OffPrint.MailSubjectLine)).FirstOrDefault();

                //Rohit
                //var query = ""; context.ThiemeOffPrints.Where(x => x.DOI.Equals(_OffPrint.DOI) && x.STAGE.Equals(_OffPrint.STAGE)).FirstOrDefault();
                    
                //if (query.STATUS.Equals("InProgress",StringComparison.OrdinalIgnoreCase))
                //{
                //    query.STATUS = "Finished";
                //    context.SaveChanges();
                //    return true;
                //}
                return false;
            }
        }
        #endregion

        #region Change Status of ThiemeOffPrint for Fiz
        public static bool ChangeFizThiemeOffPrintStatus(ThiemeOffPrint _OffPrint)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var query = "";//Rohit context.ThiemeOffPrints.Where(x => x.JID.Equals(_OffPrint.JID) && x.MailSubjectLine.Equals(_OffPrint.MailSubjectLine)
                     //&& x.DOI.Equals(_OffPrint.DOI)).FirstOrDefault();
                //if (query.STATUS == "InProgress")
                //{
                //    query.AID = _OffPrint.AID;
                //    query.STATUS = "Finished";
                //    context.SaveChanges();
                //    return true;
                //}
                return false;
            }
        }
        #endregion

        #region Get Cor Author Details
        public static CorAuthorDetaill GetCorAuthorDetails(string _JID, string _AID, string _DOI)
        {
            int _InAID;
            //bool ChkAID = int.TryParse(_AID, out _InAID);
            //if (ChkAID == true)
             //   _AID = _InAID.ToString();

            CorAuthorDetaill CorAT = new CorAuthorDetaill();
            using (var context = DataObjectFactory.CreateContext())
            {
                //IEnumerable<CorAuthorDetaill> productNames =context.CorAuthorDetaills.Select(p => p.); 
                CorAuthorDetaill Cor = new CorAuthorDetaill();// context.CorAuthorDetaills x.); //Rohit Where(x => x.JID.Equals(_JID) && x.AID.Equals(_AID) && x.DOI.Equals(_DOI)).FirstOrDefault();
                CorAT = Cor;
            }
            return CorAT;
        }
        #endregion
    }
}
