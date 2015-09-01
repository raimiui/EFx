using System;
using System.ServiceProcess;
using EFx.Configurations;
using EFx.HistoryDataService;
using EFx.IBll;
using EFx.IDal;
using Microsoft.Practices.Unity;
using System.Diagnostics;


namespace EFx.HistoryDataService
{
    class Program
    {
        static void Main(string[] args)
        {         
            //var sSource = "11 dotNET Sample App";
            //var sLog = "11 Application";
            //var sEvent = "11 Sample Event";

            //if (!EventLog.SourceExists(sSource))
            //    EventLog.CreateEventSource(sSource, sLog);

            //var messageToEventLog = string.Format("{0}\n\nStack Trace:\n{1}", "Pardon", "Pardon");
            //EventLog.WriteEntry(sSource, messageToEventLog, EventLogEntryType.Error);


            Bootstrapper.Initialize();
            var unityContainer = UnityContainerFactory.UnityContainer;
            var logService = unityContainer.Resolve<ILogService>();
            var workerService = unityContainer.Resolve<IWorkerService>();
            var tradingDao = unityContainer.Resolve<ITradingDao>();
            var configurationService = unityContainer.Resolve<IAppSettingsService>();

            var serviceName = configurationService.ServiceName;

            if (!Environment.UserInteractive)
            {
                ServiceBase.Run(new EFxService(logService, workerService, tradingDao, configurationService));
                return;
            }

            if (args != null && args.Length > 0)
            {
                switch (args[0].ToLower())
                {
                    case "/install":
                    case "/i":
                        EFxService.InstallWindowsService();
                        return;
                    case "/uninstall":
                    case "/u":
                        EFxService.UninstallWindowsService();
                        return;
                }
            }

            RunUserControlled(args, logService, workerService, tradingDao, configurationService);
        }

        private static void RunUserControlled(string[] args, ILogService logService, IWorkerService workerService, ITradingDao tradingDao, IAppSettingsService appSettingsService)
        {
            var eFxService = new EFxService(logService, workerService, tradingDao, appSettingsService);
            eFxService.StartService();
        }
    }
}
