using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// Contains the application startup event handles related to the Feather Forms functionality of Sitefinity.
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Called before the Asp.Net application is started.
        /// </summary>
        public static void OnPreApplicationStart()
        {
            Bootstrapper.Initialized -= Startup.Bootstrapper_Initialized;
            Bootstrapper.Initialized += Startup.Bootstrapper_Initialized;
            
        }

        private static void Bootstrapper_Initialized(object sender, Data.ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapped")
                ObjectFactory.Container.RegisterInstance<IControlDefinitionExtender>("FormsDefinitionsExtender", new FormsDefinitionsExtender(), new ContainerControlledLifetimeManager());
        }
    }
}
