using System.ComponentModel;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.StyleSheet;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller for the StyleSheet widget.
    /// </summary>
    [Localization(typeof(StyleSheetResources))]
    [ControllerToolboxItem(Name = "StyleSheet_MVC", Title = "CSS", SectionName = "ScriptsAndStylesControlsSection", CssClass = StyleSheetController.WidgetIconCssClass)]
    public class StyleSheetController : Controller
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
            this.AddCssInHead();
            return this.View();
        }

        #endregion

        #region Helpers

        private void AddCssInHead()
        {
            var page = this.HttpContext.CurrentHandler as Page;
            if (page != null && page.Header !=null)
            {
                var cssMarkup = this.Model.GetMarkup();
                page.Header.Controls.Add(new LiteralControl(cssMarkup));
            }
        }

        #endregion

        #region Private fields and constants

        private IStyleSheetModel model;
        private const string WidgetIconCssClass = "sfLinkedFileViewIcn sfMvcIcn";

        #endregion
    }
}
