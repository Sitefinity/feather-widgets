using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.RevisionHistory
{
    /// <summary>
    /// VerifyRevertToThisVersionInRevisionHistoryOnMvcPageTemplate test class.
    /// </summary>
    [TestClass]
    public class VerifyRevertToThisVersionInRevisionHistoryOnMvcPageTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyRevertToThisVersionInRevisionHistoryOnMvcPageTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.RevisionHistory)]
        public void VerifyRevertToThisVersionInRevisionHistoryOnMvcPageTemplate()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/design/pagetemplates", false));
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetNews, Placeholder);
            Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().IsHtmlControlPresent(TitleOfNewsItem));
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();

            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetEvents, Placeholder);
            Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().IsHtmlControlPresent(TitleOfEventsItem));
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();

            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenRevisionHistoryScreenFromActionsMenu(TemplateTitle);
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().VerifyRevisionHistoryScreen(TemplateTitle, "Templates");
            Assert.AreEqual<int>(2, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRows().Count, "The actual number of revisions doesn't match the expected (2) two");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickOnSelectedVersion(TemplatePublishedVersion1);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().ClickRevertToThisVersion();
            ActiveBrowser.WaitForElementEndsWithID("_MainToolBar");
            var actualControlsCount = BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().GetAllPageControls();
            Assert.AreEqual<int>(ExpectedControlsCount, actualControlsCount.Count, "The count isn't correct");
            bool isNewsWidgetVisible = BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().IsWidgetVisible(WidgetNews, TitleOfNewsItem);
            Assert.IsTrue(isNewsWidgetVisible);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickSaveAsDraftButton();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickBackToTemplatesButton();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenRevisionHistoryScreenFromActionsMenu(TemplateTitle);
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().VerifyRevisionHistoryScreen(TemplateTitle, "Templates");
            Assert.AreEqual<int>(3, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRows().Count, "The actual number of revisions doesn't match the expected (3) three");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickOnSelectedVersion(TemplateDraftVersion2);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(TitleOfNewsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyNotExistContentInRevisionHistory(TitleOfEventsItem);

            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            var actualControlsCount1 = BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().GetAllPageControls();
            Assert.AreEqual<int>(ExpectedControlsCount, actualControlsCount1.Count, "The count isn't correct");
            bool isNewsWidgetVisible1 = BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().IsWidgetVisible(WidgetNews, TitleOfNewsItem);
            Assert.IsTrue(isNewsWidgetVisible);
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
        private const string WidgetNews = "News";
        private const string WidgetEvents = "Events";
        private const string Placeholder = "Body";
        private const string TemplatePublishedVersion1 = "1.0";
        private const string TemplateDraftVersion2 = "2.1";
        private const string TitleOfNewsItem = "TestNewsTitle";
        private const string TitleOfEventsItem = "TestEventTitle";
        private const int ExpectedControlsCount = 1;
    }
}
