using System;
using System.Linq;
using MbUnit.Framework;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Events;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// This class contains integration tests for event date formats.
    /// </summary>
    [TestFixture]
    public class EventDatesFormatTests
    {
        /// <summary>
        /// Ensures that the past event has correct format for date 'November 2, 2015 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'November 2, 2015 at 8 PM'.")]
        public void PastEvents1DateFormatTest()
        {
            const string ExpectedResult = "November 2, 2015 at 8 PM";
            var start = new DateTime(2015, 11, 2, 20, 0, 0);
            var ev = new Event() { EventStart = start };
            var result = EventHelper.BuildEventDates(ev);
            Assert.AreEqual(ExpectedResult, result);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'January 2 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'January 2 at 8 PM'.")]
        public void PastEvents2DateFormatTest()
        {
            const string ExpectedResult = "January 2 at 8 PM";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Last week, February 2 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Last week, February 2 at 8 PM'.")]
        public void PastEvents3DateFormatTest()
        {
            const string ExpectedResult = "Last week, February 2 at 8 PM";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Yesterday, March 2 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Yesterday, March 2 at 8 PM'.")]
        public void PastEvents4DateFormatTest()
        {
            const string ExpectedResult = "Yesterday, March 2 at 8 PM";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Today, March 3 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Today, March 3 at 8 PM'.")]
        public void CurrentEvents1DateFormatTest()
        {
            const string ExpectedResult = "Today, March 3 at 8 PM";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Tomorrow, March 4 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Tomorrow, March 4 at 8 PM'.")]
        public void UpcomingEvents1DateFormatTest()
        {
            const string ExpectedResult = "Tomorrow, March 4 at 8 PM";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Saturday, March 12 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Saturday, March 12 at 8 PM'.")]
        public void UpcomingEvents2DateFormatTest()
        {
            const string ExpectedResult = "Saturday, March 12 at 8 PM";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Saturday, March 2, 2022 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Saturday, March 2, 2022 at 8 PM'.")]
        public void UpcomingEvents3DateFormatTest()
        {
            const string ExpectedResult = "Saturday, March 2, 2022 at 8 PM";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Saturday, March 22, 8 AM–5 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Saturday, March 22, 8 AM–5 PM'.")]
        public void EventsOneDay1DateFormatTest()
        {
            const string ExpectedResult = "Saturday, March 22, 8 AM–5 PM";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Yesterday, March 2 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'March 12–19 March 12 at 8 PM to March 19 at 5 AM'.")]
        public void PeriodEvents1DateFormatTest()
        {
            const string ExpectedRecurrency = "March 12–19";
            const string ExpectedDates = "March 12 at 8 PM to March 19 at 5 AM";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'March 12–April 19 March 12 at 8 PM to March 19 at 5 AM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'March 12–April 19 March 12 at 8 PM to March 19 at 5 AM'.")]
        public void PeriodEvents2DateFormatTest()
        {
            const string ExpectedRecurrency = "March 12–April 19";
            const string ExpectedDates = "March 12 at 8 PM to March 19 at 5 AM";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'March 12–19, 2022 March 12, 2022 at 8 PM to March 19, 2022 at 5 AM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'March 12–19, 2022 March 12, 2022 at 8 PM to March 19, 2022 at 5 AM'.")]
        public void PeriodEvents3DateFormatTest()
        {
            const string ExpectedRecurrency = "March 12–19, 2022";
            const string ExpectedDates = "March 12, 2022 at 8 PM to March 19, 2022 at 5 AM";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Everyday March 22 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Everyday March 22 at 8 PM'.")]
        public void RecurringEvents1DateFormatTest()
        {
            const string ExpectedRecurrency = "Everyday";
            const string ExpectedDates = "March 22 at 8 PM";
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160422T080000\r\nDTEND;TZID=\"FLE Standard Time\":20160422T120000\r\nRRULE:FREQ=DAILY;INTERVAL=1";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Every 2 days March 22 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Every 2 days March 22 at 8 PM'.")]
        public void RecurringEvents2DateFormatTest()
        {
            const string ExpectedRecurrency = "Every 2 days";
            const string ExpectedDates = "March 22 at 8 PM";
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160407T080000\r\nDTEND;TZID=\"FLE Standard Time\":20160407T120000\r\nRRULE:FREQ=DAILY;INTERVAL=2";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Every weekday March 22 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Every weekday March 22 at 8 PM'.")]
        public void RecurringEvents3DateFormatTest()
        {
            const string ExpectedRecurrency = "Every weekday";
            const string ExpectedDates = "March 22 at 8 PM";
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160407T080000\r\nDTEND;TZID=\"FLE Standard Time\":20160407T120000\r\nRRULE:FREQ=DAILY;BYDAY=MO,TU,WE,TH,FR";
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Every week on Monday March 22 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Every week on Monday March 22 at 8 PM'.")]
        public void RecurringEvents4DateFormatTest()
        {
            const string ExpectedRecurrency = "Every week on Monday";
            const string ExpectedDates = "March 22 at 8 PM";
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160407T080000\r\nDTEND;TZID=\"FLE Standard Time\":20160407T120000\r\nRRULE:FREQ=WEEKLY;INTERVAL=1;BYDAY=MO";
        }
    }
}
