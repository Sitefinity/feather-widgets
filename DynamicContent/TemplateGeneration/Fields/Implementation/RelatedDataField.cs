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
        public override string GetMarkup(DynamicModuleField field)
        {
            var markup = string.Format(this.BuildRelatedDataFieldTemplate(field.FrontendWidgetTypeName, field.FrontendWidgetLabel, field.FieldNamespace, field.RelatedDataType, field.RelatedDataProvider, field.Name, field.CanSelectMultipleItems));

            return markup;
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
        protected string BuildRelatedDataFieldTemplate(string frontendWidgetTypeName, string frontendWidgetLabel, string parentTypeName, string childTypeName, string childTypeProviderName, string fieldName, bool isMasterView)
        {
            var childType = TypeResolutionService.ResolveType(childTypeName, false);
            var identifierField = RelatedDataHelper.GetRelatedTypeIdentifierField(childTypeName);
            var template = this.GetInlineFieldTemplate(isMasterView, childType);
            var result = string.Format(template, fieldName, identifierField, frontendWidgetLabel);

            return result;
        }

        private string GetInlineFieldTemplate(bool isMasterView, Type childType)
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

        private const string InlineSingleItem = @"@Html.Sitefinity().RelatedDataInlineSingleField(((object) Model.Item).GetRelatedItems(""{0}"").FirstOrDefault<IDataItem>(), ""{1}"", frontendWidgetLabel: ""{2}"")";
        private const string InlineListItem = @"@Html.Sitefinity().RelatedDataInlineListField(((object) Model.Item).GetRelatedItems(""{0}"").ToList<IDataItem>(), ""{1}"", frontendWidgetLabel: ""{2}"")";

        private const string InlineSinglePage = @"@Html.Sitefinity().RelatedPageInlineSingleField(((object) Model.Item).GetRelatedItems(""{0}"").FirstOrDefault<IDataItem>(), ""{1}"", frontendWidgetLabel: ""{2}"")";
        private const string InlinePageList = @"@Html.Sitefinity().RelatedPageInlineListField(((object) Model.Item).GetRelatedItems(""{0}"").ToList<IDataItem>(), ""{1}"", frontendWidgetLabel: ""{2}"")";

        private const string InlineSingleImageItem = @"@Html.Sitefinity().RelatedImageInlineSingleField(((object) Model.Item).GetRelatedItems(""{0}"").FirstOrDefault<IDataItem>(), ""{1}"", frontendWidgetLabel: ""{2}"")";
        private const string InlineImageListItem = @"@Html.Sitefinity().RelatedImageInlineListField(((object) Model.Item).GetRelatedItems(""{0}"").ToList<IDataItem>(), ""{1}"", frontendWidgetLabel: ""{2}"")";

        private const string InlineSingleVideoItem = @"@Html.Sitefinity().RelatedVideoInlineSingleField(((object) Model.Item).GetRelatedItems(""{0}"").FirstOrDefault<IDataItem>(), ""{1}"", frontendWidgetLabel: ""{2}"")";
        private const string InlineVideoListItem = @"@Html.Sitefinity().RelatedVideoInlineListField(((object) Model.Item).GetRelatedItems(""{0}"").ToList<IDataItem>(), ""{1}"", frontendWidgetLabel: ""{2}"")";

        private const string InlineSingleDocumentItem = @"@Html.Sitefinity().RelatedDocumentInlineSingleField(((object) Model.Item).GetRelatedItems(""{0}"").FirstOrDefault<IDataItem>(), ""{1}"", frontendWidgetLabel: ""{2}"")";
        private const string InlineDocumentListItem = @"@Html.Sitefinity().RelatedDocumentInlineListField(((object) Model.Item).GetRelatedItems(""{0}"").ToList<IDataItem>(), ""{1}"", frontendWidgetLabel: ""{2}"")";
    }
}
