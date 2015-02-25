using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    /// This class represent the model of a thumbnail.
    /// </summary>
    public class ThumbnailModel
    {
        /// <summary>
        /// Gets or sets the thumbnail profile's name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the generated URL for this thumbnial profile.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }
    }
}
