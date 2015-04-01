using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.DocumentsList
{
    /// <summary>
    /// This class represents view model for document content.
    /// </summary>
    public class DocumentDetailsViewModel : ContentDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the file extension of the document.
        /// </summary>
        /// <value>The extension.</value>
        public string Extension { get; set; }
    }
}
