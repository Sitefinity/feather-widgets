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
        /// Ensures that the past event has correct format for date 'November 2, 2015 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'November 2, 2015 at 8 PM'.")]
        public void PastEvents1DateFormatTest()
        {
            const string ExpectedResult = "November 2, 2015 at 8 PM";
            var start = new DateTime(2015, 11, 2, 20, 0, 0, DateTimeKind.Utc);
            this.TestDateFormat(ExpectedResult, start);
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
            var start = new DateTime(DateTime.UtcNow.Year, 1, 2, 20, 0, 0, DateTimeKind.Utc);
            this.TestDateFormat(ExpectedResult, start);
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
            const string ExpectedResultFormat = "Last week, {0} at 8 PM";
            var lastWeek = DateTime.UtcNow.Date.AddDays(-7);
            var expectedResult = string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, lastWeek.ToString(DateMonthDateFormat, CultureInfo.InvariantCulture));

            var start = lastWeek.AddHours(20);
            this.TestDateFormat(expectedResult, start);
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
            const string ExpectedResultFormat = "Yesterday, {0} at 8 PM";
            var yesterday = DateTime.UtcNow.Date.AddDays(-1);
            var expectedResult = string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, yesterday.ToString(DateMonthDateFormat, CultureInfo.InvariantCulture));

            var start = yesterday.AddHours(20);
            this.TestDateFormat(expectedResult, start);
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
            const string ExpectedResultFormat = "Today, {0} at 8 PM";
            var today = DateTime.UtcNow.Date;
            var expectedResult = string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, today.ToString(DateMonthDateFormat, CultureInfo.InvariantCulture));

            var start = today.AddHours(20);
            this.TestDateFormat(expectedResult, start);
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
            const string ExpectedResultFormat = "Tomorrow, {0} at 8 PM";
            var tomorrow = DateTime.UtcNow.Date.AddDays(1);
            var expectedResult = string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, tomorrow.ToString(DateMonthDateFormat, CultureInfo.InvariantCulture));

            var start = tomorrow.AddHours(20);
            this.TestDateFormat(expectedResult, start);
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
            const string ExpectedResultFormat = "{0}, {1} at 8 PM";
            var dateThisYear = DateTime.UtcNow.Date.AddDays(2);
            var expectedResult = string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, dateThisYear.DayOfWeek.ToString(), dateThisYear.ToString(DateMonthDateFormat, CultureInfo.InvariantCulture));

            var start = dateThisYear.AddHours(20);
            this.TestDateFormat(expectedResult, start);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Saturday, March 2, 2022 at 8 PM'.
        /// </summary>
        /// <remarks>
        /// This test will fail after 02.03.2222.
        /// </remarks>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Saturday, March 2, 2222 at 8 PM'.")]
        public void UpcomingEvents3DateFormatTest()
        {
            const string ExpectedResult = "Saturday, March 2, 2222 at 8 PM";
            var start = new DateTime(2222, 3, 2, 20, 0, 0, DateTimeKind.Utc);
            this.TestDateFormat(ExpectedResult, start);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Saturday, March 22, 8 AM-5 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Saturday, March 22, 8 AM-5 PM'.")]
        public void EventsOneDay1DateFormatTest()
        {
            const string ExpectedResultFormat = "{0}, {1}, 8 AM-5 PM";

            var dateThisYear = DateTime.UtcNow.Date.AddDays(2);
            var expectedResult = string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, dateThisYear.DayOfWeek.ToString(), dateThisYear.ToString(DateMonthDateFormat, CultureInfo.InvariantCulture));

            var start = dateThisYear.AddHours(8);
            var end = start.AddHours(9);

            this.TestDateFormat(expectedResult, start, end);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Yesterday, March 2 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'March 12-19 March 12 at 8 PM to March 19 at 5 AM'.")]
        public void PeriodEvents1DateFormatTest()
        {
            const string ExpectedResultFormat = "{0}-{1}";

            var dateNextMonth = new DateTime(DateTime.UtcNow.Year, 12, 1);
            var start = dateNextMonth.AddDays(23).AddHours(20);
            var end = dateNextMonth.AddDays(28).AddHours(5);

            var expectedResult = string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, start.ToString(DateMonthDateFormat, CultureInfo.InvariantCulture), end.Day);
            this.TestDateFormat(expectedResult, start, end);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'March 12-April 19 March 12 at 8 PM to April 19 at 5 AM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'March 12-April 19 March 12 at 8 PM to April 19 at 5 AM'.")]
        public void PeriodEvents2DateFormatTest()
        {
            const string ExpectedResultFormat = "{0}-{1}";

            var dateNextMonth = DateTime.UtcNow.Date.AddMonths(1);
            var start = dateNextMonth.AddDays(12).AddHours(20);
            var end = dateNextMonth.AddMonths(1).AddDays(19).AddHours(5);

            var expectedResult = string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, start.ToString(DateMonthDateFormat, CultureInfo.InvariantCulture), end.ToString(DateMonthDateFormat, CultureInfo.InvariantCulture));
            this.TestDateFormat(expectedResult, start, end);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'March 12-19, 2222 March 12, 2022 at 8 PM to April 19, 2222 at 5 AM'.
        /// </summary>
        /// <remarks>
        /// This test will fail after year of 2222.
        /// </remarks>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'March 12-19, 2222 March 12, 2022 at 8 PM to March 19, 2022 at 5 AM'.")]
        public void PeriodEvents3DateFormatTest()
        {
            const string ExpectedResultFormat = "{0}-{1}, 2222";

            var start = new DateTime(2222, 3, 12, 20, 0, 0, DateTimeKind.Utc);
            var end = new DateTime(2222, 3, 19, 5, 0, 0, DateTimeKind.Utc);

            var expectedResult = string.Format(CultureInfo.InvariantCulture, ExpectedResultFormat, start.ToString(DateMonthDateFormat, CultureInfo.InvariantCulture), end.Day);
            this.TestDateFormat(expectedResult, start, end);
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
            const string ExpectedResult = "Everyday";
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160422T080000\r\nDTEND;TZID=\"FLE Standard Time\":20160422T120000\r\nRRULE:FREQ=DAILY;INTERVAL=1";

            var start = DateTime.UtcNow.AddDays(-1);
            this.TestDateFormat(ExpectedResult, start, null, RecurrenceExpression);
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
            const string ExpectedResult = "Every 2 days";
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160407T080000\r\nDTEND;TZID=\"FLE Standard Time\":22160407T120000\r\nRRULE:FREQ=DAILY;INTERVAL=2";

            var start = DateTime.UtcNow.AddDays(-1);
            this.TestDateFormat(ExpectedResult, start, null, RecurrenceExpression);
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
            const string ExpectedResult = "Every weekday";
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160407T080000\r\nDTEND;TZID=\"FLE Standard Time\":22160407T120000\r\nRRULE:FREQ=DAILY;BYDAY=MO,TU,WE,TH,FR";

            var start = DateTime.UtcNow.AddDays(-1);
            this.TestDateFormat(ExpectedResult, start, null, RecurrenceExpression);
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
            const string ExpectedResult = "Every week on Monday";
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160407T080000\r\nDTEND;TZID=\"FLE Standard Time\":22160407T120000\r\nRRULE:FREQ=WEEKLY;INTERVAL=1;BYDAY=MO";

            var start = DateTime.UtcNow.AddDays(-1);
            this.TestDateFormat(ExpectedResult, start, null, RecurrenceExpression);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Yesterday, March 2 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'March 12-19 March 12 at 8 PM to March 19 at 5 AM'.")]
        public void PeriodEvents1FullDateFormatTest()
        {
            const string ExpectedResult = "March 12 at 8 PM to March 19 at 5 AM";

            var start = new DateTime(DateTime.Now.Year, 3, 12, 20, 0, 0);
            var end = new DateTime(DateTime.Now.Year, 3, 19, 5, 0, 0);

            this.TestFullDateFormat(ExpectedResult, start, end);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'March 12-April 19 March 12 at 8 PM to April 19 at 5 AM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'March 12-April 19 March 12 at 8 PM to April 19 at 5 AM'.")]
        public void PeriodEvents2FullDateFormatTest()
        {
            const string ExpectedResult = "March 12 at 8 PM to April 19 at 5 AM";

            var start = new DateTime(DateTime.Now.Year, 3, 12, 20, 0, 0);
            var end = new DateTime(DateTime.Now.Year, 4, 19, 5, 0, 0);

            this.TestFullDateFormat(ExpectedResult, start, end);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'March 12-19, 2022 March 12, 2222 at 8 PM to March 19, 2222 at 5 AM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'March 12-19, 2222 March 12, 2022 at 8 PM to March 19, 2222 at 5 AM'.")]
        public void PeriodEvents3FullDateFormatTest()
        {
            const string ExpectedResult = "March 12, 2222 at 8 PM to March 19, 2222 at 5 AM";

            var start = new DateTime(2222, 3, 12, 20, 0, 0);
            var end = new DateTime(2222, 3, 19, 5, 0, 0);

            this.TestFullDateFormat(ExpectedResult, start, end);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Everyday March 22 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Everyday March 22 at 8 PM'.")]
        public void RecurringEvents1NextOccurrenceFormatTest()
        {
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160422T080000\r\nDTEND;TZID=\"FLE Standard Time\":22160422T120000\r\nRRULE:FREQ=DAILY;INTERVAL=1";

            var now = DateTime.Now;
            var start = new DateTime(2016, 04, 22, 5, 30, 0);
            var expected = new DateTime(2016, 04, 22, 5, 30, 0, DateTimeKind.Utc).ToSitefinityUITime();
            while (expected < now)
            {
                expected = expected.AddDays(1);
            }

            this.TestFullDateFormat(expected.ToString(DateMonthHourDateFormat, CultureInfo.InvariantCulture), start, null, RecurrenceExpression);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Every 2 days March 22 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Every 2 days March 22 at 8 PM'.")]
        public void RecurringEvents2NextOccurrenceFormatTest()
        {
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160407T080000\r\nDTEND;TZID=\"FLE Standard Time\":22160407T120000\r\nRRULE:FREQ=DAILY;INTERVAL=2";

            var now = DateTime.Now;
            var start = new DateTime(2016, 04, 7, 5, 30, 0);
            var expected = new DateTime(2016, 04, 7, 5, 30, 0, DateTimeKind.Utc).ToSitefinityUITime();
            while (expected < now)
            {
                expected = expected.AddDays(2);
            }

            this.TestFullDateFormat(expected.ToString(DateMonthHourDateFormat, CultureInfo.InvariantCulture), start, null, RecurrenceExpression);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Every weekday March 22 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Every weekday March 22 at 8 PM'.")]
        public void RecurringEvents3NextOccurrenceFormatTest()
        {
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160407T080000\r\nDTEND;TZID=\"FLE Standard Time\":22160407T120000\r\nRRULE:FREQ=DAILY;BYDAY=MO,TU,WE,TH,FR";

            var now = DateTime.Now;
            var start = new DateTime(2016, 04, 7, 5, 30, 0);
            var expected = new DateTime(2016, 04, 7, 5, 30, 0, DateTimeKind.Utc).ToSitefinityUITime();
            while (expected < now || expected.DayOfWeek == DayOfWeek.Saturday || expected.DayOfWeek == DayOfWeek.Sunday)
            {
                expected = expected.AddDays(1);
            }

            this.TestFullDateFormat(expected.ToString(DateMonthHourDateFormat, CultureInfo.InvariantCulture), start, null, RecurrenceExpression);
        }

        /// <summary>
        /// Ensures that the past event has correct format for date 'Every week on Monday March 22 at 8 PM'.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the past event has correct format for date 'Every week on Monday March 22 at 8 PM'.")]
        public void RecurringEvents4NextOccurrenceFormatTest()
        {
            const string RecurrenceExpression = "DTSTART;TZID=\"FLE Standard Time\":20160407T080000\r\nDTEND;TZID=\"FLE Standard Time\":22160407T120000\r\nRRULE:FREQ=WEEKLY;INTERVAL=1;BYDAY=MO";

            var now = DateTime.Now;
            var start = new DateTime(2016, 04, 7, 5, 30, 0);
            var expected = new DateTime(2016, 04, 7, 5, 30, 0, DateTimeKind.Utc).ToSitefinityUITime();
            while (expected < now || expected.DayOfWeek != DayOfWeek.Monday)
            {
                expected = expected.AddDays(1);
            }

            this.TestFullDateFormat(expected.ToString(DateMonthHourDateFormat, CultureInfo.InvariantCulture), start, null, RecurrenceExpression);
        }

        private void TestDateFormat(string expectedResult, DateTime start, DateTime? end = null, string recurrenceExpression = null)
        {
            expectedResult = this.RemoveTrailingZeros(expectedResult);
            var viewModel = this.BuildItemViewModel(start, end, recurrenceExpression);
            var result = viewModel.EventDates();
            Assert.AreEqual(expectedResult, result);
        }

        private void TestFullDateFormat(string expectedResult, DateTime start, DateTime? end = null, string recurrenceExpression = null)
        {
            expectedResult = this.RemoveTrailingZeros(expectedResult);
            var viewModel = this.BuildItemViewModel(start, end, recurrenceExpression);
            var result = viewModel.EventDetailedDates();
            Assert.AreEqual(expectedResult, result);
        }

        private ItemViewModel BuildItemViewModel(DateTime start, DateTime? end, string recurrenceExpression = null)
        {
            return new ItemViewModel(new Event()
            {
                EventStart = start,
                EventEnd = end,
                RecurrenceExpression = recurrenceExpression,
                IsRecurrent = !string.IsNullOrEmpty(recurrenceExpression)
            });
        }

        private string RemoveTrailingZeros(string str)
        {
            return Regex.Replace(str, @"([ -])0(\d+)", "$1$2");
        }

        private const string DateMonthDateFormat = @"MMMM dd";
        private const string DateMonthHourDateFormat = @"MMMM dd a\t hh tt";
    }
}
