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
    public class EditAndChangeSelectedImage_ : FeatherTestCase
    { 
        /// <summary>
        /// UI test EditAndChangeSelectedImage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock3)]
        public void EditAndChangeSelectedImage()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
          
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            ////if (this.Culture != null)
            ////{
            ////    BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectProvider(SecondProviderName);
            ////}
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitForContentToBeLoaded(false);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(ImageName1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsTitlePopulated(ImageName1), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(ImageAltText1), "Image alt text is not populated correctly");
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmMediaProperties();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SelectAllContentInEditableArea();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsTitlePopulated(ImageName1), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(ImageAltText1), "Image alt text is not populated correctly");

            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterTitle(NewImageName);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterImageAltText(NewImageAltText);

            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmMediaProperties();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SelectAllContentInEditableArea();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsTitlePopulated(NewImageName), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(NewImageAltText), "Image alt text is not populated correctly");
            string scr = this.GetImageSource(true, ImageName1, ImageTypeInPropertiesDialog);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifyImageThumbnailInPropertiesDialog(ImageName1, scr);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ChangeMediaFile();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitForContentToBeLoaded(false);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(ImageName2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();

            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsTitlePopulated(NewImageName), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(NewImageAltText), "Image alt text is not populated correctly");
            scr = this.GetImageSource(true, ImageName2, ImageTypeInPropertiesDialog);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifyImageThumbnailInPropertiesDialog(ImageName2, scr);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmMediaProperties();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            scr = this.GetImageSource(false, ImageName2, ImageType);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyImage(NewImageName, NewImageAltText, scr);
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
            currentProviderUrlName = BAT.Arrange(this.TestName).ExecuteArrangement("GetCurrentProviderUrlName").Result.Values["CurrentProviderUrlName"];
            string libraryUrl = LibraryName.ToLower();
            string imageUrl = imageName.ToLower() + imageType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, imageUrl, this.BaseUrl, "images", currentProviderUrlName);
            return scr;
        }

        private const string PageName = "PageWithImage";
        private const string WidgetName = "ContentBlock";      
        private const string LibraryName = "TestImageLibrary";         
        private const string ImageName1 = "Image1";
        private const string ImageName2 = "Image2";
        private const string NewImageName = "ImageTitleEdited";
        private const string ImageAltText1 = "AltText_Image1";
        private const string NewImageAltText = "ImageAltTextEdited";
        private const string ImageType = ".JPG";
        private const string ImageTypeInPropertiesDialog = ".TMB";
        private string currentProviderUrlName;
        private const string SecondProviderName = "SecondSite Libraries";
    }
}