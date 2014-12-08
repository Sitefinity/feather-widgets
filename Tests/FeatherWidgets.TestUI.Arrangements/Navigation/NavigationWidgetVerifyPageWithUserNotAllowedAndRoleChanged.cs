using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// A class with arrangement methods for NavigationWidgetVerifyPageWithUserNotAllowedAndRoleChanged
    /// </summary>
    public class NavigationWidgetVerifyPageWithUserNotAllowedAndRoleChanged : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string templateName = ServerArrangementContext.GetCurrent().Values["templateName"];

            AuthenticationHelper.AuthenticateUser(AdminUser, AdminPassword);           

            List<string> roles = new List<string>() { Editors, BackendUsers };

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(templateName);
            Guid pageId = ServerOperations.Pages().CreatePage(HomePage, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);

            ServerOperations.Users().CreateUserWithProfileAndRoles(User, Password, User, User, Email, roles);
            ServerOperations.Pages().CreatePage(TestPage);
            ServerOperationsFeather.Pages().DenyPermissionsForRole(Editors, RoleProvider, TestPage);

            FeatherServerOperations.Pages().AddMvcWidgetToPage(pageId, typeof(NavigationController).FullName, WidgetCaption, PlaceHolderId);        
        }

        [ServerArrangement]
        public void ChangeUserRole()
        {
            ServerOperations.Users().LogoutUser();
            AuthenticationHelper.AuthenticateUser(AdminUser, AdminPassword);
            ServerOperationsFeather.Users().RemoveUserFromRole(Editors, RoleProvider, User);
            ServerOperationsFeather.Users().AddUserToRole(Designers, RoleProvider, User);
            ServerOperationsFeather.Pages().RepublishPage(HomePage);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Users().LogoutUser();
            AuthenticationHelper.AuthenticateUser(AdminUser, AdminPassword); 
            ServerOperations.Users().DeleteUserAndProfile(User);
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string AdminUser = "admin";
        private const string AdminPassword = "admin@2";
        private const string User = "editor";
        private const string Password = "password";
        private const string Email = "editor@test.bg";
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
