using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using DynamicContent.Mvc.Controllers;
using DynamicContent.TemplateGeneration.Fields;
using DynamicContent.TemplateGeneration.Fields.Implementation;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Builder;
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
            DynamicWidgetInitializer.mvcWidgetInstaller = new MvcWidgetInstaller();

            DynamicWidgetInitializer.RegisterDynamicTemplatableControl();

            DynamicWidgetInitializer.RegisterFields();
        }

        private static void RegisterFields()
        {
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(LongTextAreaField), typeof(LongTextAreaField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(ImagesField), typeof(ImagesField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(ShortTextField), typeof(ShortTextField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(MultipleChoiceField), typeof(MultipleChoiceField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(YesNoField), typeof(YesNoField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(DateField), typeof(DateField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(NumberField), typeof(NumberField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(PriceField), typeof(PriceField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(LongRichTextField), typeof(LongRichTextField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(VideosField), typeof(VideosField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(DocumentsField), typeof(DocumentsField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(AddressField), typeof(AddressField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(ClassificationField), typeof(ClassificationField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(RelatedMediaField), typeof(RelatedMediaField).Name, new ContainerControlledLifetimeManager());
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(RelatedDataField), typeof(RelatedDataField).Name, new ContainerControlledLifetimeManager());
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
                var mvcAreaWidgetName = string.Format(CultureInfo.InvariantCulture, MvcConstants.DynamicAreaFormat, dynamicType.ModuleName, dynamicType.DisplayName);

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

        private static MvcWidgetInstaller mvcWidgetInstaller;
    }
}
