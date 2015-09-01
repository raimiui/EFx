using System.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using EFx.IBll;
using EFx.IDal;

namespace EFx.TraderService
{
    partial class EFxService : ServiceBase
    {
        private readonly ILogService _logService;
        private readonly IWorkerService _workerService;
        private readonly ITradingDao _tradingDao;
        private readonly IAppSettingsService _appSettingsService;
        private readonly Timer _timer;
        private readonly Timer _immediateExecutionTimer;
        
        public EFxService(ILogService logService, IWorkerService workerService, ITradingDao tradingDao, IAppSettingsService appSettingsService)
        {
            _logService = logService;
            _workerService = workerService;
            _tradingDao = tradingDao;
            _appSettingsService = appSettingsService;
            InitializeComponent();
            ServiceName = ConfigurationManager.AppSettings["serviceName"];
            _immediateExecutionTimer = new Timer { AutoReset = false, Interval = 1 };
            _immediateExecutionTimer.Elapsed += Run;
            _timer = new Timer { AutoReset = true, Interval = Interval };
            _timer.Elapsed += Run;
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            StartService();
        }

        protected override void OnStop()
        {
            StopService();
            base.OnStop();
        }

        public void StartService()
        {
            if(Environment.UserInteractive)
            {
                Run(null, null);
                return;
            }

            _immediateExecutionTimer.Start();
            _timer.Start();
        }

        private void Run(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                _workerService.Work();
            }
            catch (Exception ex)
            {
                _logService.Log(ex);
            }
        }

        public void StopService()
        {
            _immediateExecutionTimer.Stop();
            _immediateExecutionTimer.Dispose();
            _timer.Stop();
            _timer.Dispose();
        }

        private static int Interval
        {
            get
            {
                int interval;
                if (!Int32.TryParse(ConfigurationManager.AppSettings["workCycleInterval"], out interval))
                    interval = 60 * 60; // Default - 1h

                return interval * 1000;
            }
        }

        internal static void InstallWindowsService()
        {
            CheckUserIsAdmin();
            var path = Assembly.GetExecutingAssembly().Location;
            ManagedInstallerClass.InstallHelper(new[] { path });
        }

        internal static void UninstallWindowsService()
        {
            CheckUserIsAdmin();
            var path = Assembly.GetExecutingAssembly().Location;
            ManagedInstallerClass.InstallHelper(new[] { "/u", path });
        }

        private static void CheckUserIsAdmin()
        {
            var user = WindowsIdentity.GetCurrent();
            if (user == null)
                throw new InvalidOperationException("Cannot detect user privileges");

            var principal = new WindowsPrincipal(user);
            var isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            if (isAdmin)
                return;
            Console.WriteLine(@"You need administrative rights to install or uninstall service.");
            Console.WriteLine(@"Exiting...");
            Console.ReadKey();
            Environment.Exit(100);
        }
    }
}
