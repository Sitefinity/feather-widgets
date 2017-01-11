using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
namespace FeatherWidgets.TestUI.TestCases.DocumentsList
{
    /// <summary>
    /// VerifyViewPermissionsForDocumentsListWidget test class.
    /// </summary>
    [TestClass]
    public class VerifyViewPermissionsForDocumentsListWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyViewPermissionsForDocumentsListWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam4),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.DocumentsList),
        Telerik.TestUI.Core.Attributes.KnownIssue(BugId = 206476), Ignore]
        public void VerifyViewPermissionsForDocumentsListWidget()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", true, null, new HtmlFindExpression("class=~sfMain")));
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Macros().NavigateTo().Modules().Documents(this.Culture);
            BAT.Wrappers().Backend().DocumentsAndFiles().DocumentsAndFilesDashboardWrapper().ClickActionMenuAndChooseOption(0, OptionSetPermissions);
            BAT.Wrappers().Backend().Permissions().PermissionsContentWrapper().ClickBreakInheritanceButton();
            var changeBtn = BAT.Wrappers().Backend().Permissions().PermissionsContentWrapper().ChangePermissionButton("View document");
            changeBtn.Click();
            BAT.Wrappers().Backend().Permissions().PermissionsContentWrapper().SelectAndAddRole("TestRole2");

            BAT.Macros().NavigateTo().Modules().Documents(this.Culture);
            BAT.Wrappers().Backend().DocumentsAndFiles().DocumentsAndFilesDashboardWrapper().ClickActionMenuAndChooseOption(1, OptionSetPermissions);
            BAT.Wrappers().Backend().Permissions().PermissionsContentWrapper().ClickBreakInheritanceButton();
            changeBtn.Click();
            BAT.Wrappers().Backend().Permissions().PermissionsContentWrapper().SelectAndAddRole("TestRole1");
                        
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BAT.Macros().User().LogOut();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterEmail(UserName);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().EnterPassword(UserPassword);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressLoginButton();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().CommonWrapper().AreTitlesPresentOnThePageFrontend(new string[] { DocumentTitle1, DocumentTitle2 }));
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().ClickDocument(DocumentTitle1);
            ActiveBrowser.WaitUntilReady();
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().IsDocumentTitlePresentOnDetailMasterPage(DocumentTitle1);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().ClickDocument(DocumentTitle2);
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.ContainsText("HTTP Error 403.0 - Forbidden");
            ActiveBrowser.ContainsText("This is a generic 403 error and means the authenticated user is not authorized to view the page.");
            ActiveBrowser.ContainsText("You do not have permission to view this directory or page.");
            Assert.IsFalse(ActiveBrowser.ContainsText("You are not authorized to 'View document' ('Document')."), "Text was not found on the page");
            ActiveBrowser.WaitUntilReady();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Identity().LoginStatusWrapper().Logout();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
            currentProviderUrlName = BAT.Arrange(this.TestName).ExecuteArrangement("GetCurrentProviderUrlName").Result.Values["CurrentProviderUrlName"];
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string UserName = "user1";
        private const string UserPassword = "admin@2";
        private const string PageName = "PageWithDocument";
        private const string DocumentTitle1 = "Document1";
        private const string DocumentTitle2 = "Document2";
        private const string OptionSetPermissions = "Set Permissions";
        private const string ForbiddenText = "HTTP Error 403.0 - Forbidden";
        private string currentProviderUrlName;
    }
}