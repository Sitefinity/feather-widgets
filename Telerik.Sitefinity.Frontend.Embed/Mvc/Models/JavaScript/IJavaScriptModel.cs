using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript
{
    /// <summary>
    /// Interface that defines the model of the JavaScript widget.
    /// </summary>
    public interface IJavaScriptModel
    {
        /// <summary>
        /// Gets or sets the javascript code entered by the user.
        /// </summary>
        /// <value>The cusotm code.</value>
        string CustomCode { get; set; }

        /// <summary>
        /// Gets or sets the URL of the file where the javascript is stored.
        /// </summary>
        /// <value>The file URL.</value>
        string FileUrl { get; set; }

        /// <summary>
        /// Gets or sets the position in the document where the code will be inserted.
        /// </summary>
        /// <value>The embed position.</value>
        ScriptEmbedPosition EmbedPosition { get; set; }

        /// <summary>
        /// Gets or sets the description of the used code.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }

        /// <summary>
        /// Gets the view model of the current model that will be displayed by the view.
        /// </summary>
        /// <returns></returns>
        JavaScriptViewModel GetViewModel();
    }
}
