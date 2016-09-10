using System;
using ArtOfTest.WebAii.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.RevisionHistory
{
    /// <summary>
    /// DeleteDraftAndPublishVersionInDetailRevisionHistoryPageOnMvcPageTemplate test class.
    /// </summary>
    [TestClass]
    public class DeleteDraftAndPublishVersionInDetailRevisionHistoryPageOnMvcPageTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeleteDraftAndPublishVersionInDetailRevisionHistoryPageOnMvcPageTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.RevisionHistory)]
        public void DeleteDraftAndPublishVersionInDetailRevisionHistoryPageOnMvcPageTemplate()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/design/pagetemplates", false));
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickSaveAsDraftButton();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickBackToTemplatesButton();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenRevisionHistoryScreenFromActionsMenu(TemplateTitle);
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().VerifyRevisionHistoryScreen(TemplateTitle, "Templates");
            Assert.AreEqual<int>(3, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRows().Count, "The actual number of revisions doesn't match the expected (3) three");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickOnSelectedVersion("1.0");
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyDeleteVersionButtonIsVisible();
            BAT.Macros().DialogOperations().StartConfirmDialogMonitoring();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().PressDeleteVersionButtonInDetailPage();

            ////BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenRevisionHistoryScreenFromActionsMenu(TemplateTitle);
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().VerifyTitleRowsInRevisionHistory("2.1");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().VerifyTitleRowsInRevisionHistory("2.0");
            Assert.AreEqual<int>(2, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRows().Count, "The actual number of revisions doesn't match the expected (2) two");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickOnSelectedVersion("2.1");
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyDeleteVersionButtonIsVisible();
            BAT.Macros().DialogOperations().StartConfirmDialogMonitoring();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().PressDeleteVersionButtonInDetailPage();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.Refresh();
            ActiveBrowser.WaitForElementEndsWithID("_frontendPageTemplatesListView");
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenRevisionHistoryScreenFromActionsMenu(TemplateTitle);
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().VerifyTitleRowsInRevisionHistory("2.0");
            Assert.AreEqual<int>(1, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRows().Count, "The actual number of revisions doesn't match the expected (1) one");
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

        private const string TemplateTitle = "TestLayout";
    }
}
