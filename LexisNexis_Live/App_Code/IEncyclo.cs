using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEncyclo" in both code and config file together.
namespace LexisNexis
{
    [ServiceContract]
    public interface IEncyclo
    {
        [OperationContract]
        [WebGet(UriTemplate = "/data?ColumnName={strColumnName}", ResponseFormat = WebMessageFormat.Json)]
        FilterValueSet[] GetDistinctValue(string strColumnName);
    }
}
