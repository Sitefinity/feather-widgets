using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// VerifyFormPagesNumbers test class.
    /// </summary>
    [TestClass]
    public class VerifyFormPagesNumbers_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyFormPagesNumbers
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void VerifyFormPagesNumbers()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyHeaderAndFooterPageLabels(false, true);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FieldName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyHeaderAndFooterPageLabels();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPagingIndexes(this.pageIndexesForOnePageBreak);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().AddField(FieldName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyHeaderAndFooterPageLabels();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPagingIndexes(this.pageIndexesForTwoPageBreaks);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().DeleteWidget(FieldName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyHeaderAndFooterPageLabels();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPagingIndexes(this.pageIndexesForOnePageBreak);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().DeleteWidget(FieldName);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyHeaderAndFooterPageLabels(false);
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
        private const string FieldName = "Page break";
        private List<string> pageIndexesForOnePageBreak = new List<string>() { "1 of 2", "2 of 2" };
        private List<string> pageIndexesForTwoPageBreaks = new List<string>() { "1 of 3", "2 of 3", "3 of 3" };
    }
}
