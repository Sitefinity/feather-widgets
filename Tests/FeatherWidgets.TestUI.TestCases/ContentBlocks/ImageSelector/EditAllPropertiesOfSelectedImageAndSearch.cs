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
    public class EditAllPropertiesOfSelectedImageAndSearch_ : FeatherTestCase
    { 
        /// <summary>
        /// UI test EditAllPropertiesOfSelectedImageAndSearch
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void EditAllPropertiesOfSelectedImageAndSearch()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
          
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();

            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().WaitForContentToBeLoaded(false);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().SelectImage(ImageName);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().ConfirmMediaFileSelection();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsTitlePopulated(ImageName), "Image title is not populated correctly");
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().IsImageAltTextPopulated(ImageAltText1), "Image alt text is not populated correctly");
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EditAllProperties();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().EnterNewTitleInPropertiesDialogAndPublish(ImageNewName);            
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ChangeMediaFile();
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().SearchInMediaSelector(ImageName);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().VerifyNoItemsFoundMessage();
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().WaitCorrectCountOfMediaFiles(0);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().SearchInMediaSelector(ImageNewName);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().WaitCorrectCountOfMediaFiles(1);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().VerifyCorrectImages(ImageNewName);
            BATFeather.Wrappers().Backend().Media().ImageSelectorWrapper().PressCancelButton();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmMediaProperties();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            this.VerifyFrontend();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(EditAndChangeSelectedImageArrangement).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(EditAndChangeSelectedImageArrangement).ExecuteTearDown();
        }

        private void VerifyFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            string libraryUrl = LibraryName.ToLower();
            string imageUrl = ImageName.ToLower() + ImageType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().CommonWrapper().GetMediaSource(false, libraryUrl, imageUrl, this.BaseUrl);
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifyImage(ImageNewName, ImageAltText1, scr);
        }

        private const string PageName = "PageWithImage";
        private const string WidgetName = "ContentBlock";      
        private const string LibraryName = "TestImageLibrary";         
        private const string ImageName = "Image1";
        private const string ImageNewName = "NewTitle1";
        private const string ImageAltText1 = "AltText_Image1";
        private const string ImageType = ".JPG";
        private const string EditAndChangeSelectedImageArrangement = "EditAndChangeSelectedImage";
    }
}