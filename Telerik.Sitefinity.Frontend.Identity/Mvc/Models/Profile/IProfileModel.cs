using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile
{
    /// <summary>
    /// This interface is used as a model for the ProfileController.
    /// </summary>
    public interface IProfileModel
    {
        /// <summary>
        /// Gets or sets the css class that will be applied on the wrapping element of the widget.
        /// </summary>
        /// <value>
        /// The css class value as a string.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the save changes action.
        /// </summary>
        /// <value>
        /// The save changes action.
        /// </value>
        SaveAction SaveChangesAction { get; set; }

        /// <summary>
        /// Gets or sets the page identifier where the widget will redirect when profile is saved .
        /// </summary>
        /// <value>
        /// The profile saved page identifier.
        /// </value>
        Guid ProfileSavedPageId { get; set; }
        
        /// <summary>
        /// Gets or sets the profile provider.
        /// </summary>
        /// <value>
        /// The profile provider.
        /// </value>
        string ProfileProvider { get; set; }

        /// <summary>
        /// Gets or sets the membership provider.
        /// </summary>
        /// <value>
        /// The user provider.
        /// </value>
        string MembershipProvider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an email should be send on password change.
        /// </summary>
        /// <value>
        /// <c>true</c> if email should be send; otherwise, <c>false</c>.
        /// </value>
        bool SendEmailOnChangePassword { get; set; }

        /// <summary>
        /// Gets or sets the name of the user whose profile will be displayed in profile widget.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the profile property bindings.
        /// </summary>
        /// <value>
        /// The profile property bindings.
        /// </value>
        string ProfileBindings { get; set; }

        /// <summary>
        /// Gets the <see cref="ProfilePreviewViewModel"/>.
        /// </summary>
        /// <returns>
        /// A view model.
        /// </returns>
        ProfilePreviewViewModel GetProfilePreviewViewModel();

        /// <summary>
        /// Gets the <see cref="ProfileEditViewModel"/>.
        /// </summary>
        /// <returns>
        /// A view model.
        /// </returns>
        ProfileEditViewModel GetProfileEditViewModel();

        /// <summary>
        /// Determines whether current user can edit the profile.
        /// </summary>
        /// <returns></returns>
        bool CanEdit();

        /// <summary>
        /// Edits the user profile.
        /// </summary>
        /// <param name="profileProperties">The profile properties.</param>
        bool EditUserProfile(ProfileEditViewModel model);

        /// <summary>
        /// Gets the page URL.
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>
        /// The page url as string.
        /// </returns>
        string GetPageUrl(Guid? pageId);

        /// <summary>
        /// Gets the Id of the user that owns the profiles.
        /// </summary>
        /// <returns>The user id.</returns>
        Guid GetUserId();

        /// <summary>
        /// Updates the profile edit view model with the current User data.
        /// </summary>
        /// <param name="model">The model.</param>
        void InitializeUserRelatedData(ProfileEditViewModel model);

        /// <summary>
        /// Validates the profile related data.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="modelState">The modelState dictionary.</param>
        void ValidateProfileData(ProfileEditViewModel viewModel, System.Web.Mvc.ModelStateDictionary modelState);
    }
}
