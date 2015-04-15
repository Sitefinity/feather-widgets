using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Identity
{
    /// <summary>
    /// RegisterNewUserAndLoginTheUser_ test class.
    /// </summary>
    [TestClass]
    public class RegisterNewUserAndLoginTheUser_ : FeatherTestCase
    {
        /// <summary>
        /// UI test RegisterNewUserAndLoginTheUser
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Registration),
        TestCategory(FeatherTestCategories.LoginForm),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void RegisterNewUserAndLoginTheUser()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(RegistrationPage);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(RegistrationWidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(SelectedRoles);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(LoginPage);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(LoginWidgetName);
            BATFeather.Wrappers().Backend().Identity().LoginFormWrapper().ClickSelectButtonForRegistrationPage();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(this.pageToSelect);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(this.pageToSelect);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().User().LogOut();

            BAT.Macros().NavigateTo().CustomPage("~/" + LoginPage.ToLower());
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressRegisterNowLink();

            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillEmail(Email);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillUserName(UserName);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillPassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillRetypePassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().RegisterButton();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().VerifySuccessfullyMessage();

            BAT.Macros().NavigateTo().CustomPage("~/" + LoginPage.ToLower());
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterUserName(UserName);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterPassword(Password);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressLoginButton();

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().AssertAlreadyLoggedInMessage();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string RegistrationPage = "RegistrationPage";
        private const string LoginPage = "LoginPage";
        private readonly string[] pageToSelect = new string[] { RegistrationPage };
        private const string RegistrationWidgetName = "Registration";
        private const string LoginWidgetName = "Login form";
        private const string SelectedRoles = "Administrators";
        private const string Email = "user@test.com";
        private const string UserName = "newUser";
        private const string Password = "password";
    }
}
