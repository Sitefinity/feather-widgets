using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Image widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Image", Title = "Image", SectionName = "MvcWidgets")]
    public class ImageController: Controller
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ImageViewModel ViewModel { get; set; }

        public ActionResult Index() 
        {
            this.ViewModel = new ImageModel().GetViewModel();
            return View("Image", this.ViewModel);
        }
    }
}
