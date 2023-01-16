using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace FMSIntegrateUsingEOOLink
{
    class ArticleInfo
    {

        //public event NotifyMsg ProcessNotification;
        //public event NotifyErrMsg ErrorNotification;


        string _MailBody = string.Empty;
        MailInfo _EOOMail = null;


        public MailInfo EOOMail
        {
            get { return _EOOMail; }
        }
        public ArticleInfo()
        {
        }
        public void ProcessArticleInfo(MailInfo EOOMail)
        {
            _EOOMail = EOOMail;
            _MailBody = _EOOMail.MailBody;


            AssgnDownloadLink();
            AssgnRefCode();
            AssgnFigure();
            if (!string.IsNullOrEmpty(RefCode))
            {
                RefCode = RefCode.Replace(".", "-");
                RefCode = RefCode.Trim(new char[] { ')', '(' });
            }
        }

        private void AssgnFigure()
        {
            if (!_EOOMail.ExportMailType) return;


            _EOOMail.MailBody = _EOOMail.MailBody.Replace(":\r\n", ": ");
            if (_EOOMail.MailBody.IndexOf("JSO-2013-0306.R1") != -1)
                //{ 
                //}
                //if (_EOOMail.MailSubject.IndexOf("JSO-2013-0306.R1") != -1)
                //{
                //}
                //Revised
                //Received
                //Accepted

                _EOOMail.MailBody = _EOOMail.MailBody.Replace("Revised", "Revised\n");
            _EOOMail.MailBody = _EOOMail.MailBody.Replace("Received", "Received\n");
            _EOOMail.MailBody = _EOOMail.MailBody.Replace("Accepted", "Accepted\n");

            _EOOMail.MailBody = _EOOMail.MailBody.Replace("revised", "Revised\n");
            _EOOMail.MailBody = _EOOMail.MailBody.Replace("received", "Received\n");
            _EOOMail.MailBody = _EOOMail.MailBody.Replace("accepted", "Accepted\n");




            _EOOMail.MailBody = _EOOMail.MailBody.Replace("Document ID:", "EDITORIAL REF CODE");
            _EOOMail.MailBody = _EOOMail.MailBody.Replace("Reference number:", "EDITORIAL REF CODE");
            _EOOMail.MailBody = _EOOMail.MailBody.Replace("Manuscript Number:", "EDITORIAL REF CODE");
            _EOOMail.MailBody = _EOOMail.MailBody.Replace("Manuscript Reference Number:", "EDITORIAL REF CODE");

            _EOOMail.MailBody = _EOOMail.MailBody.Replace("Editorial reference code", "EDITORIAL REF CODE");
            _EOOMail.MailBody = _EOOMail.MailBody.Replace("Ed Ref:", "EDITORIAL REF CODE");
            _EOOMail.MailBody = _EOOMail.MailBody.Replace("Manuscript id:", "EDITORIAL REF CODE");

            _EOOMail.MailBody = _EOOMail.MailBody.Replace("EDITORIAL REF CODE:\n", "EDITORIAL REF CODE:");

            string[] MailLines = _EOOMail.MailBody.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string MailLine in MailLines)
            {
                ProcessMailLine(MailLine);
            }

            if (string.IsNullOrEmpty(RefCode))
            {
                ProcessMessage("Reference Code not founf in mailBiody or subject " + _EOOMail.MailSubject);
            }
        }
        private void ProcessMailLine(string MailLine)
        {


            if (MailLine.IndexOf(" figure", StringComparison.OrdinalIgnoreCase) != -1 && MailLine.IndexOf("color", StringComparison.OrdinalIgnoreCase) == -1)
            {
                if (string.IsNullOrEmpty(Figs)) Figs = ProcessLastChar(MailLine).ToString();
            }
            if (MailLine.IndexOf("Color figures:", StringComparison.OrdinalIgnoreCase) != -1)
            {
                int ColorFig = ProcessLastChar(MailLine);
                if (!string.IsNullOrEmpty(Figs))
                {
                    int FigCount = Int32.Parse(Figs) + ColorFig;
                    Figs = FigCount.ToString();
                }
            }
            else if (MailLine.IndexOf("Number of manuscript pages:", StringComparison.OrdinalIgnoreCase) != -1)
            {
                ManuscriptPages = ProcessLastChar(MailLine).ToString();
            }
            else if (MailLine.IndexOf("Number of pages:", StringComparison.OrdinalIgnoreCase) != -1)
            {
                ManuscriptPages = ProcessLastChar(MailLine).ToString();
            }
            else if (MailLine.IndexOf("Manuscript pages:", StringComparison.OrdinalIgnoreCase) != -1)
            {
                ManuscriptPages = ProcessLastChar(MailLine).ToString();
            }
            else if (MailLine.IndexOf("Article type:", StringComparison.OrdinalIgnoreCase) != -1)
            {
                ManuscriptType = MailLine.Substring("Article type:".Length);
            }
            else if (MailLine.IndexOf("Manuscript type:", StringComparison.OrdinalIgnoreCase) != -1)
            {
                ManuscriptType = MailLine.Substring("Manuscript type:".Length);
            }
            else if (MailLine.IndexOf("received", StringComparison.OrdinalIgnoreCase) != -1)
            {
                if (string.IsNullOrEmpty(Received)) Received = GetDate(MailLine);
            }
            else if (MailLine.IndexOf("accepted", StringComparison.OrdinalIgnoreCase) != -1)
            {
                if (string.IsNullOrEmpty(Accepted)) Accepted = GetDate(MailLine);
            }
            else if (MailLine.IndexOf("Revised", StringComparison.OrdinalIgnoreCase) != -1)
            {
                if (string.IsNullOrEmpty(Revised)) Revised = GetDate(MailLine);
            }
            else if (MailLine.IndexOf("Manuscript id ", StringComparison.OrdinalIgnoreCase) != -1)
            {
                string TempRefCode = MailLine.Replace("Manuscript id ", "").Trim(new char[] { ':', ' ', '(', ')' });
                if (!string.IsNullOrEmpty(TempRefCode))
                {
                    RefCode = TempRefCode;
                    ProcessMessage("Reference Code In Mail Line:: " + RefCode);
                }
            }
            else if (MailLine.IndexOf("EDITORIAL REF CODE", StringComparison.OrdinalIgnoreCase) != -1)
            {
                string TempRefCode = MailLine.Replace("EDITORIAL REF CODE", "").Trim(new char[] { ':', ' ', '(', ')' });
                if (!string.IsNullOrEmpty(TempRefCode))
                {
                    RefCode = TempRefCode;
                    ProcessMessage("Reference Code In Mail Line:: " + RefCode);
                }
            }
            else if (MailLine.IndexOf("Manuscript Number", StringComparison.OrdinalIgnoreCase) != -1)
            {
                string TempRefCode = MailLine.Replace("Manuscript Number", "").Trim(new char[] { ':', ' ', '(', ')' });
                if (!string.IsNullOrEmpty(TempRefCode))
                {
                    RefCode = TempRefCode;
                    ProcessMessage("Reference Code In Mail Line:: " + RefCode);
                }

            }


        }
        private string GetDate(string MailLine)
        {
            string DateStr = "";
            MatchCollection MC = Regex.Matches(MailLine, "[0-9]{2}-[a-zA-Z]{3}-[0-9]{4}");
            if (MC.Count > 0)
            {
                DateStr = MC[MC.Count - 1].Value;
                ProcessMessage("Date :: " + DateStr);
            }
            else
            {
                ProcessMessage("Date not found........");
            }

            return DateStr;
        }
        private int ProcessLastChar(string MailLine)
        {
            int IntNo;
            int LastSpcPos = MailLine.LastIndexOf(' ');
            if (LastSpcPos != -1)
            {
                //string LstChar = MailLine.Substring(LastSpcPos);
                string LstChar = Regex.Match(MailLine, "[0-9]+").Value;

                if (!string.IsNullOrEmpty(LstChar))
                {
                    Int32.TryParse(LstChar, out IntNo);
                }
                else
                {
                    IntNo = 0;
                }
            }
            else
            {
                IntNo = 0;
            }

            return IntNo;
        }

        private void AssgnDownloadLink()
        {
            if (_EOOMail.ImportMailType)
            {
                //string Ptrn = "([A-Z.]{1,})([0-9]{1,})(.ART)";
                string Ptrn = "( )([^ ]{1,})(.ART)";
                if (!Regex.Match(_EOOMail.MailBody, Ptrn).Value.Equals(""))
                {
                    DownloadFileName = Regex.Match(_EOOMail.MailBody, Ptrn).Value;
                    if (!string.IsNullOrEmpty(DownloadFileName))
                    {
                        DownloadFileName = DownloadFileName.Trim();
                        string[] JIDAID = DownloadFileName.Trim().Replace("AJMG.", "AJMG").Split('.');
                        if (JIDAID.Length > 1)
                        {
                            JID = JIDAID[0];
                            AID = JIDAID[1];
                        }
                    }
                }
                else
                {

                }

                if (JID.Equals("MPO"))
                    JID = "PBC";
                if (JID.StartsWith("AJMG"))
                    JID = JID.Replace("AJMG.", "AJMG");
                ProcessMessage("JID :: " + JID);
                ProcessMessage("AID :: " + AID);

            }
        }
        private void AssgnRefCode()
        {
            _EOOMail.MailSubject = _EOOMail.MailSubject.Replace(".", "-");
            _EOOMail.MailSubject = _EOOMail.MailSubject + " ";
            //
            string[] Patrn = new string[6];
            Patrn[0] = @"[^ ]+\-[^ ]+ ";
            Patrn[1] = @"[\(A-Z\-a-z]{2,}[0-9-]{3,}[\-a-zA-Z\.0-9\)]{1,}";
            Patrn[2] = @"[A-Z-]{2,}[0-9R\.\-]{3,}";
            Patrn[3] = @"[\(A-Z\-a-z]{2,}[0-9-]{3,}";
            Patrn[4] = @"[0-9\-R\.]{3,}";
            Patrn[5] = @"[0-9R\.\-]{3,}";

            for (int i = 0; i < Patrn.Length; i++)
            {
                if (!Regex.Match(_EOOMail.MailSubject, Patrn[i]).Value.Equals(""))
                {
                    RefCode = Regex.Match(_EOOMail.MailSubject, Patrn[i]).Value;
                    RefCode = RefCode.Trim(new char[] { '(', ')' });
                    ProcessMessage("Reference Code In Subject:: " + RefCode);
                    break;
                }
                else
                {
                }
            }
            if (string.IsNullOrEmpty(RefCode))
            {
                //for (int i = 0; i < Patrn.Length; i++)
                //{
                //    if (!Regex.Match(_EOOMail.MailBody, Patrn[i]).Value.Equals(""))
                //    {
                //        RefCode = Regex.Match(_EOOMail.MailBody, Patrn[i]).Value;

                //        if (RefCode.StartsWith("-"))
                //        {
                //        }
                //        else
                //        {
                //            RefCode = RefCode.Trim(new char[] { '(', ')' });
                //            Console.WriteLine(RefCode);
                //            ProcessMessage("Reference Code In MailBody:: " + RefCode);
                //            break;
                //        }

                //    }
                //}
            }
            if (!string.IsNullOrEmpty(RefCode))
            {
                RefCode = RefCode.Trim(new char[] { ' ', '(', ')' });
            }

        }

        private void AssignValue()
        {
            //([A-Z.]{1,})([0-9]{1,})(.ART)
        }

        public string DownloadFileName
        {
            get;
            set;
        }

        public string ImportMailNoteID
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
        public string DOI
        {
            get;
            set;
        }
        public string RefCode
        {
            get;
            set;
        }
        public string Figs
        {
            get;
            set;
        }
        public string ManuscriptPages
        {
            get;
            set;
        }
        public string Received
        {
            get;
            set;
        }
        public string Revised
        {
            get;
            set;
        }
        public string Accepted
        {
            get;
            set;
        }

        public string OrderFile
        {
            get;
            set;
        }
        public string ZipFile
        {
            get;
            set;
        }

        public string ManuscriptType
        {
            get;
            set;
        }
        public string ArticleCategory
        {
            get;
            set;
        }

        public bool isTexFileExist
        { set; get; }

        public string ClientOrgFileName
        {
            get;
            set;
        }



        private void ProcessMessage(string Msg)
        {
            //if (ProcessNotification != null)
            //{
            //    ProcessNotification(Msg);
            //}
        }

        private void ErrorMessage(Exception Ex)
        {
            //if (ErrorNotification != null)
            //{
            //    ErrorNotification(Ex);
            //}
        }

    }
}
