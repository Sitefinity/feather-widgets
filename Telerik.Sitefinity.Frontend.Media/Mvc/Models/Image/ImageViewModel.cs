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
        /// Initializes a new instance of the <see cref="ImageViewModel"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public ImageViewModel(Guid id, string providerName)
        {
            SfImage image;
            if (id != Guid.Empty)
            {
                LibrariesManager librariesManager = LibrariesManager.GetManager(providerName);
                image = librariesManager.GetImages().Where(i => i.Id == id).FirstOrDefault();
            }
            else
            {
                image = new SfImage();
            }

            this.Item = new ItemViewModel(image);
        }

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
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }
    }
}
