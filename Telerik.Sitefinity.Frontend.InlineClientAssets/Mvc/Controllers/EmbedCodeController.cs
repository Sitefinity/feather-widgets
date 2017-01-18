using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.EmbedCode;
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
    /// This class represents the controller for the EmbedCode widget.
    /// </summary>
    [Localization(typeof(EmbedCodeResources))]
    [ControllerToolboxItem(Name = "EmbedCode_MVC",
                           Title = "Embed code",
                           SectionName = "ScriptsAndStylesControlsSection",
                           CssClass = EmbedCodeController.WidgetIconCssClass)]
    public class EmbedCodeController : Controller, ICustomWidgetVisualizationExtended
    {
        #region Properties
        /// <summary>
        /// Gets whether the page is in edit mode.
        /// </summary>
        /// <value>The is edit.</value>
        protected virtual bool IsEdit
        {
            get
            {
                return SystemManager.IsDesignMode && !SystemManager.IsPreviewMode && !SystemManager.IsInlineEditingMode;
            }
        }

        /// <summary>
        /// Gets the Embed code widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IEmbedCodeModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = ControllerModelFactory.GetModel<IEmbedCodeModel>(this.GetType());
                }

                return this.model;
            }
        }

        #region Actions
        /// <summary>
        /// Handles Embed code referencing on the page.
        /// </summary>
        public ActionResult Index()
        {
            EmbedCodeViewModel viewModel = this.Model.GetViewModel();

            if (this.IsEdit)
            {
                this.SetDesignModeContent(viewModel);
            }

            return this.View("Index", viewModel);
        }
        #endregion

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <typeparam name="TResource">The type of the T resource.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected virtual string GetResource<TResource>(string key) where TResource : Resource
        {
            return Res.Get(typeof(TResource), key);
        }

        private void SetDesignModeContent(EmbedCodeViewModel viewModel)
        {
            if (!string.IsNullOrWhiteSpace(viewModel.Description))
            {
                this.ViewBag.DesignModeContent = viewModel.Description;
            }
            else
            {
                var result = EmbedCodeHelper.GetShortEmbededCode(viewModel.EmbedCode);
                
                if (!string.IsNullOrWhiteSpace(result))
                    this.ViewBag.DesignModeContent = result + Environment.NewLine + this.GetResource<EmbedCodeResources>("IncludedWhereDropped");
            }
        }
        #endregion

        #region ICustomWidgetVisualizationExtended
        /// <inheritDocs/>
        [Browsable(false)]
        public string WidgetCssClass
        {
            get { return EmbedCodeController.WidgetIconCssClass; }
        }

        /// <inheritDocs/>
        [Browsable(false)]
        public string EmptyLinkText
        {
            get { return Res.Get<EmbedCodeResources>().EmbedCode; }
        }

        /// <inheritDocs/>
        [Browsable(false)]
        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(this.Model.InlineCode); }
        }
        #endregion

        #region private fields
        private const string WidgetIconCssClass = "sfLinkedFileViewIcn sfMvcIcn";
        private IEmbedCodeModel model;
        #endregion
    }
}
