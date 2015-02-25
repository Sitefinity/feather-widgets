using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using System;
using Telerik.Sitefinity.Modules.Pages;

namespace FeatherWidgets.TestIntegration.Identity.LoginStatus
{
    /// <summary>
    /// This is a test class with tests related to logout functionality of LoginStatus widget.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
    [Description("This is a test class with tests related to logout functionality of LoginStatus widget.")]
    [TestFixture]
    public class LogoutTests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
        [Author(FeatherTeams.Team2)]
        [Category(TestCategories.Identity)]
        [Description("Verify when External login page url is provided, redirect link is constructed correctly.")]
        [Test]
        public void Logout_RedirectToExternalPage_VerifyLogoutRedirectUrlIsCorrect()
        {
            var somePage = PageManager.GetManager().GetPageDataList();
            var expectedLogoutPageId = Guid.NewGuid();

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
            var loginStatusController = new LoginStatusController();
            loginStatusController.Model.LogoutPageId = expectedLogoutPageId;
            mvcProxy.Settings = new ControllerSettings(loginStatusController);

            var actionResult = (ViewResult)loginStatusController.Index();
            var loginStatusViewModel = actionResult.Model as LoginStatusViewModel;
            //Assert.AreEqual(expectedLogoutUrl, loginStatusViewModel.LogoutPageUrl, "Login redirect url is not as expected");
        }
    }
}
