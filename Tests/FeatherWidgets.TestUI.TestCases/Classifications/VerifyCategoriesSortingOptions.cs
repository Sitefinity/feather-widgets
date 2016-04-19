using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Classifications
{
    /// <summary>
    /// VerifyCategoriesSortingOptions test class.
    /// </summary>
    [TestClass]
    public class VerifyCategoriesSortingOptions_ : FeatherTestCase
    {
        /// <summary>
        /// UI test verifying sorting of categories is correct in hybrid page
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Classifications)]
        public void VerifyCategoriesSortingOptions()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ApplyCssClasses(CssClass);
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().SwitchToSettingsTab();
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().VerifySelectedSortingOption(SortingOptionAZ);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().CheckCategoriesSorting(Category, SortingOptionAZ);

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().SwitchToSettingsTab();
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().SelectSortingOption(SortingOptionZA);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().CheckCategoriesSorting(Category, SortingOptionZA);
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

        private const string PageName = "CategoriesPage";
        private const string WidgetName = "Categories";
        private string Category = "Category";
        private const string CssClass = "Test";
        private const string SortingOptionAZ = "By Title (A-Z)";
        private const string SortingOptionZA = "By Title (Z-A)";
    }
}