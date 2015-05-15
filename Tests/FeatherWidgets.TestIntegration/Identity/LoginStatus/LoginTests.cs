using System;
using System.Globalization;
using System.Web;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
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
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pageOperations = new PagesOperations();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.pageOperations.DeletePages();
        }

        #region Login

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team2)]
        [Description("Verify when External login page url is provided, redirect link is constructed correctly.")]
        public void Login_RedirectToExternalPage_VerifyLoginRedirectUrlIsCorrect()
        {
            string loginStatusPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
            string expectedLoginUrl = "www.telerik.com";

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
            var loginStatusController = new LoginStatusController();
            loginStatusController.Model.ExternalLoginUrl = expectedLoginUrl;
            mvcProxy.Settings = new ControllerSettings(loginStatusController);

            this.pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
            var responseContent = PageInvoker.ExecuteWebRequest(loginStatusPageUrl);
            Assert.IsTrue(responseContent.Contains(expectedLoginUrl), "Login redirect url is not as expected");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team2)]
        [Description("Verify when Page id is provided, redirect link is constructed correctly.")]
        public void Login_WithPageId_VerifyLoginRedirectUrlIsCorrect()
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

                this.pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                var responseContent = PageInvoker.ExecuteWebRequest(loginStatusPageUrl);
                Assert.IsTrue(responseContent.Contains(this.GetExpectedUrlWithParams(absoluteUrl)), "Login redirect url is not as expected");
            }
            finally 
            {
                basicPageOperations.DeleteAllPages();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team2)]
        [Description("Verify when AllowWindowsStsLogin is set to true, redirect link is constructed correctly.")]
        public void Login_WithInstantLogin_VerifyLoginRedirectUrlIsCorrect()
        {
            string loginStatusPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
            var absoluteUrl = UrlPath.GetDomainUrl() + "/Sitefinity/Authenticate/SWT";
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
            var loginStatusController = new LoginStatusController();
            loginStatusController.Model.AllowWindowsStsLogin = true;
            mvcProxy.Settings = new ControllerSettings(loginStatusController);

            this.pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
            var responseContent = PageInvoker.ExecuteWebRequest(loginStatusPageUrl);
            Assert.IsTrue(responseContent.Contains(this.GetExpectedUrlWithParams(absoluteUrl)), "Login redirect url is not as expected");
        }

        #endregion

        #region Logout

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team2)]
        [Description("Verify when External logout page url is provided, redirect link is constructed correctly.")]
        public void Logout_RedirectToExternalPage_VerifyLogoutRedirectUrlIsCorrect()
        {
            string logoutStatusPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
            string expectedLogoutUrl = "www.telerik.com";

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
            var loginStatusController = new LoginStatusController();
            loginStatusController.Model.ExternalLogoutUrl = expectedLogoutUrl;
            mvcProxy.Settings = new ControllerSettings(loginStatusController);

            this.pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
            var responseContent = PageInvoker.ExecuteWebRequest(logoutStatusPageUrl);
            Assert.IsTrue(responseContent.Contains(expectedLogoutUrl), "Logout redirect url is not as expected");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team2)]
        [Description("Verify when logout Page id is provided, redirect link is constructed correctly.")]
        public void Logout_WithPageId_VerifyLogoutRedirectUrlIsCorrect()
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

                this.pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);

                var responseContent = PageInvoker.ExecuteWebRequest(logoutStatusPageUrl);
                Assert.IsTrue(responseContent.Contains(HttpUtility.UrlEncode(absoluteUrl)), "Logout redirect url is not as expected");
            }
            finally
            {
                basicPageOperations.DeleteAllPages();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team2)]
        [Description("Verify when logout Page id and external redirect url are provided, redirect link is constructed correctly.")]
        public void Logout_WithPageIdAndRedirectUrl_VerifyLogoutRedirectUrlIsCorrect()
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

                this.pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                var responseContent = PageInvoker.ExecuteWebRequest(logoutStatusPageUrl);
                Assert.IsTrue(responseContent.Contains(expectedLogoutUrl), "Logout redirect url is not as expected");
            }
            finally
            {
                basicPageOperations.DeleteAllPages();
            }
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

        private PagesOperations pageOperations;
        private string pageNamePrefix = "LoginStatusPage";
        private string pageTitlePrefix = "Login Status";
        private string urlNamePrefix = "login-status";
        private int pageIndex = 1;

        #endregion
    }
}
