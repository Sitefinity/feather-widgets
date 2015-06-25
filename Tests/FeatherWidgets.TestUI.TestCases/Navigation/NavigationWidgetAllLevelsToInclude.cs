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
    /// NavigationWidgetAllLevelsToInclude test class.
    /// </summary>
    [TestClass]
    public class NavigationWidgetAllLevelsToInclude_ : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetAllLevelsToInclude
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigationWidgetAllLevelsToInclude()
        {
            string pageTemplateName = "Bootstrap.default";
            string navTemplateClass = "nav nav-pills nav-stacked";
            string navTemplateChildClass = "group nav nav-pills nav-stacked";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectLevelsToInclude(LevelsToInclude);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectWidgetListTemplate(NavWidgetTemplate);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            string[] parentPages = new string[] { PageName };
            string[] childPages = new string[] { ChildPage1, ChildPage2 };

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, parentPages);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyChildPagesFrontEndNavigation(navTemplateChildClass, childPages);

            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().SelectPageFromNavigationByText(navTemplateClass, ChildPage1);
            ActiveBrowser.WaitForUrl("/" + ChildPage1.ToLower(), true, 60000);
        }

        /// <summary>
        /// UI test NavigationWidgetAllLevelsToIncludeFoundationTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Foundation)]
        public void NavigationWidgetAllLevelsToIncludeFoundationTemplate()
        {
            string pageTemplateName = "Foundation.default";
            string navTemplateClass = "side-nav";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectLevelsToInclude(LevelsToInclude);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectWidgetListTemplate(NavWidgetTemplate);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            string[] parentPages = new string[] { PageName };
            string[] childPages = new string[] { ChildPage1, ChildPage2 };

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, parentPages, TemplateType.Foundation);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyChildPagesFrontEndNavigation(navTemplateClass, childPages, TemplateType.Foundation);

            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().SelectPageFromNavigationByText(navTemplateClass, ChildPage1);
            ActiveBrowser.WaitForUrl("/" + ChildPage1.ToLower(), true, 60000);
        }

        /// <summary>
        /// UI test NavigationWidgetAllLevelsToIncludeSemanticUITemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void NavigationWidgetAllLevelsToIncludeSemanticUITemplate()
        {
            string pageTemplateName = "SemanticUI.default";
            string navTemplateClass = "ui orange vertical inverted menu";
            string navTemplateChildClass = "menu";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectLevelsToInclude(LevelsToInclude);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectWidgetListTemplate(NavWidgetTemplate);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            string[] parentPages = new string[] { PageName };
            string[] childPages = new string[] { ChildPage1, ChildPage2 };

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, parentPages, TemplateType.Semantic);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyChildPagesFrontEndNavigation(navTemplateChildClass, childPages, TemplateType.Semantic);

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(ChildPage1, TemplateType.Semantic, navTemplateChildClass, false);
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

        private string ArrangementMethod
        {
            get { return ArrangementMethodName; }
        }

        private const string PageName = "ParentPage";
        private const string ChildPage1 = "ChildPage1";
        private const string ChildPage2 = "ChildPage2";
        private const string WidgetName = "Navigation";
        private const string LevelsToInclude = "-1";
        private const string NavWidgetTemplate = "Vertical";
        private const string ArrangementClassName = "NavigationWidgetAllLevelsToInclude";
        private const string ArrangementMethodName = "SetUp";
    }
}
