using System;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.BackendConfigurators
{
    /// <summary>
    /// This class defines required mapping for fieldConfiguration in the backend.
    /// </summary>
    public class FieldConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldConfiguration"/> class.
        /// </summary>
        /// <param name="backendFieldType">Type of the backend field.</param>
        /// <param name="fieldConfigurator">The field configurator.</param>
        public FieldConfiguration(Type backendFieldType, IFieldConfigurator fieldConfigurator)
        {
            this.BackendFieldType = backendFieldType;
            this.FieldConfigurator = fieldConfigurator;
        }

        /// <summary>
        /// Gets or sets the type of the backend field.
        /// </summary>
        /// <value>
        /// The type of the backend field.
        /// </value>
        public Type BackendFieldType { get; set; }

        /// <summary>
        /// Gets or sets the field configurator.
        /// </summary>
        /// <value>
        /// The field configurator.
        /// </value>
        public IFieldConfigurator FieldConfigurator { get; set; }
    }
}
