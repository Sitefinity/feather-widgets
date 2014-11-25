using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for multiple choice dynamic fields.
    /// </summary>
    public class MultipleChoiceField : Field
    {
        /// <inheritdoc/>
        public override bool GetCondition(DynamicModuleField field)
        {
            var condition = base.GetCondition(field)
                && (field.FieldType == FieldType.MultipleChoice || field.FieldType == FieldType.Choices);

            return condition;
        }

        /// <inheritdoc/>
        public override string GetMarkup(DynamicModuleField field)
        {
            var markup = string.Empty;

            if (field.ChoiceRenderType.ToLowerInvariant() == "checkbox")
            {
                markup = string.Format(MultipleChoiceField.FieldMarkupMultipleChoiceTempalte, field.Name, field.Title);
            }
            else
            {
                markup = string.Format(MultipleChoiceField.FieldMarkupSingleChoiceTempalte, field.Name, field.Title);
            }

            return markup;
        }

        private const string FieldMarkupMultipleChoiceTempalte = @"@Html.Sitefinity().ChoiceField((IEnumerable)Model.Item.{0}, ""{0}"", fieldTitle: ""{1}"", cssClass: ""sfitemChoices"")";
        private const string FieldMarkupSingleChoiceTempalte = @"@Html.Sitefinity().ChoiceField((string)Model.Item.{0}, ""{0}"", fieldTitle: ""{1}"", cssClass: ""sfitemChoices"")";
    }
}
