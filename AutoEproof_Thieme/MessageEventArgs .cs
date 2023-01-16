using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoEproof
{
    public delegate void NotifyMsg(string NotificationMsg);
    public delegate void ErrorrMsg(Exception Ex);
    public  class MessageEventArgs : EventArgs
    {

        public MessageEventArgs()
        { }
        public event NotifyMsg ProcessNotification;
        public event ErrorrMsg ErrorNotification;

        public MessageEventArgs(string message)
        {
            this.message = message;
        }
        private readonly string message;
        public string Message { get { return message; } }

        public void ProessEventHandler(string Msg)
        {
            if (ProcessNotification != null)
            {
                ProcessNotification(Msg);
            }
        }

        public void ProessErrorHandler(Exception ex)
        {
            if (ErrorNotification != null)
            {
                ErrorNotification(ex);
            }
        }
    }
}
