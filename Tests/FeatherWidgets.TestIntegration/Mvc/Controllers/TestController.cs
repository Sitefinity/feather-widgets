using System.Web.Mvc;

namespace FeatherWidgets.TestIntegration.Mvc.Controllers
{
    /// <summary>
    /// This <see cref="Controller"/> represents a custom widget for testing purposes.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class TestController : Controller
    {
        /// <summary>
        /// The default action of the custom widget controller.
        /// </summary>
        /// <returns>The default view of the custom widget controller.</returns>
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
