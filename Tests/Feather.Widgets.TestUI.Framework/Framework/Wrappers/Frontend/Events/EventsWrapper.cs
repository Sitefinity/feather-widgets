using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Events
{
    /// <summary>
    /// This is the entry point class for events widget on the frontend.
    /// </summary>
    public class EventsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies the events titles on the page frontend.
        /// </summary>
        /// <param name="eventTitles">The event titles.</param>
        /// <returns>true or false depending on event titles presence on frontend</returns>
        public bool AreEventTitlesPresentOnThePageFrontend(IEnumerable<string> eventTitles)
        {
            if (eventTitles == null && eventTitles.Count() == 0)
            {
                throw new ArgumentNullException("eventTitles cannot be empty parameter.");
            }

            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            foreach (var title in eventTitles)
            {
                var eventAnchor = frontendPageMainDiv.Find.ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + title);
                if ((eventAnchor == null) || (eventAnchor != null && !eventAnchor.IsVisible()))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Verifies the events titles are missing on the page frontend.
        /// </summary>
        /// <param name="eventTitles">The event titles.</param>
        /// <returns>true or false depending on event titles missing on frontend</returns>
        public bool AreEventTitlesMissingOnThePageFrontend(IEnumerable<string> eventTitles)
        {
            if (eventTitles == null && eventTitles.Count() == 0)
            {
                throw new ArgumentNullException("eventTitles cannot be empty parameter.");
            }

            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            foreach (var title in eventTitles)
            {
                var eventAnchor = frontendPageMainDiv.Find.ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + title);
                if (eventAnchor != null && eventAnchor.IsVisible())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the inner text of the selected view 
        /// </summary>
        /// <param name="innerText"></param>
        /// <returns>Selected view string</returns>
        public string GetSelectedSchedulerView()
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var stateSelected = frontendPageMainDiv.Find.ByExpression<HtmlListItem>("class=~k-state-selected");
            var selectedView = stateSelected.ChildNodes.First().InnerText;
            return selectedView;
        }

        /// <summary>
        /// Gets inner text for event title
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <returns>Event title</returns>
        public string GetEventTitleInScheduler(string eventId)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var eventDetails = frontendPageMainDiv.Find.ByExpression<HtmlDiv>("class=sf-event-item", "data-sf-eventid=" + eventId);
            
            var eventTitle = eventDetails.ChildNodes.First().InnerText;
            return eventTitle;
        }

        /// <summary>
        /// Open Event details view from Scheduler
        /// </summary>
        /// <param name="eventTitle">Event Title</param>
        public void OpenEventsDetailsInScheduler(string eventTitle)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var eventDetailsLink = frontendPageMainDiv.Find.ByExpression<HtmlAnchor>("class=sf-event-link", "innerText=" + eventTitle);
            eventDetailsLink.AssertIsPresent("Event details link");
            eventDetailsLink.Click();
        }

        /// <summary>
        /// Get event title in scheduler detail's view
        /// </summary>
        /// <returns>Event Title</returns>
        public string GetEventTitleInDetailsView()
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var tempContainer = frontendPageMainDiv.Find.ByExpression<HtmlControl>("tagname=h3", "class=sf-event-title");
            var eventTitle = tempContainer.ChildNodes.First().InnerText;
            return eventTitle;

        }

        /// <summary>
        /// Get event datetime in sheduler detail's view
        /// </summary>
        /// <returns>Event datetime</returns>
        public string GetEventDateTimeInDetailsView()
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var eventTime = frontendPageMainDiv.Find.ByExpression<HtmlTime>("tagname=time").InnerText;
            return eventTime;
        }

        /// <summary>
        /// Verify URL in details view of event
        /// </summary>
        /// <param name="pageTitle">Pagetitle</param>
        /// <param name="activeCalendar">active calendar</param>
        /// <param name="eventDate">event date</param>
        /// <param name="eventTitle">event title</param>
        public void VerifyDetailsEventsPageUrl(string pageTitle, string activeCalendar, string eventDate, string eventTitle)
        {
            var formattedUrl = string.Format(CultureInfo.InvariantCulture, pageTitle + "/" + eventDate + "/" + activeCalendar + "/" + eventTitle.ToLower());
            Assert.IsTrue(ActiveBrowser.Url.Contains(formattedUrl));
        }

        /// <summary>
        /// Change scheduler's view 
        /// </summary>
        /// <param name="?">Scheduler View Type</param>
        public void ChangeSchedulerView(string schedulerViewType)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var schedulerStates = frontendPageMainDiv.Find.ByExpression<HtmlAnchor>("class=k-link", "role=button", "innerText=" + schedulerViewType);
            schedulerStates.Click();
        }

        /// <summary>
        /// Navigate to previous day in Scheduler Day view
        /// </summary>
        public void GoPrevious()
        {
            var previousDayButton = EM.Events.EventsFrontend.NavigatePrevious;
            previousDayButton.AssertIsPresent("Previous day button");
            previousDayButton.Click();
        }

        /// <summary>
        /// Navigate to next day in Scheduler Day view
        /// </summary>
        public void GoNext()
        {
            var nextDayButton = EM.Events.EventsFrontend.NavigateNext;
            nextDayButton.AssertIsPresent("Next day button");
            nextDayButton.Click();
        }

        /// <summary>
        /// Get event datetime in sheduler day's view
        /// </summary>
        /// <returns>Event time</returns>
        public string GetDateInDayView()
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var eventTime = frontendPageMainDiv.Find.ByExpression<HtmlSpan>("class=k-lg-date-format").InnerText;
            return eventTime;
        }

        /// <summary>
        /// Navigate back or forward to specific month in calendar accourding to given target datetime
        /// </summary>
        /// <param name="startDateTime">Current calendar datetime</param>
        /// <param name="targetDateTime">Target datetime</param>
        public void NavigateToDateInSchedulerMonthView(DateTime startDateTime, DateTime targetDateTime)
        {
            int monthsDifference = (startDateTime.Year - targetDateTime.Year) * 12 + (startDateTime.Month - targetDateTime.Month);

            if (monthsDifference < 0)
            {
                for (int i = 0; i > monthsDifference; i--)
                {
                    this.GoNext();
                }
            }
            else if (monthsDifference > 0)
            {
                for (int i = 0; i < monthsDifference; i++)
                {
                    this.GoPrevious();
                }
            }

            //Manager.Current.Wait.For(() => this.WaitForEventAppointmentToAppear());
        }


        /// <summary>
        /// Navigate back or forward to specific week in calendar accourding to given target datetime
        /// </summary>
        /// <param name="startDateTime">Current calendar datetime</param>
        /// <param name="targetDateTime">Target datetime</param>
        public void NavigateToDateInSchedulerWorkWeekView(DateTime startDateTime, DateTime targetDateTime)
        
        {
            TimeSpan ts = startDateTime.Subtract(targetDateTime);
            int dateDiff = ts.Days;
            int totalWeeks = dateDiff / 7;

            if (totalWeeks < 0)
            {
                for (int i = 0; i > totalWeeks; i--)
                {
                    this.GoNext();
                }
                if (!this.IsAppointmentVisible())
                {
                    this.GoNext();
                }
            }
            else if (totalWeeks > 0)
            {
                for (int i = 0; i < totalWeeks; i++)
                {
                    this.GoPrevious();
                }
                if (!this.IsAppointmentVisible())
                {
                    this.GoPrevious();
                }
            }
        }

        /// <summary>
        /// Return event occurence count in current view 
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>Event occurence count</returns>
        public int EventOccurenceCountInCurrentView(string eventId)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var eventItems = frontendPageMainDiv.Find.AllByExpression<HtmlDiv>("data-sf-eventid=" + eventId);
            return eventItems.Count();
        }

        /// <summary>
        /// Get event atrributes (id, start date, end date) of an event
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>List of event occurances</returns>
        public List<EventOccurence> GetEventAttributesInCurrentView(string eventId)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var eventItems = frontendPageMainDiv.Find.AllByExpression<HtmlDiv>("data-sf-eventid=" + eventId);

            List<EventOccurence> list = new List<EventOccurence>();

            foreach (HtmlDiv item in eventItems)
            {
                var id = item.Attributes.Where(p => p.Name == "data-sf-eventid").FirstOrDefault();
                var startDate = item.Attributes.Where(p => p.Name == "data-sf-date-start").FirstOrDefault();
                var endDate = item.Attributes.Where(p => p.Name == "data-sf-date-end").FirstOrDefault();
                var allDayEvent = item.Attributes.Where(p => p.Name == "data-sf-allday").FirstOrDefault();
             
                if (id == null || startDate == null || endDate == null || allDayEvent == null
                    || String.IsNullOrWhiteSpace(id.Value) || String.IsNullOrWhiteSpace(startDate.Value) 
                    || String.IsNullOrWhiteSpace(endDate.Value) || String.IsNullOrWhiteSpace(allDayEvent.Value))
                {
                    continue;
                }

                bool allDay;
                if (!bool.TryParse(allDayEvent.Value, out allDay))
                {
                    allDay = false;
                }

                list.Add(new EventOccurence()
                {
                    EndDate = endDate.Value,
                    Id = id.Value,
                    StartDate = startDate.Value,
                    AllDayEvent = allDay
                });
            }

            return list;
        }

        /// <summary>
        /// Verify date of event occurance in current view
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="date">Event date</param>
        /// <param name="isStart">Is start date</param>
        public void VerifyEventDate(string eventId, string date, bool isStart, int eventOccuranceIndex)
        {
            List<EventOccurence> list = this.GetEventAttributesInCurrentView(eventId);
           
            //for(int i = 0; i < list.Count; i++)
            //{
                if (isStart)
                {
                    string startDate = list[eventOccuranceIndex].StartDate;
                    Assert.IsTrue(startDate.Contains(date), "Wrong start date");
                }
                else
                {
                    string endDate = list[eventOccuranceIndex].EndDate;
                    Assert.IsTrue(endDate.Contains(date), "Wrong end date");
                }
           // }
        }

        /// <summary>
        /// Open Calendar selector
        /// </summary>
        public void OpenCalendarSelector()
        {
            var calendarSelector = EM.Events.EventsFrontend.CalendarSelector;
            calendarSelector.AssertIsPresent("Calendar selector button");
            calendarSelector.Click();
        }

        /// <summary>
        /// Open fast navigation by year in Calendar Selector
        /// </summary>
        public void OpenCurrentYearMonthsInCalendarSelector()
        {
            this.OpenCalendarSelector();
            var fastNavigation = EM.Events.EventsFrontend.FastNavigationInCalendarSelector;
            fastNavigation.AssertIsPresent("Fast navigation link in Calendar selector");
            fastNavigation.Click();
        }

       /// <summary>
       /// Select month in Calendar Selector
       /// </summary>
       /// <param name="monthName">Month name</param>
        public void SelectMonthInCalendarSelector(string monthName)
        {
            HtmlTable monthsTable = EM.Events.EventsFrontend.MonthsTableCalendarSelector;
            monthsTable.AssertIsPresent("Month table");
            HtmlTableCell month = monthsTable.Find.ByExpression<HtmlTableCell>("InnerText=" + monthName);
            month.AssertIsPresent("Month");
            HtmlAnchor monthLink = month.Find.AllByTagName<HtmlAnchor>("a").First();
            monthLink.AssertIsPresent("Month link");
            monthLink.Focus();
            monthLink.Click();
        }

        /// <summary>
        /// Open month view in Calendar Selector
        /// </summary>
        public void OpenMonthViewInCalendarSelector(int targetDay = 15)
        {
            HtmlTable daysTable = EM.Events.EventsFrontend.DaysInMonthTableCalendarSelector;
            daysTable.AssertIsPresent("Day table");
            HtmlTableCell day = daysTable.Find.ByExpression<HtmlTableCell>("InnerText=" + targetDay);
            day.AssertIsPresent("Day");
            HtmlAnchor monthLink = day.Find.AllByTagName<HtmlAnchor>("a").First();
            monthLink.AssertIsPresent("Day link");
            monthLink.Focus();
            monthLink.Click();
        }

        /// <summary>
        /// Fast navigation to specific year
        /// </summary>
        /// <param name="startYear">Start year</param>
        /// <param name="targetYear">Target year</param>
        public void FastNavigateToSpecificYear(int startYear, int targetYear)
        {
            int yearDifference = (startYear - targetYear);

            if (yearDifference < 0)
            {
                for (int i = 0; i > yearDifference; i--)
                {
                    this.NavigateNextYearInCalendarSelector();
                }
            }

            else if (yearDifference > 0)
            {
                for (int i = 0; i < yearDifference; i++)
                {
                    this.NavigatePreviousYearInCalendarSelector();
                }
            }
        }

        /// <summary>
        /// Navigate to next year in fast navigation
        /// </summary>
        public void NavigateNextYearInCalendarSelector()
        {
            ActiveBrowser.RefreshDomTree();
            ActiveBrowser.WaitUntilReady();
            var nextYearButton = EM.Events.EventsFrontend.NavigateNextInFastNavigation;
            nextYearButton.AssertIsPresent("Next year button");
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForElementWithCssClass("^k-link k-nav-next");
            nextYearButton.Click();
        }

        /// <summary>
        /// Open target day in day view in Scheduler
        /// </summary>
        /// <param name="targetDay">Target day</param>
        public void OpenTargetDayInDayView(string targetDay)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var day = frontendPageMainDiv.Find.ByExpression<HtmlSpan>("class=k-link k-nav-day", "innerText=" + targetDay);
            day.Click();
        }

        /// <summary>
        /// Get visible event in current view
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="allDayEvent">All day event</param>
        /// <returns>List of visible events</returns>
 
        public IEnumerable<EventOccurence> GetVisibleEventInCurrentView(string eventId, bool allDayEvent)
        {
            return this.GetEventAttributesInCurrentView(eventId).Where(p => p.AllDayEvent == allDayEvent);
        }

        /// <summary>
        /// Navigate to previous year in fast navigation
        /// </summary>
        public void NavigatePreviousYearInCalendarSelector()
        {
            ActiveBrowser.RefreshDomTree();
            ActiveBrowser.WaitUntilReady();
            var previousYearButton = EM.Events.EventsFrontend.NavigatePreviousInFastNavigation;
            previousYearButton.AssertIsPresent("Previous year button");
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForElementWithCssClass("k-link k-nav-prev");
            previousYearButton.Click();
        }

        /// <summary>
        /// Fast navigation by year in calendar selector
        /// </summary>
        /// <param name="monthName">Month name</param>
        /// <param name="startYear">Start year</param>
        /// <param name="targetYear">Target year</param>
        public void FastNavigationByYearInCalendarSelector(string monthName, int startYear, int targetYear)
        {
            this.OpenCurrentYearMonthsInCalendarSelector();
            this.FastNavigateToSpecificYear(startYear, targetYear);
            this.SelectMonthInCalendarSelector(monthName);
            this.OpenMonthViewInCalendarSelector();
        }

        public int LocalTimeZoneOffset()
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var frontendPageChildDiv = frontendPageMainDiv.ChildNodes.First();
            var temp = frontendPageChildDiv.Attributes.Single(a => a.Name == "data-sf-localtimezoneoffset").Value;
            int localTimeZonneOffset = Int32.Parse(temp);
            return localTimeZonneOffset;
        }

        /// <summary>
        /// Wait for event appointment to appear
        /// </summary>
        /// <returns>Is appointment visible</returns>
        private bool IsAppointmentVisible()
        {
            ActiveBrowser.RefreshDomTree();

            HtmlDiv appointment = EM.Events.EventsFrontend.Appointment;
            bool isAppointmentVisible = false;
            isAppointmentVisible = (appointment != null) && (appointment.IsVisible());
            return isAppointmentVisible;
        }
    }
}