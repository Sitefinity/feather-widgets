using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Builder.Install;

namespace DynamicContent
{
    public class DynamicWidgetInitializer
    {
        public static void Initialize() 
        {
            ObjectFactory.Container.RegisterType<IWidgetInstallationStrategy, MvcWidgetInstallationStrategy>(new ContainerControlledLifetimeManager());
        }
    }
}
