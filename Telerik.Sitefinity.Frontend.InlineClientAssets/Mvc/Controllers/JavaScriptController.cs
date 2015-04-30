using System;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller for the JavaScript widget.
    /// </summary>
    [Localization(typeof(JavaScriptResources))]
    [ControllerToolboxItem(Name = "JavaScript_MVC", Title = "JavaScript", SectionName = "ScriptsAndStylesControlsSection", CssClass = JavaScriptController.WidgetIconCssClass)]
    public class JavaScriptController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Account Activation widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IJavaScriptModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
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
            var viewModel = this.Model.GetViewModel();

            var page = this.GetHttpContext().CurrentHandler as Page;

            if (page != null)
            {
                if (this.Model.Position == Models.EmbedPosition.Head)
                {
                    page.Header.Controls.Add(new LiteralControl(viewModel.JavaScriptCode));
                }

                if (this.Model.Position == Models.EmbedPosition.BeforeBodyEndTag)
                {
                    page.PreRenderComplete += PagePreRenderCompleteHandler;
                }
            }

            return this.View(viewModel);
        }
        #endregion

        #region Virtual methods
        /// <summary>
        /// Gets the HTTP context.
        /// </summary>
        /// <returns></returns>
        protected virtual HttpContextBase GetHttpContext()
        {
            return this.HttpContext;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Initializes the model instance.
        /// </summary>
        /// <returns></returns>
        private IJavaScriptModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IJavaScriptModel>(this.GetType());
        }

        /// <summary>
        /// Handler called when the Page's PreRenderComplete event is fired.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        void PagePreRenderCompleteHandler(object sender, EventArgs e)
        {
            this.model.PlaceScriptBeforeBodyEnd((Page)sender, this.model.BuildScriptTag());
        }

        #endregion

        #region Private fields and constants
        private IJavaScriptModel model;
        private const string WidgetIconCssClass = "sfLinkedFileViewIcn sfMvcIcn";
        #endregion
    }
}
