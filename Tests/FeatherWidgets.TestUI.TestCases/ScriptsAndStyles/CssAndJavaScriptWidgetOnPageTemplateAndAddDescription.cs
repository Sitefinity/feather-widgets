using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ScriptsAndStyles
{
    /// <summary>
    /// CssAndJavaScriptWidgetOnPageTemplateAndAddDescription test class.
    /// </summary>
    [TestClass]
    public class CssAndJavaScriptWidgetOnPageTemplateAndAddDescription_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CssAndJavaScriptWidgetOnPageTemplateAndAddDescription
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ScriptsAndStyles), Ignore]
        public void CssAndJavaScriptWidgetOnPageTemplateAndAddDescription()
        {
            BAT.Macros().NavigateTo().Design().PageTemplates();
            this.OpenTemplateEditor();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetNameContentBlock, PlaceHolder);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetNameContentBlock);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(ContentBlockContent);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName, PlaceHolder);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().FillCodeInEditableArea(CssValue);
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().FillDescription(TestDescription);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetName, CssValue);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, TestDescription);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetNameJavaScript, PlaceHolder);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetNameJavaScript);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().FillCodeInEditableArea(JavaScriptValue);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().FillDescription(TestDescriptionJavaScript);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetNameJavaScript, TestDescriptionJavaScript);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetNameJavaScript, JavaScriptValue);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetNameJavaScript, Script);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetNameJavaScript, JavaScriptLocation);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();

            this.VerifyPageOnTheFrontend();
        }

        /// <summary>
        /// Verify css on the frontend
        /// </summary>
        public void VerifyPageOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            bool isContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(CssValue);
            Assert.IsTrue(isContained, string.Concat("Expected ", CssValue, " but the style is not found"));

            bool isContainedJavaScript = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(JavaScriptValue);
            Assert.IsTrue(isContainedJavaScript, string.Concat("Expected ", JavaScriptValue, " but the style is not found"));
            BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().VerifyJavaScriptInHeadTag(JavaScriptValue);
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
            ActiveBrowser.WaitForAsyncOperations();
        }

        private const string PageName = "FeatherPage";
        private const string TemplateTitle = "TestLayout";
        private const string WidgetName = "CSS";
        private const string CssValue = "div { color: #FF0000; font-size: 20px;} ";
        private const string PlaceHolder = "TestPlaceHolder";
        private const string TestDescription = "Test description css";
        private const string LayoutText = "Test Layout";
        private const string ServerErrorMessage = "Server Error";
        private const string ContentBlockContent = "Test content";
        private const string WidgetNameContentBlock = "Content block";
        private const string WidgetNameJavaScript = "JavaScript";
        private const string JavaScriptValue = "var a = 5;";
        private const string TestDescriptionJavaScript = "Test description java script";
        private const string Script = "<script type=\"text/javascript\">";
        private const string JavaScriptLocation = "In the head tag";
    }
}
