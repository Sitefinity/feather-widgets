using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
{
    /// <summary>
    /// This class represents field generation strategy for number dynamic fields.
    /// </summary>
    public class NumberFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.Number;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = String.Format(NumberFieldGenerationStrategy.fieldMarkupTempalte, field.Name, field.NumberUnit, field.Title);

            return markup;
        }

        private const string fieldMarkupTempalte = @"@Html.Sitefinity().NumberField((string)Model.Item.{0}.ToString(), ""{1}"", ""{0}"", ""{2}"", ""sfitemNumberWrp"")";
    }
}
