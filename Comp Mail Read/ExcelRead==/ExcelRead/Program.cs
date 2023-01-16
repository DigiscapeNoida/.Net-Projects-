using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelRead
{
    class Program
    {
        static void Main(string[] args)
        {

            ArticleAontentReport.ProcessACRExcel ACRExcelOBJ = new ArticleAontentReport.ProcessACRExcel(@"D:\JWUSA\ArticleContentReport");
            ACRExcelOBJ.StartProcess();
        }
    }

}
