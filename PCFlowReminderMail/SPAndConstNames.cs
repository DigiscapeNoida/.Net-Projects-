using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;


/// <summary>
/// Summary description for SPAndConstNames
/// </summary>


public static class SPNames
{
    public const string AuInfo = "usp_GetRemainderAuthor2";
    public const string EdInfo = "GetRemainderEditor";              
}
public class MailInfo
{   
    public MailInfo()
    {     
    }  
    public string PII
    {
        get;
        set;
    }
    public string WasSendOn
    {
        get;
        set;
    }
    public string DueDate
    {
        get;
        set;
    }       
    public string PMName
    {
        get;
        set;
    }
    public string BookTitle
    {
        get;
        set;
    }
    public string ToMail
    {
        get;
        set;
    }
    public string CCMail
    {
        get;
        set;
    }
    public string BCCMail
    {
        get;
        set;
    }
    public string ChapterNo
    {
        get;
        set;
    }
    public string Subject
    {
        get;
        set;
    }
    public string Link
    {
        get;
        set;
    }
    public string FromSender
    {
        set;
        get;
    }
}

                                    