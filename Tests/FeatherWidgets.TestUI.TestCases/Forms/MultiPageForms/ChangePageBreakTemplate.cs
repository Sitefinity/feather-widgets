using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// ChangePageBreakTemplate test class.
    /// </summary>
    [TestClass]
    public class ChangePageBreakTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ChangePageBreakTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void ChangePageBreakTemplate()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(PageBreak);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().SelectReadTemplate(TemplateNameFile);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().WaitForFieldContent(TemplateContentFile, PageBreak);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(PageBreak, TemplateContentFile);         
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent().InnerText.Contains(TemplateContentFile), "Template is not presented");

            BAT.Macros().NavigateTo().Design().WidgetTemplates();
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesWrapper().CreateTemplate();
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().SelectTemplate("PageBreak (MVC)");
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().EnterTextInTextArea(TemplateContent);
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().EnterWidgetTemplateName(TemplateName);
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().CreateThisTemplate();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(PageBreak);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().SelectReadTemplate(TemplateNameNew);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().WaitForFieldContent(TemplateContent, PageBreak);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(PageBreak, TemplateContent);     
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent().InnerText.Contains(TemplateContent), "Template is not presented");
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

        private const string FormName = "MultiPageForm";
        private const string PageBreak = "PageBreakController";
        private const string PageName = "FormPage";
        private const string TemplateName = "Read.DefaultNew";
        private const string TemplateNameNew = "DefaultNew";
        private const string TemplateNameFile = "DefaultFile";
        private const string TemplateContentFile = "File system template";
        private const string TemplateContent = "This is test content";
    }
}
