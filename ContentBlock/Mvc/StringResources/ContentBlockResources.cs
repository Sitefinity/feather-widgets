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
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockResources"/> class. 
        /// Initializes new instance of <see cref="ContentBlockResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ContentBlockResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public ContentBlockResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        /// <summary>
        /// Gets Resources for Comments
        /// </summary>
        [ResourceEntry("CreateContent", 
            Value = "Create Content", 
            Description = "The phrase that will show when the widget has no content.", 
            LastModified = "2014/02/06")]
        public string CreateContent
        {
            get
            {
                return this["CreateContent", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets phrase: You are not allowed to see this content. Contact administrator for more information
        /// </summary>
        [ResourceEntry("NoViewPermissionsMessage", 
            Value = "You are not allowed to see this content. Contact administrator for more information", 
            Description = "phrase: You are not allowed to see this content. Contact administrator for more information", 
            LastModified = "2014/02/06")]
        public string NoViewPermissionsMessage
        {
            get
            {
                return this["NoViewPermissionsMessage", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets word: Shared
        /// </summary>
        [ResourceEntry("Shared", 
            Value = "<span class='sfShared'>Shared</span>", 
            Description = "word: Shared", 
            LastModified = "2014/02/06")]
        public string Shared
        {
            get
            {
                return this["Shared", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets the cancel.
        /// </summary>
        [ResourceEntry("Cancel", 
            Value = "Cancel", 
            Description = "word: Cancel", 
            LastModified = "2014/03/05")]
        public string Cancel
        {
            get
            {
                return this["Cancel", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        [ResourceEntry("Title", 
            Value = "Title", 
            Description = "word: Title", 
            LastModified = "2014/03/05")]
        public string Title
        {
            get
            {
                return this["Title", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets the share content.
        /// </summary>
        [ResourceEntry("ShareContent", 
            Value = "Share this content", 
            Description = "phrase: Share this content", 
            LastModified = "2014/03/05")]
        public string ShareContent
        {
            get
            {
                return this["ShareContent", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets phrase: This content will not be shared anymore. The changes you make will not affect other pages. Are you sure you want to Unshare this content?
        /// </summary>
        [ResourceEntry("AreYouSureYouWantToUnshareThisContent", 
            Value = "<p>This content will not be shared anymore. The changes you make will not affect other pages.</p><p>Are you sure you want to unshare this content?</p>", 
            Description = "phrase: This content will not be shared anymore. The changes you make will not affect other pages. Are you sure you want to unshare this content?", 
            LastModified = "2014/03/07")]
        public string AreYouSureYouWantToUnshareThisContent
        {
            get
            {
                return this["AreYouSureYouWantToUnshareThisContent", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets phrase: Yes, Unshare this content
        /// </summary>
        [ResourceEntry("UnshareThisContent", 
            Value = "Yes, Unshare this content", 
            Description = "phrase: Yes, Unshare this content", 
            LastModified = "2014/03/07")]
        public string UnshareThisContent
        {
            get
            {
                return this["UnshareThisContent", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Share
        /// </summary>
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

        /// <summary>
        /// Gets use shared
        /// </summary>
        [ResourceEntry("UseShared", 
            Value = "Use shared", 
            Description = "phrase: Use shared", 
            LastModified = "2014/06/04")]
        public string UseShared
        {
            get
            {
                return this["UseShared", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Unshare
        /// </summary>
        [ResourceEntry("Unshare", 
            Value = "Unshare", 
            Description = "word: Unshare", 
            LastModified = "2014/03/10")]
        public string Unshare
        {
            get
            {
                return this["Unshare", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets This content is shared. Any changes will be reflected everywhere it is shared.
        /// </summary>
        [ResourceEntry("SharedContentEditWarning", 
            Value = "This content is shared. Any changes will be reflected everywhere it is shared.", 
            Description = "This message will pop in the ContentBlock widget when trying to edit shared content.", 
            LastModified = "2014/03/12")]
        public string SharedContentEditWarning
        {
            get
            {
                return this["SharedContentEditWarning", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Content block widget resources
        /// </summary>
        [ResourceEntry("ContentBlockResourcesTitle", 
            Value = "Content block widget resources", 
            Description = "Title for the content block widget resources class.", 
            LastModified = "2014/03/17")]
        public string ContentBlockResourcesTitle
        {
            get
            {
                return this["ContentBlockResourcesTitle", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Localizable strings for the Content block widget.
        /// </summary>
        [ResourceEntry("ContentBlockResourcesDescription", 
            Value = "Localizable strings for the Content block widget.", 
            Description = "Description for the content block widget resources class.", 
            LastModified = "2014/03/17")]
        public string ContentBlockResourcesDescription
        {
            get
            {
                return this["ContentBlockResourcesDescription", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Error!
        /// </summary>
        [ResourceEntry("Error", 
            Value = "Error!", 
            Description = "Error!", 
            LastModified = "2014/05/20")]
        public string Error
        {
            get
            {
                return this["Error", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Share content block
        /// </summary>
        [ResourceEntry("ShareContentBlock", 
            Value = "Share content block", 
            Description = "Share content block", 
            LastModified = "2014/05/20")]
        public string ShareContentBlock
        {
            get
            {
                return this["ShareContentBlock", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Title is required!
        /// </summary>
        [ResourceEntry("TitleRequired", 
            Value = "Title is required!", 
            Description = "Title is required!", 
            LastModified = "2014/05/20")]
        public string TitleRequired
        {
            get
            {
                return this["TitleRequired", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Simple
        /// </summary>
        [ResourceEntry("Simple", 
            Value = "Simple", 
            Description = "Simple", 
            LastModified = "2014/05/20")]
        public string Simple
        {
            get
            {
                return this["Simple", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Select shared content
        /// </summary>
        [ResourceEntry("SelectSharedContent", 
            Value = "Select shared content", 
            Description = "Select shared content", 
            LastModified = "2014/05/21")]
        public string SelectSharedContent
        {
            get
            {
                return this["SelectSharedContent", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets No items found
        /// </summary>
        [ResourceEntry("NoItemsFound", 
            Value = "No items found", 
            Description = "No items found", 
            LastModified = "2014/05/21")]
        public string NoItemsFound
        {
            get
            {
                return this["NoItemsFound", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets No items have been created yet
        /// </summary>
        [ResourceEntry("NoItemsHaveBeedCreatedYet", 
            Value = "No items have been created yet", 
            Description = "No items have been created yet", 
            LastModified = "2014/05/21")]
        public string NoItemsHaveBeedCreatedYet
        {
            get
            {
                return this["NoItemsHaveBeedCreatedYet", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Narrow by typing...
        /// </summary>
        [ResourceEntry("NarrowByTyping", 
            Value = "Narrow by typing...", 
            Description = "Narrow by typing...", 
            LastModified = "2014/05/21")]
        public string NarrowByTyping
        {
            get
            {
                return this["NarrowByTyping", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Done selecting
        /// </summary>
        [ResourceEntry("DoneSelecting", 
            Value = "Done selecting", 
            Description = "Done selecting", 
            LastModified = "2014/05/21")]
        public string DoneSelecting
        {
            get
            {
                return this["DoneSelecting", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets This content is shared.
        /// </summary>
        [ResourceEntry("ThisContentIsShared", 
            Value = "This content is shared.", 
            Description = "This content is shared.", 
            LastModified = "2014/05/21")]
        public string ThisContentIsShared
        {
            get
            {
                return this["ThisContentIsShared", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Any changes will be reflected everywhere it is shared.
        /// </summary>
        [ResourceEntry("SharedContentWarning", 
            Value = "Any changes will be reflected everywhere it is shared.", 
            Description = "Any changes will be reflected everywhere it is shared.", 
            LastModified = "2014/05/21")]
        public string SharedContentWarning
        {
            get
            {
                return this["SharedContentWarning", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets Unshared content block
        /// </summary>
        [ResourceEntry("UnshareContentBlock", 
            Value = "Unshare content block", 
            Description = "Unshare content block", 
            LastModified = "2014/05/21")]
        public string UnshareContentBlock
        {
            get
            {
                return this["UnshareContentBlock", System.Globalization.CultureInfo.InvariantCulture];
            }
        }

        /// <summary>
        /// Gets the Provider
        /// </summary>
        [ResourceEntry("Provider", 
            Value = "Provider", 
            Description = "Provider", 
            LastModified = "2014/05/29")]
        public string Provider
        {
            get
            {
                return this["Provider", System.Globalization.CultureInfo.InvariantCulture];
            }
        }
    }
}