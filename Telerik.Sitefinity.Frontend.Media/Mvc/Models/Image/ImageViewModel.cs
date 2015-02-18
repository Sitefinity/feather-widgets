using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public ImageViewModel()
        {
            this.Item = new ItemViewModel(new Telerik.Sitefinity.Libraries.Model.Image());
        }

        public Guid Id { get; set; }

        public string Markup { get; set; }

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
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ItemViewModel Item { get; set; }
    }
}
