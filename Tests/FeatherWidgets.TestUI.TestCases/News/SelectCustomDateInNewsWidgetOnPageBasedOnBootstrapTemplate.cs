using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// SelectCustomDateInNewsWidgetOnPageBasedOnBootstrapTemplate test class.
    /// </summary>
    [TestClass]
    public class SelectCustomDateInNewsWidgetOnPageBasedOnBootstrapTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectCustomDateInNewsWidgetOnPageBasedOnBootstrapTemplate
        /// </summary>
        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.PagesAndContent)]
        public void SelectCustomDateInNewsWidgetOnPageBasedOnBootstrapTemplate()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectWhichNewsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectCheckBox(DateName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectDisplayItemsPublishedIn(DisplayItemsPublishedIn);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SetFromDateByTyping(DayAgo);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().AddHour("10", true);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().AddMinute("2", true);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SetToDateByDatePicker(DayForward);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().AddHour("13", false);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().AddMinute("4", false);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, newsTitles[0]);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, newsTitles[1]);
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
            BATFeather.Wrappers().Frontend().News().NewsWrapper().VerifyNewsTitlesOnThePageFrontend(newsTitles);
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

        private const string PageName = "NewsPage";
        private const string WidgetName = "News";
        private const string DisplayItemsPublishedIn = "Custom range...";
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string DateName = "dateInput";
        private const int DayAgo = -1;
        private const int DayForward = 1;
        private string[] newsTitles = { "Angel", "Cat" };
    }
}
