using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.TemplateGeneration.Fields
{
    /// <summary>
    /// Class that implement this interface represent strategy for determining the markup needed depending on different <see cref="DynamicModuleField"/>.
    /// </summary>
    public interface IField
    {
        /// <summary>
        /// Gets value determining whether current strategy should handle the markup for the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        bool GetCondition(DynamicModuleField field);

        /// <summary>
        /// Gets the field markup.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        string GetMarkup(DynamicModuleField field);
    }
}
