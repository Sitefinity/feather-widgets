using System;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Publishing.Helpers;
using Telerik.Sitefinity.Frontend.Publishing.Mvc.Models;
using Telerik.Sitefinity.Frontend.Publishing.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Publishing.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Feed widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Feed_MVC", Title = "Feed", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = FeedController.WidgetIconCssClass)]
    [Localization(typeof(FeedResources))]
    public class FeedController : Controller, ICustomWidgetVisualizationExtended
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
        /// Gets the Feed widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IFeedModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        #endregion

        #region ICustomWidgetVisualizationExtended

        /// <inheritDocs/>
        [Browsable(false)]
        public string WidgetCssClass
        {
            get { return FeedController.WidgetIconCssClass; }
        }

        /// <inheritDocs/>
        [Browsable(false)]
        public string EmptyLinkText
        {
            get { return Res.Get<FeedResources>().SelectFeed; }
        }

        /// <inheritDocs/>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return this.Model.FeedId == null || this.Model.FeedId == Guid.Empty;
            }
        }

        #endregion

        #region Actions
        /// <summary>
        /// Renders appropriate view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            if (!this.IsPublishingModuleActivated())
            {
                return new EmptyResult();
            }

            var viewModel = this.Model.GetViewModel();

            var page = PageInitializer.GetPageHandler(this.GetHttpContext().CurrentHandler);

            if (page != null)
            {
                if ((viewModel.InsertionOption == FeedInsertionOption.AddressBarOnly ||
                    viewModel.InsertionOption == FeedInsertionOption.PageAndAddressBar) &&
                    viewModel.HeadLink != null)
                {
                    page.Header.Controls.Add(new LiteralControl(viewModel.HeadLink));
                }
            }

            var fullTemplateName = this.templateNamePrefix + this.TemplateName;

            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        }
        #endregion

        #region Virtual methods
        /// <summary>
        /// Gets the HTTP context.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        protected virtual HttpContextBase GetHttpContext()
        {
            return this.HttpContext;
        }

        /// <summary>
        /// Determines whether the publishing module is activated.
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsPublishingModuleActivated()
        {
            return PublishingWidgetExtensions.IsModuleActivated();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Initializes the model instance.
        /// </summary>
        /// <returns></returns>
        private IFeedModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IFeedModel>(this.GetType());
        }
        #endregion

        #region Private fields and constants
        private string templateName = "FeedLink";
        private readonly string templateNamePrefix = "Feed.";
        private IFeedModel model;
        private const string WidgetIconCssClass = "sfFeedsIcn sfMvcIcn";
        #endregion
    }
}
