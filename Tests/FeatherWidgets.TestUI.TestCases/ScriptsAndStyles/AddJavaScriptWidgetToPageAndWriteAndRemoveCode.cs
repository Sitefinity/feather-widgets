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
    /// AddJavaScriptWidgetToPageAndWriteAndRemoveCode test class.
    /// </summary>
    [TestClass]
    public class AddJavaScriptWidgetToPageAndWriteAndRemoveCode_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddJavaScriptWidgetToPageAndWriteAndRemoveCode
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ScriptsAndStyles)]
        public void AddJavaScriptWidgetToPageAndWriteAndRemoveCode()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().VerifyTips("Start writing your JavaScript");
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().VerifyCheckedRadioButtonOption(JavaScriptLocation);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().FillCodeInEditableArea(JavaScriptValue);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(Script);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(JavaScriptValue);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(JavaScriptLocation);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyCodeExistOnTheFrontend();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().FillCodeInEditableArea(string.Empty);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(Script, false);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(JavaScriptValue, false);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(JavaScriptLocation, false);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyCssNotExistOnTheFrontend();
        }

        /// <summary>
        /// Verify javascript exists on the frontend
        /// </summary>
        public void VerifyCodeExistOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            bool isContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(JavaScriptValue);
            Assert.IsTrue(isContained, string.Concat("Expected ", JavaScriptValue, " but the script is not found"));
            BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().VerifyJavaScriptInHeadTag(JavaScriptValue);
        }

        /// <summary>
        /// Verify javascript does not exist on the frontend
        /// </summary>
        public void VerifyCssNotExistOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            bool isContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(JavaScriptValue);
            Assert.IsFalse(isContained, string.Concat("Expected ", JavaScriptValue, " but the script is not found"));
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
        private const string JavaScriptValue = "var a = 5;";
        private const string Script = "<script type=\"text/javascript\">";
        private const string JavaScriptLocation = "In the head tag";
    }
}
