using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Blogs;
using SfBlog = Telerik.Sitefinity.Blogs.Model.Blog;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.Blog
{
    /// <summary>
    /// Provides API for working with <see cref="Telerik.Sitefinity.Blogs.Model.Blog"/> items.
    /// </summary>
    public class BlogModel : ContentModelBase, IBlogModel
    {
        /// <summary>
        /// Gets or sets the type of content that is loaded.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public override Type ContentType
        {
            get
            {
                return typeof(SfBlog);
            }

            set
            {
            }
        }

        /// <inheritDoc/>
        public int MinPostsCount { get; set; }

        /// <inheritDoc/>
        public int MaxPostsAge { get; set; }

        /// <inheritDoc/>
        public FilteredSelectionMode FilteredSelectionMode { get; set; }

        /// <inheritDoc/>
        public ContentListViewModel CreateListViewModel(int page)
        {
            return base.CreateListViewModel(null, page);
        }
        
        protected override string AdaptMultilingualFilterExpression(string filterExpression)
        {
            return filterExpression;
        }

        protected override string AddLiveFilterExpression(string filterExpression)
        {
            return filterExpression;
        }

        /// <summary>
        /// Gets the items query.
        /// </summary>
        /// <returns>The query.</returns>
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            return ((BlogsManager)this.GetManager()).GetBlogs();
        }
    }
}
