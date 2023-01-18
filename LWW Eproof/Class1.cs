using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LWWeProof
{
    class eProof
    {
        string[] _AIDs  = null;
        string   _Stage = string.Empty;

        public  eProof( string InputPath , string Stage)
        { 
            if (Directory.Exists(InputPath))
            {
                _AIDs = Directory.GetFiles(InputPath, "*.pdf"); 
            }
            _Stage = Stage;
        }
        public void StarteProof()
        {
            foreach (string AID in _AIDs)
            {
                //AIDInfo _A = GetHashCode("", "");
            }
        }

        private void d()
        { 

        }
    }


    class AIDInfo
    {
        public int SNO
        {
            get;set;
        }
        public string JID
        {
            get;set;
        }
        public string AID
        {
            get;set;
        }
        public string TaskName
        {
            get;set;
        }
        public string Stage
        {
            get;set;
        }
        public string GoXML
        {
            get;set;
        }
        public int PdfPages
        {
            get;
            set;
        }
    }
    
  
    class CreateZip
    {
        //Start to create Zip
        //Assign path
    }
    

    //class FtpProcess
    //{
    //    //Start to upload process
    //}


}
