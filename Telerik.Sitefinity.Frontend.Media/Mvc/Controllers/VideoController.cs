﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Media.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Video;
using Telerik.Sitefinity.Frontend.Media.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Video widget.
    /// </summary>
    [Localization(typeof(VideoResources))]
    [ControllerToolboxItem(
        Name = VideoController.WidgetName,
        Title = nameof(VideoResources.MediaPlayerControlTitle),
        Description = nameof(VideoResources.MediaPlayerControlDescription),
        ResourceClassId = nameof(VideoResources),
        SectionName = ToolboxesConfig.ContentToolboxSectionName,
        ModuleName = "Libraries",
        CssClass = VideoController.WidgetIconCssClass)]
    public class VideoController : Controller, ICustomWidgetVisualizationExtended, IContentLocatableView
    {
        #region Properties

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IVideoModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IVideoModel>(this.GetType());

                return this.model;
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
                return VideoController.WidgetIconCssClass;
            }
        }

        /// <summary>
        /// Gets the empty link text.
        /// </summary>
        /// <value>
        /// The empty link text.
        /// </value>
        [Browsable(false)]
        public string EmptyLinkText
        {
            get
            {
                return Res.Get<VideoResources>().SelectVideo;
            }
        }

        /// <summary>
        /// Gets a value indicating whether widget is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if widget has no video selected; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return this.Model.Id == Guid.Empty;
            }
        }

        /// <summary>
        /// Gets whether the page is in design mode.
        /// </summary>
        protected virtual bool IsDesignMode
        {
            get
            {
                return SystemManager.IsDesignMode;
            }
        }

        /// <summary>
        /// Gets whether the page is in inline editing mode.
        /// </summary>
        protected virtual bool IsInlineEditingMode
        {
            get
            {
                return SystemManager.IsInlineEditingMode;
            }
        }

        /// <summary>
        /// Gets whether the page is in preview mode.
        /// </summary>
        protected virtual bool IsPreviewMode
        {
            get
            {
                return SystemManager.IsPreviewMode;
            }
        }

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
        /// Gets or sets a value indicating whether the canonical URL tag should be added to the page when the canonical meta tag should be added to the page.
        /// If the value is not set, the settings from SystemConfig -> ContentLocationsSettings -> DisableCanonicalURLs will be used. 
        /// </summary>
        /// <value>The disable canonical URLs.</value>
        public bool? DisableCanonicalUrlMetaTag { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Renders appropriate view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            var viewModel = this.Model.GetViewModel();

            // Design mode should not show video, unless it's preview mode
            var canDisplayVideo = (!this.IsDesignMode || this.IsPreviewMode) && !this.IsInlineEditingMode;

            if (!canDisplayVideo && viewModel.HasSelectedVideo)
                return this.Content(Res.Get<VideoResources>().VideoWillNotBeDisplayed);
            else if (this.IsDesignMode && !viewModel.HasSelectedVideo && this.Model.Id != Guid.Empty) // Design mode should display if a video has been removed
                return this.Content(Res.Get<VideoResources>().VideoNotSelectedOrDeleted);
            else if (viewModel.HasSelectedVideo)
            {
                this.AddCacheDependencies(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(typeof(Video), this.Model.Id));

                this.AddVideoInMediaContext(viewModel);
                return this.View(VideoController.TemplatePrefix + this.TemplateName, viewModel);
            }
            else
                return new EmptyResult();
        }

        /// <summary>
        /// Gets the information for all of the content types that a control is able to show.
        /// </summary>
        /// <returns>
        /// List of location info of the content that this control is able to show.
        /// </returns>
        [NonAction]
        public IEnumerable<IContentLocationInfo> GetLocations()
        {
            return this.Model.GetLocations();
        }

        /// <inheritDoc/>
        protected override void HandleUnknownAction(string actionName)
        {
            ContentLocationHelper.HandlePreview<Telerik.Sitefinity.Libraries.Model.Video>(HttpContext.Request, this.Model.Id, this.Model.ProviderName);

            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        #endregion

        #region Private methods

        private void AddVideoInMediaContext(VideoViewModel videoViewModel)
        {
            if (videoViewModel.Item != null)
            {
                var video = videoViewModel.Item.DataItem as Video;
                if (video != null)
                {
                    if (ObjectFactory.Container.IsRegistered<IMediaContextStore>())
                    {
                        var mediaContextStore = ObjectFactory.Container.Resolve<IMediaContextStore>();
                        mediaContextStore.AddMediaItem(video);
                    }
                }
            }
        }

        #endregion

        #region Private fields and constants

        private IVideoModel model;
        private const string WidgetIconCssClass = "sfVideoIcn sfMvcIcn";
        private string templateName = "Default";
        private const string TemplatePrefix = "Video.";
        private const string WidgetName = "Video_MVC";
        #endregion
    }
}
