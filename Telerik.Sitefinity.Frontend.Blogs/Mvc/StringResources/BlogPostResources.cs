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
    }
}