using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace DynamicContent.FieldsGenerator
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
            var markup = string.Format(YesNoFieldGenerationStrategy.fieldMarkupTempalte, field.Name, field.Title);

            return markup;
        }

        private const string fieldMarkupTempalte = @"@Html.Sitefinity().YesNoField((bool)Model.Item.{0}, ""{0}"", ""{1}"", ""sfitemChoices"")";
    }
}
