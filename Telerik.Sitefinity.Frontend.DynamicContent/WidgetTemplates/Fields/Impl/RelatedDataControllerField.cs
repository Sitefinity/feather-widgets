using System;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.Resources;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Frontend.DynamicContent.WidgetTemplates.Fields.Impl
{
    public class RelatedDataControllerField : Field
    {
        public override bool GetCondition(DynamicModules.Builder.Model.DynamicModuleField field)
        {            
            var condition = base.GetCondition(field)
                && (field.FieldType == FieldType.RelatedData || field.FieldType == FieldType.RelatedMedia);

            if(!condition) return false;

            this.frontEndWidgetType = TypeResolutionService.ResolveType(field.FrontendWidgetTypeName, false);

            return this.frontEndWidgetType != null &&
                this.frontEndWidgetType.ImplementsInterface(typeof(IRelatedDataController));
        }

        public override string GetMarkup(DynamicModuleField field)
        {
            var factory = ControllerBuilder.Current.GetControllerFactory() as ISitefinityControllerFactory;

            var controllerName = factory.GetControllerName(this.frontEndWidgetType);

            var manager = ModuleBuilderManager.GetManager();
            var dynamicType = manager.Provider.GetDynamicModuleTypes()
                .Where(t => t.Id == field.ParentTypeId)
                .Select(t => String.Format("{0}.{1}", t.TypeNamespace, t.TypeName))
                .First();
            
            var viewModel = new RelatedDataViewModel()
            {
                RelatedItemType = dynamicType,
                RelatedFieldName = field.Name,
                RelationTypeToDisplay = "Child",
                ControllerName = controllerName
            };

            var templatePath = this.GetTemplatePath(field);
            var processor = new RazorTemplateProcessor();

            return processor.Run(templatePath, viewModel);
        }

        protected override string GetTemplatePath(DynamicModules.Builder.Model.DynamicModuleField field)
        {
            return RelatedDataControllerField.TemplatePath;
        }

        private const string TemplatePath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/WidgetTemplates/Fields/Templates/RelatedDataControllerField.cshtml";
        private Type frontEndWidgetType;
    }
}
