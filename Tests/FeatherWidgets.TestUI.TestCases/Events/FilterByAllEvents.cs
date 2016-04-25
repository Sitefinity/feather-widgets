using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Feather.Widgets.TestUI.Framework;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// FilterByAllEvents test class.
    /// </summary>
    [TestClass]
    public class FilterByAllEvents_ : FeatherTestCase
    {
        /// <summary>
        /// UI test FilterByAllEvents
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.Events)]
        public void FilterByAllEvents()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor("FilterByAllEvents");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            // BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
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

        private const string WidgetName = "Events";
        // private const string PageName = "FilterByAllEvents";
    }
}