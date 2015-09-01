using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.IBll;
using System.Diagnostics;

namespace EFx.Bll
{
    public class EventLogLoggerService : ILogService
    {
        private readonly IAppSettingsService _appSettingsService;
        private readonly EventLog _eventLog;

        public EventLogLoggerService(IAppSettingsService appSettingsService)
        {
            _appSettingsService = appSettingsService;
            _eventLog = new EventLog() { Log = "Application", Source = _appSettingsService.ServiceName };
            
            if (!EventLog.SourceExists(_eventLog.Source))
                EventLog.CreateEventSource(_eventLog.Source, _eventLog.Log);
        }
        
        public void Log(string message)
        {
            Log(new Exception(message));
        }

        public void Log(Exception ex)
        {
            var messageToEventLog = FormLogMessage(ex);
            _eventLog.WriteEntry(messageToEventLog, EventLogEntryType.Error);                       
        }

        private string FormLogMessage(Exception ex)
        {
            var messageToEventLog = string.Format("{0}\n\nStack Trace:\n{1}", ex.Message, ex.StackTrace);

            if (ex.InnerException != null)
                messageToEventLog += "\n\n" + FormLogMessage(ex.InnerException);

            return messageToEventLog;
        }
    }
}
