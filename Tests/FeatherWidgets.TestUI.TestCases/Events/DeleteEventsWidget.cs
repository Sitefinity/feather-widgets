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
    /// Deletes an existing Events Widget.
    /// Verifies that the Events Widget is not visible in Frontend.
    /// </summary>
    [TestClass]
    public class DeleteEventsWidget_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for DeleteEventsWidget_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void DeleteEventsWidget()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationNameDelete);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetIsAvailable("Calendar");
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), culture: this.Culture);
            Assert.IsFalse(BATFeather.Wrappers().Frontend().CommonWrapper().IsItemPresent(EventsTitle), "Events widget was not deleted");
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
        private const string OperationNameDelete = "Delete";
    }
}

