using System;
using ArtOfTest.WebAii.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// VerifyRevisionHistoryOfMVCPageTemplateContainsCalendarWidget test class.
    /// </summary>
    [TestClass]
    public class VerifyRevisionHistoryOfMVCPageTemplateContainsCalendarWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyRevisionHistoryOfMVCPageTemplateContainsCalendarWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyRevisionHistoryOfMVCPageTemplateContainsCalendarWidget()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/design/pagetemplates", false));
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickSaveAsDraftButton();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenRevisionHistoryScreenFromActionsMenu(TemplateTitle);
            Assert.AreEqual<int>(2, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRows().Count, "The actual number of revisions doesn't match the expected (2) two");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickOnSelectedVersion(TemplateVersion);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyRevisionHistoryVersionDialog(TemplateTitle, TemplateVersion, "Revision History");
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(Event1Title);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(Event2Title);

            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().ClickRevertToThisVersion();
            ActiveBrowser.WaitForElementEndsWithID("_MainToolBar");
            var actualControlsCount = BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().GetAllPageControls();
            Assert.AreEqual<int>(ExpectedControlsCount, actualControlsCount.Count, "The count isn't correct");
            bool iscalendarWidgetVisible = BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().IsWidgetVisible(WidgetName, Event1Title);
            Assert.IsTrue(iscalendarWidgetVisible);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickSaveAsDraftButton();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickBackToTemplatesButton();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenRevisionHistoryScreenFromActionsMenu(TemplateTitle);
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().VerifyRevisionHistoryScreen(TemplateTitle, "Templates");
            Assert.AreEqual<int>(3, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRows().Count, "The actual number of revisions doesn't match the expected (3) three");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickOnSelectedVersion(TemplateDraftVersion1);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(Event1Title);
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

        private const string TemplateTitle = "Calendar";
        private const string Placeholder = "Body";
        private const string TemplateVersion = "1.0";
        private const string TemplateDraftVersion1 = "1.1";
        private const string Event1Title = "Event1Title";
        private const string Event2Title = "Event2Title";
        private const int ExpectedControlsCount = 1;
        private const string WidgetName = "Calendar";
    }
}
