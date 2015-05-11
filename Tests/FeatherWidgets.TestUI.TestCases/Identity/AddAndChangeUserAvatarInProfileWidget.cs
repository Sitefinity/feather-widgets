using System;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Win32.Dialogs;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        TestCategory(FeatherTestCategories.Bootstrap), Ignore]
        public void AddAndChangeUserAvatarInProfileWidget()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + LoginPage.ToLower());
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetUsername(UserName);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().SetPassword(UserPassword);
            BAT.Wrappers().Backend().LoginView().LoginViewWrapper().ExecuteLogin();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().VerifyDefaultUserAvatar();

            this.UploadUserAvatar();           
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().VerifyNotDefaultUserAvatarSrc();
            string oldUserAvatarSrc = BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().UserAvatarSrc();           

            this.UploadUserAvatar();

            string currentUserAvatarSrc = BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().UserAvatarSrc();
            Assert.AreNotEqual(oldUserAvatarSrc, currentUserAvatarSrc, "Old avatar source and new avatar source are equal");
        }

        public void UploadUserAvatar()
        {
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().ClickUploadPhotoLink();
            string fullImagesPath = DeploymentDirectory + @"\";
            var fullFilePath = string.Concat(fullImagesPath, FileToUpload);

            var uploadDialog = new FileUploadDialog(Manager.Current.ActiveBrowser, fullFilePath, DialogButton.OPEN);
            Manager.Current.DialogMonitor.AddDialog(uploadDialog);
            Manager.Current.DialogMonitor.Start();

            uploadDialog.WaitUntilHandled();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.RefreshDomTree();

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();            
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
           BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "ProfilePage";
        private const string LoginPage = "Sitefinity";
        private const string UserName = "newUser";
        private const string UserPassword = "password";
        private const string FileToUpload = "IMG01648.jpg";
    }
}
