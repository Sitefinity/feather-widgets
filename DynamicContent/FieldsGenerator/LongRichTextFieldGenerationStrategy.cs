using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
{
    /// <summary>
    /// This class represents field generation strategy for long rich text dynamic fields.
    /// </summary>
    public class LongRichTextFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.LongText
                && field.WidgetTypeName.EndsWith("HtmlField");

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var longTextMarkup = string.Format(LongRichTextFieldGenerationStrategy.FieldMarkupTempalte, field.Name);

            return longTextMarkup;
        }

        private const string FieldMarkupTempalte = @"@Html.Sitefinity().LongRichTextField((string)Model.Item.{0}, ""{0}"", cssClass: ""sfitemRichText"")";
    }
}
