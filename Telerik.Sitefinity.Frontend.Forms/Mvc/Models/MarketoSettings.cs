
namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models
{
    /// <summary>
    /// This class represents the view model of the Marketo settings.
    /// </summary>
    public class MarketoSettings
    {
        /// <summary>
        /// Gets or sets value indicating whether to sync form fields to lead fields.
        /// </summary>
        public bool SyncFormFieldsToLeadFields { get; set; }

        /// <summary>
        /// Gets or sets value indicating whether to do specific web calls.
        /// </summary>
        public bool DoSpecificWebCalls { get; set; }
    }
}
