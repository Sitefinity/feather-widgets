using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.WebAii.Controls.Html;

namespace FeatherWidgets.TestUI.TestCases.Forms.Checkboxes
{
    /// <summary>
    /// PreviewFormWithCheckBoxField test class.
    /// </summary>
    [TestClass]
    public class PreviewFormWithCheckBoxField_ : FeatherTestCase
    {
        /// <summary>
        /// UI test PreviewFormWithCheckBoxField
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void PreviewFormWithCheckBoxField()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickPreviewButton();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
            ActiveBrowser.WaitUntilReady();
            Assert.IsTrue(ActiveBrowser.ContainsText("First choice"), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText("Second choice"), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText("Third choice"), "Text was not found on the page");
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyCheckboxesFieldLabelIsVisible(FeatherGlobals.SelectAChoiceLabelName);
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
