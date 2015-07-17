using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ImageGallery
{
    /// <summary>
    /// SelectSortedImagesWithChangedThumbnails test class.
    /// </summary>
    [TestClass]
    public class SelectSortedImagesWithChangedThumbnails_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectSortedImagesWithChangedThumbnails
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ImageGallery)]
        public void SelectSortedImagesWithChangedThumbnails()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.currentlyOpenLibrary);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInSortingSelector("Title ASC");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInThumbnailSelector("Small: 240 px width");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInThumbnailSelector("Thumbnail: 120x120 px cropped", false);                  
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyCorrectOrderOfImagesOnBackend(ImageAltText + 1, ImageAltText + 2, ImageAltText + 3);
            for (int k = 0; k <= 2; k++)
            {
                if (k == 0)
                {
                    string src = this.GetImageSource(false, this.imageTitles[k], ImageType, LibraryName, "-small.jpg");
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(this.imageTitles[k], src);
                }
                else
                {
                    string src = this.GetImageSource(false, this.imageTitles[k], ImageType, AnotherImageLibraryTitle, "-small.jpg");
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(this.imageTitles[k], src);
                }
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyCorrectOrderOfImages(ImageAltText + 1, ImageAltText + 2, ImageAltText + 3);

            for (int i = 0; i <= 2; i++)
            {
                if (i == 0)
                {
                    string src = this.GetImageSource(false, this.imageTitles[i], ImageType, LibraryName, "-small.jpg");
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + (i + 1), src);                   
                }
                else
                {
                    string src = this.GetImageSource(false, this.imageTitles[i], ImageType, AnotherImageLibraryTitle, "-small.jpg");
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + (i + 1), src);
                }
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(ImageAltText + 2);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().IsImageTitlePresentOnDetailMasterPage(this.imageTitles[1]));

            var scr = this.GetImageSource(false, this.imageTitles[1], ImageType, AnotherImageLibraryTitle, "-thumbnail.jpg");
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + 2, scr);

            var hrefPrevious = this.GetImageHref(true, this.imageTitles[0], LibraryName);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyPreviousImage(hrefPrevious);

            var hrefNext = this.GetImageHref(true, this.imageTitles[2], AnotherImageLibraryTitle);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyNextImage(hrefNext);

            var hrefBack = "/" + PageName.ToLower() + "/" + "Index/";
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyBackToAllImages(hrefBack);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower() + "/" + AnotherImageLibraryTitle.ToLower());
            for (int j = 1; j <= 3; j++)
            {
                if (j == 1)
                {
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImageIsNotPresent(ImageAltText + j);
                }
                else
                {
                    var src = this.GetImageSource(false, this.imageTitles[j - 1], ImageType, AnotherImageLibraryTitle, "-small.jpg");
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + j, src);
                }
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyCorrectOrderOfImages(ImageAltText + 2, ImageAltText + 3);
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

        private string GetImageSource(bool isBaseUrlIncluded, string imageName, string imageType, string libraryUrl, string imageThumbnailSize = "")
        {
            string imageUrl = imageName.ToLower() + imageType.ToLower() + imageThumbnailSize;
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl.ToLower(), imageUrl, this.BaseUrl);
            return scr;
        }

        private string GetImageHref(bool isBaseUrlIncluded, string imageName, string libraryUrl)
        {
            string imageUrl = imageName.ToLower();
            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl.ToLower(), imageUrl, this.BaseUrl, PageName.ToLower() + "/images");
            return href;
        }

        private const string PageName = "PageWithImage";
        private readonly string[] imageTitles = new string[] { "Image1", "Image2", "Image3" };
        private const string WidgetName = "Image gallery";
        private const string LibraryName = "TestImageLibrary";
        private const string ImageAltText = "AltText_Image";
        private const string ImageType = ".TMB";
        private const string AnotherImageLibraryTitle = "AnotherImageLibrary";
    }
}