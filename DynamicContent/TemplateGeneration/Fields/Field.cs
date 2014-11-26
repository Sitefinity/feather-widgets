using DynamicContent.TemplateGeneration.Fields.Templates;
using System.IO;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields
{
    /// <summary>
    /// This class represents strategy for determining the markup needed depending on different <see cref="DynamicModuleField"/>.
    /// </summary>
    /// <remarks>
    /// This markup will be added to the automatically generated templates for dynamic widget.
    /// </remarks>
    public abstract class Field
    {
        /// <summary>
        /// Gets or sets the field template.
        /// </summary>
        /// <value>
        /// The field template.
        /// </value>
        public FieldTemplate FieldTemplate { get; set; }

        /// <summary>
        /// Gets value determining whether current strategy should handle the markup for the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public virtual bool GetCondition(DynamicModuleField field)
        {
            var condition = field.FieldStatus != DynamicModuleFieldStatus.Removed && !field.IsHiddenField;

            return condition;
        }

        /// <summary>
        /// Gets the field markup.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public virtual string GetMarkup(DynamicModuleField field)
        {
            var templatePath = this.GetTemplatePath(field);
            var markup = this.GetDefaultTemplate(templatePath);
            var parsedMarkup = RazorEngine.Razor.Parse(markup, field); 

            return parsedMarkup;
        }

        /// <summary>
        /// Gets the template path.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        protected abstract string GetTemplatePath(DynamicModuleField field);

        /// <summary>
        /// Gets the default template.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        protected string GetDefaultTemplate(string path)
        {
            var templateText = string.Empty;

            if (VirtualPathManager.FileExists(path))
            {
                var fileStream = VirtualPathManager.OpenFile(path);

                using (var streamReader = new StreamReader(fileStream))
                {
                    templateText = streamReader.ReadToEnd();
                }
            }

            return templateText;
        }
    }
}
