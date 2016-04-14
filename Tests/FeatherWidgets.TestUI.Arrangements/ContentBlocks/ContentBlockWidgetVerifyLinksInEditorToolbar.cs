using System;
using System.IO;
using System.Threading;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for ContentBlockWidgetVerifyLinksInEditorToolbar
    /// </summary>
    public class ContentBlockWidgetVerifyLinksInEditorToolbar : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            
            var pageId = ServerOperations.Pages().CreatePage(PageTitle);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId, ContentBlockText);
            
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId1 = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageTitle1, templateId);
            pageId1 = ServerOperations.Pages().GetPageNodeId(pageId1);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId1, ContentBlockText1, PlaceHolderId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string PageTitle = "FeatherPageNonBootstrap";
        private const string ContentBlockText = "TestContent";
        private const string PageTitle1 = "TestPage";
        private const string ContentBlockText1 = "Test content1";
        private const string PageTemplateName = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
