using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.EmbedCode
{
    /// <summary>
    /// Interface that defines the model of the EmbedCodetModel widget.
    /// </summary>
    public interface IEmbedCodeModel
    {
        /// <summary>
        /// Gets or sets the embed code entered by the user which will be inlined in the page.
        /// </summary>
        /// <value>The inline code.</value>
        string InlineCode { get; set; }

        /// <summary>
        /// Gets or sets the description of the used code.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }

        /// <summary>
        /// Gets the view model of the current model that will be displayed by the view.
        /// </summary>
        /// <returns></returns>
        EmbedCodeViewModel GetViewModel();
    }
}
