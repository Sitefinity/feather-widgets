using System;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.Blog;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Helpers
{
    public static class BlogHelper
    {
        public static string GetDetailPageUrl(ItemViewModel item, Guid detailsPageId, BlogDetailLocationMode blogDetailLocationMode)
        {
            try
            {
                return DetailLocationHyperLinkHelper.GetDetailPageUrl(item, detailsPageId, blogDetailLocationMode);
            }
            catch (ArgumentException ex)
            {
                // When the default page is delete, the blog should be rendered as a normal blog without default page.
                if (ex.Message.Contains("Invalid details page"))
                {
                    return null;
                }

                throw;
            }
        }
    }
}
