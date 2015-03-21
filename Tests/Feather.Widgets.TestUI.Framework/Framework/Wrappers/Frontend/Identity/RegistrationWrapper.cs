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
    /// This is the entry point class for registration on the frontend.
    /// </summary>
    public class RegistrationWrapper : BaseWrapper
    {
        /// <summary>
        /// Fill user first name
        /// </summary>
        /// <param name="firstName">First name</param>
        public void FillFirstName(string firstName)
        {
            HtmlInputText firstNameInput = EM.Identity.RegistrationFrontend.FirstName
                .AssertIsPresent("First name field");

            firstNameInput.Text = firstName;
        }

        /// <summary>
        /// Fill user last name
        /// </summary>
        /// <param name="firstName">Last name</param>
        public void FillLastName(string lastName)
        {
            HtmlInputText lastNameInput = EM.Identity.RegistrationFrontend.LastName
                .AssertIsPresent("Last name field");

            lastNameInput.Text = lastName;
        }

        /// <summary>
        /// Fill user email
        /// </summary>
        /// <param name="firstName">Email address</param>
        public void FillEmail(string email)
        {
            HtmlInputText emailInput = EM.Identity.RegistrationFrontend.Email
                .AssertIsPresent("Email field");

            emailInput.Text = email;
        }

        /// <summary>
        /// Fill user name
        /// </summary>
        /// <param name="firstName">User name</param>
        public void FillUserName(string userName)
        {
            HtmlInputText userNameInput = EM.Identity.RegistrationFrontend.Username
                .AssertIsPresent("User name field");

            userNameInput.Text = userName;
        }

        /// <summary>
        /// Fill user password
        /// </summary>
        /// <param name="firstName">Password</param>
        public void FillPassword(string password)
        {
            HtmlInputPassword passwordInput = EM.Identity.RegistrationFrontend.Password
                .AssertIsPresent("Password field");

            passwordInput.Text = password;
        }

        /// <summary>
        /// Retype user password
        /// </summary>
        /// <param name="firstName">Retype Password</param>
        public void FillRetypePassword(string password)
        {
            HtmlInputPassword passwordInput = EM.Identity.RegistrationFrontend.ReTypePassword
                .AssertIsPresent("Re-type passord field");

            passwordInput.Text = password;
        }

        /// <summary>
        /// Press Register button
        /// </summary>
        public void RegisterButton()
        {
            HtmlButton registerButton = EM.Identity.RegistrationFrontend.RegisterButton
            .AssertIsPresent("register button");
            registerButton.Click();
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }

        /// <summary>
        /// Verify successfully registered message
        /// </summary>
        public void VerifySuccessfullyMessage()
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            var isContained = frontendPageMainDiv.InnerText.Contains("Thank you!You are successfully registered.");
            Assert.IsTrue(isContained, "Successfully message ");
        }
    }
}
