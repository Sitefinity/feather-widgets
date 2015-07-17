using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.VideoGallery
{
    /// <summary>
    /// SelectAllPublishedVideos test class.
    /// </summary>
    [TestClass]
    public class SelectAllPublishedVideos_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectAllPublishedVideos
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.VideoGallery)]
        public void SelectAllPublishedVideos()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.allPublished);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ExpandNarrowSelectionByArrow();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.allItems);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.usePaging);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.samePage);           
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            foreach (var video in this.videoTitles)
            {
                string src = this.GetVideoSource(false, video, ImageType);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(video, src);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            int i = 3;
            foreach (var video in this.videoTitles)
            {
                var src = this.GetVideoSource(false, video, ImageType);
                BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(VideoAltText + i, src);
                i--;
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(VideoAltText + 2);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().IsVideoTitlePresentOnDetailMasterPage(this.videoTitles[1]));

            var scr = this.GetVideoSource(true, this.videoTitles[1], VideoType);
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyVideo(scr);

            var hrefPrevious = this.GetVideoHref(true, this.videoTitles[0]);
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyPreviousVideo(hrefPrevious);

            var hrefNext = this.GetVideoHref(true, this.videoTitles[2]);
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyNextVideo(hrefNext);

            var hrefBack = "/" + PageName.ToLower() + "/" + "Index/";
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyBackToAllVideos(hrefBack);
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

        private string GetVideoSource(bool isBaseUrlIncluded, string videoName, string videoType)
        {
            string libraryUrl = LibraryName.ToLower();
            string videoUrl = videoName.ToLower() + videoType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, videoUrl, this.BaseUrl, "videos");
            return scr;
        }

        private string GetVideoHref(bool isBaseUrlIncluded, string videoName)
        {
            string libraryUrl = LibraryName.ToLower();
            string videoUrl = videoName.ToLower();
            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, videoUrl, this.BaseUrl, PageName.ToLower() + "/videos");
            return href;
        }

        private const string PageName = "PageWithVideo";
        private readonly string[] videoTitles = new string[] { "Video3", "Video2", "Video1" };
        private const string WidgetName = "Video gallery";
        private const string LibraryName = "TestVideoLibrary";
        private const string VideoAltText = "Video";
        private const string VideoType = ".mp4";
        private const string ImageType = ".TMB";
    }
}