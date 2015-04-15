using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ImageGallery
{
    /// <summary>
    /// SelectAllPublishedDocuments test class.
    /// </summary>
    [TestClass]
    public class SelectAllPublishedDocuments_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectAllPublishedDocuments
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ImageGallery)]
        public void SelectAllPublishedDocuments()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.allPublished);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ExpandNarrowSelectionByArrow();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.allItems);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.usePaging);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.samePage);           
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            foreach (var doc in this.documentTitles)
            {
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyDocument(doc, this.GetDocumentHref(true, doc, PageName + "/" + ContentType));
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyDocumentIconOnTemplate(DocumentType);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            foreach (var doc in this.documentTitles)
            {
                BATFeather.Wrappers().Frontend().DocumentList().DocumentListWrapper().VerifyDocument(doc, this.GetDocumentHref(true, doc, PageName + "/" + ContentType));
                BATFeather.Wrappers().Frontend().DocumentList().DocumentListWrapper().VerifyDocumentIconOnTemplate(DocumentType);
                BATFeather.Wrappers().Frontend().DocumentList().DocumentListWrapper().VerifyCorrectOrderOfDocuments(this.documentTitles);
                BATFeather.Wrappers().Frontend().DocumentList().DocumentListWrapper().VerifyDownloadButton(this.GetDocumentHref(true, doc, ContentType));              
            }

            BATFeather.Wrappers().Frontend().DocumentList().DocumentListWrapper().ClickDocument(SelectedDocument);
            ActiveBrowser.WaitForUrl(this.GetDocumentHref(false, SelectedDocument, PageName + "/" + ContentType), true, 60000);
            BATFeather.Wrappers().Frontend().DocumentList().DocumentListWrapper().IsDocumentTitlePresentOnDetailMasterPage(SelectedDocument);
            BATFeather.Wrappers().Frontend().DocumentList().DocumentListWrapper().VerifyDownloadButton(this.GetDocumentHref(true, SelectedDocument, ContentType));
            BATFeather.Wrappers().Frontend().DocumentList().DocumentListWrapper().VerifySizeAndExtension("5 KB", "(" + DocumentType + ")");
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

        private const string PageName = "PageWithDocument";
        private readonly string[] documentTitles = new string[] { SelectedDocument, "Image2", "Image1" };
        private const string WidgetName = "Documents list";
        private const string LibraryName = "TestDocumentLibrary";
        private const string DocumentType = "jpg";
        private const string SelectedDocument = "Image3";
        private const string ContentType = "docs";
    }
}