using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Blog widget
    /// </summary>
    [ObjectInfo(typeof(BlogResources), ResourceClassId = "BlogResources", Title = "BlogResourcesTitle", Description = "BlogResourcesDescription")]
    public class BlogResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogResources"/> class. 
        /// Initializes new instance of <see cref="BlogResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public BlogResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public BlogResources(ResourceDataProvider dataProvider)
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

        #endregion
    }
}