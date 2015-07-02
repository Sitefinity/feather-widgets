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
    /// SubscribeFormWidgetOnHybridPageAndSetCssClass test class.
    /// </summary>
    [TestClass]
    public class SubscribeFormWidgetOnHybridPageAndSetCssClass_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SubscribeFormWidgetOnHybridPageAndSetCssClass
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.EmailCampaigns),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void SubscribeFormWidgetOnHybridPageAndSetCssClass()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().EmailCampaigns().SubscribeFormWrapper().SelectItemsInFlatSelector(MailingList);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ApplyCssClasses(CssClass);
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SaveChanges();            
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().VerifySubscribeMessageOnTheFrontendNotStyledPage();
            Assert.IsTrue(ActiveBrowser.ContainsText(CssClass), "Css class was not found on the page");
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().FillEmailNotStyledPage(SubscriberEmail);
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().ClickSubscribeButtonNotStyledPage();
            BATFeather.Wrappers().Frontend().EmailCampaigns().SubscibeFormWrapper().VerifySuccessfullySubscribeMessageOnTheFrontendNotStyledPage(SubscriberEmail);
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

        private const string PageName = "SubscribeFormPage";
        private const string MailingList = "MailList";
        private const string SubscriberEmail = "test@email.com";
        private const string WidgetName = "Subscribe form";
        private const string CssClass = "This is css class";
    }
}
