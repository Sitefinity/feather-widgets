using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Resources;

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
            return Field.templateProcessor.Run(templatePath, field);
        }

        /// <summary>
        /// Gets the template path.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        protected abstract string GetTemplatePath(DynamicModuleField field);

        private static readonly RazorTemplateProcessor templateProcessor = new RazorTemplateProcessor();
    }
}
