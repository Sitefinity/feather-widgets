using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// ContentBlockWidgetWithShareButtonsOnPageBasedOnBootstrap test class.
    /// </summary>
    [TestClass]
    public class ContentBlockWidgetWithShareButtonsOnPageBasedOnBootstrap_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ContentBlockWidgetWithShareButtonsOnPageBasedOnBootstrap
        /// </summary>
        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.PagesAndContent), Ignore]
        public void ContentBlockWidgetWithShareButtonsOnPageBasedOnBootstrap()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().AdvanceButtonSelecting();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().EnableSocialShareButtons(IsEnabled);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyPageFrontEnd();
        }

        /// <summary>
        /// Verify page frontend
        /// </summary>
        /// <param name="expectedCount">Content value</param>
        public void VerifyPageFrontEnd()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ContentBlockContent);
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifySocialShareButtonsOnThePageFrontend();
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
        private const string WidgetName = "ContentBlock";
        private const string ContentBlockContent = "Test content";
        private const string IsEnabled = "True";
    }
}
