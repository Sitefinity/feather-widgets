using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// NavigationWidgetAllSiblingPagesOfCurrentlyOpenedPage test class.
    /// </summary>
    [TestClass]
    public class NavigationWidgetAllSiblingPagesOfCurrentlyOpenedPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetAllSiblingPagesOfCurrentlyOpenedPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigationWidgetAllSiblingPagesOfCurrentlyOpenedPage()
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

            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, this.ChildPages);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.ParentPages);

            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().SelectPageFromNavigationByText(navTemplateClass, SiblingPageName);
            ActiveBrowser.WaitForUrl("/" + SiblingPageName.ToLower(), true, 60000);
        }

        /// <summary>
        /// UI test NavigationWidgetAllSiblingPagesOfCurrentlyOpenedPageFoundation
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Foundation)]
        public void NavigationWidgetAllSiblingPagesOfCurrentlyOpenedPageFoundation()
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

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, this.ChildPages, TemplateType.Foundation);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.ParentPages, TemplateType.Foundation);

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(SiblingPageName, TemplateType.Foundation, navTemplateClass);
        }

        /// <summary>
        /// UI test NavigationWidgetAllSiblingPagesOfCurrentlyOpenedPageSemanticUI
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void NavigationWidgetAllSiblingPagesOfCurrentlyOpenedPageSemanticUI()
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

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, this.ChildPages, TemplateType.Semantic);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.ParentPages, TemplateType.Semantic);

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(SiblingPageName, TemplateType.Semantic, navTemplateClass);
        }

        private string ArrangementClass
        {
            get { return ArrangementClassName; }
        }

        private string[] ParentPages
        {
            get
            {
                return new string[] { PageName, SiblingPageName };
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
        private const string SiblingPageName = "SiblingPage";
        private const string ChildPage1 = "ChildPage1";
        private const string ChildPage2 = "ChildPage2";
        private const string WidgetName = "Navigation";
        private const string NavWidgetDisplayMode = "All sibling pages of currently opened page";
        private const string ArrangementClassName = "NavigationWidgetAllSiblingPagesOfCurrentlyOpenedPage";
    }
}
