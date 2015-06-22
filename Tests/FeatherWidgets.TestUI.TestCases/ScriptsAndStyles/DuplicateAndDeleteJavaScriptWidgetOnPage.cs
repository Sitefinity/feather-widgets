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
    /// DuplicateAndDeleteJavaScriptWidgetOnPage test class.
    /// </summary>
    [TestClass]
    public class DuplicateAndDeleteJavaScriptWidgetOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DuplicateAndDeleteJavaScriptWidgetOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ScriptsAndStyles)]
        public void DuplicateAndDeleteJavaScriptWidgetOnPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().FillCodeInEditableArea(JavaScriptValue);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().VerifyCodeInEditableArea("vara=5");
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().FillCodeInEditableArea(SecondJavaScriptValue);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            this.VerifyCodeExistOnTheFrontend(JavaScriptValue);
            this.VerifyCodeExistOnTheFrontend(SecondJavaScriptValue);

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName1);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            this.VerifyCodeExistOnTheFrontend(SecondJavaScriptValue);
            this.VerifyCodeDoesNotExistOnTheFrontend(JavaScriptValue);
        }

        /// <summary>
        /// Verify code exists on the frontend
        /// </summary>
        public void VerifyCodeExistOnTheFrontend(string code)
        {
            bool isContainedFirst = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(code);
            Assert.IsTrue(isContainedFirst, string.Concat("Expected ", code, " but the style is not found"));
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

        private void VerifyCodeDoesNotExistOnTheFrontend(string code)
        {
            bool isContainedSecond = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(code);
            Assert.IsFalse(isContainedSecond, string.Concat("Expected not", code, " but the style is found"));
        }

        private const string TestArrangement = "AddJavaScriptBeforeTheClosingBodyTag";
        private const string PageName = "PageWithJavaScriptWidget";
        private const string WidgetName = "JavaScript";
        private const string JavaScriptValue = "var a = 5;";
        private const string SecondJavaScriptValue = "var b = 10; ";
        private const string OperationName = "Duplicate";
        private const string OperationName1 = "Delete";
    }
}
