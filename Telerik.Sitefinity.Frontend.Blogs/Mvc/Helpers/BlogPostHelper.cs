using System;
using System.Linq;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.GenericContent.Model;
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

            var lastPost = blog.BlogPosts().Where(bp => bp.Status == ContentLifecycleStatus.Live).OrderByDescending(p => p.DateCreated).FirstOrDefault();
            if (lastPost != null)
            {
                return lastPost.DateCreated;
            }
            else
            {
                return null;
            }
        }
    }
}
