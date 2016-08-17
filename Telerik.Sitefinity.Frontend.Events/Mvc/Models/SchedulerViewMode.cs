using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Models
{
    /// <summary>
    /// Defines available modes for displaying the event scheduler views.
    /// </summary>
    public enum SchedulerViewMode
    {
        Month = 0,
        Week = 1,
        WorkWeek = 2,
        Day = 3,
        Agenda = 4,
        Timeline = 5
    }
}