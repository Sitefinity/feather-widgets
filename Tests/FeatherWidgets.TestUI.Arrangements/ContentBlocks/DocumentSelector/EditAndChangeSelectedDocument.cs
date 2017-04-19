using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Edit And Change Selected Document arrangement class.
    /// </summary>
    public class EditAndChangeSelectedDocument : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);

            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId, string.Empty, PlaceHolderId);

            ServerOperations.Documents().CreateLibrary(LibraryTitle);
            ServerOperations.Documents().Upload(LibraryTitle, DocumentTitle1, DocumentResource1);

            ServerOperations.Documents().Upload(LibraryTitle, DocumentTitle2, DocumentResource2);
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

        /// Gets the current libraries provider Url name.
        /// </summary>
        [ServerArrangement]
        public void GetCurrentProviderUrlName()
        {
            string urlName = ServerOperations.Media().GetCurrentProviderUrlName;

            ServerArrangementContext.GetCurrent().Values.Add("CurrentProviderUrlName", urlName);
        }

        private const string PageName = "PageWithDocument";
        private const string LibraryTitle = "TestDocumentLibrary";
        private const string DocumentTitle1 = "Image1";
        private const string DocumentTitle2 = "Document1";
        private const string DocumentResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string DocumentResource2 = "Telerik.Sitefinity.TestUtilities.Data.Documents.Document1.docx";
        private const string PageTemplateName = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}