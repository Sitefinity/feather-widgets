using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.ImageGallery
{
    /// <summary>
    /// This class represents view model for Image gallery content.
    /// </summary>
    public class ImageDetailsViewModel : ContentDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the previous item.
        /// </summary>
        /// <value>The previous item.</value>
        public ItemViewModel PreviousItem { get; set; }

        /// <summary>
        /// Gets or sets the next item.
        /// </summary>
        /// <value>The next item.</value>
        public ItemViewModel NextItem { get; set; }

        /// <summary>
        /// Gets or sets the total items count.
        /// </summary>
        /// <value>The total items count.</value>
        public int TotalItemsCount { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image.
        /// </summary>
        /// <value>The thumbnail URL.</value>
        public string MediaUrl { get; set; }
    }
}
