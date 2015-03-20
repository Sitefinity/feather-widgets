using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Identity
{
    /// <summary>
    /// This is an entry point for identity wrappers for the backend.
    /// </summary>
    public class IdentityWrapperFacade
    {
        /// <summary>
        /// Provides access to LoginFormWrapper
        /// </summary>
        /// <returns>New instance of LoginFormWrapper</returns>
        public LoginFormWrapper LoginFormWrapper()
        {
            return new LoginFormWrapper();
        }

        /// <summary>
        /// Provides access to LoginStatusWrapper
        /// </summary>
        /// <returns>New instance of LoginStatusWrapper</returns>
        public LoginStatusWrapper LoginStatusWrapper()
        {
            return new LoginStatusWrapper();
        }

        /// <summary>
        /// Provides access to ProfileWrapper
        /// </summary>
        /// <returns>New instance of ProfileWrapper</returns>
        public ProfileWrapper ProfileWrapper()
        {
            return new ProfileWrapper();
        }

        /// <summary>
        /// Provides access to RegistrationWrapper
        /// </summary>
        /// <returns>New instance of RegistrationWrapper</returns>
        public RegistrationWrapper RegistrationWrapper()
        {
            return new RegistrationWrapper();
        }
    }
}
