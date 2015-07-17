using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestUI.Framework.Framework.ElementMap.Permissions;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// This is a test class with tests related to permissions for content block widget
    /// </summary>
    [TestClass]
    public class ContentBlockWidgetVerifyPermissions : FeatherTestCase
    {
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void AddContentBlockWidgetToPageVerifyViewPermissions()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BAT.Wrappers().Backend().Permissions().PermissionsContentWrapper().ClickChangePermissionsButton(PermissionTypes.View);
            BAT.Wrappers().Backend().Permissions().PermissionsContentWrapper().SelectAndAddRole("Authenticated");
            BAT.Wrappers().Backend().Permissions().PermissionsContentWrapper().ClickBackButton();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().WaitUntilReady();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().User().LogOut();
            ActiveBrowser.RefreshDomTree();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), false);

            Assert.IsFalse(ActiveBrowser.ContainsText(ContentBlockText), "Content block text was found but it shouldn't");
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

        private const string PageTitle = "FeatherPage";
        private const string OperationName = "Permissions";
        private const string ContentBlockText = "TestContent";
    }
}
