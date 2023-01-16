using System;
using ProcessNotification;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoEproof
{
    class AIDInfo : MessageEventArgs
    {
        string _Client = string.Empty;
        string _JID = string.Empty;
        string _AID = string.Empty;

        public AIDInfo(string Client, string JID, string AID)
        {
            _Client = Client;
            _JID = JID;
            _AID = AID;
        }
        public string Client
        {
            get { return _Client; }
            set { _Client = value; }
        }
        public string JID
        {
            get { return _JID; }
            set { _JID = value; }
        }
        public string AID
        {
            get { return _AID; }
            set { _AID = value; }
        }

    }
    class FMSStructure : AIDInfo
    {
        readonly string FMSFolder = string.Empty;
        protected string MNTFolder = string.Empty;

        protected string S280dataset = string.Empty;
        protected string AIDFolder = string.Empty;
        protected string Output = string.Empty;
        protected string Art = string.Empty;
        protected string Text = string.Empty;
        protected string WorkArea = string.Empty;
        protected string MNTWorkArea = string.Empty;
        protected string WorkAreaGraphics = string.Empty;


        public string OutFolder
        {
            get { return Output; }
        }
        public FMSStructure(string Client, string JID, string AID, string FMSFolder)
            : base(Client, JID, AID )
        {
            this.FMSFolder = FMSFolder;
            AssignPath();
        }
        private void AssignPath()
        {

            MNTFolder = "MNT_" + Client + "_JOURNAL_" + JID + "_" + AID + "_110";
            AIDFolder = FMSFolder + "\\" + Client + "\\JOURNAL\\" + JID + "\\" + AID;

            S280dataset = AIDFolder + "\\S280dataset";
            Art = AIDFolder + "\\Art";
            Output = AIDFolder + "\\Output";
            Text = AIDFolder + "\\Text";
            WorkArea = AIDFolder + "\\WorkArea";
            MNTWorkArea = AIDFolder + "\\WorkArea\\" + MNTFolder;

            ProcessEventHandler(MNTFolder);
            ProcessEventHandler(AIDFolder);
            ProcessEventHandler(Art);
            ProcessEventHandler(Output);
            ProcessEventHandler(Text);
            ProcessEventHandler(WorkArea);
            ProcessEventHandler(MNTWorkArea);
        }
    }
    class GetPDFXML : FMSStructure
    {
          string IssuePath = string.Empty;
          string TempFolder = string.Empty;
          public GetPDFXML(string Client, string JID, string AID, string FMSFolder , string TempFolder)
              : base(Client, JID, AID,FMSFolder)
          {
              this.TempFolder = TempFolder;      
          }
          public void StartProcess()
          {
              string CopyFromMNTFolder = GetMNTFolderPath(false);
              if (!string.IsNullOrEmpty(CopyFromMNTFolder))
              {
                  MoveToFMS MoveToFMSObj = new MoveToFMS(CopyFromMNTFolder);
                  MoveToFMSObj.ProcessNotification += ProcessEventHandler;
                  MoveToFMSObj.ErrorNotification += ProcessErrorHandler;

                  MoveToFMSObj.CreateMNTFolder();
              }

          }
          public bool StartProcessToGetS280Files(string CopyTo)
          {
              bool Rslt = false;
              ProcessEventHandler("Call GetMNTFolderPath");
              string CopyFromMNTFolder = TempFolder + "\\" + MNTFolder;

              if (!Directory.Exists(CopyFromMNTFolder))
                  CopyFromMNTFolder = GetMNTFolderPath(false);

              if (!string.IsNullOrEmpty(CopyFromMNTFolder))
              {
                  FilesFromTDXPS FilesFromTDXPSObj = new AutoEproof.FilesFromTDXPS(CopyFromMNTFolder,this);

                  FilesFromTDXPSObj.ProcessNotification += ProcessEventHandler;
                  FilesFromTDXPSObj.ErrorNotification += ProcessErrorHandler;

                  if (FilesFromTDXPSObj.GetFilesForS280(CopyTo))
                  {
                      Rslt = true;
                  }
              }
              else
              {
                  Rslt = true;
              }

              return Rslt;
          }
          public bool StartProcessToGetRvsFiles(string CopyTo)
          {
              bool Rslt = false;
              ProcessEventHandler("Call GetMNTFolderPath");
              string CopyFromMNTFolder = GetMNTFolderPath(true);

              if (!string.IsNullOrEmpty(CopyFromMNTFolder))
              {
                  FilesFromTDXPS FilesFromTDXPSObj = new AutoEproof.FilesFromTDXPS(CopyFromMNTFolder);

                  FilesFromTDXPSObj.ProcessNotification += ProcessEventHandler;
                  FilesFromTDXPSObj.ErrorNotification += ProcessErrorHandler;

                  if (FilesFromTDXPSObj.GetFilesForRvsPagination(CopyTo))
                  {
                      Rslt = true;
                  }
              }
              else
              {
                  Rslt = true;
              }

              return Rslt;
          }
          public bool StartProcessToGetIssFiles(string CopyTo)
          {
              bool Rslt = false;
              string CopyFromMNTFolder = GetMNTFolderPath(false);
              if (!string.IsNullOrEmpty(CopyFromMNTFolder))
              {
                  FilesFromTDXPS FilesFromTDXPSObj = new AutoEproof.FilesFromTDXPS(CopyFromMNTFolder);

                  FilesFromTDXPSObj.ProcessNotification += ProcessEventHandler;
                  FilesFromTDXPSObj.ErrorNotification += ProcessErrorHandler;

                  Rslt = FilesFromTDXPSObj.GetFilesForIssPagination(CopyTo);
              }
              else
              {
                  if (!string.IsNullOrEmpty(this.MNTWorkArea))
                  {
                      FilesFromTDXPS FilesFromTDXPSObj = new AutoEproof.FilesFromTDXPS(this.MNTWorkArea);
                      Rslt = FilesFromTDXPSObj.GetFilesForIssPagination(CopyTo);
                  }
                  Rslt = true;
              }
              return Rslt;
          }

          private string GetMNTFolderPath(bool isRvs)
          {
              string CopyFromMNTFolder = string.Empty;
              string MNTFolder = this.MNTFolder;
              string MNTFolderPath = this.MNTWorkArea;

              ProcessEventHandler("MNTFolderPath ::" + MNTFolderPath);

              string MNTInTemp = TempFolder + "\\" + MNTFolder;
              if (Directory.Exists(MNTFolderPath))
              {
                  string[] _3dFileInServer = Directory.GetFiles(MNTFolderPath, AID + ".3d");

                  if (isRvs)
                      _3dFileInServer = Directory.GetFiles(MNTFolderPath, "tx1.xml");

                  FileInfo _3dFileInTemp = null;
                  FileInfo _3dFileInMNT = null;

                  if (_3dFileInServer.Length == 1)
                  {
                      _3dFileInMNT = new FileInfo(_3dFileInServer[0]);
                  }



                  if (Directory.Exists(MNTInTemp))
                  {
                      string[] _3dFile = Directory.GetFiles(MNTInTemp, AID + ".3d");
                      if (isRvs)
                          _3dFile = Directory.GetFiles(MNTInTemp, "tx1.xml");

                      if (_3dFile.Length == 1)
                          _3dFileInTemp = new FileInfo(_3dFile[0]);
                  }

                  if (_3dFileInTemp != null)
                  {
                      if (_3dFileInMNT == null)
                          CopyFromMNTFolder = MNTInTemp;
                      else if (_3dFileInTemp.LastWriteTime > _3dFileInMNT.LastWriteTime)
                          CopyFromMNTFolder = MNTInTemp;
                      else
                          CopyFromMNTFolder = MNTFolderPath;
                  }
                  else
                  {
                      CopyFromMNTFolder = MNTFolderPath;
                  }
                  

              }
              else if (Directory.Exists(MNTInTemp))
              {
                  CopyFromMNTFolder = MNTInTemp;
              }

              else
              {

                  ProcessEventHandler(MNTFolderPath + " does not exist");
              }
              ProcessEventHandler("CopyFromMNTFolder : " + CopyFromMNTFolder);
              return CopyFromMNTFolder;
          }

         

    }
}
