using System.Collections.Generic;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.NavigationField
{
    /// <summary>
    /// This interface provides API for form navigation fields.
    /// </summary>
    public interface INavigationFieldModel : IFormElementModel
    {
        /// <summary>
        /// Gets or sets the serialized pages.
        /// </summary>
        /// <value>
        /// The serialized pages.
        /// </value>
        string SerializedPages { get; set; }
        
        /// <summary>
        /// De-serializes the pages.
        /// </summary>
        /// <returns>De-serialized pages.</returns>
        IEnumerable<FormPage> DeserializePages();
    }
}
