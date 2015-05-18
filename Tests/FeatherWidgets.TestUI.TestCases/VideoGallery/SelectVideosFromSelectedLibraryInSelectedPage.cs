using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.VideoGallery
{
    /// <summary>
    /// SelectVideosFromSelectedLibraryInSelectedPage test class.
    /// </summary>
    [TestClass]
    public class SelectVideosFromSelectedLibraryInSelectedPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectVideosFromSelectedLibraryInSelectedPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.VideoGallery)]
        public void SelectVideosFromSelectedLibraryInSelectedPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.selectedLibrariesOnly);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(ChildVideoLibrary);        
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(new string[] { LibraryName + " > " + ChildVideoLibrary });

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.existingPage);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(SingleItemPage);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(new string[] { SingleItemPage });
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            for (int i = 1; i <= 4; i++)
            {
                if (i <= 2)
                {
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageIsNotPresent(VideoBaseTitle + i);
                }
                else
                {
                    string src = this.GetVideoSource(false, VideoBaseTitle + i, ImageType);
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(VideoBaseTitle + i, src);
                }
            }
     
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            for (int i = 1; i <= 4; i++)
            {

                if (i <= 2)
                {
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImageIsNotPresent(VideoAltText + i);
                }
                else
                {
                    var src = this.GetVideoSource(false, VideoBaseTitle + i, ImageType);
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(VideoAltText + i, src);
                }
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(VideoAltText + 3);
            var scr = this.GetVideoSource(false, VideoBaseTitle + 3, string.Empty);
            string url = SingleItemPage.ToLower() + scr;
            ActiveBrowser.WaitForUrl("/" + url, true, 60000);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().IsVideoTitlePresentOnDetailMasterPage(VideoBaseTitle + 3));

            scr = this.GetVideoSource(true, VideoBaseTitle + 3, VideoType);
            BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyVideo(scr);          
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
            string libraryUrl = LibraryName.ToLower() + "/" + ChildVideoLibrary.ToLower();
            string imageUrl = videoName.ToLower() + videoType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl, "videos");
            return scr;
        }


        private const string PageName = "PageWithVideo";
        private const string VideoBaseTitle = "Video";
        private const string WidgetName = "Video gallery";
        private const string LibraryName = "TestVideoLibrary";
        private const string VideoAltText = "Video";
        private const string ImageType = ".TMB";
        private const string VideoType = ".webm";
        private const string ChildVideoLibrary = "ChildVideoLibrary";
        private const string SingleItemPage = "TestPage";
    }
}