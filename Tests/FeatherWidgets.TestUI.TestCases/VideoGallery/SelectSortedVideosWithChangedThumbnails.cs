using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.VideoGallery
{
    /// <summary>
    /// SelectSortedVideosWithChangedThumbnails test class.
    /// </summary>
    [TestClass]
    public class SelectSortedVideosWithChangedThumbnails_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectSortedVideosWithChangedThumbnails
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.VideoGallery)]
        public void SelectSortedVideosWithChangedThumbnails()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.CurrentlyOpenLibrary);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInSortingSelector("Title ASC");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInThumbnailSelector("Thumbnail: 120x94 px cropped");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifySelectedOptionAspectRatioSelector("Auto");
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().SelectOptionAspectRatioSelector("4x3");
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifyWidthAndHeightValues("600", "450");   
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyCorrectOrderOfImagesOnBackend(VideoAltText + 1, VideoAltText + 2, VideoAltText + 3);
            for (int k = 0; k <= 2; k++)
            {
                if (k == 0)
                {
                    string src = this.GetVideoSource(this.videoTitles[k], ImageType, LibraryName, "-vthumbnail.jpg");
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(this.videoTitles[k], src);
                }
                else
                {
                    string src = this.GetVideoSource(this.videoTitles[k], ImageType, AnotherVideoLibraryTitle, "-vthumbnail.jpg");
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(this.videoTitles[k], src);
                }
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyCorrectOrderOfImages(VideoAltText + 1, VideoAltText + 2, VideoAltText + 3);

            for (int i = 0; i <= 2; i++)
            {
                if (i == 0)
                {
                    string src = this.GetVideoSource(this.videoTitles[i], ImageType, LibraryName, "-vthumbnail.jpg");
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(VideoAltText + (i + 1), src);                   
                }
                else
                {
                    string src = this.GetVideoSource(this.videoTitles[i], ImageType, AnotherVideoLibraryTitle, "-vthumbnail.jpg");
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(VideoAltText + (i + 1), src);
                }
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(VideoAltText + 2);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().IsVideoTitlePresentOnDetailMasterPage(this.videoTitles[1]));

            var scr = this.GetVideodHref(this.videoTitles[1]);
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyVideo(scr, Width, Height);

            var hrefPrevious = this.GetVideoHref(this.videoTitles[0], LibraryName);
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyPreviousVideo(hrefPrevious);

            var hrefNext = this.GetVideoHref(this.videoTitles[2], AnotherVideoLibraryTitle);
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyNextVideo(hrefNext);

            var hrefBack = "/" + PageName.ToLower() + "/" + "Index/";
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyBackToAllVideos(hrefBack);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower() + "/" + AnotherVideoLibraryTitle.ToLower(), true, this.Culture);
            for (int j = 1; j <= 3; j++)
            {
                if (j == 1)
                {
                    BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyVideoIsNotPresent(VideoAltText + j);
                }
                else
                {
                    var src = this.GetVideoSource(this.videoTitles[j - 1], ImageType, AnotherVideoLibraryTitle, "-vthumbnail.jpg");
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(VideoAltText + j, src);
                }
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyCorrectOrderOfImages(VideoAltText + 2, VideoAltText + 3);
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

        private string GetVideoSource(string videoName, string videoType, string libraryUrl, string videoThumbnailSize = "")
        {
            string videoUrl = videoName.ToLower() + videoType.ToLower() + videoThumbnailSize;
            string url;

            if (this.Culture == null)
            {
                url = this.BaseUrl;
            }
            else
            {
                url = ActiveBrowser.Url.Substring(0, 20);
            }

            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(libraryUrl.ToLower(), videoUrl, "videos", currentProviderUrlName, this.Culture);
            return scr;
        }

        private string GetVideoHref(string videoName, string libraryUrl)
        {
            string videoUrl = videoName.ToLower();
            string url;

            if (this.Culture == null)
            {
                url = this.BaseUrl;
            }
            else
            {
                url = ActiveBrowser.Url.Substring(0, 20);
            }

            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(libraryUrl.ToLower(), videoUrl, PageName.ToLower() + "/videos", currentProviderUrlName, this.Culture);
            return href;
        }

        private string GetVideodHref(string videoName)
        {
            string documentUrl = videoName.ToLower();
            string libraryUrl = AnotherVideoLibraryTitle.ToLower();
            string url;

            if (this.Culture == null)
            {
                url = this.BaseUrl;
            }
            else
            {
                url = ActiveBrowser.Url.Substring(0, 20);
            }

            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetDownloadButtonSource(libraryUrl, documentUrl, "videos", currentProviderUrlName);
            return href;
        }

        private const string PageName = "PageWithVideo";
        private readonly string[] videoTitles = new string[] { "Video1", "Video2", "Video3" };
        private const string WidgetName = "Video gallery";
        private const string LibraryName = "TestVideoLibrary";
        private const string VideoAltText = "Video";
        private const string ImageType = ".TMB";
        private const string VideoType = ".mp4";
        private const string AnotherVideoLibraryTitle = "AnotherVideoLibrary";
        private const int Width = 600;
        private const int Height = 450;
        private string currentProviderUrlName;
    }
}