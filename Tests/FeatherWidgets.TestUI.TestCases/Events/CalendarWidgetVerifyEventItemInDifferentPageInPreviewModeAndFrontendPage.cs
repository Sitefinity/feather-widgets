using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create one primary page with Calendar widget and one with Events widget.
    /// Create one secondary page with Calendar widget that opens items from Calendar widget primary page.
    /// Create one secondary page with Calendar widget that opens items from Events widget primary page.
    /// Verify events details from both secondary pages ( event title, url and content).
    /// </summary>
    [TestClass]
    public class CalendarWidgetVerifyEventItemInDifferentPageInPreviewModeAndFrontendPage_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for CalendarWidgetVerifyEventItemInDifferentPageInPreviewModeAndFrontendPage_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void CalendarWidgetVerifyEventItemInDifferentPageInPreviewModeAndFrontendPage()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(Page2Title);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Calendar");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectExistingPage();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(Page1Title);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(this.selectedItemsFromPage2Selector);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + Page2Title.ToLower(), true, this.Culture);
            this.EventDetailsVerification(Page1Title, EventTitle);

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(Page4Title);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Calendar");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectExistingPage();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(Page3Title);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(this.selectedItemsFromPage4Selector);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + Page4Title.ToLower(), true, this.Culture);
            this.EventDetailsVerification(Page3Title, EventTitle);
        }
        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            var result = BAT.Arrange(this.TestName).ExecuteSetUp();
            currentEventId = result.Result.Values["currentEventId"];
            currentEventStartDate = result.Result.Values["currentEventStartTime"];
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        /// <summary>
        /// Verifies event title in event details view
        /// </summary>
        /// <param name="eventTitle">event Title</param>
        private void EventDetailsVerification(string pageTitle, string eventTitle)
        {
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().OpenEventsDetailsInScheduler(eventTitle);
            var expectedEventTitleInDetailsView = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetEventTitleInDetailsView();
            Assert.AreEqual(expectedEventTitleInDetailsView, eventTitle);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventConentInDetailsView(EventContent);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyDetailsEventsPageUrl(pageTitle, ActiveCalendar, currentEventStartDate, EventTitle);
        }

        private const string Page1Title = "PrimaryPage_CalendarWidget";
        private const string Page2Title = "SecondaryPage_CalendarWidget";
        private const string Page3Title = "PrimaryPage_EventsWidget";
        private const string Page4Title = "SecondaryPage_EventsWidget";
        private const string EventTitle = "EventTitle";
        private const string EventContent = "This is a test content";
        private const string ActiveCalendar = "default-calendar";
        private readonly string[] selectedItemsFromPage2Selector = new string[] { "PrimaryPage_CalendarWidget" };
        private readonly string[] selectedItemsFromPage4Selector = new string[] { "PrimaryPage_EventsWidget" };
        private string currentEventId = string.Empty;
        private string currentEventStartDate = string.Empty;
    }
}
