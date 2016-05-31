using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// Test class with tests related to BreadcrumbDetailBlogPostItemVirtualNodes.
    /// </summary>
    [TestClass]
    public class BreadcrumbDetailBlogPostItemVirtualNodes_ : FeatherTestCase
    {
        /// <summary>
        /// UI test BreadcrumbDetailBlogPostItemVirtualNodes.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Navigation)]
        public void BreadcrumbDetailBlogPostItemVirtualNodes()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", false));
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickAdvancedSettingsButton();
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickModelButton();
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SetAllowVirtualNodes("True");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifySelectedAnchorLink(PostTitle, this.expectedUrl);
            ActiveBrowser.WaitUntilReady();
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyBreadcrumbInFrontend(PageTitle, PostTitle);

            BAT.Macros().NavigateTo().Pages(this.Culture);
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

        private const string PageTitle = "TestPageWithBlogPostsWidget";
        private const string WidgetName = "Breadcrumb";
        private const string PostTitle = "post1";
        private readonly string expectedUrl = string.Format("/TestPageWithBlogPostsWidget/TestBlog/{0}/{1:00}/{2:00}/post1", DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
    }
}
