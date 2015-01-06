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
        /// Initializes a new instance of the <see cref="SocialShareModel" /> class.
        /// </summary>
        /// <param name="socialShareMap">The social share map.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SocialShareModel(IList<SocialShareMap> socialShareMap)
        {
            this.InitializeSocialShareButton(socialShareMap);
        }

        #region Helper methods

        /// <summary>
        /// Gets the basic settings social share depending on the Sitefinity configurations.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        protected virtual void InitializeSocialShareButton(IList<SocialShareMap> socialShareMaps)
        {
            var settings = this.GetSocialShareSection();
            bool addText = settings.SocialShareMode == SocialShareMode.IconsWithText;
            bool bigSize = settings.SocialShareMode == SocialShareMode.BigIcons;
            bool displayCounters = settings.DisplayCounters && addText;

            foreach (SocialShareMap socialShareMap in socialShareMaps)
            {
                foreach (var property in socialShareMap.Groups.Where(i => i.Value))
                {
                    this.SocialButtons.Add(new SocialButtonModel(property.Key.ToPascalCase(), addText, displayCounters, bigSize));
                }
            }
        }

        private ISocialShareSettings GetSocialShareSection()
        {
            return SystemManager.CurrentContext.GetSetting<SocialShareSettingsContract, ISocialShareSettings>();
        }

        #endregion

        private List<SocialButtonModel> socialButtons;
    }
}
