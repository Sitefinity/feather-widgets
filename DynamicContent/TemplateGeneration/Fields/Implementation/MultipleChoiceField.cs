using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.DynamicContent.TemplateGeneration.Fields.Implementation
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
        protected override string GetTemplatePath(DynamicModuleField field)
        {
            var path = string.Empty;

            if (field.ChoiceRenderType.ToLowerInvariant() == "checkbox")
            {
                path = string.Format(MultipleChoiceField.MultiChoiceTemplatePath, field.Name, field.Title);
            }
            else
            {
                path = string.Format(MultipleChoiceField.SingleChoiceTemplatePath, field.Name, field.Title);
            }

            return path;
        }

        private const string MultiChoiceTemplatePath = "~/Frontend-Assembly/DynamicContent/TemplateGeneration/Fields/Templates/MultiChoiceField.cshtml";
        private const string SingleChoiceTemplatePath = "~/Frontend-Assembly/DynamicContent/TemplateGeneration/Fields/Templates/SingleChoiceField.cshtml";
    }
}
