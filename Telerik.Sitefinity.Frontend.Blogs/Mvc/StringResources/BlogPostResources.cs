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

        #endregion
    }
}