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
        public HtmlListItem PreviousDay
        {
            get
            {
                return this.Get<HtmlListItem>("class=~k-nav-prev");
            }
        }

        /// <summary>
        /// Next day button in Scheduler Day's view
        /// </summary>
        public HtmlListItem NextDay
        {
            get
            {
                return this.Get<HtmlListItem>("class=~k-nav-next");
            }
        }
    }
}
