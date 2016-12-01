using System;
using ArtOfTest.WebAii.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using Feather.Widgets.TestUI.Framework;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// VerifyRevisionHistoryOfPageContainsCalendarWidget test class.
    /// </summary>
    [TestClass]
    public class VerifyRevisionHistoryOfPageContainsCalendarWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyRevisionHistoryOfPageContainsCalendarWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyRevisionHistoryOfPageContainsCalendarWidget()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", false));
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SaveAsDraft();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Wrappers().Backend().Pages().PagesWrapper().GetActionsLink("Actions", "Revision History");
            Assert.AreEqual<int>(3, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRows().Count, "The actual number of revisions doesn't match the expected (3) three");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickOnSelectedVersion(PageVersion);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyRevisionHistoryVersionDialog(PageTitle, PageVersion, "Revision History");
            ////BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(Event1Title);
            ////BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(Event2Title);

            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().ClickRevertToThisVersion();
            ActiveBrowser.WaitForElementEndsWithID("sfPageContainer");
            var actualControlsCount = BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().GetAllDroppedWidgets();
            Assert.AreEqual<int>(ExpectedControlsCount, actualControlsCount.Count, "The count isn't correct");
            bool isCalendarWidgetVisible = BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().IsWidgetAvailable(WidgetName);
            Assert.IsTrue(isCalendarWidgetVisible);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SaveAsDraft();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().BackToPages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().GetActionsLink("Actions", "Revision History");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().VerifyRevisionHistoryScreen(PageTitle, "Pages");
            Assert.AreEqual<int>(4, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRows().Count, "The actual number of revisions doesn't match the expected (4) four");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickOnSelectedVersion(PageDraftVersion1);
            ////BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(Event1Title);
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

        private const string PageTitle = "CalendarPage";
        private const string Placeholder = "Body";
        private const string PageVersion = "2.0";
        private const string PageDraftVersion1 = "2.1";
        private const string Event1Title = "Event1Title";
        private const string Event2Title = "Event2Title";
        private const int ExpectedControlsCount = 1;
        private const string WidgetName = "Calendar";
    }
}
