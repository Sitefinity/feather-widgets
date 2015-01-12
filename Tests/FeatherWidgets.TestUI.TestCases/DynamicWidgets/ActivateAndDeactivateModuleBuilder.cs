using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    [TestClass]
    public class ActivateAndDeactivateModuleBuilder_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ActivateAndDeactivateModuleBuilder
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void ActivateAndDeactivateModuleBuilder()
        {
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().NavigateToModules();
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().DeactivateModule(ModuleName);
            this.NavigatePageOnTheFrontend();
            Assert.IsFalse(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(this.dynamicTitles));
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().NavigateToModules();
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().ActivateModule(ModuleName);
            this.NavigatePageOnTheFrontend();
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(this.dynamicTitles));
        }

        /// <summary>
        /// Navigate page on the frontend
        /// </summary>
        public void NavigatePageOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();

            string[] pages = new string[] { PageName };

            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ContentBlockContent);
            BATFeather.Wrappers().Frontend().News().NewsWrapper().VerifyNewsTitlesOnThePageFrontend(this.newsTitles);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(MvcNavClass, pages);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "TestPage";
        private const string WidgetName = "Press Articles MVC";
        private string[] dynamicTitles = { "Angel" };
        private const string ModuleName = "Module builder";
        private const string ContentBlockContent = "Test content";
        private string[] newsTitles = new string[] { NewsTitle };
        private const string MvcNavClass = "nav navbar-nav";
        private const string NewContentBlockWidget = "ContentBlock";
        private const string NewsTitle = "NewsTitleNew";  
    }
}
