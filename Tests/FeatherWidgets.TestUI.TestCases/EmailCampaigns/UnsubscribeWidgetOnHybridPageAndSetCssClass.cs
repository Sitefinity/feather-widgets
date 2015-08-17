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
    /// UnsubscribeWidgetOnHybridPageAndSetCssClass test class.
    /// </summary>
    [TestClass]
    public class UnsubscribeWidgetOnHybridPageAndSetCssClass_ : FeatherTestCase
    {
        /// <summary>
        /// UI test UnsubscribeWidgetOnHybridPageAndSetCssClass
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.EmailCampaigns),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void UnsubscribeWidgetOnHybridPageAndSetCssClass()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().EmailCampaigns().UnsubscribeWrapper().SelectEmailAddress();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().SelectItemsInFlatSelector(MailingList);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().EmailCampaigns().UnsubscribeWrapper().ApplyCssClasses(CssClass);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().VerifyUnsubscribeMessageOnTheFrontendHybridPage();
            Assert.IsTrue(ActiveBrowser.ContainsText(CssClass), "Css class was not found on the page");
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().FillEmailNotStyledPage(SubscriberEmail);
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().ClickUnsubscribeButtonNotStyled();
            BATFeather.Wrappers().Frontend().EmailCampaigns().UnsubscribeWrapper().VerifySuccessfullyUnsubscribedMessageOnTheFrontendNotStyled(SubscriberEmail);
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
        private const string SubscriberEmail = "test@email.com";
        private const string WidgetName = "Unsubscribe";
        private const string CssClass = "This is css class";
    }
}
