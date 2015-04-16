using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks.VideoSelector
{
    /// <summary>
    /// This is a test class for content block > video selector tests
    /// </summary>
    [TestClass]
    public class InsertVideoFromAlreadyUploaded_ : FeatherTestCase
    {
        /// <summary>
        /// UI test InsertVideoFromAlreadyUploaded
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void InsertVideoFromAlreadyUploaded()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenVideoSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyNoMediaEmptyScreen("No videos");
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().PressCancelButton();

            // Uploading document after epmty screen is verified.
            string documentId = BAT.Arrange(this.TestName).ExecuteArrangement("UploadVideo").Result.Values["videoId"];

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenVideoSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifySelectedFilter(SelectedFilterName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyMediaTooltip(VideoName, LibraryName, DocumentType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(VideoName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().IsTitlePopulated(VideoName), "Document title is not populated correctly");
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().VerifyDocumentLink(VideoName, this.GetDocumentHref(true));
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().EnterTitle(NewDocumentName);
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().ConfirmMediaProperties();

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper()
                .VerifyContentBlockDocumentDesignMode(this.GetDocumentHref(true), this.GetSfRef(documentId), NewDocumentName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifyDocument(NewDocumentName, this.GetDocumentHref(false));
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

        private string GetSfRef(string documentId)
        {
            return "[documents|OpenAccessDataProvider]" + documentId;
        }

        private string GetDocumentHref(bool isBaseUrlIncluded)
        {
            string libraryUrl = LibraryName.ToLower();
            string documentUrl = VideoName.ToLower() + DocumentType.ToLower();
            string href = BATFeather.Wrappers().Frontend().CommonWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, documentUrl, this.BaseUrl, "docs");
            return href;
        }

        private const string PageName = "PageWithDocument";
        private const string WidgetName = "ContentBlock";
        private const string VideoName = "Image1";
        private const string LibraryName = "TestDocumentLibrary";
        private const string DocumentType = ".JPG";
        private const string SelectedFilterName = "Recent Documents";
        private const string NewDocumentName = "DocumentTitleEdited";
    }
}
