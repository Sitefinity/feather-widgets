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
    /// Create a few events and a pure Mvc template with Calendar Widget.
    /// Verify events visibility on template preview and in edit mode.
    /// </summary>
    [TestClass]
    public class VerifyCalendarWidgetInPageTemplateAndInPage_ : FeatherTestCase
    {
        /// <summary>
        /// Test Method that provides test steps for VerifyCalendarWidgetInPageTemplateAndInPage_ UI Test.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Events)]
        public void VerifyCalendarWidgetInPageTemplateAndInPage()
        {
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().ClickOnCreateNewTemplateBtn();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateCreateScreen().SetTemplateName(TemplateName);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateCreateScreen().ClickOnAdvancedSettingsBtn();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateCreateScreen().ClickMvcOnlyRadioButton();
            BAT.Wrappers().Backend().PageTemplates().PageTemplateCreateScreen().ClickOnCreateTemplateAndGoToAddContentBtn();
            ActiveBrowser.WaitForElementEndsWithID("PublicWrapper");
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName, "Body");
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event1Id, 1, false);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event2Id, 1, false);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenCreatePageWindow();
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().SetPageTitle(PageName);
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().ClickSelectAnotherTemplateButton();
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().SelectATemplate(TemplateName);
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().ClickDoneButton();
            BAT.Wrappers().Backend().Pages().PagesWrapper().SavePageDataAndContinue();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().WaitUntilReady();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName, "Body");
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event1Id, 2, false);
            BATFeather.Wrappers().Frontend().Events().EventsWrapper().VerifyEventVisibilityInCurrentView(event2Id, 2, false);
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

        private const string PageName = "Events";
        private const string WidgetName = "Calendar";
        private const string TemplateName = "Calendar";
        private string event1Id = string.Empty;
        private string event2Id = string.Empty;
    }
}
