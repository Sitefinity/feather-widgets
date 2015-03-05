using Microsoft.IdentityModel.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile
{
    /// <summary>
    /// This class is used as a model for the <see cref="ProfileController"/>.
    /// </summary>
    public class ProfileModel :IProfileModel
    {
        #region Properties

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        public SaveAction SaveChangesAction { get; set; }

        /// <inheritdoc />
        public Guid ProfileSavedPageId { get; set; }

        /// <inheritdoc />
        public string ProfileSaveMsg { get; set; }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string ProfileBindings
        {
            get 
            {
                return this.profileBindings;
            }
            set 
            {
                this.profileBindings = value;
            }
        }


        /// <summary>
        /// Gets collection of <see cref="UserProfile"/> for selected user.
        /// </summary>
        /// <remarks>
        /// If no <see cref="UserName"/> is selected gets the collection of <see cref="UserProfile"/> for current user.
        /// </remarks>
        /// <value>The current user.</value>
        public IList<UserProfile> SelectedUserProfiles
        {
            get
            {
                if (this.selectedUserProfiles != null)
                    return this.selectedUserProfiles;

                UserProfileManager profileManager = UserProfileManager.GetManager();
                if (!this.UserName.IsNullOrEmpty()){
                    this.selectedUserProfiles = profileManager.GetUserProfiles().Where(prof=>prof.User.UserName == this.UserName).ToList();
                }

                if (this.selectedUserProfiles == null)
                {
                    var userId = ClaimsManager.GetCurrentIdentity().UserId;
                    this.selectedUserProfiles = profileManager.GetUserProfiles(userId).ToList();
                }

                return this.selectedUserProfiles;
            }
        }

        /// <inheritdoc />
        public string UserName { get; set; }

        #endregion

        #region Public methods

        /// <inheritdoc />
        public ProfilePreviewViewModel GetProfilePreviewViewModel()
        {
            var viewModel = new ProfilePreviewViewModel(this.SelectedUserProfiles)
            {
                CssClass = this.CssClass,
                CanEdit = this.CanEdit()
            };

            return viewModel;
        }

        /// <inheritdoc />
        public ProfileEditViewModel GetProfileEditViewModel()
        {
            var profileFields = this.GetProfileFieldValues();
            var viewModel = new ProfileEditViewModel(this.SelectedUserProfiles, profileFields)
            {
                CssClass = this.CssClass,
                ProfileSaveMsg = this.ProfileSaveMsg,
                CanEdit = this.CanEdit()
            };

            return viewModel;
        }

        /// <summary>
        /// Edits the user profile.
        /// </summary>
        /// <param name="profileProperties">The profile properties.</param>
        public bool EditUserProfile(IDictionary<string, string> profileProperties)
        {
            var canEdit = this.CanEdit();
            if (canEdit)
            {
                var bindingContract = new JavaScriptSerializer().Deserialize<List<ProfileBindingsContract>>(this.ProfileBindings);

                var userProfileManager = UserProfileManager.GetManager();
                using (new ElevatedModeRegion(userProfileManager))
                {
                    foreach (var profileBinding in bindingContract)
                    {
                        var userProfile = this.SelectedUserProfiles.Where(prof => prof.GetType().FullName == profileBinding.ProfileType).SingleOrDefault();
                        foreach (var property in profileBinding.Properties)
                        {
                            var value = profileProperties.GetValueOrDefault(property.Name);
                            userProfile.SetValue(property.FieldName, value);
                        }

                        userProfileManager.RecompileItemUrls(userProfile);
                    }

                    userProfileManager.SaveChanges();
                }
            }

            return canEdit;
        }

        /// <summary>
        /// Determines whether this instance can edit.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanEdit()
        {
            var currentIdentity = ClaimsManager.GetCurrentIdentity();
            var canEdit = currentIdentity.IsUnrestricted ||
                (!this.UserName.IsNullOrEmpty() && currentIdentity.Name == this.UserName) ||
                this.UserName.IsNullOrEmpty();

            return canEdit;
        }

        #endregion

        /// <summary>
        /// Gets the profile field values.
        /// </summary>
        /// <returns></returns>
        protected virtual IDictionary<string, string> GetProfileFieldValues()
        {
            IDictionary<string, string> profileFields = new Dictionary<string, string>();
            var bindingContract = new JavaScriptSerializer().Deserialize<List<ProfileBindingsContract>>(this.ProfileBindings);

            foreach (var profileBinding in bindingContract)
            {
                var userProfile = this.SelectedUserProfiles.Where(prof => prof.GetType().FullName == profileBinding.ProfileType).SingleOrDefault();
                foreach (var property in profileBinding.Properties)
                {
                    var propValue = userProfile.GetValue(property.FieldName);
                    profileFields.Add(property.Name, (string)propValue);
                }
            }

            return profileFields;
        }

        private IList<UserProfile> selectedUserProfiles;
        private string profileBindings = "[{ProfileType: 'Telerik.Sitefinity.Security.Model.SitefinityProfile',Properties: [{ Name: 'FirstName', FieldName: 'FirstName' },{ Name: 'LastName', FieldName: 'LastName' }, {Name:'About', FieldName:'About'} ]}]";
    }
}
