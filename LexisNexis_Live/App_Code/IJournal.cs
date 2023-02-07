using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IJournal" in both code and config file together.
namespace LexisNexis
{
    [ServiceContract]
    public interface IJournal
    {
        [OperationContract]
        [WebInvoke(
    Method = "GET",
    BodyStyle = WebMessageBodyStyle.WrappedRequest,
    ResponseFormat = WebMessageFormat.Json,
    RequestFormat = WebMessageFormat.Json,
    UriTemplate = "/data?ColumnName={strColumnName}"
    )]
        //[WebGet(UriTemplate = "/data?ColumnName={strColumnName}", ResponseFormat = WebMessageFormat.Json)]
        FilterValueSet[] GetDistinctValue(string strColumnName);
    }
}
