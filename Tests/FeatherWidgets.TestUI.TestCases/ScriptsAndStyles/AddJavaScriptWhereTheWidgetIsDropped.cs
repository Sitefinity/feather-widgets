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
    /// AddJavaScriptWhereTheWidgetIsDropped test class.
    /// </summary>
    [TestClass]
    public class AddJavaScriptWhereTheWidgetIsDropped_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddJavaScriptWhereTheWidgetIsDropped
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ScriptsAndStyles)]
        public void AddJavaScriptWhereTheWidgetIsDropped()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().IncludeJavaScriptWhereTheWidgetIsDropped();
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
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().MoreOptions();
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().VerifyCheckedRadioButtonOption(JavaScriptLocation);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().IncludeJavaScriptBeforeTheClosingBodyTag();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(Script);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(JavaScriptValue);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyJavaScriptWidgetText(JavaScriptNewLocation);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyCodeExistOnTheFrontend(true);
        }

        /// <summary>
        /// Verify javascript exists on the frontend
        /// </summary>
        public void VerifyCodeExistOnTheFrontend(bool isLocationOptionChanged = false)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            bool isContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(JavaScriptValue);
            Assert.IsTrue(isContained, string.Concat("Expected ", JavaScriptValue, " but the script is not found"));
            if (isLocationOptionChanged)
            {
                BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().VerifyJavaScriptBeforeTheClosingBodyTag(JavaScriptValue);
            }
            else 
            {
                BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().VerifyWhereTheWidgetIsDroppedOption(JavaScriptValue);
            }
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

        private const string TestArrangement = "AddJavaScriptBeforeTheClosingBodyTag";
        private const string PageName = "PageWithJavaScriptWidget";
        private const string WidgetName = "JavaScript";
        private const string JavaScriptValue = "var a = 5;";
        private const string Script = "<script type=\"text/javascript\">";
        private const string JavaScriptLocation = "Where the widget is dropped";
        private const string JavaScriptNewLocation = "Before the closing body tag";
    }
}
