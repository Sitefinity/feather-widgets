using System;
using System.Collections.Generic;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for related data dynamic fields.
    /// </summary>
    public class RelatedDataField : Field
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedDataField"/> class.
        /// </summary>
        public RelatedDataField()
        {
            this.RelatedDataHelperMapper = new List<Tuple<Type, string, string>>();
            this.RelatedDataHelperMapper.Add(Tuple.Create(
                typeof(Image),
                RelatedDataField.InlineImageListItem,
                RelatedDataField.InlineSingleImageItem));
            this.RelatedDataHelperMapper.Add(Tuple.Create(
                typeof(Video),
                RelatedDataField.InlineVideoListItem,
                RelatedDataField.InlineSingleVideoItem));
            this.RelatedDataHelperMapper.Add(Tuple.Create(
                typeof(Document),
                RelatedDataField.InlineDocumentListItem,
                RelatedDataField.InlineSingleDocumentItem));
            this.RelatedDataHelperMapper.Add(Tuple.Create(
                typeof(PageNode),
                RelatedDataField.InlinePageList,
                RelatedDataField.InlineSinglePage));
        }

        /// <summary>
        /// Gets or sets the related data helper mapper.
        /// </summary>
        /// <remarks>
        /// Item1 is the type of the related item; Item2 is the List template for it; Item3 represents the single template.
        /// </remarks>
        /// <value>
        /// The related data helper mapper.
        /// </value>
        public IList<Tuple<Type, string, string>> RelatedDataHelperMapper { get;  private set; }

        /// <inheritdoc/>
        public override bool GetCondition(DynamicModuleField field)
        {
            var condition = base.GetCondition(field)
                && field.FieldType == FieldType.RelatedData;

            return condition;
        }

        /// <inheritdoc/>
        protected override string GetTemplatePath(DynamicModuleField field)
        {
            return this.FindRelatedDataFieldTemplatePath(field.FrontendWidgetTypeName, field.FrontendWidgetLabel, field.FieldNamespace, field.RelatedDataType, field.RelatedDataProvider, field.Name, field.CanSelectMultipleItems);
        }

        /// <summary>
        /// Builds the related data field template.
        /// </summary>
        /// <param name="frontendWidgetTypeName">Name of the frontend widget type.</param>
        /// <param name="frontendWidgetLabel">The frontend widget label.</param>
        /// <param name="parentTypeName">Name of the parent type.</param>
        /// <param name="childTypeName">Name of the child type.</param>
        /// <param name="childTypeProviderName">Name of the child type provider.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="isMasterView">if set to <c>true</c> is master view.</param>
        /// <returns></returns>
        protected string FindRelatedDataFieldTemplatePath(string frontendWidgetTypeName, string frontendWidgetLabel, string parentTypeName, string childTypeName, string childTypeProviderName, string fieldName, bool isMasterView)
        {
            var childType = TypeResolutionService.ResolveType(childTypeName, false);
            var identifierField = RelatedDataHelper.GetRelatedTypeIdentifierField(childTypeName);
            var templateMarkup = this.GetInlineFieldTemplatePath(isMasterView, childType);
            //var result = string.Format(template, fieldName, identifierField, frontendWidgetLabel);

            return templateMarkup;
        }

        private string GetInlineFieldTemplatePath(bool isMasterView, Type childType)
        {
            var template = string.Empty;

            foreach (var mapper in this.RelatedDataHelperMapper)
            {
                if (mapper.Item1.IsAssignableFrom(childType))
                {
                    template = isMasterView ? mapper.Item2 : mapper.Item3;

                    return template;
                }
            }

            template = isMasterView ? RelatedDataField.InlineListItem : RelatedDataField.InlineSingleItem;

            return template;
        }

        internal const string InlineControlValue = "inline";

        private const string InlineSingleItem = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/RelatedDataSingleField.cshtml";
        private const string InlineListItem = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/RelatedDataInlineListField.cshtml";

        private const string InlineSinglePage = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/RelatedPageSingleField.cshtml";
        private const string InlinePageList = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/RelatedPageInlineListField.cshtml";

        private const string InlineSingleImageItem = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/RelatedImageSingleField.cshtml";
        private const string InlineImageListItem = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/RelatedImageInlineListField.cshtml";

        private const string InlineSingleVideoItem = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/RelatedVideoSingleField.cshtml";
        private const string InlineVideoListItem = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/RelatedVideoInlineListField.cshtml";

        private const string InlineSingleDocumentItem = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/RelatedDocumentSingleField.cshtml";
        private const string InlineDocumentListItem = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/RelatedDocumentInlineListField.cshtml";
    }
}
