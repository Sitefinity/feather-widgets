using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.DocumentsList
{
    /// <summary>
    /// SelectSortedDocumentsInTableView test class.
    /// </summary>
    [TestClass]
    public class SelectSortedDocumentsInTableView_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectSortedDocumentsInTableView
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.DocumentsList)]
        public void SelectSortedDocumentsInTableView()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.currentlyOpenLibrary);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInSortingSelector("Title ASC");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectOptionInListTemplateSelector("DocumentsTable");                
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyCorrectOrderOfDocumentsInTableView(DocumentBaseTitle + 1, DocumentBaseTitle + 2, DocumentBaseTitle + 3);
            foreach (var doc in this.documentTitles)
            {
                if (doc.Equals("Document1"))
                {
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyDocumentInTableView(doc, this.GetDocumentHref(true, doc, PageName + "/" + ContentType, LibraryName.ToLower()));
                }
                else 
                {
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyDocumentInTableView(doc, this.GetDocumentHref(true, doc, PageName + "/" + ContentType, AnotherDocumentLibraryTitle.ToLower()));
                }
            }

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyDocumentIconOnTemplate(DocumentType, true);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyCorrectOrderOfDocumentsInTableView(DocumentBaseTitle + 1, DocumentBaseTitle + 2, DocumentBaseTitle + 3);
            foreach (var doc in this.documentTitles)
            {
                if (doc.Equals("Document1"))
                {
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocumentInTableView(doc, this.GetDocumentHref(true, doc, PageName + "/" + ContentType, LibraryName.ToLower()));
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDownloadButton(this.GetDocumentHref(true, doc, ContentType, LibraryName));
                }
                else
                {
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocumentInTableView(doc, this.GetDocumentHref(true, doc, PageName + "/" + ContentType, AnotherDocumentLibraryTitle.ToLower()));
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDownloadButton(this.GetDocumentHref(true, doc, ContentType, AnotherDocumentLibraryTitle));
                }               
            }

            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocumentIconOnTemplate(DocumentType, true);
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().ClickDocument(SelectedDocument);
            ActiveBrowser.WaitForUrl(this.GetDocumentHref(false, SelectedDocument, PageName + "/" + ContentType, AnotherDocumentLibraryTitle), true, 60000);
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().IsDocumentTitlePresentOnDetailMasterPage(SelectedDocument);
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDownloadButton(this.GetDocumentHref(true, SelectedDocument, ContentType, AnotherDocumentLibraryTitle));
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifySizeAndExtensionOnTemplate("(5 KB)", DocumentType);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower() + "/" + AnotherDocumentLibraryTitle.ToLower());
            for (int j = 1; j <= 3; j++)
            {
                if (j == 1)
                {
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocumentIsNotPresent(DocumentBaseTitle + j);
                }
                else
                {
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocumentInTableView(DocumentBaseTitle + j, this.GetDocumentHref(true, DocumentBaseTitle + j, PageName + "/" + ContentType, AnotherDocumentLibraryTitle.ToLower()));
                }
            }

            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyCorrectOrderOfDocumentsInTableView(DocumentBaseTitle + 2, DocumentBaseTitle + 3);
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

        private string GetDocumentHref(bool isBaseUrlIncluded, string documentName, string contentType, string libraryUrl)
        {
            string documentUrl = documentName.ToLower();
            string href = BATFeather.Wrappers().Frontend().CommonWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, documentUrl, this.BaseUrl, contentType);
            return href;
        }

        private const string PageName = "PageWithDocument";
        private readonly string[] documentTitles = new string[] { "Document1", "Document2", "Document3" };
        private const string WidgetName = "Documents list";
        private const string LibraryName = "TestDocumentLibrary";
        private const string DocumentBaseTitle = "Document";
        private const string AnotherDocumentLibraryTitle = "AnotherDocumentLibrary";
        private const string ContentType = "docs";
        private const string DocumentType = "jpg";
        private const string SelectedDocument = "Document3";
    }
}