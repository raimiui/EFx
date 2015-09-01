using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;

namespace EFx.Configurations
{
    public static class Bootstrapper
    {
        public static void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            UnityContainerFactory.ConfigureUnityContainer();
            Dal.Automapping.Configure();
        }
    }
}
