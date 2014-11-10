using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
{
    public class ShortTextFieldGenerationStrategy : FieldGenerationStrategy
    {
        public ShortTextFieldGenerationStrategy(DynamicModuleType moduleType)
        {
            this.moduleType = moduleType;
        }

        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && (field.FieldType == FieldType.ShortText || field.FieldType == FieldType.Guid)
                && field.Name != this.moduleType.MainShortTextFieldName;

            return condition;
        }

        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = String.Format(ShortTextFieldGenerationStrategy.fieldMarkupTempalte, field.Name);

            return markup;
        }

        private DynamicModuleType moduleType;
        private const string fieldMarkupTempalte = "@Html.Sitefinity.ShortTextField(Model.Item.{0})";
    }
}
