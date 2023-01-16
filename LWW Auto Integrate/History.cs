using System;
namespace LWWAutoIntegrate
{
    class DatePart
    {
        string _Year = string.Empty;
        string _Month = string.Empty;
        string _Day = string.Empty;
        string _Hour = string.Empty;
        string _Minute = string.Empty;
        string _Second = string.Empty;

        string _Date = string.Empty;


        public DatePart()
        { }
        public DatePart(string Year, String Month, string Day)
        {
            _Year = Year;
            _Month = Month;
            _Day = Day;
        }

        public string Date
        {
            get { return (_Day + "-" + _Month + "-" + _Year).Trim('-') ; }
            
        }

        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        public string Month
        {
            get { return _Month; }
            set { _Month = value; }
        }

        public string Day
        {
            get { return _Day; }
            set { _Day = value; }
        }

        public string Hour
        {
            get { return _Hour; }
            set { _Hour = value; }
        }

        public string Minute
        {
            get { return _Minute; }
            set { _Minute = value; }
        }

        public string Second
        {
            get { return _Second; }
            set { _Second = value; }
        }
    }
    class History
    {
        DatePart _ReceivedDate = new DatePart();
        DatePart _RevisedDate = new DatePart();
        DatePart _AcceptedDate = new DatePart();

        public DatePart ReceivedDate
        {
            get { return _ReceivedDate; }
            set { _ReceivedDate = value; }
        }

        public DatePart RevisedDate
        {
            get { return _RevisedDate; }
            set { _RevisedDate = value; }
        }

        public DatePart AcceptedDate
        {
            get { return _AcceptedDate; }
            set { _AcceptedDate = value; }
        }
    }
}
