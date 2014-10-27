using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void NavigationWidgetVerifyPageWithUserNotAllowedAndRoleChanged()
        {
            BAT.Macros().User().EnsureLoggedIn(User, Password);

            string[] pages = new string[] { TestPage };

            BAT.Macros().NavigateTo().CustomPage("~/" + HomePage.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyPagesNotPresentFrontEndNavigation(MvcNavClass, pages);

            BAT.Arrange(this.TestName).ExecuteArrangement("ChangeUserRole");

            string[] newPages = new string[] { HomePage, TestPage };

            BAT.Macros().User().EnsureLoggedIn(User, Password);
            BAT.Macros().NavigateTo().CustomPage("~/" + HomePage.ToLower(), false);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(MvcNavClass, newPages);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Arrange(this.TestName).ExecuteSetUp();         
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string HomePage = "HomePage";
        private const string TestPage = "TestPage";
        private const string MvcNavClass = "nav navbar-nav";
        private const string User = "editor";
        private const string Password = "password";
    }
}
