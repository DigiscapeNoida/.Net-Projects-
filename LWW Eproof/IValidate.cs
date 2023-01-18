using System;
using System.Collections.Generic;
using ProcessNotification;
using System.Text;

namespace LWWeProof
{

    public interface IAuthorEMailValidation
    {
        bool IsAuthorEmailExist
        {
            get;
            set;
        }
        bool IsAuthorEMailWellForm
        {
            get;
            set;
        }
        bool IsAuthorNameExist
        {
            get;
            set;
        }
    }
    public interface IRValidation:IAuthorEMailValidation
    {
        bool ValidationResult
        {
            get;
            set;
        }
        bool IsAlreadyProcessed
        {
            get;
            set;
        }
        string PDFPath
        {
            get;
        }
        bool IsValidJID
        {
            get;
            set;
        }
       
    }
     public interface IProcessValidation
     {
        bool IsValidJID
        {
            get;
            set;
        }
        bool IsValidAID
        {
            get;
            set;

        }
        bool IsAlreadyProcessed
        {
            get;
            set;
        }
        bool ValidationResult
        {
            get;
            set;
        }
        bool IsPdfProcessError
        {
            get;
            set;
        }
        string Stage
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
        void StartValidation();
    }
    public interface IValidation:IProcessValidation
    {
        bool IsValidTakName
        {
            get;
            set;
        }
        bool IsValidGOXML
        {
            get;
            set;
        }
        bool IsValidStage
        {
            get;
            set;
        } 
        
    }
}
