using System.ComponentModel;
using System.Web.Mvc;
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

        #region Private fields and constants

        private IStyleSheetModel model;
        private const string WidgetIconCssClass = "sfLinkedFileViewIcn";

        #endregion
    }
}
