using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DynamicContent.Mvc.Controllers;
using DynamicContent.TemplateGeneration.Fields;
using DynamicContent.TemplateGeneration.Fields.Implementation;
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

            DynamicWidgetInitializer.RegisterFieldGenerationStrategies();
        }

        private static void RegisterFieldGenerationStrategies()
        {
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(LongTextAreaFieldGenerationStrategy), typeof(LongTextAreaFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(ImagesFieldGenerationStrategy), typeof(ImagesFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(ShortTextFieldGenerationStrategy), typeof(ShortTextFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(MultipleChoiceFieldGenerationStrategy), typeof(MultipleChoiceFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(YesNoFieldGenerationStrategy), typeof(YesNoFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(DateFieldGenerationStrategy), typeof(DateFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(NumberFieldGenerationStrategy), typeof(NumberFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(PriceFieldGenerationStrategy), typeof(PriceFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(LongRichTextFieldGenerationStrategy), typeof(LongRichTextFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(VideosFieldGenerationStrategy), typeof(VideosFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(DocumentsFieldGenerationStrategy), typeof(DocumentsFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(AddressFieldGenerationStrategy), typeof(AddressFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(ClassificationFieldGenerationStrategy), typeof(ClassificationFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(RelatedMediaFieldGenerationStrategy), typeof(RelatedMediaFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(IFieldGenerationStrategy), typeof(RelatedDataFieldGenerationStrategy), typeof(RelatedDataFieldGenerationStrategy).Name, new ContainerControlledLifetimeManager());
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

                var mvcWidgetName = string.Format(CultureInfo.InvariantCulture, MvcConstants.MvcFieldControlNameTemplate, mvcAreaWidgetName);

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
