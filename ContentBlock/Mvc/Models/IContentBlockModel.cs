using System;

using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Models
{
    /// <summary>
    /// This interface is used as a model for the ContentBlockController.
    /// </summary>
    public interface IContentBlockModel : ICacheDependable
    {
        /// <summary>
        /// Gets or sets the HTML.
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable social sharing].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable social sharing]; otherwise, <c>false</c>.
        /// </value>
        bool EnableSocialSharing { get; set; }

        /// <summary>
        /// Gets or sets the ID of the ContentBlockItem if the HTML is shared across multiple controls
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
        Guid SharedContentID { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        string ProviderName { get; set; }

        /// <summary>
        /// Gets the content manager.
        /// </summary>
        /// <value>
        /// The content manager.
        /// </value>
        ContentManager ContentManager { get; }

        /// <summary>
        /// Gets or sets the type of the content. If shared it should be ContentItem otherwise PageDraftControl
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        string ContentType { get; set; }

        /// <summary>
        /// Creates the blank data item.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object CreateBlankDataItem();
    }
}