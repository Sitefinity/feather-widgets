using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// SelectNewsItemInNewsWidgetFromPage_ test class.
    /// </summary>
    [TestClass]
    public class Load200TagsAndSearchInDynamicWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test Load200TagsAndSearchInDynamicWidget
        /// </summary>
        [TestMethod,
        Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void Load200TagsAndSearchInDynamicWidget()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectWhichNewsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectCheckBox(TaxonomyName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().WaitForItemsToAppear(200);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SearchItemByTitle(TaxonTitle);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().WaitForItemsToAppear(0);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().NoItemsFound();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SearchItemByTitle(TaxonTitle1);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().WaitForItemsToAppear(1);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectItemsInFlatSelector(TaxonTitle1);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().VerifySelectedItemsFromFlatSelector(new[] { TaxonTitle1 });
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, ItemsTitle1);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().VerifySelectedItemsFromFlatSelector(new[] { TaxonTitle1 });
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
        private const string TaxonTitle1 = "Tag125";
        private const string TaxonTitle = "Tag200";
        private const string ItemsTitle1 = "Title125";
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string TaxonomyName = "Tags";
    }
}