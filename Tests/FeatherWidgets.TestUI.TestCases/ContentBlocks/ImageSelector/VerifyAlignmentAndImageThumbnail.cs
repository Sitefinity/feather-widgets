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
    public class VerifyAlignmentAndImageThumbnail_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyAlignmentAndImageThumbnail
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void VerifyAlignmentAndImageThumbnail()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            //// image 1
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(ImageName1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().SelectLeftAlignmentOption();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().SelectOptionThumbnailSelector(ImageThumbnailOption1);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifySelectedOptionThumbnailSelector(ImageThumbnailOption1);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmMediaProperties();

            //// image 2
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(ImageName2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().SelectCenterAlignmentOption();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().SelectOptionThumbnailSelector(ImageThumbnailOption2);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifySelectedOptionThumbnailSelector(ImageThumbnailOption2);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmMediaProperties();

            //// image 3
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenImageSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(ImageName3);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().SelectRightAlignmentOption();
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().SelectOptionThumbnailSelector(ImageThumbnailOption3);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().VerifySelectedOptionThumbnailSelector(ImageThumbnailOption3);
            BATFeather.Wrappers().Backend().Media().ImagePropertiesWrapper().ConfirmMediaProperties();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyImageStyle(ImageStyle1, ImageName1, ImageAltText1);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyImageStyle(ImageStyle2, ImageName2, ImageAltText2);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyImageStyle(ImageStyle3, ImageName3, ImageAltText3);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyImageThumbnail(ImageThumbnailExtension1, ImageName1, ImageAltText1);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyImageThumbnail(ImageThumbnailExtension2, ImageName2, ImageAltText2);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyImageThumbnail(ImageThumbnailExtension3, ImageName3, ImageAltText3);
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

        private const string PageName = "PageWithImage";
        private const string WidgetName = "ContentBlock";
        private const string ImageName1 = "Image1";
        private const string ImageName2 = "Image2";
        private const string ImageName3 = "Image3";
        private const string ImageAltText1 = "AltText_Image1";
        private const string ImageAltText2 = "AltText_Image2";
        private const string ImageAltText3 = "AltText_Image3";
        private const string ImageThumbnailOption1 = "Small: 240 px width";
        private const string ImageThumbnailOption2 = "Thumbnail: 36x36 px";
        private const string ImageThumbnailOption3 = "Thumbnail: 120x120 px cropped";
        private const string ImageStyle1 = "float: left;";
        private const string ImageStyle2 = "margin-right: auto; margin-left: auto; display: block;";
        private const string ImageStyle3 = "float: right;";
        private const string ImageThumbnailExtension1 = "-small.jpg";
        private const string ImageThumbnailExtension2 = "-thumb36.jpg";
        private const string ImageThumbnailExtension3 = "-thumbnail.jpg";
        private const string LibraryName = "TestImageLibrary";
        private const string ImageType = ".tmb";
    }
}
