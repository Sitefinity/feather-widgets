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
    public class DocumentsFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.Media
                && field.MediaType == "file";

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = string.Format(DocumentsFieldGenerationStrategy.FieldMarkupTempalte, field.Name);

            return markup;
        }

        private const string FieldMarkupTempalte = @"@Html.Sitefinity().DocumentField((IEnumerable<ContentLink>)Model.Item.{0}, ""{0}"")";
    }
}
