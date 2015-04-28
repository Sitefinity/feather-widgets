using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.UsersList
{
    /// <summary>
    /// This class represents view model for sitefinity profile items.
    /// </summary>
    public class SitefinityProfileItemViewModel : ItemViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SitefinityProfileItemViewModel" /> class.
        /// </summary>
        /// <param name="dataItem">The data item.</param>
        public SitefinityProfileItemViewModel(IDataItem dataItem)
            : base(dataItem)
        {
            var sfProfile = dataItem as SitefinityProfile;
            if (sfProfile == null)
            {
                throw new ArgumentNullException("dataItem is not a SitefinityProfile object");
            }

            var displayNameBuilder = new SitefinityUserDisplayNameBuilder();
            Libraries.Model.Image avatarImage;

            this.AvatarImageUrl = displayNameBuilder.GetAvatarImageUrl(sfProfile.User.Id, out avatarImage);

            this.About = sfProfile.About;
        }

        /// <summary>
        /// Gets or sets the avatar image URL.
        /// </summary>
        /// <value>The avatar image URL.</value>
        public string AvatarImageUrl { get; private set; }

        /// <summary>
        /// Gets or sets the about.
        /// </summary>
        /// <value>The about.</value>
        public string About { get; private set; }
    }
}
