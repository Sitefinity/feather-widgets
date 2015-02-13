using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Wrappers.Backend.PageTemplates;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// AddContentBlockWidgetToPageTemplate test class.
    /// </summary>
    [TestClass]
    public class AddContentBlockWidgetToPageTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddContentBlockWidgetToPageTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void AddContentBlockWidgetToPageTemplate()
        {
            BAT.Macros().NavigateTo().Design().PageTemplates();

            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().ClickOnCreateNewTemplateBtn();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateCreateScreen().SetTemplateName(TemplateName);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateCreateScreen().ClickOnCreateTemplateAndGoToAddContentBtn();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().DragAndDropContentWidget(WidgetName, new PageTemplateModifyWrapper.ContainerElement(0, 0));
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(ContentBlockContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenCreatePageWindow();
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().SetPageTitle(PageName);
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().ClickSelectAnotherTemplateButton();
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().SelectATemplate(TemplateName);
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().ClickDoneButton();
            BAT.Wrappers().Backend().Pages().PagesWrapper().SavePageDataAndContinue();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().WaitUntilReady();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ContentBlockContent);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "ContentBlock";
        private const string ContentBlockContent = "Test content";
        private const string WidgetName = "ContentBlock";
        private const string TemplateName = "template with content block feather widget";
    }
}
