using System;
using System.Configuration;
using System.IO;
using EFx.IBll;

namespace EFx.Bll
{
    public class FileLogService : ILogService
    {
        private readonly IAppSettingsService _appSettingsService;

        public FileLogService(IAppSettingsService appSettingsService)
        {
            _appSettingsService = appSettingsService;
        }

        public void Log(Exception ex)
        {
            Log(null, ex);
        }

        public void Log(string message)
        {
            Log(message, null);
        }

        public int MaxLogFileSizeInMb
        {
            get
            {
                int result;
                if (!Int32.TryParse(_appSettingsService.MaxLogFileSizeInMB, out result))
                    result = 100;
                return result;
            }
        }

        private void Log(string message, Exception ex)
        {
            var logFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
            
            if (File.Exists(logFileName) && new FileInfo(logFileName).Length > MaxLogFileSizeInMb * 1024) // delete log file when greater than 100MB
                File.WriteAllText(logFileName, string.Empty);

            using (StreamWriter logWriter = File.Exists(logFileName) ? File.AppendText(logFileName) : new StreamWriter(logFileName))
            {
                logWriter.WriteLine("\n---------------" + DateTime.Now.ToString() + "--------------------");

                if (!string.IsNullOrEmpty(message))
                    logWriter.WriteLine(message);

                if (ex != null)
                {
                    logWriter.WriteLine(ex.Message);
                    logWriter.WriteLine(ex.StackTrace);    
                }
            }
        }
    }
}