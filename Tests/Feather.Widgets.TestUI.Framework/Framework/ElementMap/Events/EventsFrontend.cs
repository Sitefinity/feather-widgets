using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Events
{
    /// <summary>
    /// Elements from Events Frontend
    /// </summary>
    public class EventsFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventsFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public EventsFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Previous day button in Scheduler Day's view
        /// </summary>
        public HtmlListItem NavigatePrevious
        {
            get
            {
                return this.Get<HtmlListItem>("class=~k-nav-prev");
            }
        }

        /// <summary>
        /// Next day button in Scheduler Day's view
        /// </summary>
        public HtmlListItem NavigateNext
        {
            get
            {
                return this.Get<HtmlListItem>("class=~k-nav-next");
            }
        }

        /// <summary>
        /// Calendar selector button
        /// </summary>
        public HtmlSpan CalendarSelector
        {
            get
            {
                return this.Get<HtmlSpan>("class=k-icon k-i-calendar");
            }
        }

        /// <summary>
        /// Fast navigation in Calendar Selector
        /// </summary>
        public HtmlAnchor FastNavigationInCalendarSelector
        {
            get
            {
                return this.Get<HtmlAnchor>("class=k-link k-nav-fast");
            }
        }

        /// <summary>
        /// Navigate next link in fast navigation 
        /// </summary>
        public HtmlAnchor NavigateNextInFastNavigation
        {
            get
            {
                return this.Get<HtmlAnchor>("class=k-link k-nav-next");
            }
                
        }

        /// <summary>
        /// Navigate previous link in fast navigation 
        /// </summary>
        public HtmlAnchor NavigatePreviousInFastNavigation
        {
            get
            {
                return this.Get<HtmlAnchor>("class=k-link k-nav-prev");
            }

        }

        /// <summary>
        /// Navigate previous link in fast navigation 
        /// </summary>
        public HtmlTable MonthsTableCalendarSelector
        {
            get
            {
                return this.Get<HtmlTable>("class=k-content k-meta-view");
            }

        }

        public HtmlTable DaysInMonthTableCalendarSelector
        {
            get
            {
                return this.Get<HtmlTable>("class=k-content");
            }
        }

        public HtmlDiv Appointment
        {
            get
            {
                return this.Find.ByExpression<HtmlDiv>("class=sf-event-item");
            }
        }
    }
}
