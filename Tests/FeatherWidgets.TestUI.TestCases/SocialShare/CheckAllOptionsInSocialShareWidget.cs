using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// CheckAllOptionsInSocialShareWidget test class.
    /// </summary>
    [TestClass]
    public class CheckAllOptionsInSocialShareWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CheckAllOptionsInSocialShareWidget
        /// </summary>
        [TestMethod,
        Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.SocialShare)]
        public void CheckAllOptionsInSocialShareWidget()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().SocialShare().SocialSharePageEditorWrapper().VerifySocialShareOptionsPresentInBackend(4, SocialShareOptions.Facebook, SocialShareOptions.Tweeter, SocialShareOptions.GooglePlus, SocialShareOptions.LinkedIn);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().SocialShare().SocialShareWidgetEditWrapper().SelectUnselectAllSocialShareOptions(false);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().SocialShare().SocialSharePageEditorWrapper().VerifySocialShareOptionsNotPresentInBackend(SocialShareOptions.Facebook, SocialShareOptions.Tweeter, SocialShareOptions.GooglePlus, SocialShareOptions.LinkedIn);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().SocialShare().SocialShareWidgetEditWrapper().SelectUnselectAllSocialShareOptions();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().SocialShare().SocialSharePageEditorWrapper().VerifySocialShareOptionsPresentInBackend(13, SocialShareOptions.Facebook, SocialShareOptions.Tweeter, 
                SocialShareOptions.GooglePlus, SocialShareOptions.GoogleBookmarks, SocialShareOptions.Digg, 
                SocialShareOptions.Blogger, SocialShareOptions.Tumblr, SocialShareOptions.LinkedIn,
                SocialShareOptions.Delicious, SocialShareOptions.MySpace, 
                SocialShareOptions.StumbleUpon, SocialShareOptions.Reddit, SocialShareOptions.MailTo);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().SocialShare().SocialShareWrapper().VerifySocialShareOptionsInFrontend(13, SocialShareOptions.Facebook, SocialShareOptions.Tweeter,
              SocialShareOptions.GooglePlus, SocialShareOptions.GoogleBookmarks, SocialShareOptions.Digg,
              SocialShareOptions.Blogger, SocialShareOptions.Tumblr, SocialShareOptions.LinkedIn,
              SocialShareOptions.Delicious, SocialShareOptions.MySpace,
              SocialShareOptions.StumbleUpon, SocialShareOptions.Reddit, SocialShareOptions.MailTo);
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
