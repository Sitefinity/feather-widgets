using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Blog post widget
    /// </summary>
    [ObjectInfo(typeof(BlogPostResources), ResourceClassId = "BlogPostResources", Title = "BlogPostResourcesTitle", Description = "BlogPostResourcesDescription")]
    public class BlogPostResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogPostResources"/> class. 
        /// Initializes new instance of <see cref="BlogPostResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public BlogPostResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogPostResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public BlogPostResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        #region Meta resources

        /// <summary>
        /// Gets Blog post widget resources title.
        /// </summary>
        [ResourceEntry("ContentBlockResourcesTitle", 
            Value = "Blog post widget resources", 
            Description = "Title for the blog post widget resources class.", 
            LastModified = "2015/04/21")]
        public string BlogPostResourcesTitle
        {
            get
            {
                return this["BlogPostResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Blog post widget resources description.
        /// </summary>
        [ResourceEntry("BlogPostResourcesDescription", 
            Value = "Localizable strings for the Blog post widget.", 
            Description = "Description for the blog post widget resources class.", 
            LastModified = "2015/04/21")]
        public string BlogPostResourcesDescription
        {
            get
            {
                return this["BlogPostResourcesDescription"];
            }
        }

        #endregion

        #region Frontend resources

        /// <summary>
        /// Gets word : by
        /// </summary>
        [ResourceEntry("By",
            Value = "by",
            Description = "word: by",
            LastModified = "2015/04/21")]
        public string By
        {
            get
            {
                return this["By"];
            }
        }

        /// <summary>
        /// Gets phrase : Full story
        /// </summary>
        [ResourceEntry("FullStory",
            Value = "Full story",
            Description = "phrase : Full story",
            LastModified = "2015/04/21")]
        public string FullStory
        {
            get
            {
                return this["FullStory"];
            }
        }

        #endregion

        #region Designer resources

        /// <summary>
        /// Gets word : Content
        /// </summary>
        [ResourceEntry("Content",
            Value = "Content",
            Description = "word: Content",
            LastModified = "2015/04/21")]
        public string Content
        {
            get
            {
                return this["Content"];
            }
        }

        /// <summary>
        /// phrase: List settings
        /// </summary>
        /// <value>List settings</value>
        [ResourceEntry("ListSettings",
            Value = "List settings",
            Description = "phrase: List settings",
            LastModified = "2015/04/21")]
        public string ListSettings
        {
            get
            {
                return this["ListSettings"];
            }
        }

        /// <summary>
        /// Gets phrase : Single item settings
        /// </summary>
        [ResourceEntry("SingleItemSettings",
            Value = "Single item settings",
            Description = "phrase : Single item settings",
            LastModified = "2015/04/21")]
        public string SingleItemSettings
        {
            get
            {
                return this["SingleItemSettings"];
            }
        }

        /// <summary>
        /// phrase: Some of the selected blog were deleted.
        /// </summary>
        /// <value>Some of the selected blogs were deleted.</value>
        [ResourceEntry("MissingBlogs",
            Value = "Some of the selected blogs were deleted.",
            Description = "phrase: Some of the selected blogs were deleted.",
            LastModified = "2015/04/21")]
        public string MissingBlogs
        {
            get
            {
                return this["MissingBlogs"];
            }
        }

        /// <summary>
        /// Gets phrase : Which blog posts to display?
        /// </summary>
        [ResourceEntry("ItemsToDisplay",
            Value = "Which blog posts to display?",
            Description = "phrase : Which blog posts to display?",
            LastModified = "2015/04/21")]
        public string ItemsToDisplay
        {
            get
            {
                return this["ItemsToDisplay"];
            }
        }

        /// Phrase: Use paging
        /// </summary>
        /// <value>Use paging</value>
        [ResourceEntry("UsePaging",
            Value = "Use paging",
            Description = "Phrase: Use paging",
            LastModified = "2015/04/21")]
        public string UsePaging
        {
            get
            {
                return this["UsePaging"];
            }
        }

        /// <summary>
        /// Gets phrase : Selected blog posts...
        /// </summary>
        [ResourceEntry("SelectedItems",
            Value = "Selected blog posts...",
            Description = "phrase : Selected blog posts...",
            LastModified = "2015/04/21")]
        public string SelectedItems
        {
            get
            {
                return this["SelectedItems"];
            }
        }

        /// Phrase: Divide the list on pages up to {0} items per page
        /// </summary>
        /// <value>Divide the list on pages up to {0} items per page</value>
        [ResourceEntry("DivideToItemPerPage",
            Value = "Divide the list on pages up to {0} items per page",
            Description = "Phrase: Divide the list on pages up to {0} items per page",
            LastModified = "2015/04/21")]
        public string DivideToItemPerPage
        {
            get
            {
                return this["DivideToItemPerPage"];
            }
        }

        /// <summary>
        /// Gets phrase : Use limit
        /// </summary>
        [ResourceEntry("UseLimit",
            Value = "Use limit",
            Description = "phrase : Use limit",
            LastModified = "2015/04/21")]
        public string UseLimit
        {
            get
            {
                return this["UseLimit"];
            }
        }

        /// <summary>
        /// Phrase: Show only limited number of items {0} items in total
        /// </summary>
        /// <value>Show only limited number of items {0} items in total</value>
        [ResourceEntry("ShowLimitedItems",
            Value = "Show only limited number of items {0} items in total",
            Description = "Phrase: Show only limited number of items {0} items in total",
            LastModified = "2015/04/21")]
        public string ShowLimitedItems
        {
            get
            {
                return this["ShowLimitedItems"];
            }
        }

        /// <summary>
        /// Phrase: No limit and paging
        /// </summary>
        /// <value>No limit and paging</value>
        [ResourceEntry("NoLimitPaging",
            Value = "No limit and paging",
            Description = "Phrase: No limit and paging",
            LastModified = "2015/04/21")]
        public string NoLimitPaging
        {
            get
            {
                return this["NoLimitPaging"];
            }
        }

        /// <summary>
        /// Phrase: Show all published items at once
        /// </summary>
        /// <value>Show all published items at once</value>
        [ResourceEntry("ShowAllItems",
            Value = "Show all published items at once",
            Description = "Phrase: Show all published items at once",
            LastModified = "2015/04/21")]
        public string ShowAllItems
        {
            get
            {
                return this["ShowAllItems"];
            }
        }

        /// <summary>
        /// Phrase: Sort blog posts.
        /// </summary>
        /// <value>Sort blog posts</value>
        [ResourceEntry("SortItems",
            Value = "Sort blog posts",
            Description = "Phrase: Sort blog posts",
            LastModified = "2015/04/21")]
        public string SortItems
        {
            get
            {
                return this["SortItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Last published on top
        /// </summary>
        [ResourceEntry("LastPublished",
            Value = "Last published on top",
            Description = "phrase : Last published on top",
            LastModified = "2015/04/21")]
        public string LastPublished
        {
            get
            {
                return this["LastPublished"];
            }
        }

        /// <summary>
        /// Gets phrase : Last modified on top
        /// </summary>
        [ResourceEntry("LastModified",
            Value = "Last modified on top",
            Description = "phrase : Last modified on top",
            LastModified = "2015/04/21")]
        public string LastModified
        {
            get
            {
                return this["LastModified"];
            }
        }

        /// <summary>
        /// Gets phrase : By Title (A-Z)
        /// </summary>
        [ResourceEntry("ByTitleAZ",
            Value = "By Title (A-Z)",
            Description = "phrase : By Title (A-Z)",
            LastModified = "2015/04/21")]
        public string ByTitleAZ
        {
            get
            {
                return this["ByTitleAZ"];
            }
        }

        /// <summary>
        /// Gets phrase : By Title (Z-A)
        /// </summary>
        [ResourceEntry("ByTitleZA",
            Value = "By Title (Z-A)",
            Description = "phrase : By Title (Z-A)",
            LastModified = "2015/04/21")]
        public string ByTitleZA
        {
            get
            {
                return this["ByTitleZA"];
            }
        }

        /// <summary>
        /// phrase: As set manually
        /// </summary>
        /// <value>As set manually</value>
        [ResourceEntry("AsSetManually",
            Value = "As set manually",
            Description = "phrase: As set manually",
            LastModified = "2015/04/21")]
        public string AsSetManually
        {
            get
            {
                return this["AsSetManually"];
            }
        }

        /// <summary>
        /// phrase: As set in Advanced mode
        /// </summary>
        /// <value>As set in Advanced mode</value>
        [ResourceEntry("AsSetInAdvancedMode",
            Value = "As set in Advanced mode",
            Description = "phrase: As set in Advanced mode",
            LastModified = "2015/04/21")]
        public string AsSetInAdvancedMode
        {
            get
            {
                return this["AsSetInAdvancedMode"];
            }
        }

        /// <summary>
        /// Gets phrase : List template
        /// </summary>
        [ResourceEntry("ListTemplate",
            Value = "List template",
            Description = "phrase : List template",
            LastModified = "2015/04/21")]
        public string ListTemplate
        {
            get
            {
                return this["ListTemplate"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/04/21")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/04/21")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets phrase : Open single item in...
        /// </summary>
        [ResourceEntry("OpenSingleItem",
            Value = "Open single item in...",
            Description = "phrase : Open single item in...",
            LastModified = "2015/04/21")]
        public string OpenSingleItem
        {
            get
            {
                return this["OpenSingleItem"];
            }
        }

        /// <summary>
        /// Gets phrase : Auto-generated page
        /// </summary>
        [ResourceEntry("ShowInSamePage",
            Value = "Auto-generated page",
            Description = "phrase : Auto-generated page (with the same layout as the list page)",
            LastModified = "2015/04/21")]
        public string ShowInSamePage
        {
            get
            {
                return this["ShowInSamePage"];
            }
        }

        /// <summary>
        /// Gets phrase : Selected existing page...
        /// </summary>
        [ResourceEntry("ShowInExistingPage",
            Value = "Selected existing page...",
            Description = "phrase : Selected existing page...",
            LastModified = "2015/04/21")]
        public string ShowInExistingPage
        {
            get
            {
                return this["ShowInExistingPage"];
            }
        }

        /// <summary>
        /// Gets phrase : Detail template
        /// </summary>
        [ResourceEntry("DetailTemplate",
            Value = "Detail template",
            Description = "phrase : Detail template",
            LastModified = "2015/04/21")]
        public string DetailTemplate
        {
            get
            {
                return this["DetailTemplate"];
            }
        }

        #endregion
    }
}