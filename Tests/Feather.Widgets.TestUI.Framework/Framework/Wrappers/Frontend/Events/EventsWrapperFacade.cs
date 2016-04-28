using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Events
{
    /// <summary>
    /// This is the entry point class for events frontend wrappers.
    /// </summary>
    public class EventsWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the EventsWrapper
        /// </summary>
        /// <returns>Returns the EventsWrapper</returns>
        public EventsWrapper EventsWrapper()
        {
            return new EventsWrapper();
        }
    }
}
