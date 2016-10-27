using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.SocialShare
{
    /// <summary>
    /// CheckIfRequestToDecIsSendOnSocialButtonClick test class.
    /// </summary>
    [TestClass]
    public class CheckIfRequestToDecIsSendOnSocialButtonClick_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CheckIfRequestToDecIsSendOnSocialButtonClick
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam3),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.SocialShare)]
        public void CheckIfRequestToDecIsSendOnSocialButtonClick()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().SocialShare().SocialShareWidgetEditWrapper().SelectUnselectAllSocialShareOptions();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Backend().Widgets().WidgetDecTrackingWrapper().SetDecDataToBeChecked("Share on social media");
            BATFeather.Wrappers().Frontend().SocialShare().SocialShareWrapper().VerifyCorrectDecInfoIsSend();
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

        private const string PageName = "SocialShare";
        private const string WidgetName = "Social share";
    }
}
