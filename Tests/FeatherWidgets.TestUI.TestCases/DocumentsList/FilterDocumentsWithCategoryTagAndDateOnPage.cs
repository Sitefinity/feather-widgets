using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        Owner(FeatherTeams.SitefinityTeam4),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.DocumentsList)]
        public void FilterDocumentsWithCategoryTagAndDateOnPage()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ExpandNarrowSelectionByArrow();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.filterItems);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(DateName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButtonByDate();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectDisplayItemsPublishedIn(DisplayItemsPublishedIn);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetFromDateByTyping(DayAgo);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("10", true);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("0", true);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SetToDateByDatePicker(DayForward);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddHour("13", false);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().AddMinute("10", false);
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
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyDocumentIsNotPresent(DocumentBaseTitle + i);
                }
                else
                {
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyDocument(DocumentBaseTitle + i, this.GetDocumentHref(DocumentBaseTitle + i, PageName.ToLower() + "/" + ContentType));
                }
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);

            for (int i = 1; i <= 4; i++)
            {
                if (i <= 2 || i > 3)
                {
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocumentIsNotPresent(DocumentBaseTitle + i);
                }
                else
                {
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocument(DocumentBaseTitle + i, this.GetDocumentHref(DocumentBaseTitle + i, PageName.ToLower() + "/" + ContentType));
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDownloadButton(this.GetDownloadHref(DocumentBaseTitle + i, ContentType));              
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
            currentProviderUrlName = BAT.Arrange(this.TestName).ExecuteArrangement("GetCurrentProviderUrlName").Result.Values["CurrentProviderUrlName"];
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private string GetDocumentHref(string documentName, string contentType)
        {
            string libraryUrl = LibraryName.ToLower();
            string documentUrl = documentName.ToLower();
            string url;

            if (this.Culture == null)
            {
                url = this.BaseUrl;
            }
            else
            {
                Uri uri = new Uri(ActiveBrowser.Url);
                url = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port + "/";
            }

            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(libraryUrl, documentUrl, contentType, currentProviderUrlName, this.Culture);
            return href;
        }

        private string GetDownloadHref(string documentName, string contentType)
        {
            string libraryUrl = LibraryName.ToLower();
            string documentUrl = documentName.ToLower();
            string url;

            if (this.Culture == null)
            {
                url = this.BaseUrl;
            }
            else
            {
                Uri uri = new Uri(ActiveBrowser.Url);
                url = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port + "/";
            }

            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetDownloadButtonSource(libraryUrl, documentUrl, contentType, currentProviderUrlName);
            return href;
        }

        private const string DisplayItemsPublishedIn = "Custom range...";
        private const string PageName = "PageWithDocument";
        private const string DocumentBaseTitle = "Document";
        private const string WidgetName = "Documents list";
        private const string LibraryName = "TestDocumentLibrary";
        private const string DateName = "sfPublicationDateInput";
        private const int DayAgo = -1;
        private const int DayForward = 1;
        private const string CategoryTitle = "Category3";
        private const string TagTitle = "Tag3";
        private const string TaxonomyCategory = "Category";
        private const string TaxonomyTags = "Tags";
        private const string ContentType = "docs";
        private string currentProviderUrlName;
    }
}