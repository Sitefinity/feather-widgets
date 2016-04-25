using System;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements.Events
{
    public static class EventsTestsCommon
    {
        public static void CreateEvents()
        {
            ServerOperations.Events().CreateEvent(EventsTestsCommon.BaseEventTitle);
            ServerOperations.Events().CreateEvent(EventsTestsCommon.BasePastEventTitle, string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent(EventsTestsCommon.BaseUpcomingEventTitle, string.Empty, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            ServerOperations.Events().CreateEvent(EventsTestsCommon.BaseAllDayEventTitle, string.Empty, true, DateTime.Now, DateTime.Now.AddHours(1));
            ServerOperations.Events().CreateDailyRecurrentEvent(EventsTestsCommon.BaseRepeatEventTitle, string.Empty, DateTime.Now, DateTime.Now.AddHours(1), 60, 5, 1, TimeZoneInfo.Local.StandardName);
            ServerOperations.Events().CreateDraftEvent(EventsTestsCommon.BaseDraftEventTitle, string.Empty, false, DateTime.Now, DateTime.Now.AddHours(1));
        }

        public static void DeleteEvents()
        {
            ServerOperations.Events().DeleteAllEvents();
        }

        public const string BaseEventTitle = "TestEvent";
        public const string BasePastEventTitle = "PastTestEvent";
        public const string BaseUpcomingEventTitle = "UpcomingTestEvent";
        public const string BaseDraftEventTitle = "DraftTestEvent";
        public const string BaseAllDayEventTitle = "AllDayTestEvent";
        public const string BaseRepeatEventTitle = "RepeatTestEvent";
    }
}
