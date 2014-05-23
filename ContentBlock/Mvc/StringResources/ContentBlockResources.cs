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

        /// <summary>
        /// Error!
        /// </summary>
        [ResourceEntry("Error",
            Value = "Error!",
            Description = "Error!",
            LastModified = "2014/05/20")]
        public string Error
        {
            get
            {
                return this["Error"];
            }
        }

        /// <summary>
        /// Share content block
        /// </summary>
        [ResourceEntry("ShareContentBlock",
            Value = "Share content block",
            Description = "Share content block",
            LastModified = "2014/05/20")]
        public string ShareContentBlock
        {
            get
            {
                return this["ShareContentBlock"];
            }
        }

        /// <summary>
        /// Title is required!
        /// </summary>
        [ResourceEntry("TitleRequired",
            Value = "Title is required!",
            Description = "Title is required!",
            LastModified = "2014/05/20")]
        public string TitleRequired
        {
            get
            {
                return this["TitleRequired"];
            }
        }

        /// <summary>
        /// Simple
        /// </summary>
        [ResourceEntry("Simple",
            Value = "Simple",
            Description = "Simple",
            LastModified = "2014/05/20")]
        public string Simple
        {
            get
            {
                return this["Simple"];
            }
        }

        /// <summary>
        /// Advanced
        /// </summary>
        [ResourceEntry("PropertyGrid",
            Value = "Advanced",
            Description = "Advanced",
            LastModified = "2014/05/23")]
        public string PropertyGrid
        {
            get
            {
                return this["PropertyGrid"];
            }
        }

        /// <summary>
        /// Select shared content
        /// </summary>
        [ResourceEntry("SelectSharedContent",
            Value = "Select shared content",
            Description = "Select shared content",
            LastModified = "2014/05/21")]
        public string SelectSharedContent
        {
            get
            {
                return this["SelectSharedContent"];
            }
        }

        /// <summary>
        /// No items found
        /// </summary>
        [ResourceEntry("NoItemsFound",
            Value = "No items found",
            Description = "No items found",
            LastModified = "2014/05/21")]
        public string NoItemsFound
        {
            get
            {
                return this["NoItemsFound"];
            }
        }

        /// <summary>
        /// No items have been created yet
        /// </summary>
        [ResourceEntry("NoItemsHaveBeedCreatedYet",
            Value = "No items have been created yet",
            Description = "No items have been created yet",
            LastModified = "2014/05/21")]
        public string NoItemsHaveBeedCreatedYet
        {
            get
            {
                return this["NoItemsHaveBeedCreatedYet"];
            }
        }

        /// <summary>
        /// Narrow by typing...
        /// </summary>
        [ResourceEntry("NarrowByTyping",
            Value = "Narrow by typing...",
            Description = "Narrow by typing...",
            LastModified = "2014/05/21")]
        public string NarrowByTyping
        {
            get
            {
                return this["NarrowByTyping"];
            }
        }

        /// <summary>
        /// Done selecting
        /// </summary>
        [ResourceEntry("DoneSelecting",
            Value = "Done selecting",
            Description = "Done selecting",
            LastModified = "2014/05/21")]
        public string DoneSelecting
        {
            get
            {
                return this["DoneSelecting"];
            }
        }

        /// <summary>
        /// This content is shared.
        /// </summary>
        [ResourceEntry("ThisContentIsShared",
            Value = "This content is shared.",
            Description = "This content is shared.",
            LastModified = "2014/05/21")]
        public string ThisContentIsShared
        {
            get
            {
                return this["ThisContentIsShared"];
            }
        }

        /// <summary>
        /// Any changes will be reflected everywhere it is shared.
        /// </summary>
        [ResourceEntry("SharedContentWarning",
            Value = "Any changes will be reflected everywhere it is shared.",
            Description = "Any changes will be reflected everywhere it is shared.",
            LastModified = "2014/05/21")]
        public string SharedContentWarning
        {
            get
            {
                return this["SharedContentWarning"];
            }
        }

        /// <summary>
        /// Unshare content block
        /// </summary>
        [ResourceEntry("UnshareContentBlock",
            Value = "Unshare content block",
            Description = "Unshare content block",
            LastModified = "2014/05/21")]
        public string UnshareContentBlock
        {
            get
            {
                return this["UnshareContentBlock"];
            }
        }
    }
}