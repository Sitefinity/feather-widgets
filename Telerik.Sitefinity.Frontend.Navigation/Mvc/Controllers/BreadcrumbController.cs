﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.Breadcrumb;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Breadcrumb widget.
    /// </summary>
    [ControllerToolboxItem(
        Name = BreadcrumbController.WidgetName,
        Title = nameof(BreadcrumbResources.BreadcrumbTitle),
        Description = nameof(BreadcrumbResources.BreadcrumbDescription),
        ResourceClassId = nameof(BreadcrumbResources),
        SectionName = ToolboxesConfig.NavigationControlsSectionName,
        CssClass = BreadcrumbController.WidgetIconCssClass)]
    [Localization(typeof(BreadcrumbResources))]
    [IndexRenderMode(IndexRenderModes.NoOutput)]
    public class BreadcrumbController : Controller, IDelayedExecutionController
    {
        /// <summary>
        /// Gets or sets the name of the template that will be displayed.
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
        /// Gets the model.
        /// </summary>
        /// <value>The model.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IBreadcrumbModel Model
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

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var viewModel = this.Model.CreateViewModel(this.BreadCrumExtender);

            if (viewModel.IsTemplateRendered)
            {
                return this.Content(this.BreadcrumbOnTemplateMessage);
            }

            return this.View(this.TemplateName, viewModel);
        }

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        /// <summary>
        /// Gets the virtual nodes.
        /// </summary>
        /// <returns></returns>
        protected virtual IBreadcrumExtender BreadCrumExtender
        {
            get
            {
                var page = this.GetHttpContext.CurrentHandler.GetPageHandler();

                return page == null ? null : page.GetBreadcrumbExtender();
            }
        }

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns></returns>
        protected virtual IBreadcrumbModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IBreadcrumbModel>(this.GetType());
        }

        /// <summary>
        /// Gets the breadcrumb on template message.
        /// </summary>
        /// <returns></returns>
        protected virtual string BreadcrumbOnTemplateMessage
        {
            get
            {
                return Res.Get<BreadcrumbResources>().BreadcrumbOnTemplateMessage;
            }
        }

        /// <summary>
        /// Gets the HTTP context.
        /// </summary>
        /// <returns></returns>
        protected virtual HttpContextBase GetHttpContext
        {
            get
            {
                return this.HttpContext;
            }
        }

        #region Private fields and constants
        internal const string WidgetIconCssClass = "sfBreadcrumbIcn sfMvcIcn";
        private IBreadcrumbModel model;
        private string templateName = "Breadcrumb";
        private const string WidgetName = "Breadcrumb_MVC";
        #endregion
    }
}
