using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.RevisionHistory
{
    /// <summary>
    /// VerifyDetailedInformationOfMvcPageTemplateRevisionHistoryVersion test class.
    /// </summary>
    [TestClass]
    public class VerifyDetailedInformationOfMvcPageTemplateRevisionHistoryVersion_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyDetailedInformationOfMvcPageTemplateRevisionHistoryVersion
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.RevisionHistory)]
        public void VerifyDetailedInformationOfMvcPageTemplateRevisionHistoryVersion()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/design/pagetemplates", false));
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName, Placeholder);
            Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().IsHtmlControlPresent(TitleOfNewsItem));
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenRevisionHistoryScreenFromActionsMenu(TemplateTitle);
            Assert.AreEqual<int>(1, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRows().Count, "The actual number of revisions doesn't match the expected (1) one");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickOnSelectedVersion(TemplateVersion);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyRevisionHistoryVersionDialog(TemplateTitle, TemplateVersion, "Revision History");
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory(TitleOfNewsItem);
            BAT.Wrappers().Backend().RevisionHistory().VersionPreview().VerifyContentInRevisionHistory("Leave a comment");
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
        private const string WidgetName = "News";
        private const string Placeholder = "Body";
        private const string TemplateVersion = "1.0";
        private const string TitleOfNewsItem = "TestNewsTitle";
    }
}
