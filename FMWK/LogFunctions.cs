using System;
using System.Diagnostics;

namespace FMWK
{
    public class LogFunctions
    {
        private string _SOURCE = @"Triple Triad Extreme Server";
        private string _LOG = @"Application";

        public void WriteLog(string logText)
        {
            if (!EventLog.SourceExists(_SOURCE))
                EventLog.CreateEventSource(_SOURCE, _LOG);

            EventLog.WriteEntry(_SOURCE, logText);
        }
    }
}
