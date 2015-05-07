using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Identity
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather login and regisration widgets.
    /// </summary>
    public class IdentityMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public IdentityMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the login form widget backend
        /// </summary>
        public LoginFormEditScreen LoginFormEditScreen
        {
            get
            {
                return new LoginFormEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the login form widget frontend.
        /// </summary>
        public LoginFormFrontend LoginFormFrontend
        {
            get
            {
                return new LoginFormFrontend(this.find);
            }
        }

        /// <summary>
        /// Gets the login status widget backend
        /// </summary>
        public LoginStatusEditScreen LoginStatusEditScreen
        {
            get
            {
                return new LoginStatusEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the login status widget frontend.
        /// </summary>
        public LoginStatusFrontend LoginStatusFrontend
        {
            get
            {
                return new LoginStatusFrontend(this.find);
            }
        }

        /// <summary>
        /// Gets the profile widget backend
        /// </summary>
        public ProfileEditScreen ProfileEditScreen
        {
            get
            {
                return new ProfileEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the profile widget frontend.
        /// </summary>
        public ProfileFrontend ProfileFrontend
        {
            get
            {
                return new ProfileFrontend(this.find);
            }
        }

        /// <summary>
        /// Gets the registration widget backend
        /// </summary>
        public RegistrationEditScreen RegistrationEditScreen
        {
            get
            {
                return new RegistrationEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the registration widget frontend.
        /// </summary>
        public RegistrationFrontend RegistrationFrontend
        {
            get
            {
                return new RegistrationFrontend(this.find);
            }
        }

        /// <summary>
        /// Gets the users list widget backend.
        /// </summary>
        public UsersListEditScreen UsersListEditScreen
        {
            get
            {
                return new UsersListEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the users list widget frontend.
        /// </summary>
        public UsersListFrontend UsersListFrontend
        {
            get
            {
                return new UsersListFrontend(this.find);
            }
        }

        private Find find;
    }
}
