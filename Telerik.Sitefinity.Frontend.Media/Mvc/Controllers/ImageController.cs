using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Frontend.Media.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
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
        /// <summary>
        /// Gets the Image widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IImageModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

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
                return (this.Model.Id == Guid.Empty);
            }
        }

        #endregion

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() 
        {
            var viewModel = this.Model.GetViewModel();

            return View("Image", viewModel);
        }


        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="INewsModel"/>.
        /// </returns>
        private IImageModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IImageModel>(this.GetType());
        }

        #endregion


        private IImageModel model;
    }
}
