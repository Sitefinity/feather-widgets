using SocialShare.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SocialShare.Mvc.Controllers
{
    public class SocialShareController : Controller
    {
        #region Actions

        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            var model = new SocialShareModel();

            return View("SocialShare", model);
        }


        #endregion

    }
}
