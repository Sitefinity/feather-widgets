using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.VideoGallery;
using Telerik.Sitefinity.Frontend.Media.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Controllers
{
    /// <summary>
    /// This class represents controller for the Video Gallery widget.
    /// </summary>
    [Localization(typeof(VideoGalleryResources))]
    [ControllerToolboxItem(Name = "VideoGallery", Title = "Video gallery", SectionName = "MvcWidgets", ModuleName = "Libraries", CssClass = "sfVideoLibraryViewIcn")]
    public class VideoGalleryController: Controller
    {
        #region Public properties

        /// <summary>
        /// Gets the Video gallery widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IVideoGalleryModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        #endregion

        #region Actions
        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="IVideoGalleryModel"/>.
        /// </returns>
        private IVideoGalleryModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IVideoGalleryModel>(this.GetType());
        }

        #endregion

        #region Private fields and constants

        private IVideoGalleryModel model;

        #endregion
    }
}
