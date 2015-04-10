using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Video
{
    /// <summary>
    /// Represent a DTO that has information regarding <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item. 
    /// </summary>
    public class VideoViewModel
    {
        /// <summary>
        /// Gets or sets the applied css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the file extension of the <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>The extension.</value>
        public string Extension { get; set; }

        /// <summary>
        /// Gets or sets the size of the <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> file in KB.
        /// </summary>
        /// <value>The size of the file.</value>
        public long FileSize { get; set; }

        /// <summary>
        /// Gets or sets whether there is <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item. 
        /// </summary>
        /// <value><c>true</c>if there is selected  <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item, otherwise <c>false</c></value>
        public bool HasSelectedVideo { get; set; }

        /// <summary>
        /// Gets or sets the URL of the <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>The media URL.</value>
        public string MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the title of the <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
    }
}
