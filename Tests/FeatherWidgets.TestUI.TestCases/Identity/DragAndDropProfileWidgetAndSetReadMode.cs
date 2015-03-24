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
    /// DragAndDropProfileWidgetAndSetReadMode_ test class.
    /// </summary>
    [TestClass]
    public class DragAndDropProfileWidgetAndSetReadMode_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DragAndDropProfileWidgetAndSetReadMode
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Profile),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void DragAndDropProfileWidgetAndSetReadMode()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(ProfilePage);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Identity().ProfileWrapper().SwitchToReadMode();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().User().LogOut();
            BAT.Macros().NavigateTo().CustomPage("~/" + LoginPage.ToLower());
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetUsername(NewUserName);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetPassword(NewUserPassword);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().ExecuteLogin();
            BAT.Macros().NavigateTo().CustomPage("~/" + ProfilePage.ToLower(), false);

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().VerifyUserFirstAndLastName(NewUserFirstAndLastName);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().VerifyUserEmailAddress(NewUserEmail);
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

        private const string ProfilePage = "ProfilePage";
        private const string WidgetName = "Profile";
        private const string NewUserName = "newUser";
        private const string NewUserPassword = "password";
        private const string NewUserFirstAndLastName = "First name Last name";
        private const string NewUserEmail = "newuser@test.com";
        private const string LoginPage = "Sitefinity";
    }
}
