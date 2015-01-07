using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI
{
    /// <summary>f
    /// This is test class for CheckSelectorsAfterUNPublishingDynamicItem.
    /// </summary>
    [TestClass]
    public class CheckSelectorsAfterUNPublishingDynamicItem_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CheckSelectorsAfterUNPublishingDynamicItem.
        /// </summary>
        [TestMethod,
        Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void CheckSelectorsAfterUNPublishingDynamicItem()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectWhichItemsToDisplay(WhichNewsToDisplay);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().WaitForItemsToAppear(20);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SelectItemsInFlatSelector(selectedItemsNames);
            var countOfSelectedItems = selectedItemsNames.Count();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().CheckNotificationInSelectedTab(countOfSelectedItems);           
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().VerifySelectedItemsFromFlatSelector(selectedItemsNames);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SaveChanges();
            foreach (var dynamicItem in newSelectedItemsNames)
            {
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, dynamicItem);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            this.UnpublishDynamicItem();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().VerifySelectedItemsFromFlatSelector(selectedItemsNames);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().WaitForItemsToAppear(countOfSelectedItems);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().OpenAllTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().WaitForItemsToAppear(20);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SaveChanges();
            foreach (var dynamicTitle in newSelectedItemsNames)
            {
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, dynamicTitle);
            }
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNewsOnTheFrontend();
        }

        /// <summary>
        /// Verify news widget on the frontend
        /// </summary>
        public void VerifyNewsOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();
            BATFeather.Wrappers().Frontend().News().NewsWrapper().VerifyNewsTitlesOnThePageFrontend(this.newSelectedItemsNames);
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
        private readonly string[] newSelectedItemsNames = { "Dynamic Item Title1", "Dynamic Item Title6", "Dynamic Item Title12" };
    }
}