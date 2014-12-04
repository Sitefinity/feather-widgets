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
            this.VerifyNavigationOnTheFrontend(NavTemplateClass, TemplateType.Bootstrap);
        }

        /// <summary>
        /// UI test NavigationWidgetHorizontalTemplateWithOneLevelOnPageFoundation
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Foundation)]
        public void NavigationWidgetHorizontalTemplateWithOneLevelOnPageFoundation()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Macros().NavigateTo().Pages();
            this.CreatePageWithTemplate(PageName, FoundationTemplateName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidget(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNavigationOnTheFrontend(FoundationNavTemplateClass, TemplateType.Foundation);
        }

        /// <summary>
        /// UI test NavigationWidgetHorizontalTemplateWithOneLevelOnPageSemanticUI
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void NavigationWidgetHorizontalTemplateWithOneLevelOnPageSemanticUI()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Macros().NavigateTo().Pages();
            this.CreatePageWithTemplate(PageName, SemanticUITemplateName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidget(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNavigationOnTheFrontend(SemanticNavTemplateClass ,TemplateType.Semantic);
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
        public void VerifyNavigationOnTheFrontend(string navClass, TemplateType type)
        {
            string[] selectedPages = new string[] { PageName };

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navClass, selectedPages, type);
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.ArrangementClass).ExecuteTearDown();
        }

        private string ArrangementClass
        {
            get { return ArrangementClassName; }
        }

        private const string PageName = "ParentPage";
        private const string WidgetName = "Navigation";
        private const string NavTemplateClass = "nav navbar-nav";
        private const string FoundationNavTemplateClass = "top-bar-section";
        private const string SemanticNavTemplateClass = "ui menu purple inverted";
        private const string PageTemplateName = "Bootstrap.default";
        private const string FoundationTemplateName = "Foundation.default";
        private const string SemanticUITemplateName = "SemanticUI.default";
        private string ArrangementClassName = "NavigationWidgetHorizontalTemplateWithOneLevelOnPage";
    }
}
