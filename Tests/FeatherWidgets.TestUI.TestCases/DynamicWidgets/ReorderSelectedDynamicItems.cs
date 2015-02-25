using System;
using System.Collections.Generic;
using System.Linq;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    /// <summary>
    /// This is test class for ReorderSelectedDynamicItems.
    /// </summary>
    [TestClass]
    public class ReorderSelectedDynamicItems_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ReorderSelectedDynamicItems.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void ReorderSelectedDynamicItems()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(this.selectedItemsNames);
            var countOfSelectedItems = this.selectedItemsNames.Count();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().CheckNotificationInSelectedTab(countOfSelectedItems);

            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().OpenSelectedTab();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(4);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().ReorderSelectedItems(this.expectedOrderOfNames, this.selectedItemsNames, this.reorderedIndexMapping);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(this.expectedOrderOfNames);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectSortingOption(SortingOption);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyCorrectOrderOfItemsOnBackend(this.expectedOrderOfNames);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(this.expectedOrderOfNames);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
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
        private const string WhichNewsToDisplay = "Selected PressArticles";
        private readonly string[] selectedItemsNames = { "Dynamic Item Title1", "Dynamic Item Title5", "Dynamic Item Title6", "Dynamic Item Title9" };
        private readonly string[] expectedOrderOfNames = { "Dynamic Item Title9", "Dynamic Item Title5", "Dynamic Item Title1", "Dynamic Item Title6" };
        private const string SortingOption = "AsSetManually";

        private readonly Dictionary<int, int> reorderedIndexMapping = new Dictionary<int, int>()
        {
            { 3, 0 },
            { 1, 3 }
        };
    }
}
