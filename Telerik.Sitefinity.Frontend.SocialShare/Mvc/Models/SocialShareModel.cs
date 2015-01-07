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
    public class SocialShareModel : ISocialShareModel
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
        public SocialShareModel()
        {
        }

        #region Helper methods

        /// <summary>
        /// Gets the basic settings social share depending on the Sitefinity configurations.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public virtual void InitializeSocialShareButton(IList<SocialShareGroupMap> socialShareMaps)
        {
            ISocialShareSettings settings = this.SocialShareSettings;
            bool addText = settings.SocialShareMode == SocialShareMode.IconsWithText;
            bool bigSize = settings.SocialShareMode == SocialShareMode.BigIcons;
            bool displayCounters = settings.DisplayCounters && addText;

            foreach (SocialShareGroupMap socialShareMap in socialShareMaps)
            {
                foreach (var property in socialShareMap.Groups.Where(i => i.IsChecked))
                {
                    this.SocialButtons.Add(new SocialButtonModel(property.Key.ToPascalCase(), addText, displayCounters, bigSize));
                }
            }
        }

        public string CssClass { get; set; }

        /// <summary>
        /// Gets the social share settings.
        /// </summary>
        /// <value>The social share settings.</value>
        protected virtual ISocialShareSettings SocialShareSettings
        {
            get
            {
                return SystemManager.CurrentContext.GetSetting<SocialShareSettingsContract, ISocialShareSettings>();
            }
        }

        #endregion

        private List<SocialButtonModel> socialButtons;
    }
}
