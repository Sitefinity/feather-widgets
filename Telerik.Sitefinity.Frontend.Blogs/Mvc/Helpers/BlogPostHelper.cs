using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Modules.Blogs;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Helpers
{
    /// <summary>
    /// Provides helpers for working with blogs.
    /// </summary>
    public static class BlogPostHelper
    {
        /// <summary>
        /// Gets the last post date.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static DateTime? GetLastPostDate(this ItemViewModel item)
        {
            var blog = item.DataItem as Blog;

            if (blog == null)
                return null;

            var blogPosts = blog.BlogPosts();
            DateTime? lastPostDate = null;

            if (blogPosts.Count() > 0)
                lastPostDate = blogPosts.Max(bp => bp.PublicationDate);

            return lastPostDate;
        }
    }
}
