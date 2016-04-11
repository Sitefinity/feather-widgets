using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// A class with tests related to Event widget list settings.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Event widget list settings tests.")]
    public class EventWidgetListSettingsTests
    {
        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display events in paging - two item per page")]
        public void EventWidget_UsePaging_TwoItemPerPage()
        {
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display limited events.")]
        public void EventWidget_UseLimit_TwoItem()
        {
            
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display events with no limit and paging.")]
        public void EventWidget_NoLimitAndPaging()
        {
            
        }

        [Test]
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Add Event widget to a page and display sorted events.")]
        public void EventWidget_ItemsAreSorted()
        {

        }
    }
}
