using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Events.Mvc.Models;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Controllers
{
    /// <summary>
    /// The controller for Events Scheduler widget.
    /// </summary>
    public class SchedulerEventsController : Controller
    {
        /// <summary>
        /// Gets the scheduler events.
        /// </summary>
        /// <returns></returns>
        [Route("web-interface/events/")]
        public ActionResult GetSchedulerEvents(SchedulerEventsModel model)
        {
            var events = model.GetSchedulerEvents();

            return this.Json(events, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the calendars.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route("web-interface/calendars/")]
        public ActionResult GetCalendars(SchedulerEventsModel model)
        {
            var calendars = model.GetCalendars();

            return this.Json(calendars, JsonRequestBehavior.AllowGet);
        }
    }
}