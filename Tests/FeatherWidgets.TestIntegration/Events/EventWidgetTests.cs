using MbUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

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

        public void EventWidget_DisplaysCurrentEvents()
        {
            // ServerOperations.Events().CreateAllDayEvent("")
        }

        private class AllKindsOfEventsSetup : IDisposable
        {
            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

    }
}
