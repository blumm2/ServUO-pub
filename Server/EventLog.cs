#region References
using System;
using System.Diagnostics;

using DiagELog = System.Diagnostics.EventLog;
#endregion

namespace Server
{
	// Writes to the Windows Event Log; on other systems the entries go to the console instead.
	public static class EventLog
	{
		static EventLog()
		{
			if (OperatingSystem.IsWindows() && !DiagELog.SourceExists("ServUO"))
			{
				DiagELog.CreateEventSource("ServUO", "Application");
			}
		}

		private static void Write(string text, EventLogEntryType type, int eventID)
		{
			if (OperatingSystem.IsWindows())
			{
				DiagELog.WriteEntry("ServUO", text, type, eventID);
			}
			else
			{
				Console.WriteLine("EventLog {0} {1}: {2}", type, eventID, text);
			}
		}

		public static void Error(int eventID, string text)
		{
			Write(text, EventLogEntryType.Error, eventID);
		}

		public static void Error(int eventID, string format, params object[] args)
		{
			Error(eventID, String.Format(format, args));
		}

		public static void Warning(int eventID, string text)
		{
			Write(text, EventLogEntryType.Warning, eventID);
		}

		public static void Warning(int eventID, string format, params object[] args)
		{
			Warning(eventID, String.Format(format, args));
		}

		public static void Inform(int eventID, string text)
		{
			Write(text, EventLogEntryType.Information, eventID);
		}

		public static void Inform(int eventID, string format, params object[] args)
		{
			Inform(eventID, String.Format(format, args));
		}
	}
}
