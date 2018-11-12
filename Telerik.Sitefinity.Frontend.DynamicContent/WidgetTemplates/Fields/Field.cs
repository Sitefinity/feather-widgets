using System.Collections.Generic;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Resources;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Frontend.DynamicContent.WidgetTemplates.Fields
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
        /// Gets value determining whether current strategy should handle the markup for the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public virtual bool GetCondition(DynamicModuleField field)
        {
            var condition = field.FieldStatus != DynamicModuleFieldStatus.Removed && !field.IsHiddenField
                && !this.ExcludedFieldNames.Contains(field.Name);

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
            return Field.TemplateProcessor.Run(templatePath, field);
        }

        /// <summary>
        /// Gets the template path.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        protected abstract string GetTemplatePath(DynamicModuleField field);

        /// <summary>
        /// The excluded field names
        /// </summary>
        protected IList<string> ExcludedFieldNames
        {
            get
            {
                return new List<string>()
                {
                    PageHelper.MetaDataProperties.MetaTitle,
                    PageHelper.MetaDataProperties.MetaDescription,
                    PageHelper.MetaDataProperties.OpenGraphTitle,
                    PageHelper.MetaDataProperties.OpenGraphDescription,
                    PageHelper.MetaDataProperties.OpenGraphImage,
                    PageHelper.MetaDataProperties.OpenGraphVideo
                };
            }
        }

        private static readonly RazorTemplateProcessor TemplateProcessor = new RazorTemplateProcessor();
    }
}
