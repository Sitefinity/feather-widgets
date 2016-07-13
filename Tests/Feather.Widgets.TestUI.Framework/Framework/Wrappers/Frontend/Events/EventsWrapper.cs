using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="actualDateTime"></param>
        /// <returns>Event title string</returns>
        public string GetEventTitleInScheduler(string dateTime)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            var eventDetails = frontendPageMainDiv.Find.ByExpression<HtmlDiv>("class=sf-event-item", "data-sf-date=~" + dateTime);
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
        public void NavigateToPreviousDay()
        {
            var previousDayButton = EM.Events.EventsFrontend.PreviousDay;
            previousDayButton.AssertIsPresent("Previous day button");
            previousDayButton.Click();
        }

        /// <summary>
        /// Navigate to next day in Scheduler Day view
        /// </summary>
        public void NavigateToNextDay()
        {
            var nextDayButton = EM.Events.EventsFrontend.NextDay;
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
    }
}