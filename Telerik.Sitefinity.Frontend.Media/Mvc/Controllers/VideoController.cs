using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Video;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Video widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Video", Title = "Video", SectionName = "MvcWidgets", ModuleName = "Libraries", CssClass = VideoController.WidgetIconCssClass)]
    public class VideoController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IVideoModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IVideoModel>(this.GetType());

                return this.model;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Renders the Index view.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            var viewModel = this.Model.GetViewModel();

            return View("Index", viewModel);
        }

        #endregion
        
        #region Private fields and constants

        private IVideoModel model;
        private const string WidgetIconCssClass = "sfVideoIcn";
        
        #endregion
    }
}