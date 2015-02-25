using System.Web.Mvc;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;

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

        #region Fields and constants

        #endregion
    }
}
