using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// This class contains test related to basic functionality of Event Widget.
    /// </summary>
    [TestFixture]
    public class EventWidgetTests
    {
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display selected events only.")]
        public void EventWidget_DisplaySelectedEventsOnly()
        {
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and create events(past, current, upcoming, all day, repeat) with published and draft posts in order to verify that ")]
        public void EventWidget_DisplayAllPublishedEvents()
        {
        }
    }
}
