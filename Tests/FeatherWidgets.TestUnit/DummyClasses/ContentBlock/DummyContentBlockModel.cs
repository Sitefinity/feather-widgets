using System;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Models;

namespace FeatherWidgets.TestUnit.DummyClasses.ContentBlock
{
    /// <summary>
    /// The dummy content block model.
    /// </summary>
    public class DummyContentBlockModel : ContentBlockModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummyContentBlockModel"/> class.
        /// </summary>
        public DummyContentBlockModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyContentBlockModel"/> class.
        /// </summary>
        /// <param name="providerName">
        /// Name of the provider.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="enableSocialSharing">
        /// The enable social sharing.
        /// </param>
        /// <param name="sharedContentId">
        /// The shared content id.
        /// </param>
        public DummyContentBlockModel(string providerName, string content, bool enableSocialSharing, Guid sharedContentId)
            : base()
        {
            this.ProviderName = providerName;
            this.Content = content;
            this.EnableSocialSharing = enableSocialSharing;
            this.SharedContentID = sharedContentId;
        }

        /// <summary>
        /// Check if the model is shared.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool PublicIsShared()
        {
            return this.IsShared();
        }

        /// <summary>
        /// Creates the blank data item.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public override object CreateBlankDataItem()
        {
            var dummyContent = new DummyContentItem { Content = "DummyContent" };
            return dummyContent;
        }
    }
}
