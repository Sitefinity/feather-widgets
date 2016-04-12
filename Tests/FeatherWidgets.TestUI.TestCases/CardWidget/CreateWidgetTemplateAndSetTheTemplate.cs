using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.CardWidget
{
    /// <summary>
    /// CreateWidgetTemplateAndSetTheTemplate test class.
    /// </summary>
    [TestClass]
    public class CreateWidgetTemplateAndSetTheTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CreateWidgetTemplateAndSetTheTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Card)]
        public void CreateWidgetTemplateAndSetTheTemplate()
        {
            BAT.Macros().NavigateTo().Design().WidgetTemplates();
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesWrapper().CreateTemplate();
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().SelectTemplate("Card (MVC)");
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().EnterTextInTextArea(TemplateContent);
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().EnterWidgetTemplateName(TemplateName);
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().CreateThisTemplate();

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Card().CardWrapper().FillHeadingText(HeadingText);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().Card().CardWrapper().SelectCardWidetTemplate(TemplateName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
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

        private const string PageName = "CardPage";
        private const string PageName2 = "Page2";
        private const string ImageTitle = "Image1";
        private const string WidgetName = "Card";
        private const string HeadingText = "Heading text";
        private const string TextArea = "Text area text";
        private const string LabelText = "Label text";
        private const string TemplateName = "Card.CardTemplate";
        private const string TemplateContent = "This is test content";
    }
}
