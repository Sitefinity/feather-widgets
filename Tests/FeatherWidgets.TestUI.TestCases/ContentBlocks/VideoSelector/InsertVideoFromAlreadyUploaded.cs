using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks.VideoSelector
{
    /// <summary>
    /// This is a test class for content block > video selector tests
    /// </summary>
    [TestClass]
    public class InsertVideoFromAlreadyUploaded_ : FeatherTestCase
    {
        /// <summary>
        /// UI test InsertVideoFromAlreadyUploaded
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void InsertVideoFromAlreadyUploaded()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenVideoSelector();           
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().PressCancelButton();

            // Uploading video after epmty screen is verified.
            string videoId = BAT.Arrange(this.TestName).ExecuteArrangement("UploadVideo").Result.Values["videoId"];

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenVideoSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifySelectedFilter(SelectedFilterName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyMediaTooltip(VideoName, LibraryName, VideoType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(VideoName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifySmallVideoProperites(this.GetVideoSource(true));
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifySelectedOptionAspectRatioSelector("Auto");
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().PlayVideo();
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifyBigVideoProperites(this.GetVideoSource(true));
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().SelectOptionAspectRatioSelector("4x3");
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifyWidthAndHeightValues("600", "450");
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().ConfirmMediaProperties();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper()
                .VerifyContentBlockVideoDesignMode(this.GetVideoSource(true), this.GetSfRef(videoId), "600", "450");
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyVideo(this.GetVideoSource(false), Width, Height);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyVideo(this.GetVideoSource(false), Width, Height);
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

        private string GetSfRef(string videoId)
        {
            return "[videos|OpenAccessDataProvider]" + videoId;
        }

        private string GetVideoSource(bool isBaseUrlIncluded)
        {
            string libraryUrl = LibraryName.ToLower();
            string imageUrl = VideoName.ToLower() + VideoType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl, "videos");
            return scr;
        }

        private const string PageName = "PageWithVideo";
        private const string WidgetName = "ContentBlock";
        private const string VideoName = "big_buck_bunny";
        private const string LibraryName = "TestVideoLibrary";
        private const string VideoType = ".MP4";
        private const string SelectedFilterName = "Recent Videos";
        private const int Width = 600;
        private const int Height = 450;
    }
}
