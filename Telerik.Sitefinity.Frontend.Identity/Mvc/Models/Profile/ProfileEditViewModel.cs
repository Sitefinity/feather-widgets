using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile
{
    /// <summary>
    /// This class represents view model for Profile widget.
    /// </summary>
    public class ProfileEditViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileEditViewModel"/> class.
        /// </summary>
        public ProfileEditViewModel()
        {
            this.Profile = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileEditViewModel"/> class.
        /// </summary>
        /// <param name="userProfile">The user profile.</param>
        public ProfileEditViewModel(IDictionary<string, string> profile)
        {
            this.Profile = profile;
        }
        
        /// <summary>
        /// Gets the selected user profiles.
        /// </summary>
        /// <value>
        /// The selected user profiles.
        /// </value>
        public IList<CustomProfileViewModel> SelectedUserProfiles { get; set; }

        /// <summary>
        /// Gets or sets the profile.
        /// </summary>
        /// <value>
        /// The profile.
        /// </value>
        public IDictionary<string, string> Profile { get; set; }
        
        /// <summary>
        /// Gets or sets the old password.
        /// </summary>
        /// <value>
        /// The old password.
        /// </value>
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>
        /// The new password.
        /// </value>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the repeat password.
        /// </summary>
        /// <value>
        /// The repeat password.
        /// </value>
        public string RepeatPassword { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the profile can be edited.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [can edit]; otherwise, <c>false</c>.
        /// </value>
        public bool CanEdit { get; set; }

        /// <summary>
        /// Gets or sets the css class that will be applied on the wrapping element of the widget.
        /// </summary>
        /// <value>
        /// The css class value as a string.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show profile changed message.
        /// </summary>
        /// <value>
        /// <c>true</c> if should display profile changed message; otherwise, <c>false</c>.
        /// </value>
        public bool ShowProfileChangedMsg { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the avatar image URL.
        /// </summary>
        /// <value>
        /// The avatar image URL.
        /// </value>
        public string AvatarImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user wants to delete his picture.
        /// </summary>
        /// <value>
        /// <c>true</c> if the user wants to delete his picture; otherwise, <c>false</c>.
        /// </value>
        public bool DeletePicture { get; set; }

        /// <summary>
        /// Gets or sets the default avatar URL.
        /// </summary>
        /// <value>
        /// The default avatar URL.
        /// </value>
        public string DefaultAvatarUrl { get; set; }

        /// <summary>
        /// Gets or sets the uploaded image.
        /// </summary>
        /// <value>
        /// The uploaded image.
        /// </value>
        public HttpPostedFileBase UploadedImage { get; set; }
    }
}
