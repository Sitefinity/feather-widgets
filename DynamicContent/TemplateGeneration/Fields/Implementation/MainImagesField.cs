using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class generates template markup for main image field.
    /// </summary>
    /// <remarks>
    /// Used for backward compatibility.
    /// </remarks>
    public class MainImagesField : Field
    {
        /// <inheritdoc/>
        protected override string GetTemplatePath(DynamicModuleField field)
        {
            var path = string.Empty;
            if (field.AllowMultipleImages)
            {
                path = string.Format(MainImagesField.MultiImageTemplatePath, field.Name);
            }
            else
            {
                path = string.Format(MainImagesField.SingleImageTemplatePath, field.Name);
            }

            return path;
        }

        private const string SingleImageTemplatePath = "~/Frontend-Assembly/DynamicContent/TemplateGeneration/Fields/Templates/MainImageField.cshtml";
        private const string MultiImageTemplatePath = "~/Frontend-Assembly/DynamicContent/TemplateGeneration/Fields/Templates/MainMultiImageField.cshtml";
    }
}
