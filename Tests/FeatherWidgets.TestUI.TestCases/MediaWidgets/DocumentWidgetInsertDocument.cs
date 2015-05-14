using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.MediaWidgets
{
    /// <summary>
    /// This is a test class for DocumentWidgetInsertDocument tests
    /// </summary>
    [TestClass]
    public class DocumentWidgetInsertDocument_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ImageWidgetInsertImageWithCustomThumbnail
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.MediaSelector),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void DocumentWidgetInsertDocument()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName, 0, true);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().WaitForContentToBeLoaded(false);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifySelectedFilter(SelectedFilterName);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().VerifyMediaTooltip(DocumentName, LibraryName, DocumentType);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().SelectMediaFile(DocumentName, true);
            BATFeather.Wrappers().Backend().Media().MediaSelectorWrapper().ConfirmMediaFileSelectionInWidget();
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().VerifyDocumentLink(DocumentName, this.GetDocumentHref(true));
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().VerifyDocumentIcon(DocumentExtension);
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().VerifyTemplateDropdownValueInWidget("DocumentLink");
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().ApplyCssClasses(CssClassesToApply);
            BATFeather.Wrappers().Backend().Media().DocumentPropertiesWrapper().ConfirmMediaPropertiesInWidget();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyDocument(DocumentName, this.GetDocumentHref(false));
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyDocumentIconOnTemplate(DocumentExtension);

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyDocumentFromWidget(DocumentName, this.GetDocumentHref(false));
            BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().VerifyDocumentCssClass(CssClassesToApply, DocumentName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorMediaWrapper().VerifyDocumentIconOnTemplate(DocumentExtension);
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

        private string GetDocumentHref(bool isBaseUrlIncluded)
        {
            string libraryUrl = LibraryName.ToLower();
            string documentUrl = DocumentName.ToLower() + DocumentType.ToLower();
            string href = BATFeather.Wrappers().Frontend().MediaWidgets().MediaWidgetsWrapper().GetMediaSource(isBaseUrlIncluded, libraryUrl, documentUrl, this.BaseUrl, "docs");
            return href;
        }

        private const string PageName = "PageWithDocument";
        private const string WidgetName = "Document link";
        private const string LibraryName = "TestDocumentLibrary";
        private const string DocumentName = "Document1";
        private const string DocumentType = ".DOCX";
        private const string SelectedFilterName = "Recent Documents";
        private const string CssClassesToApply = "testCssClass";
        private const string DocumentExtension = "docx";
    }
}
