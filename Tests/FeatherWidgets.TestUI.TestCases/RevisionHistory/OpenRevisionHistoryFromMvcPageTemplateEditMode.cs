using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Wrappers.Backend.PageTemplates;
using Telerik.Sitefinity.TestUI.Framework.Framework.Wrappers.Backend.RevisionHistory;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.RevisionHistory
{
    /// <summary>
    /// OpenRevisionHistoryFromMVCPageTemplateEditMode test class.
    /// </summary>
    [TestClass]
    public class OpenRevisionHistoryFromMvcPageTemplateEditMode_ : FeatherTestCase
    {
        /// <summary>
        /// UI test OpenRevisionHistoryFromMVCPageTemplateEditMode
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.RevisionHistory)]
        public void OpenRevisionHistoryFromMvcPageTemplateEditMode()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/design/pagetemplates", false));
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickItemFromMoreActionssMenu(RevisionHistoryLink);
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().VerifyRevisionHistoryScreenFromEditFromMoreActionsMenu(TemplateTitle);
            Assert.AreEqual<int>(1, BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().GetRevisionHistoryRowsFromEdiFromMoreeActionsMenu().Count, "The actual number of revisions doesn't match the expected (1) one");
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickBackToLinInRevisionHistoryScreenFromEditFromMoreActionsMenu(TemplateTitle);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();

            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle1);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickItemFromMoreActionssMenu(RevisionHistoryLink);
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().VerifyRevisionHistoryScreenFromEditFromMoreActionsMenu(TemplateTitle1);
            BAT.Wrappers().Backend().RevisionHistory().RevisionHistoryWrapper().ClickBackToLinInRevisionHistoryScreenFromEditFromMoreActionsMenu(TemplateTitle1);
            ////BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().ClickBackToTemplatesButton();
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
        private const string TemplateTitle1 = "Bootstrap.default";
        private const string RevisionHistoryLink = "Revision History";
    }
}
