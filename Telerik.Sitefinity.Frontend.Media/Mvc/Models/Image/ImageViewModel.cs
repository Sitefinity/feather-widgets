using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    /// This class represents view model for Image content.
    /// </summary>
    public class ImageViewModel
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ItemViewModel Item { get; set; }

        /// <summary>
        /// Gets or sets the markup.
        /// </summary>
        /// <value>
        /// The markup.
        /// </value>
        public string Markup { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        /// <value>
        /// The alternative text.
        /// </value>
        public string AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use image as link.
        /// </summary>
        /// <value>
        ///   <c>true</c> if should use image as link; otherwise, <c>false</c>.
        /// </value>
        public bool UseAsLink { get; set; }

        /// <summary>
        /// Gets or sets the page identifier to use as link.
        /// </summary>
        /// <value>
        /// The page identifier to use as link.
        /// </value>
        public Guid PageIdToUseAsLink { get; set; }
    }
}
