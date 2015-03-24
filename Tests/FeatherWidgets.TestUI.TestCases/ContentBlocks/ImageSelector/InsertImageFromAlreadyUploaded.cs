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
    /// This is a test class for content block > image selector tests
    /// </summary>
    [TestClass]
    public class InsertImageFromAlreadyUploaded_ : FeatherTestCase
    {
        /// <summary>
        /// UI test InsertImageFromAlreadyUploaded
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.ImageSelector),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void InsertImageFromAlreadyUploaded()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().VerifyNoImagesEmptyScreen();
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().PressCancelButton();

            // Uploading image after epmty screen is verified.
            string imageId = BAT.Arrange(this.TestName).ExecuteArrangement("UploadImage").Result.Values["imageId"];

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().VerifySelectedFilter(SelectedFilterName);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().VerifyImageTooltip(ImageName, LibraryName, ImageDimensions, ImageType);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().SelectImage(ImageName);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().ConfirmImageSelection();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageTitlePopulated(ImageName), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(ImageAltText), "Image alt text is not populated correctly"); 
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterImageTitle(NewImageName);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterImageAltText(NewImageAltText);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifySelectedOptionThumbnailSelector(ThumbnailOption);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmImageProperties();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper()
                .VerifyContentBlockImageDesignMode(this.GetImageSource(true), this.GetSfRef(imageId), NewImageName, NewImageAltText);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifyImage(NewImageName, NewImageAltText, this.GetImageSource(false));
        }

        private string GetImageSource(bool isBaseUrlIncluded)
        {
            string libraryUrl = LibraryName.ToLower();
            string imageUrl = ImageName.ToLower() + ImageType.ToLower();
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

        private string GetSfRef(string imageId)
        {
            return "[images|OpenAccessDataProvider]" + imageId;
        }

        private const string PageName = "PageWithImage";
        private const string WidgetName = "ContentBlock";
        private const string ImageName = "Image1";
        private const string ImageAltText = "AltText_Image1";
        private const string LibraryName = "TestImageLibrary";
        private const string ImageDimensions = "300x300";
        private const string ThumbnailOption = "Original size: 300x300 px";
        private const string ImageType = ".JPG";
        private const string SelectedFilterName = "Recent Images";
        private const string NewImageName = "ImageTitleEdited";
        private const string NewImageAltText = "ImageAltTextEdited";
    }
}
