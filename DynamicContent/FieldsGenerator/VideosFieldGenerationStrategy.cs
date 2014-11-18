using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
{
    /// <summary>
    /// This class represents field generation strategy for video dynamic fields.
    /// </summary>
    /// <remarks>
    /// Used for backward compatibility.
    /// </remarks>
    public class VideosFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.Media
                && field.MediaType == "video";

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = string.Format(VideosFieldGenerationStrategy.FieldMarkupTempalte, field.Name, field.Title);

            return markup;
        }

        private const string FieldMarkupTempalte = @"@Html.Sitefinity().VideoField((IEnumerable<ContentLink>)Model.Item.{0}, ""{0}"", ""{1}"")";
    }
}
