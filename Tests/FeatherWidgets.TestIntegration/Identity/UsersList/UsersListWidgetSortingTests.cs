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
    /// This is a class with Users list tests for Sorting options.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Users list sorting tests.")]
    public class UsersListWidgetSortingTests
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
        /// Verify First Name A-Z sorting and All registered users in Users list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team7)]
        public void UsersList_VerifySortingFirstNameAZ()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(UsersListController).FullName;
            var usersListController = new UsersListController();
            usersListController.Model.SelectionMode = SelectionMode.AllItems;
            usersListController.Model.SortExpression = "FirstName ASC";
            mvcProxy.Settings = new ControllerSettings(usersListController);

            var users = usersListController.Model.CreateListViewModel(1).Items.ToArray();
            Assert.IsTrue(users.Length.Equals(3), "Number of users is not correct");

            //// expected: Admin Admin, fname lname, test last
            Assert.AreEqual(SitefinityAdmin, users[0].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(AdministratorUserName, users[1].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(AuthorUserName, users[2].Fields.User.UserName, "Wrong username");
        }

        /// <summary>
        /// Verify First Name Z-A sorting and All registered users in Users list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team7)]
        public void UsersList_VerifySortingFirstNameZA()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(UsersListController).FullName;
            var usersListController = new UsersListController();
            usersListController.Model.SelectionMode = SelectionMode.AllItems;
            usersListController.Model.SortExpression = "FirstName DESC";
            mvcProxy.Settings = new ControllerSettings(usersListController);

            var users = usersListController.Model.CreateListViewModel(1).Items.ToArray();
            Assert.IsTrue(users.Length.Equals(3), "Number of users is not correct");

            //// expected: test last, fname lname, Admin Admin
            Assert.AreEqual(AuthorUserName, users[0].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(AdministratorUserName, users[1].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(SitefinityAdmin, users[2].Fields.User.UserName, "Wrong username");
        }

        /// <summary>
        /// Verify Last Name A-Z sorting and All registered users in Users list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team7)]
        public void UsersList_VerifySortingLastNameAZ()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(UsersListController).FullName;
            var usersListController = new UsersListController();
            usersListController.Model.SelectionMode = SelectionMode.AllItems;
            usersListController.Model.SortExpression = "LastName ASC";
            mvcProxy.Settings = new ControllerSettings(usersListController);

            var users = usersListController.Model.CreateListViewModel(1).Items.ToArray();
            Assert.IsTrue(users.Length.Equals(3), "Number of users is not correct");

            //// expected: Admin Admin, test last, fname lname
            Assert.AreEqual(SitefinityAdmin, users[0].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(AuthorUserName, users[1].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(AdministratorUserName, users[2].Fields.User.UserName, "Wrong username");
        }

        /// <summary>
        /// Verify Last Name Z-A sorting and All registered users in Users list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team7)]
        public void UsersList_VerifySortingLastNameZA()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(UsersListController).FullName;
            var usersListController = new UsersListController();
            usersListController.Model.SelectionMode = SelectionMode.AllItems;
            usersListController.Model.SortExpression = "LastName DESC";
            mvcProxy.Settings = new ControllerSettings(usersListController);

            var users = usersListController.Model.CreateListViewModel(1).Items.ToArray();
            Assert.IsTrue(users.Length.Equals(3), "Number of users is not correct");

            //// expected: fname lname, test last, Admin Admin
            Assert.AreEqual(AdministratorUserName, users[0].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(AuthorUserName, users[1].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(SitefinityAdmin, users[2].Fields.User.UserName, "Wrong username");
        }

        /// <summary>
        /// Verify Last created sorting and All registered users in Users list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.Team7)]
        public void UsersList_VerifySortingLastCreated()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(UsersListController).FullName;
            var usersListController = new UsersListController();
            usersListController.Model.SelectionMode = SelectionMode.AllItems;
            usersListController.Model.SortExpression = "DateCreated DESC";
            mvcProxy.Settings = new ControllerSettings(usersListController);

            var users = usersListController.Model.CreateListViewModel(1).Items.ToArray();
            Assert.IsTrue(users.Length.Equals(3), "Number of users is not correct");

            //// expected: fname lname, test last, Admin Admin
            Assert.AreEqual(AdministratorUserName, users[0].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(AuthorUserName, users[1].Fields.User.UserName, "Wrong username");
            Assert.AreEqual(SitefinityAdmin, users[2].Fields.User.UserName, "Wrong username");
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
