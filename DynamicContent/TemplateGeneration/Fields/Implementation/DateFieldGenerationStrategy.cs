using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for date time dynamic fields.
    /// </summary>
    public class DateFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.DateTime;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = string.Format(DateFieldGenerationStrategy.FieldMarkupTempalte, field.Name, field.Title);

            return markup;
        }

        private const string FieldMarkupTempalte = @"@Html.Sitefinity().DateField((DateTime?)Model.Item.{0}, ""MMM d, yyyy, HH:mm tt"", ""{0}"", fieldTitle: ""{1}"", cssClass: ""sfitemDateWrp"")";
    }
}
