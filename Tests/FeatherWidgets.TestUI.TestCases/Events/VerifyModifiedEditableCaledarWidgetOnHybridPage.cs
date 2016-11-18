using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Events;
using System.Globalization;

namespace FeatherWidgets.TestUI.TestCases.Events
{
    /// <summary>
    /// Create a few events and a Hybrid page template with editable Calendar Widget.
    /// Create a page based on that template.
    /// Edit Calendar widget and publish the page.
    /// Verify events on page.
    /// </summary>
    [TestClass]
    public class VerifyModifiedEditableCaledarWidgetOnHybridPage_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyModifiedEditableCaledarWidgetOnHybridPage_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyModifiedEditableCaledarWidgetOnHybridPage()
        {
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName, "Body");
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(MakeEditableInPagesOption);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenCreatePageWindow();
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().SetPageTitle(PageName);
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().ClickSelectAnotherTemplateButton();
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().SelectATemplate(TemplateName);
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().ClickDoneButton();
            BAT.Wrappers().Backend().Pages().PagesWrapper().SavePageDataAndContinue();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().WaitUntilReady();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget("Calendar");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ExpandNarrowSelectionByArrow();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ExpandFilteredEventsBy();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectFilterByCalendar();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectCalendarInHierarchicalSelector(Calendar1Title);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event1Id, 1, false);
            ////BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event2Id, 0, false);
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

        private const string MakeEditableInPagesOption = "Make editable on pages";
        private const string PageName = "Events";
        private const string WidgetName = "Calendar";
        private const string TemplateName = "Calendar";
       
        private const string Calendar1Title = "Calendar1";
        private const string Event1Title = "Event1Title";
        private string event1Id = string.Empty;

        private const string Calendar2Title = "Calendar2";
        private const string Event2Title = "Event2Title";
        private string event2Id = string.Empty;
    }
}
