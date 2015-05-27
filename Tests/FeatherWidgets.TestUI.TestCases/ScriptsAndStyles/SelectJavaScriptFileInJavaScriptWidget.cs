using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ScriptsAndStyles
{
    /// <summary>
    /// SelectJavaScriptFileInJavaScriptWidget test class.
    /// </summary>
    [TestClass]
    public class SelectJavaScriptFileInJavaScriptWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectJavaScriptFileInJavaScriptWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ScriptsAndStyles)]
        public void SelectJavaScriptFileInJavaScriptWidget()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().FillCodeInEditableArea(JavaScriptValue);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().IncludeJavaScriptBeforeTheClosingBodyTag();
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().SwitchToLinkFileOption();
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().VerifyCheckedRadioButtonOption(JavaScriptLocation);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().ExpandFolder(FolderName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().SelectFile(FileName);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(Script);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(JavaScriptLocation);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyPageOnTheFrontend();
        }

        /// <summary>
        /// Verify css on the frontend
        /// </summary>
        public void VerifyPageOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            bool isContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(ExpectedValue);
            Assert.IsTrue(isContained, string.Concat("Expected ", ExpectedValue, " but the script is not found"));
            BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().VerifyJavaScriptFileBeforeTheClosingBodyTag(ExpectedValue);
            BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().VerifyScriptResultInAboutFieldOfProfileWidget("11");

            bool isNotContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(JavaScriptValue);
            Assert.IsFalse(isNotContained, string.Concat("Not expected ", JavaScriptValue, " but the style is found"));
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

        private const string PageName = "PageWithJavaScriptWidget";
        private const string WidgetName = "JavaScript";
        private const string ExpectedValue = "/JavaScript/JavaScriptWidgetTest.js";
        private const string FolderName = "JavaScript";
        private const string FileName = "JavaScriptWidgetTest.js";
        private const string JavaScriptValue = "var a = 5;";
        private const string JavaScriptLocation = "Before the closing body tag";
        private const string Script = "<script type=\"text/javascript\" src=\"/JavaScript/JavaScriptWidgetTest.js\"></script>";
    }
}
