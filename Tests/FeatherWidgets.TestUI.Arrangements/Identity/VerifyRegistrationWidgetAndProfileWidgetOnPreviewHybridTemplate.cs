using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for VerifyRegistrationWidgetAndProfileWidgetOnPreviewHybridTemplate
    /// </summary>
    public class VerifyRegistrationWidgetAndProfileWidgetOnPreviewHybridTemplate : TestArrangementBase
    {  
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            ServerOperations.Users().DeleteUserAndProfile(NewUserEmail);
            ServerOperations.Templates().DeletePageTemplate("TestHybrid");
        }

        private const string NewUserEmail = "newuser@test.test";
    }
}
