using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models;
using Telerik.Sitefinity.SiteSettings.Basic;
using Telerik.Sitefinity.Web.Utilities;

namespace FeatherWidgets.TestUnit.DummyClasses.SocialShare
{
    /// <summary>
    /// This class creates dummy <see cref="SocialShare.Mvc.Controllers.SocialShareController"/>
    /// </summary>
    public class DummySocialShareController : SocialShareController
    {
        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>The <see cref="ISocialShareModel" />.</returns>
        protected override ISocialShareModel InitializeModel()
        {
            return new DummySocialShareModel();
        }

        /// <summary>
        /// Gets the social share settings.
        /// </summary>
        /// <value>The social share settings.</value>
        protected override ISocialShareSettings SocialShareSettings
        {
            get
            {
                var settings = new SocialShareSettingsContract()
                {
                    Facebook = true,
                    Twitter = true,
                    GooglePlusOne = true,
                    LinkedIn = true,
                    Tumblr = true,
                    Digg = false,
                    Blogger = false,
                    GoogleBookmarks = false,
                    Delicious = false,
                    MySpace = false,
                    StumbleUpon = false,
                    Reddit = false,
                    MailTo = false,
                    SocialShareMode = SocialShareMode.SmallIcons
                };

                return settings;
            }
        }
    }
}
