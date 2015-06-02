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
    /// AddJavaScriptInBodyTag test class.
    /// </summary>
    [TestClass]
    public class AddJavaScriptBeforeTheClosingBodyTag_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddJavaScriptInBodyTag
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ScriptsAndStyles)]
        public void AddJavaScriptBeforeTheClosingBodyTag()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().IncludeJavaScriptBeforeTheClosingBodyTag();
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().FillCodeInEditableArea(JavaScriptValue);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(Script);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(JavaScriptValue);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(JavaScriptLocation);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyCodeExistOnTheFrontend(JavaScriptValue);

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().VerifyCheckedRadioButtonOption(JavaScriptLocation);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().FillCodeInEditableArea(JavaScriptNewValue);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(Script);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(JavaScriptNewValue);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(JavaScriptLocation);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyCodeExistOnTheFrontend(JavaScriptNewValue);
        }

        /// <summary>
        /// Verify javascript exists on the frontend
        /// </summary>
        public void VerifyCodeExistOnTheFrontend(string value)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            bool isContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(value);
            Assert.IsTrue(isContained, string.Concat("Expected ", JavaScriptValue, " but the script is not found"));
            BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().VerifyJavaScriptBeforeTheClosingBodyTag(value);
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
        private const string JavaScriptLocation = "Before the closing body tag";
        private const string JavaScriptNewValue = "var a = 10;";
    }
}
