using System;
using System.Collections.Generic;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.SocialShare
{
    /// <summary>
    /// CheckIfRequestToDecIsSendOnSocialButtonClick test class.
    /// </summary>
    [TestClass]
    public class CheckIfRequestToDecIsSendOnListExpanded_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CheckIfRequestToDecIsSendOnListExpanded
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam3),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Lists)]
        public void CheckIfRequestToDecIsSendOnListExpanded()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton(0);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(ListTitle);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Lists().ListsWidgetWrapper().SelectListTemplate(ListTemplate);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Backend().Widgets().WidgetDecTrackingWrapper().SetDecDataToBeChecked("Expand list");
            BATFeather.Wrappers().Frontend().Lists().ListsWidgetWrapper().VerifyCorrectDecInfoIsSend();
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

        private const string PageName = "TestPage";
        private const string WidgetName = "Lists";
        private const string ListTitle = "Test list";
        private const string ListTemplate = "Expandable list";
    }
}
