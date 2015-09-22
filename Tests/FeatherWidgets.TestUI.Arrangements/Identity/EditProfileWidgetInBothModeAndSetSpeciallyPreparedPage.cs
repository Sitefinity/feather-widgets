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
    /// Arrangement methods for EditProfileWidgetInBothModeAndSetSpeciallyPreparedPage
    /// </summary>
    public class EditProfileWidgetInBothModeAndSetSpeciallyPreparedPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddProfileWidgetToPage(pageId, PlaceHolderId);

            var page2Id = ServerOperations.Pages().CreatePage(SpeciallPage);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page2Id, ContentBlockText);

            ServerOperations.Users().CreateUserWithProfileAndRoles(NewUserName, NewUserPassword, NewUserFirstName, NewUserLastName, NewUserEmail, new List<string> { "BackendUsers", "Administrators" });
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
            ServerOperations.Users().DeleteUserAndProfile(NewUserName);
        }

        private const string SpeciallPage = "SpeciallPage";
        private const string ContentBlockText = "Specially prepared page";
        private const string PageName = "ProfilePage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string NewUserName = "newUser";
        private const string NewUserPassword = "password";
        private const string NewUserFirstName = "First name";
        private const string NewUserLastName = "Last name";
        private const string NewUserEmail = "newuser@test.com";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
    }
}
