using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.WebAii.Controls.Html;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// NavigationWidgetHorizontalTemplateWithOneLevelOnPage test class.
    /// </summary>
    [TestClass]
    public class NavigationWidgetHorizontalTemplateWithOneLevelOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetHorizontalTemplateWithOneLevelOnPage
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigationWidgetHorizontalTemplateWithOneLevelOnPage()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Macros().NavigateTo().Pages();
            this.CreatePageWithTemplate(PageName, PageTemplateName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidget(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNavigationOnTheFrontend();
        }

        /// <summary>
        /// Create page with template
        /// </summary>
        /// <param name="pageName">Page name</param>
        /// <param name="templateName">Template name</param>
        public void CreatePageWithTemplate(string pageName, string templateName)
        {
            var createPageLink = BAT.Wrappers().Backend().Pages().PagesWrapper().GetCreatePageFromDecisionScreen();
            createPageLink.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().SetPageTitle(pageName);
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().ClickSelectAnotherTemplateButton();
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().SelectATemplate(templateName);
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().ClickDoneButton();
            BAT.Wrappers().Backend().Pages().PagesWrapper().SavePageDataAndContinue();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().WaitUntilReady();
        }

        /// <summary>
        /// Verify navigation widget on the frontend
        /// </summary>
        public void VerifyNavigationOnTheFrontend()
        {
            string[] selectedPages = new string[] { PageName };

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(NavTemplateClass, selectedPages);
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "ParentPage";
        private const string WidgetName = "Navigation";
        private const string NavTemplateClass = "nav navbar-nav";
        private const string PageTemplateName = "Bootstrap.default";
    }
}
