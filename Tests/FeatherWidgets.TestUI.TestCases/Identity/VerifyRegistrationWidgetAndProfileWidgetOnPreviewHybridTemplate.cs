using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using ArtOfTest.WebAii.Core;

namespace FeatherWidgets.TestUI.TestCases.Identity
{
    /// <summary>
    /// VerifyRegistrationWidgetAndProfileWidgetOnPreviewHybridTemplate_ test class.
    /// </summary>
    [TestClass]
    public class VerifyRegistrationWidgetAndProfileWidgetOnPreviewHybridTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyRegistrationWidgetAndProfileWidgetOnPreviewHybridTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam4),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void VerifyRegistrationWidgetAndProfileWidgetOnPreviewHybridTemplate()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", false));
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().ClickOnCreateNewTemplateBtn();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateCreateScreen().SetTemplateName(TemplateTitle);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateCreateScreen().ClickOnCreateTemplateAndGoToAddContentBtn();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(ProfileWidget);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(RegistrationWidget);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PreviewTemplateFromEdit();
            ActiveBrowser.WaitUntilReady();
            ////Verify save changes message for Profile widget in Preview mode
            BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().ClickButtonByValue("Save changes");
            Assert.IsTrue(ActiveBrowser.ContainsText(MessageSaveChangesProfileWidget), "Message was not found on the page");
            ////Verify all required fields message for Registration widget in Preview mode
            BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().ClickButtonByValue("Register");
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertEmptyEmailFieldMessage();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertEmptyUsernameFieldMessage();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertEmptyPasswordFieldMessage();
            ////Verify successful message for Registration widget in Preview mode
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillEmail(Email);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillUserName(UserName);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillPassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillRetypePassword(Password);
            BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().ClickButtonByValue("Register");
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

        private const string TemplateTitle = "TestHybrid";
        private const string Email = "user20@dsds.bg";
        private const string UserName = "newUser";
        private const string Password = "password";
        private const string MessageSaveChangesProfileWidget = "Saving changes is not available in Preview";
        private const string OperationNameDelete = "Delete";
        private const string ProfileWidget = "Profile";
        private const string RegistrationWidget = "Registration";
    }
}
