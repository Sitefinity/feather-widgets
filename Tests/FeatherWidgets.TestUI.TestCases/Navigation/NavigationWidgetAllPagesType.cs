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
    /// NavigationWidgetAllPagesType test class.
    /// </summary>
    [TestClass]
    public class NavigationWidgetAllPagesType_ : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetAllPagesType
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigationWidgetAllPagesType()
        {
            string pageTemplateName = "Bootstrap.default";
            string navTemplateClass = "nav navbar-nav";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            BAT.Wrappers().Frontend().Navigation().NavigationFrontendWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, this.NotVisiblePages);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.VisiblePages);
        }

        /// <summary>
        /// UI test NavigationWidgetAllPagesTypeFoundationTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Foundation)]
        public void NavigationWidgetAllPagesTypeFoundationTemplate()
        {
            string pageTemplateName = "Foundation.default";
            string navTemplateClass = "top-bar-section";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, this.NotVisiblePages, TemplateType.Foundation);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.VisiblePages, TemplateType.Foundation);
        }

        /// <summary>
        /// UI test NavigationWidgetAllPagesTypeSemanticUITemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void NavigationWidgetAllPagesTypeSemanticUITemplate()
        {
            string pageTemplateName = "SemanticUI.default";
            string navTemplateClass = "ui menu inverted";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(navTemplateClass, this.NotVisiblePages, TemplateType.Semantic);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.VisiblePages, TemplateType.Semantic);
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

        private string[] VisiblePages
        {
            get { return new string[] { PageName, Page2Redirect, Page1Redirect, PageGroup }; }
        }

        private string[] NotVisiblePages
        {
            get { return new string[] { ChildPage1, ChildPage2, UnpublishPage, PageDraft, Page2Group }; }
        }

        private const string WidgetName = "Navigation";
        private const string PageName = "ParentPage";
        private const string ChildPage1 = "ChildPage1";
        private const string ChildPage2 = "ChildPage2";
        private const string Page2Redirect = "Page2Redirect";
        private const string Page1Redirect = "Page1Redirect";
        private const string UnpublishPage = "UnpublishPage";
        private const string PageDraft = "PageDraft";
        private const string Page2Group = "Page2Group";
        private const string PageGroup = "PageGroup";
        private const string ArrangementClassName = "NavigationWidgetAllPagesType";
    }
}
