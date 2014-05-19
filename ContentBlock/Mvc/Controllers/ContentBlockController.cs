﻿using ContentBlock.Mvc.Models;
using ContentBlock.Mvc.StringResources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace ContentBlock.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "ContentBlock", Title = "ContentBlock", SectionName = "MvcWidgets")]
    [Localization(typeof(ContentBlockResources))]
    public class ContentBlockController : Controller, IZoneEditorReloader, ICustomWidgetVisualization, ICustomWidgetTitlebar, IHasEditCommands, IContentItemControl
    {
       
        #region Properties

        /// <summary>
        /// Gets or sets the HTML content to be displayed by the ContentBlock control.
        /// </summary>
        [DynamicLinksContainer]
        public virtual string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
                this.IsEmpty = string.IsNullOrEmpty(this.content);
            }
        }

        /// <summary>
        /// Gets or sets the ID of the ContentBlockItem if the Content is shared across multiple controls
        /// </summary>
        public Guid SharedContentID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        public string ProviderName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current content version.
        /// </summary>
        /// <value>The version.</value>
        [Browsable(false)]
        public int ContentVersion
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current mode of the control.
        /// </summary>
        public bool EnableSocialSharing { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        protected IContentBlockModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = this.InitializeModel();
                }
                return this.model;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            this.IsEmpty = string.IsNullOrEmpty(this.Model.Content);
            this.SharedContentID = this.Model.SharedContentID;
            this.InitializeCommands();

            return View("Default", this.Model);
        }

        /// <summary>
        /// Shares the ContentItem
        /// </summary>
        /// <returns></returns>
        public ActionResult Share()
        {
            ViewBag.BlankDataItem = JsonConvert.SerializeObject(this.Model.CreateBlankDataItem());
            return View("Share");
        }

        /// <summary>
        /// Making the content item not shared
        /// </summary>
        /// <returns></returns>
        public ActionResult Unshare()
        {
            return View("UnshareAlert");
        }

        /// <summary>
        /// Uses the shared content item.
        /// </summary>
        /// <returns></returns>
        public ActionResult UseSharedContentItem()
        {
            return View("SharedContentItemSelector", this.Model);
        }

        #endregion

        #region IZoneEditorReloader

        /// <summary>
        /// Defines whether controls of same key will be
        /// reloaded when this control is updated in the ZoneEditor
        /// </summary>
        /// <returns>True if a reload is required</returns>
        [Browsable(false)]
        bool IZoneEditorReloader.ShouldReloadControlsWithSameKey()
        {
            return this.SharedContentID != Guid.Empty;
        }

        /// <summary>
        /// Gets unique reload data (i.e. all controls with the same key will get reloaded)
        /// </summary>
        /// <value></value>
        [Browsable(false)]
        string IZoneEditorReloader.Key
        {
            get
            {
                var id = this.SharedContentID.ToString("N", CultureInfo.InvariantCulture);
                var key = "ContentBlock_" + id;
                return key;
            }
        }

        #endregion

        #region ICustomWidgetVisualization

        /// <summary>
        /// Gets or sets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return this.isEmpty;
            }
            protected set
            {
                this.isEmpty = value;
            }
        }

        /// <summary>
        /// Gets the empty link text.
        /// </summary>
        /// <value>The empty link text.</value>
        [Browsable(false)]
        public string EmptyLinkText
        {
            get
            {
                return Res.Get<ContentBlockResources>().CreateContent;
            }
        }

        #endregion

        #region ICustomWidgetTitlebar Members

        /// <summary>
        /// Gets a list with custom messages which will be applied to dock titlebar.
        /// </summary>
        /// <value>The custom messages.</value>
        [Browsable(false)]
        [TypeConverter(typeof(StringListConverter))]
        public virtual IList<string> CustomMessages
        {
            get
            {
                var messages = new List<string>();
                if (this.SharedContentID != Guid.Empty)
                {
                    messages.Add(Res.Get<ContentBlockResources>().Shared);
                }
                return messages;
            }
        }


        #endregion

        #region IHasEditCommands Members

        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        /// <value>
        /// The commands.
        /// </value>
        [Browsable(false)]
        public IList<WidgetMenuItem> Commands
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes the commands.
        /// </summary>
        protected virtual void InitializeCommands()
        {
            var shareActionLink = RouteHelper.ResolveUrl(string.Format(ContentBlockController.ActionTemplate, "Share"), UrlResolveOptions.Rooted);
            var unshareActionLink = RouteHelper.ResolveUrl(string.Format(ContentBlockController.ActionTemplate, "Unshare"), UrlResolveOptions.Rooted);
            var useSharedActionLink = RouteHelper.ResolveUrl(string.Format(ContentBlockController.ActionTemplate, "UseSharedContentItem"), UrlResolveOptions.Rooted);

            this.Commands = new List<WidgetMenuItem>();
            this.Commands.Add(new WidgetMenuItem() { Text = Res.Get<Labels>("Delete", Res.CurrentBackendCulture), CommandName = "beforedelete", CssClass = "sfDeleteItm" });
            this.Commands.Add(new WidgetMenuItem() { Text = Res.Get<Labels>("Duplicate", Res.CurrentBackendCulture), CommandName = "duplicate", CssClass = "sfDuplicateItm" });
            if (this.SharedContentID == Guid.Empty)
                this.Commands.Add(new WidgetMenuItem() { Text = Res.Get<ContentBlockResources>().Share, ActionUrl = shareActionLink, NeedsModal = true });
            else
                this.Commands.Add(new WidgetMenuItem() { Text = Res.Get<ContentBlockResources>().Unshare, ActionUrl = unshareActionLink, NeedsModal = true });
            this.Commands.Add(new WidgetMenuItem() { Text = "Use Shared", ActionUrl = useSharedActionLink, NeedsModal = true });
            this.Commands.Add(new WidgetMenuItem() { Text = Res.Get<Labels>("Permissions", Res.CurrentBackendCulture), CommandName = "permissions", CssClass = "sfPermItm" });
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns></returns>
        private IContentBlockModel InitializeModel()
        {
            var assemblies = new List<Assembly>();
            var contentBlockControllerAssembly = typeof(ContentBlockController).Assembly;
            var currentAssembly = this.GetType().Assembly;
            
            assemblies.Add(currentAssembly);
            if (!contentBlockControllerAssembly.Equals(currentAssembly))
                assemblies.Add(contentBlockControllerAssembly);

            var constructorParameters = new Dictionary<string, object> 
                        {
                           {"providerName", this.ProviderName},
                           {"content", this.content},
                           {"enableSocialSharing", this.EnableSocialSharing},
                           {"sharedContentId", this.SharedContentID}
                        };


            return ControllerModelFactory.GetModel<IContentBlockModel>(assemblies, constructorParameters) as IContentBlockModel;
        }

        #endregion

        #region Private fields

        private bool isEmpty = true;
        private string content = "";
        private IContentBlockModel model;
        private const string contentItemsServiceUrl = "~/Sitefinity/Services/Content/ContentItemService.svc/";
        internal const string ActionTemplate = "ContentBlock/ContentBlock/{0}";

        #endregion
    }
}
