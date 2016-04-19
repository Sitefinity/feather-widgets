using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Classifications
{
    /// <summary>
    /// ShowEmptyCategoriesAndItemCount test class.
    /// </summary>
    [TestClass]
    public class ShowEmptyCategoriesAndItemCount_ : FeatherTestCase
    {
        /// <summary>
        /// UI test verifying empty categories and item count are correctly displayed in hybrid page
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Classifications)]
        public void ShowEmptyCategoriesAndItemCount()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().VerifyCheckedRadioButtonOption(CategoriesRadioButtonIds.AllCategories);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ApplyCssClasses(CssClass);
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().SwitchToSettingsTab();
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().CheckShowEmptyCategories();
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().VerifySelectedSortingOption(SortingOption);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, Category + 1);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, Category + 2);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, Category + 3);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().ClickCategoryTitle(Category + 3);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(new string[] { NewsTitle + 1, NewsTitle + 2, NewsTitle + 3 }));
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().CheckCategoriesItemCount(count, Category, 3);
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

        private const string PageName= "CategoriesPage";
        private const string WidgetName = "Categories";
        private const string Category = "Category";
        private const string NewsTitle = "NewsTitle";
        private string count = "(0)" ;
        private const string CssClass = "Test";
        private const string SortingOption = "By Title (A-Z)";
    }
}