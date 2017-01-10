using System;
using System.Linq;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Identity
{
    /// <summary>
    /// This is a test class related to tests for login form widget and login status (login/logout button).
    /// </summary>
    [TestClass]
    public class LoginFormAndLoginStatusOnTheSamePage : FeatherTestCase
    {
        /// <summary>
        /// UI test LoginAndVerifyUserStatusOnTheSamePage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam3),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.LoginForm),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void LoginAndVerifyUserStatusOnTheSamePage()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(LoginFormWidget);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(LoginStatusWidget);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().User().LogOut();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture, new HtmlFindExpression("class=~sfPublicWrapper"));

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressLoginButton();
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().AssertEmptyUserNameFieldMessage();
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().AssertEmptyPasswordFieldMessage();

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterEmail(TestAdminEmail);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterPassword(WrongPassword);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressLoginButton();
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().AssertIncorrectUserNamePasswordMessage();

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterEmail(TestAdminEmail);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterPassword(TestAdminPass);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressLoginButton();

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().AssertAlreadyLoggedInMessage();
            BATFeather.Wrappers().Frontend().Identity().LoginStatusWrapper().AssertUserLoggedInName(TestAdminFirstName, TestAdminLastName);
            BATFeather.Wrappers().Frontend().Identity().LoginStatusWrapper().AssertUserEmail(TestAdminEmail);
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

        private const string PageName = "LoginPage";
        private const string LoginFormWidget = "Login form";
        private const string LoginStatusWidget = "Login / Logout button";
        private const string TestAdminEmail = "admin2@test.test";
        private const string TestAdminPass = "password";
        private const string TestAdminFirstName = "admin2";
        private const string TestAdminLastName = "admin2";
        private const string WrongPassword = "password123";
    }
}
