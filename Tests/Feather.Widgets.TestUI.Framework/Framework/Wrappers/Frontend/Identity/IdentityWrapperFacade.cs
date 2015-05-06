using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.Identity
{
    /// <summary>
    /// This is an entry point for identity wrappers for the frontend.
    /// </summary>
    public class IdentityWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the LoginFormWrapper
        /// </summary>
        /// <returns>Returns the LoginFormWrapper</returns>
        public LoginFormWrapper LoginFormWrapper()
        {
            return new LoginFormWrapper();
        }

        /// <summary>
        /// Provides unified access to the LoginStatusWrapper
        /// </summary>
        /// <returns>Returns the LoginStatusWrapper</returns>
        public LoginStatusWrapper LoginStatusWrapper()
        {
            return new LoginStatusWrapper();
        }

        /// <summary>
        /// Provides unified access to the ProfileWrapper
        /// </summary>
        /// <returns>Returns the ProfileWrapper</returns>
        public ProfileWrapper ProfileWrapper()
        {
            return new ProfileWrapper();
        }

        /// <summary>
        /// Provides unified access to the RegistrationWrapper
        /// </summary>
        /// <returns>Returns the RegistrationWrapper</returns>
        public RegistrationWrapper RegistrationWrapper()
        {
            return new RegistrationWrapper();
        }

        /// <summary>
        /// Provides unified access to the UsersListWrapper
        /// </summary>
        /// <returns>Returns the UsersListWrapper</returns>
        public UsersListWrapper UsersListWrapper()
        {
            return new UsersListWrapper();
        }
    }
}
