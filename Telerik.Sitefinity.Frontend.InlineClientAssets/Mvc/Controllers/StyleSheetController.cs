using System;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.StyleSheet;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller for the StyleSheet widget.
    /// </summary>
    [Localization(typeof(StyleSheetResources))]
    [ControllerToolboxItem(Name = "StyleSheet_MVC", Title = "CSS", SectionName = "ScriptsAndStylesControlsSection", CssClass = StyleSheetController.WidgetIconCssClass)]
    public class StyleSheetController : Controller, ICustomWidgetVisualizationExtended
    {
        #region Properties

        /// <summary>
        /// Gets the model of the StyleSheet widget.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IStyleSheetModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IStyleSheetModel>(this.GetType());

                return this.model;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Handles CSS referencing on the page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var cssMarkup = this.Model.GetMarkup();
            if (!cssMarkup.IsNullOrEmpty())
            {
                this.AddCssInHead(cssMarkup);
            }

            if (this.ShouldDisplayContent())
            {
                string markup = null;
                if (!string.IsNullOrEmpty(this.Model.Description))
                {
                    markup = this.Model.Description;                  
                }
                else if(this.Model.Mode == Models.ResourceMode.Inline)
                {
                    markup = EmbedCodeHelper.GetShortEmbededCode(this.Model.InlineStyles);
                }
                else if (this.Model.Mode == Models.ResourceMode.Reference)
                {
                    markup = cssMarkup;
                }

                if (!string.IsNullOrEmpty(markup))
                {
                    this.ViewBag.DesignModeContent = markup;
                    
                    return this.View();
                }
            }

            return new EmptyResult();
        }

        #endregion

        #region ICustomWidgetVisualizationExtended

        /// <inheritDocs/>
        [Browsable(false)]
        public string WidgetCssClass
        {
            get { return StyleSheetController.WidgetIconCssClass; }
        }

        /// <inheritDocs/>
        [Browsable(false)]
        public string EmptyLinkText
        {
            get { return Res.Get<StyleSheetResources>().SetCss; }
        }

        /// <inheritDocs/>
        [Browsable(false)]
        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(this.Model.InlineStyles) && string.IsNullOrEmpty(this.Model.ResourceUrl) && string.IsNullOrEmpty(this.Model.Description); }
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

        #region Helpers

        /// <summary>
        /// Adds the CSS in the head of the page.
        /// </summary>
        /// <param name="cssMarkup">The CSS markup.</param>
        protected virtual void AddCssInHead(string cssMarkup)
        {
            var page = PageInitializer.GetPageHandler(this.HttpContext.CurrentHandler);
            if (page != null && page.Header !=null)
            {
                if (!string.IsNullOrEmpty(cssMarkup))
                {
                    page.Header.Controls.Add(new LiteralControl(cssMarkup));
                }
            }
        }

        /// <summary>
        /// Returns true if the controller should display content.
        /// </summary>
        protected virtual bool ShouldDisplayContent()
        {
            return !this.IsEmpty && SystemManager.IsDesignMode && !SystemManager.IsInlineEditingMode && !SystemManager.IsPreviewMode;
        }

        /// <summary>
        /// Gets a resource with the specified key.
        /// </summary>
        /// <typeparam name="TResource">The type of the resource.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>Localized string.</returns>
        protected virtual string Resource<TResource>(string key) where TResource : Resource
        {
            return Res.Get(typeof(TResource), key);
        }

        #endregion

        #region Private fields and constants

        private IStyleSheetModel model;
        private const string WidgetIconCssClass = "sfLinkedFileViewIcn sfMvcIcn";

        #endregion
    }
}
