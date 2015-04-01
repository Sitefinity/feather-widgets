using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.DocumentsList
{
    /// <summary>
    /// This class represents view model for document item.
    /// </summary>
    public class DocumentItemViewModel : ItemViewModel
    {
        /// <inheritdoc />
        public DocumentItemViewModel(IDataItem item)
            : base(item)
        {
        }

        /// <summary>
        /// Gets or sets the file extension of the document.
        /// </summary>
        /// <value>The extension.</value>
        public string Extension { get; set; }
    }
}
