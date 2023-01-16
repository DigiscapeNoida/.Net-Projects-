using System;
using System.Collections.Generic;
using ProcessNotification;
using System.Text;

namespace AutoEproof
{
    public interface IRValidation
    {
       
        bool IsValidJID
        {
            get;
            set;
        }
        bool IsAuthorNameExist
        {
            get;
            set;

        }
        bool IsAuthorEmailExist
        {
            get;
            set;
        }
        bool IsAlreadyProcessed
        {
            get;
            set;
        }
        bool IsMatchCorEmailXMLANDDB
        {
            get;
            set;
        }
        bool ValidationResult
        {
            get;
            set;
        }
        bool IsAuthorEMailWellForm
        {
            get;
            set;
        }
     
        bool IsPdfProcessError
        {
            get;
            set;
        }
        int PdfPages
        {
            get;
            set;
        }
        int AutoPdfPages
        {
            get;
            set;
        }
        bool IsPdfPagesEqualAutopage
        {
            get;
            set;
        }     

        string  Remark
        {
            get;
            set;
        }
        string PDFPath
        {
            get;
        }
        string XMLPath
        {
            get;
        }
        bool IsMatchDOI
        {
            get;
            set;
        }
        bool IsQueryIDExist
        {
            get;
            set;
        }
        bool IscPDFExist
        {
            get;
            set;
        }
        void StartValidation();
    }
    public interface IValidation:IRValidation
    {
        bool IsCDCArticle
        {
            get;
            set;
        }
        bool IsArticleTitleExist
        {
            get;
            set;
        }
        bool IsACRJID
        {
            get;
            set;
        }
    }
}
