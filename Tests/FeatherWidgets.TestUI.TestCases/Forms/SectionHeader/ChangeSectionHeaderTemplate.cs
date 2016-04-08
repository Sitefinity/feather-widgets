using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.SectionHeader
{
    /// <summary>
    /// ChangeSectionHeaderTemplate test class.
    /// </summary>
    [TestClass]
    public class ChangeSectionHeaderTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ChangeSectionHeaderTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void ChangeSectionHeaderTemplate()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(SectionHeaderController);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().SelectReadTemplate(TemplateNameFile);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().WaitForFieldContent(TemplateContentFile, SectionHeaderController);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(SectionHeaderController, TemplateContentFile);         
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent().InnerText.Contains(TemplateContentFile), "Template is not presented");

            BAT.Macros().NavigateTo().Design().WidgetTemplates();
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesWrapper().CreateTemplate();
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().SelectTemplate("SectionHeader (MVC)");
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().EnterTextInTextArea(TemplateContent);
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().EnterWidgetTemplateName(TemplateName);
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().CreateThisTemplate();
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(SectionHeaderController);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().SelectReadTemplate(TemplateNameNew);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().WaitForFieldContent(TemplateContent, SectionHeaderController);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(SectionHeaderController, TemplateContent);     
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

        private const string FormName = "NewForm";
        private const string SectionHeaderController = "SectionHeaderController";
        private const string PageName = "FormPage";
        private const string TemplateName = "Read.DefaultSectionHeaderNew";
        private const string TemplateNameNew = "DefaultSectionHeaderNew";
        private const string TemplateNameFile = "DefaultSectionHeaderFile";
        private const string TemplateContentFile = "File system template";
        private const string TemplateContent = "This is test content";
    }
}
