using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// DeleteNavigationWidgetFromPage test class.
    /// </summary>
    [TestClass]
    public class DeleteNavigationWidgetFromPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeleteNavigationWidgetFromPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void DeleteNavigationWidgetFromPage()
        {
            string pageTemplateName = "Bootstrap.default";
            string navTemplateClass = "nav navbar-nav";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.ParentPages);

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);

            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(navTemplateClass, TemplateType.Foundation), "Navigation is visible but it shouldn't");
        }

        /// <summary>
        /// UI test DeleteNavigationWidgetFromPageFoundationTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Foundation)]
        public void DeleteNavigationWidgetFromPageFoundationTemplate()
        {
            string pageTemplateName = "Foundation.default";
            string navTemplateClass = "top-bar-section";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.ParentPages, TemplateType.Foundation);

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);

            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(navTemplateClass, TemplateType.Foundation), "Navigation is visible but it shouldn't");
        }

        /// <summary>
        /// UI test DeleteNavigationWidgetFromPageSemanticUITemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void DeleteNavigationWidgetFromPageSemanticUITemplate()
        {
            string pageTemplateName = "SemanticUI.default";
            string navTemplateClass = "ui menu inverted";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(navTemplateClass, this.ParentPages, TemplateType.Semantic);

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);

            Assert.IsFalse(BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().AssertNavigationIsVisible(navTemplateClass, TemplateType.Semantic), "Navigation is visible but it shouldn't");
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

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.ArrangementClass).ExecuteTearDown();
        }

        private const string PageName = "ParentPage";
        private const string WidgetName = "Navigation";
        private const string OperationName = "Delete";
        private const int ExpectedCount = 0;
        private const string ArrangementClassName = "DeleteNavigationWidgetFromPage";
    }
}
