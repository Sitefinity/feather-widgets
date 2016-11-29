using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// A class with arrangement methods for NavigationWidgetVerifyPageWithUserNotAllowedAndRoleChanged
    /// </summary>
    public class NavigationWidgetVerifyPageWithUserNotAllowedAndRoleChanged : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string templateName = ServerArrangementContext.GetCurrent().Values["templateName"];

            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass);           

            List<string> roles = new List<string>() { Editors, BackendUsers };

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(templateName);
            Guid pageId = ServerOperations.Pages().CreatePage(HomePage, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);

            ServerOperations.Users().CreateUserWithProfileAndRoles(Email, Password, Email, Email, Email, roles);
            ServerOperations.Pages().CreatePage(TestPage);
            ServerOperationsFeather.Pages().DenyPermissionsForRole(Editors, RoleProvider, TestPage);

            FeatherServerOperations.Pages().AddMvcWidgetToPage(pageId, typeof(NavigationController).FullName, WidgetCaption, PlaceHolderId);        
        }

        [ServerArrangement]
        public void ChangeUserRole()
        {
            ServerOperations.Users().LogoutUser();
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass);
            ServerOperationsFeather.Users().RemoveUserFromRole(Editors, RoleProvider, Email);
            ServerOperationsFeather.Users().AddUserToRole(Designers, RoleProvider, Email);
            ServerOperationsFeather.Pages().RepublishPage(HomePage);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Users().LogoutUser();
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass);
            ServerOperations.Users().DeleteUserAndProfile(Email);
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string Password = "password";
        private const string Email = "editor@test.test";
        private const string Editors = "Editors";
        private const string BackendUsers = "BackendUsers";
        private const string Designers = "Designers";
        private const string RoleProvider = "AppRoles";
        private const string HomePage = "HomePage";
        private const string TestPage = "TestPage";
        private const string WidgetCaption = "Navigation";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
