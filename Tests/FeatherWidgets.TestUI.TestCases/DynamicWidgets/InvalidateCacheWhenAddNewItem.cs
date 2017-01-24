using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.TestUI.Core.Utilities;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    /// <summary>
    /// InvalidateCacheWhenAddNewItem_ test class.
    /// </summary>
    [TestClass]
    public class InvalidateCacheWhenAddNewItem_ : FeatherTestCase
    {
        /// <summary>
        /// UI test InvalidateCacheWhenAddNewItem
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.DynamicWidgets),
        Telerik.TestUI.Core.Attributes.KnownIssue(BugId = 210944), Ignore]
        public void InvalidateCacheWhenAddNewItem()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", true, null, new HtmlFindExpression("class=~sfMain")));
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickAdvancedSettingsButton();
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickModelButton();
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().SetItemsPerPage(ItemsPerPage);
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().SetSortExpression(SortExpression);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().User().LogOut();
            this.NavigatePageOnTheFrontend(this.dynamicTitles, this.dynamicTitlesSecondPage);

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteArrangement("AddNewItem");

            BAT.Macros().User().LogOut();
            this.NavigatePageOnTheFrontend(this.dynamicTitles2, this.dynamicTitles2Second);
            BAT.Macros().User().EnsureAdminLoggedIn();
        }

        /// <summary>
        /// Navigate page on the frontend
        /// </summary>
        public void NavigatePageOnTheFrontend(string[] dynamicTitle, string[] secondPageTitles)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            ActiveBrowser.WaitUntilReady();
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(dynamicTitle));
            BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().NavigateToPage(Page);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(secondPageTitles));
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

        private const string PageName = "TestPage";
        private const string WidgetName = "Press Articles MVC";
        private const string ItemsPerPage = "3";
        private const string Page = "2";
        private const string SortExpression = "Title ASC";
        private string[] dynamicTitles = { "Boat", "Cat", "Dog" };
        private string[] dynamicTitlesSecondPage = { "Elephant" };
        private string[] dynamicTitles2 = { "Angel", "Boat", "Cat" };
        private string[] dynamicTitles2Second = { "Dog", "Elephant" };
    }
}
