using ContentBlock.Mvc.StringResources;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Frontend.InlineEditing.Attributes;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;

namespace ContentBlock.Mvc.Models
{
    public class ContentBlockModel
    {
        /// <summary>
        /// Gets or sets the html.
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
        public Guid SharedContentID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        public string ProviderName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the content manager.
        /// </summary>
        /// <value>
        /// The content manager.
        /// </value>
        public ContentManager ContentManager
        {
            get
            {
                if (this.contentManager == null)
                {
                    this.contentManager = this.InitializeManager();
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

        /// <summary>
        /// The is content loaded correctly
        /// </summary>
        internal bool isContentLoadedCorrectly = true;

        /// <summary>
        /// Gets the content HTML value depending whether it is shared.
        /// </summary>
        /// <returns></returns>
        public string GetContentHtmlValue()
        {
            if (this.IsShared())
            {
                this.ContentType = typeof(ContentItem).FullName;
                var exMessage = string.Empty;
                try
                {
                    var sharedContent = this.ContentManager.GetContent(this.SharedContentID);
                    object tempItem;
                    if (Telerik.Sitefinity.ContentLocations.ContentLocatableViewExtensions.TryGetItemWithRequestedStatus(sharedContent, this.ContentManager, out tempItem))
                    {
                        sharedContent = tempItem as ContentItem;
                        this.isContentLoadedCorrectly = true;
                    }
                    
                    return sharedContent.Content;
                }
                catch (UnauthorizedAccessException)
                {
                    exMessage = Res.Get<ContentBlockResources>().NoViewPermissionsMessage;
                    this.isContentLoadedCorrectly = false;
                }
                catch (ItemNotFoundException ex)
                {
                    exMessage = ex.Message;
                    this.SharedContentID = Guid.Empty;
                    this.isContentLoadedCorrectly = false;
                }

                //this.SubscribeCacheDependency();
            }
            else
            {
                this.ContentType = typeof(Telerik.Sitefinity.Pages.Model.PageDraftControl).FullName;
            }

            return String.Empty;
        }


        /// <summary>
        /// Creates the blank data item.
        /// </summary>
        /// <returns></returns>
        internal object CreateBlankDataItem()
        {
            bool securitySuppressed = this.ContentManager.Provider.SuppressSecurityChecks;
            this.ContentManager.Provider.SuppressSecurityChecks = true;
            var item = this.ContentManager.CreateItem(typeof(ContentItem), Guid.Empty);
            this.ContentManager.Provider.SuppressSecurityChecks = securitySuppressed;

            return item;
        }


        #region Private members

        private ContentManager InitializeManager()
        {
            if (this.contentManager == null)
            {
                try
                {
                    return ContentManager.GetManager(this.ProviderName);
                }
                catch (ConfigurationErrorsException)
                {
                    this.isControlDefinitionProviderCorrect = false;
                    return null;
                }
            }
            return this.contentManager;
        }

        /// <summary>
        /// Determines whether this content block is shared.
        /// </summary>
        /// <returns></returns>
        private bool IsShared()
        {
            this.InitializeManager();
            if (this.SharedContentID != Guid.Empty && this.isControlDefinitionProviderCorrect)
            {
                return true;
            }
            return false;
        }


        private ContentManager contentManager;
        private bool isControlDefinitionProviderCorrect = true;

        #endregion
    }

}
