﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Models;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Resources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers
{
    /// <summary>
    ///     The content block controller
    /// </summary>
    [ControllerToolboxItem(Name = "ContentBlock_MVC", Title = "Content block", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = ContentBlockController.WidgetIconCssClass)]
    [ControllerToolboxItem(Name = "MvcInstructionalTextField", Title = "Instructional Text", Toolbox = "FormControls", SectionName = "Common")]
    [Localization(typeof(ContentBlockResources))]
    public class ContentBlockController : Controller, 
                                          IHasContainerType,
                                          IZoneEditorReloader,
                                          ICustomWidgetVisualizationExtended, 
                                          ICustomWidgetTitlebar, 
                                          IHasEditCommands, 
                                          IContentItemControl
    {
        #region Explicit Interface Properties

        /// <summary>
        ///     Gets unique reload data (i.e. all controls with the same key will get reloaded)
        /// </summary>
        /// <value></value>
        [Browsable(false)]
        string IZoneEditorReloader.Key
        {
            get
            {
                var id = this.SharedContentID.ToString("N", CultureInfo.InvariantCulture);
                var key = string.Format(CultureInfo.InvariantCulture, IZoneEditorReloaderKeyStringFormat, id);
                return key;
            }
        }

        /// <summary>
        ///     Gets the empty link text.
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

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is empty.
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
        /// Gets the widget CSS class.
        /// </summary>
        /// <value>
        /// The widget CSS class.
        /// </value>
        [Browsable(false)]
        public string WidgetCssClass
        {
            get
            {
                return ContentBlockController.WidgetIconCssClass;
            }
        }

        /// <summary>
        ///     Gets a list with custom messages which will be applied to dock title bar.
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

        /// <summary>
        ///     Gets or sets the widget commands.
        /// </summary>
        /// <value>
        ///     The commands.
        /// </value>
        [Browsable(false)]
        public IList<WidgetMenuItem> Commands { get; private set; }

        /// <summary>
        ///     Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        public string ProviderName { get; set; }

        /// <summary>
        ///     Gets or sets the ID of the ContentBlockItem if the Content is shared across multiple controls
        /// </summary>
        public Guid SharedContentID { get; set; }

        /// <inheritdoc />
        Type IHasContainerType.ContainerType { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the HTML content to be displayed by the ContentBlock control.
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
        ///     Gets or sets the current content version.
        /// </summary>
        /// <value>The version.</value>
        [Browsable(false)]
        public int ContentVersion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether should enable social sharing buttons.
        /// </summary>
        /// <value>
        ///   <c>true</c> if social sharing buttons should be displayed; otherwise, <c>false</c>.
        /// </value>
        public bool EnableSocialSharing { get; set; }

        /// <summary>
        /// Gets the model.
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
                    this.isEmpty = string.IsNullOrEmpty(this.model.Content);
                    this.SharedContentID = this.model.SharedContentID;
                }

                return this.model;
            }
        }

        #endregion

        #region Cotnroller Actions

        /// <summary>
        /// This is the default Action.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            this.Commands = this.InitializeCommands();

            if (SystemManager.CurrentHttpContext != null)
            {
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects());
            }

            return this.View("Default", this.Model);
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        ///     Defines whether controls of same key will be
        ///     reloaded when this control is updated in the ZoneEditor
        /// </summary>
        /// <returns>True if a reload is required</returns>
        [Browsable(false)]
        [NonAction]
        public bool ShouldReloadControlsWithSameKey()
        {
            return this.ShouldReloadControlsWithSameKey();
        }

        /// <summary>
        ///     Defines whether controls of same key will be
        ///     reloaded when this control is updated in the ZoneEditor
        /// </summary>
        /// <returns>True if a reload is required</returns>
        [Browsable(false)]
        bool IZoneEditorReloader.ShouldReloadControlsWithSameKey()
        {
            return this.SharedContentID != Guid.Empty;
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

        #region Helper Methods

        /// <summary>
        /// Initializes the widget commands.
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        protected virtual IList<WidgetMenuItem> InitializeCommands()
        {
            var packageManager = new PackageManager();
            var commandsList = new List<WidgetMenuItem>();

            commandsList.Add(
                new WidgetMenuItem
                {
                    Text = Res.Get<PageResources>().ZoneEditorEnablePageOverrideDisplayContenxtMenuInfo,
                    CommandName = "displayWidgetOverrideText",
                    CssClass = "sfDisplayText"
                });

            commandsList.Add(
                new WidgetMenuItem
                    {
                        Text = Res.Get<Labels>().Delete, 
                        CommandName = "beforedelete", 
                        CssClass = "sfDeleteItm"
                    });
            commandsList.Add(
                new WidgetMenuItem
                    {
                        Text = Res.Get<Labels>().Duplicate, 
                        CommandName = "duplicate", 
                        CssClass = "sfDuplicateItm"
                    });

            commandsList.Add(
                new WidgetMenuItem
                {
                    Text = Res.Get<PageResources>().ZoneEditorEnablePageOverride,
                    CommandName = "widgetOverride",
                    CssClass = "sfWidgetOverrideItm"
                });

            commandsList.Add(
                new WidgetMenuItem
                {
                    Text = Res.Get<PageResources>().ZoneEditorDisablePageOverride,
                    CommandName = "widgetDisableOverride",
                    CssClass = "sfWidgetOverrideItm"
                });

            if (this.ResolveCurrentSitemapNode() != null)
            {
                commandsList.Add(
                    new WidgetMenuItem
                    {
                        Text = Res.Get<PageResources>().ZoneEditorRollback,
                        CommandName = "rollback",
                        CssClass = "sfDisableWidgetOverrideItm"
                    });
            }

            if (this.SharedContentID == Guid.Empty)
            {
                var shareActionLink =
                    packageManager.EnhanceUrl(
                        RouteHelper.ResolveUrl(string.Format(CultureInfo.InvariantCulture, DesignerTemplate, "Share"), UrlResolveOptions.Rooted));
                commandsList.Add(
                    new WidgetMenuItem
                        {
                            Text = Res.Get<ContentBlockResources>().Share, 
                            ActionUrl = shareActionLink, 
                            NeedsModal = true
                        });
            }
            else
            {
                var unshareActionLink =
                    packageManager.EnhanceUrl(
                        RouteHelper.ResolveUrl(string.Format(CultureInfo.InvariantCulture, DesignerTemplate, "Unshare"), UrlResolveOptions.Rooted));
                commandsList.Add(
                    new WidgetMenuItem
                        {
                            Text = Res.Get<ContentBlockResources>().Unshare, 
                            ActionUrl = unshareActionLink, 
                            NeedsModal = true
                        });
            }

            var useSharedActionLink =
                packageManager.EnhanceUrl(
                    RouteHelper.ResolveUrl(string.Format(CultureInfo.InvariantCulture, DesignerTemplate, "UseShared"), UrlResolveOptions.Rooted));
            commandsList.Add(
                new WidgetMenuItem
                    {
                        Text = Res.Get<ContentBlockResources>().UseShared, 
                        ActionUrl = useSharedActionLink, 
                        NeedsModal = true
                    });
            commandsList.Add(
                new WidgetMenuItem
                    {
                        Text = Res.Get<Labels>().Permissions, 
                        CommandName = "permissions", 
                        CssClass = "sfPermItm"
                    });
            return commandsList;
        }

        /// <summary>
        /// Resolves the current sitemap node.
        /// </summary>
        /// <returns></returns>
        protected virtual PageSiteNode ResolveCurrentSitemapNode()
        {
            return SiteMapBase.GetCurrentNode();
        }

        /// <summary>
        /// Initializes the content block model.
        /// </summary>
        /// <returns>
        /// The <see cref="IContentBlockModel"/>.
        /// </returns>
        private IContentBlockModel InitializeModel()
        {
            var constructorParameters = new Dictionary<string, object>
                                            {
                                                { "providerName", this.ProviderName }, 
                                                { "content", this.content }, 
                                                {
                                                    "enableSocialSharing", 
                                                    this.EnableSocialSharing
                                                }, 
                                                { "sharedContentId", this.SharedContentID },
                                                { "containerType", ((IHasContainerType)this).ContainerType }
                                            };

            return ControllerModelFactory.GetModel<IContentBlockModel>(this.GetType(), constructorParameters);
        }

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfContentBlockIcn sfMvcIcn";
        private const string DesignerTemplate = "Telerik.Sitefinity.Frontend/Designer/Master/ContentBlock?view={0}";
        private const string IZoneEditorReloaderKeyStringFormat = "ContentBlock_{0}";
        private string content;
        private bool isEmpty = true;
        private IContentBlockModel model;

        #endregion
    }
}