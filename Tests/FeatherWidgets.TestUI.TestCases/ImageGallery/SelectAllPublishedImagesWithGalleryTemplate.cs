using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.ImageGallery;

namespace FeatherWidgets.TestUI.TestCases.News
{
    /// <summary>
    /// SelectAllPublishedImagesWithGalleryTemplate test class.
    /// </summary>
    [TestClass]
    public class SelectAllPublishedImagesWithGalleryTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectAllPublishedImagesWithOverlayGalleryTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ImageGallery)]
        public void SelectAllPublishedImagesWithOverlayGalleryTemplate()
        {
            this.SelectListTemplateInImageGalleryDesigner(BootstrapTemplate, OverlayGalleryTemplate);

            foreach (var image in this.imageTitles)
            {
                string src = this.GetImageSource(false, image, ImageType);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyImageThumbnail(image, src);
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
            var scr = this.GetImageSource(false, this.imageTitles[1], "");
            string url = PageName.ToLower() + scr + "?itemIndex=1";
            ActiveBrowser.WaitForUrl("/" + url, true, 60000);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().CloseSelectedImageOverlayTemplate();
            ActiveBrowser.WaitForUrl("/" + PageName.ToLower(), true, 60000);
        }

        /// <summary>
        /// UI test SelectAllPublishedImagesWithThumbnailStripTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ImageGallery)]
        public void SelectAllPublishedImagesWithThumbnailStripTemplate()
        {
            this.SelectListTemplateInImageGalleryDesigner(FoundationTemplate, ThumbnailStripTemplate);

            string src = this.GetImageSource(true, this.imageTitles[0], ImageOriginalType);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyImageThumbnail(this.imageTitles[0], src);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyThumbnailStripTemplateInfo("1of 3", this.imageTitles[0]);

            for (int j = 1; j <= 2; j++)
            {
                src = this.GetImageSource(false, this.imageTitles[j], ImageType);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyImageThumbnail(this.imageTitles[j], src);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyThumbnailStripTemplateInfo("1of 3", this.imageTitles[0]);
            src = this.GetImageSource(true, this.imageTitles[0], ImageOriginalType);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + 3, src);

            for (int i = 1; i <= 2; i++)
            {
                src = this.GetImageSource(false, this.imageTitles[i], ImageType);
                BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + (3 - i), src);
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(ImageAltText + 2);

            src = this.GetImageSource(true, this.imageTitles[1], ImageOriginalType);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + 2, src);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyThumbnailStripTemplateInfo("2of 3", this.imageTitles[1]);
            var scr = this.GetImageSource(false, this.imageTitles[1], "");
            string url = PageName.ToLower() + scr + "?itemIndex=1";
            ActiveBrowser.WaitForUrl("/" + url, true, 60000);
        }

        /// <summary>
        /// UI test SelectAllPublishedImagesWithSimpleListTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ImageGallery)]
        public void SelectAllPublishedImagesWithSimpleListTemplate()
        {
            this.SelectListTemplateInImageGalleryDesigner(SemanticUITemplate, SimpleListTemplate);

            foreach (var image in this.imageTitles)
            {
                string src = this.GetImageSource(false, image, ImageType);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyImageThumbnail(image, src);
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
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(ArrangementClassName).ExecuteTearDown();
        }

        private void SelectListTemplateInImageGalleryDesigner(string pageTemplate, string listTemplate)
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(ArrangementClassName).AddParameter("templateName", pageTemplate).ExecuteSetUp();

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().ImageGallery().ImageGalleryWrapper().SelectOptionInListTemplateSelector(listTemplate);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
        }

        private string GetImageSource(bool isBaseUrlIncluded, string imageName, string imageType)
        {
            string libraryUrl = LibraryName.ToLower();
            string imageUrl = imageName.ToLower() + imageType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().CommonWrapper().GetImageSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl);
            return scr;
        }

        private const string BootstrapTemplate = "Bootstrap.default";
        private const string FoundationTemplate = "Foundation.default";
        private const string SemanticUITemplate = "SemanticUI.default";
        private const string ArrangementClassName = "SelectAllPublishedImagesWithGalleryTemplate";
        private const string PageName = "PageWithImage";
        private readonly string[] imageTitles = new string[] { "Image3", "Image2", "Image1" };
        private const string WidgetName = "Image gallery";
        private const string LibraryName = "TestImageLibrary";
        private const string ImageAltText = "AltText_Image";
        private const string ImageType = ".TMB";
        private const string ImageOriginalType = ".JPG";
        private const string OverlayGalleryTemplate = "OverlayGallery";
        private const string ThumbnailStripTemplate = "ThumbnailStrip";
        private const string SimpleListTemplate = "SimpleList";
    }
}