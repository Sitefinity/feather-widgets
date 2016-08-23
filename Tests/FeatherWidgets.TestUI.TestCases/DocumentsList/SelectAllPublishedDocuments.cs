using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.DocumentsList
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
        Owner(FeatherTeams.SitefinityTeam4),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.DocumentsList)]
        public void SelectAllPublishedDocuments()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.AllPublished);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ExpandNarrowSelectionByArrow();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.AllItemsExpander);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.UsePaging);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToSingleItemSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.SamePage);           
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            foreach (var doc in this.documentTitles)
            {
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyDocument(doc, this.GetDocumentHref(doc, PageName + "/" + ContentType));
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyDocumentIconOnTemplate(DocumentType);
            }

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);

            foreach (var doc in this.documentTitles)
            {
                BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocument(doc, this.GetDocumentHref(doc, PageName + "/" + ContentType));
                BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDownloadButton(this.GetDownloadHref(doc, ContentType));              
            }

            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyCorrectOrderOfDocuments(this.documentTitles);
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDocumentIconOnTemplate(DocumentType);

            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().ClickDocument(SelectedDocument);
            ActiveBrowser.WaitForUrl(this.GetDocumentHref(SelectedDocument, PageName + "/" + ContentType), true, 60000);
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().IsDocumentTitlePresentOnDetailMasterPage(SelectedDocument);
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifyDownloadButton(this.GetDownloadHref(SelectedDocument, ContentType));              
            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().VerifySizeAndExtensionOnTemplate("5 KB", "(" + DocumentType + ")");
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
                url = ActiveBrowser.Url.Substring(0, 20);
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
                url = ActiveBrowser.Url.Substring(0, 20);
            }

            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetDownloadButtonSource(libraryUrl, documentUrl, contentType, currentProviderUrlName);
            return href;
        }

        private const string PageName = "PageWithDocument";
        private readonly string[] documentTitles = new string[] { "Image3", "Image2", "Image1" };
        private const string WidgetName = "Documents list";
        private const string LibraryName = "TestDocumentLibrary";
        private const string DocumentType = "jpg";
        private const string SelectedDocument = "Image3";
        private const string ContentType = "docs";
        private string currentProviderUrlName;
    }
}