using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// CreateSocialShareWidgetTemplateAndCheckTheDesigner test class.
    /// </summary>
    [TestClass]
    public class CreateSocialShareWidgetTemplateAndCheckTheDesigner_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CreateSocialShareWidgetTemplateAndCheckTheDesigner
        /// </summary>
        [TestMethod,
        Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.SocialShare)]
        public void CreateSocialShareWidgetTemplateAndCheckTheDesigner()
        {
            BAT.Macros().NavigateTo().Design().WidgetTemplates();
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesWrapper().CreateTemplate();
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().SelectTemplate("SocialShare (MVC)");
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().EnterTextInTextArea(TemplateContent);
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().EnterWidgetTemplateName(TemplateTitle);
            BATFeather.Wrappers().Backend().WidgetTemplates().WidgetTemplatesCreateScreenFrameWrapper().CreateThisTemplate();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToDropZone(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().SocialShare().SocialShareWidgetEditWrapper().SelectWidgetListTemplate(TemplateTitle);        
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, TemplateContent);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.IsTrue(BATFeather.Wrappers().Frontend().SocialShare().SocialShareWrapper().IsSocialShareTemplateContentPresentOnTheFrontend(new[] { TemplateContent }), "Template content not found");

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().SocialShare().SocialShareWidgetEditWrapper().SelectWidgetListTemplate(TemplateIconsWithText);
            BATFeather.Wrappers().Backend().SocialShare().SocialShareWidgetEditWrapper().SelectSocialShareOptions(this.optionTitlesToSelect);
            BATFeather.Wrappers().Backend().SocialShare().SocialShareWidgetEditWrapper().UnselectSocialShareOptions(this.optionTitlesToUnselect);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            Assert.AreEqual(5, BATFeather.Wrappers().Backend().SocialShare().SocialSharePageEditorWrapper().CountOfSocialShareOptions(), "Count is not correct");
            BATFeather.Wrappers().Backend().SocialShare().SocialSharePageEditorWrapper().VerifySocialShareTextPresentOnBackend(this.optionTitlesTextToBeVisible);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.AreEqual(5, BATFeather.Wrappers().Frontend().SocialShare().SocialShareWrapper().CountOfSocialShareOptions(), "Count is not correct");
            BATFeather.Wrappers().Frontend().SocialShare().SocialShareWrapper().VerifySocialShareTextPresentOnFrontend(this.optionTitlesTextToBeVisible);
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
        /// Performs clean up and clears all data created by the test
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "SocialShare";
        private const string WidgetName = "Social share";
        private const string TemplateTitle = "SocialShareTest";
        private const string TemplateIconsWithText = "SocialShareIconsWithText";
        private const string TemplateContent = "SocialShareTest";
        private readonly string[] optionTitlesToSelect = new string[] { "Blogger", "Digg", "My Space", "Google bookmarks", "Delicious" };
        private readonly string[] optionTitlesToUnselect = new string[] { "Google +", "LinkedIn", "Facebook", "Twitter" };
        private readonly string[] optionTitlesTextToBeVisible = new string[] { "Google Bookmarks", "Save this on Delicious", "Blogger", "MySpace", "Digg" };
    }
}
