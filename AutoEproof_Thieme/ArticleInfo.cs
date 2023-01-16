using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoEproof
{
    public class ArticleInfo:AuthorInfo
    {
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
        public string CorEmail
        {
            get;
            set;
        }
        public string CorEmailCC
        {
            get;
            set;
        }
        public string PEEmail
        {
            get;
            set;
        }
        public string PEName
        {
            get;
            set;
        }
        public string JournalTitle
        {
            get;
            set;
        }
        public string ArticleTitle
        {
            get;
            set;
        }
        public string Authors
        {
            get;
            set;
        }
        public string DOI
        {
            get;
            set;
        }
        public string ContactData
        {
            get;
            set;
        }
        public string Publisher
        {
            get;
            set;
        }
        public bool IsQuerypage
        {
            get;
            set;
        }
        public bool IsgraphicalAbs
        {
            get;
            set;
        }
        public string AJIMEditor
        {
            get;
            set;
        }
    }
}
