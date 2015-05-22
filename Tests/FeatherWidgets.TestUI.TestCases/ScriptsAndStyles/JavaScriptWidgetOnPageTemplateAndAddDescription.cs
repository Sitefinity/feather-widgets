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
    /// JavaScriptWidgetOnPageTemplateAndAddDescription test class.
    /// </summary>
    [TestClass]
    public class JavaScriptWidgetOnPageTemplateAndAddDescription_ : FeatherTestCase
    {
        /// <summary>
        /// UI test JavaScriptWidgetOnPageTemplateAndAddDescription
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ScriptsAndStyles)]
        public void JavaScriptWidgetOnPageTemplateAndAddDescription()
        {
            ActiveBrowser.WaitUntilReady();
            BAT.Macros().NavigateTo().Design().PageTemplates();
            this.OpenTemplateEditor();
          
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName, PlaceHolder);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().FillCodeInEditableArea(JavaScriptValue);
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().FillDescription(TestDescription);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, TestDescription);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetName, JavaScriptValue);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetName, Script);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetName, JavaScriptLocation);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();

            this.VerifyPageOnTheFrontend();
        }

        /// <summary>
        /// Verify css on the frontend
        /// </summary>
        public void VerifyPageOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            bool isContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(JavaScriptValue);
            Assert.IsTrue(isContained, string.Concat("Expected ", JavaScriptValue, " but the style is not found"));
            BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().VerifyJavaScriptInHeadTag(JavaScriptValue);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(TestArrangement).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(TestArrangement).ExecuteTearDown();
        }

        private void OpenTemplateEditor()
        {
            var templateId = BAT.Arrange(TestArrangement).ExecuteArrangement("GetTemplateId").Result.Values["templateId"];

            BAT.Macros().NavigateTo().CustomPage("~/Sitefinity/Template/" + templateId, false);
            ActiveBrowser.WaitForAsyncOperations();
        }

        private const string TestArrangement = "CssWidgetOnPageTemplateAndAddDescription";
        private const string PageName = "FeatherPage";
        private const string WidgetName = "JavaScript";
        private const string JavaScriptValue = "var a = 5;";
        private const string PlaceHolder = "TestPlaceHolder";
        private const string TestDescription = "Test description";
        private const string Script = "<script type=\"text/javascript\">";
        private const string JavaScriptLocation = "In the head tag";
    }
}
