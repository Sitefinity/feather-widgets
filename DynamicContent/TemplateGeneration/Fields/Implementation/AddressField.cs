using DynamicContent.TemplateGeneration.Fields.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for address dynamic fields.
    /// </summary>
    public class AddressField : Field
    {
        /// <inheritdoc/>
        public override bool GetCondition(DynamicModuleField field)
        {
            var condition = base.GetCondition(field)
                && field.FieldType == FieldType.Address;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetMarkup(DynamicModuleField field)
        {
            var fieldContent = new AddressFieldTemplate();
            fieldContent.FieldData = field;
            var markup = fieldContent.TransformText();

            return markup;
        }
    }
}
