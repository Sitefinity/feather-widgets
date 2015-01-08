using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.Text;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.StringResources;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings.Basic;

namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Social Share widget.
    /// </summary>
    [ControllerToolboxItem(Name = "SocialShare", Title = "Social share", SectionName = "MvcWidgets")]
    [Localization(typeof(SocialShareResources))]
    public class SocialShareController : Controller
    {
        #region Actions
        /// <summary>
        /// Default Action
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            this.Model.InitializeSocialShareButtons(this.SocialShareMap);

            return this.View(this.TemplateName, this.Model);
        }
        #endregion

        #region Overridden methods

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        }

        #endregion

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="ISocialShareModel"/>.
        /// </returns>
        protected virtual ISocialShareModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<ISocialShareModel>(this.GetType());
        }

        #region Properties
        
        /// <summary>
        /// Gets or sets the name of the template that widget will be displayed.
        /// </summary>
        /// <value></value>
        public string TemplateName
        {
            get
            {
                return this.templateName;
            }

            set
            {
                this.templateName = value;
            }
        }

        /// <summary>
        /// Gets the Social share widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ISocialShareModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        /// <summary>
        /// Gets or sets the serialize social share section map.
        /// </summary>
        /// <value>The serialize social share section map.</value>
        public string SerializedSocialShareSectionMap
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.serializedSocialShareSectionMap))
                {
                    this.serializedSocialShareSectionMap = JsonSerializer.SerializeToString(this.SocialShareSectionMap);
                }

                return this.serializedSocialShareSectionMap;
            }

            set
            {
                if (this.serializedSocialShareSectionMap != value)
                {
                    this.serializedSocialShareSectionMap = value;
                }
            }
        }

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

        /// <summary>
        /// Gets the social share map.
        /// </summary>
        /// <value>The social share map.</value>
        protected internal virtual IList<SocialShareGroupMap> SocialShareMap
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.serializedSocialShareSectionMap) ?
                                        this.SocialShareSectionMap :
                                        JsonSerializer.DeserializeFromString<IList<SocialShareGroupMap>>(this.serializedSocialShareSectionMap);
            }
        }

        /// <summary>
        /// Gets or sets the social share section map.
        /// </summary>
        /// <value>The social share section map.</value>
        private IList<SocialShareGroupMap> SocialShareSectionMap
        {
            get
            {
                var socialShareSettings = this.SocialShareSettings;

                var socialShareSectionMap = new List<SocialShareGroupMap>();

                socialShareSectionMap.Add(new SocialShareGroupMap(new List<SocialShareMap> 
                { 
                    { new SocialShareMap("Facebook", socialShareSettings.Facebook) },
                    { new SocialShareMap("Twitter", socialShareSettings.Twitter) },
                    { new SocialShareMap("GooglePlusOne", socialShareSettings.GooglePlusOne) },
                    { new SocialShareMap("LinkedIn", socialShareSettings.LinkedIn) },
                    { new SocialShareMap("Digg", socialShareSettings.Digg) }
                }));

                socialShareSectionMap.Add(new SocialShareGroupMap(new List<SocialShareMap> 
                { 
                    { new SocialShareMap("Blogger", socialShareSettings.Blogger) },
                    { new SocialShareMap("Tumblr", socialShareSettings.Tumblr) },
                    { new SocialShareMap("GoogleBookmarks", socialShareSettings.GoogleBookmarks) },
                    { new SocialShareMap("Delicious", socialShareSettings.Delicious) },
                    { new SocialShareMap("MySpace", socialShareSettings.MySpace) }
                }));

                socialShareSectionMap.Add(new SocialShareGroupMap(new List<SocialShareMap> 
                { 
                    { new SocialShareMap("StumbleUpon", socialShareSettings.StumbleUpon) },
                    { new SocialShareMap("Reddit", socialShareSettings.Reddit) },
                    { new SocialShareMap("MailTo", socialShareSettings.MailTo) }
                }));

                return socialShareSectionMap;
            }
        }
        #endregion

        private ISocialShareModel model;
        private string templateName = "SocialShare";
        private string serializedSocialShareSectionMap;
    }
}
