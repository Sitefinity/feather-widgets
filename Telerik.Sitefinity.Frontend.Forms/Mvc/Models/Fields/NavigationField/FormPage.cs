using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.NavigationField
{
    /// <summary>
    /// Represents a page in multi-page form
    /// </summary>
    [DataContract]
    public class FormPage
    {
        /// <summary>
        /// Gets or sets the previous page break field id
        /// </summary>
        [DataMember]
        public string PreviousPageBreakId { get; set; }

        /// <summary>
        /// Gets or sets the title of the form page
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets index of current page
        /// </summary>
        [DataMember]
        public int Index { get; set; }
    }
}