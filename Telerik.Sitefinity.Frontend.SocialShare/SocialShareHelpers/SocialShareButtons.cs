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

        /// <summary>
        /// Facebook button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="showCount">if set to <c>true</c> shows count.</param>
        /// <param name="isLarge">if set to <c>true</c> is large.</param>
        /// <param name="addText">if set to <c>true</c> [add text].</param>
        /// <returns>FacebookButton Html</returns>
        public static System.Web.Mvc.MvcHtmlString FacebookButton(this System.Web.Mvc.HtmlHelper helper, bool showCount, bool isLarge, bool addText)
        {
            var currentNode = SiteMapBase.GetCurrentProvider().CurrentNode;

            var shareUrl = string.Empty;
            var siteMapNode = SiteMapBase.GetCurrentProvider().CurrentNode;

            if (currentNode != null && currentNode.Url != null && siteMapNode != null)
                shareUrl = RouteHelper.GetAbsoluteUrl(siteMapNode.Url);

            string buttonTypeAttribute;

            if (showCount)
                buttonTypeAttribute = "data-type='button_count'";
            else if (isLarge || addText)
                buttonTypeAttribute = "data-type='button'";
            else
                buttonTypeAttribute = "data-type='icon'";

            var htmlString = string.Format(System.Globalization.CultureInfo.InvariantCulture, @"<div class='fb-share-button' data-href='{0}' {1}></div>", shareUrl, buttonTypeAttribute);

            var scriptString = @"<div id='fb-root'></div><script>(function(d, s, id) {var js, fjs = d.getElementsByTagName(s)[0];  if (d.getElementById(id)) return;  js = d.createElement(s); js.id = id;  js.src = '//connect.facebook.net/en_EN/all.js#xfbml=1';  fjs.parentNode.insertBefore(js, fjs);}(document, 'script', 'facebook-jssdk'));</script>";

            return new System.Web.Mvc.MvcHtmlString(htmlString + scriptString);
        }

        /// <summary>
        /// Twitter Button HTML
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="showCount">if set to <c>true</c> shows count.</param>
        /// <param name="isLarge">if set to <c>true</c> is large.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <param name="textToShare">The text to share.</param>
        /// <returns>
        /// TwitterButton Html
        /// </returns>
        public static System.Web.Mvc.MvcHtmlString TwitterButton(this System.Web.Mvc.HtmlHelper helper, bool showCount, bool isLarge, bool addText, string textToShare)
        {
            var shareUrl = string.Empty;

            var currentNode = SiteMapBase.GetCurrentProvider().CurrentNode;
            if (currentNode != null && currentNode.Url != null)
                shareUrl = RouteHelper.GetAbsoluteUrl(currentNode.Url);

            var countAttribute = "data-count='horizontal'";
            if (!showCount)
                countAttribute = "data-count='none'";

            var sizeAttribute = string.Empty;

            if (isLarge)
                sizeAttribute = "data-size='large'";

            var text = string.Empty;

            if (addText)
                text = "Tweet";

            var htmlString = string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                @"<a href='https://twitter.com/share' title='Share on Twitter' class='twitter-share-button' data-url={0} data-text={1} {2} {3}>{4}</a>",
                shareUrl,
                textToShare,
                sizeAttribute,
                countAttribute,
                text);

            var scriptString = @"<script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0],p=/^http:/.test(d.location)?'http':'https';if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src=p+'://platform.twitter.com/widgets.js';fjs.parentNode.insertBefore(js,fjs);}}(document, 'script', 'twitter-wjs');</script>";

            return new System.Web.Mvc.MvcHtmlString(htmlString + scriptString);
        }

        /// <summary>
        /// Twitter Button HTML
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="showCount">if set to <c>true</c> shows count.</param>
        /// <param name="isLarge">if set to <c>true</c> is large.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns>
        /// TwitterButton Html
        /// </returns>
        public static System.Web.Mvc.MvcHtmlString TwitterButton(this System.Web.Mvc.HtmlHelper helper, bool showCount, bool isLarge, bool addText)
        {
            return SocialShareButtons.TwitterButton(helper, showCount, isLarge, addText, string.Empty);
        }

        /// <summary>
        /// LinkedIn button
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="showCount">if set to <c>true</c> shows count.</param>
        /// <returns>
        /// LinkedInButton Html
        /// </returns>
        public static System.Web.Mvc.MvcHtmlString LinkedInButton(this System.Web.Mvc.HtmlHelper helper, bool showCount)
        {
            var shareUrl = string.Empty;
            var currentNode = SiteMapBase.GetCurrentProvider().CurrentNode;
            if (currentNode != null && currentNode.Url != null)
                shareUrl = RouteHelper.GetAbsoluteUrl(currentNode.Url);

            var countAttribute = string.Empty;
            if (showCount)
                countAttribute = "data-counter='right'";

            var scriptString = string.Format(
                                System.Globalization.CultureInfo.InvariantCulture,
                                @"<script src='//platform.linkedin.com/in.js' type='text/javascript'>lang: en_US</script><script type='IN/Share' data-url='{0}' {1}></script>",
                                shareUrl,
                                countAttribute);

            return new System.Web.Mvc.MvcHtmlString(scriptString);
        }

        /// <summary>
        /// GooglePlus One Button
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="showCount">if set to <c>true</c> shows count.</param>
        /// <param name="isLarge">if set to <c>true</c> is large.</param>
        /// <param name="addText">if set to <c>true</c> [add text].</param>
        /// <returns>
        /// GooglePlusButton Html
        /// </returns>
        public static System.Web.Mvc.MvcHtmlString GooglePlusOneButton(this System.Web.Mvc.HtmlHelper helper, bool showCount, bool isLarge, bool addText)
        {
            var countAttribute = string.Empty;

            if (!showCount)
                countAttribute = "data-annotation='none'";

            string sizeAttribute;

            if (isLarge || addText)
                sizeAttribute = "data-size='medium'";
            else
                sizeAttribute = "data-size='small'";

            var htmlString = string.Format(
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    @"<div class='g-plusone' {0} {1}></div>",
                                    sizeAttribute,
                                    countAttribute);

            var scriptString = @"<script type='text/javascript'>  (function() {   var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;    po.src = 'https://apis.google.com/js/platform.js';    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);  })();</script>";

            return new System.Web.Mvc.MvcHtmlString(htmlString + scriptString);
        }

        /// <summary>
        /// Bloggers the button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns>BloggerButton Html</returns>
        public static System.Web.Mvc.MvcHtmlString BloggerButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"https://www.blogger.com/blog_this.pyra?t&u=" + "{0}&n={1}";
            var linkText = "Blogger";
            var tooltipText = "Share on Blogger";
            var cssClass = "ss-icon ss-blogger";
            var htmlString = CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);
        }

        /// <summary>
        /// Thumbler button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns>ThumblerButton Html</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static System.Web.Mvc.MvcHtmlString ThumblerButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"https://www.tumblr.com/share?v=3&u=" + "{0}&t={1}";
            var linkText = "Tumblr";
            var tooltipText = "Share on Tumblr";
            var cssClass = "ss-icon ss-tumblr";
            var htmlString = CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);
        }

        /// <summary>
        /// Google bookmarks button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns>GoogleBookmarksButton Html</returns>
        public static System.Web.Mvc.MvcHtmlString GoogleBookmarksButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"https://www.google.com/bookmarks/mark?op=add&bkmk=" + "{0}&title={1}";
            var linkText = "Google bookmarks";
            var tooltipText = "Share on GoogleBookmarks";
            var cssClass = "ss-icon ss-googlebookmarks";
            var htmlString = SocialShareButtons.CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);
        }

        /// <summary>
        /// DeliciousButton Html
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> [add text].</param>
        /// <returns>Gets DeliciousButton Html</returns>
        public static System.Web.Mvc.MvcHtmlString DeliciousButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"https://delicious.com/save?v=5&noui&jump=close&url=" + "{0}&title={1}";
            var linkText = "Delicious";
            var tooltipText = "Share on Delicious";
            var cssClass = "ss-icon ss-delicious";
            var htmlString = SocialShareButtons.CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);
        }

        /// <summary>
        /// RedditButton Html
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns>Reddit Button Html</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static System.Web.Mvc.MvcHtmlString RedditButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"http://www.reddit.com/submit?newwindow=1&url=" + "{0}&title={1}";
            var linkText = "Reddit";
            var tooltipText = "Share on Reddit";
            var cssClass = "ss-icon ss-reddit";
            var htmlString = SocialShareButtons.CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);
        }

        /// <summary>
        /// StumbleUponButton Html
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns>StumbleUpon Button Html</returns>
        public static System.Web.Mvc.MvcHtmlString StumbleUponButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"http://stumbleupon.com/submit?url=" + "{0}&title={1}";
            var linkText = "Stumble upon";
            var tooltipText = "Share on StumbleUpon";
            var cssClass = "ss-icon ss-stumbleupon";
            var htmlString = CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);
        }

        /// <summary>
        /// Digg Button Html
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns>DiggButton Html</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public static System.Web.Mvc.MvcHtmlString DiggButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"http://digg.com/submit?url=" + "{0}&title={1}";
            var linkText = "Digg";
            var tooltipText = "Share on Digg";
            var cssClass = "ss-icon ss-digg";
            var htmlString = SocialShareButtons.CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);
        }

        /// <summary>
        /// MySpace Button Html
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns>MySpaceButton Html</returns>
        public static System.Web.Mvc.MvcHtmlString MySpaceButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"http://myspace.com/Modules/PostTo/Pages/?u=" + "{0}&t={1}";
            var linkText = "My Space";
            var tooltipText = "Share on MySpace";
            var cssClass = "ss-icon ss-myspace";
            var htmlString = SocialShareButtons.CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);
        }

        /// <summary>
        /// Creates the link button.
        /// </summary>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="tooltipText">The tooltip text.</param>
        /// <param name="cssClass">The CSS class.</param>
        /// <returns>
        /// CreateLinkButton Html
        /// </returns>
        private static string CreateLinkButton(bool addText, string baseUrl, string linkText, string tooltipText, string cssClass)
        {
            var shareUrl = string.Empty;
            var currentNode = SiteMapBase.GetCurrentProvider().CurrentNode;

            var title = string.Empty;

            if (currentNode != null && currentNode.Url != null)
            {
                shareUrl = RouteHelper.GetAbsoluteUrl(currentNode.Url);
                title = currentNode.Title;
            }

            var url = string.Format(System.Globalization.CultureInfo.InvariantCulture, baseUrl, Uri.EscapeUriString(shareUrl), Uri.EscapeUriString(title));
            var clickScript = string.Format(System.Globalization.CultureInfo.InvariantCulture, @"window.open('{0}', '{1}','toolbar=no,width=550,height=550'); return false", url, linkText);

            var text = string.Empty;

            if (addText)
                text = linkText;

            var htmlString = string.Format(System.Globalization.CultureInfo.InvariantCulture, @"<a onclick=""{0}"" title=""{1}""><span class=""{3}""></span>{2}</a>", clickScript, tooltipText, text, cssClass);
            return htmlString;
        }
    }
}
