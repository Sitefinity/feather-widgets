using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings.Basic;
using Telerik.Sitefinity.Web.Utilities;

namespace SocialShare.Mvc.Models
{
    public class SocialShareModel
    {
        #region Public members

        /// <summary>
        /// The social buttons
        /// </summary>
        public List<SocialButtonModel> SocialButtons = new List<SocialButtonModel>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareModel"/> class based on the Sitefinity basic settings.
        /// </summary>
        public SocialShareModel()
        {
            this.GetBasicSettingsSocialShare();
        }

        #region Helper methods

        /// <summary>
        /// Gets the basic settings social share depending on the Sitefinity configurations.
        /// </summary>
        internal void GetBasicSettingsSocialShare()
        {
            var settings = GetSocialShareSection();
            var properties = typeof(ISocialShareSettings).GetProperties().Where(p => p.PropertyType.Equals(typeof(Boolean)) && p.Name != "DisplayCounters");
            bool addText = settings.SocialShareMode == SocialShareMode.IconsWithText;
            bool bigSize = settings.SocialShareMode == SocialShareMode.BigIcons;
            bool displayCounters = settings.DisplayCounters && addText;
            foreach (var property in properties)
            {
                if ((bool)property.GetValue(settings, null))
                    SocialButtons.Add(new SocialButtonModel(property.Name.ToPascalCase(), addText, displayCounters, bigSize));
            }
        }

        internal static ISocialShareSettings GetSocialShareSection()
        {
            return SystemManager.CurrentContext.GetSetting<SocialShareSettingsContract, ISocialShareSettings>();
        }

        #endregion
    }
}
