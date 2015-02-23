using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    [TestClass]
    public class ActivateAndDeactivateModuleBuilder_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ActivateAndDeactivateModuleBuilder
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.ModuleBuilder)]
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

            string[] pages = new string[] { PageName };

            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ContentBlockContent);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitles));
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
        private readonly string[] dynamicTitles = { "Angel" };
        private const string ModuleName = "Module builder";
        private const string ContentBlockContent = "Test content";
        private readonly string[] newsTitles = new string[] { NewsTitle };
        private const string MvcNavClass = "nav navbar-nav";
        private const string NewsTitle = "NewsTitleNew";  
    }
}
