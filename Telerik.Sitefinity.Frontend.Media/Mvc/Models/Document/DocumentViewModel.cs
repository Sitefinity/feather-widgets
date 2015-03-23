using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document
{
    /// <summary>
    /// This class represents view model for Document content.
    /// </summary>
    public class DocumentViewModel
    {
        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Determines whether document was selected.
        /// </summary>
        /// <value>The document selected.</value>
        public bool DocumentWasNotFound { get; set; }

        /// <summary>
        /// Gets or sets the URL of the document.
        /// </summary>
        /// <value>The media URL.</value>
        public string MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the title of the document.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the file extension of the document.
        /// </summary>
        /// <value>The extension.</value>
        public string Extension { get; set; }
    }
}
