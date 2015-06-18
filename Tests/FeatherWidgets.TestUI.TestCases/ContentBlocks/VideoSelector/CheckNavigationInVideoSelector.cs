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
    public class CheckNavigationInVideoSelector_ : FeatherTestCase
    {     
        /// <summary>
        /// UI test CheckNavigationInVideoSelector
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void CheckNavigationInVideoSelector()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
          
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenVideoSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifySelectedFilter(SelectedFilterName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(3, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(VideoName1, VideoName2, VideoName3);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFilter(MyVideos);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(2, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(VideoName1, VideoName2);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFilter(AllLibraries);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(0, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(DefaultLibrary, LibraryName);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolder(LibraryName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(VideoName1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(ChildLibrary);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolder(ChildLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(VideoName2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(NextChildLibrary);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolder(NextChildLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(VideoName3);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolderFromBreadCrumb(ChildLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(VideoName2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(NextChildLibrary);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolderFromSideBar(NextChildLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(VideoName3);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(VideoName3, false);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifyVideoInfo(VideoName3, VideoType, Size);
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().ConfirmMediaProperties();
        
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            this.VerifyFrontend();
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

        private void VerifyFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            string libraryUrl = LibraryName.ToLower() + "/" + ChildLibrary.ToLower() + "/" + NextChildLibrary.ToLower();
            string imageUrl = VideoName3.ToLower() + VideoType;
            string src = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(false, libraryUrl, imageUrl, this.BaseUrl, "videos");
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyVideo(src);
        }

        private const string PageName = "PageWithVideo";
        private const string WidgetName = "ContentBlock";
        private const string LibraryName = "TestVideoLibrary";
        private const string SelectedFilterName = "Recent Videos";
        private const string VideoName1 = "Video1";
        private const string VideoName2 = "Video2";
        private const string VideoName3 = "Video3";
        private const string VideoType = ".ogv";
        private const string ChildLibrary = "ChildVideoLibrary";
        private const string NextChildLibrary = "NextChildVideoLibrary";
        private const string DefaultLibrary = "Default Library";
        private const string MyVideos = "My Videos";
        private const string AllLibraries = "All Libraries";
        private const string MediaType = "videos";
        private const string Size = "428 KB";
    }
}