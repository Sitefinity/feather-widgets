using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.StyleSheet;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller for the StyleSheet widget.
    /// </summary>
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

        private IStyleSheetModel model;
    }
}
