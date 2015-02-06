using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// This is a class with tests related to navigation widget option "All pages under particular page".
    /// </summary>
    [TestClass]
    public class NavigationWidgetAllPagesUnderParticularPage : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetAllPagesUnderParticularPage in Bootstrap template
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigationWidgetAllPagesUnderParticularPageBootstrap()
        {
            string pageTemplateName = "Bootstrap.default";
            string navTemplateClass = "nav navbar-nav";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(Page1);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectNavigationWidgetDisplayMode(NavWidgetDisplayMode);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(this.pagesToSelect);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().VerifySelectedItemsFromFlatSelector(this.pagesToSelect);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + Page1.ToLower());
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.childPage);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, this.parentPages);
        }

        /// <summary>
        /// UI test NavigationWidgetAllPagesUnderParticularPage in Foundation template
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Foundation)]
        public void NavigationWidgetAllPagesUnderParticularPageFoundation()
        {
            string pageTemplateName = "Foundation.default";
            string navTemplateClass = "top-bar-section";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(Page1);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectNavigationWidgetDisplayMode(NavWidgetDisplayMode);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(this.pagesToSelect);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().VerifySelectedItemsFromFlatSelector(this.pagesToSelect);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + Page1.ToLower());
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.childPage, TemplateType.Foundation);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, this.parentPages, TemplateType.Foundation);
        }

        /// <summary>
        /// UI test NavigationWidgetAllPagesUnderParticularPage in SemanticUI template
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void NavigationWidgetAllPagesUnderParticularPageSemanticUI()
        {
            string pageTemplateName = "SemanticUI.default";
            string navTemplateClass = "ui menu inverted";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteArrangement(this.ArrangementMethod);
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(Page1);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SelectNavigationWidgetDisplayMode(NavWidgetDisplayMode);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(this.pagesToSelect);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().VerifySelectedItemsFromFlatSelector(this.pagesToSelect);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + Page1.ToLower());
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.childPage, TemplateType.Semantic);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, this.parentPages, TemplateType.Semantic);           
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
            get { return "NavigationWidgetAllPagesUnderParticularPage"; }
        }

        private string ArrangementMethod
        {
            get { return "SetUp"; }
        }

        private readonly string[] pagesToSelect = new string[] { "ParentPage" };
        private readonly string[] childPage = new string[] { "ChildPage" };
        private readonly string[] parentPages = new string[] { "PageWithNavigationWidget", "ParentPage" };
        private const string Page1 = "PageWithNavigationWidget";
        private const string WidgetName = "Navigation";
        private const string NavWidgetDisplayMode = "All pages under particular page";
    }
}
