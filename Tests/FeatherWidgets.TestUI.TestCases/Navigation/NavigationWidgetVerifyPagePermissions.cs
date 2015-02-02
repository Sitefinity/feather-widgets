using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// This is a test class with tests related to page permissions and navigation widget.
    /// </summary>
    [TestClass]
    public class NavigationWidgetVerifyPagePermissions : FeatherTestCase
    {
        /// <summary>
        /// UI test NavigationWidgetVerifyPageWithUserNotAllowedAndRoleChanged
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void NavigationWidgetVerifyPageWithUserNotAllowedAndRoleChanged()
        {
            string pageTemplateName = "Bootstrap.default";
            string mvcNavClass = "nav navbar-nav";

            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().User().EnsureLoggedIn(User, Password);

            BAT.Macros().NavigateTo().CustomPage("~/" + HomePage.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(mvcNavClass, this.TestPages);

            BAT.Arrange(this.ArrangementClass).ExecuteArrangement("ChangeUserRole");

            BAT.Macros().User().EnsureLoggedIn(User, Password);
            BAT.Macros().NavigateTo().CustomPage("~/" + HomePage.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(mvcNavClass, this.NewPages);
        }

        /// <summary>
        /// UI test NavigationWidgetFoundationVerifyPageWithUserNotAllowedAndRoleChanged
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.Foundation)]
        public void NavigationWidgetFoundationVerifyPageWithUserNotAllowedAndRoleChanged()
        {
            string pageTemplateName = "Foundation.default";
            string mvcNavClass = "top-bar-section";

            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().User().EnsureLoggedIn(User, Password);

            BAT.Macros().NavigateTo().CustomPage("~/" + HomePage.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(mvcNavClass, this.TestPages, TemplateType.Foundation);

            BAT.Arrange(this.ArrangementClass).ExecuteArrangement("ChangeUserRole");

            BAT.Macros().User().EnsureLoggedIn(User, Password);
            BAT.Macros().NavigateTo().CustomPage("~/" + HomePage.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(mvcNavClass, this.NewPages, TemplateType.Foundation);
        }

        /// <summary>
        /// UI test NavigationWidgetSemanticUIVerifyPageWithUserNotAllowedAndRoleChanged
        /// </summary>
        [TestMethod,
        Owner("Feather team"),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void NavigationWidgetSemanticUIVerifyPageWithUserNotAllowedAndRoleChanged()
        {
            string pageTemplateName = "SemanticUI.default";
            string mvcNavClass = "ui menu inverted";

            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", pageTemplateName).ExecuteSetUp();

            BAT.Macros().User().EnsureLoggedIn(User, Password);

            BAT.Macros().NavigateTo().CustomPage("~/" + HomePage.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(mvcNavClass, this.TestPages, TemplateType.Semantic);

            BAT.Arrange(this.ArrangementClass).ExecuteArrangement("ChangeUserRole");

            BAT.Macros().User().EnsureLoggedIn(User, Password);
            BAT.Macros().NavigateTo().CustomPage("~/" + HomePage.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(mvcNavClass, this.NewPages, TemplateType.Semantic);
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

        private string[] TestPages
        {
            get
            {
                return new string[] { TestPage };
            }
        }

        private string[] NewPages
        {
            get
            {
                return new string[] { HomePage, TestPage };
            }
        }

        private const string HomePage = "HomePage";
        private const string TestPage = "TestPage";
        private const string User = "editor";
        private const string Password = "password";
        private const string ArrangementClassName = "NavigationWidgetVerifyPageWithUserNotAllowedAndRoleChanged";
    }
}
