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
    /// EditProfileWidgetInBothModeAndSetSpeciallyPreparedPage_ test class.
    /// </summary>
    [TestClass]
    public class EditProfileWidgetInBothModeAndSetSpeciallyPreparedPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test EditProfileWidgetInBothModeAndSetSpeciallyPreparedPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Profile),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void EditProfileWidgetInBothModeAndSetSpeciallyPreparedPage()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + LoginPage.ToLower());
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetUsername(UserName);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetPassword(UserPassword);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().ExecuteLogin();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(ProfilePage);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Identity().ProfileWrapper().SwitchToBothMode();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + ProfilePage.ToLower(), false);

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().VerifyUserFirstAndLastName(NewUserFirstAndLastName);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().VerifyUserEmailAddress(NewUserEmail);

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().ClickEditProfileLink();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillFirstName(UserFirstNameEdited);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().AssertSuccessfullySavedMessage();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(ProfilePage);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Identity().ProfileWrapper().SelectDisplayModeWhenChangesAreSaved("Open a specially prepared page...");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(SpeciallPage);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + ProfilePage.ToLower(), false);
            
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().ClickEditProfileLink();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillLastName(UserLastNameEdited);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ContentBlockText);
            Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent().InnerText.Contains(ContentBlockText), "Page not found");
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

        private const string ProfilePage = "ProfilePage";
        private const string WidgetName = "Profile";
        private const string UserName = "newUser";
        private const string UserPassword = "password";
        private const string NewUserFirstAndLastName = "First name Last name";
        private const string NewUserEmail = "newuser@test.com";
        private const string UserFirstNameEdited = "First name Edited";
        private const string UserLastNameEdited = "Last name Edited";
        private const string LoginPage = "Sitefinity";
        private const string SpeciallPage = "SpeciallPage";
        private const string ContentBlockText = "Specially prepared page";
    }
}
