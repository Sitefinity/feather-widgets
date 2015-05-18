using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.VideoGallery
{
    /// <summary>
    /// FilterVideosWithCategoryTagAndDateOnPage test class.
    /// </summary>
    [TestClass]
    public class FilterVideosWithCategoryTagAndDateOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test FilterVideosWithCategoryTagAndDateOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.VideoGallery)]
        public void FilterVideosWithCategoryTagAndDateOnPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ExpandNarrowSelectionByArrow();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.filterItems);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(DateName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButtonByDate();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectDisplayItemsPublishedIn(DisplayItemsPublishedIn);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetFromDateByTyping(DayAgo);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("10", true);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("2", true);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetToDateByDatePicker(DayForward);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("13", false);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("4", false);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(TaxonomyTags);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(4);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(TagTitle);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(TaxonomyCategory);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(1);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(CategoryTitle);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            for (int i = 1; i <= 4; i++)
            {
                if (i <= 2 || i > 3)
                {
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageIsNotPresent(VideoBaseTitle + i);
                }
                else
                {
                    string src = this.GetVideoSource(false, VideoBaseTitle + i, ImageType);
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyImageThumbnail(VideoBaseTitle + i, src);
                }
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            for (int i = 1; i <= 4; i++)
            {
                if (i <= 2 || i > 3)
                {
                    BATFeather.Wrappers().Frontend().VideoGallery().VideoGalleryWrapper().VerifyVideoIsNotPresent(VideoAltText + i);
                }
                else
                {
                    var src = this.GetVideoSource(false, VideoBaseTitle + i, VideoType);
                    BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImage(VideoAltText + i, src);
                }
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

        private string GetVideoSource(bool isBaseUrlIncluded, string videoName, string videoType)
        {
            string libraryUrl = LibraryName.ToLower();
            string videoUrl = videoName.ToLower() + videoType.ToLower();
            string scr = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, videoUrl, this.BaseUrl, "videos");
            return scr;
        }

        private const string DisplayItemsPublishedIn = "Custom range...";
        private const string PageName = "PageWithVideo";
        private const string VideoBaseTitle = "Video";
        private const string WidgetName = "Video gallery";
        private const string LibraryName = "TestVideoLibrary";
        private const string VideoAltText = "Video";
        private const string VideoType = ".TMB";
        private const string DateName = "dateInput";
        private const int DayAgo = -1;
        private const int DayForward = 1;
        private const string CategoryTitle = "Category3";
        private const string TagTitle = "Tag3";
        private const string TaxonomyCategory = "Category";
        private const string TaxonomyTags = "Tags";
        private const string ImageType = ".TMB";
    }
}