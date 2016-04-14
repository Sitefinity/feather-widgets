using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// ContentBlockWidgetVerifyLinksInEditorToolbar test class.
    /// </summary>
    [TestClass]
    public class ContentBlockWidgetVerifyLinksInEditorToolbar_ : FeatherTestCase
    {/// <summary>
        /// UI test ContentBlockWidgetVerifyLinksInEditorToolbar
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.ContentBlock2)]
        public void ContentBlockWidgetVerifyLinksInEditorToolbar()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            //Verify Links in Editor Toolbar in MVC Content Block widget in page with MVC template
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Bold");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Italic");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Underline");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert unordered list");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert ordered list");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Indent");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert hyperlink");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert image");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert file");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert video");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Create table");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Clean formatting");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("All tools");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Fullscreen");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenButtonsInEditorToolbar("Strikethrough");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenButtonsInEditorToolbar("Subscript");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenButtonsInEditorToolbar("Superscript");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenDropDownInEditorToolbar("Color");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenDropDownInEditorToolbar("Background color");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenSelectMenuInEditorToolbar("Font Name");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenSelectMenuInEditorToolbar("Font Size");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            
            //Verify Links in Editor Toolbar in MVC Content Block widget in page with MVC template
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Bold");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Italic");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Underline");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert unordered list");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert ordered list");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Indent");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert hyperlink");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert image");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert file");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Insert video");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Create table");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Clean formatting");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("All tools");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyVisibleButtonsInEditorToolbar("Fullscreen");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenButtonsInEditorToolbar("Strikethrough");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenButtonsInEditorToolbar("Subscript");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenButtonsInEditorToolbar("Superscript");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenDropDownInEditorToolbar("Color");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenDropDownInEditorToolbar("Background color");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenSelectMenuInEditorToolbar("Font Name");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyHiddenSelectMenuInEditorToolbar("Font Size");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// 
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "FeatherPageNonBootstrap";
        private const string WidgetName = "ContentBlock";
        private const string PageTitle = "TestPage";
    }
}
