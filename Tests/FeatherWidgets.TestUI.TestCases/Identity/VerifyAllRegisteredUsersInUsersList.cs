using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Identity
{
    /// <summary>
    /// VerifyAllRegisteredUsersInUsersList_ test class.
    /// </summary>
    [TestClass]
    public class VerifyAllRegisteredUsersInUsersList_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyAllRegisteredUsersInUsersList
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.UsersList)]
        public void VerifyAllRegisteredUsersInUsersList()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            //// verify Content tab
            BATFeather.Wrappers().Backend().Identity().UsersListWrapper().VerifyWhichUsersToDisplayLabel();
            BATFeather.Wrappers().Backend().Identity().UsersListWrapper().VerifyWhichUsersToDisplayOptions();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.allUsers);

            BATFeather.Wrappers().Backend().Identity().UsersListWrapper().VerifyWhichProfileToDisplayLabel();
            BATFeather.Wrappers().Backend().Identity().UsersListWrapper().SelectUserProfile(UserProfile);

            //// verify List settings tab
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPagingAndLimitOptions();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectSortingOption(SortingOption);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySortingOptions(this.sortingOptions);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInListTemplateSelector(ListTemplateName);

            //// verify Single item settings
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyOpenSingleItemsOptions();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectExistingPage();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton(2);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(SingleUserPageName);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectDetailTemplate(DetailTemplateName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().Identity().UsersListWrapper().VerifyUsersListOnHybridPage(this.users);
            BATFeather.Wrappers().Frontend().Identity().UsersListWrapper().VerifySingleUser(UserFirstLastName, UserEmail, SingleUserPageURLEnding);
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

        private const string PageName = "UsersListPage";
        private const string SingleUserPageName = "UserPage";
        private const string SingleUserPageURLEnding = "userpage/admin";
        private const string WidgetName = "Users list";
        private const string UserProfile = "Basic profile";
        private const string SortingOption = "FirstName ASC";
        private const string ListTemplateName = "UsersList";
        private const string DetailTemplateName = "UserDetails";
        private const string UserFirstLastName = "admin admin";
        private const string UserEmail = "admin@admin.com";

        private readonly string[] sortingOptions = new string[] { "First name (A-Z)", "First name (Z-A)", "Last name (A-Z)", "Last name (Z-A)", "Last created", "Last modified", "As set in Advanced mode" };
        private readonly string[] users = new string[] { "fname lname", "miroslava nikolova" };
    }
}
