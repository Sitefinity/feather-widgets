using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.Modules.Blogs;
using Telerik.Sitefinity.TestUtilities.Utilities;

namespace FeatherWidgets.TestUtilities.CommonOperations.Blogs
{
    public class BlogsOperations
    {
        /// <summary>
        /// Creates a blog post with specified publication date.
        /// </summary>
        /// <param name="blogPostTitle">The blog post title.</param>
        /// <param name="blogId">The blog id.</param>
        /// <param name="publicationDate">The blog post publication date.</param>
        /// <returns></returns>
        public Guid CreateBlogPostSpecificPublicationDate(string blogPostTitle, Guid blogId, DateTime publicationDate)
        {
            BlogsManager blogsManager = new BlogsManager();

            var blog = blogsManager.GetBlog(blogId);
            var post = blogsManager.CreateBlogPost();
            post.Parent = blog;
            post.Title = blogPostTitle;
            post.UrlName = ServerArrangementUtilities.GetFormatedUrlName(blogPostTitle);

            Guid blogPostId = post.Id;

            post.SetWorkflowStatus(blogsManager.Provider.ApplicationName, "Published");
            blogsManager.RecompileAndValidateUrls(blog);
            blogsManager.Lifecycle.PublishWithSpecificDate(post, publicationDate);

            blogsManager.SaveChanges();

            return blogPostId;
        }
    }
}
