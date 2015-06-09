using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Publishing.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Feed widget
    /// </summary>
    [ObjectInfo(typeof(FeedResources), ResourceClassId = "FeedResources", Title = "FeedResourcesTitle", Description = "FeedResourcesDescription")]
    public class FeedResources : Resource
    {
        #region Constructions
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedResources"/> class.
        /// </summary>
        public FeedResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public FeedResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        /// <summary>
        /// Gets Title for the Feed widget resources class.
        /// </summary>
        [ResourceEntry("FeedResourcesTitle",
            Value = "Feed widget resources",
            Description = "Title for the Feed widget resources class.",
            LastModified = "2015/06/08")]
        public string FeedResourcesTitle
        {
            get
            {
                return this["FeedResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the Feed widget resources class.
        /// </summary>
        [ResourceEntry("FeedResourcesDescription",
            Value = "Localizable strings for the Feed widget.",
            Description = "Description for the Feed widget resources class.",
            LastModified = "2015/06/08")]
        public string FeedResourcesDescription
        {
            get
            {
                return this["FeedResourcesDescription"];
            }
        }

        /// <summary>
        /// Phrase: Select a feed
        /// </summary>
        /// <value>Select a feed</value>
        [ResourceEntry("SelectFeed",
            Value = "Select a feed",
            Description = "Phrase: Select a feed",
            LastModified = "2015/06/08")]
        public string SelectFeed
        {
            get
            {
                return this["SelectFeed"];
            }
        }

        /// <summary>
        /// Phrase: Which feed to use?
        /// </summary>
        /// <value>Which feed to use?</value>
        [ResourceEntry("WhichFeedToUse",
            Value = "Which feed to use?",
            Description = "Phrase: Which feed to use?",
            LastModified = "2015/06/08")]
        public string WhichFeedToUse
        {
            get
            {
                return this["WhichFeedToUse"];
            }
        }

        /// <summary>
        /// Phrase: Links both in the page and in the browser address-bar
        /// </summary>
        /// <value>Links both in the page and in the browser address-bar</value>
        [ResourceEntry("InsertLinkBothInPageAndBrowser",
            Value = "Links both in the page and in the browser address-bar",
            Description = "Phrase: Links both in the page and in the browser address-bar",
            LastModified = "2015/06/08")]
        public string InsertLinkBothInPageAndBrowser
        {
            get
            {
                return this["InsertLinkBothInPageAndBrowser"];
            }
        }

        /// <summary>
        /// Phrase: Link in the page only
        /// </summary>
        /// <value>Link in the page only</value>
        [ResourceEntry("InsertLinkInPageOnly",
            Value = "Link in the page only",
            Description = "Phrase: Link in the page only",
            LastModified = "2015/06/08")]
        public string InsertLinkInPageOnly
        {
            get
            {
                return this["InsertLinkInPageOnly"];
            }
        }

        /// <summary>
        /// Phrase: Link in the browser address-bar only
        /// </summary>
        /// <value>Link in the browser address-bar only</value>
        [ResourceEntry("InsertLinkInBrowserOnly",
            Value = "Link in the browser address-bar only",
            Description = "Phrase: Link in the browser address-bar only",
            LastModified = "2015/06/08")]
        public string InsertLinkInBrowserOnly
        {
            get
            {
                return this["InsertLinkInBrowserOnly"];
            }
        }

        /// <summary>
        /// Phrase: Text to display
        /// </summary>
        /// <value>Text to display</value>
        [ResourceEntry("TextToDisplay",
            Value = "Text to display",
            Description = "Phrase: Text to display",
            LastModified = "2015/06/08")]
        public string TextToDisplay
        {
            get
            {
                return this["TextToDisplay"];
            }
        }

        /// <summary>
        /// Phrase: Open this link in a new window
        /// </summary>
        /// <value>Open this link in a new window</value>
        [ResourceEntry("OpenInNewWin",
            Value = "Open this link in a new window",
            Description = "Phrase: Open this link in a new window",
            LastModified = "2015/06/08")]
        public string OpenInNewWin
        {
            get
            {
                return this["OpenInNewWin"];
            }
        }

        /// <summary>
        /// Word: Tooltip
        /// </summary>
        /// <value>Tooltip</value>
        [ResourceEntry("Tooltip",
            Value = "Tooltip",
            Description = "Word: Tooltip",
            LastModified = "2015/06/08")]
        public string Tooltip
        {
            get
            {
                return this["Tooltip"];
            }
        }

        /// <summary>
        /// Word: Template
        /// </summary>
        /// <value>Template</value>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "Word: Template",
            LastModified = "2015/06/08")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Phrase: Css classes
        /// </summary>
        /// <value>Css classes</value>
        [ResourceEntry("CssClasses",
            Value = "Css classes",
            Description = "Phrase: Css classes",
            LastModified = "2015/06/08")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Phrase: Insert...
        /// </summary>
        /// <value>Insert...</value>
        [ResourceEntry("Insert",
            Value = "Insert...",
            Description = "Phrase: Insert...",
            LastModified = "2015/06/08")]
        public string Insert
        {
            get
            {
                return this["Insert"];
            }
        }

        /// <summary>
        /// Phrase: Appears when the cursor is pointed to the link
        /// </summary>
        /// <value>Appears when the cursor is pointed to the link</value>
        [ResourceEntry("TooltipDescription",
            Value = "Appears when the cursor is pointed to the link",
            Description = "Phrase: Appears when the cursor is pointed to the link",
            LastModified = "2015/06/08")]
        public string TooltipDescription
        {
            get
            {
                return this["TooltipDescription"];
            }
        }

        /// <summary>
        /// Phrase: More options
        /// </summary>
        /// <value>More options</value>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "Phrase: More options",
            LastModified = "2015/06/08")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }
    }
}
