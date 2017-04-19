using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for VerifyRegistrationWidgetAndProfileWidgetOnPreviewPage
    /// </summary>
    public class VerifyRegistrationWidgetAndProfileWidgetOnPreviewPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            Guid pageId = ServerOperations.Pages().CreatePage(PageTitle, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddRegistrationWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddProfileWidgetToPage(pageId, PlaceHolderId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile(NewUserEmail);
        }

        private const string PageTitle = "RegistrationPage";
        private const string TemplateTitle = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string NewUserEmail = "newuser@test.test";
    }
}
