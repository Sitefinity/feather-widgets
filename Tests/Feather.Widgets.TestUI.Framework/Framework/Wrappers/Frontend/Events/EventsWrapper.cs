using ArtOfTest.WebAii.Controls.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Events
{
    /// <summary>
    /// This is the entry point class for events widget on the frontend.
    /// </summary>
    public class EventsWrapper
    {
        /// <summary>
        /// Verifies the events titles on the page frontend.
        /// </summary>
        /// <param name="eventTitles">The event titles.</param>
        /// <returns>true or false depending on event titles presence on frontend</returns>
        public bool AreEventTitlesPresentOnThePageFrontend(IEnumerable<string> eventTitles)
        {
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
    }
}