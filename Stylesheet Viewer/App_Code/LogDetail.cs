using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for LogDetail
/// </summary>
public class LogDetail
{
    public static int SeqCount=0;   ///////Sequence Counter

    private int    _SEQNO;
    private string _FileName;
    private string _Result;
    private string _FMSResult;

    public LogDetail(string FileName, string Result, string FMSResult)
    {
        this._FileName = FileName;
        this._Result   = Result;
                         SeqCount++;
           this._SEQNO = SeqCount;
       this._FMSResult = FMSResult;
    }
    public string FileName
    {
        get
        {
            return _FileName;
        }
    }
    public string FMSResult
    {
        get
        {
            return _FMSResult;
        }
    }
    public string Result
    {
        get
        {
            return _Result;
        }
    }
    public int    SeqNo
    {
        get
        {
            return _SEQNO;
        }
        
    }
}
