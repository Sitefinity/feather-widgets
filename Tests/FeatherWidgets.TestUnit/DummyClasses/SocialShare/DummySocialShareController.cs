using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities.DummyClasses.HttpContext;
using Telerik.Sitefinity.SiteSettings.Basic;
using Telerik.Sitefinity.Web.Utilities;

namespace FeatherWidgets.TestUnit.DummyClasses.SocialShare
{
    /// <summary>
    /// This class creates dummy <see cref="SocialShare.Mvc.Controllers.SocialShareController"/>
    /// </summary>
    public class DummySocialShareController : SocialShareController
    {
        public DummySocialShareController(IList<SocialShareGroup> socialShareGroups) 
            : this()
        {
            this.socialShareGroups = socialShareGroups;
        }

        public DummySocialShareController()
        {
            var routeData = new RouteData();
            var httpContext = new DummyHttpContext();
            this.ControllerContext = new ControllerContext(new RequestContext(httpContext, routeData), this);
        }

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

        protected override IList<SocialShareGroup> SocialShareGroups
        {
            get
            {
                if (this.socialShareGroups == null)
                {
                    return base.SocialShareGroups;
                }

                return this.socialShareGroups;
            }
        }

        private readonly IList<SocialShareGroup> socialShareGroups;
    }
}
