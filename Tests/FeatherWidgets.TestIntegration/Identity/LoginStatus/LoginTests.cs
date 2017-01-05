using System;
using System.Globalization;
using System.Net;
using System.Web;
using System.Web.Helpers;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestIntegration.Helpers;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Identity.LoginStatus
{
    /// <summary>
    /// This is a test class with tests related to login functionality of LoginStatus widget.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), TestFixture]
    [Description("This is a test class with tests related to login functionality of LoginStatus widget.")]
    public class LoginTests
    {
        /// <summary>
        /// Verify that the login status widget does not throw exception when viewed from details page.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestIntegration.Data.Content.PageContentGenerator.AddControlToPage(System.Guid,System.Web.UI.Control,System.String,System.String,System.Action<Telerik.Sitefinity.Pages.Model.PageDraftControl>)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.SitefinityTeam3)]
        [Description("Verify that the login status widget does not throw exception when viewed from details page.")]
        public void LoginStatus_OnDetailsPage_DoesNotThrowException()
        {
            const string LoginStatusCaption = "login status";
            Guid newsItemId = Guid.Parse("4785b751-ce3a-4e5e-ba81-138f8f2a8a09");
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;

            var mvcNewsProxy = new MvcControllerProxy();
            mvcNewsProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.OpenInSamePage = true;
            mvcNewsProxy.Settings = new ControllerSettings(newsController);

            var mvcLoginStatusProxy = new MvcControllerProxy();
            mvcLoginStatusProxy.ControllerName = typeof(LoginStatusController).FullName;

            using (var generator = new PageContentGenerator())
            {
                var pageId = generator.CreatePageWithWidget(mvcNewsProxy, null, testName, testName, testName, 0);
                PageContentGenerator.AddControlToPage(pageId, mvcLoginStatusProxy, LoginStatusCaption);

                string pageUrl = UrlPath.ResolveAbsoluteUrl("~/" + testName + "0");
                var newsManager = NewsManager.GetManager();
                try
                {
                    var newsItem = newsManager.CreateNewsItem(newsItemId);
                    newsItem.Title = testName;
                    newsItem.UrlName = testName;
                    newsManager.Lifecycle.Publish(newsItem);
                    newsManager.SaveChanges();

                    string detailNewsUrl = pageUrl + newsItem.ItemDefaultUrl;
                    var pageContent = WebRequestHelper.GetPageWebContent(RouteHelper.GetAbsoluteUrl(detailNewsUrl));

                    Assert.DoesNotContain(pageContent, "Exception occured while executing the controller. Check error logs for details.", StringComparison.Ordinal);
                }
                finally
                {
                    newsManager.Delete(newsManager.GetNewsItem(newsItemId));
                    newsManager.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Verify that the login status widget does not resolve non existing URLs on a page.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestIntegration.Data.Content.PageContentGenerator.AddControlToPage(System.Guid,System.Web.UI.Control,System.String,System.String,System.Action<Telerik.Sitefinity.Pages.Model.PageDraftControl>)")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.SitefinityTeam3)]
        [Description("Verify that the login status widget does not resolve non existing URLs on a page.")]
        public void LoginStatus_NonExistingUrl_Returns404()
        {
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;

            var mvcLoginStatusProxy = new MvcControllerProxy();
            mvcLoginStatusProxy.ControllerName = typeof(LoginStatusController).FullName;

            using (var generator = new PageContentGenerator())
            {
                generator.CreatePageWithWidget(mvcLoginStatusProxy, null, testName, testName, testName, 0);

                var pageUrl = UrlPath.ResolveAbsoluteUrl("~/" + testName + "0");
                try
                {
                    HttpWebRequest.Create(pageUrl + "/non-existing-page").GetResponse();
                }
                catch (WebException ex)
                {
                    var response = (HttpWebResponse)ex.Response;
                    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                }
            }
        }

        #region Login

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verify when External login page url is provided, redirect link is constructed correctly.")]
        public void Login_RedirectToExternalPage_VerifyLoginRedirectUrlIsCorrect()
        {
            var pageOperations = new PagesOperations();

            try
            {
                string loginStatusPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
                string expectedLoginUrl = "www.telerik.com";

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
                var loginStatusController = new LoginStatusController();
                loginStatusController.Model.ExternalLoginUrl = expectedLoginUrl;
                mvcProxy.Settings = new ControllerSettings(loginStatusController);

                pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                var responseContent = PageInvoker.ExecuteWebRequest(loginStatusPageUrl);
                Assert.IsTrue(responseContent.Contains(expectedLoginUrl), "Login redirect url is not as expected");
            }
            finally
            {
                pageOperations.DeletePages();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verify when Page id is provided, redirect link is constructed correctly.")]
        [Ignore("Failing over SSL - #203166")]
        public void Login_WithPageId_VerifyLoginRedirectUrlIsCorrect()
        {
            var pageOperations = new PagesOperations();

            try
            {
                string loginStatusPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
                var basicPageOperations = new Telerik.Sitefinity.TestUtilities.CommonOperations.PagesOperations();
                var pageTitle = "Page Title1";
                var pageUrl = "PageTitle1";
                var absoluteUrl = UrlPath.GetDomainUrl() + "/" + pageUrl;
                try
                {
                    var loginPageId = basicPageOperations.CreatePage(pageTitle, pageUrl);

                    var mvcProxy = new MvcControllerProxy();
                    mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
                    var loginStatusController = new LoginStatusController();
                    loginStatusController.Model.LoginPageId = loginPageId;
                    mvcProxy.Settings = new ControllerSettings(loginStatusController);

                    pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                    var responseContent = PageInvoker.ExecuteWebRequest(loginStatusPageUrl);
                    Assert.IsTrue(responseContent.Contains(this.GetExpectedUrlWithParams(absoluteUrl)), "Login redirect url is not as expected");
                }
                finally
                {
                    basicPageOperations.DeleteAllPages();
                }
            }
            finally
            {
                pageOperations.DeletePages();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verify when AllowWindowsStsLogin is set to true, redirect link is constructed correctly.")]
        [Ignore("Failing over SSL - #203166")]
        public void Login_WithInstantLogin_VerifyLoginRedirectUrlIsCorrect()
        {
            var pageOperations = new PagesOperations();

            try
            {
                string loginStatusPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
                var absoluteUrl = UrlPath.GetDomainUrl() + "/Sitefinity/Authenticate/SWT";
                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
                var loginStatusController = new LoginStatusController();                
                mvcProxy.Settings = new ControllerSettings(loginStatusController);

                pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                var responseContent = PageInvoker.ExecuteWebRequest(loginStatusPageUrl);
                var expectedUrl = this.GetExpectedUrlWithParams(absoluteUrl);
                Assert.IsTrue(responseContent.Contains(expectedUrl), "{0}\n{1}\n{2}", "Login redirect url is not as expected", expectedUrl, responseContent);
            }
            finally
            {
                pageOperations.DeletePages();
            }
        }

        #endregion

        #region Logout

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verify when External logout page url is provided, redirect link is constructed correctly.")]
        public void Logout_RedirectToExternalPage_VerifyLogoutRedirectUrlIsCorrect()
        {
            var pageOperations = new PagesOperations();

            try
            {
                string logoutStatusPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
                string expectedLogoutUrl = "www.telerik.com";

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
                var loginStatusController = new LoginStatusController();
                loginStatusController.Model.ExternalLogoutUrl = expectedLogoutUrl;
                mvcProxy.Settings = new ControllerSettings(loginStatusController);

                pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                var responseContent = PageInvoker.ExecuteWebRequest(logoutStatusPageUrl);
                Assert.IsTrue(responseContent.Contains(expectedLogoutUrl), "Logout redirect url is not as expected");
            }
            finally
            {
                pageOperations.DeletePages();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verify when logout Page id is provided, redirect link is constructed correctly.")]
        public void Logout_WithPageId_VerifyLogoutRedirectUrlIsCorrect()
        {
            var pageOperations = new PagesOperations();

            try
            {
                string logoutStatusPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
                var basicPageOperations = new Telerik.Sitefinity.TestUtilities.CommonOperations.PagesOperations();
                var pageTitle = "Page Title1";
                var pageUrl = "PageTitle1";
                var absoluteUrl = UrlPath.GetDomainUrl() + "/" + pageUrl;
                try
                {
                    var logoutPageId = basicPageOperations.CreatePage(pageTitle, pageUrl);

                    var mvcProxy = new MvcControllerProxy();
                    mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
                    var loginStatusController = new LoginStatusController();
                    loginStatusController.Model.LogoutPageId = logoutPageId;
                    mvcProxy.Settings = new ControllerSettings(loginStatusController);

                    pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);

                    var responseContent = PageInvoker.ExecuteWebRequest(logoutStatusPageUrl);
                    Assert.IsTrue(responseContent.Contains(absoluteUrl), "Logout redirect url is not as expected");
                }
                finally
                {
                    basicPageOperations.DeleteAllPages();
                }
            }
            finally
            {
                pageOperations.DeletePages();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Verify when logout Page id and external redirect url are provided, redirect link is constructed correctly.")]
        public void Logout_WithPageIdAndRedirectUrl_VerifyLogoutRedirectUrlIsCorrect()
        {
            var pageOperations = new PagesOperations();

            try
            {
                string expectedLogoutUrl = "www.telerik.com";
                string logoutStatusPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
                var basicPageOperations = new Telerik.Sitefinity.TestUtilities.CommonOperations.PagesOperations();
                var pageTitle = "Page Title1";
                var pageUrl = "PageTitle1";
                try
                {
                    var logoutPageId = basicPageOperations.CreatePage(pageTitle, pageUrl);

                    var mvcProxy = new MvcControllerProxy();
                    mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
                    var loginStatusController = new LoginStatusController();
                    loginStatusController.Model.LogoutPageId = logoutPageId;
                    loginStatusController.Model.ExternalLogoutUrl = expectedLogoutUrl;
                    mvcProxy.Settings = new ControllerSettings(loginStatusController);

                    pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                    var responseContent = PageInvoker.ExecuteWebRequest(logoutStatusPageUrl);
                    Assert.IsTrue(responseContent.Contains(expectedLogoutUrl), "Logout redirect url is not as expected");
                }
                finally
                {
                    basicPageOperations.DeleteAllPages();
                }
            }
            finally
            {
                pageOperations.DeletePages();
            }
        }
        
        #endregion

        #region Status

        /// <summary>
        /// Test that Status has available endpoint.
        /// </summary>
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Test that Status has available endpoint.")]
        [Test]
        public void Status_TestRouting_Available()
        {
            HttpContext.Current.Request.Headers["Authorization"] = string.Empty;
            string statusUrl = UrlPath.ResolveAbsoluteUrl("~/rest-api/login-status");
            var responseContent = PageInvoker.ExecuteWebRequest(statusUrl);
            var statusResponse = Json.Decode(responseContent, typeof(StatusViewModel));

            Assert.IsFalse(statusResponse.IsLoggedIn);
        }

        #endregion

        #region Private members

        /// <summary>
        /// Gets the expected URL with parameters.
        /// </summary>
        /// <param name="redirectUrl">The redirect URL.</param>
        /// <returns></returns>
        private string GetExpectedUrlWithParams(string redirectUrl)
        {
            var currentUrl = string.Format(CultureInfo.InvariantCulture, "/{0}?sf-hru=true", this.urlNamePrefix + this.pageIndex);
            var realm = UrlPath.GetDomainUrl() + "/";
            string urlFormat = "{0}?realm={1}&amp;redirect_uri={2}&amp;deflate=true";
            var urlWithParams = urlFormat.Arrange(redirectUrl, realm, HttpUtility.UrlEncode(currentUrl));

            return urlWithParams;
        }

        private readonly string pageNamePrefix = "LoginStatusPage";
        private readonly string pageTitlePrefix = "Login Status";
        private readonly string urlNamePrefix = "login-status";
        private readonly int pageIndex = 1;

        #endregion
    }
}
