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
    public class EditAllPropertiesOfSelectedVideoAndSearch_ : FeatherTestCase
    { 
        /// <summary>
        /// UI test EditAllPropertiesOfSelectedVideoAndSearch
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void EditAllPropertiesOfSelectedVideoAndSearch()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenVideoSelector();

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitForContentToBeLoaded(false);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(VideoName1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();           
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().EditAllProperties();
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().EnterNewTitleInPropertiesDialogAndPublish(VideoNewName);
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifyVideoInfo(VideoNewName, VideoType, Size);
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifySmallVideoProperites(this.GetVideoSource(true));

            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().ChangeMediaFile();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SearchInMediaSelector(VideoName1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyNoItemsFoundMessage();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(0, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SearchInMediaSelector(VideoNewName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(VideoNewName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().PressCancelButton();
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().ConfirmMediaProperties();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifyVideo(this.GetVideoSource(false));

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

        private string GetVideoSource(bool isBaseUrlIncluded)
        {
            string libraryUrl = LibraryName.ToLower();
            string videoUrl = VideoName1.ToLower() + VideoType;
            string scr = BATFeather.Wrappers().Frontend().CommonWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, videoUrl, this.BaseUrl, "videos");
            return scr;
        }

        private const string PageName = "PageWithVideo";
        private const string WidgetName = "ContentBlock";      
        private const string LibraryName = "TestVideoLibrary";
        private const string VideoName1 = "big_buck_bunny1";
        private const string VideoNewName = "NewTitle1";
        private const string VideoType = ".mp4";
        private const string Size = "1117 KB";
        private const string MediaType = "videos";
    }
}