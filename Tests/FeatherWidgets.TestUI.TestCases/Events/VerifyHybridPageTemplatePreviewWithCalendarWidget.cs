using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Events;
using System.Globalization;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create a few events and a Hybrid template with Calendar Widget.
    /// Verify events visibility on template preview and in edit mode.
    /// </summary>
    [TestClass]
    public class VerifyHybridPageTemplatePreviewWithCalendarWidget_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyHybridPageTemplatePreviewWithCalendarWidget_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyHybridPageTemplatePreviewWithCalendarWidget()
        {
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateName);
            this.VerifyEventVisibilityInCurrentView(event1Id, 1);
            this.VerifyEventVisibilityInCurrentView(event2Id, 1);

            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PreviewTemplateFromEdit();
            this.VerifyEventVisibilityInCurrentView(event1Id, 1);
            this.VerifyEventVisibilityInCurrentView(event2Id, 1);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            var result = BAT.Arrange(this.TestName).ExecuteSetUp();
            event1Id = result.Result.Values["event1Id"];
            event2Id = result.Result.Values["event2Id"];
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        /// <summary>
        /// Verify event visibility in current view
        /// </summary>
        /// <param name="eventId">Event ID</param>
        private void VerifyEventVisibilityInCurrentView(string eventId, int expectedCount)
        {
            var list = BATFeather.Wrappers().Frontend().Events().EventsWrapper().GetVisibleEventInCurrentView(eventId, false);
            Assert.IsTrue(list.Count() == expectedCount, "The event is not visible");
        }

        private const string TemplateName = "Calendar";
        private string event1Id = string.Empty;
        private string event2Id = string.Empty;
    }
}
