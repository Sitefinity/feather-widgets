using System;
using System.Collections.Generic;
using System.Linq;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// This is test class for ReorderSelectedNewsItems.
    /// </summary>
    [TestClass]
    public class ReorderSelectedNewsItems_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ReorderSelectedNewsItems.
        /// </summary>
        [TestMethod,
        Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.NewsSelectors),
        Ignore]
        public void ReorderSelectedNewsItems()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectWhichItemsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectItemsInFlatSelector(selectedNewsNames);
            var countOfSelectedItems = selectedNewsNames.Count();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().CheckNotificationInSelectedTab(countOfSelectedItems);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().OpenSelectedTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().ReorderSelectedItems(expectedOrderOfNames, selectedNewsNames, reorderedIndexMapping);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().VerifySelectedItemsFromFlatSelector(this.expectedOrderOfNames);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectSortingOption(SortingOption);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SaveChanges();

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyCorrectOrderOfItemsOnBackend(this.expectedOrderOfNames);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().VerifySelectedItemsFromFlatSelector(this.expectedOrderOfNames);
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

        private const string PageName = "News";
        private const string WidgetName = "News";
        private const string WhichNewsToDisplay = "Selected news";
        private readonly string[] selectedNewsNames = { "News Item Title1", "News Item Title5", "News Item Title6", "News Item Title9" };
        private readonly string[] expectedOrderOfNames = { "News Item Title9", "News Item Title5", "News Item Title1", "News Item Title6" };
        private const string SortingOption = "AsSetManually";

        private readonly Dictionary<int, int> reorderedIndexMapping = new Dictionary<int, int>()
        {
            {3, 0},
            {1, 3}
        };
    }
}
