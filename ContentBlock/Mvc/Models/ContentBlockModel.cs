using System;
using System.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.InlineEditing.Attributes;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.Utilities;

namespace ContentBlock.Mvc.Models
{
    /// <summary>
    /// This class is used as a model for the content block controller
    /// </summary>
    public class ContentBlockModel : IContentBlockModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockModel"/> class.
        /// This parameterless constructor is used for testing purposes
        /// </summary>
        protected ContentBlockModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockModel"/> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="content">The content.</param>
        /// <param name="enableSocialSharing">if set to <c>true</c> [enable social sharing].</param>
        /// <param name="sharedContentId">The shared content identifier.</param>
        public ContentBlockModel(string providerName, string content, bool enableSocialSharing, Guid sharedContentId)
        {
            this.ProviderName = providerName;
            this.EnableSocialSharing = enableSocialSharing;
            this.SharedContentID = sharedContentId;

            string htmlContent;
            if (this.TryGetContentHtmlValue(out htmlContent))
                content = htmlContent;

            this.Content = LinkParser.ResolveLinks(content, DynamicLinksParser.GetContentUrl, null,
                SystemManager.IsInlineEditingMode);

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the HTML.
        /// </summary>
        [DynamicLinksContainer]
        [FieldInfo("Content", "LongText")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the current mode of the control.
        /// </summary>
        public bool EnableSocialSharing { get; set; }

        /// <summary>
        /// Gets or sets the ID of the ContentBlockItem if the HTML is shared across multiple controls
        /// </summary>
        public Guid SharedContentID { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets the content manager.
        /// </summary>
        /// <value>
        /// The content manager.
        /// </value>
        protected virtual ContentManager ContentManager
        {
            get
            {
                if (this.contentManager == null)
                {
                    try
                    {
                        this.contentManager = ContentManager.GetManager(this.ProviderName);
                    }
                    catch (ConfigurationErrorsException)
                    {
                        return null;
                    }
                }
                return this.contentManager;
            }
        }

        /// <summary>
        /// Gets or sets the type of the content. If shared it should be ContentItem otherwise PageDraftControl
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Creates the blank data item.
        /// </summary>
        /// <returns></returns>
        public virtual object CreateBlankDataItem()
        {
            ContentItem item;
            using (new ElevatedModeRegion(this.ContentManager))
            {
                item = this.ContentManager.CreateContent(Guid.Empty);
            }
            return item;

        }

        #endregion

        #region Protected members


        /// <summary>
        /// Determines whether this content block is shared.
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsShared()
        {
            return this.SharedContentID != Guid.Empty && this.ContentManager != null;
        }

        /// <summary>
        /// Gets the content HTML value from a shared content item if such is available
        /// </summary>
        /// <returns></returns>
        protected virtual bool TryGetContentHtmlValue(out string content)
        {
            content = string.Empty;
            bool isContentItemAvailable = false;
            try
            {
                if (this.IsShared())
                {
                    this.ContentType = typeof(ContentItem).FullName;
                    var sharedContent = this.ContentManager.GetContent(this.SharedContentID);
                    object tempItem;
                    if (Telerik.Sitefinity.ContentLocations.ContentLocatableViewExtensions.TryGetItemWithRequestedStatus(sharedContent, this.ContentManager, out tempItem))
                    {
                        sharedContent = tempItem as ContentItem;
                        isContentItemAvailable = true;
                    }
                    content = sharedContent.Content;
                }
                else
                {
                    this.ContentType = typeof(Telerik.Sitefinity.Pages.Model.PageDraftControl).FullName;
                }
            }
            catch (ItemNotFoundException ex)
            {
                this.SharedContentID = Guid.Empty;
            }
            catch (Exception ex)
            {
            }
            return isContentItemAvailable;
        }

        #endregion

        #region Private fields

        private ContentManager contentManager;

        #endregion
    }
}
