using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.Identity.UsersList
{
    /// <summary>
    /// This is a class with Users list tests for paging and limit options.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Users list paging and limit tests.")]
    public class UsersListWidgetPagingLimitTests
    {
        /// <summary>
        /// Create author and administrator users
        /// </summary>
        [SetUp]
        public void Setup()
        {
            ServerOperations.Users().CreateUserWithProfileAndRoles(AuthorUserName, AuthorPassword, AuthorFirstName, AuthorLastName, AuthorEmail, new List<string> { "BackendUsers", "Authors" });
            ServerOperations.Users().CreateUserWithProfileAndRoles(AdministratorUserName, AdministratorPassword, AdministratorFirstName, AdministratorLastName, AdministratorEmail, new List<string> { "BackendUsers", "Administrators" });
        }

        /// <summary>
        /// Delete users
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            ServerOperations.Users().DeleteUserAndProfile(AuthorUserName);
            ServerOperations.Users().DeleteUserAndProfile(AdministratorUserName);
        }

        /// <summary>
        /// Verify Paging (1 item per page) and All registered users in Users list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.FeatherTeam)]
        public void UsersList_VerifyPaging()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(UsersListController).FullName;
            var usersListController = new UsersListController();
            usersListController.Model.SelectionMode = SelectionMode.AllItems;
            usersListController.Model.DisplayMode = ListDisplayMode.Paging;
            usersListController.Model.SortExpression = "FirstName ASC";
            usersListController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(usersListController);

            var usersPage1 = usersListController.Model.CreateListViewModel(1).Items.ToArray();
            Assert.IsTrue(usersPage1.Length.Equals(1), "Number of users is not correct");
            Assert.AreEqual(SitefinityAdmin, usersPage1[0].Fields.User.UserName, "Wrong username");

            var usersPage2 = usersListController.Model.CreateListViewModel(2).Items.ToArray();
            Assert.IsTrue(usersPage2.Length.Equals(1), "Number of users is not correct");
            Assert.AreEqual(AdministratorUserName, usersPage2[0].Fields.User.UserName, "Wrong username");

            var usersPage3 = usersListController.Model.CreateListViewModel(3).Items.ToArray();
            Assert.IsTrue(usersPage3.Length.Equals(1), "Number of users is not correct");
            Assert.AreEqual(AuthorUserName, usersPage3[0].Fields.User.UserName, "Wrong username");
        }

        /// <summary>
        /// Verify Limit to 1 and All registered users in Users list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.FeatherTeam)]
        public void UsersList_VerifyLimit()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(UsersListController).FullName;
            var usersListController = new UsersListController();
            usersListController.Model.SelectionMode = SelectionMode.AllItems;
            usersListController.Model.DisplayMode = ListDisplayMode.Limit;
            usersListController.Model.SortExpression = "FirstName ASC";
            usersListController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(usersListController);

            var users = usersListController.Model.CreateListViewModel(1).Items.ToArray();
            Assert.IsTrue(users.Length.Equals(1), "Number of users is not correct");
            Assert.AreEqual(SitefinityAdmin, users[0].Fields.User.UserName, "Wrong username");
        }

        /// <summary>
        /// Verify No limit and All registered users in Users list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.FeatherTeam)]
        public void UsersList_VerifyNoLimit()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(UsersListController).FullName;
            var usersListController = new UsersListController();
            usersListController.Model.SelectionMode = SelectionMode.AllItems;
            usersListController.Model.DisplayMode = ListDisplayMode.All;
            usersListController.Model.SortExpression = "FirstName ASC";
            usersListController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(usersListController);

            var users = usersListController.Model.CreateListViewModel(1).Items.ToArray();
            Assert.IsTrue(users.Length.Equals(3), "Number of users is not correct");
            Assert.AreEqual(SitefinityAdmin, users[0].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(AdministratorUserName, users[1].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(AuthorUserName, users[2].Fields.User.UserName, "Wrong username");
        }

        private const string SitefinityAdmin = "admin";

        private const string AuthorUserName = "author";
        private const string AuthorPassword = "password";
        private const string AuthorFirstName = "test";
        private const string AuthorLastName = "last";
        private const string AuthorEmail = "author@test.com";

        private const string AdministratorUserName = "admin2";
        private const string AdministratorPassword = "passoword";
        private const string AdministratorFirstName = "fname";
        private const string AdministratorLastName = "lname";
        private const string AdministratorEmail = "admin2@admin.com";
    }
}
