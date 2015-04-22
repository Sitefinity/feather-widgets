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