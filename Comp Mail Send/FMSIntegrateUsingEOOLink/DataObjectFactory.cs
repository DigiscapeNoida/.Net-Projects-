using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace FMSIntegrateUsingEOOLink
{
    public static class DataObjectFactory
    {
        

        private static readonly string _connectionString;

        /// <summary>
        /// Static constructor. Reads the connectionstring from web.config just once.
        /// </summary>
        static DataObjectFactory()
        {
           // string connectionStringName = ConfigurationManager.AppSettings.Get("ConnectionStringName");
            _connectionString = ConfigurationManager.ConnectionStrings["OPSTestEntities"].ConnectionString;
        }

        /// <summary>
        /// Creates the Context using the current connectionstring.
        /// </summary>
        /// <remarks>
        /// Gof pattern: Factory method. 
        /// </remarks>
        /// <returns>Action Entities context.</returns>
        public static OPSEntities CreateContext()
        {
            return new OPSEntities(_connectionString);
        }
    }
}
