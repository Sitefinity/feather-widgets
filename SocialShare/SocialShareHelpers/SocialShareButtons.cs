using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Web;

namespace SocialShare.SocialShareHelpers
{
    /// <summary>
    /// Html helpers for the social share buttons
    /// </summary>
    public static class SocialShareButtons
    {
        /// <summary>
        /// Facebooks the button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="showCount">if set to <c>true</c> shows count.</param>
        /// <param name="isLarge">if set to <c>true</c> is large.</param>
        /// <returns></returns>
        public static System.Web.Mvc.MvcHtmlString FacebookButton(this System.Web.Mvc.HtmlHelper helper, bool showCount, bool isLarge, bool addText)
        {
            var currentNode = Telerik.Sitefinity.Web.SiteMapBase.GetCurrentProvider().CurrentNode;
            var shareUrl="";
            if (currentNode != null && currentNode.Url != null)
                shareUrl = RouteHelper.GetAbsoluteUrl(Telerik.Sitefinity.Web.SiteMapBase.GetCurrentProvider().CurrentNode.Url);

            var buttonTypeAttribute = "";
            if (showCount)
                buttonTypeAttribute = "data-type='button_count'";
            else if (isLarge || addText)
                buttonTypeAttribute = "data-type='button'";
            else
                buttonTypeAttribute = "data-type='icon'";

            var htmlString = String.Format(@"<div class='fb-share-button' data-href='{0}' {1}></div>",
                shareUrl, buttonTypeAttribute);
            var scriptString = @"<div id='fb-root'></div><script>(function(d, s, id) {var js, fjs = d.getElementsByTagName(s)[0];  if (d.getElementById(id)) return;  js = d.createElement(s); js.id = id;  js.src = '//connect.facebook.net/en_EN/all.js#xfbml=1';  fjs.parentNode.insertBefore(js, fjs);}(document, 'script', 'facebook-jssdk'));</script>";

            return new System.Web.Mvc.MvcHtmlString(htmlString + scriptString);
        }

        /// <summary>
        /// Twitters the button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="showCount">if set to <c>true</c> shows count.</param>
        /// <param name="isLarge">if set to <c>true</c> is large.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <param name="textToShare">The text to share.</param>
        /// <returns></returns>
        public static System.Web.Mvc.MvcHtmlString TwitterButton(this System.Web.Mvc.HtmlHelper helper, bool showCount, bool isLarge, bool addText, string textToShare = "")
        {
            var shareUrl = "";
            var currentNode = Telerik.Sitefinity.Web.SiteMapBase.GetCurrentProvider().CurrentNode;
            if (currentNode != null && currentNode.Url != null)
                shareUrl = RouteHelper.GetAbsoluteUrl(Telerik.Sitefinity.Web.SiteMapBase.GetCurrentProvider().CurrentNode.Url);

            var countAttribute = "data-count='horizontal'";
            if (!showCount)
                countAttribute = "data-count='none'";

            var sizeAttribute = "";
            if (isLarge)
                sizeAttribute = "data-size='large'";

            var text = "";
            if (addText)
                text = "Tweet";

            var htmlString = String.Format(@"<a href='https://twitter.com/share' title='Share on Twitter' class='twitter-share-button' data-url={0} data-text={1} {2} {3}>{4}</a>",
                shareUrl, textToShare, sizeAttribute, countAttribute, text);
            var scriptString = @"<script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0],p=/^http:/.test(d.location)?'http':'https';if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src=p+'://platform.twitter.com/widgets.js';fjs.parentNode.insertBefore(js,fjs);}}(document, 'script', 'twitter-wjs');</script>";

            return new System.Web.Mvc.MvcHtmlString(htmlString + scriptString);
        }


        /// <summary>
        /// Linkeds the in button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="showCount">if set to <c>true</c> shows count.</param>
        /// <returns></returns>
        public static System.Web.Mvc.MvcHtmlString LinkedInButton(this System.Web.Mvc.HtmlHelper helper,  bool showCount)
        {
            var shareUrl = "";
            var currentNode = Telerik.Sitefinity.Web.SiteMapBase.GetCurrentProvider().CurrentNode;
            if (currentNode != null && currentNode.Url != null)
                shareUrl = RouteHelper.GetAbsoluteUrl(Telerik.Sitefinity.Web.SiteMapBase.GetCurrentProvider().CurrentNode.Url);

            var countAttribute = "";
            if (showCount)
                countAttribute = "data-counter='right'";


            var scriptString = String.Format(@"<script src='//platform.linkedin.com/in.js' type='text/javascript'>lang: en_US</script><script type='IN/Share' data-url='{0}' {1}></script>",
                shareUrl, countAttribute);

            return new System.Web.Mvc.MvcHtmlString(scriptString);
        }

        /// <summary>
        /// Googles the plus one button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="showCount">if set to <c>true</c> shows count.</param>
        /// <param name="isLarge">if set to <c>true</c> is large.</param>
        /// <returns></returns>
        public static System.Web.Mvc.MvcHtmlString GooglePlusOneButton(this System.Web.Mvc.HtmlHelper helper, bool showCount, bool isLarge, bool addText)
        {
            var countAttribute = "";
            if (!showCount)
                countAttribute = "data-annotation='none'";

            var sizeAttribute = "";
            if (isLarge || addText)
                sizeAttribute = "data-size='medium'";
            else
                sizeAttribute = "data-size='small'";

            var htmlString = String.Format(@"<div class='g-plusone' {0} {1}></div>",
                sizeAttribute, countAttribute);
            var scriptString = @"<script type='text/javascript'>  (function() {   var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;    po.src = 'https://apis.google.com/js/platform.js';    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);  })();</script>";

            return new System.Web.Mvc.MvcHtmlString(htmlString + scriptString);
        }

        /// <summary>
        /// Bloggers the button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns></returns>
        public static System.Web.Mvc.MvcHtmlString BloggerButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"https://www.blogger.com/blog_this.pyra?t&u=" + "{0}&n={1}";
            var linkText = "Blogger";
            var tooltipText = "Share on Blogger";
            var cssClass = "ss-icon ss-blogger";
            var htmlString = SocialShareButtons.CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);
        }


        /// <summary>
        /// Thumblers the button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns></returns>
        public static System.Web.Mvc.MvcHtmlString ThumblerButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"https://www.tumblr.com/share?v=3&u=" + "{0}&t={1}";
            var linkText = "Tumblr";
            var tooltipText = "Share on Tumblr";
            var cssClass = "ss-icon ss-tumblr";
            var htmlString = SocialShareButtons.CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);
        }

        /// <summary>
        /// Googles the bookmarks button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns></returns>
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
        /// Deliciouses the button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns></returns>
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
        /// Reddits the button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns></returns>
        public static System.Web.Mvc.MvcHtmlString RedditButton(this System.Web.Mvc.HtmlHelper helper,bool addText)
        {
            var baseUrl = @"http://www.reddit.com/submit?newwindow=1&url=" + "{0}&title={1}";
            var linkText = "Reddit";
            var tooltipText = "Share on Reddit";
            var cssClass = "ss-icon ss-reddit";
            var htmlString = SocialShareButtons.CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);            
        }

        /// <summary>
        /// Stumbles the upon button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns></returns>
        public static System.Web.Mvc.MvcHtmlString StumbleUponButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"http://stumbleupon.com/submit?url=" + "{0}&title={1}";
            var linkText = "Stumble upon";
            var tooltipText = "Share on StumbleUpon";
            var cssClass = "ss-icon ss-stumbleupon";
            var htmlString = SocialShareButtons.CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);

            return new System.Web.Mvc.MvcHtmlString(htmlString);
        }

        /// <summary>
        /// Diggs the button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns></returns>
        public static System.Web.Mvc.MvcHtmlString DiggButton(this System.Web.Mvc.HtmlHelper helper, bool addText)
        {
            var baseUrl = @"http://digg.com/submit?url=" + "{0}&title={1}";
            var linkText = "Digg";
            var tooltipText = "Share on Digg";
            var cssClass = "ss-icon ss-digg";
            var htmlString = SocialShareButtons.CreateLinkButton(addText, baseUrl, linkText, tooltipText, cssClass);          

            return new System.Web.Mvc.MvcHtmlString(htmlString );
        }

        /// <summary>
        /// Mies the space button.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addText">if set to <c>true</c> adds text.</param>
        /// <returns></returns>
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
        /// <returns></returns>
        private static string CreateLinkButton(bool addText, string baseUrl, string linkText, string tooltipText, string cssClass)
        {
            var shareUrl = "";
            var currentNode = Telerik.Sitefinity.Web.SiteMapBase.GetCurrentProvider().CurrentNode;
            if (currentNode != null && currentNode.Url != null)
                shareUrl = RouteHelper.GetAbsoluteUrl(Telerik.Sitefinity.Web.SiteMapBase.GetCurrentProvider().CurrentNode.Url);
            var title = currentNode.Title;

            var url = String.Format(baseUrl, Uri.EscapeUriString(shareUrl), Uri.EscapeUriString(title));
            var clickScript = String.Format(@"window.open('{0}', '{1}','toolbar=no,width=550,height=550'); return false", url, linkText);

            var text = "";
            if (addText)
                text = linkText;

            var htmlString = String.Format(@"<a onclick=""{0}"" title=""{1}""><span class=""{3}""></span>{2}</a>", clickScript, tooltipText, text, cssClass);
            return htmlString;
        }
    }
}
