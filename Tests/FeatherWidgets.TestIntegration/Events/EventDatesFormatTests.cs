using System;
using System.Globalization;
using System.Text.RegularExpressions;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// This class contains integration tests for event date formats.
    /// </summary>
    [TestFixture]
    public class EventDatesFormatTests
    {
        /// <summary>
        /// Ensures that the event with start and end hour is displayed correctly - '2 March, 2014 at 8 AM–3 March, 2016 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the event with start and end hour is displayed correctly - '2 March, 2014 at 8 AM–3 March, 2016 at 8 PM'.")]
        public void EventWithStartEndHourDateFormatTest()
        {
            const string ExpectedResult = "2 March, 2014 at 8 AM-3 March, 2016 at 8 PM";
            var start = new DateTime(2014, 3, 2, 8, 0, 0);
            var end = new DateTime(2016, 3, 3, 20, 0, 0);
            this.TestDateFormat(ExpectedResult, start, end);
        }

        /// <summary>
        /// Ensures that the all day event is displayed correctly - '2 March, 2016–3 March, 2016'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the all day event is displayed correctly - '2 March, 2016–3 March, 2016'.")]
        public void AllDayEventDateFormatTest()
        {
            const string ExpectedResult = "2 March, 2016-3 March, 2016";
            var start = new DateTime(2016, 3, 2, 20, 0, 0);
            var end = new DateTime(2016, 3, 3, 20, 0, 0);
            this.TestDateFormat(ExpectedResult, start, end, null, true);
        }

        /// <summary>
        /// Ensures that the event in one day is displayed correctly - '2 March, 2016, 8 AM–8PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the event in one day is displayed correctly - '2 March, 2016, 8 AM–8PM'.")]
        public void OneDayEventDateFormatTest()
        {
            const string ExpectedResult = "2 March, 2016, 8 AM-8 PM";
            var start = new DateTime(2016, 3, 2, 8, 0, 0);
            var end = new DateTime(2016, 3, 2, 20, 0, 0);
            this.TestDateFormat(ExpectedResult, start, end);
        }

        /// <summary>
        /// Ensures that the recurring event has correct format for date 'Everyday, 2 March, 2014 at 8 AM–3 March, 2016 at 8 PM, London'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the recurring event has correct format for date 'Everyday, 2 March, 2014 at 8 AM–3 March, 2016 at 8 PM, London'.")]
        public void RecurringEvents1DateFormatTest()
        {
            const string ExpectedResultFormat = "Everyday, 2 March, 2014 at 8 AM-3 March, {0} at 8 PM"; // curr year
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160422T080000\r\nDTEND;TZID=\"FLE Standard Time\":20160422T120000\r\nRRULE:FREQ=DAILY;INTERVAL=1";

            var start = new DateTime(2014, 3, 2, 8, 0, 0);
            var end = new DateTime(DateTime.UtcNow.Year, 3, 3, 20, 0, 0);
            this.TestDateFormat(string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, DateTime.UtcNow.Year), start, end, RecurrenceExpression);
        }

        /// <summary>
        /// Ensures that the recurring event has correct format for date 'Everyday, 2 March, 2016–3 March, 2016'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the recurring event has correct format for date 'Everyday, 2 March, 2016–3 March, 2016'.")]
        public void RecurringEvents2DateFormatTest()
        {
            const string ExpectedResultFormat = "Everyday, 2 March, {0}-3 March, {0}"; // curr year
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160422T080000\r\nDTEND;TZID=\"FLE Standard Time\":20160422T120000\r\nRRULE:FREQ=DAILY;INTERVAL=1";

            var start = new DateTime(DateTime.UtcNow.Year, 3, 2, 8, 0, 0);
            var end = new DateTime(DateTime.UtcNow.Year, 3, 3, 20, 0, 0);
            this.TestDateFormat(string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, DateTime.UtcNow.Year), start, end, RecurrenceExpression, true);
        }

        /// <summary>
        /// Ensures that the recurring event has correct format for date 'Everyday, 2 March, 2016, 8 AM–8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the recurring event has correct format for date 'Everyday, 2 March, 2016, 8 AM–8 PM'.")]
        public void RecurringEvents3DateFormatTest()
        {
            const string ExpectedResultFormat = "Everyday, 2 March, {0}, 8 AM-8 PM"; // curr year
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160422T080000\r\nDTEND;TZID=\"FLE Standard Time\":20160422T120000\r\nRRULE:FREQ=DAILY;INTERVAL=1";

            var start = new DateTime(DateTime.UtcNow.Year, 3, 2, 8, 0, 0);
            var end = new DateTime(DateTime.UtcNow.Year, 3, 2, 20, 0, 0);
            this.TestDateFormat(string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, DateTime.UtcNow.Year), start, end, RecurrenceExpression);
        }
                
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "end"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "recurrenceExpression"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "start"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "expectedResult")]
        private void TestDateFormat(string expectedResult, DateTime start, DateTime? end = null, string recurrenceExpression = null, bool isOneDayEvent = false)
        {
            expectedResult = this.RemoveTrailingZeros(expectedResult);
            var viewModel = this.BuildItemViewModel(start, end, recurrenceExpression, isOneDayEvent);
            var result = viewModel.EventDates();
            Assert.AreEqual(expectedResult, result);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private ItemViewModel BuildItemViewModel(DateTime start, DateTime? end, string recurrenceExpression = null, bool isOneDayEvent = false)
        {
            return new ItemViewModel(new Event()
            {
                AllDayEvent = isOneDayEvent,
                EventStart = start,
                EventEnd = end,
                RecurrenceExpression = recurrenceExpression,
                IsRecurrent = !string.IsNullOrEmpty(recurrenceExpression)
            });
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private string RemoveTrailingZeros(string str)
        {
            return Regex.Replace(str, @"([ -])0(\d+)", "$1$2");
        }

        private const string DateMonthDateFormat = @"MMMM dd";
        private const string DateMonthHourDateFormat = @"MMMM dd a\t hh tt";
    }
}
