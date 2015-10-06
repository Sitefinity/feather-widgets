using System;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace Telerik.Sitefinity.Frontend.Card.Mvc.Models.Card
{
    /// <summary>
    /// The view model for the detail page of <see cref="CardController"/>
    /// </summary>
    public class CardViewModel : ContentDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Gets or sets the HTML.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the action name.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the action url.
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// Gets or sets the selected size image URL.
        /// </summary>
        public string SelectedSizeUrl { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        public string CssClass { get; set; }
    }
}
