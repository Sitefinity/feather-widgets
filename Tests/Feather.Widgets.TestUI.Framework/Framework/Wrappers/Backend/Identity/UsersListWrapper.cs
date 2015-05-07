using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.jQuery;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Identity
{
    /// <summary>
    /// This is the entry point class for users list widget wrapper.
    /// </summary>
    public class UsersListWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies Which profile to display label
        /// </summary>
        public void VerifyWhichProfileToDisplayLabel()
        {
            this.EM.Identity.UsersListEditScreen.WhichProfileToDisplayLabel.AssertIsPresent("Which profile to display label label");
        }

        /// <summary>
        /// Verifies Which users to display label
        /// </summary>
        public void VerifyWhichUsersToDisplayLabel()
        {
            this.EM.Identity.UsersListEditScreen.WhichUsersToDisplayLabel.AssertIsPresent("Which users to display label");
        }

        /// <summary>
        /// Selects user profile in dropdown
        /// </summary>
        /// <param name="userProfile">user profile name</param>
        public void SelectUserProfile(string userProfile)
        {
            HtmlSelect userProfileDropdown = this.EM.Identity.UsersListEditScreen.ProfileDropdown.AssertIsPresent("User profile dropdown");

            userProfileDropdown.SelectByText(userProfile);
            userProfileDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            userProfileDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Verifies Which users to display options
        /// </summary>
        public void VerifyWhichUsersToDisplayOptions()
        {
            this.EM.Identity.UsersListEditScreen.AllRegisteredUsersRadioButton.AssertIsPresent("All registered users radio button");
            this.EM.Identity.UsersListEditScreen.SelectedUsersRadioButton.AssertIsPresent("Selected users radio button");
            this.EM.Identity.UsersListEditScreen.UsersByRolesRadioButton.AssertIsPresent("Users by role radio button");
        }

        /// <summary>
        /// Selects users and role provider dropdown
        /// </summary>
        /// <param name="userProfile">users and roles provider</param>
        public void SelectProvider(string provider)
        {
            HtmlSelect userProfileDropdown = this.EM.Selectors.SelectorsScreen.ProvidersDropdown.AssertIsPresent("Users and roles provider dropdown");

            userProfileDropdown.SelectByText(provider);
            userProfileDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.click);
            userProfileDropdown.AsjQueryControl().InvokejQueryEvent(jQueryControl.jQueryControlEvents.change);
        }

        /// <summary>
        /// Selects provider values for users in providers dropdown
        /// </summary>
        public void VerifyUsersProviderOptions()
        {
            HtmlSelect profileDropdown = this.EM.Selectors.SelectorsScreen.ProvidersDropdown.AssertIsPresent("Profile dropdown");

            //// count of options is 3, because there is one empty entry at 0 position
            Assert.AreEqual(3, profileDropdown.Options.Count, "User providers count is not correct");
            Assert.AreEqual("All Users", profileDropdown.Options[1].Text, "Incorrect provider");
            Assert.AreEqual("Default", profileDropdown.Options[2].Text, "Incorrect provider");
        }

        /// <summary>
        /// Selects provider values for roles in providers dropdown
        /// </summary>
        public void VerifyRolesProviderOptions()
        {
            HtmlSelect profileDropdown = this.EM.Selectors.SelectorsScreen.ProvidersDropdown.AssertIsPresent("Profile dropdown");

            //// count of options is 4, because there is one empty entry at 0 position
            Assert.AreEqual(4, profileDropdown.Options.Count, "Role providers count is not correct");
            Assert.AreEqual("All Roles", profileDropdown.Options[1].Text, "Incorrect provider");
            Assert.AreEqual("Default", profileDropdown.Options[2].Text, "Incorrect provider");
            Assert.AreEqual("AppRoles", profileDropdown.Options[3].Text, "Incorrect provider");
        }
    }
}
