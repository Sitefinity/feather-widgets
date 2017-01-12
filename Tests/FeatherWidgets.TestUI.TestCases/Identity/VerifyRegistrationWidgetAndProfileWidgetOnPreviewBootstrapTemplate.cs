using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using ArtOfTest.WebAii.Core;

namespace FeatherWidgets.TestUI.TestCases.Identity
{
    /// <summary>
    /// VerifyRegistrationWidgetAndProfileWidgetOnPreviewBootstrapTemplate_ test class.
    /// </summary>
    [TestClass]
    public class VerifyRegistrationWidgetAndProfileWidgetOnPreviewBootstrapTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyRegistrationWidgetAndProfileWidgetOnPreviewBootstrapTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam4),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Identity),
        TestCategory(FeatherTestCategories.Registration),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void VerifyRegistrationWidgetAndProfileWidgetOnPreviewBootstrapTemplate()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", false));
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(ProfileWidget, PlaceHolderId);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(RegistrationWidget, PlaceHolderId);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PreviewTemplateFromEdit();
            ActiveBrowser.WaitUntilReady();
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
            BAT.Wrappers().Backend().Preview().RealPreviewWrapper().CloseBrowserWithPreview();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationNameDelete);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationNameDelete);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
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

        private const string TemplateTitle = "Bootstrap.default";
        private const string Email = "user20@dsds.bg";
        private const string UserName = "newUser";
        private const string Password = "password";
        private const string MessageSaveChangesProfileWidget = "Saving changes is not available in Preview";
        private const string OperationNameDelete = "Delete";
        private const string ProfileWidget = "Profile";
        private const string RegistrationWidget = "Registration";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
