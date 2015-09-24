using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// DeleteContentBlockWidgetFromPage test class.
    /// </summary>
    [TestClass]
    public class DeleteContentBlockWidgetFromPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeleteContentBlockWidgetFromPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.ContentBlock2)]
        public void DeleteContentBlockWidgetFromPage()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/" + PageName, false, this.Culture));
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName, true, this.Culture);
            Assert.AreEqual(InitialContentBlocksCount, BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().GetCountOfContentBlocksOnFrontend(ContentBlockContent));
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName, true, this.Culture);
            Assert.AreEqual(ExpectedContentBlocksCount, BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().GetCountOfContentBlocksOnFrontend(ContentBlockContent));
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
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
        private const string OperationName = "Delete";
        private const int InitialContentBlocksCount = 1;
        private const int ExpectedContentBlocksCount = 0;
        private const string ContentBlockContent = "Test content";
    }
}
