using System.Globalization;
using System.Web;
using System.Web.Mvc;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team2)]
        [Description("Verify when External login page url is provided, redirect link is constructed correctly.")]
        public void Login_RedirectToExternalPage_VerifyLoginRedirectUrlIsCorrect()
        {
            string expectedLoginUrl = "www.telerik.com";

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
            var loginStatusController = new LoginStatusController();
            loginStatusController.Model.ExternalLoginUrl = expectedLoginUrl;
            mvcProxy.Settings = new ControllerSettings(loginStatusController);

            var actionResult = (ViewResult)loginStatusController.Index();
            var loginStatusViewModel = actionResult.Model as LoginStatusViewModel;
            Assert.AreEqual(expectedLoginUrl, loginStatusViewModel.LoginPageUrl, "Login redirect url is not as expected");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team2)]
        [Description("Verify when Page id is provided, redirect link is constructed correctly.")]
        public void Login_WithPageId_VerifyLoginRedirectUrlIsCorrect()
        {
            var pageOperations = new PagesOperations();
            var pageTitle = "Page Title1";
            var pageUrl = "PageTitle1";
            var absoluteUrl = UrlPath.GetDomainUrl() + "/" + pageUrl;

            try
            {
                var loginPageId = pageOperations.CreatePage(pageTitle, pageUrl);

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
                var loginStatusController = new LoginStatusController();
                loginStatusController.Model.LoginPageId = loginPageId;
                mvcProxy.Settings = new ControllerSettings(loginStatusController);

                var actionResult = (ViewResult)loginStatusController.Index();
                var loginStatusViewModel = actionResult.Model as LoginStatusViewModel;
                Assert.AreEqual(this.GetExpectedUrlWithParams(absoluteUrl), loginStatusViewModel.LoginPageUrl, "Login redirect url is not as expected");
            }
            finally
            {
                pageOperations.DeleteAllPages();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team2)]
        [Description("Verify when AllowInstantLogin is set to true, redirect link is constructed correctly.")]
        public void Login_WithInstantLogin_VerifyLoginRedirectUrlIsCorrect()
        {
            var absoluteUrl = UrlPath.GetDomainUrl() + "/Sitefinity/Authenticate/SWT";
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
            var loginStatusController = new LoginStatusController();
            loginStatusController.Model.AllowInstantLogin = true;
            mvcProxy.Settings = new ControllerSettings(loginStatusController);

            var actionResult = (ViewResult)loginStatusController.Index();
            var loginStatusViewModel = actionResult.Model as LoginStatusViewModel;
            Assert.AreEqual(this.GetExpectedUrlWithParams(absoluteUrl), loginStatusViewModel.LoginPageUrl, "Login redirect url is not as expected");
        }

        /// <summary>
        /// Gets the expected URL with parameters.
        /// </summary>
        /// <param name="redirectUrl">The redirect URL.</param>
        /// <returns></returns>
        private string GetExpectedUrlWithParams(string redirectUrl)
        {
            var currentUrl = HttpContext.Current.Request.RawUrl + "&sf-hru=true";
            var realm = UrlPath.GetDomainUrl() + "/";
            var urlFormat = "{0}?realm={1}&redirect_uri={2}&deflate=true";
            var urlWithParams = string.Format(CultureInfo.InvariantCulture, urlFormat, redirectUrl, realm, HttpUtility.UrlEncode(currentUrl));

            return urlWithParams;
        }
    }
}
