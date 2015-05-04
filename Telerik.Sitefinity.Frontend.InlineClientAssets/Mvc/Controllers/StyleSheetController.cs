using System;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.StyleSheet;
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
                string encodedMarkup = null;
                if (!string.IsNullOrEmpty(this.Model.Description))
                {
                    return this.Content(this.Model.Description);
                }
                else if(this.Model.Mode == Models.ResourceMode.Inline)
                {
                    var firstLinesContent = this.Model.GetShortInlineStylesEncoded();

                    encodedMarkup = firstLinesContent;
                }
                else if (this.Model.Mode == Models.ResourceMode.Reference)
                {
                    encodedMarkup = HttpUtility.HtmlEncode(cssMarkup);
                }

                if (!string.IsNullOrEmpty(encodedMarkup))
                {
                    var includedIn = Res.Get<StyleSheetResources>().IncludedInHead;
                    return this.Content(encodedMarkup + includedIn);
                }
            }

            return new EmptyResult();
        }

        #endregion

        #region ICustomWidgetVisualizationExtended

        /// <inheritDocs/>
        public string WidgetCssClass
        {
            get { return StyleSheetController.WidgetIconCssClass; }
        }

        /// <inheritDocs/>
        public string EmptyLinkText
        {
            get { return Res.Get<StyleSheetResources>().SetCss; }
        }

        /// <inheritDocs/>
        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(this.Model.InlineStyles) && string.IsNullOrEmpty(this.Model.ResourceUrl); }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Adds the CSS in the head of the page.
        /// </summary>
        /// <param name="cssMarkup">The CSS markup.</param>
        protected virtual void AddCssInHead(string cssMarkup)
        {
            var page = this.HttpContext.CurrentHandler as Page;
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
            return !this.IsEmpty && SystemManager.IsDesignMode && !SystemManager.IsInlineEditingMode;
        }

        #endregion

        #region Private fields and constants

        private IStyleSheetModel model;
        private const string WidgetIconCssClass = "sfLinkedFileViewIcn sfMvcIcn";

        #endregion
    }
}
