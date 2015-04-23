using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Blog widget
    /// </summary>
    [ObjectInfo(typeof(BlogListResources), ResourceClassId = "BlogResources", Title = "BlogResourcesTitle", Description = "BlogResourcesDescription")]
    public class BlogListResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogListResources"/> class. 
        /// Initializes new instance of <see cref="BlogListResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public BlogListResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogListResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public BlogListResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        #region Meta resources

        /// <summary>
        /// Gets Blog widget resources title.
        /// </summary>
        [ResourceEntry("BlogResourcesTitle", 
            Value = "Blog widget resources", 
            Description = "Title for the blog widget resources class.", 
            LastModified = "2015/04/22")]
        public string BlogResourcesTitle
        {
            get
            {
                return this["BlogResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Blog widget resources description.
        /// </summary>
        [ResourceEntry("BlogResourcesDescription", 
            Value = "Localizable strings for the Blog widget.", 
            Description = "Description for the blog widget resources class.", 
            LastModified = "2015/04/22")]
        public string BlogResourcesDescription
        {
            get
            {
                return this["BlogResourcesDescription"];
            }
        }

        #endregion

        #region Frontend resources

        /// <summary>
        /// phrase: {PostCount} posts
        /// </summary>
        [ResourceEntry("PostsCount",
            Value = "{0} posts",
            Description = "phrase: {PostCount} posts",
            LastModified = "2015/04/23")]
        public string PostsCount
        {
            get
            {
                return this["PostsCount"];
            }
        }

        /// <summary>
        /// phrase: Last post
        /// </summary>
        [ResourceEntry("LastPost",
            Value = "Last post",
            Description = "phrase: Last post",
            LastModified = "2015/04/23")]
        public string LastPost
        {
            get
            {
                return this["LastPost"];
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
            LastModified = "2015/04/22")]
        public string Content
        {
            get
            {
                return this["Content"];
            }
        }

        /// <summary>
        /// Gets the Provider
        /// </summary>
        [ResourceEntry("Provider",
            Value = "Provider",
            Description = "Provider",
            LastModified = "2015/04/23")]
        public string Provider
        {
            get
            {
                return this["Provider"];
            }
        }

        /// <summary>
        /// phrase: List settings
        /// </summary>
        /// <value>List settings</value>
        [ResourceEntry("ListSettings",
            Value = "List settings",
            Description = "phrase: List settings",
            LastModified = "2015/04/22")]
        public string ListSettings
        {
            get
            {
                return this["ListSettings"];
            }
        }
        
        /// <summary>
        /// Gets phrase : Use limit
        /// </summary>
        [ResourceEntry("UseLimit",
            Value = "Use limit",
            Description = "phrase : Use limit",
            LastModified = "2015/04/22")]
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
            LastModified = "2015/04/22")]
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
            LastModified = "2015/04/22")]
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
            LastModified = "2015/04/22")]
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
            LastModified = "2015/04/22")]
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

        /// Phrase: Divide the list on pages up to {0} items per page
        /// </summary>
        /// <value>Divide the list on pages up to {0} items per page</value>
        [ResourceEntry("DivideToItemPerPage",
            Value = "Divide the list on pages up to {0} items per page",
            Description = "Phrase: Divide the list on pages up to {0} items per page",
            LastModified = "2015/04/22")]
        public string DivideToItemPerPage
        {
            get
            {
                return this["DivideToItemPerPage"];
            }
        }

        /// <summary>
        /// Gets phrase : Which blogs to display?
        /// </summary>
        [ResourceEntry("ItemsToDisplay",
            Value = "Which blogs to display?",
            Description = "phrase : Which blogs to display?",
            LastModified = "2015/04/22")]
        public string ItemsToDisplay
        {
            get
            {
                return this["ItemsToDisplay"];
            }
        }

        /// <summary>
        /// phrase: All blogs
        /// </summary>
        /// <value>All blogs</value>
        [ResourceEntry("AllBlogs",
            Value = "All blogs",
            Description = "phrase: All blogs",
            LastModified = "2015/04/22")]
        public string AllBlogs
        {
            get
            {
                return this["AllBlogs"];
            }
        }

        /// <summary>
        /// Gets phrase : Selected blogs...
        /// </summary>
        [ResourceEntry("SelectedItems",
            Value = "Selected blogs...",
            Description = "phrase : Selected blogs...",
            LastModified = "2015/04/22")]
        public string SelectedItems
        {
            get
            {
                return this["SelectedItems"];
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

        /// </summary>
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
        /// Gets phrase : Default page for blog
        /// </summary>
        [ResourceEntry("DefaultPageForBlog",
            Value = "Default page for blog",
            Description = "phrase : Default page for blog",
            LastModified = "2015/04/23")]
        public string DefaultPageForBlog
        {
            get
            {
                return this["DefaultPageForBlog"];
            }
        }
        
        /// <summary>
        /// Gets phrase : Detail template
        /// </summary>
        [ResourceEntry("DetailTemplate",
            Value = "Detail template",
            Description = "phrase : Detail template",
            LastModified = "2015/04/23")]
        public string DetailTemplate
        {
            get
            {
                return this["DetailTemplate"];
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

        #endregion
    }
}