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
    /// AddCardWidgetOnPageTemplate test class.
    /// </summary>
    [TestClass]
    public class AddCardWidgetOnPageTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddCardWidgetOnPageTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Card)]
        public void AddCardWidgetOnPageTemplate()
        {
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(PageTemplateName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName, "Body");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Card().CardWrapper().FillHeadingText(HeadingText);
            BATFeather.Wrappers().Backend().Card().CardWrapper().FillTextArea(TextArea);
            BATFeather.Wrappers().Backend().Card().CardWrapper().ClickSelectImageButton();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(ImageTitle);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Card().CardWrapper().FillLabel(LabelText);
            BATFeather.Wrappers().Backend().Card().CardWrapper().SelectExternalUrlOption();
            BATFeather.Wrappers().Backend().Card().CardWrapper().FillExternalUrl(ExternalUrl);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Card().CardWrapper().VerifyCardWidgetContentOnFrontend(HeadingText);
            BATFeather.Wrappers().Frontend().Card().CardWrapper().VerifyCardWidgetContentOnFrontend(TextArea);
            BATFeather.Wrappers().Frontend().Card().CardWrapper().VerifyImageIsPresentOnFrontend(ImageTitle);
            BATFeather.Wrappers().Frontend().Card().CardWrapper().VerifyCardWidgetContentOnFrontend(LabelText);
            BATFeather.Wrappers().Frontend().Card().CardWrapper().VerifyPageIsPresentOnFrontend(LabelText, ExternalUrl.ToLower());
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

        private const string PageTemplateName = "CardTemplate";
        private const string PageName = "CardPage";
        private const string ImageTitle = "Image1";
        private const string WidgetName = "Card";
        private const string HeadingText = "Heading text";
        private const string TextArea = "Text area text";
        private const string LabelText = "Sitefinity site";
        private const string ExternalUrl = "http://sitefinity.com";
    }
}
