using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using MbUnit.Framework;
using ServiceStack.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Events.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Events.Mvc.Models;
using Telerik.Sitefinity.Frontend.Events.Mvc.Models.EventScheduler;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Mvc.TestUtilities.Helpers;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.CommonOperations.Multilingual;
using Telerik.Sitefinity.Web;
using Telerik.WebTestRunner.Server.Attributes;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// This class contains test related to basic functionality of Event Widget.
    /// </summary>
    [TestFixture]
    public class EventWidgetTests
    {
        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.pageOperations = new FeatherWidgets.TestUtilities.CommonOperations.PagesOperations();

            ServerOperations.Events().CreateEvent(EventWidgetTests.BaseEventTitle);
            ServerOperations.Events().CreateEvent(EventWidgetTests.BasePastEventTitle, string.Empty, false, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1));
            ServerOperations.Events().CreateEvent(EventWidgetTests.BaseUpcomingEventTitle, string.Empty, false, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
            ServerOperations.Events().CreateEvent(EventWidgetTests.BaseAllDayEventTitle, string.Empty, true, DateTime.Now, DateTime.Now.AddHours(1));
            ServerOperations.Events().CreateDailyRecurrentEvent(EventWidgetTests.BaseRepeatEventTitle, string.Empty, DateTime.Now, DateTime.Now.AddHours(1), 60, 5, 1, TimeZoneInfo.Local.StandardName);
            ServerOperations.Events().CreateDraftEvent(EventWidgetTests.BaseDraftEventTitle, string.Empty, false, DateTime.Now, DateTime.Now.AddHours(1));

            this.InitializeSitefinityLanguages();
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Events().DeleteAllEvents();

            foreach (var item in this.calendarList)
            {
                ServerOperations.Events().DeleteCalendar(item);
            }
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display selected events only.")]
        public void EventWidget_DisplaySelectedEventsOnly()
        {
            var selectedEventTitle = EventWidgetTests.BaseEventTitle;

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventController).FullName;
            var eventController = new EventController();
            eventController.Model.SelectionMode = SelectionMode.SelectedItems;

            var eventsManager = EventsManager.GetManager();
            var selectedEvent = eventsManager.GetEvents().FirstOrDefault(n => n.Title == selectedEventTitle && n.OriginalContentId != Guid.Empty);
            eventController.Model.SerializedSelectedItemsIds = "[\"" + selectedEvent.Id.ToString() + "\"]";

            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(eventController);

            string responseContent = this.pageOperations.AddWidgetToPageAndRequest(mvcProxy);

            Assert.IsTrue(responseContent.Contains(selectedEventTitle), "The event with this title was not found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BaseAllDayEventTitle), "The event with this title was found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BaseDraftEventTitle), "The event with this title was found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BaseRepeatEventTitle), "The event with this title was found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BaseUpcomingEventTitle), "The event with this title was found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BasePastEventTitle), "The event with this title was found!");
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and create events(past, current, upcoming, all day, repeat) with published and draft posts in order to verify that ")]
        public void EventWidget_DisplayAllPublishedEvents()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(EventController).FullName;
            var eventController = new EventController();
            eventController.Model.SelectionMode = SelectionMode.AllItems;
            mvcProxy.Settings = new Telerik.Sitefinity.Mvc.Proxy.ControllerSettings(eventController);

            string responseContent = this.pageOperations.AddWidgetToPageAndRequest(mvcProxy);

            Assert.IsTrue(responseContent.Contains(EventWidgetTests.BaseEventTitle), "The event with this title was not found!");
            Assert.IsTrue(responseContent.Contains(EventWidgetTests.BaseAllDayEventTitle), "Add day event with this title was not found!");
            Assert.IsFalse(responseContent.Contains(EventWidgetTests.BaseDraftEventTitle), "Draft event with this title was found!");
            Assert.IsTrue(responseContent.Contains(EventWidgetTests.BaseRepeatEventTitle), "Repeat event with this title was not found!");
            Assert.IsTrue(responseContent.Contains(EventWidgetTests.BasePastEventTitle), "Past event with this title was not found!");
            Assert.IsTrue(responseContent.Contains(EventWidgetTests.BaseUpcomingEventTitle), "Upcoming event with this title was not found!");
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that event calendar color is retrieved correctly via the helper.")]
        public void EventHelperCalendarColor()
        {
            const string ExpectedColor = "FF0000";
            var ev = new Event() { Parent = new Telerik.Sitefinity.Events.Model.Calendar() { Color = ExpectedColor } };
            Assert.AreEqual(ExpectedColor, new ItemViewModel(ev).EventCalendarColour());
        }

        [Test]
        ////[Ignore("Not Completed")]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events()
        {
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle1", string.Empty, DateTime.Now, DateTime.MaxValue, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle2", string.Empty, DateTime.Now, DateTime.MaxValue, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle3", string.Empty, DateTime.Now, DateTime.MaxValue, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle4", string.Empty, DateTime.Now, DateTime.MaxValue, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle5", string.Empty, DateTime.Now, DateTime.MaxValue, 60, 10000, 1, true);
            ServerOperations.Events().CreаteAllDayRecurrentEvent("RepeatEventTitle6", string.Empty, DateTime.Now, DateTime.MaxValue, 60, 10000, 1, true);

            var eventController = new EventSchedulerController();
            var model = eventController.Model;
            this.ApplySerializedFilters(model);
            var queryStringDictionary = this.CreateQueryStringDictionary(model);
            queryStringDictionary["StartDate"] = HttpUtility.UrlEncode(DateTime.MinValue.ToString("o", System.Globalization.CultureInfo.InvariantCulture));
            queryStringDictionary["EndDate"] = HttpUtility.UrlEncode(DateTime.MaxValue.AddDays(-1).ToString("o", System.Globalization.CultureInfo.InvariantCulture));
            var queryStringValue = string.Join("&", queryStringDictionary.Select(t => t.Key + "=" + t.Value).ToArray());

            string exceptionMessage = "[InvalidOperationException: Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property.]";
            var pageContent = WebRequestHelper.GetPageWebContent(UrlPath.ResolveAbsoluteUrl("~/web-interface/events/?") + queryStringValue);
            Assert.DoesNotContain(pageContent, exceptionMessage, exceptionMessage);
            List<EventOccurrenceViewModel> eventsList = JsonSerializer.DeserializeFromString<List<EventOccurrenceViewModel>>(pageContent);
            Assert.AreEqual(eventsList.Count, EventsManager.GetManager().GetEventsOccurrences(DateTime.MinValue, DateTime.MaxValue).Count());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Ignore("Not Completed")]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after events request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Events_Multilingual()
        {
            var manager = EventsManager.GetManager();
            var multiOperations = new MultilingualEventOperations();
            var bulgarian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "bg-BG").FirstOrDefault();

            var calendartDefault = manager.GetCalendars().FirstOrDefault();
            this.CreateLocalizedRecurrentEvent(multiOperations, "Event 1 bg", Guid.NewGuid(), bulgarian, calendartDefault);
            this.CreateLocalizedRecurrentEvent(multiOperations, "Event 2 bg", Guid.NewGuid(), bulgarian, calendartDefault);
            this.CreateLocalizedRecurrentEvent(multiOperations, "Event 3 bg", Guid.NewGuid(), bulgarian, calendartDefault);
            this.CreateLocalizedRecurrentEvent(multiOperations, "Event 4 bg", Guid.NewGuid(), bulgarian, calendartDefault);
            this.CreateLocalizedRecurrentEvent(multiOperations, "Event 5 bg", Guid.NewGuid(), bulgarian, calendartDefault);
            this.CreateLocalizedRecurrentEvent(multiOperations, "Event 6 bg", Guid.NewGuid(), bulgarian, calendartDefault);
            this.CreateLocalizedRecurrentEvent(multiOperations, "Event 7 bg", Guid.NewGuid(), bulgarian, calendartDefault);
            this.CreateLocalizedRecurrentEvent(multiOperations, "Event 8 bg", Guid.NewGuid(), bulgarian, calendartDefault);
            this.CreateLocalizedRecurrentEvent(multiOperations, "Event 9 bg", Guid.NewGuid(), bulgarian, calendartDefault);
            this.CreateLocalizedRecurrentEvent(multiOperations, "Event 10 bg", Guid.NewGuid(), bulgarian, calendartDefault);

            var eventController = new EventSchedulerController();
            var model = eventController.Model;
            this.ApplySerializedFilters(model);
            var queryStringDictionary = this.CreateQueryStringDictionary(model);
            queryStringDictionary["StartDate"] = HttpUtility.UrlEncode(DateTime.MinValue.ToString("o", System.Globalization.CultureInfo.InvariantCulture));
            queryStringDictionary["EndDate"] = HttpUtility.UrlEncode(DateTime.MaxValue.AddDays(-1).ToString("o", System.Globalization.CultureInfo.InvariantCulture));
            var queryStringValue = string.Join("&", queryStringDictionary.Select(t => t.Key + "=" + t.Value).ToArray());

            string exceptionMessage = "[InvalidOperationException: Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property.]";
            var pageContent = WebRequestHelper.GetPageWebContent(UrlPath.ResolveAbsoluteUrl("~/web-interface/events/?") + queryStringValue);
            Assert.DoesNotContain(pageContent, exceptionMessage, exceptionMessage);
            List<EventOccurrenceViewModel> eventsList = JsonSerializer.DeserializeFromString<List<EventOccurrenceViewModel>>(pageContent);
            Assert.AreEqual(eventsList.Count, EventsManager.GetManager().GetEventsOccurrences(DateTime.MinValue, DateTime.MaxValue).Where(p => p.Event.LanguageData.Where(t => t.Language == bulgarian.Name).Count() > 0).Count());
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after calendar request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Calendar()
        {
            var manager = EventsManager.GetManager();
            var calendarCount = 0;
            var calendarTitle1 = "New custom calendar 1";
            var calendarTitle2 = "New custom calendar 2";

            var calendartDefault = manager.GetCalendars().FirstOrDefault();
            if (this.SetCalendarToEvent(manager, BaseUpcomingEventTitle, calendartDefault.Id))
            {
                calendarCount++;
            }

            var calendar1Id = ServerOperations.Events().CreateCalendar(Guid.NewGuid(), calendarTitle1);
            if (this.SetCalendarToEvent(manager, BaseRepeatEventTitle, calendar1Id))
            {
                this.calendarList.Add(calendar1Id);
                calendarCount++;
            }

            var calendar2Id = ServerOperations.Events().CreateCalendar(Guid.NewGuid(), calendarTitle2);
            if (this.SetCalendarToEvent(manager, BasePastEventTitle, calendar2Id))
            {
                this.calendarList.Add(calendar2Id);
                calendarCount++;
            }

            var eventController = new EventSchedulerController();
            var model = eventController.Model;
            this.ApplySerializedFilters(model);

            var queryStringDictionary = this.CreateQueryStringDictionary(model);
            queryStringDictionary["StartDate"] = HttpUtility.UrlEncode(DateTime.MinValue.ToString("o", System.Globalization.CultureInfo.InvariantCulture));
            queryStringDictionary["EndDate"] = HttpUtility.UrlEncode(DateTime.MaxValue.AddDays(-1).ToString("o", System.Globalization.CultureInfo.InvariantCulture));
            var queryStringValue = string.Join("&", queryStringDictionary.Select(t => t.Key + "=" + t.Value).ToArray());

            var pageContent = WebRequestHelper.GetPageWebContent(UrlPath.ResolveAbsoluteUrl("~/web-interface/calendars/?") + queryStringValue);
            List<EventCalendarViewModel> jsonCalendarsList = JsonSerializer.DeserializeFromString<List<EventCalendarViewModel>>(pageContent);
            Assert.AreEqual(jsonCalendarsList.Count, calendarCount);
        }

        [Test]
        [Multilingual]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensure that after calendar request, data is correctly retrieved as json content")]
        public void EventSchedulerWidget_Route_Calendar_Multilingual()
        {
            var multiOperations = new MultilingualEventOperations();
            var calendarCount = 0;
            var calendarTitle1 = "New custom calendar 1";
            var calendarTitle2 = "New custom calendar 2";
            var eventTitle1 = "Event 1 bg";
            var eventTitle2 = "Event 2 bg";
            var event1Id = Guid.NewGuid();
            var event2Id = Guid.NewGuid();

            var bulgarian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "bg-BG").FirstOrDefault();

            var calendar1Id = ServerOperations.Events().CreateCalendar(Guid.NewGuid(), calendarTitle1, bulgarian.Name);
            multiOperations.CreateLocalizedEvent(event1Id, calendar1Id, eventTitle1, string.Empty, string.Empty, DateTime.Now, DateTime.Now.AddDays(1), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, false, bulgarian.Name);
            if (calendar1Id != Guid.Empty)
            {
                this.calendarList.Add(calendar1Id);
                calendarCount++;
            }

            var calendar2Id = ServerOperations.Events().CreateCalendar(Guid.NewGuid(), calendarTitle2, bulgarian.Name);
            multiOperations.CreateLocalizedEvent(event2Id, calendar2Id, eventTitle2, string.Empty, string.Empty, DateTime.Now, DateTime.Now.AddDays(1), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, false, bulgarian.Name);
            if (calendar2Id != Guid.Empty)
            {
                this.calendarList.Add(calendar2Id);
                calendarCount++;
            }

            var eventController = new EventSchedulerController();
            var model = eventController.Model;
            this.ApplySerializedFilters(model);
            var queryStringDictionary = this.CreateQueryStringDictionary(model);
            queryStringDictionary["StartDate"] = HttpUtility.UrlEncode(DateTime.MinValue.ToString("o", System.Globalization.CultureInfo.InvariantCulture));
            queryStringDictionary["EndDate"] = HttpUtility.UrlEncode(DateTime.MaxValue.AddDays(-1).ToString("o", System.Globalization.CultureInfo.InvariantCulture));
            queryStringDictionary["UiCulture"] = bulgarian.Name;
            var queryStringValue = string.Join("&", queryStringDictionary.Select(t => t.Key + "=" + t.Value).ToArray());

            var pageContent = WebRequestHelper.GetPageWebContent(UrlPath.ResolveAbsoluteUrl("~/web-interface/calendars/?") + queryStringValue);
            List<EventCalendarViewModel> jsonCalendarsList = JsonSerializer.DeserializeFromString<List<EventCalendarViewModel>>(pageContent);
            Assert.AreEqual(jsonCalendarsList.Count, calendarCount);
        }

        private void ApplySerializedFilters(IEventSchedulerModel model)
        {
            model.SerializedNarrowSelectionFilters = "{\"__msdisposeindex\":257,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
            model.SerializedAdditionalFilters = "{\"__msdisposeindex\":256,\"Title\":null,\"Id\":\"00000000-0000-0000-0000-000000000000\",\"QueryItems\":[],\"TypeProperties\":[],\"_itemPathSeparator\":\"_\"}";
        }

        private void CreateLocalizedRecurrentEvent(MultilingualEventOperations multiOperations, string eventTitle, Guid eventId, CultureInfo bulgarian, Telerik.Sitefinity.Events.Model.Calendar calendartDefault)
        {
            multiOperations.CreateLocalizedEvent(eventId, calendartDefault.Id, eventTitle, string.Empty, string.Empty, DateTime.Now, DateTime.MaxValue, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, false, bulgarian.Name);
            multiOperations.MakeEventRecurrent(eventId, DateTime.Now, 10000);
        }

        private bool SetCalendarToEvent(EventsManager manager, string eventTitle, Guid calendarId)
        {
            var selectedEvent = manager.GetEvents().Where(i => i.Title == eventTitle).FirstOrDefault();
            var selectedCalendar = manager.GetCalendar(calendarId);
            if (selectedEvent != null && selectedCalendar != null)
            {
                selectedEvent.Parent = selectedCalendar;
                manager.Lifecycle.Publish(selectedEvent);
                manager.SaveChanges();
                return true;
            }

            return false;
        }

        private Dictionary<string, string> CreateQueryStringDictionary(IEventSchedulerModel model)
        {
            var queryStringList = model.GetType().GetProperties()
                .Where(p => p.GetValue(model, null) != null)
                .Select(property => new { Key = property.Name, Value = HttpUtility.UrlEncode(property.GetValue(model, null).ToString()) })
                .ToDictionary(t => t.Key, t => t.Value);

            return queryStringList;
        }

        private void InitializeSitefinityLanguages()
        {
            var english = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "en-US").FirstOrDefault();
            var turkish = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "tr-TR").FirstOrDefault();
            var arabic = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "ar-MA").FirstOrDefault();
            var serbian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "sr-Cyrl-BA").FirstOrDefault();
            var bulgarian = AppSettings.CurrentSettings.DefinedFrontendLanguages.Where(x => x.Name == "bg-BG").FirstOrDefault();

            this.sitefinityLanguages.Add("English", english);
            this.sitefinityLanguages.Add("Turkish", turkish);
            this.sitefinityLanguages.Add("Arabic", arabic);
            this.sitefinityLanguages.Add("Serbian", serbian);
            this.sitefinityLanguages.Add("Bulgarian", bulgarian);
        }

        private readonly Dictionary<string, CultureInfo> sitefinityLanguages = new Dictionary<string, CultureInfo>();
        private List<Guid> calendarList = new List<Guid>();
        private FeatherWidgets.TestUtilities.CommonOperations.PagesOperations pageOperations;
        private const string BaseEventTitle = "TestEvent";
        private const string BasePastEventTitle = "PastTestEvent";
        private const string BaseUpcomingEventTitle = "UpcomingTestEvent";
        private const string BaseDraftEventTitle = "DraftTestEvent";
        private const string BaseAllDayEventTitle = "AllDayTestEvent";
        private const string BaseRepeatEventTitle = "RepeatTestEvent";
    }
}