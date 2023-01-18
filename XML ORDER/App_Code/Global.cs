using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Compression;
/// <summary>
/// Summary description for Global
/// </summary>
namespace Orders
{
    public class Global
    {
        static string validateerror, validateDTDerror, directoryPath;
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

        public static string GetDocFileName(string ZipPath)
        {
            string _DocFile = string.Empty;
            string ExeLocationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string ExtractTo = ZipPath.Replace(".zip", "");

            if (Directory.Exists(ExtractTo))
                Directory.Delete(ExtractTo, true);

            ZipFile.ExtractToDirectory(ZipPath, ExtractTo);

            string[] DocFiles = Directory.GetFiles(ExtractTo, "*R*.doc*", SearchOption.AllDirectories);
            foreach (string Doc in DocFiles)
            {
                if (!string.IsNullOrEmpty(Regex.Match(Path.GetFileName(Doc).ToUpper(), "R[0-9]+.DOC").Value))
                {
                    _DocFile = Path.GetFileNameWithoutExtension( Doc);
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(_DocFile))
            {
                DocFiles = Directory.GetFiles(ExtractTo, "*.doc", SearchOption.AllDirectories);
                foreach (string Doc in DocFiles)
                {
                    if (!string.IsNullOrEmpty(Regex.Match(Path.GetFileName(Doc).ToUpper(), "_[0-9]+.DOC").Value))
                    {
                        _DocFile = Path.GetFileNameWithoutExtension(Doc); ;
                        break;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(_DocFile))
            { 
                _DocFile=GetPdfFileName(ExtractTo);
            }
            return _DocFile;
        }

        private static string GetPdfFileName(string ExtractTo)
        {
            string _PdfFile = string.Empty;

            string[] PdfFiles = Directory.GetFiles(ExtractTo, "*R*.pdf*", SearchOption.AllDirectories);
            foreach (string Doc in PdfFiles)
            {
                if (!string.IsNullOrEmpty(Regex.Match(Path.GetFileName(Doc).ToUpper(), "R[0-9]+.PDF").Value))
                {
                    _PdfFile = Path.GetFileNameWithoutExtension(Doc);
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(_PdfFile))
            {
                PdfFiles = Directory.GetFiles(ExtractTo, "*.pdf", SearchOption.AllDirectories);
                foreach (string Doc in PdfFiles)
                {
                    if (!string.IsNullOrEmpty(Regex.Match(Path.GetFileName(Doc).ToUpper(), "_[0-9]+.PDF").Value))
                    {
                        _PdfFile = Path.GetFileNameWithoutExtension(Doc); ;
                        break;
                    }
                }
            }
            return _PdfFile;
        }
    }
}