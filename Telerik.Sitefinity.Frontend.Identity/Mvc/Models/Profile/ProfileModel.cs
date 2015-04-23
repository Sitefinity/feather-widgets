using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile
{
    /// <summary>
    /// This class is used as a model for the <see cref="ProfileController"/>.
    /// </summary>
    public class ProfileModel : IProfileModel
    {
        #region Properties

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        public SaveAction SaveChangesAction { get; set; }

        /// <inheritdoc />
        public Guid ProfileSavedPageId { get; set; }

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

        /// <inheritDoc/>
        public string ProfileProvider
        {
            get
            {
                this.profileProvider = this.profileProvider ?? UserProfileManager.GetDefaultProviderName();
                return this.profileProvider;
            }
            set
            {
                this.profileProvider = value;
            }
        }

        /// <inheritDoc/>
        public string MembershipProvider
        {
            get
            {
                this.membrshipProvider = this.membrshipProvider ?? UserManager.GetDefaultProviderName();
                return this.membrshipProvider;
            }
            set
            {
                this.membrshipProvider = value;
            }
        }

        /// <inheritDoc/>
        public bool SendEmailOnChangePassword { get; set; }

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

                Guid userId = this.GetUserId();
                UserProfileManager profileManager = UserProfileManager.GetManager(this.ProfileProvider);
                
                if (userId != Guid.Empty)
                {
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
            if (this.SelectedUserProfiles == null || this.SelectedUserProfiles.Count == 0)
                return null;

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
            if (this.SelectedUserProfiles == null || this.SelectedUserProfiles.Count == 0)
                return null;

            var profileFields = this.GetProfileFieldValues();
            var viewModel = new ProfileEditViewModel(profileFields)
            {
                CssClass = this.CssClass,
                CanEdit = this.CanEdit()
            };

            this.InitializeUserRelatedData(viewModel);

            return viewModel;
        }

        /// <summary>
        /// Edits the user profile.
        /// </summary>
        /// <param name="profileProperties">The profile properties.</param>
        public bool EditUserProfile(ProfileEditViewModel model)
        {
            if (!this.CanEdit())
            {
                return false;
            }

            var userProfileManager = UserProfileManager.GetManager(this.ProfileProvider);

            try
            {
                this.EditProfileProperties(model.Profile, userProfileManager);

                this.EditPassword(model);

                this.EditAvatar(model, userProfileManager);

                userProfileManager.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
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

        /// <inheritDoc/>
        public string GetPageUrl(Guid? pageId)
        {
            if (!pageId.HasValue)
            {
                var currentNode = SiteMapBase.GetActualCurrentNode();
                if (currentNode == null)
                    return null;

                pageId = currentNode.Id;
            }

            return HyperLinkHelpers.GetFullPageUrl(pageId.Value);
        }

        /// <inheritDoc/>
        public Guid GetUserId()
        {
            Guid userId;
            if (!this.UserName.IsNullOrEmpty())
            {
                userId = SecurityManager.GetUserId(this.MembershipProvider, this.UserName);
            }
            else
            {
                userId = ClaimsManager.GetCurrentUserId();
            }

            return userId;
        }

        /// <inheritDoc/>
        public virtual void InitializeUserRelatedData(ProfileEditViewModel model)
        {
            model.User = SecurityManager.GetUser(this.GetUserId());

            model.UserName = model.User.UserName;
            model.Email = model.User.Email;
            model.UserName = model.User.UserName;
            Libraries.Model.Image avatarImage;

            var displayNameBuilder = new SitefinityUserDisplayNameBuilder();
            model.DisplayName = displayNameBuilder.GetUserDisplayName(model.User.Id);
            model.AvatarImageUrl = displayNameBuilder.GetAvatarImageUrl(model.User.Id, out avatarImage);
            model.DefaultAvatarUrl = displayNameBuilder.GetAvatarImageUrl(Guid.Empty, out avatarImage);

            model.SelectedUserProfiles = UserProfileManager.GetManager(this.ProfileProvider).GetUserProfiles(model.User).Select(p => new CustomProfileViewModel(p)).ToList();
        }

        /// <inheritDoc/>
        public virtual void ValidateProfileData(ProfileEditViewModel viewModel, System.Web.Mvc.ModelStateDictionary modelState)
        {
            List<ProfileBindingsContract> profileBindingsList = this.GetDeserializedProfileBindings();

            foreach (var profile in this.SelectedUserProfiles)
            {
                var profileBindings = profileBindingsList.Single(p=>p.ProfileType == profile.GetType().FullName);
                var requiredProperties =  profileBindings.Properties.Where(p=> p.Required);

                foreach (var prop in requiredProperties)
                {
                    string propValue;

                    if (!viewModel.Profile.TryGetValue(prop.Name, out propValue) || string.IsNullOrWhiteSpace(propValue))
                    {
                        modelState.AddModelError(string.Format("Profile[{0}]", prop.Name), string.Format(Res.Get<ProfileResources>().RequiredProfileField, prop.Name));
                    }
                }
            }

            var minPassLength = UserManager.GetManager(this.MembershipProvider).MinRequiredPasswordLength;
            if (!string.IsNullOrEmpty(viewModel.OldPassword) && !string.IsNullOrEmpty(viewModel.NewPassword) && !string.IsNullOrEmpty(viewModel.RepeatPassword))
            {
                if (viewModel.NewPassword.Length < minPassLength)
                {
                    modelState.AddModelError("NewPassword", string.Format(Res.Get<ProfileResources>().MinimumPasswordLength, minPassLength));
                }

                if (viewModel.RepeatPassword.Length < minPassLength)
                {
                    modelState.AddModelError("RepeatPassword", string.Format(Res.Get<ProfileResources>().MinimumPasswordLength, minPassLength));
                }
            }
        }
        
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the profile field values.
        /// </summary>
        /// <returns></returns>
        protected virtual IDictionary<string, string> GetProfileFieldValues()
        {
            IDictionary<string, string> profileFields = new Dictionary<string, string>();
            var bindingContract = this.GetDeserializedProfileBindings();

            foreach (var profileBinding in bindingContract)
            {
                var userProfile = this.SelectedUserProfiles.Where(prof => prof.GetType().FullName == profileBinding.ProfileType).SingleOrDefault();
                foreach (var property in profileBinding.Properties)
                {
                    object propValue = userProfile.GetValue(property.FieldName);
                    if (propValue != null)
                    {
                        string propValueAsString = propValue as string;
                        if (propValueAsString != null)
                        {
                            profileFields.Add(property.Name, propValueAsString);
                        }
                        else
                        {
                            profileFields.Add(property.Name, propValue.ToString());
                        }
                    }
                    else
                    {
                        profileFields.Add(property.Name, string.Empty);
                    }
                }
            }

            return profileFields;
        }

        /// <summary>
        /// Edits the profile properties.
        /// </summary>
        /// <param name="profileProperties">The profile properties.</param>
        /// <param name="userProfileManager">The user profile manager.</param>
        private void EditProfileProperties(IDictionary<string, string> profileProperties, UserProfileManager userProfileManager)
        {
            if (profileProperties != null)
            {
                var bindingContract = new JavaScriptSerializer().Deserialize<List<ProfileBindingsContract>>(this.ProfileBindings);

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
                }
            }
        }

        private List<ProfileBindingsContract> GetDeserializedProfileBindings()
        {
            return new JavaScriptSerializer().Deserialize<List<ProfileBindingsContract>>(this.ProfileBindings);
        }

        /// <summary>
        /// Edits the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="System.ArgumentException">Both passwords must match</exception>
        private void EditPassword(ProfileEditViewModel model)
        {
            if (!string.IsNullOrEmpty(model.OldPassword))
            {
                if (model.NewPassword != model.RepeatPassword)
                {
                    throw new ArgumentException("Both passwords must match");
                }

                var userId = this.GetUserId();
                var userManager = UserManager.GetManager(SecurityManager.GetUser(userId).ProviderName);
                UserManager.ChangePasswordForUser(userManager, userId, model.OldPassword, model.NewPassword, this.SendEmailOnChangePassword);
            }
        }

        /// <summary>
        /// Edits the avatar.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userProfileManager">The user profile manager.</param>
        private void EditAvatar(ProfileEditViewModel model, UserProfileManager userProfileManager)
        {
            if (model.UploadedImage != null)
            {
                var image = this.UploadAvatar(model.UploadedImage, model.UserName);
                this.ChangeProfileAvatar(this.GetUserId(), image, userProfileManager);

                Image avatarImage;
                model.AvatarImageUrl = new UserDisplayNameBuilder().GetAvatarImageUrl(model.User.Id, out avatarImage);
            }
        }

        /// <summary>
        /// Changes the profile avatar.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The profile image.</param>
        /// <param name="userProfileManager">The user profile manager.</param>
        private void ChangeProfileAvatar(Guid userId, Image image, UserProfileManager userProfileManager)
        {
            User user = SecurityManager.GetUser(userId);

            if (user != null && image != null)
            {
                SitefinityProfile profile = userProfileManager.GetUserProfile<SitefinityProfile>(user);

                if (profile != null)
                {
                    ContentLink avatarLink = ContentLinksExtensions.CreateContentLink(profile, image);

                    profile.Avatar = avatarLink;

                    // Setting the Avatar does not modify the actual Profile persistent object and cache entries that depend on the Profile are not invalidated.
                    // By setting another property of the Profile we force all cache entries that depend ot this profile to be invalidated.
                    profile.LastModified = DateTime.UtcNow;
                }
            }
        }

        /// <summary>
        /// Uploads the avatar.
        /// </summary>
        /// <param name="uploadedImage">The uploaded image.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        private Image UploadAvatar(HttpPostedFileBase uploadedImage, string username)
        {
            this.ValidateImage(uploadedImage);

            LibrariesManager librariesManager = LibrariesManager.GetManager(LibrariesModule.SystemLibrariesProviderName);

            Image image;
            using (new ElevatedModeRegion(librariesManager))
            {
                image = librariesManager.CreateImage();

                image.Parent = this.GetProfileImagesAlbum(librariesManager);

                image.Title = string.Format("{0}_avatar_{1}", username, Guid.NewGuid());
                image.DateCreated = DateTime.UtcNow;
                image.PublicationDate = DateTime.UtcNow;
                image.LastModified = DateTime.UtcNow;
                image.UrlName = Regex.Replace(image.Title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

                //Upload the image file.
                librariesManager.Upload(image, uploadedImage.InputStream, Path.GetExtension(uploadedImage.FileName));

                image = librariesManager.Lifecycle.Publish(image) as Image;

                //Save the changes.
                librariesManager.SaveChanges();
            }

            return image;
        }

        /// <summary>
        /// Validates the image.
        /// </summary>
        /// <param name="uploadedImage">The uploaded image.</param>
        /// <exception cref="System.ArgumentException">Image type is not allowed</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Image size is too large</exception>
        private void ValidateImage(HttpPostedFileBase uploadedImage)
        {
            var allowedTypes = new string[] { "image/jpg", "image/jpeg", "image/pjpeg", "image/gif", "image/x-png", "image/png" };
            var allowedSize = 25 * 1024 * 1024;

            if (!allowedTypes.Contains(uploadedImage.ContentType))
            {
                throw new ArgumentException("Image type is not allowed");
            }

            if (uploadedImage.ContentLength > allowedSize)
            {
                throw new ArgumentOutOfRangeException("Image size is too large");
            }
        }

        private Album GetProfileImagesAlbum(LibrariesManager manager)
        {
            var album = manager.GetAlbums().FirstOrDefault(l => l.Title == ProfileModel.ProfileImagesAlbumTitle);
            if (album == null)
            {
                album = manager.CreateAlbum();
                album.Title = ProfileModel.ProfileImagesAlbumTitle;
                album.UrlName = ProfileModel.ProfileImagesAlbumUrl;
            }

            return album;
        }

        #endregion

        #region Private fields

        private string profileProvider;
        private string membrshipProvider;
        private IList<UserProfile> selectedUserProfiles;
        private string profileBindings = "[{ProfileType: 'Telerik.Sitefinity.Security.Model.SitefinityProfile',Properties: [{ Name: 'FirstName', FieldName: 'FirstName', Required: true },{ Name: 'LastName', FieldName: 'LastName', Required: true }, {Name:'About', FieldName:'About' } ]}]";

        private const string ProfileImagesAlbumTitle = "Profile images";
        private const string ProfileImagesAlbumUrl = "sys-profile-images";

        #endregion
    }
}
