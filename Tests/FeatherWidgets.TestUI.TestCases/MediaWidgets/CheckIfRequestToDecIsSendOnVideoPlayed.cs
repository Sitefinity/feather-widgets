using System;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.SocialShare
{
    /// <summary>
    /// CheckIfRequestToDecIsSendOnVideoPlayed test class.
    /// </summary>
    [TestClass]
    public class CheckIfRequestToDecIsSendOnVideoPlayed_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CheckIfRequestToDecIsSendOnVideoPlayed
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam3),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.MediaSelector)]
        public void CheckIfRequestToDecIsSendOnVideoPlayed()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName, 0, true);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitForContentToBeLoaded(false);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(VideoName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelectionInWidget();
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().ConfirmMediaPropertiesInWidget();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Backend().Widgets().WidgetDecTrackingWrapper().SetDecDataToBeChecked("Play video");
            var video = ActiveBrowser.Find.ByExpression<HtmlVideo>("data-sf-role=playVideo");
            ActiveBrowser.WaitForAjax(1000);
            video.Play();
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyCorrectDecInfoIsSend(video.Src);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
            currentProviderUrlName = BAT.Arrange(this.TestName).ExecuteArrangement("GetCurrentProviderUrlName").Result.Values["CurrentProviderUrlName"];
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private string currentProviderUrlName;

        private const string PageName = "PageWithVideo";
        private const string WidgetName = "Video";
        private const string VideoName = "big_buck_bunny1";
    }
}
