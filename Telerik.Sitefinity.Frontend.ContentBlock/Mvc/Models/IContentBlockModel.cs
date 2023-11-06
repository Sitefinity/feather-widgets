using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Newsletters.Composition;

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
        /// Gets or sets a value indicating whether to enable social sharing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if should enable social sharing; otherwise, <c>false</c>.
        /// </value>
        [Obsolete("Social sharing module has been removed. This property is no longer used.")]
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
        /// Gets or sets the type of the container.
        /// </summary>
        /// <value>
        /// The type of the container.
        /// </value>
        [Browsable(false)]
        Type ContainerType { get; set; }

        /// <summary>
        /// Creates the blank data item.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object CreateBlankDataItem();

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper tag of the widget view.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        [Category("Advanced")]
        string WrapperCssClass { get; set; }
    }
}