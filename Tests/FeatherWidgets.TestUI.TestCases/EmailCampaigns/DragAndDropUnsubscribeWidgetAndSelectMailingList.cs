using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.EmailCampaigns
{
    /// <summary>
    /// DragAndDropUnsubscribeWidgetAndSelectMailingList test class.
    /// </summary>
    [TestClass]
    public class DragAndDropUnsubscribeWidgetAndSelectMailingList_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DragAndDropUnsubscribeWidgetAndSelectMailingList
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.EmailCampaigns),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void DragAndDropUnsubscribeWidgetAndSelectMailingList()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().EmailCampaigns().UnsubscribeWrapper().SelectEmailAddress();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().SelectItemsInFlatSelector(MailingList);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().VerifyUnsubscribeMessageOnTheFrontend();
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().FillEmail(UnsubscriberEmail);
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().ClickUnsubscribeButton();
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().VerifySuccessfullyUnsubscribeMessageOnTheFrontend(UnsubscriberEmail);
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

        private const string PageName = "UnsubscribeWidgetOnPage";
        private const string MailingList = "MailList";
        private const string UnsubscriberEmail = "test@email.com";
        private const string WidgetName = "Unsubscribe";
    }
}
