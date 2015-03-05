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
        /// Gets or sets the profile saved message.
        /// </summary>
        /// <value>
        /// Message to show when profile is saved.
        /// </value>
        string ProfileSaveMsg { get; set; }

        /// <summary>
        /// Gets or sets the name of the user whose profile will be displayed in profile widget.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Gets the <see cref="ProfileViewModel"/>.
        /// </summary>
        /// <returns>
        /// A view model.
        /// </returns>
        ProfileViewModel GetViewModel();

        /// <summary>
        /// Edits the user profile.
        /// </summary>
        /// <param name="profileProperties">The profile properties.</param>
        bool EditUserProfile(IDictionary<string, object> profileProperties);

        string ProfileBindings { get; set; }
    }
}
