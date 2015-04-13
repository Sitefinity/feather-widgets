using System;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Video;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Video widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Video", Title = "Video", SectionName = "MvcWidgets", ModuleName = "Libraries", CssClass = VideoController.WidgetIconCssClass)]
    public class VideoController : Controller, ICustomWidgetVisualizationExtended
    {
        #region Properties

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IVideoModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IVideoModel>(this.GetType());

                return this.model;
            }
        }
        
        /// <summary>
        /// Gets the widget CSS class.
        /// </summary>
        /// <value>
        /// The widget CSS class.
        /// </value>
        [Browsable(false)]
        public string WidgetCssClass
        {
            get
            {
                return VideoController.WidgetIconCssClass;
            }
        }

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
                return "Select a video or other file";
            }
        }

        /// <summary>
        /// Gets a value indicating whether widget is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if widget has no video selected; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return (this.Model.Id == Guid.Empty);
            }
        }

        /// <summary>
        /// Gets whether the page is in design mode.
        /// </summary>
        protected virtual bool IsDesignMode
        {
            get
            {
                return SystemManager.IsDesignMode;
            }
        }

        /// <summary>
        /// Gets whether the page is in inline editing mode.
        /// </summary>
        protected virtual bool IsInlineEditingMode
        {
            get
            {
                return SystemManager.IsInlineEditingMode;
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

        #region Public methods

        /// <summary>
        /// Renders appropriate view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            var viewModel = this.Model.GetViewModel();

            if (viewModel.HasSelectedVideo && !this.IsEmpty && this.IsDesignMode && !this.IsInlineEditingMode)
                return Content("A video was not selected or has been deleted. Please select another one.");
            else if (this.Model.Id != Guid.Empty)
                return View(this.TemplateName, viewModel);
            else
                return new EmptyResult();
        }

        #endregion
        
        #region Private fields and constants

        private IVideoModel model;
        private const string WidgetIconCssClass = "sfVideoIcn";
        private string templateName = "Video.Index";
        
        #endregion
    }
}