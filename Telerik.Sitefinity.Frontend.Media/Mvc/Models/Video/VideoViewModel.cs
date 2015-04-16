using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Video
{
    /// <summary>
    /// Represent a DTO that has information regarding <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item. 
    /// </summary>
    public class VideoViewModel
    {
        /// <summary>
        /// Gets or sets the <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ItemViewModel Item { get; set; }

        /// <summary>
        /// Gets or sets the applied css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets whether there is <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item. 
        /// </summary>
        /// <value><c>true</c>if there is selected  <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item, otherwise <c>false</c></value>
        public bool HasSelectedVideo { get; set; }
        
        /// <summary>
        /// Gets or sets the width of the <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the ascpect ration of the selected <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>
        /// The aspect ratio.
        /// </value>
        public string AspectRatio { get; set; }

    }
}
