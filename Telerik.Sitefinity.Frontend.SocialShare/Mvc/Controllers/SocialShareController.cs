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
    [ControllerToolboxItem(Name = "SocialShare_MVC", Title = "Social share", SectionName = "Social", CssClass = SocialShareController.WidgetIconCssClass)]
    [Localization(typeof(SocialShareResources))]
    public class SocialShareController : Controller
    {
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
        /// Gets or sets the serialized list of social share options.
        /// </summary>
        public string SerializedSocialShareOptionsList
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.serializedSocialShareOptionsList))
                {
                    this.serializedSocialShareOptionsList = JsonSerializer.SerializeToString(this.SocialShareGroups);
                }

                return this.serializedSocialShareOptionsList;
            }

            set
            {
                if (this.serializedSocialShareOptionsList != value)
                {
                    this.serializedSocialShareOptionsList = value;
                }
            }
        }

        /// <summary>
        /// Gets the social share settings.
        /// </summary>
        protected virtual ISocialShareSettings SocialShareSettings
        {
            get
            {
                return SystemManager.CurrentContext.GetSetting<SocialShareSettingsContract, ISocialShareSettings>();
            }
        }

        /// <summary>
        /// Gets the list of <see cref="SocialShareGroup" /> objects.
        /// </summary>
        protected virtual IList<SocialShareGroup> SocialShareGroups
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.serializedSocialShareOptionsList) ?
                                        this.SocialShareOptionsList :
                                        JsonSerializer.DeserializeFromString<IList<SocialShareGroup>>(this.serializedSocialShareOptionsList);
            }
        }

        /// <summary>
        /// Gets the list of <see cref="SocialShareGroup" /> objects based on Sitefinity settings.
        /// </summary>
        private IList<SocialShareGroup> SocialShareOptionsList
        {
            get
            {
                var socialShareSettings = this.SocialShareSettings;
                var socialShareOptionsList = new List<SocialShareGroup>();

                socialShareOptionsList.Add(new SocialShareGroup(new List<SocialShareOption> 
                { 
                    { new SocialShareOption("Facebook", socialShareSettings.Facebook) },
                    { new SocialShareOption("Twitter", socialShareSettings.Twitter) },
                    { new SocialShareOption("GooglePlusOne", socialShareSettings.GooglePlusOne) },
                    { new SocialShareOption("LinkedIn", socialShareSettings.LinkedIn) },
                    { new SocialShareOption("Digg", socialShareSettings.Digg) }
                }));

                socialShareOptionsList.Add(new SocialShareGroup(new List<SocialShareOption> 
                { 
                    { new SocialShareOption("Blogger", socialShareSettings.Blogger) },
                    { new SocialShareOption("Tumblr", socialShareSettings.Tumblr) },
                    { new SocialShareOption("GoogleBookmarks", socialShareSettings.GoogleBookmarks) },
                    { new SocialShareOption("Delicious", socialShareSettings.Delicious) },
                    { new SocialShareOption("MySpace", socialShareSettings.MySpace) }
                }));

                socialShareOptionsList.Add(new SocialShareGroup(new List<SocialShareOption> 
                { 
                    { new SocialShareOption("StumbleUpon", socialShareSettings.StumbleUpon) },
                    { new SocialShareOption("Reddit", socialShareSettings.Reddit) },
                    { new SocialShareOption("MailTo", socialShareSettings.MailTo) }
                }));

                return socialShareOptionsList;
            }
        }
        #endregion

        #region Actions

        /// <summary>
        /// Default Action
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            this.Model.InitializeSocialShareButtons(this.SocialShareGroups);

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

        internal const string WidgetIconCssClass = "sfPageSharingIcn sfMvcIcn";
        private ISocialShareModel model;
        private string templateName = "SocialShare";
        private string serializedSocialShareOptionsList;
    }
}
