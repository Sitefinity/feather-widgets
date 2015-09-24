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
    /// VerifyAnonymousAndLoggedUsersAreAbleToSubmitOnlyOneReview arrangement class.
    /// </summary>
    public class VerifyAnonymousAndLoggedUsersAreAbleToSubmitOnlyOneReview : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

            ServerOperations.Comments().AllowComments(ThreadType, true);
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddReviewsWidgetToPage(pageId, "Contentplaceholder1");

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
            var siteID = ServerOperations.Comments().GetCurrentSiteId.ToString();
            ServerOperations.Comments().DeleteAllComments(siteID);
            ServerOperations.Comments().AllowComments(ThreadType, false);
            ServerOperations.Users().DeleteUserAndProfile(NewUserName);
        }

        private const string PageName = "ReviewsPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string ThreadType = "Telerik.Sitefinity.Pages.Model.PageNode";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string NewUserName = "newUser";
        private const string NewUserPassword = "password";
        private const string NewUserFirstName = "First name";
        private const string NewUserLastName = "Last name";
        private const string NewUserEmail = "newuser@test.com";
    }
}
