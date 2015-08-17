using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Identity
{
    /// <summary>
    /// ValidateAllFieldsAndChangeUserInfoInProfileWidget_ test class.
    /// </summary>
    [TestClass]
    public class ValidateAllFieldsAndChangeUserInfoInProfileWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ValidateAllFieldsAndChangeUserInfoInProfileWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Profile),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void ValidateAllFieldsAndChangeUserInfoInProfileWidget()
        {
            this.LoginUser(UserName, UserPassword);

            ////Verify first and last name empty fields message

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillFirstName(string.Empty);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillLastName(string.Empty);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().AssertFirstNameFieldMessage();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().AssertLastNameFieldMessage();

            ////Change first and last name 

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillFirstName(UserFirstNameEdited);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillLastName(UserLastNameEdited);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().AssertSuccessfullySavedMessage();

            ////Verify new password and retype empty fields message

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().ChangePasswordLink();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillOldPassword(OldPassword);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().AssertNewPasswordInvalidFieldMessage();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().AssertRetypePasswordInvalidFieldMessage();

            ////Verify old password empty field message

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillNewPassword(UserPasswordEdited);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillRetypePassword(UserPasswordEdited);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().AssertOldPasswordInvalidFieldMessage();

            ////Verify short password validation message

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillOldPassword(OldPassword);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillNewPassword(ShortPassword);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillRetypePassword(ShortPassword);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().AssertPasswordLengthFieldMessage();

            ////Verify wrong re-type password validation message

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillOldPassword(OldPassword);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillNewPassword(UserPasswordEdited);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillRetypePassword(WrongPassword);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().AssertNewAndRepeatPassDoNotMatchFieldMessage();
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().AssertRepeatAndNewPassDoNotMatchFieldMessage();

            ////Change password

            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillOldPassword(OldPassword);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillNewPassword(UserPasswordEdited);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().FillRetypePassword(UserPasswordEdited);
            BATFeather.Wrappers().Frontend().Identity().ProfileWrapper().SaveChangesButton();

            ////Log in with new password 

            BAT.Macros().User().LogOut();
            this.LoginUser(UserName, UserPasswordEdited);
            BAT.Macros().User().LogOut();
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
        private const string WrongPassword = "password1";
        private const string OldPassword = "password";
        private const string ShortPassword = "pass";
        private const string UserName = "newUser";
        private const string UserPassword = "password";
        private const string UserPasswordEdited = "passwordEdited";
        private const string UserFirstNameEdited = "First name Edited";
        private const string UserLastNameEdited = "Last name Edited";
    }
}
