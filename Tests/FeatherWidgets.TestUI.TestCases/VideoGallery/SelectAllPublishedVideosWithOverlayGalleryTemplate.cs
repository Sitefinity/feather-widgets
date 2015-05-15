using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.VideoGallery
{
    /// <summary>
    /// SelectAllPublishedVideosWithOverlayGalleryTemplate test class.
    /// </summary>
    [TestClass]
    public class SelectAllPublishedVideosWithOverlayGalleryTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectAllPublishedVideosWithOverlayGalleryTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.VideoGallery)]
        public void SelectAllPublishedVideosWithOverlayGalleryTemplate()
        {
            this.SelectListTemplateInImageGalleryDesigner(BootstrapTemplate, OverlayGalleryTemplate);

            foreach (var image in this.imageTitles)
            {
                string src = this.GetImageSource(false, image, ImageType);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(image, src);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            int i = 3;
            foreach (var image in this.imageTitles)
            {
                var src = this.GetImageSource(false, image, ImageType);
                BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + i, src);
                i--;
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(ImageAltText + 2);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifySelectedImageOverlayTemplate(ImageAltText + 2);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyPreviousAndNextImageArrowsOverlayTemplate();
            var scr = this.GetImageSource(false, this.imageTitles[1], string.Empty);
            string url = PageName.ToLower() + scr + "?itemIndex=1";
            ActiveBrowser.WaitForUrl("/" + url, true, 60000);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().CloseSelectedImageOverlayTemplate();
            ActiveBrowser.WaitForUrl("/" + PageName.ToLower(), true, 60000);
        }
    
        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private void SelectListTemplateInImageGalleryDesigner(string pageTemplate, string listTemplate)
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).AddParameter("templateName", pageTemplate).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInListTemplateSelector(listTemplate);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
        }

        private string GetImageSource(bool isBaseUrlIncluded, string imageName, string imageType)
        {
            string libraryUrl = LibraryName.ToLower();
            string imageUrl = imageName.ToLower() + imageType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl);
            return scr;
        }

        private const string BootstrapTemplate = "Bootstrap.default";
        private const string PageName = "PageWithImage";
        private readonly string[] imageTitles = new string[] { "Image3", "Image2", "Image1" };
        private const string WidgetName = "Image gallery";
        private const string LibraryName = "TestImageLibrary";
        private const string ImageAltText = "AltText_Image";
        private const string ImageType = ".TMB";
        private const string OverlayGalleryTemplate = "OverlayGallery";
    }
}