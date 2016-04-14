using System;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Mvc.Controllers;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Frontend.DynamicContent.WidgetTemplates.Fields.Impl
{
    /// <summary>
    /// This class represents field generation strategy for related media dynamic fields.
    /// </summary>
    public class RelatedMediaField : RelatedDataField
    {
        /// <inheritdoc/>
        public override bool GetCondition(DynamicModuleField field)
        {
            var condition = field.FieldStatus != DynamicModuleFieldStatus.Removed 
                && !field.IsHiddenField
                && field.FieldType == FieldType.RelatedMedia;

            if (!condition) return false;

            var frontEndWidgetType = TypeResolutionService.ResolveType(field.FrontendWidgetTypeName, false);

            return frontEndWidgetType == null ||
                !frontEndWidgetType.ImplementsInterface(typeof(IRelatedDataController));
        }

        /// <inheritdoc/>
        protected override string GetTemplatePath(DynamicModuleField field)
        {
            var isMasterView = false;
            var childItemTypeName = string.Empty;
            switch (field.MediaType)
            {
                case "image":
                    isMasterView = field.AllowMultipleImages;
                    childItemTypeName = typeof(Image).FullName;
                    break;
                case "video":
                    isMasterView = field.AllowMultipleVideos;
                    childItemTypeName = typeof(Video).FullName;
                    break;
                case "file":
                    isMasterView = field.AllowMultipleFiles;
                    childItemTypeName = typeof(Document).FullName;
                    break;
            }

            var path = this.FindRelatedDataFieldTemplatePath(childItemTypeName, isMasterView);

            return path;
        }
    }
}
