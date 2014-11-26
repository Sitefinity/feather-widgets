using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
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

            return condition;
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

            var path = this.FindRelatedDataFieldTemplatePath(field.FrontendWidgetTypeName, field.FrontendWidgetLabel, field.FieldNamespace, childItemTypeName, field.RelatedDataProvider, field.Name, isMasterView);

            return path;
        }
    }
}
