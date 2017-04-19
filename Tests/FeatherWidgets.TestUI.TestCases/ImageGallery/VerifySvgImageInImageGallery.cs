using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.ImageGallery
{
    /// <summary>
    /// Upload and verify svg in MVC Image Gallery and in frontend 
    /// </summary>
    [TestClass]
    public class VerifySvgImageInImageGallery_ : FeatherTestCase
    {
         /// <summary>
        /// UI test 
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam8),
            TestCategory(FeatherTestCategories.PagesAndContent),
            TestCategory(FeatherTestCategories.ImageGallery)]
        public void VerifySvgImageInImageGallery()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInListTemplateSelector("OverlayGallery");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInThumbnailSelector("Thumbnail: 36x36 px");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInThumbnailSelector("Thumbnail: 120x120 px cropped", false);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            for (int k = 2; k >= 0; k--)
            {
                var src = this.GetImageSource(imageTitles[k], ImageType);
                BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(imageTitles[k], src);
                BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImageThumbnail(src, Width, Height);
            }
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
        private const string WidgetName = "Image gallery";
        private const string ImageType = ".svg";
        private readonly string[] imageTitles = new string[] { "Image1", "Image2", "Image3" };
        private const string Image1Name = "Image1";
        private const string Image2Name = "Image2";
        private const string Image3Name = "Image3";
        private const string LibraryName = "TestImageLibrary";
        private const string ChildImageLibrary = "ChildImageLibrary";
        private const string Width = "36";
        private const string Height = "36";
        private string currentProviderUrlName;
    }
}
