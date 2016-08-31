using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create a page with Calendar widget.
    /// Change "Week starts on" day and verify that the correct start day is displayed.
    /// </summary>
    [TestClass]
    public class VerifyWeekStartDay_CalendarView_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyWeekStartDay_CalendarView_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyWeekStartDay_CalendarView()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            var actualWeekStartDay = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetWeekStartDayInCalendarView();
            Assert.AreEqual("Sunday", actualWeekStartDay);

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Calendar");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWeekStartsOnMonday();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            var actualWeekStartDay1 = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetWeekStartDayInCalendarView();
            Assert.AreEqual("Monday", actualWeekStartDay1);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.AreEqual("Monday", actualWeekStartDay1);

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Calendar");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().OpenListSettingsView();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWeekStartsOnSunday();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            Assert.AreEqual("Sunday", actualWeekStartDay);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.AreEqual("Sunday", actualWeekStartDay);
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

        private const string PageTitle = "EventsPage";
    }
}
