using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Events
{
    /// <summary>
    /// Provides predefined option for event occurence
    /// </summary>
    public class EventOccurence
    {
        public string Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool AllDayEvent { get; set; }
    }
}
