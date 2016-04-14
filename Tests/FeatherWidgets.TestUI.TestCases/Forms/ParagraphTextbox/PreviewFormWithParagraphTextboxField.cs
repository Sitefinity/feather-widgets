using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.ParagraphTextbox
{
    /// <summary>
    /// PreviewFormWithParagraphTextboxField test class.
    /// </summary>
    [TestClass]
    public class PreviewFormWithParagraphTextboxField_ : FeatherTestCase
    {
        /// <summary>
        /// UI test PreviewFormWithParagraphTextboxField
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void PreviewFormWithParagraphTextboxField()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickPreviewButton();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
            ActiveBrowser.WaitUntilReady();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyParagraphFieldContainerIsVisible();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyParagraphTextFieldLabelIsVisible(FeatherGlobals.UntitledLabelName);  
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

        private const string FormName = "NewForm";
    }
}
