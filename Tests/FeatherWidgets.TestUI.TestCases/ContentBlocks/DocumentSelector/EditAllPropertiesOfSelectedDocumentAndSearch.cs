using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

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
        Owner(FeatherTeams.SitefinityTeam7),
        //TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void EditAllPropertiesOfSelectedDocumentAndSearch()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().OpenDocumentSelector();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitForContentToBeLoaded(false);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(DocumentName1, true);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelection();
            Assert.IsTrue(BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().IsTitlePopulated(DocumentName1), "Document title is not populated correctly");
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().EditAllProperties();
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().EnterNewTitleInPropertiesDialogAndPublish(DocumentNewName);
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().ChangeMediaFile();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SearchInMediaSelector(DocumentName1);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyNoItemsFoundMessage();
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(0, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SearchInMediaSelector(DocumentNewName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitCorrectCountOfMediaFiles(1, MediaType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyCorrectMediaFiles(DocumentNewName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().PressCancelButton();
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
            currentProviderUrlName = BAT.Arrange(EditAndChangeSelectedDocumentArrangement).ExecuteArrangement("GetCurrentProviderUrlName").Result.Values["CurrentProviderUrlName"];
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            string libraryUrl = LibraryName.ToLower();
            string documentUrl = DocumentName1.ToLower() + DocumentType.ToLower();
            string url;

            if (this.Culture == null)
            {
                url = this.BaseUrl;
            }
            else
            {
                url = ActiveBrowser.Url.Substring(0, 20);
            }

            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(libraryUrl, documentUrl, "docs", currentProviderUrlName);
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyDocument(DocumentNewName, href);
        }

        private const string PageName = "PageWithDocument";
        private const string WidgetName = "ContentBlock";      
        private const string LibraryName = "TestDocumentLibrary";         
        private const string DocumentName1 = "Image1";
        private const string DocumentNewName = "NewTitle1";
        private const string DocumentType = ".JPG";
        private const string EditAndChangeSelectedDocumentArrangement = "EditAndChangeSelectedDocument";
        private const string MediaType = "docs";
        private string currentProviderUrlName;
        private const string SecondProviderName = "SecondSite Libraries";
    }
}