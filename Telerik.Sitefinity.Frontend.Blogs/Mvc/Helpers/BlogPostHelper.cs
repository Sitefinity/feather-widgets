using System;
using System.Collections.Generic;
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
        /// Gets the last post dates for each blog in the model that has blog posts.
        /// </summary>
        /// <param name="model">The blog model.</param>
        /// <returns>Prefetched dictionary containing last post date for each blog in the model that has blog posts.</returns>
        public static IDictionary<Guid, DateTime> GetLastPostDates(this ContentListViewModel model)
        {
            const int BatchSize = 200;

            var ids = model.Items.Select(vm => vm.DataItem.Id).ToArray();
            var blogPosts = BlogsManager.GetManager(model.ProviderName).GetBlogPosts().Where(i => i.Status == ContentLifecycleStatus.Master);
            IEnumerable<KeyValuePair<Guid, DateTime>> blogsWithChildPosts;
            if (ids.Length <= BatchSize)
            {
                blogsWithChildPosts = BlogPostHelper.PartialBlogsLastPostDates(ids, blogPosts);
            }
            else
            {
                var tempResult = new List<KeyValuePair<Guid, DateTime>>(ids.Length);

                // Integer division, rounded up
                var pagesCount = (ids.Length + BatchSize - 1) / BatchSize;
                for (var p = 0; p < pagesCount; p++)
                {
                    var batch = ids.Skip(p * BatchSize).Take(BatchSize).ToArray();
                    tempResult.AddRange(BlogPostHelper.PartialBlogsLastPostDates(batch, blogPosts));
                }

                blogsWithChildPosts = tempResult;
            }

            var result = blogsWithChildPosts.ToDictionary(k => k.Key, k => k.Value);
            return result;
        }

        /// <summary>
        /// Gets the last post date of the given post.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="lastDates">The last dates.</param>
        /// <returns>Last post date for the given blog.</returns>
        /// <exception cref="System.ArgumentNullException">lastDates</exception>
        public static DateTime? GetLastPostDate(this ItemViewModel item, IDictionary<Guid, DateTime> lastDates)
        {
            if (lastDates == null)
                throw new ArgumentNullException("lastDates");

            if (lastDates.ContainsKey(item.DataItem.Id))
            {
                return lastDates[item.DataItem.Id];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the last post date of the blog.
        /// </summary>
        /// <param name="item">The blog view model.</param>
        /// <returns>The date of the last post if such exists.</returns>
        /// <remarks>This method causes an SQL query. For multiple items use the overload that accepts a dictionary with preloaded dates. Get the dictionary with Model.GetLastPostDates().</remarks>
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

        private static IEnumerable<KeyValuePair<Guid, DateTime>> PartialBlogsLastPostDates(Guid[] ids, IQueryable<BlogPost> blogPosts)
        {
            IEnumerable<KeyValuePair<Guid, DateTime>> blogsWithChildPosts;
            blogsWithChildPosts = from blogPost in blogPosts
                                  where ids.Contains(blogPost.Parent.Id)
                                  group blogPost by blogPost.Parent.Id into blogPostsGroup
                                  where blogPostsGroup.Count() > 0
                                  select new KeyValuePair<Guid, DateTime>(blogPostsGroup.Key, blogPostsGroup.Max(p => p.DateCreated));
            return blogsWithChildPosts;
        }
    }
}
