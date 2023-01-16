using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WileyIssueOrder
{
    class AIDInfo : IssueInfo
    {
        string _sPage = string.Empty;
        string _ePage = string.Empty;
        static string[] RomanStr = "Dummy#I#II#III#IV#V#VI#VII#VIII#IX#X#XI#XII#XIII#XIV#XV#XVI#XVII#XVIII#XIX#XX#XXI#XXII#XXIII#XXIV#XXV#XXVI#XXVII#XXVIII#XXIX#XXX#XXXI#XXXII#XXXIII#XXXIV#XXXV#XXXVI#XXXVII#XXXIX#XXXVIII#XL#XLI#XXIX#XLIII#LIV#XLV#XLVI#XLVII#XLVIII#XLIX#L#LI#LII#LIII#LIV#LV#LVI#LVII#LVIII#LIX#LX#LXI#LXII#LXIII#LXIV#LXV#LXVI#LXVII#LXVIII#LXIX#LXX#LXXI#LXXII#LXXIII#LXXIV#LXXV#LXXVI#LXXVII#LXXVIII#LXXIX#LXXX#LXXXI#LXXXII#LXXXIII#LXXXIV#LXXXV#LXXXVI#LXXXVII#LXXXVIII#LXXXIX#XC#XCI#XCII#XCIII#XCIV#XCV#XCVI#XCVII#XCVIII#XCIX#C#CI".Split('#');
        public AIDInfo()
        {
        }
        public string JID   { get; set; }
        public string AID   { get; set; }
        public string Category { get; set; }
        public string TOCCategory { get; set; }
        public string PrdType  { get; set; }

        public string pdfPages { get; set; }
        public string sPage { get; set; }
        public string ePage { get; set; }

        public string OnlineDate   { get; set; }
        public string AAOnlineDate { get; set; }
        public string ColorFigure  { get; set; }
        public string Figs     { get; set; }
        public string ArticleRemarks { get; set; }

        public string ArticleTitle { get; set; }
        public string Doi { get; set; }
        public string AuthorName { get; set; }


    }
}