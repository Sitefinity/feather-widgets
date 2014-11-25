using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for long rich text dynamic fields.
    /// </summary>
    public class LongRichTextField : Field
    {
        /// <inheritdoc/>
        public override bool GetCondition(DynamicModuleField field)
        {
            var condition = base.GetCondition(field)
                && field.FieldType == FieldType.LongText
                && field.WidgetTypeName.EndsWith("HtmlField");

            return condition;
        }

        /// <inheritdoc/>
        public override string GetMarkup(DynamicModuleField field)
        {
            var longTextMarkup = string.Format(LongRichTextField.FieldMarkupTempalte, field.Name);

            return longTextMarkup;
        }

        private const string FieldMarkupTempalte = @"@Html.Sitefinity().LongRichTextField((string)Model.Item.{0}, ""{0}"", cssClass: ""sfitemRichText"")";
    }
}
