using System;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessNotification;
using System.Configuration;

namespace LWWeProof
{
    class AIDInfo
    {
        public int SNO
        {
            get;
            set;
        }
        public string JID
        {
            get;
            set;
        }
        public string AID
        {
            get;
            set;
        }
        public string TaskName
        {
            get;
            set;
        }
        public string Stage
        {
            get;
            set;
        }
        public string GoXML
        {
            get;
            set;
        }
        public string MetaDataXML
        {
            get;
            set;
        }
        public string SubmissionXML
        {
            get;
            set;
        }
        public int PdfPages
        {
            get;
            set;
        }
    }
    
  
    class InputFiles
    {

        public string XMLPath
        {
            get;
            set;
        }

        public string PDFPath
        {
            get;
            set;
        }

        public string Stage
        {
            get;
            set;
        }
        public InputFiles()
        { 

        }

        public InputFiles(string PDFFile ,string XMLFile, string FMSStage)
        {
            XMLPath = XMLFile;
            PDFPath = PDFFile;
            Stage   = FMSStage;
        }
    }

    class Validation : MessageEventArgs,  IValidation
    {
        DBProcess _DBObj   = null;
        AIDInfo   _AIDObj  = null;
        IGoXml    _GoXMLObj;

        public Validation(InputFiles InputFile)
        {
            Stage = InputFile.Stage;
            PDFPath = InputFile.PDFPath;
        }

        public DBProcess DBObj
        {
            get { return _DBObj; }
        }
        public AIDInfo AIDObj
        {
            get { return _AIDObj; }
        }
        public IGoXml GoXMLObj
        {
            get { return _GoXMLObj; }
        }

        private void IntializeProcess()
        {
             DBProcess.DBConStr = ConfigDetails.LWWConStr;

            ProcessEventHandler("Getting DBProcess object");

            _DBObj = DBProcess.DBProcessObj;

            

            _AIDObj = new AIDInfo();

            ProcessEventHandler("Getting PdfPages");
            _AIDObj.PdfPages = PdfProcess.PDF.GetPdfPageCount(PDFPath);

            string AID       = Path.GetFileNameWithoutExtension(PDFPath);
            ProcessEventHandler("AID :" +AID);
            
            ProcessEventHandler("Call usp_GetAIDDetailsResult");

            var AIDDtl       = _DBObj.usp_GetAIDDetailsResult(AID, Stage);

            foreach (usp_GetAIDDetailsResult a in AIDDtl)
            {
                _AIDObj.SNO = a.SNO;
                _AIDObj.JID = a.JID.Trim();
                _AIDObj.AID = a.AID.Trim();
                _AIDObj.TaskName = a.TASKNAME.Trim();
                _AIDObj.Stage = a.STAGE;
                _AIDObj.GoXML = a.GOXML;
                _AIDObj.MetaDataXML = a.METADATAXML;
                _AIDObj.SubmissionXML = a.SubmissionXML;
            }
            if (!string.IsNullOrEmpty(_AIDObj.GoXML))
            {
                ProcessEventHandler("IntializeProcess");
                GoXML GoXMLObj = new GoXML(_AIDObj.GoXML);
                GoXMLObj.ProcessNotification +=ProcessEventHandler;
                GoXMLObj.ErrorNotification   +=ProcessErrorHandler;

                GoXMLObj.ProcessGoXml();
               _GoXMLObj = GoXMLObj;
            }
            else
            {
                ProcessEventHandler("_AIDObj.GoXML is empty");
            }
        }
        public void StartValidation()
        {
            ProcessEventHandler("IntializeProcess");
            IntializeProcess();
            


            IsAlreadyProcessed = DBObj.isDatasetUpload(AIDObj.JID, AIDObj.AID, AIDObj.Stage, AIDObj.TaskName);

            //Rohit
            string jidlist = ConfigurationManager.AppSettings["JIDFORAPCTASKCLOSE"].ToString();
            if (jidlist.Contains(";" + AIDObj.JID + ";"))
            {
                if (AIDObj.TaskName == "Apply Proof Corrections")
                {
                    IsAlreadyProcessed = true;
                }
            }
            string jidlist1 = ConfigurationManager.AppSettings["JIDFORTASKCLOSE"].ToString();
            if (jidlist1.Contains(";" + AIDObj.JID + ";"))
            {
                if (AIDObj.TaskName == "Revise Article Proofs")
                {
                    IsAlreadyProcessed = false;
                }
            }

            ProcessEventHandler("ValidateGoXML");
            ValidateGoXML();

            ProcessEventHandler("ValidateTaskName");
            ValidateTaskName();

            ProcessEventHandler("AssignValidationResult");
            AssignValidationResult();
        }

        private void AssignValidationResult()
        {
            ValidationResult = true;
            IValidation Val = this;
            Type OBJ = Val.GetType();
            PropertyInfo[] Properties = OBJ.GetProperties();
            foreach (PropertyInfo propertyInfo in Properties)
            {
                if (propertyInfo.CanRead)
                {
                    if (propertyInfo.PropertyType == typeof(bool))
                    {
                        bool value = (bool)propertyInfo.GetValue(Val, null);



                        ProcessEventHandler(propertyInfo.Name +" :: " +  (value ? "True" : "false"));
                        

                        if (propertyInfo.Name.Equals("IsAlreadyProcessed"))
                        {
                            if (value == true)
                                ValidationResult = false;
                        }
                        else if (propertyInfo.Name.Equals("IsPdfProcessError"))
                        {
                            if (value == true)
                                ValidationResult = false;
                        }
                        else if (value == false)
                        {
                            ValidationResult = false;
                        }
                    }
                }
            }
        }
        private void ValidateTaskName()
        {
            string[] TaskNames=  ConfigDetails.S100TaskList;
            if (Stage.Equals("S200"))
               TaskNames=  ConfigDetails.S200TaskList;

            foreach (string TaskName in TaskNames)
            {
                if (!string.IsNullOrEmpty(AIDObj.TaskName))
                {
                    string str = AIDObj.TaskName;
                    if (AIDObj.TaskName.StartsWith(TaskName, StringComparison.OrdinalIgnoreCase))
                    {
                        IsValidTakName = true;
                        break;
                    }
                }
            }
        }
        private void ValidateGoXML()
        {
            if (!string.IsNullOrEmpty(AIDObj.GoXML))
            {

                ProcessEventHandler("_AIDObj.JID :: " + _AIDObj.JID);
                ProcessEventHandler("_AIDObj.AID :: " + _AIDObj.AID);

                ProcessEventHandler("_GoXMLObj.JID :: " + _GoXMLObj.JID);
                ProcessEventHandler("_GoXMLObj.AID :: " + _GoXMLObj.AID);

                if (AIDObj.JID.Equals(_GoXMLObj.JID, StringComparison.OrdinalIgnoreCase) && AIDObj.AID.Equals(_GoXMLObj.AID))
                {
                    IsValidGOXML = true;
                    IsValidJID = true;
                    IsValidAID = true;
                    IsValidStage = true;
                }
                else
                {
                    ProcessEventHandler("JID and AID mismatch");
                }
            }
            else
            {
                ProcessEventHandler("Blanl GoXML for " + _AIDObj.AID);
            }
        }

        public bool IsAlreadyProcessed
        {
            get;set;
        }

        public bool ValidationResult
        {
            get;
            set;
        }

        public bool IsPdfProcessError
        {
            get;
            set;
        }

        public string Remark
        {
            get;
            set;
        }

        public string PDFPath
        {
            get;
            set;
        }

        public bool IsValidTakName
        {
            get;
            set;
           
        }

        public bool IsValidGOXML
        {
            get;
            set;
           
        }

        public bool IsValidStage
        {
            get;
            set;
           
        }

        public bool IsValidJID
        {
            get;
            set;
           
        }

        public bool IsValidAID
        {
            get;
            set;
        }
        public string Stage
        {
            get;
            set;
        }
    }
}
