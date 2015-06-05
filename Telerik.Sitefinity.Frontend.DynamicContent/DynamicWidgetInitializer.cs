using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.DynamicContent.WidgetTemplates.Fields;
using Telerik.Sitefinity.Frontend.DynamicContent.WidgetTemplates.Fields.Impl;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.DynamicContent
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
            if (SystemManager.GetModule("ModuleBuilder") == null)
                return;
            
            MvcWidgetInstaller.Initialize();

            DynamicWidgetInitializer.RegisterDynamicTemplatableControl();

            DynamicWidgetInitializer.RegisterFields();
        }

        /// <summary>
        /// Deactivates this instance.
        /// </summary>
        public static void Deactivate()
        {
            DynamicWidgetInitializer.RegisterFields();
            MvcWidgetInstaller.Deactivate();
        }

        private static void RegisterFields()
        {
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(LongTextAreaField), typeof(LongTextAreaField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(ImagesField), typeof(ImagesField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(ShortTextField), typeof(ShortTextField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(MultipleChoiceField), typeof(MultipleChoiceField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(YesNoField), typeof(YesNoField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(DateField), typeof(DateField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(NumberField), typeof(NumberField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(PriceField), typeof(PriceField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(LongRichTextField), typeof(LongRichTextField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(VideosField), typeof(VideosField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(DocumentsField), typeof(DocumentsField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(AddressField), typeof(AddressField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(ClassificationField), typeof(ClassificationField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(RelatedMediaField), typeof(RelatedMediaField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(RelatedDataField), typeof(RelatedDataField).Name);
            ObjectFactory.Container.RegisterType(typeof(Field), typeof(RelatedDataControllerField), typeof(RelatedDataControllerField).Name);
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
    }
}
