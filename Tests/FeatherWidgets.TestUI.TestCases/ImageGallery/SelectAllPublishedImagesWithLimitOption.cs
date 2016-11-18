﻿using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.ImageGallery
{
    /// <summary>
    /// SelectAllPublishedImagesWithLimitOption test class.
    /// </summary>
    [TestClass]
    public class SelectAllPublishedImagesWithLimitOption_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectAllPublishedImagesWithLimitOption
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam1),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ImageGallery)]
        public void SelectAllPublishedImagesWithLimitOption()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.AllPublished);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ExpandNarrowSelectionByArrow();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.AllItemsExpander);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.UsePaging);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.UseLimit);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ChangePagingOrLimitValue("3", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.SamePage);           
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            foreach (var image in this.imageTitles)
            {
                string src = this.GetImageSource(image, ImageType);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(image, src);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            int i = 3;
            foreach (var image in this.imageTitles)
            {
                var src = this.GetImageSource(image, ImageType);
                BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + i, src);
                i--;
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(ImageAltText + 2);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().IsImageTitlePresentOnDetailMasterPage(this.imageTitles[1]));

            var scr = this.GetImageSource(this.imageTitles[1], ImageTypeFrontend);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + 2, scr);

            var hrefPrevious = this.GetImageHref(this.imageTitles[0]);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyPreviousImage(hrefPrevious);

            var hrefNext = this.GetImageHref(this.imageTitles[2]);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyNextImage(hrefNext);

            var hrefBack = "/" + PageName.ToLower() + "/" + "Index/";
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyBackToAllImages(hrefBack);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
            currentProviderUrlName = BAT.Arrange(this.TestName).ExecuteArrangement("GetCurrentProviderUrlName").Result.Values["CurrentProviderUrlName"];
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private string GetImageSource(string imageName, string imageType)
        {
            string libraryUrl = LibraryName.ToLower();
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
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(libraryUrl, imageUrl, ContentType, currentProviderUrlName, this.Culture);
            return scr;
        }

        private string GetImageHref(string imageName)
        {
            string libraryUrl = LibraryName.ToLower();
            string imageUrl = imageName.ToLower();
            string url;
            if (this.Culture == null)
            {
                url = this.BaseUrl;
            }
            else
            {
                url = ActiveBrowser.Url.Substring(0, 20);
            }

            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(libraryUrl, imageUrl, PageName.ToLower() + "/images", currentProviderUrlName, this.Culture);
            return href;
        }

        private const string PageName = "PageWithImage";
        private readonly string[] imageTitles = new string[] { "Image3", "Image2", "Image1" };
        private const string WidgetName = "Image gallery";
        private const string LibraryName = "TestImageLibrary";
        private const string ImageAltText = "AltText_Image";
        private const string ImageType = ".TMB";
        private const string ImageTypeFrontend = ".JPG";
        private string currentProviderUrlName;
        private const string ContentType = "images";
    }
}