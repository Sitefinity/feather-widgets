using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks.VideoSelector
{
    /// <summary>
    /// This is a test class for content block > video selector tests
    /// </summary>
    [TestClass]
    public class UploadVideoWithCategoryAndTag_ : FeatherTestCase
    { 
        /// <summary>
        /// UI test UploadVideoWithCategoryAndTag
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        //TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void UploadVideoWithCategoryAndTag()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenVideoSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitForContentToBeLoaded(true);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SwitchToUploadMode();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitForContentToBeLoaded(true);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFileFromYourComputer();
            string fullImagesPath = DeploymentDirectory + @"\";
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().PerformSingleFileUpload(FileToUpload, fullImagesPath);

            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().WaitForContentToBeLoaded();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().CancelUpload();

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFileFromYourComputer();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().PerformSingleFileUpload(FileToUpload, fullImagesPath);

            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().WaitForContentToBeLoaded();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().VerifyMediaToUploadSection(FileToUpload, Size);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().ClickSelectLibraryButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(ChildVideoLibrary);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().VerifySelectedLibrary(LibraryName + " > " + ChildVideoLibrary);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().IsMediaFileTitlePopulated(VideoName);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().EnterTitle(NewVideoName);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().ExpandCategoriesAndTagsSection();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().ClickSelectCategoryButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector("Category1");
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().VerifySelectedCategory("Category0 > Category1");
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().AddTag(TagName);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().VerifySelectedTag(TagName);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().UploadMediaFile();

            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifyVideoInfo(NewVideoName, VideoType, Size);
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifySmallVideoProperites(this.GetVideoSource());
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifySelectedOptionAspectRatioSelector("Auto");
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().PlayVideo();
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().VerifyBigVideoProperites(this.GetVideoSource());
            BATFeather.Wrappers().Backend().Media().VideoPropertiesWrapper().ConfirmMediaProperties();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyVideo(this.GetVideoSource());
            BAT.Arrange(this.TestName).ExecuteArrangement("VerifyCreatedTag");
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

        private string GetVideoSource()
        {
            currentProviderUrlName = BAT.Arrange(this.TestName).ExecuteArrangement("GetCurrentProviderUrlName").Result.Values["CurrentProviderUrlName"];
            string libraryUrl = LibraryName.ToLower() + "/" + ChildVideoLibrary.ToLower();
            string imageUrl = VideoNameWithDash.ToLower() + VideoType;
            string url;

            if (this.Culture == null)
            {
                url = this.BaseUrl;
            }
            else
            {
                url = ActiveBrowser.Url.Substring(0, 20);
            }

            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(libraryUrl, imageUrl, "videos", currentProviderUrlName);
            return scr;
        }

        private const string PageName = "PageWithVideo";
        private const string WidgetName = "ContentBlock";      
        private const string LibraryName = "TestVideoLibrary";
        private const string ChildVideoLibrary = "ChildVideoLibrary";
        private const string FileToUpload = "Ogv File.ogv";
        private const string VideoName = "Ogv File";
        private const string NewVideoName = "VideoTitleEdited";
        private const string VideoNameWithDash = "Ogv-File";
        private const string VideoType = ".ogv";
        private const string TagName = "Tag0";
        private const string Size = "428 KB";
        private string currentProviderUrlName;
        private const string SecondProviderName = "SecondSite Libraries";
    }
}