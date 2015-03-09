using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Libraries.Model;
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
                pageId = SiteMapBase.GetActualCurrentNode().Id;
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

        #endregion

        #region Private Methods

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
                var imageId = this.UploadAvatar(model.UploadedImage, model.UserName);
                this.ChangeProfileAvatar(this.GetUserId(), imageId, userProfileManager);
            }
        }

        /// <summary>
        /// Changes the profile avatar.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="imageId">The image identifier.</param>
        /// <param name="userProfileManager">The user profile manager.</param>
        private void ChangeProfileAvatar(Guid userId, Guid imageId, UserProfileManager userProfileManager)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();

            User user = SecurityManager.GetUser(userId);

            if (user != null)
            {
                SitefinityProfile profile = userProfileManager.GetUserProfile<SitefinityProfile>(user);

                if (profile != null)
                {
                    Image avatarImage = librariesManager.GetImages().Where(i => i.Id == imageId).SingleOrDefault();

                    if (avatarImage != null)
                    {
                        ContentLink avatarLink = ContentLinksExtensions.CreateContentLink(profile, avatarImage);

                        profile.Avatar = avatarLink;
                    }
                }
            }
        }

        /// <summary>
        /// Uploads the avatar.
        /// </summary>
        /// <param name="uploadedImage">The uploaded image.</param>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        private Guid UploadAvatar(HttpPostedFileBase uploadedImage, string username)
        {
            this.ValidateImage(uploadedImage);

            LibrariesManager librariesManager = LibrariesManager.GetManager();

            var image = librariesManager.CreateImage();

            // TODO: Use library from a selector.
            image.Parent = librariesManager.GetAlbums().First();

            image.Title = string.Format("{0}_avatar_{1}", username, Guid.NewGuid());
            image.DateCreated = DateTime.UtcNow;
            image.PublicationDate = DateTime.UtcNow;
            image.LastModified = DateTime.UtcNow;
            image.UrlName = Regex.Replace(image.Title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");

            //Upload the image file.
            librariesManager.Upload(image, uploadedImage.InputStream, Path.GetExtension(uploadedImage.FileName));

            //Save the changes.
            librariesManager.SaveChanges();

            //Publish the Albums item. The live version acquires new ID.
            var bag = new Dictionary<string, string>();
            bag.Add("ContentType", typeof(Image).FullName);
            WorkflowManager.MessageWorkflow(image.Id, typeof(Image), null, "Publish", false, bag);

            return image.Id;
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

        #endregion

        #region Private fields

        private string profileProvider;
        private string membrshipProvider;
        private IList<UserProfile> selectedUserProfiles;
        private string profileBindings = "[{ProfileType: 'Telerik.Sitefinity.Security.Model.SitefinityProfile',Properties: [{ Name: 'FirstName', FieldName: 'FirstName' },{ Name: 'LastName', FieldName: 'LastName' }, {Name:'About', FieldName:'About'} ]}]";

        #endregion
    }
}
