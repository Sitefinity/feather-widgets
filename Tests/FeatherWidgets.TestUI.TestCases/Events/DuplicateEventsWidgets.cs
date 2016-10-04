using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Duplicate existing Events Widget on a page.
    /// Verify that both Events Widgets are displayed in Frontend.
    /// </summary>
    [TestClass]
    public class DuplicateEventsWidgets_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for DuplicateEventsWidgets_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void DuplicateEventsWidgets()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyCorrectOrderOfItemsOnBackend(EventsTitle, EventsTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), culture: this.Culture);
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifyDuplicatedWidgetInFrontend(EventsTitle, expectedCount);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageTitle = "EventsPage";
        private const string EventsTitle = "EventTitle";
        private const int expectedCount = 2;
        private const string OperationName = "Duplicate";
    }
}
