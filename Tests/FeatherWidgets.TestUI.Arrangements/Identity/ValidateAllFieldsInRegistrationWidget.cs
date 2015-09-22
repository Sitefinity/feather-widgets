﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for ValidateAllFieldsInRegistrationWidget
    /// </summary>
    public class ValidateAllFieldsInRegistrationWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            Guid pageId = ServerOperations.Pages().CreatePage(RegistrationPage, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddRegistrationWidgetToPage(pageId, PlaceHolderId);

            ServerOperations.Users().CreateUserWithProfileAndRoles(EditorUserName, EditorUserPassword, EditorUserFirstName, EditorUserLastName, EditorUserEmail, new List<string> { "BackendUsers", "Administrators" });

            AuthenticationHelper.LogoutUser(AdminUserName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile(EditorUserName);
        }

        private const string RegistrationPage = "RegistrationPage";
        private const string TemplateTitle = "Bootstrap.default";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string EditorUserName = "editor";
        private const string EditorUserPassword = "password";
        private const string EditorUserFirstName = "First name";
        private const string EditorUserLastName = "Last name";
        private const string EditorUserEmail = "editor@test.com";
    }
}
