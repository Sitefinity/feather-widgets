using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.Controllers
{
    /// <summary>
    /// Social Share
    /// </summary>
    [ControllerToolboxItem(Name = "SocialShare", Title = "Social share", SectionName = "MvcWidgets")]
    public class SocialShareController : Controller
    {
        #region Actions
        /// <summary>
        /// Default Action
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var model = new SocialShareModel();

            return this.View("SocialShare", model);
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
    }
}
