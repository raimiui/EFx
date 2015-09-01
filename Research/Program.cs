using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.Configurations;
using EFx.IBll;
using Microsoft.Practices.Unity;

namespace Research
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.Initialize();
            var unityContainer = UnityContainerFactory.UnityContainer;
            var configurationService = unityContainer.Resolve<IAppSettingsService>();

            var serviceName = configurationService.ServiceName;
        }
    }
}
