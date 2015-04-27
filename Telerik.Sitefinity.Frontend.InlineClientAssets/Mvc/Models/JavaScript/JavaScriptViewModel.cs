using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript
{
    public class JavaScriptViewModel
    {
        /// <summary>
        /// Gets or sets the description of the used javascript.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the java script code.
        /// </summary>
        /// <value>The java script code.</value>
        public string JavaScriptCode { get; set; }

        /// <summary>
        /// Gets or sets the position where the javascript will be placed in the document.
        /// </summary>
        /// <value>The position.</value>
        public EmbedPosition Position { get; set; }
    }
}
