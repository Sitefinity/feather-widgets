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
    /// Create a few events and a hybrid page template with Calendar Widget.
    /// Create a page based on the created template.
    /// Verify events visibility on page view.
    /// </summary>
    [TestClass]
    public class VerifyPageWithHybridPageTemplateContainingCalendarWidget_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyPageWithHybridPageTemplateContainingCalendarWidget_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyPageWithHybridPageTemplateContainingCalendarWidget()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + pageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event1Id, 1, false);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event2Id, 1, false);
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

        private const string pageTitle = "EventsPage";
        private const string Event1Title = "Event1Title";
        private string event1Id = string.Empty;
        private const string Event2Title = "Event2Title";
        private string event2Id = string.Empty;

    }
}
