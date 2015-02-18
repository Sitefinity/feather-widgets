using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Frontend.Media.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Image widget.
    /// </summary>
    [Localization(typeof(ImageResources))]
    [ControllerToolboxItem(Name = "Image", Title = "Image", SectionName = "MvcWidgets")]
    public class ImageController : Controller, ICustomWidgetVisualization
    {
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ImageViewModel ViewModel { get; set; }

        #region ICustomWidgetVisualization

        /// <summary>
        /// Gets the empty link text.
        /// </summary>
        /// <value>
        /// The empty link text.
        /// </value>
        public string EmptyLinkText
        {
            get
            {
                return Res.Get<ImageResources>().SelectImage;
            }
        }

        /// <summary>
        /// Gets a value indicating whether widget is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if widget has no image selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty
        {
            get
            {
                return (this.ViewModel == null || 
                    this.ViewModel.Item == null || 
                    this.ViewModel.Item.DataItem.Id == default(Guid));
            }
        }

        #endregion

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() 
        {
            this.ViewModel = new ImageModel().GetViewModel();

            return View("Image", this.ViewModel);
        }
    }
}
