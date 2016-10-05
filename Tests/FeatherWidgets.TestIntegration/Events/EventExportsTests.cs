﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using MbUnit.Framework;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Services;

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
        [Category(TestCategories.Events)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that event export google link is correctly generated.")]
        public void EventGoogleExportLinkTest()
        {
            var url = SystemManager.RootUrl.TrimEnd('/');

            string expectedLink = "http://www.google.com/calendar/event?action=TEMPLATE&text=&dates=20160203T040000Z/20160203T060000Z&location=&sprop=website:" + url + "&sprop=name:&details=&recur=";
            var ev = new Event() { EventStart = new DateTime(2016, 2, 3, 4, 0, 0, DateTimeKind.Utc), EventEnd = new DateTime(2016, 2, 3, 6, 0, 0, DateTimeKind.Utc) };
            Assert.AreEqual(expectedLink, new ItemViewModel(ev).GenerateGoogleUrl());
        }

        /// <summary>
        /// Ensures that event export outlook link is correctly generated.
        /// </summary>
        [Test]
        [Category(TestCategories.Events)]
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
        [Category(TestCategories.Events)]
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