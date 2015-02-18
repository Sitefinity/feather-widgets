using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    /// This class represents view model for Image content.
    /// </summary>
    public class ImageViewModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public Lstring Title { get; set; }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        /// <value>
        /// The alternative text.
        /// </value>
        public Lstring AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public ItemViewModel Item { get; set; }
    }
}
