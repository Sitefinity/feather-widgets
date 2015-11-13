using System;
using System.Globalization;
using System.Web;
using System.Web.Helpers;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
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
                loginStatusController.Model.AllowWindowsStsLogin = true;
                mvcProxy.Settings = new ControllerSettings(loginStatusController);

                pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                var responseContent = PageInvoker.ExecuteWebRequest(loginStatusPageUrl);
                Assert.IsTrue(responseContent.Contains(this.GetExpectedUrlWithParams(absoluteUrl)), "Login redirect url is not as expected");
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
                    Assert.IsTrue(responseContent.Contains(HttpUtility.UrlEncode(absoluteUrl)), "Logout redirect url is not as expected");
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
