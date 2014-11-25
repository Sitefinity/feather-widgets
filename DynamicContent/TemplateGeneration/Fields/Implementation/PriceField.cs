using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for price dynamic fields.
    /// </summary>
    public class PriceField : Field
    {
        /// <inheritdoc/>
        public override bool GetCondition(DynamicModuleField field)
        {
            var condition = base.GetCondition(field)
                && field.FieldType == FieldType.Currency;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetMarkup(DynamicModuleField field)
        {
            var markup = string.Format(PriceField.FieldMarkupTempalte, field.Name, PriceField.CurrencyFormat);

            return markup;
        }

        private const string CurrencyFormat = "{0:C}";
        private const string FieldMarkupTempalte = @"@Html.Sitefinity().PriceField((string)Model.Item.{0}, ""{0}"", ""{1}"", cssClass: ""sfitemPrice"")";
    }
}
