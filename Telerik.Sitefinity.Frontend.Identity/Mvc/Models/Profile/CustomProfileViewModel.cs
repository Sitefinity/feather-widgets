using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile
{
    /// <summary>
    /// This class provides abstraction for accessing properties of different types of <see cref="UserProfile"/> in widget views.
    /// </summary>
    public class CustomProfileViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomProfileViewModel"/> class.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        public CustomProfileViewModel(UserProfile userProfile)
        {
            this.Fields = new DynamicUserProfileFieldAccessor(userProfile);
            this.UserProfile = userProfile;
        }

        /// <summary>
        /// Gets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public dynamic Fields { get; internal set; }

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <value>
        /// The user profile.
        /// </value>
        public UserProfile UserProfile { get; internal set; }
    }
}
