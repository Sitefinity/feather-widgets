using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings.Basic;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Social Share Model
    /// </summary>
    public class SocialShareModel
    {
        #region Public members

        /// <summary>
        /// Gets the social buttons
        /// </summary>
        public ICollection<SocialButtonModel> SocialButtons
        {
            get
            {
                return this.socialButtons ?? (this.socialButtons = new List<SocialButtonModel>());
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareModel"/> class based on the Sitefinity basic settings.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public SocialShareModel()
        {
            this.GetBasicSettingsSocialShare();
        }

        #region Helper methods

        /// <summary>
        /// Gets the basic settings social share depending on the Sitefinity configurations.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        internal void GetBasicSettingsSocialShare()
        {
            var settings = this.GetSocialShareSection();
            var properties = typeof(ISocialShareSettings).GetProperties().Where(p => p.PropertyType.Equals(typeof(bool)) && p.Name != "DisplayCounters");
            bool addText = settings.SocialShareMode == SocialShareMode.IconsWithText;
            bool bigSize = settings.SocialShareMode == SocialShareMode.BigIcons;
            bool displayCounters = settings.DisplayCounters && addText;
            foreach (var property in properties)
            {
                if ((bool)property.GetValue(settings, null))
                    this.SocialButtons.Add(new SocialButtonModel(property.Name.ToPascalCase(), addText, displayCounters, bigSize));
            }
        }

        internal ISocialShareSettings GetSocialShareSection()
        {
            return SystemManager.CurrentContext.GetSetting<SocialShareSettingsContract, ISocialShareSettings>();
        }

        #endregion

        private List<SocialButtonModel> socialButtons;
    }
}
