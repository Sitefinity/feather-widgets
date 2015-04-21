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

        #endregion
    }
}