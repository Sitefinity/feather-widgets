using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for VerifyRegistrationWidgetAndProfileWidgetOnPreviewHybridPage
    /// </summary>
    public class VerifyRegistrationWidgetAndProfileWidgetOnPreviewHybridPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

            var pageId = ServerOperations.Pages().CreatePage(PageTitle);
            ServerOperationsFeather.Pages().AddRegistrationWidgetToPage(pageId);
            ServerOperationsFeather.Pages().AddProfileWidgetToPage(pageId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile(NewUserName);
        }

        private const string PageTitle = "RegistrationPage";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string NewUserName = "newUser";
    }
}
