using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
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
            var markup = String.Format(DateFieldGenerationStrategy.fieldMarkupTempalte, field.Name);

            return markup;
        }

        private const string fieldMarkupTempalte = @"@Html.Sitefinity().DateField(Model.Item.{0}, ""MMM d, yyyy, HH:mm tt"")";
    }
}
