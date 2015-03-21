using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Identity
{
    /// <summary>
    /// This is the entry point class for login form on the frontend.
    /// </summary>
    public class LoginFormWrapper : BaseWrapper
    {
        /// <summary>
        /// Asserts that already logged in message is present.
        /// </summary>
        public void AssertAlreadyLoggedInMessage()
        {
            this.EM.Identity.LoginFormFrontend.AlreadyLoggedInMessage.AssertIsPresent("Already logged in message");
        }

        /// <summary>
        /// Types user name in the input text field.
        /// </summary>
        /// <param name="userName">the user name.</param>
        public void EnterUserName(string userName)
        {
            HtmlInputText userNameField = this.EM.Identity.LoginFormFrontend.UserName.AssertIsPresent("user name input");

            userNameField.Text = string.Empty;
            userNameField.Text = userName;
        }

        /// <summary>
        /// Types password in the input text field.
        /// </summary>
        /// <param name="userName">the password.</param>
        public void EnterPassword(string password)
        {
            HtmlInputPassword passwordField = this.EM.Identity.LoginFormFrontend.Password.AssertIsPresent("password input");

            passwordField.Text = string.Empty;
            passwordField.Text = password;
        }

        /// <summary>
        /// Click the login button.
        /// </summary>
        public void PressLoginButton()
        {
            HtmlButton loginBtn = this.EM.Identity.LoginFormFrontend.LoginButton.AssertIsPresent("Login button");
            loginBtn.Click();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Verifies required message for username field.
        /// </summary>
        public void AssertEmptyUserNameFieldMessage()
        {
            HtmlSpan message = this.EM.Identity.LoginFormFrontend.UserNameRequiredMessage
                .AssertIsPresent("The Username field is required.");
        }

        /// <summary>
        /// Verifies required message for password field.
        /// </summary>
        public void AssertEmptyPasswordFieldMessage()
        {
            HtmlSpan message = this.EM.Identity.LoginFormFrontend.PasswordRequiredMessage
                .AssertIsPresent("The Password field is required.");
        }

        /// <summary>
        /// Verifies incorrect username/password message after press login.
        /// </summary>
        public void AssertIncorrectUserNamePasswordMessage()
        {
            HtmlSpan message = this.EM.Identity.LoginFormFrontend.IncorrectUserNamePasswordMessage
                .AssertIsPresent("Incorrect Username/Password Message"); 
        }
    }
}
