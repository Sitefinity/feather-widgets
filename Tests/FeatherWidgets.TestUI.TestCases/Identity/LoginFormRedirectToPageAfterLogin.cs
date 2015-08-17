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
    /// This is a test class with tests related to login form widget and redirect to another page after login.
    /// </summary>
    [TestClass]
    public class LoginFormRedirectToPageAfterLogin : FeatherTestCase
    {
        /// <summary>
        /// UI test LoginFormAddChangeRemoveRedirectToPageAfterLogin
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.LoginForm),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void LoginFormAddChangeRemoveRedirectToPageAfterLogin()
        {
            // Edit the page with login form and select page to redirect after login
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(LoginPageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(LoginFormWidget);
            BATFeather.Wrappers().Backend().Identity().LoginFormWrapper().ClickSelectButtonForRedirectToPage();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(this.pageToSelect);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(this.pageToSelect);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().User().LogOut();

            BAT.Macros().NavigateTo().CustomPage("~/" + LoginPageName.ToLower(), false);

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterUserName(AdminUserName);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterPassword(AdminPassword);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressLoginButton();

            BATFeather.Wrappers().Frontend().CommonWrapper().WaitForNewPageToLoad(this.pageToSelect[0].ToLower(), false);

            // Edit the page with login form and CHANGE the page to redirect after login
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(LoginPageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(LoginFormWidget);
            BATFeather.Wrappers().Backend().Identity().LoginFormWrapper().ClickSelectButtonForRedirectToPage();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(this.pageToChange);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(this.pageToChange);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().User().LogOut();

            BAT.Macros().NavigateTo().CustomPage("~/" + LoginPageName.ToLower(), false);

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterUserName(AdminUserName);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterPassword(AdminPassword);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressLoginButton();

            BATFeather.Wrappers().Frontend().CommonWrapper().WaitForNewPageToLoad(this.pageToChange[0].ToLower(), false);

            // Edit the page with login form and REMOVE the page to redirect after login
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(LoginPageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(LoginFormWidget);
            BATFeather.Wrappers().Backend().Identity().LoginFormWrapper().ClickSelectButtonForRedirectToPage();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(this.pageToChange);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().User().LogOut();

            BAT.Macros().NavigateTo().CustomPage("~/" + LoginPageName.ToLower(), false);

            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterUserName(AdminUserName);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterPassword(AdminPassword);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressLoginButton();

            BATFeather.Wrappers().Frontend().CommonWrapper().WaitForNewPageToLoad(LoginPageName.ToLower(), false);
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

        private const string LoginPageName = "LoginPage";
        private const string LoginFormWidget = "Login form";
        private readonly string[] pageToSelect = new string[] { "RedirectPage1" };
        private readonly string[] pageToChange = new string[] { "RedirectPage2" };
        private const string AdminUserName = "admin";
        private const string AdminPassword = "admin@2"; 
    }
}
