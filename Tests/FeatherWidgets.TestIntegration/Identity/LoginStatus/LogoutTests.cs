using System;
using System.Linq;
using System.Web.Mvc;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;

namespace FeatherWidgets.TestIntegration.Identity.LoginStatus
{
    /// <summary>
    /// This is a test class with tests related to logout functionality of LoginStatus widget.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout")]
    [Description("This is a test class with tests related to logout functionality of LoginStatus widget.")]
    [TestFixture]
    public class LogoutTests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout")]
        [Author(FeatherTeams.Team2)]
        [Category(TestCategories.Identity)]
        [Description("Verify when External logout page url is provided, redirect link is constructed correctly.")]
        [Test]
        public void Logout_RedirectToExternalPage_VerifyLogoutRedirectUrlIsCorrect()
        {
            string expectedLogoutUrl = "www.telerik.com";

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
            var controller = new LoginStatusController();
            controller.Model.ExternalLogoutUrl = expectedLogoutUrl;
            mvcProxy.Settings = new ControllerSettings(controller);

            var actionResult = (ViewResult)controller.Index();
            var viewModel = actionResult.Model as LoginStatusViewModel;

            Assert.AreEqual(expectedLogoutUrl, viewModel.LogoutPageUrl, "Logout redirect url is not as expected");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout")]
        [Author(FeatherTeams.Team2)]
        [Category(TestCategories.Identity)]
        [Description("Verify that a logged in user is successfully logged out.")]
        [Test]
        public void Logout_LogoutUser_VerifyUserIsLoggedOut()
        {
            string expectedLogoutUrl = "www.telerik.com";

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
            var controller = new LoginStatusController();
            controller.Model.ExternalLogoutUrl = expectedLogoutUrl;
            mvcProxy.Settings = new ControllerSettings(controller);

            var actionResult = (ViewResult)controller.Index();
            var viewModel = actionResult.Model as LoginStatusViewModel;

            Assert.AreEqual(expectedLogoutUrl, viewModel.LogoutPageUrl, "Logout redirect url is not as expected");
        }
    }
}
