using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace LWWUploadTrigger
{
	class Log
	{
		public Log()
		{
			if (Directory.Exists(Application.StartupPath + "\\Log"))
			{
				string[] fls = Directory.GetFiles(Application.StartupPath + "\\Log");
				if (fls.Count() > 0)
				{
					foreach (string fl in fls)
					{
						if (File.GetLastWriteTime(fl) < DateTime.Now.AddDays(-30))
						{
							try
							{
								File.Delete(fl);
							}
							catch
							{
							}
						}
					}
				}
			}
		}
		public void Generatelog(string logstring)
		{
			try
			{
				if (logstring != null)
				{
					if (!Directory.Exists(Application.StartupPath + "\\Log"))
					{
						Directory.CreateDirectory(Application.StartupPath + "\\Log");
					}
					Console.WriteLine(DateTime.Now + " " + logstring);
					string LogFile = Application.StartupPath + "\\Log\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".log";
					File.AppendAllText(LogFile, DateTime.Now + " " + logstring + Environment.NewLine);
				}
			}
			catch
			{
			}
		}
		public void Errorlog(string logstring)
		{
			try
			{
				if (logstring != null)
				{
					if (!Directory.Exists(Application.StartupPath + "\\Log"))
					{
						Directory.CreateDirectory(Application.StartupPath + "\\Log");
					}
					string LogFile = Application.StartupPath + "\\Log\\" + DateTime.Now.ToString("dd-MM-yyyy") + "_error.log";
					File.AppendAllText(LogFile, DateTime.Now + " " + logstring + Environment.NewLine);
				}
			}
			catch
			{
			}
		}
	}
}
