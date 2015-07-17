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
    /// SelectCssFileInCssWidget test class.
    /// </summary>
    [TestClass]
    public class SelectCssFileInCssWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectCssFileInCssWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ScriptsAndStyles)]
        public void SelectCssFileInCssWidget()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().FillCodeInEditableArea(CssValue);
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().SwitchToLinkFileOption();
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().ExpandFolder(FolderName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().SelectFile(FileName);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyPageOnTheFrontend();
        }

        /// <summary>
        /// Verify css on the frontend
        /// </summary>
        public void VerifyPageOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            bool isContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(CssValueExpected);
            Assert.IsTrue(isContained, string.Concat("Expected ", CssValueExpected, " but the style is not found"));

            bool isNotContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(CssValue);
            Assert.IsFalse(isNotContained, string.Concat("Not expected ", CssValue, " but the style is found"));
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

        private const string PageName = "PageWithCssWidget";
        private const string ContentBlockContent = "Test content";
        private const string WidgetName = "CSS";
        private const string CssValue = "div { color: #00FF00;} ";
        private const string CssValueExpected = "/Css/styles.css";
        private const string FolderName = "Css";
        private const string FileName = "styles.css";
    }
}
