using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

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
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.LoginForm),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void LoginAndVerifyUserStatusOnTheSamePage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(LoginFormWidget);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(LoginStatusWidget);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().User().LogOut();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressLoginButton();
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().AssertEmptyUserNameFieldMessage();
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().AssertEmptyPasswordFieldMessage();

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterUserName(AdminUserName);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterPassword(WrongPassword);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressLoginButton();
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().AssertIncorrectUserNamePasswordMessage();

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterUserName(AdminUserName);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterPassword(AdminPassword);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressLoginButton();

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().AssertAlreadyLoggedInMessage();
            BATFeather.Wrappers().Frontend().Identity().LoginStatusWrapper().AssertUserLoggedInName(AdminUserFirstName, AdminUserLastName);
            BATFeather.Wrappers().Frontend().Identity().LoginStatusWrapper().AssertUserEmail(AdminUserEmail);
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
        private const string AdminUserFirstName = "admin";
        private const string AdminUserLastName = "admin";
        private const string AdminUserEmail = "dadasda@dasd.fdf";
        private const string AdminUserName = "admin";
        private const string WrongPassword = "password123";
        private const string AdminPassword = "admin@2"; 
    }
}
