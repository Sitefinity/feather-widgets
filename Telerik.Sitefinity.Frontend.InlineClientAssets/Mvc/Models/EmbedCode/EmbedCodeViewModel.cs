using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.EmbedCode
{
    /// <summary>
    /// This class represents the view model of the EmbedCode widget
    /// </summary>
    public class EmbedCodeViewModel
    {
        /// <summary>
        /// Gets or sets the EmbedCode that will be embedded in the page.
        /// </summary>
        /// <value>The java script code.</value>
        public string EmbedCode { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
    }
}
