using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for long text area dynamic fields.
    /// </summary>
    public class LongTextAreaFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.LongText
                && field.WidgetTypeName.EndsWith("TextField");

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var longTextMarkup = string.Format(LongTextAreaFieldGenerationStrategy.FieldMarkupTempalte, field.Name);

            return longTextMarkup;
        }

        private const string FieldMarkupTempalte = @"@Html.Sitefinity().LongTextAreaField((string)Model.Item.{0}, ""{0}"", cssClass: ""sfitemLongText"")";
    }
}
