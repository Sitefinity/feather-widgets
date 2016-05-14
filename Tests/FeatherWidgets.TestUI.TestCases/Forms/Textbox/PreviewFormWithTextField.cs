using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.Textbox
{
    /// <summary>
    /// PreviewFormWithTextField test class.
    /// </summary>
    [TestClass]
    public class PreviewFormWithTextField_ : FeatherTestCase
    {
        /// <summary>
        /// UI test PreviewFormWithTextField
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void PreviewFormWithTextField()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
            ActiveBrowser.RefreshDomTree();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(TextField);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangeTexboxLabel(TextFieldLabel);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickPreviewButton();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();
            ActiveBrowser.WaitUntilReady();
            Assert.IsTrue(ActiveBrowser.ContainsText(TextFieldLabel), "Text was not found on the page");
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyTextboxFieldContainerIsVisible();
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
        private const string TextField = "TextFieldController";
        private const string TextFieldLabel = "This is text label";
    }
}
