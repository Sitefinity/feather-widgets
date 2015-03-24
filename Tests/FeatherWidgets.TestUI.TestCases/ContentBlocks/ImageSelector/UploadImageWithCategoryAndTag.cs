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
    public class UploadImageWithCategoryAndTag_ : FeatherTestCase
    { 
        /// <summary>
        /// UI test UploadImageWithCategoryAndTag
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.ImageSelector),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void UploadImageWithCategoryAndTag()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().WaitForContentToBeLoaded(true);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().SwitchToUploadMode();
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().WaitForContentToBeLoaded(true);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().SelectImageFromYourComputer();
            string fullImagesPath = DeploymentDirectory + @"\";
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().PerformSingleFileUpload(FileToUpload, fullImagesPath);

            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().WaitForContentToBeLoaded();
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().CancelImageUpload();

            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().SelectImageFromYourComputer();
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().PerformSingleFileUpload(FileToUpload, fullImagesPath);

            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().WaitForContentToBeLoaded();
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().VerifyImageToUploadSection(FileToUpload, "6 KB");
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().ClickSelectLibraryButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(ChildImageLibrary);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().VerifySelectedLibrary(LibraryName + " > " + ChildImageLibrary);
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().IsImageTitlePopulated(ImageName);
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().EnterImageTitle(NewImageName);
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().EnterImageAltText(NewImageAltText);
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().ExpandCategoriesAndTagsSection();
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().ClickSelectCategoryButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector("Category1");
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().VerifySelectedCategory("Category0 > Category1");
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().AddTag("Tag0");
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().VerifySelectedTag("Tag0");
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().UploadImage();

            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageTitlePopulated(NewImageName), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(NewImageAltText), "Image alt text is not populated correctly");
            string scr = this.GetImageSource(true, NewImageName, ImageTypeInPropertiesDialog);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifyImageInPropertiesDialog(NewImageName, scr);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifySelectedOptionThumbnailSelector(ThumbnailOption);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmImageProperties();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            scr = this.GetImageSource(false, NewImageName, ImageType);
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifyImage(NewImageName, NewImageAltText, scr);
        }

        private string GetImageSource(bool isBaseUrlIncluded, string imageName, string imageType)
        {
            string libraryUrl = LibraryName.ToLower() + "/" + ChildImageLibrary.ToLower();
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
        private const string WidgetName = "ContentBlock";      
        private const string LibraryName = "TestImageLibrary";
        private const string ChildImageLibrary = "ChildImageLibrary";
        private const string FileToUpload = "IMG01648.jpg";
        private const string ImageName = "IMG01648";
        private const string NewImageName = "ImageTitleEdited";
        private const string NewImageAltText = "ImageAltTextEdited";
        private const string ImageType = ".JPG";
        private const string ImageTypeInPropertiesDialog = ".TMB";
        private const string ThumbnailOption = "Original size: 320x214 px";
    }
}