using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for number dynamic fields.
    /// </summary>
    public class NumberField : Field
    {
        /// <inheritdoc/>
        public override bool GetCondition(DynamicModuleField field)
        {
            var condition = base.GetCondition(field)
                && field.FieldType == FieldType.Number;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetMarkup(DynamicModuleField field)
        {
            var markup = string.Format(NumberField.FieldMarkupTempalte, field.Name, field.NumberUnit, field.Title);

            return markup;
        }

        private const string FieldMarkupTempalte = @"@Html.Sitefinity().NumberField((decimal?)Model.Item.{0}, ""{1}"", ""{0}"", fieldTitle: ""{2}"", cssClass: ""sfitemNumberWrp"")";
    }
}
