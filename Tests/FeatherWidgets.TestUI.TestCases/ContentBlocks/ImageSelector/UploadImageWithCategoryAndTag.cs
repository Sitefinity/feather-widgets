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
        TestCategory(FeatherTestCategories.MediaSelector),
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
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().SelectMediaFileFromYourComputer();
            string fullImagesPath = DeploymentDirectory + @"\";
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().PerformSingleFileUpload(FileToUpload, fullImagesPath);

            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().WaitForContentToBeLoaded();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().CancelUpload();

            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().SelectMediaFileFromYourComputer();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().PerformSingleFileUpload(FileToUpload, fullImagesPath);

            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().WaitForContentToBeLoaded();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().VerifyMediaToUploadSection(FileToUpload, "6 KB");
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().ClickSelectLibraryButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(ChildImageLibrary);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().VerifySelectedLibrary(LibraryName + " > " + ChildImageLibrary);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().IsMediaFileTitlePopulated(ImageName);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().EnterTitle(NewImageName);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().EnterImageAltText(NewImageAltText);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().ExpandCategoriesAndTagsSection();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().ClickSelectCategoryButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector("Category1");
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().VerifySelectedCategory("Category0 > Category1");
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().AddTag(TagName);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().VerifySelectedTag(TagName);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().UploadMediaFile();

            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsTitlePopulated(NewImageName), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(NewImageAltText), "Image alt text is not populated correctly");
            string scr = this.GetImageSource(true, NewImageName, ImageTypeInPropertiesDialog);

            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifyImageThumbnailInPropertiesDialog(NewImageName, scr);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifySelectedOptionThumbnailSelector(ThumbnailOption);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmMediaProperties();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            scr = this.GetImageSource(false, NewImageName, ImageType);
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifyImage(NewImageName, NewImageAltText, scr);
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

        private string GetImageSource(bool isBaseUrlIncluded, string imageName, string imageType)
        {
            string libraryUrl = LibraryName.ToLower() + "/" + ChildImageLibrary.ToLower();
            string imageUrl = imageName.ToLower() + imageType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().CommonWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl);
            return scr;
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
        private const string TagName = "Tag0";
    }
}