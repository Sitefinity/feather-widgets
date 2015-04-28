using System;
using System.Linq;
using System.Web.UI;

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
        EmbedPosition Position { get; set; }

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

        /// <summary>
        /// Builds the script tag with url pointing to the script file or containig the script in its content.
        /// </summary>
        /// <returns></returns>
        string BuildScriptTag();

        /// <summary>
        /// Places the script before the end of the body.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="script">The script.</param>
        void PlaceScriptBeforeBodyEnd(Page page, string script);
    }
}
