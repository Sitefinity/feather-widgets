using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Blogs;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Models
{
    /// <summary>
    /// A model for the Blog Post MVC widget.
    /// </summary>
    public class BlogPostModel : ContentModelBase, IBlogPostModel
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
                return typeof(BlogPost);
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets the items query.
        /// </summary>
        /// <returns>The query.</returns>
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            return ((BlogsManager)this.GetManager()).GetBlogPosts();
        }
    }
}
