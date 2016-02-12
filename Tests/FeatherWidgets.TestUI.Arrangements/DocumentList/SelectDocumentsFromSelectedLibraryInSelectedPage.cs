using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Select Documents From Selected Library In Selected Page arrangement class.
    /// </summary>
    public class SelectDocumentsFromSelectedLibraryInSelectedPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddDocumentsListWidgetToPage(page1Id);

            Guid singleItemPageId = ServerOperations.Pages().CreatePage(SingleItemPage);
            ServerOperationsFeather.Pages().AddDocumentsListWidgetToPage(singleItemPageId);

            var parentId = ServerOperations.Documents().CreateLibrary(DocumentLibraryTitle);
            var childId = ServerOperations.Documents().CreateFolder(ChildLibraryTitle, parentId);

            ServerOperations.Documents().Upload(DocumentLibraryTitle, DocumentTitle + 1, ImageResource1);
            ServerOperations.Documents().Upload(DocumentLibraryTitle, DocumentTitle + 4, ImageResource4);
            ServerOperationsFeather.MediaOperations().UploadDocumentInFolder(childId, DocumentTitle + 3, ImageResource3);
            ServerOperationsFeather.MediaOperations().UploadDocumentInFolder(childId, DocumentTitle + 2, ImageResource2);
        }

        /// <summary>
        /// Gets the current libraries provider Url name.
        /// </summary>
        [ServerArrangement]
        public void GetCurrentProviderUrlName()
        {
            string urlName = ServerOperations.Media().GetCurrentProviderUrlName;

            ServerArrangementContext.GetCurrent().Values.Add("CurrentProviderUrlName", urlName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Documents().DeleteAllLibrariesExceptDefaultOne();
        }

        private const string PageName = "PageWithDocument";
        private const string SingleItemPage = "TestPage";
        private const string DocumentLibraryTitle = "TestDocumentLibrary";
        private const string DocumentTitle = "Document";
        private const string ChildLibraryTitle = "ChildDocumentLibrary";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "FeatherWidgets.TestUtilities.Data.MediaFiles.2.jpg";
        private const string ImageResource3 = "FeatherWidgets.TestUtilities.Data.MediaFiles.3.jpg";
        private const string ImageResource4 = "Telerik.Sitefinity.TestUtilities.Data.Images.4.jpg";
    }
}