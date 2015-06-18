using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks.DocumentSelector
{
    /// <summary>
    /// This is a test class for content block > document selector tests
    /// </summary>
    [TestClass]
    public class CheckNavigationInDocumentSelector_ : FeatherTestCase
    {     
        /// <summary>
        /// UI test CheckNavigationInDocumentSelector
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void CheckNavigationInDocumentSelector()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
          
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenDocumentSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifySelectedFilter(SelectedFilterName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(3, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(DocumentName1, DocumentName2, DocumentName3);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFilter(MyDocuments);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(2, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(DocumentName1, DocumentName2);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFilter(AllLibraries);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(0, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(DefaultLibrary, LibraryName);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolder(LibraryName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(DocumentName1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(ChildLibrary);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolder(ChildLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(DocumentName2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(NextChildLibrary);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolder(NextChildLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(DocumentName3);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolderFromBreadCrumb(ChildLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(DocumentName2);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectFolders(NextChildLibrary);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectFolderFromSideBar(NextChildLibrary);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfFolders(0);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(DocumentName3);

            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(DocumentName3, true);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().IsTitlePopulated(DocumentName3), "Document title is not populated correctly");
            //// BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().ConfirmMediaProperties();
            //// This is workaround fix for a bug!
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().ConfirmMediaPropertiesDocsTemporary();
        
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
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private void VerifyFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            string libraryUrl = LibraryName.ToLower() + "/" + ChildLibrary.ToLower() + "/" + NextChildLibrary.ToLower();
            string imageUrl = DocumentName3.ToLower() + DocumentType.ToLower();
            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(false, libraryUrl, imageUrl, this.BaseUrl, MediaType);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyDocument(DocumentName3, href);
        }

        private const string PageName = "PageWithDocument";
        private const string WidgetName = "ContentBlock";
        private const string LibraryName = "TestDocumentLibrary";
        private const string SelectedFilterName = "Recent Documents";
        private const string DocumentName1 = "Document1";
        private const string DocumentName2 = "Document2";
        private const string DocumentName3 = "Document3";
        private const string DocumentType = ".JPG";
        private const string ChildLibrary = "ChildDocumentLibrary";
        private const string NextChildLibrary = "NextChildDocumentLibrary";
        private const string DefaultLibrary = "Default Library";
        private const string MyDocuments = "My Documents";
        private const string AllLibraries = "All Libraries";
        private const string MediaType = "docs";
    }
}