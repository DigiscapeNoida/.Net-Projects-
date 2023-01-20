using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCFlowReminderMail
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                
                Console.Title = "[PC Work Flow For Book Reminder Mail]";
                Worker oWorker = new Worker();
                oWorker.SendNotification();
                System.Threading.Thread.Sleep(5000);

                oWorker.SendInternalNotification();
            }
            catch (Exception exe)
            {
                Console.WriteLine("Error :" + exe.Message.ToString());
                Console.ReadLine();
            }
        }
    }
}
