using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ColumnSelectedData
/// </summary>
/// 
namespace LexisNexis
{
    public class SelectedData
    {
        public string ColumnName { get; set; }
        public IEnumerable<string> SelectedValue { get; set; }
    }
    public class SelectedDataCollection
    {
        public IEnumerable<SelectedData> DataCollection { get; set; }
    }
}