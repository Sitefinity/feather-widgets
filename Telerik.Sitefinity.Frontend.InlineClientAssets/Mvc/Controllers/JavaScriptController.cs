using System;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller for the JavaScript widget.
    /// </summary>
    [Localization(typeof(JavaScriptResources))]
    [ControllerToolboxItem(Name = "JavaScript_MVC", Title = "JavaScript", SectionName = "ScriptsAndStylesControlsSection", CssClass = JavaScriptController.WidgetIconCssClass)]
    public class JavaScriptController : Controller, ICustomWidgetVisualizationExtended
    {
        #region Properties

        /// <summary>
        /// Gets the Javascript widget model.
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
        /// Gets whether the page is in edit mode.
        /// </summary>
        /// <value>The is edit.</value>
        private bool IsEdit
        {
			get
			{
				var isEdit = false;
				if (this.IsDesignMode && !SystemManager.IsPreviewMode && !SystemManager.IsInlineEditingMode)
				{
					isEdit = true;
				}
				return isEdit;
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

            if (page != null && !this.IsEdit)
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

            if (this.IsEdit)
            {
                string result = null;

                if (!string.IsNullOrEmpty(this.Model.Description))
                {
                    result = this.Model.Description;
                }
                else if (Model.Mode == ResourceMode.Inline && !string.IsNullOrEmpty(viewModel.JavaScriptCode))
                {
                    result = EmbedCodeHelper.GetShortEmbededCode(viewModel.JavaScriptCode);
                }
                else
                {
                    result = viewModel.JavaScriptCode;
                }

                viewModel.DesignModeContent = result;

                if (!string.IsNullOrEmpty(result) && string.IsNullOrEmpty(this.Model.Description))
                {
                    var includedIn = this.GetIncludeInResource(viewModel.Position);
                    viewModel.DesignModeContent = viewModel.DesignModeContent + Environment.NewLine + includedIn;
                }
            }

            return this.View("Index", viewModel);
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

         #region ICustomWidgetVisualizationExtended

        /// <inheritDocs/>
        [Browsable(false)]
        public string WidgetCssClass
        {
            get { return JavaScriptController.WidgetIconCssClass; }
        }

        /// <inheritDocs/>
        [Browsable(false)]
        public string EmptyLinkText
        {
            get { return Res.Get<JavaScriptResources>().SetJS; }
        }

        /// <inheritDocs/>
        [Browsable(false)]
        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(this.Model.InlineCode) && string.IsNullOrEmpty(this.Model.FileUrl); }
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

        /// <summary>
        /// Gets the resource indicates where the script is included.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        protected virtual string GetIncludeInResource(EmbedPosition position)
        {
            switch (position)
            {
                case EmbedPosition.Head:
                    return Res.Get<JavaScriptResources>().IncludedInTheHead;
                case EmbedPosition.InPlace:
                    return Res.Get<JavaScriptResources>().IncludedWhereDropped;
                case EmbedPosition.BeforeBodyEndTag:
                    return Res.Get<JavaScriptResources>().IncludedBeforeTheBodyEnd;
                default:
                    return string.Empty;
            }
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
