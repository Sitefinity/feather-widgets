using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for VerifyRegistrationWidgetAndProfileWidgetOnPreviewBootstrapTemplate
    /// </summary>
    public class VerifyRegistrationWidgetAndProfileWidgetOnPreviewBootstrapTemplate : TestArrangementBase
    {  
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperations.Users().DeleteUserAndProfile(NewUserName);
        }

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string NewUserName = "newUser";
    }
}
