using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    /// <summary>f
    /// This is test class for CheckSelectorsAfterSelectUnselectAndUNPublishingDynamicItem.
    /// </summary>
    [TestClass]
    public class CheckSelectorsAfterSelectUnselectAndUNPublishingDynamicItem_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CheckSelectorsAfterSelectUnselectAndUNPublishingDynamicItem.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void CheckSelectorsAfterSelectUnselectAndUNPublishingDynamicItem()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(WhichNewsToDisplay);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(20);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(this.selectedItemsNames);
            var countOfSelectedItems = this.selectedItemsNames.Count();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().CheckNotificationInSelectedTab(countOfSelectedItems);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(this.selectedItemsNames);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            foreach (var dynamicItem in this.selectedItemsNames)
            {
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, dynamicItem);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            this.UnpublishDynamicItem();

            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(this.selectedItemsNames);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().OpenAllTab();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(20);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(SelectedItemsName6);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().CheckNotificationInSelectedTab(countOfSelectedItems - 1);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            foreach (var dynamicTitle in this.finalItemsNames)
            {
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, dynamicTitle);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(this.finalItemsNames));
        }

        protected void UnpublishDynamicItem()
        {
            BAT.Arrange(this.TestName).ExecuteArrangement("UNPublishDynamicItem");
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

        private readonly string[] selectedItemsNames = { "Dynamic Item Title1", "Dynamic Item Title5", "Dynamic Item Title6", "Dynamic Item Title12" };
        private const string SelectedItemsName6 = "Dynamic Item Title6";
        private readonly string[] finalItemsNames = { "Dynamic Item Title1", "Dynamic Item Title12" };
    }
}