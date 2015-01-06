using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.SocialShare.SocialShareHelpers
{
    /// <summary>
    /// Html helpers for the social share buttons
    /// </summary>
    public static class SocialShareButtons
    {
        /// <summary>
        /// Gets the URL of the page you want to share.
        /// </summary>
        /// <value>The page URL.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public static string ShareUrl
        {
            get
            {
                var shareUrl = string.Empty;
                var currentNode = SiteMapBase.GetCurrentProvider().CurrentNode;
                if (currentNode != null && currentNode.Url != null)
                {
                    shareUrl = RouteHelper.GetAbsoluteUrl(currentNode.Url);
                }

                return shareUrl;
            }
        }

        /// <summary>
        /// Gets the title of the page you want to share.
        /// </summary>
        /// <value>The page title.</value>
        public static string PageTitle
        {
            get
            {
                var title = string.Empty;
                var currentNode = SiteMapBase.GetCurrentProvider().CurrentNode;
                if (currentNode != null && currentNode.Url != null)
                {
                    title = currentNode.Title;
                }

                return title;
            }
        }
    }
}
