using System;
using System.Collections.Generic;
using System.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.InlineEditing.Attributes;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Models
{
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
        /// <param name="sharedContentId">
        /// The shared content identifier.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContentBlockModel(string providerName, string content, Guid sharedContentId) :
            this(providerName, content, sharedContentId, null)
        {   
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockModel"/> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="content">The content.</param>
        /// <param name="sharedContentId">The shared content identifier.</param>
        /// <param name="containerType">Type of the container.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContentBlockModel(string providerName, string content, Guid sharedContentId, Type containerType) :
            this(providerName, content, sharedContentId, containerType, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockModel"/> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="content">The content.</param>
        /// <param name="sharedContentId">The shared content identifier.</param>
        /// <param name="containerType">Type of the container.</param>
        /// <param name="wrapperCssClass">The CSS class that will be applied on the wrapper tag of the widget view.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContentBlockModel(string providerName, string content, Guid sharedContentId, Type containerType, string wrapperCssClass)
        {
            this.ContainerType = containerType;
            this.ProviderName = providerName;
            this.SharedContentID = sharedContentId;
            this.WrapperCssClass = wrapperCssClass;

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
        public Type ContainerType { get; set; }

        /// <inheritdoc />
        [Obsolete("Social sharing module has been removed. This property is no longer used.")]
        public bool EnableSocialSharing { get; set; }

        /// <inheritdoc />
        public string ProviderName { get; set; }

        /// <inheritdoc />
        public Guid SharedContentID { get; set; }

        /// <inheritdoc />
        public string WrapperCssClass { get; set; }

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
                result.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(typeof(ContentItem), this.SharedContentID));
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected virtual string GetContentHtmlValue(string content)
        {
            try
            {
                if (this.IsShared())
                {
                    content = string.Empty;
                    this.ContentType = typeof(ContentItem).FullName;
                    ContentItem sharedContent = this.ContentManager.GetContent(this.SharedContentID);

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
                    bool isEditable = false;
                    if (this.ContainerType != null)
                        isEditable = this.ContainerType.GetCustomAttributes(typeof(EditableControlsContainerAttribute), false).Length > 0;

                    if (isEditable)
                        this.ContentType = typeof(PageDraftControl).FullName;
                }
            }
            catch (ItemNotFoundException)
            {
                this.SharedContentID = Guid.Empty;
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
            return this.SharedContentID != Guid.Empty;
        }

        #endregion

        #region Fields

        private ContentManager contentManager;

        #endregion
    }
}