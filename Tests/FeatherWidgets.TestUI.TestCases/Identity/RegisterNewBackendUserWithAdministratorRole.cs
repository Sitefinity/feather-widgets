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
    /// RegisterNewBackendUserWithAdministratorRole_ test class.
    /// </summary>
    [TestClass]
    public class RegisterNewBackendUserWithAdministratorRole_ : FeatherTestCase
    {
        /// <summary>
        /// UI test RegisterNewBackendUserWithAdministratorRole
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Registration),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void RegisterNewBackendUserWithAdministratorRole()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(RegistrationPage);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(SelectedRoles);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().User().LogOut();

            BAT.Macros().NavigateTo().CustomPage("~/" + RegistrationPage.ToLower());
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillFirstName(FirstName);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillLastName(LastName);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillEmail(Email);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillUserName(UserName);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillPassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillRetypePassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().RegisterButton();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().VerifySuccessfullyMessage();

            BAT.Macros().NavigateTo().CustomPage("~/" + LoginPage.ToLower());
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetUsername(UserName);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetPassword(Password);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().ExecuteLogin();
            BAT.Macros().NavigateTo().UsersManagement().Users();
            Assert.IsTrue(BAT.Wrappers().Backend().Users().UsersWrapper().IsUserPresentInGrid(UserName), "Registered user was not found in the grid");
            BAT.Macros().User().LogOut();
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
        private const string WidgetName = "Registration";
        private const string SelectedRoles = "Administrators";
        private const string FirstName = "FirstName";
        private const string LastName = "LastName";
        private const string Email = "user@test.com";
        private const string UserName = "newUser";
        private const string Password = "password";
        private const string LoginPage = "Sitefinity";
    }
}
