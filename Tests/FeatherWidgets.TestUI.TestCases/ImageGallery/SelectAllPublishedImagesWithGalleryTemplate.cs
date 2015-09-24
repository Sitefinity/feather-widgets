﻿
using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ImageGallery
{
    /// <summary>
    /// SelectAllPublishedImagesWithGalleryTemplate test class.
    /// </summary>
    [TestClass]
    public class SelectAllPublishedImagesWithGalleryTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectAllPublishedImagesWithOverlayGalleryTemplate_Bootstrap
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ImageGallery)]
        public void SelectAllPublishedImagesWithOverlayGalleryTemplate_Bootstrap()
        {
            this.SelectListTemplateInImageGalleryDesigner(BootstrapTemplate, OverlayGalleryTemplate);

            foreach (var image in this.imageTitles)
            {
                string src = this.GetImageSource(false, image, ImageType);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(image, src);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
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
        /// UI test SelectAllPublishedImagesWithThumbnailStripTemplate_Foundation
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ImageGallery)]
        public void SelectAllPublishedImagesWithThumbnailStripTemplate_Foundation()
        {
            this.SelectListTemplateInImageGalleryDesigner(FoundationTemplate, ThumbnailStripTemplate);

            string src = this.GetImageSource(false, this.imageTitles[0], ImageOriginalType);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(this.imageTitles[0], src);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyThumbnailStripTemplateInfo("1of 3", this.imageTitles[0]);

            for (int j = 1; j <= 2; j++)
            {
                src = this.GetImageSource(false, this.imageTitles[j], ImageType);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(this.imageTitles[j], src);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyThumbnailStripTemplateInfo("1of 3", this.imageTitles[0]);
            src = this.GetImageSource(false, this.imageTitles[0], ImageOriginalType);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + 3, src);

            for (int i = 1; i <= 2; i++)
            {
                src = this.GetImageSource(false, this.imageTitles[i], ImageType);
                BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + (3 - i), src);
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(ImageAltText + 2);

            src = this.GetImageSource(false, this.imageTitles[1], ImageOriginalType);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + 2, src);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyThumbnailStripTemplateInfo("2of 3", this.imageTitles[1]);
            var scr = this.GetImageSource(false, this.imageTitles[1], string.Empty);
            string url = PageName.ToLower() + scr;
            ActiveBrowser.WaitForUrl("/" + url, true, 60000);
        }

        /// <summary>
        /// UI test SelectAllPublishedImagesWithSimpleListTemplateSemantics
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ImageGallery)]
        public void SelectAllPublishedImagesWithSimpleListTemplate_Semantics()
        {
            this.SelectListTemplateInImageGalleryDesigner(SemanticUITemplate, SimpleListTemplate);

            foreach (var image in this.imageTitles)
            {
                string src = this.GetImageSource(false, image, ImageType);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(image, src);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
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
            currentProviderUrlName = BAT.Arrange(ArrangementClassName).ExecuteArrangement("GetCurrentProviderUrlName").Result.Values["CurrentProviderUrlName"];

            BAT.Macros().NavigateTo().Pages(this.Culture);
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
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl, ContentType, currentProviderUrlName, this.Culture);
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
        private string currentProviderUrlName;
        private const string ContentType = "images";
    }
}