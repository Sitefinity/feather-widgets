using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// UI tests for navigation widget on bootstrap template
    /// </summary>
    [TestClass]
    public class NavigationWidgetOnBootstrapTemplate : FeatherTestCase
    {
        /// <summary>
        /// UI test DragAndDropNavigationWidgetOnBootstrapTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void DragAndDropNavigationWidgetOnBootstrapTemplate()
        {
            string[] parentPages = new string[] { Page1, Page2 };

            BAT.Macros().NavigateTo().Design().PageTemplates();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(PageTemplateName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();

            BAT.Arrange(this.ArrangementClass).ExecuteArrangement(this.ArrangementMethod);

            BAT.Macros().NavigateTo().CustomPage("~/" + Page1.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(NavTemplateClass, parentPages);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(Page2, TemplateType.Bootstrap, NavTemplateClass);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(NavTemplateClass, parentPages);        
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).ExecuteSetUp();
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

        private const string Page1 = "BootstrapPage1";
        private const string Page2 = "BootstrapPage2";
        private const string PageTemplateName = "defaultNew";
        private const string ArrangementClassName = "NavigationWidgetOnBootstrapTemplate";
        private const string ArrangementMethodName = "CreatePages";
        private const string WidgetName = "Navigation";
        private const string NavTemplateClass = "nav navbar-nav";
    }
}
