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

namespace DynamicContent.FieldsGenerator
{
    /// <summary>
    /// This class represents field generation strategy for related data dynamic fields.
    /// </summary>
    public class RelatedDataFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedDataFieldGenerationStrategy"/> class.
        /// </summary>
        public RelatedDataFieldGenerationStrategy()
        {
            this.RelatedDataHelperMapper = new List<Tuple<Type, string, string>>();
            this.RelatedDataHelperMapper.Add(
                Tuple.Create(typeof(Image), 
                RelatedDataFieldGenerationStrategy.inlineImageListItem,
                RelatedDataFieldGenerationStrategy.inlineSingleImageItem));
            this.RelatedDataHelperMapper.Add(
                Tuple.Create(typeof(Video), 
                RelatedDataFieldGenerationStrategy.inlineVideoListItem,
                RelatedDataFieldGenerationStrategy.inlineSingleVideoItem));
            this.RelatedDataHelperMapper.Add(
                Tuple.Create(typeof(Document),
                RelatedDataFieldGenerationStrategy.inlineDocumentListItem,
                RelatedDataFieldGenerationStrategy.inlineSingleDocumentItem));
            this.RelatedDataHelperMapper.Add(
                Tuple.Create(typeof(PageNode),
                RelatedDataFieldGenerationStrategy.inlinePageList,
                RelatedDataFieldGenerationStrategy.inlineSinglePage));
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
        public IList<Tuple<Type, string, string>> RelatedDataHelperMapper { private set; get; }

        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.RelatedData;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
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
            var result = String.Format(template, fieldName, identifierField, frontendWidgetLabel);

            return result;
        }

        private string GetInlineFieldTemplate(bool isMasterView, Type childType)
        {
            var template = String.Empty;

            foreach (var mapper in this.RelatedDataHelperMapper)
            {
                if (mapper.Item1.IsAssignableFrom(childType))
                {
                    template = isMasterView ? mapper.Item2 : mapper.Item3;

                    return template;
                }
            }

            template = isMasterView ? RelatedDataFieldGenerationStrategy.inlineListItem : RelatedDataFieldGenerationStrategy.inlineSingleItem;

            return template;
        }

        internal const string InlineControlValue = "inline";

        private const string inlineSingleItem = @"@Html.Sitefinity().RelatedDataInlineSingleField(((object) Model.Item).GetRelatedItems(""{0}"").FirstOrDefault<IDataItem>(), ""{1}"", ""{2}"")";
        private const string inlineListItem = @"@Html.Sitefinity().RelatedDataInlineListField(((object) Model.Item).GetRelatedItems(""{0}"").ToList<IDataItem>(), ""{1}"", ""{2}"")";

        private const string inlineSinglePage = @"@Html.Sitefinity().RelatedPageInlineSingleField(((object) Model.Item).GetRelatedItems(""{0}"").FirstOrDefault<IDataItem>(), ""{1}"", ""{2}"")";
        private const string inlinePageList = @"@Html.Sitefinity().RelatedPageInlineListField(((object) Model.Item).GetRelatedItems(""{0}"").ToList<IDataItem>(), ""{1}"", ""{2}"")";

        private const string inlineSingleImageItem = @"@Html.Sitefinity().RelatedImageInlineSingleField(((object) Model.Item).GetRelatedItems(""{0}"").FirstOrDefault<IDataItem>(), ""{1}"", ""{2}"")";
        private const string inlineImageListItem = @"@Html.Sitefinity().RelatedImageInlineListField(((object) Model.Item).GetRelatedItems(""{0}"").ToList<IDataItem>(), ""{1}"", ""{2}"")";

        private const string inlineSingleVideoItem = @"@Html.Sitefinity().RelatedVideoInlineSingleField(((object) Model.Item).GetRelatedItems(""{0}"").FirstOrDefault<IDataItem>(), ""{1}"", ""{2}"")";
        private const string inlineVideoListItem = @"@Html.Sitefinity().RelatedVideoInlineListField(((object) Model.Item).GetRelatedItems(""{0}"").ToList<IDataItem>(), ""{1}"", ""{2}"")";

        private const string inlineSingleDocumentItem = @"@Html.Sitefinity().RelatedDocumentInlineSingleField(((object) Model.Item).GetRelatedItems(""{0}"").FirstOrDefault<IDataItem>(), ""{1}"", ""{2}"")";
        private const string inlineDocumentListItem = @"@Html.Sitefinity().RelatedDocumentInlineListField(((object) Model.Item).GetRelatedItems(""{0}"").ToList<IDataItem>(), ""{1}"", ""{2}"")";
    }
}
