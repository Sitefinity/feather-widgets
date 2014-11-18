using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
{
    /// <summary>
    /// This class represents field generation strategy for price dynamic fields.
    /// </summary>
    public class PriceFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.Currency;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = string.Format(PriceFieldGenerationStrategy.FieldMarkupTempalte, field.Name, PriceFieldGenerationStrategy.CurrencyFormat);

            return markup;
        }

        private const string CurrencyFormat = "{0:C}";
        private const string FieldMarkupTempalte = @"@Html.Sitefinity().PriceField((string)Model.Item.{0}, ""{0}"", ""{1}"", ""sfitemPrice"")";
    }
}
