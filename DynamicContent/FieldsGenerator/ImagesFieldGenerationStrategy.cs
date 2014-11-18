using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
{
    /// <summary>
    /// This class represents field generation strategy for image dynamic fields.
    /// </summary>
    /// <remarks>
    /// Used for backward compatibility.
    /// </remarks>
    public class ImagesFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.Media
                && field.MediaType == "image";

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = string.Empty;
            if (field.AllowMultipleImages)
            {
                markup = string.Format(ImagesFieldGenerationStrategy.MultiImageFieldMarkupTempalte, field.Name, field.Title);
            }
            else
            {
                markup = string.Format(ImagesFieldGenerationStrategy.SingleImageFieldMarkupTempalte, field.Name, field.Title);
            }

            return markup;
        }

        private const string SingleImageFieldMarkupTempalte = @"@Html.Sitefinity().ImageField(((IEnumerable<ContentLink>)Model.Item.{0}).FirstOrDefault(), ""{0}"", ""{1}"")";
        private const string MultiImageFieldMarkupTempalte = @"@Html.Sitefinity().ImageField((IEnumerable<ContentLink>)Model.Item.{0}, ""{0}"", ""{1}"")";
    }
}
