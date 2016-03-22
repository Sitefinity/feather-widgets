using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// VerifyPageBreakInHeaderAndFooter test class.
    /// </summary>
    [TestClass]
    public class VerifyPageBreakInHeaderAndFooter_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyPageBreakInHeaderAndFooter
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void VerifyPageBreakInHeaderAndFooter()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(PageBreak);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyCommonHeaderAndFooterAreVisible();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(PageBreak, Footer);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(PageBreak, Header);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyCommonHeaderAndFooterAreVisible();
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
        private const string Footer = "Footer";
        private const string Header = "Header";
    }
}
