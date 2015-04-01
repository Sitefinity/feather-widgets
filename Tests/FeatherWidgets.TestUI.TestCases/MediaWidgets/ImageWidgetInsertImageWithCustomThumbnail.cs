using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks.ImageSelector
{
    /// <summary>
    /// This is a test class for ImageWidgetInsertImageWithCustomThumbnail tests
    /// </summary>
    [TestClass]
    public class ImageWidgetInsertImageWithCustomThumbnail_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ImageWidgetInsertImageWithCustomThumbnail
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.ImageSelector),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void ImageWidgetInsertImageWithCustomThumbnail()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName, 0 , true);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().WaitForContentToBeLoaded(false);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().SelectImage(ImageName);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().ConfirmImageSelectionInImageWidget();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageTitlePopulated(ImageName), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(ImageAltText1), "Image alt text is not populated correctly");
            string scr = this.GetImageSource(true, ImageName, ImageTypeInPropertiesDialog);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifyImageThumbnailInPropertiesDialog(ImageName, scr);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifySelectedOptionThumbnailSelector(ThumbnailOption);

            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterImageTitle(NewImageName);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterImageAltText(NewImageAltText);

            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmImagePropertiesInImageWidget();

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName, 0, true);
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageTitlePopulated(NewImageName), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(NewImageAltText), "Image alt text is not populated correctly");
            scr = this.GetImageSource(true, ImageName, ImageTypeInPropertiesDialog);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifyImageThumbnailInPropertiesDialog(ImageName, scr);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().SelectOptionThumbnailSelector("Custom size...");
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().SelectResizeImageOption("Crop to area");
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().SelectResizeImageOption("Resize to area");
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifyWidthIsRequiredMessage();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifyHeightIsRequiredMessage();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterWidth("111");
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterHeight("111");
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().SelectQualityOption("Medium");
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().DoneResizing();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifySelectedOptionThumbnailSelector(NewThumbnailOption);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ClickThisImageIsALink();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmImagePropertiesInImageWidget();

            string src = this.GetImageSource(false, ImageName, ImageType);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyImageThumbnail(NewImageAltText, src);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyImageResizingProperties(NewImageAltText, "MaxWidth=111", "MaxHeight=111", "Quality=Medium" , "Method=ResizeFitToAreaArguments");

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            scr = this.GetImageSource(false, ImageName, ImageType);
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifyImage(NewImageName, NewImageAltText, scr);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(NewImageAltText);
            ActiveBrowser.WaitForUrl(scr + "?sfvrsn=0", true, 10000);
        }

        private string GetImageSource(bool isBaseUrlIncluded, string imageName, string imageType)
        {
            string libraryUrl = LibraryName.ToLower();
            string imageUrl = imageName.ToLower() + imageType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().CommonWrapper().GetImageSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl);
            return scr;
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

        private const string PageName = "PageWithImage";
        private const string WidgetName = "Image";
        private const string LibraryName = "TestImageLibrary";
        private const string ImageName = "Image1";
        private const string NewImageName = "ImageTitleEdited";
        private const string NewImageAltText = "ImageAltTextEdited";
        private const string ImageAltText1 = "AltText_Image1";
        private const string ImageType = ".JPG";
        private const string ImageTypeInPropertiesDialog = ".TMB";
        private const string ThumbnailOption = "Original size: 300x300 px";
        private const string NewThumbnailOption = "Custom size: 111x111 px";
    }
}
