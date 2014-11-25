using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for document dynamic fields.
    /// </summary>
    /// <remarks>
    /// Used for backward compatibility.
    /// </remarks>
    public class DocumentsField : Field
    {
        /// <inheritdoc/>
        public override bool GetCondition(DynamicModuleField field)
        {
            var condition = base.GetCondition(field)
                && field.FieldType == FieldType.Media
                && field.MediaType == "file";

            return condition;
        }

        /// <inheritdoc/>
        public override string GetMarkup(DynamicModuleField field)
        {
            var markup = string.Format(DocumentsField.FieldMarkupTempalte, field.Name);

            return markup;
        }

        private const string FieldMarkupTempalte = @"@Html.Sitefinity().DocumentField((IEnumerable<ContentLink>)Model.Item.{0}, ""{0}"")";
    }
}
