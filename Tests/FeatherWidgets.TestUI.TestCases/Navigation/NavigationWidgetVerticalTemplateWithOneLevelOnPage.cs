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
    /// NavigationWidgetVerticalTemplateWithOneLevelOnPage test class.
    /// </summary>
    [TestClass]
    public class NavigationWidgetVerticalTemplateWithOneLevelOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetVerticalTemplateWithOneLevelOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigationWidgetVerticalTemplateWithOneLevelOnPage()
        {
            string pageTemplateName = "Bootstrap.default";
            string navTemplateClass = "nav nav-pills nav-stacked";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectWidgetListTemplate(NavWidgetTemplate);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.Pages);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(SiblingPageName, TemplateType.Bootstrap, navTemplateClass);
        }

        /// <summary>
        /// UI test NavigationWidgetVerticalTemplateWithOneLevelOnPageFoundation
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Foundation)]
        public void NavigationWidgetVerticalTemplateWithOneLevelOnPageFoundation()
        {
            string pageTemplateName = "Foundation.default";
            string navTemplateClass = "side-nav";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectWidgetListTemplate(NavWidgetTemplate);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.Pages, TemplateType.Foundation);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(SiblingPageName, TemplateType.Foundation, navTemplateClass);
        }

        /// <summary>
        /// UI test NavigationWidgetVerticalTemplateWithOneLevelOnPageSemanticUI
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void NavigationWidgetVerticalTemplateWithOneLevelOnPageSemanticUI()
        {
            string pageTemplateName = "SemanticUI.default";
            string navTemplateClass = "ui orange vertical inverted menu";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectWidgetListTemplate(NavWidgetTemplate);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.Pages, TemplateType.Semantic);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(SiblingPageName, TemplateType.Semantic, navTemplateClass);
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

        private string[] Pages
        {
            get
            {
                return new string[] { PageName, SiblingPageName };
            }
        }

        private const string PageName = "ParentPage";
        private const string SiblingPageName = "SiblingPage";
        private const string WidgetName = "Navigation";
        private const string NavWidgetTemplate = "Vertical";
        private const string ArrangementClassName = "NavigationWidgetVerticalTemplateWithOneLevelOnPage";
    }
}
