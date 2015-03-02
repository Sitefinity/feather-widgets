using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Change Password widget
    /// </summary>
    [ObjectInfo(typeof(LoginStatusResources), Title = "ChangePasswordResources", Description = "ChangePasswordResources")]
    public class ChangePasswordResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes new instance of <see cref="ChangePasswordResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ChangePasswordResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public ChangePasswordResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        #region Index

        

        /// <summary>
        /// Gets phrase : You need to be logged in to change your password
        /// </summary>
        [ResourceEntry("LogInFirst",
            Value = "You need to be logged in to change your password",
            Description = "phrase : You need to be logged in to change your password",
            LastModified = "2015/03/02")]
        public string LogInFirst
        {
            get
            {
                return this["LogInFirst"];
            }
        }

        /// <summary>
        /// Gets phrase : Your password is successfully changed
        /// </summary>
        [ResourceEntry("ChangePasswordSuccess",
            Value = "Your password is successfully changed",
            Description = "phrase : Your password is successfully changed",
            LastModified = "2015/03/02")]
        public string ChangePasswordSuccess
        {
            get
            {
                return this["ChangePasswordSuccess"];
            }
        }

        /// <summary>
        /// Gets phrase : Change Password
        /// </summary>
        [ResourceEntry("ChangePasswordHeader",
            Value = "Change Password",
            Description = "phrase : Change Password",
            LastModified = "2015/03/02")]
        public string ChangePasswordHeader
        {
            get
            {
                return this["ChangePasswordHeader"];
            }
        }

        /// <summary>
        /// Gets phrase : Old password
        /// </summary>
        [ResourceEntry("ChangePasswordOldPassword",
            Value = "Old password",
            Description = "phrase : Old password",
            LastModified = "2015/03/02")]
        public string ChangePasswordOldPassword
        {
            get
            {
                return this["ChangePasswordOldPassword"];
            }
        }

        /// <summary>
        /// Gets phrase : New password
        /// </summary>
        [ResourceEntry("ChangePasswordNewPassword",
            Value = "New password",
            Description = "phrase : New password",
            LastModified = "2015/03/02")]
        public string ChangePasswordNewPassword
        {
            get
            {
                return this["ChangePasswordNewPassword"];
            }
        }
        
        /// <summary>
        /// Gets phrase : Repeat new Password
        /// </summary>
        [ResourceEntry("ChangePasswordRepeatPassword",
            Value = "Repeat new Password",
            Description = "phrase : Repeat new Password",
            LastModified = "2015/03/02")]
        public string ChangePasswordRepeatPassword
        {
            get
            {
                return this["ChangePasswordRepeatPassword"];
            }
        }

        /// <summary>
        /// Gets phrase : Save
        /// </summary>
        [ResourceEntry("ChangePasswordSave",
            Value = "Save",
            Description = "phrase : Save",
            LastModified = "2015/03/02")]
        public string ChangePasswordSave
        {
            get
            {
                return this["ChangePasswordSave"];
            }
        }

        #endregion

        #region SetChangePassword



        /// </summary>
        /// Gets phrase : Both passwords must match.
        /// </summary>
        [ResourceEntry("ChangePasswordNonMatchingPasswordsMessage",
            Value = "Both passwords must match.",
            Description = "phrase : Both passwords must match.",
            LastModified = "2015/03/02")]
        public string ChangePasswordNonMatchingPasswordsMessage
        {
            get
            {
                return this["ChangePasswordNonMatchingPasswordsMessage"];
            }
        }

        /// </summary>
        /// Gets phrase : Invalid data.
        /// </summary>
        [ResourceEntry("ChangePasswordGeneralErrorMessage",
            Value = "Invalid data.",
            Description = "phrase : Invalid data.",
            LastModified = "2015/03/02")]
        public string ChangePasswordGeneralErrorMessage
        {
            get
            {
                return this["ChangePasswordGeneralErrorMessage"];
            }
        }

        #endregion

        #region DesignerView

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/03/02")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        #endregion
    }
}
