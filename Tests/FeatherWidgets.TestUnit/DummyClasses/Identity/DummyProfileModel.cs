using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile;

namespace FeatherWidgets.TestUnit.DummyClasses.Identity
{
    /// <summary>
    /// This class is used for testing the profile controller.
    /// </summary>
    public class DummyProfileModel : IProfileModel
    {
        public string CssClass { get; set; }

        public SaveAction SaveChangesAction { get; set; }

        public Guid ProfileSavedPageId { get; set; }

        public string ProfileSaveMsg { get; set; }

        public string UserName { get; set; }

        public string ProfileBindings { get; set; }

        public ProfilePreviewViewModel GetProfilePreviewViewModel()
        {
            return new ProfilePreviewViewModel();
        }

        public ProfileEditViewModel GetProfileEditViewModel()
        {
            return new ProfileEditViewModel();
        }

        public bool CanEdit()
        {
            return true;
        }

        public bool EditUserProfile(IDictionary<string, string> profileProperties)
        {
            return true;
        }
    }
}
