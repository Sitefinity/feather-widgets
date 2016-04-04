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
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.PageEditor;

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
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.ContentBlock4)]
        public void AddContentBlockWidgetToTemplateBasedOnLayoutFile()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/design/pagetemplates", false, this.Culture));
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SwitchEditorLayoutMode(EditorLayoutMode.Layout);
            BAT.Wrappers().Backend().Pages().PageLayoutEditorWrapper().SelectAnotherTemplate();
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().SelectATemplate("Bootstrap.default");
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().ClickDoneButton();

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(ContentBlockContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
            BAT.Arrange(this.TestName).ExecuteArrangement("GetTemplateId");
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);

            Assert.IsFalse(ActiveBrowser.ContainsText(ServerErrorMessage), "Server error was found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(ContentBlockContent), "Content block content was not found on the page");
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

        private const string PageName = "FeatherPage";
        private const string TemplateTitle = "TestLayout";
        private const string ContentBlockContent = "Test content";
        private const string WidgetName = "Content block";
        private const string ServerErrorMessage = "Server Error"; 
    }
}
