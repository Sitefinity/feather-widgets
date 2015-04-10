using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document;
using Telerik.Sitefinity.Frontend.Media.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Video widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Video", Title = "Video", SectionName = "MvcWidgets", ModuleName = "Libraries", CssClass = VideoController.WidgetIconCssClass)]
    public class VideoController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        private const string WidgetIconCssClass = "sfVideoIcn";
    }
}