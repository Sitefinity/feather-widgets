using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Images
{
    /// <summary>
    /// Upload and verify svg in MVC Image widget and in frontend 
    /// </summary>
    [TestClass]
    public class VerifySvgImageInImageWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test 
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
            TestCategory(FeatherTestCategories.PagesAndContent),
            TestCategory(FeatherTestCategories.ImageGallery)]
        public void VerifySvgImageInImageWidget()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditImageWidget(WidgetName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFileFromYourComputer();
            string fullImagesPath = DeploymentDirectory + @"\";
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().PerformSingleFileUpload(FileToUpload, fullImagesPath);

            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().WaitForContentToBeLoaded();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().CancelUpload();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFileFromYourComputer();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().PerformSingleFileUpload(FileToUpload, fullImagesPath);

            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().WaitForContentToBeLoaded();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().VerifyMediaToUploadSection(FileToUpload, "3 KB");
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().ClickSelectLibraryButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(ChildImageLibrary);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().VerifySelectedLibrary(LibraryName + " > " + ChildImageLibrary);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().IsMediaFileTitlePopulated(ImageName);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().EnterTitle(NewImageName);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().EnterImageAltText(NewImageAltText);
            BATFeather.Wrappers().Backend().Media().MediaUploadPropertiesWrapper().UploadMediaFile();

            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifyImageThumbnailOptionsForSVG(2);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().SelectOptionThumbnailSelector(ImageThumbnailOption1);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterWidth("150");
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterHeight("150");
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmCustomThumbnailSize();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            var src = this.GetImageSource(ImageName, ImageType);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyImage(NewImageName, NewImageAltText, src);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImageThumbnail(src, Width, Height);
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
        /// <summary>
        /// Get image source
        /// </summary>
        /// <param name="imageName">Image name</param>
        /// <param name="imageType">Image type</param>
        /// <returns>Returns image src</returns>
        private string GetImageSource(string imageName, string imageType)
        {
            currentProviderUrlName = BAT.Arrange(this.TestName).ExecuteArrangement("GetCurrentProviderUrlName").Result.Values["CurrentProviderUrlName"];
            string libraryUrl = LibraryName.ToLower() + "/" + ChildImageLibrary.ToLower();
            string imageUrl = imageName.ToLower() + imageType.ToLower();
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
            return scr;
        }

        private const string PageName = "PageWithSvgImage";
        private readonly string[] pageToSelect = new string[] { "PageWithSvgImage" };
        private const string WidgetName = "Image";
        private const string LibraryName = "TestImageLibrary";
        private const string ChildImageLibrary = "ChildImageLibrary";
        private const string FileToUpload = "kiwi.svg";
        private const string ImageName = "kiwi";
        private const string ImageType = ".svg";
        private const string Width = "150";
        private const string Height = "150";
        private const string NewImageName = "ImageSvgTitleEdited";
        private const string NewImageAltText = "ImageSvgAltTextEdited";
        private const string ImageThumbnailOption1 = "Custom size...";
        private string currentProviderUrlName;
    }
}
