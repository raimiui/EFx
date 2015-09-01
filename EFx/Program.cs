using System;
using System.ServiceProcess;
using EFx.Configurations;
using EFx.TraderService;
using EFx.IBll;
using EFx.IDal;
using Microsoft.Practices.Unity;


namespace EFx.TraderService
{
    class Program
    {
        static void Main(string[] args)
        {
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
