using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.DocumentsList
{
    /// <summary>
    /// FilterDocumentsWithCategoryTagAndDateOnPage test class.
    /// </summary>
    [TestClass]
    public class FilterDocumentsWithCategoryTagAndDateOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test FilterDocumentsWithCategoryTagAndDateOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.DocumentsList)]
        public void FilterDocumentsWithCategoryTagAndDateOnPage()
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
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyDocumentIsNotPresent(DocumentBaseTitle + i);
                }
                else
                {
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyDocument(DocumentBaseTitle + i, this.GetDocumentHref(true, DocumentBaseTitle + i, PageName.ToLower() + "/" + ContentType));
                }
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            for (int i = 1; i <= 4; i++)
            {
                if (i <= 2 || i > 3)
                {
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocumentIsNotPresent(DocumentBaseTitle + i);
                }
                else
                {
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocument(DocumentBaseTitle + i, this.GetDocumentHref(true, DocumentBaseTitle + i, PageName.ToLower() + "/" + ContentType));
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDownloadButton(this.GetDocumentHref(true, DocumentBaseTitle + i, ContentType));              
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

        private string GetDocumentHref(bool isBaseUrlIncluded, string documentName, string contentType)
        {
            string libraryUrl = LibraryName.ToLower();
            string documentUrl = documentName.ToLower();
            string href = BATFeather.Wrappers().Frontend().CommonWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, documentUrl, this.BaseUrl, contentType);
            return href;
        }

        private const string DisplayItemsPublishedIn = "Custom range...";
        private const string PageName = "PageWithDocument";
        private const string DocumentBaseTitle = "Document";
        private const string WidgetName = "Documents list";
        private const string LibraryName = "TestDocumentLibrary";
        private const string DateName = "dateInput";
        private const int DayAgo = -1;
        private const int DayForward = 1;
        private const string CategoryTitle = "Category3";
        private const string TagTitle = "Tag3";
        private const string TaxonomyCategory = "Category";
        private const string TaxonomyTags = "Tags";
        private const string ContentType = "docs";
    }
}