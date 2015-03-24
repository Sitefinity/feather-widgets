using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for UI test LoginFormAddChangeRemoveRedirectToPageAfterLogin
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login")]
    public class LoginFormAddChangeRemoveRedirectToPageAfterLogin : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName, templateId);
            Guid pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);

            ServerOperationsFeather.Pages().AddLoginFormWidgetToPage(pageNodeId, PlaceHolderId);

            ServerOperations.Pages().CreatePage(FirstRedirectPage);
            ServerOperations.Pages().CreatePage(SecondRedirectPage);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string PageName = "LoginPage";
        private const string FirstRedirectPage = "RedirectPage1";
        private const string SecondRedirectPage = "RedirectPage2";
        private const string TemplateTitle = "Bootstrap.default";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
