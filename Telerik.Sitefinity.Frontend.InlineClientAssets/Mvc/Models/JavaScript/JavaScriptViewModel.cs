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
        /// Gets or sets the JavaScript code that will be embedded in the page.
        /// </summary>
        /// <value>The java script code.</value>
        public string JavaScriptCode { get; set; }

        /// <summary>
        /// Gets or sets the summary (the first two lines) of the JavaScript code.
        /// </summary>
        /// <value>The code summary.</value>
        public string CodeSummary { get; set; }

        /// <summary>
        /// Gets or sets the position where the javascript will be placed in the document.
        /// </summary>
        /// <value>The position.</value>
        public EmbedPosition Position { get; set; }
    }
}
