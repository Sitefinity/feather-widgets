using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Wrappers.Backend.PageTemplates;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// AddContentBlockToTemplateBasedOnLayoutFile test class
    /// </summary>
    [TestClass]
    public class AddContentBlockToTemplateBasedOnLayoutFile : FeatherTestCase
    {
        /// <summary>
        /// UI test AddContentBlockWidgetToTemplateBasedOnLayoutFile
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void AddContentBlockWidgetToTemplateBasedOnLayoutFile()
        {
            BAT.Macros().NavigateTo().Design().PageTemplates();
            this.OpenTemplateEditor();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToSelectedPlaceHolder(WidgetName, PlaceHolder);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(ContentBlockContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            ActiveBrowser.WaitUntilReady();

            Assert.IsFalse(ActiveBrowser.ContainsText(ServerErrorMessage), "Server error was found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(ContentBlockContent), "Content block content was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(LayoutText), "Layout template text was not found");
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

        private void OpenTemplateEditor()
        {
            var templateId = BAT.Arrange(this.TestName).ExecuteArrangement("GetTemplateId").Result.Values["templateId"];

            BAT.Macros().NavigateTo().CustomPage("~/Sitefinity/Template/" + templateId, false);

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncOperations();
        }

        private const string PageName = "FeatherPage";
        private const string TemplateTitle = "TestLayout";
        private const string ContentBlockContent = "Test content";
        private const string WidgetName = "ContentBlock";
        private const string PlaceHolder = "TestPlaceHolder";
        private const string LayoutText = "Test Layout";
        private const string ServerErrorMessage = "Server Error"; 
    }
}
