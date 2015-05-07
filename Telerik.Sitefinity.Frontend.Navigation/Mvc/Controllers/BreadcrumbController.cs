using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.Breadcrumb;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Breadcrumb widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Breadcrumb_MVC",
        Title = "Breadcrumb",
        SectionName = ToolboxesConfig.NavigationControlsSectionName,
        CssClass = BreadcrumbController.WidgetIconCssClass)]
    [Localization(typeof(BreadcrumbResources))]
    public class BreadcrumbController : Controller
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
            var viewModel = this.Model.CreateViewModel(this.VirtualNodes);

            if (this.Model.IsTemplateRendered)
            {
                return this.Content(this.BreadcrumbOnTemplateMessage);
            }

            return this.View(this.TemplateName, viewModel);
        }

        /// <summary>
        /// Gets the virtual nodes.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<SiteMapNode> VirtualNodes
        {
            get
            {
                if (this.Model.AllowVirtualNodes)
                {
                    var page = this.GetHttpContext.CurrentHandler as Page;

                    if (page != null)
                    {
                        IBreadcrumExtender extender = page.GetBreadcrumbExtender();
                        if (extender != null)
                        {
                            return extender.GetVirtualNodes(this.Model.SiteMapProvider);
                        }
                    }
                }

                return Enumerable.Empty<SiteMapNode>();
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
        #endregion
    }
}
