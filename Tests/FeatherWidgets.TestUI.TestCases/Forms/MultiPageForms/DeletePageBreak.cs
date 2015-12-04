using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// DeletePageBreak test class.
    /// </summary>
    [TestClass]
    public class DeletePageBreak_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeletePageBreak
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void DeletePageBreak()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(PageBreak);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyCommonHeaderAndFooterAreVisible();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().DeleteWidget(PageBreak, 2);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyCommonHeaderAndFooterAreVisible(false);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
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

        private const string FormName = "MultiPageForm";
        private const string PageBreak = "Page break";
    }
}
