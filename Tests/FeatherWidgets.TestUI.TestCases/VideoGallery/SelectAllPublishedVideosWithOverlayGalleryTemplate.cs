using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.VideoGallery
{
    /// <summary>
    /// SelectAllPublishedVideosWithOverlayGalleryTemplate test class.
    /// </summary>
    [TestClass]
    public class SelectAllPublishedVideosWithOverlayGalleryTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectAllPublishedVideosWithOverlayGalleryTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.VideoGallery)]
        public void SelectAllPublishedVideosWithOverlayGalleryTemplate()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInListTemplateSelector(OverlayGalleryTemplate);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();

            foreach (var video in this.videoTitles)
            {
                string src = this.GetVideoSource(false, video, ImageType);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(video, src);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            int i = 3;
            foreach (var image in this.videoTitles)
            {
                var src = this.GetVideoSource(false, image, ImageType);
                BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + i, src);
                i--;
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(ImageAltText + 2);
            var scr = this.GetVideoSource(true, this.videoTitles[1], VideoType);
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifySelectedVideoOverlayTemplate(scr);
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyPreviousAndNextVideoArrowsOverlayTemplate();
            scr = this.GetVideoSource(false, this.videoTitles[1], string.Empty);
            string url = PageName.ToLower() + scr;
            ActiveBrowser.WaitForUrl("/" + url, true, 60000);
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().CloseSelectedVideoOverlayTemplate();
            ActiveBrowser.WaitForUrl("/" + PageName.ToLower(), true, 60000);
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

        private string GetVideoSource(bool isBaseUrlIncluded, string imageName, string imageType)
        {
            string libraryUrl = LibraryName.ToLower();
            string imageUrl = imageName.ToLower() + imageType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl, "videos");
            return scr;
        }

        private const string PageName = "PageWithVideo";
        private readonly string[] videoTitles = new string[] { "Video3", "Video2", "Video1" };
        private const string WidgetName = "Video gallery";
        private const string LibraryName = "TestVideoLibrary";
        private const string ImageAltText = "Video";
        private const string ImageType = ".TMB";
        private const string VideoType = ".mp4";
        private const string OverlayGalleryTemplate = "OverlayGallery";
    }
}