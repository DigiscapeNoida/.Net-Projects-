using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMSIntegrateUsingEOOLink
{
    class eFirstProcess
    {
        string _Stage   = string.Empty;
        string _DOI     = string.Empty;                                                   
        string _XMLFile = string.Empty;
        string _PDFFile = string.Empty;

         
        
        const string _FIZPath    = @"\\wip\Thieme_3B2\For_Final_Thieme\online\FIZ2012-13\Uploaded\FIZ201";
        const string _eFirstPath = @"\\wip\Thieme_3B2\For_Final_Thieme\online\efirst2014";

        public eFirstProcess(string DOI, string Stage)
        {
            _DOI = DOI;
            _Stage = Stage;
        }

        private void GetXMLPDFFiles()
        {
            string SrchPath = string.Empty;
            if (_Stage == "Fiz")
                SrchPath = _FIZPath;
            else if (_Stage == "Fiz")
                SrchPath = _eFirstPath;

            string[] XMLFiles = Directory.GetFiles(SrchPath,   _DOI + "*.xml", SearchOption.AllDirectories);
            string[] PDFFiles    = Directory.GetFiles(SrchPath, _DOI + "*.pdf", SearchOption.AllDirectories);

            if (XMLFiles.Length == 1)
            {
                _XMLFile = XMLFiles[0];
            }

            if (PDFFiles.Length == 1)
            {
                _PDFFile = PDFFiles[0];
            }
        }

    }
}
