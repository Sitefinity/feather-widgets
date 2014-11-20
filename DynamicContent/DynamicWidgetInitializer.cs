using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DynamicContent.Mvc.Controllers;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;

namespace DynamicContent
{
    /// <summary>
    /// This class is responsible for initialization of the dynamic widget.
    /// </summary>
    public class DynamicWidgetInitializer
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize() 
        {
            ObjectFactory.Container.RegisterType<IWidgetInstallationStrategy, MvcWidgetInstallationStrategy>(new ContainerControlledLifetimeManager());

            DynamicWidgetInitializer.RegisterDynamicTemplatableControl();
        }

        /// <summary>
        /// Register MVC widgets used for dynamic content types as templatable controls
        /// </summary>
        private static void RegisterDynamicTemplatableControl()
        {
            var dynamicContentType = DynamicWidgetInitializer.GetActiveDynamicModuleTypes();

            var dynamicContentControllerType = typeof(DynamicContentController);

            foreach (var dynamicType in dynamicContentType)
            {
                var mvcAreaWidgetName = string.Format(CultureInfo.InvariantCulture, "{0} - {1}", dynamicType.ModuleName, dynamicType.DisplayName);
                var mvcWidgetName = string.Concat(mvcAreaWidgetName, " ", MvcConstants.MvcSuffix);

                Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(dynamicContentControllerType, dynamicContentControllerType, string.Empty, mvcAreaWidgetName, mvcWidgetName);
            }
        }

        /// <summary>
        /// Gets the current active dynamic module types.
        /// </summary>
        /// <returns>List of DynamiModuleType</returns>
        private static List<DynamicModuleType> GetActiveDynamicModuleTypes()
        {
            var manager = ModuleBuilderManager.GetManager();
            var provider = manager.Provider as ModuleBuilderDataProvider;
            List<DynamicModuleType> activeModuleTypes = new List<DynamicModuleType>();
            var activeModules = provider.GetDynamicModules().Where(m => m.Status == DynamicModuleStatus.Active);

            foreach (var activeModule in activeModules)
            {
                var moduleTypes = provider.GetDynamicModuleTypes().Where(t => t.ParentModuleId == activeModule.Id);
                activeModuleTypes.AddRange(moduleTypes);
            }

            return activeModuleTypes.ToList();
        }
    }
}
