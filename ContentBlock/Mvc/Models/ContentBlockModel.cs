namespace ContentBlock.Mvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    using Telerik.Sitefinity.ContentLocations;
    using Telerik.Sitefinity.Data;
    using Telerik.Sitefinity.Frontend.InlineEditing.Attributes;
    using Telerik.Sitefinity.GenericContent.Model;
    using Telerik.Sitefinity.Modules.GenericContent;
    using Telerik.Sitefinity.Pages.Model;
    using Telerik.Sitefinity.Services;
    using Telerik.Sitefinity.SitefinityExceptions;
    using Telerik.Sitefinity.Web.Utilities;

    /// <summary>
    ///     This class is used as a model for the content block controller.
    /// </summary>
    public class ContentBlockModel : IContentBlockModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockModel"/> class.
        /// </summary>
        /// <param name="providerName">
        /// Name of the provider.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="enableSocialSharing">
        /// if set to <c>true</c> [enable social sharing].
        /// </param>
        /// <param name="sharedContentId">
        /// The shared content identifier.
        /// </param>
        public ContentBlockModel(string providerName, string content, bool enableSocialSharing, Guid sharedContentId)
        {
            this.ProviderName = providerName;
            this.EnableSocialSharing = enableSocialSharing;
            this.SharedContentId = sharedContentId;

            content = this.GetContentHtmlValue(content);

            this.Content = LinkParser.ResolveLinks(
                content, 
                DynamicLinksParser.GetContentUrl, 
                null, 
                SystemManager.IsInlineEditingMode);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContentBlockModel" /> class.
        ///     This parameterless constructor is used for testing purposes.
        /// </summary>
        protected ContentBlockModel()
        {
        }

        #endregion

        #region Public Properties

        /// <inheritdoc />
        [DynamicLinksContainer]
        [FieldInfo("Content", "LongText")]
        public string Content { get; set; }

        /// <inheritdoc />
        public virtual ContentManager ContentManager
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

        /// <inheritdoc />
        public string ContentType { get; set; }

        /// <inheritdoc />
        public bool EnableSocialSharing { get; set; }

        /// <inheritdoc />
        public string ProviderName { get; set; }

        /// <inheritdoc />
        public Guid SharedContentId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <inheritdoc />
        public virtual object CreateBlankDataItem()
        {
            ContentItem item;
            using (new ElevatedModeRegion(this.ContentManager))
            {
                item = this.ContentManager.CreateContent(Guid.Empty);
            }

            return item;
        }

        /// <summary>
        /// Gets a collection of <see cref="CacheDependencyNotifiedObject"/>.
        ///     The <see cref="CacheDependencyNotifiedObject"/> represents a key for which cached items could be subscribed for
        ///     notification.
        ///     When notified, all cached objects with dependency on the provided keys will expire.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public virtual IList<CacheDependencyKey> GetKeysOfDependentObjects()
        {
            var result = new List<CacheDependencyKey>(1);
            if (this.IsShared())
            {
                result.Add(new CacheDependencyKey { Key = this.SharedContentId.ToString(), Type = typeof(ContentItem) });
            }

            return result;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the content HTML value from a shared content item if such is available.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected virtual string GetContentHtmlValue(string content)
        {
            try
            {
                if (this.IsShared())
                {
                    content = string.Empty;
                    this.ContentType = typeof(ContentItem).FullName;
                    ContentItem sharedContent = this.ContentManager.GetContent(this.SharedContentId);

                    object tempItem;
                    ContentLocatableViewExtensions.TryGetItemWithRequestedStatus(
                        sharedContent, 
                        this.ContentManager, 
                        out tempItem);
                    sharedContent = (ContentItem)tempItem;
                    content = sharedContent.Content;
                }
                else
                {
                    this.ContentType = typeof(PageDraftControl).FullName;
                }
            }
            catch (ItemNotFoundException)
            {
                this.SharedContentId = Guid.Empty;
            }
            catch (Exception)
            {
            }

            return content;
        }

        /// <summary>
        /// Determines whether this content block is shared.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected virtual bool IsShared()
        {
            return this.SharedContentId != Guid.Empty;
        }

        #endregion

        #region Fields

        private ContentManager contentManager;

        #endregion
    }
}