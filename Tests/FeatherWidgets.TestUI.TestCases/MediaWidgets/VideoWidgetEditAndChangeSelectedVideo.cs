using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.MediaWidgets
{
    /// <summary>
    /// This is a test class for content block > video selector tests
    /// </summary>
    [TestClass]
    public class VideoWidgetEditAndChangeSelectedVideo_ : FeatherTestCase
    { 
        /// <summary>
        /// UI test VideoWidgetEditAndChangeSelectedVideo
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void VideoWidgetEditAndChangeSelectedVideo()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName, 0, true);
  
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitForContentToBeLoaded(false);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(VideoName1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelectionInWidget();
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifyVideoInfo(VideoName1, VideoType, Size);
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().SelectOptionAspectRatioSelector(Ratio);
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().ConfirmMediaPropertiesInWidget();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName, 0, true);
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifyVideoInfo(VideoName1, VideoType, Size);
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifySelectedOptionAspectRatioSelector(Ratio);
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().ChangeMediaFile();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitForContentToBeLoaded(false);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(VideoName2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelectionInWidget();

            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifyVideoInfo(VideoName2, VideoType, Size);
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifySelectedOptionAspectRatioSelector(Ratio);
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().ConfirmMediaPropertiesInWidget();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyVideo(this.GetVideoSource(false, VideoName2), Width, Height);
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

        private string GetVideoSource(bool isBaseUrlIncluded, string documentName)
        {
            string libraryUrl = LibraryName.ToLower();
            string documentUrl = documentName.ToLower() + VideoType;
            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, documentUrl, this.BaseUrl, "videos");
            return href;
        }

        private const string PageName = "PageWithVideo";
        private const string WidgetName = "Video";      
        private const string LibraryName = "TestVideoLibrary";
        private const string VideoName1 = "big_buck_bunny1";
        private const string VideoName2 = "big_buck_bunny2";
        private const string Size = "1117 KB";
        private const string VideoType = ".mp4";
        private const int Width = 600;
        private const int Height = 450;
        private const string Ratio = "4x3";
    }
}