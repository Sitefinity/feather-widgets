using System;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements.Events
{
    public static class EventsTestsCommon
    {
        public static void CreateEvents()
        {
            ServerOperations.Events().CreateEvent(EventsTestsCommon.BaseEventTitle);
            ServerOperations.Events().CreateEvent(EventsTestsCommon.BasePastInTwoDaysEventTitle, string.Empty, false, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-3));
            ServerOperations.Events().CreateEvent(EventsTestsCommon.BasePastInFourDaysEventTitle, string.Empty, false, DateTime.Now.AddDays(-6), DateTime.Now.AddDays(-4));
            ServerOperations.Events().CreateEvent(EventsTestsCommon.BaseUpcomingInOneDayEventTitle, string.Empty, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            ServerOperations.Events().CreateEvent(EventsTestsCommon.BaseUpcomingInThreeDaysEventTitle, string.Empty, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            ServerOperations.Events().CreateEvent(EventsTestsCommon.BaseAllDayEventTitle, string.Empty, true, DateTime.Now, DateTime.Now.AddHours(1));
            ServerOperations.Events().CreateDailyRecurrentEvent(EventsTestsCommon.BaseRepeatEventTitle, string.Empty, DateTime.Now, DateTime.Now.AddHours(1), 60, 5, 1, TimeZoneInfo.Local.StandardName);
            ServerOperations.Events().CreateDraftEvent(EventsTestsCommon.BaseDraftEventTitle, string.Empty, false, DateTime.Now, DateTime.Now.AddHours(1));
        }

        public static void DeleteEvents()
        {
            ServerOperations.Events().DeleteAllEvents();
            ServerOperations.Events().DeleteEvent(null, EventsTestsCommon.BaseEventTitle, EventsTestsCommon.BasePastInFourDaysEventTitle, EventsTestsCommon.BasePastInTwoDaysEventTitle, EventsTestsCommon.BaseUpcomingInOneDayEventTitle, EventsTestsCommon.BaseUpcomingInThreeDaysEventTitle, EventsTestsCommon.BaseAllDayEventTitle, EventsTestsCommon.BaseRepeatEventTitle, EventsTestsCommon.BaseDraftEventTitle);
        }

        public const string BaseEventTitle = "TestEvent";
        public const string BasePastInFourDaysEventTitle = "BasePastInFourDaysEvent";
        public const string BasePastInTwoDaysEventTitle = "BasePastInTwoDaysEvent";
        public const string BaseUpcomingInOneDayEventTitle = "BaseUpcomingInOneDayEvent";
        public const string BaseUpcomingInThreeDaysEventTitle = "BaseUpcomingInThreeDaysEvent";
        public const string BaseDraftEventTitle = "DraftTestEvent";
        public const string BaseAllDayEventTitle = "AllDayTestEvent";
        public const string BaseRepeatEventTitle = "RepeatTestEvent";
    }
}
