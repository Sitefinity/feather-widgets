using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields
{
    /// <summary>
    /// This class represents strategy for determining the markup needed depending on different <see cref="DynamicModuleField"/>.
    /// </summary>
    /// <remarks>
    /// This markup will be added to the automatically generated templates for dynamic widget.
    /// </remarks>
    public abstract class FieldGenerationStrategy : IFieldGenerationStrategy
    {
        /// <summary>
        /// Gets value determining whether current strategy should handle the markup for the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public virtual bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = field.FieldStatus != DynamicModuleFieldStatus.Removed && !field.IsHiddenField;

            return condition;
        }

        /// <summary>
        /// Gets the field markup.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public abstract string GetFieldMarkup(DynamicModuleField field);
    }
}
