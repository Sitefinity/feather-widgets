﻿using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.DocumentsList
{
    /// <summary>
    /// SelectDocumentsFromSelectedLibraryInSelectedPage test class.
    /// </summary>
    [TestClass]
    public class SelectDocumentsFromSelectedLibraryInSelectedPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectDocumentsFromSelectedLibraryInSelectedPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.DocumentsList)]
        public void SelectDocumentsFromSelectedLibraryInSelectedPage()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.selectedLibrariesOnly);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(ChildDocumentLibrary);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(new string[] { LibraryName + " > " + ChildDocumentLibrary });

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.existingPage);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInHierarchicalSelector(SingleItemPage);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromHierarchicalSelector(new string[] { SingleItemPage });
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            for (int i = 1; i <= 4; i++)
            {
                if (i == 1 || i == 4)
                {
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyDocumentIsNotPresent(DocumentBaseTitle + i);
                }
                else
                {
                    BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyDocument(DocumentBaseTitle + i, this.GetDocumentHref(true, DocumentBaseTitle + i, SingleItemPage.ToLower() + "/" + ContentType));
                }
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);

            for (int i = 1; i <= 4; i++)
            {
                if (i == 1 || i == 4)
                {
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocumentIsNotPresent(DocumentBaseTitle + i);
                }
                else
                {
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocument(DocumentBaseTitle + i, this.GetDocumentHref(true, DocumentBaseTitle + i, SingleItemPage.ToLower() + "/" + ContentType));
                    BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDownloadButton(this.GetDownloadHref(true, DocumentBaseTitle + i, ContentType));
                }
            }

            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().ClickDocument(SelectedDocument);
            ActiveBrowser.WaitForUrl(this.GetDocumentHref(false, SelectedDocument, SingleItemPage.ToLower() + "/" + ContentType), true, 60000);
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().IsDocumentTitlePresentOnDetailMasterPage(SelectedDocument);
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDownloadButton(this.GetDownloadHref(true, SelectedDocument, ContentType));
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifySizeOnHybridPage("5 KB");
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

        private string GetDocumentHref(bool isBaseUrlIncluded, string documentName, string contentType)
        {
            string libraryUrl = LibraryName.ToLower() + "/" + ChildDocumentLibrary.ToLower();
            string documentUrl = documentName.ToLower();
            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, documentUrl, this.BaseUrl, contentType, currentProviderUrlName, this.Culture);
            return href;
        }

        private string GetDownloadHref(bool isBaseUrlIncluded, string documentName, string contentType)
        {
            string libraryUrl = LibraryName.ToLower() + "/" + ChildDocumentLibrary.ToLower();
            string documentUrl = documentName.ToLower();
            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetDownloadButtonSource(isBaseUrlIncluded, libraryUrl, documentUrl, this.BaseUrl, contentType, currentProviderUrlName);
            return href;
        }

        private const string PageName = "PageWithDocument";
        private const string WidgetName = "Documents list";
        private const string DocumentBaseTitle = "Document";
        private const string LibraryName = "TestDocumentLibrary";
        private const string ChildDocumentLibrary = "ChildDocumentLibrary";
        private const string SingleItemPage = "TestPage";
        private const string ContentType = "docs";
        private const string SelectedDocument = "Document3";
        private string currentProviderUrlName;
    }
}