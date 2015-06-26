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
    /// AddCssWidgetToPageAndWriteAndRemoveCss test class.
    /// </summary>
    [TestClass]
    public class AddCssWidgetToPageAndWriteAndRemoveCss_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddCssWidgetToPageAndWriteAndRemoveCss
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ScriptsAndStyles)]
        public void AddCssWidgetToPageAndWriteAndRemoveCss()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().FillCodeInEditableArea(CssValue);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyCssExistOnTheFrontend();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().FillCodeInEditableArea(string.Empty);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyCssNotExistOnTheFrontend();
        }

        /// <summary>
        /// Verify css exists on the frontend
        /// </summary>
        public void VerifyCssExistOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            bool isContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(CssValue);
            Assert.IsTrue(isContained, string.Concat("Expected ", CssValue, " but the style is not found"));
        }

        /// <summary>
        /// Verify css does not exist on the frontend
        /// </summary>
        public void VerifyCssNotExistOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            bool isContained = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(CssValue);
            Assert.IsFalse(isContained, string.Concat("Expected ", CssValue, " but the style is not found"));
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
        private const string CssValue = "div { color: #FF0000; font-size: 20px;} ";
    }
}
