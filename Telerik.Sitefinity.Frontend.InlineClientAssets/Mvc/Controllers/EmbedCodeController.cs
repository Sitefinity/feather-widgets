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
    [Localization(typeof(EmbedCodeResources))]
    [ControllerToolboxItem(Name = "EmbedCode_MVC",
                           Title = "Embed code",
                           SectionName = "ScriptsAndStylesControlsSection",
                           CssClass = EmbedCodeController.WidgetIconCssClass)]
    public class EmbedCodeController : Controller, ICustomWidgetVisualizationExtended
    {
        public ActionResult Index()
        {
            var viewModel = this.Model.GetViewModel();

            if (this.IsEdit)
            {
                this.SetDesignTimeContent(viewModel);
            }

            return this.View(viewModel);
        }

        private void SetDesignTimeContent(EmbedCodeViewModel viewModel)
        {
            if (!string.IsNullOrWhiteSpace(viewModel.Description))
            {
                this.ViewBag.DesignModeContent = viewModel.Description;
            }
            else
            {
                var result = ScriptHelper.GetShortScript(viewModel.EmbedCode);

                this.ViewBag.DesignModeContent = result + Environment.NewLine + Res.Get<EmbedCodeResources>().IncludedWhereDropped;
            }
        }

        /// <summary>
        /// Gets whether the page is in edit mode.
        /// </summary>
        /// <value>The is edit.</value>
        protected virtual bool IsEdit
        {
            get
            {
                return SystemManager.IsDesignMode && !SystemManager.IsPreviewMode;
            }
        }

        /// <summary>
        /// Gets the Account Activation widget model.
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
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        protected IEmbedCodeModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IEmbedCodeModel>(this.GetType());
        }

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
