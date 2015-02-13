using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// UnshareContentBlockFromPage test class.
    /// </summary>
    [TestClass]
    public class UnshareContentBlockFromPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test UnshareContentBlockFromPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void UnshareContentBlockFromPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksShareWrapper().UnshareButton();
            this.VerifyIfSharedLabelNotExist();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
        }

        /// <summary>
        /// Verify if shared label exist
        /// </summary>
        public void VerifyIfSharedLabelNotExist()
        {
            bool isExist = BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyContentBlockWidgetSharedLabel();
            Assert.IsFalse(isExist, "Shared label exists!");
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

        private const string PageName = "ContentBlock";
        private const string OperationName = "Unshare";
        private const string ContentBlockTitle = "ContentBlockTitle";
    }
}
