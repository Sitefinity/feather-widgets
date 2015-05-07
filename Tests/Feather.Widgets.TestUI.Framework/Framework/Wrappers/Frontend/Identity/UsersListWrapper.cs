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
    /// This is the entry point class for users list on the frontend.
    /// </summary>
    public class UsersListWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies users list on hybrid page
        /// </summary>
        /// <param name="usersToVerify">users first and last name to be verified</param>
        public void VerifyUsersListOnHybridPage(string[] usersToVerify)
        {
            List<HtmlDiv> usersDivs = this.EM.Identity.UsersListFrontend.UsersDivsHybridPage;
            Assert.IsNotNull(usersDivs, "List of div elements is null.");
            Assert.AreEqual(usersToVerify.Length, usersDivs.Count, "Expected and actual count of list items are not equal.");

            for (int i = 0; i < usersToVerify.Length; i++)
            {
                //// default user profile photo is used
                Assert.AreEqual("/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png", usersDivs[i].ChildNodes[0].As<HtmlImage>().Src, "Scr of user profile");
                Assert.AreEqual(usersToVerify[i], usersDivs[i].ChildNodes[1].InnerText, "first and last name");
                Assert.AreEqual(string.Empty, usersDivs[i].ChildNodes[2].InnerText, "email should not be visible");
            }
        }

        /// <summary>
        /// Verifies user single page after clicking on a user link
        /// </summary>
        /// <param name="userFirstLastName">user first and last anme</param>
        /// <param name="singlePageURlEnding">single user page URL ending</param>
        public void VerifySingleUserOnHybridPage(string userFirstLastName, string userEmail, string singlePageURlEnding)
        {
            HtmlAnchor userLink = this.EM.Identity.UsersListFrontend.GetSingleUserLink(userFirstLastName).AssertIsPresent("link to single user page");
            userLink.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForUrl(singlePageURlEnding);
            ActiveBrowser.WaitForAsyncJQueryRequests();

            HtmlDiv usersDiv = this.EM.Identity.UsersListFrontend.SingleUserDivHybridPage.AssertIsPresent("single user");

            Assert.AreEqual("/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png", usersDiv.ChildNodes[0].As<HtmlImage>().Src, "Scr of user profile");
            Assert.AreEqual(userFirstLastName, usersDiv.ChildNodes[1].InnerText, "first and last name");
            Assert.AreEqual(userEmail, usersDiv.ChildNodes[2].InnerText, "user email");
        }

        /// <summary>
        /// Verifies users list on bootstrap page
        /// </summary>
        /// <param name="usersToVerify">users first and last name to be verified</param>
        public void VerifyUsersListOnBootstrapPage(string[] usersToVerify)
        {
            List<HtmlDiv> usersDivs = this.EM.Identity.UsersListFrontend.UsersDivsBootstrapPage;
            Assert.IsNotNull(usersDivs, "List of div elements is null.");
            Assert.AreEqual(usersToVerify.Length, usersDivs.Count, "Expected and actual count of list items are not equal.");

            for (int i = 0; i < usersToVerify.Length; i++)
            {
                //// default user profile photo is used
                Assert.AreEqual("/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png", usersDivs[i].ChildNodes[0].ChildNodes[0].As<HtmlImage>().Src, "Scr of user profile");
                Assert.AreEqual(usersToVerify[i], usersDivs[i].ChildNodes[1].ChildNodes[0].InnerText, "first and last name");
                Assert.AreEqual(string.Empty, usersDivs[i].ChildNodes[1].ChildNodes[1].InnerText, "email should not be visible");
            }
        }

        /// <summary>
        /// Verifies user single bootstrap page after clicking on a user link
        /// </summary>
        /// <param name="userFirstLastName">user first and last anme</param>
        /// <param name="singlePageURlEnding">single user page URL ending</param>
        public void VerifySingleUserOnBootstrapPage(string userFirstLastName, string userEmail, string singlePageURlEnding)
        {
            HtmlAnchor userLink = this.EM.Identity.UsersListFrontend.GetSingleUserLink(userFirstLastName).AssertIsPresent("link to single user page");
            userLink.Click();

            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForUrl(singlePageURlEnding);
            ActiveBrowser.WaitForAsyncJQueryRequests();

            HtmlDiv usersDiv = this.EM.Identity.UsersListFrontend.SingleUserDivBootstrapPage.AssertIsPresent("single user");

            Assert.AreEqual("/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png", usersDiv.ChildNodes[0].ChildNodes[0].As<HtmlImage>().Src, "Scr of user profile");
            Assert.AreEqual(userFirstLastName, usersDiv.ChildNodes[1].ChildNodes[0].InnerText, "first and last name");
            Assert.AreEqual(userEmail, usersDiv.ChildNodes[1].ChildNodes[1].InnerText, "user email");
        }
    }
}
