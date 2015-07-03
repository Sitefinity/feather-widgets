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
    /// SubscribeFormWidgetOnFoundationAndSemanticPage test class.
    /// </summary>
    [TestClass]
    public class SubscribeFormWidgetOnFoundationAndSemanticPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubscribeFormWidgetOnFoundationAndSemanticPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.EmailCampaigns),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void SubscribeFormWidgetOnFoundationAndSemanticPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageNameSemantic);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().SelectItemsInFlatSelector(MailingList);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageNameSemantic.ToLower(), false);
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().VerifySubscribeMessageOnTheFrontend();
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().FillEmailNotStyledPage(SubscriberEmail);
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().ClickSubscribeButtonNotStyledPage();
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().VerifySuccessfullySubscribeMessageOnTheFrontend(SubscriberEmail);

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageNameFoundation);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().SelectItemsInFlatSelector(MailingList);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageNameFoundation.ToLower(), false);
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().VerifySubscribeMessageOnTheFrontend();
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().FillEmailNotStyledPage(SubscriberEmail);
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().ClickSubscribeButtonNotStyledPage();
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().VerifySuccessfullySubscribeMessageOnTheFrontend(SubscriberEmail);
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

        private const string PageNameSemantic = "SubscribeFormPageSemantic";
        private const string PageNameFoundation = "SubscribeFormPageFoundation";
        private const string PageTemplateName = "Bootstrap.default";
        private const string MailingList = "MailList";
        private const string SubscriberEmail = "testNew@email.com";
        private const string WidgetName = "Subscribe form";
    }
}
