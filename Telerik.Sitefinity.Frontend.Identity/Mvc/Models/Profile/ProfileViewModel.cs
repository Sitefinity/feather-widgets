using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile
{
    /// <summary>
    /// This class represents view model for Profile widget.
    /// </summary>
    public class ProfileViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewModel"/> class.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        public ProfileViewModel(IList<UserProfile> userProfiles, IDictionary<string, object> profile)
        {
            this.Profile = profile;
            this.SelectedUserProfiles = new List<CustomProfileViewModel>();
            foreach (var item in userProfiles)
            {
                this.SelectedUserProfiles.Add(new CustomProfileViewModel(item));
            }
        }

        public IList<CustomProfileViewModel> SelectedUserProfiles { get; private set; }

        public IDictionary<string, object> Profile { get; set; }

        public string NewPassword { get; set; }

        public string RepeatPassword { get; set; }

        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the css class that will be applied on the wrapping element of the widget.
        /// </summary>
        /// <value>
        /// The css class value as a string.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the profile saved message.
        /// </summary>
        /// <value>
        /// Message to show when profile is saved.
        /// </value>
        public string ProfileSaveMsg { get; set; }
    }
}
