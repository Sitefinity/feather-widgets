using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// NavigationWidgetAllPagesUnderCurrentlyOpenedPage test class.
    /// </summary>
    [TestClass]
    public class NavigationWidgetAllPagesUnderCurrentlyOpenedPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetAllPagesUnderCurrentlyOpenedPage
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigationWidgetAllPagesUnderCurrentlyOpenedPage()
        {
            string pageTemplateName = "Bootstrap.default";
            string navTemplateClass = "nav navbar-nav";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectNavigationWidgetDisplayMode(NavWidgetDisplayMode);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();

            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, ParentPages);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, ChildPages);

            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().SelectPageFromNavigationByText(navTemplateClass, ChildPage1);
            ActiveBrowser.WaitForUrl("/" + ChildPage1.ToLower(), true, 60000);
        }

        /// <summary>
        /// UI test NavigationWidgetAllPagesUnderCurrentlyOpenedPageFoundation
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Foundation)]
        public void NavigationWidgetAllPagesUnderCurrentlyOpenedPageFoundation()
        {
            string pageTemplateName = "Foundation.default";
            string navTemplateClass = "top-bar-section";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectNavigationWidgetDisplayMode(NavWidgetDisplayMode);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, ParentPages, TemplateType.Foundation);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, ChildPages, TemplateType.Foundation);

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(ChildPage1, TemplateType.Foundation, navTemplateClass);
        }

        /// <summary>
        /// UI test NavigationWidgetAllPagesUnderCurrentlyOpenedPageSemanticUI
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void NavigationWidgetAllPagesUnderCurrentlyOpenedPageSemanticUI()
        {
            string pageTemplateName = "SemanticUI.default";
            string navTemplateClass = "ui menu inverted";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectNavigationWidgetDisplayMode(NavWidgetDisplayMode);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, ParentPages, TemplateType.Semantic);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, ChildPages, TemplateType.Semantic);

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(ChildPage1, TemplateType.Semantic, navTemplateClass);
        }

        private string ArrangementClass
        {
            get { return ArrangementClassName; }
        }

        private string[] ParentPages
        {
            get
            {
                return new string[] { PageName };
            }
        }

        private string[] ChildPages
        {
            get
            {
                return new string[] { ChildPage1, ChildPage2 };
            }
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.ArrangementClass).ExecuteTearDown();
        }

        private const string PageName = "ParentPage";
        private const string ChildPage1 = "ChildPage1";
        private const string ChildPage2 = "ChildPage2";
        private const string WidgetName = "Navigation";
        private const string NavWidgetDisplayMode = "All pages under currently opened page";
        private const string ArrangementClassName = "NavigationWidgetAllPagesUnderCurrentlyOpenedPage";
    }
}
