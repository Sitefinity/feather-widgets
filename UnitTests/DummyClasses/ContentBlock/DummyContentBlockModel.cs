using ContentBlock.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.DummyClasses.ContentBlock
{
    public class DummyContentBlockModel : ContentBlockModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummyContentBlockModel" /> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="content">The content.</param>
        /// <param name="enableSocialSharing">The enable social sharing.</param>
        /// <param name="sharedContentId">The shared content id.</param>
        public DummyContentBlockModel(string providerName, string content, bool enableSocialSharing, Guid sharedContentId) : base()
        {
            this.ProviderName = providerName;
            this.Content = content;
            this.EnableSocialSharing = enableSocialSharing;
            this.SharedContentID = sharedContentId;
        }
    }
}
