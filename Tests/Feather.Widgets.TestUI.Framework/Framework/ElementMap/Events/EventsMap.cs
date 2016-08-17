using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Events
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather Events widget back-end screens.
    /// </summary>
    public class EventsMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventsMap" /> class.
        /// </summary>
        public EventsMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the Events widget frontend
        /// </summary>
        public EventsFrontend EventsFrontend
        {
            get
            {
                return new EventsFrontend(this.find);
            }
        }

        private Find find;
    }
}
