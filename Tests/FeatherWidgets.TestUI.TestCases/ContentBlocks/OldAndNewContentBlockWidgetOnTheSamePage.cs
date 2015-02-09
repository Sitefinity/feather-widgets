using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// OldAndNewContentBlockWidgetOnTheSamePage test class.
    /// </summary>
    [TestClass]
    public class OldAndNewContentBlockWidgetOnTheSamePage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test OldAndNewContentBlockWidgetOnTheSamePage
        /// </summary>
        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.PagesAndContent)]
        public void OldAndNewContentBlockWidgetOnTheSamePage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(NewContentBlockWidget);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(NewContentBlockContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(NewContentBlockWidget, NewContentBlockContent);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(OldContentBlockWidget);
            BAT.Wrappers().Backend().Pages().WidgetDesigners().ContentBlockDesigner().FillContentBlockContent(OldContentBlockContent);
            BAT.Wrappers().Backend().Pages().WidgetDesigners().ContentBlockDesigner().SaveEditedWidgetChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(OldContentBlockWidget, OldContentBlockContent);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(NewContentBlockContent);
            BAT.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(OldContentBlockContent);
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
        private const string NewContentBlockContent = "New content block widget";
        private const string NewContentBlockWidget = "ContentBlock";
        private const string OldContentBlockContent = "Old content block widget";
        private const string OldContentBlockWidget = "Content block";
    }
}
