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
    /// This is a sample test class.
    /// </summary>
    [TestClass]
    public class DuplicateContentBlockWidgetFromPage_ : FeatherTestCase
    {
        // <summary>
        /// Pefroms Server Setup and prepare the system with needed data.
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

        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.PagesAndContent)]
        public void DuplicateContentBlockWidgetFromPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyEmptyPageFrontEnd(ContentBlockContent);           
        }

        private void VerifyEmptyPageFrontEnd(string content)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName);
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(content);
        }

        private const string PageName = "ContentBlock";
        private const string WidgetName = "ContentBlock";
        private const string OperationName = "Duplicate";
        private const string ContentBlockContent = "Test content";
        private const int ExpectedCountOfContentDivEmpty = 2;
    }
}
