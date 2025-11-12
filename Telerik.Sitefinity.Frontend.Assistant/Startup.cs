using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DesignerToolbox;

namespace Telerik.Sitefinity.Frontend.Assistant
{
    public static class Startup
    {
        public static void OnPreApplicationStart()
        {
            Bootstrapper.Initialized -= Bootstrapper_Initialized;
            Bootstrapper.Initialized += Bootstrapper_Initialized;
        }

        private static void Bootstrapper_Initialized(object sender, Data.ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapped")
            {
                ObjectFactory.Container.RegisterType<IToolboxFilter, SitefinityAssistantToolboxFilter>(typeof(SitefinityAssistantToolboxFilter).FullName);
            }
        }
    }
}
