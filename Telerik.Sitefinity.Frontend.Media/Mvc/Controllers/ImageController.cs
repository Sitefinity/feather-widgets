using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Frontend.Media.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Image widget.
    /// </summary>
    [Localization(typeof(ImageResources))]
    [ControllerToolboxItem(Name = "Image", Title = "Image", SectionName = "MvcWidgets", ModuleName = "Libraries")]
    public class ImageController : Controller, ICustomWidgetVisualization, IContentLocatableView
    {
        #region Properties

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

        /// <summary>
        /// Gets or sets the name of the template that widget will be displayed.
        /// </summary>
        /// <value></value>
        public string TemplateName
        {
            get
            {
                return this.templateName;
            }

            set
            {
                this.templateName = value;
            }
        }

        #endregion

        #region ICustomWidgetVisualization

        /// <summary>
        /// Gets the empty link text.
        /// </summary>
        /// <value>
        /// The empty link text.
        /// </value>
        [Browsable(false)]
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
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return (this.Model.Id == Guid.Empty);
            }
        }

        #endregion

        #region IContentLocatableView

        /// <summary>
        /// Gets or sets a value indicating whether the canonical URL tag should be added to the page when the canonical meta tag should be added to the page.
        /// If the value is not set, the settings from SystemConfig -> ContentLocationsSettings -> DisableCanonicalURLs will be used. 
        /// </summary>
        /// <value>The disable canonical URLs.</value>
        public bool? DisableCanonicalUrlMetaTag
        {
            get
            {
                return this.disableCanonicalUrlMetaTag;
            }
            set
            {
                this.disableCanonicalUrlMetaTag = value;
            }
        }

        /// <summary>
        /// Gets the information for all of the content types that a control is able to show.
        /// </summary>
        /// <returns>
        /// List of location info of the content that this control is able to show.
        /// </returns>
        [NonAction]
        public IEnumerable<IContentLocationInfo> GetLocations()
        {
            return this.Model.GetLocations();
        }

        /// <inheritDoc/>
        protected override void HandleUnknownAction(string actionName)
        {
            string contentAction = HttpContext.Request.QueryString["sf-content-action"];
            
            if (contentAction != null && contentAction == "preview")
            {
                var image = Telerik.Sitefinity.Modules.Libraries.LibrariesManager.GetManager(this.Model.ProviderName).GetImages().FirstOrDefault(i => i.Id == this.Model.Id);

                if (image != null)
                {
                    var routeDataParams = this.HttpContext.Request.RequestContext.RouteData.Values["params"] as string[];

                    if (routeDataParams != null && routeDataParams.Contains(image.UrlName.Value))
                    {
                        this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
                        Telerik.Sitefinity.Web.RouteHelper.SetUrlParametersResolved();
                        return;
                    }
                }
            }

            base.HandleUnknownAction(actionName);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index() 
        {
            var viewModel = this.Model.GetViewModel();

            if (viewModel.Item.DataItem!=null && viewModel.Item.DataItem.Id != Guid.Empty)
            {
                return View(this.TemplateName, viewModel);
            }
            else if (SystemManager.IsDesignMode && !SystemManager.IsInlineEditingMode)
            {
                string errorMessage = Res.Get<ImageResources>().ImageWasNotSelectedOrHasBeenDeleted;

                return Content(errorMessage);
            }
            else
            {
                return new EmptyResult();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="IImageModel"/>.
        /// </returns>
        private IImageModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IImageModel>(this.GetType());
        }

        #endregion

        #region Private fields and constants

        private string templateName = "Image";
        private IImageModel model;
        private bool? disableCanonicalUrlMetaTag;

        #endregion
    }
}
