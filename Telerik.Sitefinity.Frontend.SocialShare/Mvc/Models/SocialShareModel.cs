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
    /// This class represents model used for Social share widget.
    /// </summary>
    public class SocialShareModel : ISocialShareModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareModel" /> class.
        /// </summary>
        public SocialShareModel()
        {
        }

        #region Public Methods
        /// <summary>
        /// Gets the basic settings social share depending on the Sitefinity configurations.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public virtual void InitializeSocialShareButtons(IList<SocialShareGroupMap> socialShareMaps)
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
        #endregion

        #region Properties

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

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets the social share settings.
        /// </summary>
        /// <value>The social share settings.</value>
        protected internal virtual ISocialShareSettings SocialShareSettings
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
