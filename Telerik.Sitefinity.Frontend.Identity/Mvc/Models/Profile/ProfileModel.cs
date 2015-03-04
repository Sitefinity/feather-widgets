using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        #endregion

        #region Public methods

        /// <inheritdoc />
        public ProfileViewModel GetViewModel()
        {
            return new ProfileViewModel();
        }

        #endregion
    }
}
