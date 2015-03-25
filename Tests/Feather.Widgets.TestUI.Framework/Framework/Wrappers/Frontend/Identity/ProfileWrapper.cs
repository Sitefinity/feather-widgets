using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Identity
{
    /// <summary>
    /// This is the entry point class for profile on the frontend.
    /// </summary>
    public class ProfileWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify user names
        /// </summary>
        /// <param name="userNames">User first and last names</param>
        public void VerifyUserFirstAndLastName(string userNames)
        {
           ActiveBrowser.Find.ByExpression<HtmlControl>("tagname=h3", "InnerText=" + userNames)
                                                    .AssertIsPresent("User with this name" + userNames + "not found");
        }

        /// <summary>
        /// Verify user email address
        /// </summary>
        /// <param name="emailAddress">User email address</param>
        public void VerifyUserEmailAddress(string emailAddress)
        {
            ActiveBrowser.Find.ByExpression<HtmlControl>("tagname=p", "InnerText=" + emailAddress)
                                                     .AssertIsPresent("User with this email" + emailAddress + "not found");
        }

        /// <summary>
        /// Press Save changes button
        /// </summary>
        public void SaveChangesButton()
        {
            HtmlButton saveChangesButton = EM.Identity.ProfileFrontend.SaveChanges
            .AssertIsPresent("Save changes button");
            saveChangesButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Click change password link
        /// </summary>
        public void ChangePasswordLink()
        {
            HtmlAnchor changePassworLink = EM.Identity.ProfileFrontend.ChangePasswordLink
            .AssertIsPresent("Change password link");
            changePassworLink.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Fill user first name
        /// </summary>
        /// <param name="firstName">First name</param>
        public void FillFirstName(string firstName)
        {
            HtmlInputText firstNameInput = EM.Identity.ProfileFrontend.FirstName
                .AssertIsPresent("First name field");

            firstNameInput.Text = string.Empty;
            firstNameInput.Text = firstName;
        }

        /// <summary>
        /// Fill user last name
        /// </summary>
        /// <param name="lastName">Last name</param>
        public void FillLastName(string lastName)
        {
            HtmlInputText lastNameInput = EM.Identity.ProfileFrontend.LastName
                .AssertIsPresent("Last name field");

            lastNameInput.Text = string.Empty;
            lastNameInput.Text = lastName;
        }

        /// <summary>
        /// Fill user old password
        /// </summary>
        /// <param name="oldPassword">Old password</param>
        public void FillOldPassword(string oldPassword)
        {
            HtmlInputPassword oldPasswordInput = EM.Identity.ProfileFrontend.OldPassword
                .AssertIsPresent("Last name field");

            oldPasswordInput.Text = string.Empty;
            oldPasswordInput.Text = oldPassword;
        }

        /// <summary>
        /// Fill user new password
        /// </summary>
        /// <param name="newPassword">New password</param>
        public void FillNewPassword(string newPassword)
        {
            HtmlInputPassword newPasswordInput = EM.Identity.ProfileFrontend.NewPassword
                .AssertIsPresent("Password field");

            newPasswordInput.Text = string.Empty;
            newPasswordInput.Text = newPassword;
        }

        /// <summary>
        /// Retype user new password
        /// </summary>
        /// <param name="password">Retype Password</param>
        public void FillRetypePassword(string password)
        {
            HtmlInputPassword passwordInput = EM.Identity.ProfileFrontend.ReTypePassword
                .AssertIsPresent("Re-type passord field");

            passwordInput.Text = string.Empty;
            passwordInput.Text = password;
        }

        /// <summary>
        /// Verifies required message for first name field.
        /// </summary>
        public void AssertFirstNameFieldMessage()
        {
            this.EM.Identity.ProfileFrontend.FirstNameRequiredMessage.AssertIsPresent("The first name field");
        }

        /// <summary>
        /// Verifies required message for last name field.
        /// </summary>
        public void AssertLastNameFieldMessage()
        {
            this.EM.Identity.ProfileFrontend.LastNameRequiredMessage.AssertIsPresent("The last name ");
        }

        /// <summary>
        /// Verifies required message for password length field.
        /// </summary>
        public void AssertPasswordLengthFieldMessage()
        {
            this.EM.Identity.ProfileFrontend.PasswordLengthMessage.AssertIsPresent("The password length ");
        }

        /// <summary>
        /// Verifies required message for empty old password field.
        /// </summary>
        public void AssertOldPasswordInvalidFieldMessage()
        {
            this.EM.Identity.ProfileFrontend.OldPasswordInvalidMessage.AssertIsPresent("The old password ");
        }

        /// <summary>
        /// Verifies required message for empty new password field.
        /// </summary>
        public void AssertNewPasswordInvalidFieldMessage()
        {
            this.EM.Identity.ProfileFrontend.NewPasswordInvalidMessage.AssertIsPresent("The new password ");
        }

        /// <summary>
        /// Verifies required message for empty retype password field.
        /// </summary>
        public void AssertRetypePasswordInvalidFieldMessage()
        {
            this.EM.Identity.ProfileFrontend.PasswordRetypeInvalidMessage.AssertIsPresent("The re type password ");
        }

        /// <summary>
        /// Verifies new password and re-type password do not match.
        /// </summary>
        public void AssertNewAndRepeatPassDoNotMatchFieldMessage()
        {
            this.EM.Identity.ProfileFrontend.NewAndRetypePasswordDoNotMatchMessage
                .AssertIsPresent("The new and retype password ");
        }

        /// <summary>
        /// Verifies re-type and new passworddo not match.
        /// </summary>
        public void AssertRepeatAndNewPassDoNotMatchFieldMessage()
        {
            this.EM.Identity.ProfileFrontend.RetypeAndNewPasswordDoNotMatchMessage
                .AssertIsPresent("The retype and new password ");
        }

        /// <summary>
        /// Verifies successfully saved message.
        /// </summary>
        public void SuccessfullySavedMessage()
        {
            this.EM.Identity.ProfileFrontend.SuccessfullySavedMessage.AssertIsPresent("Successfully saved changes message.");
        }
    }
}
