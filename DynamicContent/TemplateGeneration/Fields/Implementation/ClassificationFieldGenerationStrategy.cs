using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for classification dynamic fields.
    /// </summary>
    public class ClassificationFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.Classification;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = string.Format(ClassificationFieldGenerationStrategy.TaxonomyFieldMarkupTempalte, field.ClassificationId, field.Name, field.Title);

            return markup;
        }

        private const string TaxonomyFieldMarkupTempalte = @"@Html.Sitefinity().TaxonomyField((object)Model.Item.{1}, new Guid(""{0}""), ""{1}"", fieldTitle: ""{2}"")";
    }
}
