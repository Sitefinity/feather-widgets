using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Identity
{
    /// <summary>
    /// This is the entry point class for login status on the frontend.
    /// </summary>
    public class LoginStatusWrapper : BaseWrapper
    {
        /// <summary>
        /// Asserts that the user first and last name are present.
        /// </summary>
        /// <param name="firstName">user first name</param>
        /// <param name="lastName">user last name</param>
        public void AssertUserLoggedInName(string firstName, string lastName)
        {
            HtmlAnchor loggedInNameLink = this.EM.Identity.LoginStatusFrontend.LoggedInName.AssertIsPresent("Logged in name");
            Assert.AreEqual(firstName + " " + lastName, loggedInNameLink.InnerText);
        }

        /// <summary>
        /// Assters that logged user email is present.
        /// </summary>
        /// <param name="email">user email address</param>
        public void AssertUserEmail(string email)
        {
            HtmlContainerControl loggedInEmail = this.EM.Identity.LoginStatusFrontend.LoggedInEmail.AssertIsPresent("Logged in email");
            Assert.AreEqual(email, loggedInEmail.InnerText);
        }

        /// <summary>
        /// Performs logout action.
        /// </summary>
        public void Logout()
        {
            HtmlButton logoutBtn = this.EM.Identity.LoginStatusFrontend.LogoutButton.AssertIsPresent("Logout button");
            logoutBtn.Click();
            ActiveBrowser.WaitForAsyncJQueryRequests();
        }
    }
}
