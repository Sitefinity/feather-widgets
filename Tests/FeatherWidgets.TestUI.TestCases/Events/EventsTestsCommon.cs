using System.Collections.Generic;
namespace FeatherWidgets.TestUI.TestCases.Events
{
    public class EventsTestsCommon
    {
        public const string BaseEventTitle = "TestEvent";
        public const string BasePastInFourDaysEventTitle = "BasePastInFourDaysEvent";
        public const string BasePastInTwoDaysEventTitle = "BasePastInTwoDaysEvent";
        public const string BaseUpcomingInOneDayEventTitle = "BaseUpcomingInOneDayEvent";
        public const string BaseUpcomingInThreeDaysEventTitle = "BaseUpcomingInThreeDaysEvent";
        public const string BaseDraftEventTitle = "DraftTestEvent";
        public const string BaseAllDayEventTitle = "AllDayTestEvent";
        public const string BaseRepeatEventTitle = "RepeatTestEvent";
        public static List<string> ExpectedEvents = new List<string>() { EventsTestsCommon.BaseEventTitle, EventsTestsCommon.BasePastInFourDaysEventTitle, EventsTestsCommon.BasePastInTwoDaysEventTitle, EventsTestsCommon.BaseUpcomingInOneDayEventTitle, EventsTestsCommon.BaseUpcomingInThreeDaysEventTitle, EventsTestsCommon.BaseAllDayEventTitle, EventsTestsCommon.BaseRepeatEventTitle, EventsTestsCommon.BaseDraftEventTitle };

        public const string BaseArrangementName = "FilterEventCommon";
    }
}
