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
    public class EditAllPropertiesOfSelectedDocumentAndSearch_ : FeatherTestCase
    { 
        /// <summary>
        /// UI test EditAllPropertiesOfSelectedDocumentAndSearch
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void EditAllPropertiesOfSelectedDocumentAndSearch()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenDocumentSelector();

            BATFeather.Wrappers().Backend().Media().DocumentSelectorWrapper().WaitForContentToBeLoaded(false);
            BATFeather.Wrappers().Backend().Media().DocumentSelectorWrapper().SelectDocument(DocumentName1);
            BATFeather.Wrappers().Backend().Media().DocumentSelectorWrapper().ConfirmMediaFileSelection();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().IsTitlePopulated(DocumentName1), "Document title is not populated correctly");
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().EditAllProperties();
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().EnterNewTitleInPropertiesDialogAndPublish(DocumentNewName);
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().ChangeMediaFile();
            BATFeather.Wrappers().Backend().Media().DocumentSelectorWrapper().SearchInMediaSelector(DocumentName1);
            BATFeather.Wrappers().Backend().Media().DocumentSelectorWrapper().VerifyNoItemsFoundMessage();
            BATFeather.Wrappers().Backend().Media().DocumentSelectorWrapper().WaitCorrectCountOfMediaFiles(0);
            BATFeather.Wrappers().Backend().Media().DocumentSelectorWrapper().SearchInMediaSelector(DocumentNewName);
            BATFeather.Wrappers().Backend().Media().DocumentSelectorWrapper().WaitCorrectCountOfMediaFiles(1);
            BATFeather.Wrappers().Backend().Media().DocumentSelectorWrapper().VerifyCorrectDocuments(DocumentNewName);
            BATFeather.Wrappers().Backend().Media().DocumentSelectorWrapper().PressCancelButton();
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().ConfirmMediaProperties();
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
            BAT.Arrange(EditAndChangeSelectedDocumentArrangement).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(EditAndChangeSelectedDocumentArrangement).ExecuteTearDown();
        }

        private void VerifyFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            string libraryUrl = LibraryName.ToLower();
            string documentUrl = DocumentName1.ToLower() + DocumentType.ToLower();
            string href = BATFeather.Wrappers().Frontend().CommonWrapper().GetMediaSource(false, libraryUrl, documentUrl, this.BaseUrl, "docs");
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifyDocument(DocumentNewName, href);
        }

        private const string PageName = "PageWithDocument";
        private const string WidgetName = "ContentBlock";      
        private const string LibraryName = "TestDocumentLibrary";         
        private const string DocumentName1 = "Image1";
        private const string DocumentNewName = "NewTitle1";
        private const string DocumentType = ".JPG";
        private const string EditAndChangeSelectedDocumentArrangement = "EditAndChangeSelectedDocument";
    }
}