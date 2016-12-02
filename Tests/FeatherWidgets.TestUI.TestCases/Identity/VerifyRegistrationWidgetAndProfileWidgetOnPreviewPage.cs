using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Identity
{
    /// <summary>
    /// VerifyRegistrationWidgetAndProfileWidgetOnPreviewPage_ test class.
    /// </summary>
    [TestClass]
    public class VerifyRegistrationWidgetAndProfileWidgetOnPreviewPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyRegistrationWidgetAndProfileWidgetOnPreviewPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam4),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Bootstrap),
        Telerik.TestUI.Core.Attributes.KnownIssue(BugId = 206481), Ignore]
        public void VerifyRegistrationWidgetAndProfileWidgetOnPreviewPage()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", false));
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BAT.Wrappers().Backend().Pages().PagesWrapper().PreviewPage(PageTitle, isEditMode: true);
            ////Verify save changes message for Profile widget in Preview mode
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();
            Assert.IsTrue(ActiveBrowser.ContainsText(MessageSaveChangesProfileWidget), "Message was not found on the page");
            ////Verify all required fields message for Registration widget in Preview mode
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().RegisterButton();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertEmptyEmailFieldMessage();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertEmptyUsernameFieldMessage();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertEmptyPasswordFieldMessage();
            ////Verify successful message for Registration widget in Preview mode
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillEmail(Email);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillUserName(UserName);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillPassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillRetypePassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().RegisterButton();
            ActiveBrowser.WaitForElement("TextContent=~Thank you!");
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().VerifySuccessfullyMessage();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageTitle = "RegistrationPage";
        private const string Email = "user20@dsds.bg";
        private const string UserName = "newUser";
        private const string Password = "password";
        private const string MessageSaveChangesProfileWidget = "Saving changes is not available in Preview";
    }
}
