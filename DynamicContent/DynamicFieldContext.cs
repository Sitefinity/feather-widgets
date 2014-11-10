using DynamicContent.FieldsGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent
{
    /// <summary>
    /// This class represents the context for registering <see cref="FieldGenerationStrategy"/> for different field types.
    /// </summary>
    public class DynamicFieldContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicFieldContext"/> class.
        /// </summary>
        /// <param name="fieldType">Type of the field.</param>
        /// <param name="fieldGenerationStrategy">The field generation strategy.</param>
        public DynamicFieldContext(string fieldType, FieldGenerationStrategy fieldGenerationStrategy)
        {
            this.FieldType = fieldType;
            this.FieldGenerationStrategy = fieldGenerationStrategy;
        }

        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        /// <value>
        /// The type of the field.
        /// </value>
        public string FieldType { get; set; }

        /// <summary>
        /// Gets or sets the field generation strategy.
        /// </summary>
        /// <value>
        /// The field generation strategy.
        /// </value>
        public FieldGenerationStrategy FieldGenerationStrategy { get; set; }
    }
}
