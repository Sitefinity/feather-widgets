using System;
using System.Collections.Generic;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Mvc.Controllers;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Frontend.DynamicContent.WidgetTemplates.Fields.Impl
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

            if (!condition) return false;

            var frontEndWidgetType = TypeResolutionService.ResolveType(field.FrontendWidgetTypeName, false);

            return frontEndWidgetType == null ||
                !frontEndWidgetType.ImplementsInterface(typeof(IRelatedDataController));
        }

        /// <inheritdoc/>
        protected override string GetTemplatePath(DynamicModuleField field)
        {
            return this.FindRelatedDataFieldTemplatePath(field.RelatedDataType, field.CanSelectMultipleItems);
        }

        /// <summary>
        /// Builds the related data field template.
        /// </summary>
        /// <param name="childTypeName">Name of the child type.</param>
        /// <param name="isMasterView">if set to <c>true</c> is master view.</param>
        /// <returns></returns>
        protected string FindRelatedDataFieldTemplatePath(string childTypeName, bool isMasterView)
        {
            var childType = TypeResolutionService.ResolveType(childTypeName, false);
            var templateMarkup = this.GetInlineFieldTemplatePath(isMasterView, childType);

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

        private const string InlineSingleItem = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/WidgetTemplates/Fields/Templates/RelatedDataSingleField.cshtml";
        private const string InlineListItem = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/WidgetTemplates/Fields/Templates/RelatedDataInlineListField.cshtml";

        private const string InlineSinglePage = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/WidgetTemplates/Fields/Templates/RelatedPageSingleField.cshtml";
        private const string InlinePageList = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/WidgetTemplates/Fields/Templates/RelatedPageInlineListField.cshtml";

        private const string InlineSingleImageItem = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/WidgetTemplates/Fields/Templates/RelatedImageSingleField.cshtml";
        private const string InlineImageListItem = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/WidgetTemplates/Fields/Templates/RelatedImageInlineListField.cshtml";

        private const string InlineSingleVideoItem = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/WidgetTemplates/Fields/Templates/RelatedVideoSingleField.cshtml";
        private const string InlineVideoListItem = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/WidgetTemplates/Fields/Templates/RelatedVideoInlineListField.cshtml";

        private const string InlineSingleDocumentItem = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/WidgetTemplates/Fields/Templates/RelatedDocumentSingleField.cshtml";
        private const string InlineDocumentListItem = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/WidgetTemplates/Fields/Templates/RelatedDocumentInlineListField.cshtml";
    }
}
