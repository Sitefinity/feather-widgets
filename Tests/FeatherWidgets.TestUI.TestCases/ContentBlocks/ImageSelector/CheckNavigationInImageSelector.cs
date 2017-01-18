using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks.ImageSelector
{
    /// <summary>
    /// This is a test class for content block > image selector tests
    /// </summary>
    [TestClass]
    public class CheckNavigationInImageSelector_ : FeatherTestCase
    {     
        /// <summary>
        /// UI test CheckNavigationInImageSelector
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void CheckNavigationInImageSelector()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
          
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifySelectedFilter(SelectedFilterName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(3);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(ImageName1, ImageName2, ImageName3);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFilter(MyImages);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(ImageName1, ImageName2);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFilter(AllLibraries);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(DefaultLibrary, LibraryName);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolder(LibraryName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(ImageName1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(ChildImageLibrary);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolder(ChildImageLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(ImageName2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(NextChildImageLibrary);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolder(NextChildImageLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(ImageName3);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolderFromBreadCrumb(ChildImageLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(ImageName2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(NextChildImageLibrary);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolderFromSideBar(NextChildImageLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(ImageName3);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(ImageName3);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsTitlePopulated(ImageName3), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(ImageAltText), "Image alt text is not populated correctly");
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmMediaProperties();
        
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
            currentProviderUrlName = BAT.Arrange(this.TestName).ExecuteArrangement("GetCurrentProviderUrlName").Result.Values["CurrentProviderUrlName"];
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            string libraryUrl = LibraryName.ToLower() + "/" + ChildImageLibrary.ToLower() + "/" + NextChildImageLibrary.ToLower();
            string imageUrl = ImageName3.ToLower() + ImageType.ToLower();
            string url;

            if (this.Culture == null)
            {
                url = this.BaseUrl;
            }
            else
            {
                url = ActiveBrowser.Url.Substring(0, 20);
            }

            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(libraryUrl, imageUrl, "images", currentProviderUrlName);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyImage(ImageName3, ImageAltText, scr);
        }

        private const string PageName = "PageWithImage";
        private const string WidgetName = "ContentBlock";      
        private const string LibraryName = "TestImageLibrary";         
        private const string SelectedFilterName = "Recent Images";
        private const string ImageName1 = "Image1";
        private const string ImageName2 = "Image2";
        private const string ImageName3 = "Image3";
        private const string ImageAltText = "AltText_Image3";
        private const string ImageType = ".JPG";
        private const string ChildImageLibrary = "ChildImageLibrary";
        private const string NextChildImageLibrary = "NextChildImageLibrary";
        private const string DefaultLibrary = "Default Library";
        private const string MyImages = "My Images";
        private const string AllLibraries = "All Libraries";
        private string currentProviderUrlName;
        private const string SecondProviderName = "SecondSite Libraries";
    }
}