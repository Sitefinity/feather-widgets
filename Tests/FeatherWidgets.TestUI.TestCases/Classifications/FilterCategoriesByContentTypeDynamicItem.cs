using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Classifications
{
    /// <summary>
    /// FilterCategoriesByContentType test class.
    /// </summary>
    [TestClass]
    public class FilterCategoriesByContentTypeDynamicItem_ : FeatherTestCase
    {
        /// <summary>
        /// UI test verifying the filtering of categories by content type in hybrid page
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Classifications)]
        public void FilterCategoriesByContentTypeDynamicItem()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().SelectRadioButtonOption(CategoriesRadioButtonIds.ContentCategories);
            BATFeather.Wrappers().Backend().Classifications().CategoriesWrapper().SelectUsedByContentTypeOption(DynamicWidgetOption);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, taxonTitleDynamic + 1);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, taxonTitleDynamic + 2);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, taxonTitleDynamic + 3);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsCategoriesTitlesPresentOnTheFrontendPage(new string[] { taxonTitleDynamic + 3, taxonTitleDynamic + 2, taxonTitleDynamic + 1 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsCategoriesTitlesPresentOnTheFrontendPage(new string[] { taxonTitleNews + 1, taxonTitleNews + 2, taxonTitleNews + 3 }));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(new string[] { NewsTitle + 1, NewsTitle + 2, NewsTitle + 3 }));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(new string[] { dynamicModuleTitle + 0, dynamicModuleTitle + 1, dynamicModuleTitle + 2 }));
            
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().ClickCategoryTitle(taxonTitleDynamic + 1);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(new string[] { dynamicModuleTitle + 0 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(new string[] { dynamicModuleTitle + 1, dynamicModuleTitle + 2 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(new string[] { NewsTitle + 1, NewsTitle + 2, NewsTitle + 3 }));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsCategoriesTitlesPresentOnTheFrontendPage(new string[] { taxonTitleDynamic + 3, taxonTitleDynamic + 2 }));
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
        private string taxonTitleNews = "CategoryNews";
        private string taxonTitleDynamic = "CategoryDynamic";
        private string dynamicModuleTitle = "Title";
        private const string NewsTitle = "NewsTitle";
        private const string CssClass = "Test";
        private const string DynamicWidgetOption = "Press Article";
    }
}