using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.RevisionHistory
{
    /// <summary>
    /// VerifyOlderAndNewerVersionInRevisionHistoryOnMvcPageTemplate test class.
    /// </summary>
    [TestClass]
    public class VerifyOlderAndNewerVersionInRevisionHistoryOnMvcPageTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyOlderAndNewerVersionInRevisionHistoryOnMvcPageTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.RevisionHistory)]
        public void VerifyOlderAndNewerVersionInRevisionHistoryOnMvcPageTemplate()
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

            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetContentBlock, Placeholder);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetContentBlock);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(ContentBlockContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().IsHtmlControlPresent(ContentBlockContent));
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickSaveAsDraftButton();

            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickBackToTemplatesButton();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenRevisionHistoryScreenFromActionsMenu(TemplateTitle);
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().VerifyRevisionHistoryScreen(TemplateTitle, "Templates");
            Assert.AreEqual<int>(3, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRows().Count, "The actual number of revisions doesn't match the expected (3) three");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickOnSelectedVersion(TemplatePublishedVersion1);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyRevisionHistoryVersionDialog(TemplateTitle, TemplatePublishedVersion1, RevisionHistoryText);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyDeleteVersionButtonIsVisible();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(TitleOfNewsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyNotExistContentInRevisionHistory(TitleOfEventsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyNotExistContentInRevisionHistory(ContentBlockContent);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyOlderVersionLinkIsHidden();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().PressNewerVersionLink();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyRevisionHistoryVersionDialog(TemplateTitle, TemplatePublishedVersion2, RevisionHistoryText);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(TitleOfNewsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(TitleOfEventsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyNotExistContentInRevisionHistory(ContentBlockContent);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyDeleteVersionButtonIsHidden();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().PressNewerVersionLink();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyRevisionHistoryVersionDialog(TemplateTitle, TemplateDraftVersion2, RevisionHistoryText);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyDeleteVersionButtonIsVisible();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(TitleOfNewsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(TitleOfEventsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(ContentBlockContent);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyNewerVersionLinkIsHidden();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().PressOlderVersionLink();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyRevisionHistoryVersionDialog(TemplateTitle, TemplatePublishedVersion2, RevisionHistoryText);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(TitleOfNewsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(TitleOfEventsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyNotExistContentInRevisionHistory(ContentBlockContent);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyDeleteVersionButtonIsHidden();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().PressOlderVersionLink();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyRevisionHistoryVersionDialog(TemplateTitle, TemplatePublishedVersion1, RevisionHistoryText);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyDeleteVersionButtonIsVisible();
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(TitleOfNewsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyNotExistContentInRevisionHistory(TitleOfEventsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyNotExistContentInRevisionHistory(ContentBlockContent);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyOlderVersionLinkIsHidden();
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
        private const string WidgetContentBlock = "Content block";
        private const string Placeholder = "Body";
        private const string TemplatePublishedVersion1 = "1.0";
        private const string TemplatePublishedVersion2 = "2.0";
        private const string TemplateDraftVersion2 = "2.1";
        private const string TitleOfNewsItem = "TestNewsTitle";
        private const string TitleOfEventsItem = "TestEventTitle";
        private const string ContentBlockContent = "TestContent";
        private const string RevisionHistoryText = "Revision History";
    }
}
