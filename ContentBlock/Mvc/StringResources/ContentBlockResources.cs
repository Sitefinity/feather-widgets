using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace ContentBlock.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Content block widget
    /// </summary>
    [ObjectInfo(typeof(ContentBlockResources), Title = "ContentBlockResourcesTitle", Description = "ContentBlockResourcesDescription")]
    public class ContentBlockResources : Resource
    {
        #region Constructions
        static ContentBlockResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="ContentBlockResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ContentBlockResources()
        {
        }

        /// <summary>
        /// Initializes new instance of <see cref="ContentBlockResources"/> class with the provided <see cref="ResourceDataProvider"/>.
        /// </summary>
        /// <param name="dataProvider"><see cref="ResourceDataProvider"/></param>
        public ContentBlockResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion


        /// <summary>
        /// Resources for Comments
        /// </summary>
        [ResourceEntry("CreateContent",
            Value = "Create Content",
            Description = "The phrase that will show when the widget has no content.",
            LastModified = "2014/02/06")]
        public string CreateContent
        {
            get
            {
                return this["CreateContent"];
            }
        }

        /// <summary>
        /// phrase: You are not allowed to see this content. Contact administrator for more information
        /// </summary>
        [ResourceEntry("NoViewPermissionsMessage",
            Value = "You are not allowed to see this content. Contact administrator for more information",
            Description = "phrase: You are not allowed to see this content. Contact administrator for more information",
            LastModified = "2014/02/06")]
        public string NoViewPermissionsMessage
        {
            get
            {
                return this["NoViewPermissionsMessage"];
            }
        }

        /// <summary>
        /// word: Shared
        /// </summary>
        [ResourceEntry("Shared",
            Value = "<span class='sfShared'>Shared</span>",
            Description = "word: Shared",
            LastModified = "2014/02/06")]
        public string Shared
        {
            get
            {
                return this["Shared"];
            }
        }

        [ResourceEntry("Cancel",
            Value = "Cancel",
            Description = "word: Cancel",
            LastModified = "2014/03/05")]
        public string Cancel
        {
            get
            {
                return this["Cancel"];
            }
        }

        [ResourceEntry("Title",
            Value = "Title",
            Description = "word: Title",
            LastModified = "2014/03/05")]
        public string Title
        {
            get
            {
                return this["Title"];
            }
        }

        [ResourceEntry("ShareContent",
            Value = "Share this content",
            Description = "phrase: Share this content",
            LastModified = "2014/03/05")]
        public string ShareContent
        {
            get
            {
                return this["ShareContent"];
            }
        }

        /// <summary>
        /// phrase: This content will not be shared anymore. The changes you make will not affect other pages. Are you sure you want to Unshare this content?
        /// </summary>
        [ResourceEntry("AreYouSureYouWantToUnshareThisContent",
            Value = "<p>This content will not be shared anymore. The changes you make will not affect other pages.</p><p>Are you sure you want to unshare this content?</p>",
            Description = "phrase: This content will not be shared anymore. The changes you make will not affect other pages. Are you sure you want to unshare this content?",
            LastModified = "2014/03/07")]
        public string AreYouSureYouWantToUnshareThisContent
        {
            get
            {
                return this["AreYouSureYouWantToUnshareThisContent"];
            }
        }

        /// <summary>
        /// phrase: Yes, Unshare this content
        /// </summary>
        [ResourceEntry("UnshareThisContent",
            Value = "Yes, Unshare this content",
            Description = "phrase: Yes, Unshare this content",
            LastModified = "2014/03/07")]
        public string UnshareThisContent
        {
            get
            {
                return this["UnshareThisContent"];
            }
        }

        [ResourceEntry("Share",
            Value = "Share",
            Description = "word: Share",
            LastModified = "2014/03/10")]
        public string Share
        {
            get
            {
                return this["Share"];
            }
        }

        [ResourceEntry("Unshare",
            Value = "Unshare",
            Description = "word: Unshare",
            LastModified = "2014/03/10")]
        public string Unshare
        {
            get
            {
                return this["Unshare"];
            }
        }

        [ResourceEntry("SharedContentEditWarning",
            Value = "This content is shared. Any changes will be reflected everywhere it is shared.",
            Description = "This message will pop in the ContentBlock widget when trying to edit shared content.",
            LastModified = "2014/03/12")]
        public string SharedContentEditWarning
        {
            get
            {
                return this["SharedContentEditWarning"];
            }
        }

        [ResourceEntry("ContentBlockResourcesTitle",
            Value = "Content block widget resources",
            Description = "Title for the content block widget resources class.",
            LastModified = "2014/03/17")]
        public string ContentBlockResourcesTitle
        {
            get
            {
                return this["ContentBlockResourcesTitle"];
            }
        }

        [ResourceEntry("ContentBlockResourcesDescription",
            Value = "Localizable strings for the Content block widget.",
            Description = "Description for the content block widget resources class.",
            LastModified = "2014/03/17")]
        public string ContentBlockResourcesDescription
        {
            get
            {
                return this["ContentBlockResourcesDescription"];
            }
        }
    }
}