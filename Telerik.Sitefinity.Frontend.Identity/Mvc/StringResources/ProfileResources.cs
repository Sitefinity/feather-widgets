using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Profile widget
    /// </summary>
    [ObjectInfo(typeof(ProfileResources), Title = "ProfileResources", Description = "ProfileResources")]
    public class ProfileResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileResources"/> class. 
        /// Initializes new instance of <see cref="ProfileResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ProfileResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public ProfileResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/03/04")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets phrase: Profile
        /// </summary>
        [ResourceEntry("Profile",
            Value = "Profile",
            Description = "phrase : Profile",
            LastModified = "2015/03/05")]
        public string Profile
        {
            get
            {
                return this["Profile"];
            }
        }

        /// <summary>
        /// Gets phrase: Editing profile
        /// </summary>
        [ResourceEntry("EditingProfile",
            Value = "Editing profile",
            Description = "phrase : Editing profile",
            LastModified = "2015/03/05")]
        public string EditingProfile
        {
            get
            {
                return this["EditingProfile"];
            }
        }

        /// <summary>
        /// Gets phrase: Templates
        /// </summary>
        [ResourceEntry("Templates",
            Value = "Templates",
            Description = "phrase : Templates",
            LastModified = "2015/03/04")]
        public string Templates
        {
            get
            {
                return this["Templates"];
            }
        }

        /// <summary>
        /// Gets phrase: Template
        /// </summary>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "phrase : Template",
            LastModified = "2015/03/04")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/03/04")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase: Edit mode only
        /// </summary>
        [ResourceEntry("EditModeOnly",
            Value = "Edit mode only",
            Description = "phrase : Edit mode only",
            LastModified = "2015/03/04")]
        public string EditModeOnly
        {
            get
            {
                return this["EditModeOnly"];
            }
        }

        /// <summary>
        /// Gets phrase: Read mode only
        /// </summary>
        [ResourceEntry("ReadModeOnly",
            Value = "Read mode only",
            Description = "phrase : Read mode only",
            LastModified = "2015/03/04")]
        public string ReadModeOnly
        {
            get
            {
                return this["ReadModeOnly"];
            }
        }

        /// <summary>
        /// Gets phrase: Both: Read mode can be edited
        /// </summary>
        [ResourceEntry("BothModes",
            Value = "Both: Read mode can be edited",
            Description = "phrase : Both: Read mode can be edited",
            LastModified = "2015/03/04")]
        public string BothModes
        {
            get
            {
                return this["BothModes"];
            }
        }

        /// <summary>
        /// Gets phrase: When the changes are saved...
        /// </summary>
        [ResourceEntry("WhenChangesAreSaved",
            Value = "When the changes are saved...",
            Description = "phrase : When the changes are saved...",
            LastModified = "2015/03/04")]
        public string WhenChangesAreSaved
        {
            get
            {
                return this["WhenChangesAreSaved"];
            }
        }

        /// <summary>
        /// Gets phrase: Switch to 'Read mode'
        /// </summary>
        [ResourceEntry("SwitchToReadMode",
            Value = "Switch to 'Read mode'",
            Description = "phrase : Switch to 'Read mode'",
            LastModified = "2015/03/04")]
        public string SwitchToReadMode
        {
            get
            {
                return this["SwitchToReadMode"];
            }
        }

        /// <summary>
        /// Gets phrase: Show a message above the form
        /// </summary>
        [ResourceEntry("ShowMsg",
            Value = "Show a message above the form",
            Description = "phrase : Show message",
            LastModified = "2015/03/04")]
        public string ShowMsg
        {
            get
            {
                return this["ShowMsg"];
            }
        }

        /// <summary>
        /// Gets phrase: Open a specially prepared page...
        /// </summary>
        [ResourceEntry("OpenPage",
            Value = "Open a specially prepared page...",
            Description = "phrase : Open a specially prepared page...",
            LastModified = "2015/03/04")]
        public string OpenPage
        {
            get
            {
                return this["OpenPage"];
            }
        }

        /// <summary>
        /// Gets phrase: Upload photo
        /// </summary>
        [ResourceEntry("EditProfileUploadImage",
            Value = "Upload photo",
            Description = "phrase : Upload photo",
            LastModified = "2015/03/05")]
        public string EditProfileUploadImage
        {
            get
            {
                return this["EditProfileUploadImage"];
            }
        }

        /// <summary>
        /// Gets phrase: First name
        /// </summary>
        [ResourceEntry("EditProfileFirstName",
            Value = "First name",
            Description = "phrase : First name",
            LastModified = "2015/03/05")]
        public string EditProfileFirstName
        {
            get
            {
                return this["EditProfileFirstName"];
            }
        }

        /// <summary>
        /// Gets phrase: Last name
        /// </summary>
        [ResourceEntry("EditProfileLastName",
            Value = "Last name",
            Description = "phrase : Last name",
            LastModified = "2015/03/05")]
        public string EditProfileLastName
        {
            get
            {
                return this["EditProfileLastName"];
            }
        }

        /// <summary>
        /// Gets phrase: Email
        /// </summary>
        [ResourceEntry("EditProfileEmail",
            Value = "Email",
            Description = "phrase : Email",
            LastModified = "2015/03/05")]
        public string EditProfileEmail
        {
            get
            {
                return this["EditProfileEmail"];
            }
        }

        /// <summary>
        /// Gets phrase: User name
        /// </summary>
        [ResourceEntry("EditProfileUsername",
            Value = "User name",
            Description = "phrase : User name",
            LastModified = "2015/03/05")]
        public string EditProfileUsername
        {
            get
            {
                return this["EditProfileUsername"];
            }
        }

        /// <summary>
        /// Gets phrase: About
        /// </summary>
        [ResourceEntry("EditProfileAbout",
            Value = "About",
            Description = "phrase : About",
            LastModified = "2015/03/05")]
        public string EditProfileAbout
        {
            get
            {
                return this["EditProfileAbout"];
            }
        }

        /// <summary>
        /// Gets phrase: Change password
        /// </summary>
        [ResourceEntry("EditProfileChangePasswordButton",
            Value = "Change password",
            Description = "phrase : Change password",
            LastModified = "2015/03/05")]
        public string EditProfileChangePasswordButton
        {
            get
            {
                return this["EditProfileChangePasswordButton"];
            }
        }

        /// <summary>
        /// Gets phrase: Change password
        /// </summary>
        [ResourceEntry("EditProfileEditChangePasswordHeader",
            Value = "Change password",
            Description = "phrase : Change password",
            LastModified = "2015/03/05")]
        public string EditProfileEditChangePasswordHeader
        {
            get
            {
                return this["EditProfileEditChangePasswordHeader"];
            }
        }

        /// <summary>
        /// Gets phrase: Old password
        /// </summary>
        [ResourceEntry("EditProfileOldPassword",
            Value = "Old password",
            Description = "phrase : Old password",
            LastModified = "2015/03/05")]
        public string EditProfileOldPassword
        {
            get
            {
                return this["EditProfileOldPassword"];
            }
        }

        /// <summary>
        /// Gets phrase: New password
        /// </summary>
        [ResourceEntry("EditProfileNewPassword",
            Value = "New password",
            Description = "phrase : New password",
            LastModified = "2015/03/05")]
        public string EditProfileNewPassword
        {
            get
            {
                return this["EditProfileNewPassword"];
            }
        }

        /// <summary>
        /// Gets phrase: Repeat new password
        /// </summary>
        [ResourceEntry("EditProfileRepeatPassword",
            Value = "Repeat new password",
            Description = "phrase : Repeat new password",
            LastModified = "2015/03/05")]
        public string EditProfileRepeatPassword
        {
            get
            {
                return this["EditProfileRepeatPassword"];
            }
        }

        /// <summary>
        /// Gets phrase: Save changes
        /// </summary>
        [ResourceEntry("EditProfileSave",
            Value = "Save changes",
            Description = "phrase : Save changes",
            LastModified = "2015/03/05")]
        public string EditProfileSave
        {
            get
            {
                return this["EditProfileSave"];
            }
        }

        /// <summary>
        /// Gets phrase: Cancel
        /// </summary>
        [ResourceEntry("EditProfileCancel",
            Value = "Cancel",
            Description = "phrase : Cancel",
            LastModified = "2015/03/05")]
        public string EditProfileCancel
        {
            get
            {
                return this["EditProfileCancel"];
            }
        }

        /// <summary>
        /// Gets phrase: Edit profile
        /// </summary>
        [ResourceEntry("EditProfileLink",
            Value = "Edit profile",
            Description = "phrase : Edit profile",
            LastModified = "2015/03/05")]
        public string EditProfileLink
        {
            get
            {
                return this["EditProfileLink"];
            }
        }

        /// <summary>
        /// Gets phrase: Edit is not allowed!
        /// </summary>
        [ResourceEntry("EditNotAllowed",
            Value = "Edit is not allowed!",
            Description = "phrase : Edit is not allowed!",
            LastModified = "2015/03/05")]
        public string EditNotAllowed
        {
            get
            {
                return this["EditNotAllowed"];
            }
        }

        /// <summary>
        /// Gets phrase: Your changes are saved
        /// </summary>
        [ResourceEntry("ChangesAreSaved",
            Value = "Your changes are saved",
            Description = "phrase : Your changes are saved",
            LastModified = "2015/03/06")]
        public string ChangesAreSaved
        {
            get
            {
                return this["ChangesAreSaved"];
            }
        }

        /// <summary>
        /// Gets phrase: The {0} profile field is required!
        /// </summary>
        [ResourceEntry("RequiredProfileField",
            Value = "The {0} profile field is required!",
            Description = "phrase : The {0} profile field is required!",
            LastModified = "2015/03/11")]
        public string RequiredProfileField
        {
            get
            {
                return this["RequiredProfileField"];
            }
        }

        /// <summary>
        /// Gets phrase: Password length must be at least {0} symbols long!
        /// </summary>
        [ResourceEntry("MinimumPasswordLength",
            Value = "Password length must be at least {0} symbols long!",
            Description = "phrase : Password length must be at least {0} symbols long!",
            LastModified = "2015/03/13")]
        public string MinimumPasswordLength
        {
            get
            {
                return this["MinimumPasswordLength"];
            }
        }
    }
}
