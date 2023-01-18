#pragma checksum "D:\WinCVS\ThimeXMLORDER\App_Code\Global.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "36F835E5ECCA0042B787ED939EA88950C4FE762C"

#line 1 "D:\WinCVS\ThimeXMLORDER\App_Code\Global.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Global
/// </summary>
public class Global
{
    static string validateerror, validateDTDerror, directoryPath ;
    public static string DirectoryPath
    {
        get
        {
            return directoryPath;

        }
        set
        {
            directoryPath = value;
        }
    }
    public static string ValidateError
    {
        get
        {
            return validateerror;

        }
        set
        {
            validateerror = value;
        }
    }
    public static string ValidateDTDError
    {
        get
        {
            return validateDTDerror;

        }
        set
        {
            validateDTDerror = value;
        }
    }  
}

#line default
#line hidden
