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
    /// Select All Published Documents arrangement class.
    /// </summary>
    public class SelectAllPublishedDocuments : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
           Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);

           ServerOperations.Documents().CreateDocumentLibrary(LibraryTitle);

            ServerSideUpload.UploadDocument(LibraryTitle, DocumentTitle + 1, DocumentResource1);
            ServerSideUpload.UploadDocument(LibraryTitle, DocumentTitle + 2, DocumentResource2);
            ServerSideUpload.UploadDocument(LibraryTitle, DocumentTitle + 3, DocumentResource3);
        }

        /// <summary>
        /// Gets the current libraries provider Url name.
        /// </summary>
        [ServerArrangement]
        public void GetCurrentProviderUrlName()
        {
            // string urlName = ServerOperations.Libraries().GetCurrentProviderUrlName;

            // ServerArrangementContext.GetCurrent().Values.Add("CurrentProviderUrlName", urlName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Libraries().DeleteAllDocumentLibrariesExceptDefaultOne();
        }

        private const string PageName = "PageWithDocument";
        private const string LibraryTitle = "TestDocumentLibrary";
        private const string DocumentTitle = "Image";
        private const string DocumentResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string DocumentResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string DocumentResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
        private const string PageTemplateName = "Bootstrap.default";
    }
}