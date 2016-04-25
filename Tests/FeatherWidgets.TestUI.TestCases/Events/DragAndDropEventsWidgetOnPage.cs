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
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void DragAndDropEventsWidgetOnPage()
        {
            this.pageTemplateName = "Bootstrap.default";

            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).AddParameter("templateName", this.pageTemplateName).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, EventsTitle);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNewsOnTheFrontend();
        }

        /// <summary>
        /// Verify news widget on the frontend
        /// </summary>
        public void VerifyNewsOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.eventsTitles));
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.ArrangementClass).ExecuteTearDown();
        }

        private string ArrangementClass
        {
            get { return ArrangementClassName; }
        }

        private const string PageName = "Events";
        private const string WidgetName = "Events";
        private const string EventsTitle = "EventsTitle";
        private readonly string[] eventsTitles = new string[] { EventsTitle };
        private string pageTemplateName;
        private const string ArrangementClassName = "DragAndDropEventsWidgetOnPage";
    }
}
