using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.Blog;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.DataResolving;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Helpers
{
    /// <summary>
    /// This class contains helpers for determining detail action URLs for blogs.
    /// </summary>
    public static class DetailLocationHyperLinkHelper
    {
        /// <summary>
        /// Gets the detail page URL for blog item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="detailsPageId">The details page identifier.</param>
        /// <param name="blogDetailLocationMode">The blog detail location mode.</param>
        /// <returns></returns>
        public static string GetDetailPageUrl(ItemViewModel item, Guid detailsPageId, BlogDetailLocationMode blogDetailLocationMode)
        {
            string url = null;
            if (blogDetailLocationMode == BlogDetailLocationMode.SelectedExistingPage)
            {
                url = HyperLinkHelpers.GetDetailPageUrl(item.DataItem, detailsPageId);
            }
            else if (blogDetailLocationMode == BlogDetailLocationMode.PerItem)
            {
                var blog = item.DataItem as Blog;

                if (blog != null)
                {
                    var blogPageId = blog.DefaultPageId;

                    if (blogPageId.HasValue)
                        url = HyperLinkHelpers.GetDetailPageUrl(item.DataItem, blogPageId.Value);
                }

            }
            else if (blogDetailLocationMode == BlogDetailLocationMode.SamePage)
            {
                url = DetailLocationHyperLinkHelper.ConstructSamePageUrl(item);
            }

            return url;
        }

        private static string ConstructSamePageUrl(ItemViewModel item)
        {
            var appRelativeUrl = DataResolver.Resolve(item.DataItem, "URL");
            var url = UrlPath.ResolveUrl(appRelativeUrl, true);

            return url;
        }
    }
}
