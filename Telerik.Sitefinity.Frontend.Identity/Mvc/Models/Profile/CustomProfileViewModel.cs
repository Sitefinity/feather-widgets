using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile
{
    public class CustomProfileViewModel
    {
        public CustomProfileViewModel(UserProfile userProfile)
        {
            this.Fields = new DynamicUserProfileFieldAccessor(userProfile);
            this.UserProfile = userProfile;
        }

        public dynamic Fields { get; internal set; }

        public UserProfile UserProfile { get; set; }
    }
}
