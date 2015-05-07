using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ImageGallery
{
    /// <summary>
    /// SelectImagesFromSelectedLibraryInSelectedPage test class.
    /// </summary>
    [TestClass]
    public class SelectImagesFromSelectedLibraryInSelectedPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectImagesFromSelectedLibraryInSelectedPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ImageGallery)]
        public void SelectImagesFromSelectedLibraryInSelectedPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.selectedLibrariesOnly);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(ChildImageLibrary);        
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(new string[] { LibraryName + " > " + ChildImageLibrary });

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.existingPage);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(SingleItemPage);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(new string[] { SingleItemPage });
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            for (int i = 1; i <= 4; i++)
            {
                if (i <= 2)
                {
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyImageIsNotPresent(ImageBaseTitle + i);
                }
                else
                {
                    string src = this.GetImageSource(false, ImageBaseTitle + i, ImageType);
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyImageThumbnail(ImageBaseTitle + i, src);
                }
            }
     
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            for (int i = 1; i <= 4; i++)
            {
                if (i <= 2)
                {
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImageIsNotPresent(ImageAltText + i);
                }
                else
                {
                    var src = this.GetImageSource(false, ImageBaseTitle + i, ImageType);
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + i, src);
                }
            }

            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(ImageAltText + 3);
            var scr = this.GetImageSource(false, ImageBaseTitle + 3, string.Empty);
            string url = SingleItemPage.ToLower() + scr;
            ActiveBrowser.WaitForUrl("/" + url, true, 60000);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().IsImageTitlePresentOnDetailMasterPage(ImageBaseTitle + 3));

            scr = this.GetImageSource(false, ImageBaseTitle + 3, ImageTypeFrontend);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(ImageAltText + 3, scr);          
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
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl);
            return scr;
        }

        private string GetImageHref(bool isBaseUrlIncluded, string imageName)
        {
            string libraryUrl = LibraryName.ToLower() + "/" + ChildImageLibrary.ToLower();
            string imageUrl = imageName.ToLower();
            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl, PageName.ToLower() + "/images");
            return href;
        }

        private const string PageName = "PageWithImage";
        private const string ImageBaseTitle = "Image";
        private const string WidgetName = "Image gallery";
        private const string LibraryName = "TestImageLibrary";
        private const string ImageAltText = "AltTextImage";
        private const string ImageType = ".TMB";
        private const string ImageTypeFrontend = ".JPG";
        private const string ChildImageLibrary = "ChildImageLibrary";
        private const string SingleItemPage = "TestPage";
    }
}