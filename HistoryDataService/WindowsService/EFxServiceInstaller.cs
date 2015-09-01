using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.ServiceProcess;


namespace EFx.HistoryDataService
{
    [RunInstaller(true)]
    public partial class EFxServiceInstaller : System.Configuration.Install.Installer
    {
        private readonly ServiceProcessInstaller _serviceProcessInstaller;
        private readonly ServiceInstaller _serviceInstaller;
        public readonly static string AppDataFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        //private FileLogger _logger;

        public EFxServiceInstaller()
        {
            //InitializeComponent();
            _serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.NetworkService,
                Username = null,
                Password = null
            };


            _serviceInstaller = new ServiceInstaller
            {
                DisplayName = ConfigurationManager.AppSettings["serviceName"],
                ServiceName = ConfigurationManager.AppSettings["serviceName"],
                Description = ConfigurationManager.AppSettings["serviceName"],
                StartType = ServiceStartMode.Automatic
            };

            Installers.AddRange(new Installer[] 
            {
                _serviceProcessInstaller,
                _serviceInstaller
            });

            //_logger = new FileLogger();
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            //using (var sc = new ServiceController(_serviceInstaller.ServiceName))
            //{
            //    if (sc.Status != ServiceControllerStatus.Stopped)
            //    {
            //        Context.LogMessage("Attempting to stop service.");

            //        sc.Stop();
            //        sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 5, 0));
            //        sc.Close();

            //        Context.LogMessage("Service stopped successfully."); Console.ReadKey();
            //    }
            //}
            base.OnBeforeInstall(savedState);
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            //if (!Directory.Exists(AppDataFolder)) Directory.CreateDirectory(AppDataFolder);

            try
            {
                var dir = new DirectoryInfo(AppDataFolder);
                var dirSecurity = dir.GetAccessControl();
                dirSecurity.AddAccessRule
                (
                    new FileSystemAccessRule
                        (
                            ServiceAccount.NetworkService.ToString(),
                            FileSystemRights.Write | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.CreateFiles | FileSystemRights.Modify | FileSystemRights.FullControl,
                            InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                            PropagationFlags.InheritOnly,
                            AccessControlType.Allow
                        )
                );
                dir.SetAccessControl(dirSecurity);
            }
            catch (Exception)
            {
            }

            //try
            //{


            //using (var sc = new ServiceController(_serviceInstaller.ServiceName))
            //{
            //    if (sc.Status != ServiceControllerStatus.Stopped)
            //        return;

            //    Context.LogMessage("Attempting to start service.");

            //    sc.Start();
            //    sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 2, 0));
            //    sc.Close();

            //    Context.LogMessage("Service started successfully.");
            //}
            //}
            //catch (Exception e)
            //{

            //    Console.WriteLine("OnAfterInstall error:" + e.Message);

            //}
            //Console.ReadKey();
        }

        protected override void OnAfterRollback(IDictionary savedState)
        {
            //if (Directory.Exists(AppDataFolder)) Directory.Delete(AppDataFolder);

            base.OnAfterRollback(savedState);
        }

        protected override void OnAfterUninstall(IDictionary savedState)
        {
            //if (Directory.Exists(AppDataFolder)) Directory.Delete(AppDataFolder);

            base.OnAfterUninstall(savedState);
        }
    }
}
