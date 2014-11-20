using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
{
    /// <summary>
    /// This class represents field generation strategy for address dynamic fields.
    /// </summary>
    public class AddressFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.Address;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var longTextMarkup = string.Format(AddressFieldGenerationStrategy.FieldMarkupTempalte, field.Name, field.Title);

            return longTextMarkup;
        }

        private const string FieldMarkupTempalte = @"@Html.Sitefinity().AddressField((Address)Model.Item.{0}, ""{0}"", ""#=Street# #=City# #=State# #=Country#"", fieldTitle: ""{1}"", cssClass: ""sfitemAddressWrp"")";
    }
}
