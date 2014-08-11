using System.Web.Mvc;
using SocialShare.Mvc.Models;

namespace SocialShare.Mvc.Controllers
{
    /// <summary>
    /// Social Share
    /// </summary>
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
    }
}
