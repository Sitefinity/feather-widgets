using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Feather.Widgets.TestUI.Framework;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// DragAndDropEventsWidgetOnPage test class.
    /// </summary>
    [TestClass]
    public class DragAndDropEventsWidgetOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DragAndDropEventsWidgetOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.Events),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void DragAndDropEventsWidgetOnPage()
        {
            this.pageTemplateName = "Bootstrap.default";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).AddParameter("templateName", this.pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, EventsTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyEventsOnTheFrontend();
        }

        /// <summary>
        /// Verify events widget on the frontend
        /// </summary>
        public void VerifyEventsOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Events().EventsWrapper().AreEventTitlesPresentOnThePageFrontend(this.eventsTitles));
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "EventsPage";
        private const string WidgetName = "Events";
        private const string EventsTitle = "EventsTitle";
        private readonly string[] eventsTitles = new string[] { EventsTitle };
        private string pageTemplateName;
    }
}
