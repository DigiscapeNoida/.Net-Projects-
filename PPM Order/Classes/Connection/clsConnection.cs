using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace PPM_TRACKING_SYSTEM.Classes.Connection
{
    class clsConnection
    {

        private static SqlConnection objSqlCon = null;
        private clsConnection()
        {
            //Prevents from Initialization of the Class.
            //Duplicate Connection Objects cannot be created.
        }
        public static SqlConnection getSQLConnection()
        {
            //objSqlCon = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=PPMTrackingSystem;Data Source=TD-KC5");
            //objSqlCon = new SqlConnection(@"Initial Catalog=PPMTrackingSystem;uid=sa;password=hi;Data Source=10.2.48.155");
            
            string constr = System.Configuration.ConfigurationSettings.AppSettings["ConnString"];
            objSqlCon = new SqlConnection(constr);
            SqlConnection.ClearAllPools();
            objSqlCon.Open();
            return objSqlCon;
        }
    }
}
