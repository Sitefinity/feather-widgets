﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace FeatherWidgets.TestUI.TestCases.ScriptsAndStyles
{
    /// <summary>
    /// DuplicateCssWidgetOnPage test class.
    /// </summary>
    [TestClass]
    public class DuplicateCssWidgetOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DuplicateCssWidgetOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.ScriptsAndStyles),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void DuplicateCssWidgetOnPage()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            HtmlDiv radDockZone = ActiveBrowser.Find
                                  .ByExpression<HtmlDiv>("placeholderid=" + "Contentplaceholder1")
                .AssertIsPresent<HtmlDiv>("Contentplaceholder1");
            radDockZone.MouseClick();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().FillCodeInEditableArea(CssValue);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().CssWidgetEditWrapper().FillCodeInEditableArea(SecondCssValue);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyCssExistOnTheFrontend();
        }

        /// <summary>
        /// Verify css exists on the frontend
        /// </summary>
        public void VerifyCssExistOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            bool isContainedFirst = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(CssValue);
            Assert.IsTrue(isContainedFirst, string.Concat("Expected ", CssValue, " but the style is not found"));

            bool isContainedSecond = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(SecondCssValue);
            Assert.IsTrue(isContainedSecond, string.Concat("Expected ", SecondCssValue, " but the style is not found"));
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
        private const string WidgetName = "CSS";
        private const string CssValue = "div { color: #FF0000; font-size: 20px;} ";
        private const string SecondCssValue = "div { color: #00FF00;} ";
        private const string OperationName = "Duplicate";
    }
}
