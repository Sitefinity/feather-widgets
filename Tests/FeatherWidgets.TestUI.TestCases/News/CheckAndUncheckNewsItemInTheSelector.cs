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
    /// <summary>
    /// This is test class for MvcSelectMoreThanOneNewsItem.
    /// </summary>
    [TestClass]
    public class CheckAndUncheckNewsItemInTheSelector_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CheckAndUncheckNewsItemInTheSelector.
        /// </summary>
        [TestMethod,
        Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void CheckAndUncheckNewsItemInTheSelector()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectWhichNewsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(20);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItemInMultipleSelector(selectedNewsNames);
            var countOfSelectedItems = selectedNewsNames.Count();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().CheckNotificationInSelectedTab(countOfSelectedItems);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SearchItemByTitle(SelectedNewsName15);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(1);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItem(SelectedNewsName15);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().CheckNotificationInSelectedTab(countOfSelectedItems + 1);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().OpenSelectedTab();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(5);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().VerifySelectedItemInMultipleSelectors(newSelectedNewsNames1);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SaveChanges();
            foreach (var newsTitle in newSelectedNewsNames1)
            {
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, newsTitle);
            }

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItemInMultipleSelector(UnSelectedNewsName5);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().CheckNotificationInSelectedTab(countOfSelectedItems);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().OpenAllTab();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SearchItemByTitle(SelectedNewsName15);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(1);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItem(SelectedNewsName15);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().OpenSelectedTab();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().CheckNotificationInSelectedTab(countOfSelectedItems - 1);                        
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().VerifySelectedItemInMultipleSelectors(newSelectedNewsNames2);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SaveChanges();
            foreach (var newsTitle in newSelectedNewsNames2)
            {
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, newsTitle);
            }
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNewsOnTheFrontend(this.newSelectedNewsNames2);
        }

        /// <summary>
        /// Verify news widget on the frontend
        /// </summary>
        public void VerifyNewsOnTheFrontend(string[] selectedNewsNames)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();
            BATFeather.Wrappers().Frontend().News().NewsWrapper().VerifyNewsTitlesOnThePageFrontend(selectedNewsNames);
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

        private const string ArrangementClassName = "SelectMoreThanOneNewsItem";
        private const string PageName = "News";
        private const string WhichNewsToDisplay = "Selected news";
        private const string WidgetName = "News";
        private const string SelectedNewsName15 = "News Item Title15";
        private readonly string[] UnSelectedNewsName5 = { "News Item Title5" };

        private readonly string[] selectedNewsNames = { "News Item Title1", "News Item Title5", "News Item Title6", "News Item Title12" };
        private readonly string[] newSelectedNewsNames1 = { "News Item Title1", "News Item Title5", "News Item Title6", "News Item Title12", "News Item Title15" };
        private readonly string[] newSelectedNewsNames2 = { "News Item Title1", "News Item Title6", "News Item Title12" };
    }
}
