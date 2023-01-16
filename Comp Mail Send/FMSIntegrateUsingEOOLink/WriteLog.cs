using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;
using System.Web;

/// <summary>
/// Summary description for MakeLog
/// </summary>
public class WriteLog
{
    string _LogFile = string.Empty;
    string _IPLogPath = "";
    string _DateLogPath = "";
    StringBuilder _LogStr = new StringBuilder("");

    public string AppPath { set; get; }
    public string ClientIP { set; get; }

    public enum LOGTPE { IP, DATE };

    public string IPLogPath
    {
        get { _IPLogPath = GetIPLogPath(); return _IPLogPath; }
    }

    public string DateLogPath
    {
        get { _DateLogPath = GetDateLogPath(); return _DateLogPath; }
    }

    public WriteLog(string ApplicationPath)
    {
        //if (LogTypeEnum == LOGTPE.DATE)
        //    _IPLogPath = GetIPLogPath();
        //else if (LogTypeEnum == LOGTPE.DATE)
        //    _DateLogPath = GetDateLogPath();

        _LogFile = ApplicationPath + @"\LogFile\" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".log";
        AppPath = ApplicationPath;
        //
        // TODO: Add constructor logic here
        //
    }
    private string GetDateLogPath()
    {
        //////////////************Start Create log file path 
        StringBuilder LogFilePath = new StringBuilder("");
        string LogDirPath = "";
        LogDirPath = AppPath + @"\LogFile\" + DateTime.Now.AddDays(-7).ToShortDateString().Replace("/", "-");

        if (Directory.Exists(LogDirPath))
        {
            try
            {
                Directory.Delete(LogDirPath, true);
            }
            catch { }
        }

        LogDirPath = AppPath + @"\LogFile\" + DateTime.Now.ToShortDateString().Replace("/", "-");

        if (!Directory.Exists(LogDirPath))
        {
            Directory.CreateDirectory(LogDirPath);
        }

        LogFilePath = new StringBuilder(String.Format(DateTime.Now.ToString(), "{0:dd/MM/yyyy}") + ".log");
        LogFilePath.Replace('/', '-');
        LogFilePath.Replace(':', '_').Replace(" ", "_");
        LogFilePath = new StringBuilder(LogDirPath + @"\" + LogFilePath);

        //////////////************End Create log file path 
        return LogFilePath.ToString();
    }

    private string GetIPLogPath()
    {
        //////////////************Start Create log file path 
        StringBuilder LogFilePath = new StringBuilder("");
        string LogDirPath = "";
        LogDirPath = AppPath + @"\LogFile\" + DateTime.Now.AddDays(-7).ToShortDateString().Replace("/", "-");

        if (Directory.Exists(LogDirPath))
        {
            try
            {
                Directory.Delete(LogDirPath, true);
            }
            catch { }
        }

        LogDirPath = AppPath + @"\LogFile\" + DateTime.Now.ToShortDateString().Replace("/", "-");
        if (!ClientIP.Equals(""))
            LogDirPath = LogDirPath + "\\" + ClientIP;

        if (!Directory.Exists(LogDirPath))
        {
            Directory.CreateDirectory(LogDirPath);
        }
        LogFilePath = new StringBuilder(String.Format(DateTime.Now.ToString(), "{0:dd/MM/yyyy}") + ".log");
        LogFilePath.Replace('/', '-');
        LogFilePath.Replace(':', '_').Replace(" ", "_");
        LogFilePath = new StringBuilder(LogDirPath + @"\" + LogFilePath);
        //////////////************End Create log file path 
        return LogFilePath.ToString();
    }

    public void AppendLog(string LogText)
    {
        File.AppendAllText(_LogFile, LogText + Environment.NewLine);
        _LogStr.AppendLine(LogText);
    }

    public void AppendLog(object SrcOBJ)
    {
        Type OBJ = SrcOBJ.GetType();
        ///////////////***********Writing Log*********************************\\\\\\\\\\\\\\\\\
        PropertyInfo[] Properties = OBJ.GetProperties();
        foreach (PropertyInfo P in Properties)
        {
            _LogStr.AppendLine(P.Name + "\t" + P.GetValue(SrcOBJ, null));
            File.AppendAllText(_LogFile, P.Name + "\t" + P.GetValue(SrcOBJ, null) + Environment.NewLine);
        }
        ///////////////********************************************\\\\\\\\\\\\\\\\\
    }
    public bool WriteLogFileInIP()
    {
        if (_IPLogPath.Equals(""))
            _IPLogPath = GetIPLogPath();

        File.WriteAllText(_IPLogPath, _LogStr.ToString());
        return true;
    }
    public bool WriteLogFileInDate()
    {
        if (_DateLogPath.Equals(""))
            _DateLogPath = GetDateLogPath();

        File.WriteAllText(_DateLogPath, _LogStr.ToString());
        return true;
    }
}
