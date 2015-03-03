
namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration
{
    /// <summary>
    /// This class is used for serializing property binding configurations of a single user profile type.
    /// </summary>
    public class ProfileBindingsModel
    {
        /// <summary>
        /// Gets or sets the full type name of the profile.
        /// </summary>
        /// <value>
        /// The full type name of the profile.
        /// </value>
        public string ProfileType { get; set; }

        /// <summary>
        /// Gets or sets the property bindings.
        /// </summary>
        /// <value>
        /// The property bindings.
        /// </value>
        public PropertyBindingModel[] Properties { get; set; }

        /// <summary>
        /// This class is used for serializing the binding of a single user profile property.
        /// </summary>
        public class PropertyBindingModel
        {
            /// <summary>
            /// Gets or sets the name of the property as it appears in the view model.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the field name as it appears in the profile type.
            /// </summary>
            /// <value>
            /// The field name.
            /// </value>
            public string FieldName { get; set; }
        }
    }
}
