using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
{
    public class NumberFieldGenerationStrategy : FieldGenerationStrategy
    {
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.Number;

            return condition;
        }

        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = String.Format(NumberFieldGenerationStrategy.fieldMarkupTempalte, field.Name, field.NumberUnit);

            return markup;
        }

        private const string fieldMarkupTempalte = "@Html.Sitefinity.NumberField(Model.Item.{0}, {1})";
    }
}
