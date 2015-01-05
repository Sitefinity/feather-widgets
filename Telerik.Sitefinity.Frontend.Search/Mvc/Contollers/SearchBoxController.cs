using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Search.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Contollers
{
    /// <summary>
    /// This class represents the controller of Search box widget.
    /// </summary>
    [ControllerToolboxItem(Name = "SearchBox", Title = "Search box", SectionName = "MvcWidgets", ModuleName = "Search")]
    [Localization(typeof(SearchWidgetsResources))]
    public class SearchBoxController : Controller
    {
        #region Properties

        #endregion

        #region Actions
        public ActionResult Index()
        {
            return View("SearchBox");
        }
        #endregion

        #region Private methods

        #endregion

        #region Private fields and constants

        #endregion
    }
}
