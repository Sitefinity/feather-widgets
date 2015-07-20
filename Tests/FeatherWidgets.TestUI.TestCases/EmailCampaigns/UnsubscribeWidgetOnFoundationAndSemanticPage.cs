using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.EmailCampaigns
{
    /// <summary>
    /// UnsubscribeWidgetOnFoundationAndSemanticPage test class.
    /// </summary>
    [TestClass]
    public class UnsubscribeWidgetOnFoundationAndSemanticPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test UnsubscribeWidgetOnFoundationAndSemanticPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.EmailCampaigns),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void UnsubscribeWidgetOnFoundationAndSemanticPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageNameSemantic);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().EmailCampaigns().UnsubscribeWrapper().SelectEmailAddress();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().SelectItemsInFlatSelector(MailingList);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageNameSemantic.ToLower(), false);
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().VerifyUnsubscribeMessageOnTheFrontend();
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().FillEmailNotStyledPage(SubscriberEmail);
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().ClickUnsubscribeButtonNotStyled();
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().VerifySuccessfullyUnsubscribedMessageOnTheFrontend(SubscriberEmail);

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageNameFoundation);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().EmailCampaigns().UnsubscribeWrapper().SelectEmailAddress();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().SelectItemsInFlatSelector(SecondMailingList);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageNameFoundation.ToLower(), false);
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().VerifyUnsubscribeMessageOnTheFrontend();
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().FillEmailNotStyledPage(SubscriberEmail2);
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().ClickUnsubscribeButtonNotStyled();
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().VerifySuccessfullyUnsubscribedMessageOnTheFrontend(SubscriberEmail2);
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

        private const string PageNameSemantic = "UnsubscribePageSemantic";
        private const string PageNameFoundation = "UnsubscribeFoundation";
        private const string PageTemplateName = "Bootstrap.default";
        private const string MailingList = "MailList";
        private const string SecondMailingList = "SecondMailList";
        private const string SubscriberEmail = "test@email.com";
        private const string SubscriberEmail2 = "test2@email.com";
        private const string WidgetName = "Unsubscribe";
    }
}
