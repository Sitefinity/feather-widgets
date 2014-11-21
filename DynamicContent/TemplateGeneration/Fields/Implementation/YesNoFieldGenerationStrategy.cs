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
    /// This class represents field generation strategy for yes/no dynamic fields.
    /// </summary>
    public class YesNoFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.YesNo;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = string.Format(YesNoFieldGenerationStrategy.FieldMarkupTempalte, field.Name, field.Title);

            return markup;
        }

        private const string FieldMarkupTempalte = @"@Html.Sitefinity().YesNoField((bool)Model.Item.{0}, ""{0}"", fieldTitle: ""{1}"", cssClass: ""sfitemChoices"")";
    }
}
