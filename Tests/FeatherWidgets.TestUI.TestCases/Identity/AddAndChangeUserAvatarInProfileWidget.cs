using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Identity
{
    /// <summary>
    /// AddAndChangeUserAvatarInProfileWidget_ test class.
    /// </summary>
    [TestClass]
    public class AddAndChangeUserAvatarInProfileWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddAndChangeUserAvatarInProfileWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.Profile),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void AddAndChangeUserAvatarInProfileWidget()
        {
            this.LoginUser(UserName, UserPassword);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(),false);

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().ClickEditProfileLink();
            ////BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().VerifyDefaultUserAvatar();

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().ClickUploadPhotoLink();

            string fullImagesPath = DeploymentDirectory + @"\";
            BATFeather.Wrappers().Backend().Media().ImageUploadPropertiesWrapper().PerformSingleFileUpload(FileToUpload, fullImagesPath);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().VerifyNotDefaultUserAvatarSrc();
        }

        /// <summary>
        /// Log in user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="password">Password</param>
        public void LoginUser(string userName, string password)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + LoginPage.ToLower());
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetUsername(userName);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetPassword(password);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().ExecuteLogin();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            ////BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            ////BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "ProfilePage";
        private const string LoginPage = "Sitefinity";
        private const string UserName = "newUser";
        private const string UserPassword = "password";

        private const string FileToUpload = "IMG01648.jpg";
    }
}
