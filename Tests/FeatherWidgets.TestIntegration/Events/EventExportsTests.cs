using System;
using System.Globalization;
using System.Text.RegularExpressions;
using MbUnit.Framework;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestIntegration.Events
{
    /// <summary>
    /// This class contains integration tests for event export link generation.
    /// </summary>
    [TestFixture]
    public class EventExportsTests
    {
        /// <summary>
        /// Ensures that event export google link is correctly generated.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that event export google link is correctly generated.")]
        public void EventGoogleExportLinkTest()
        {
            const string ExpectedLink = "http://www.google.com/calendar/event?action=TEMPLATE&text=&dates=20160203T020000Z/20160203T040000Z&location=&sprop=website:http://localhost&sprop=name:&details=&recur=";
            var ev = new Event() { EventStart = new DateTime(2016, 2, 3, 4, 0, 0), EventEnd = new DateTime(2016, 2, 3, 6, 0, 0) };
            Assert.AreEqual(ExpectedLink, new ItemViewModel(ev).GenerateGoogleUrl());
        }

        /// <summary>
        /// Ensures that event export outlook link is correctly generated.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that event export outlook link is correctly generated.")]
        public void EventOutlookExportLinkTest()
        {
            const string ExpectedLink = "/Sitefinity/Public/Services/ICalanderService/file.ics/?id=00000000-0000-0000-0000-000000000000&provider=&uiculture=en-US";
            var ev = new Event() { EventStart = new DateTime(2016, 2, 3, 4, 0, 0), EventEnd = new DateTime(2016, 2, 3, 6, 0, 0) };
            Assert.AreEqual(ExpectedLink, new ItemViewModel(ev).GenerateOutlookUrl());
        }

        /// <summary>
        /// Ensures that event export iCal link is correctly generated.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that event export iCal link is correctly generated.")]
        public void EventICalExportLinkTest()
        {
            const string ExpectedLink = "/Sitefinity/Public/Services/ICalanderService/file.ics/?id=00000000-0000-0000-0000-000000000000&provider=&uiculture=en-US";
            var ev = new Event() { EventStart = new DateTime(2016, 2, 3, 4, 0, 0), EventEnd = new DateTime(2016, 2, 3, 6, 0, 0) };
            Assert.AreEqual(ExpectedLink, new ItemViewModel(ev).GenerateICalUrl());
        }
    }
}