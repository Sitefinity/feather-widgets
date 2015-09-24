﻿using System;
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
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock3)]
        public void InsertImageFromAlreadyUploaded()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().PressCancelButton();

            // Uploading image after epmty screen is verified.
            string imageId = BAT.Arrange(this.TestName).ExecuteArrangement("UploadImage").Result.Values["imageId"];

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            ////if (this.Culture != null)
            ////{
            ////    BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectProvider(SecondProviderName);
            ////}
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifySelectedFilter(SelectedFilterName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyMediaTooltip(ImageName, LibraryName, ImageType, ImageDimensions);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(ImageName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsTitlePopulated(ImageName), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(ImageAltText), "Image alt text is not populated correctly"); 
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterTitle(NewImageName);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterImageAltText(NewImageAltText);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifySelectedOptionThumbnailSelector(ThumbnailOption);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmMediaProperties();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper()
                .VerifyContentBlockImageDesignMode(this.GetImageSource(true), this.GetSfRef(imageId), NewImageName, NewImageAltText);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyImage(NewImageName, NewImageAltText, this.GetImageSource(false));
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
            return "[images|" + currentProviderUrlName + "]" + imageId;
        }

        private string GetImageSource(bool isBaseUrlIncluded)
        {
            currentProviderUrlName = BAT.Arrange(this.TestName).ExecuteArrangement("GetCurrentProviderUrlName").Result.Values["CurrentProviderUrlName"];
            string libraryUrl = LibraryName.ToLower();
            string imageUrl = ImageName.ToLower() + ImageType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl, "images", currentProviderUrlName);
            return scr;
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
        private string currentProviderUrlName;
        private const string SecondProviderName = "SecondSite Libraries";
    }
}
