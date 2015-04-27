using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Media.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document;
using Telerik.Sitefinity.Frontend.Media.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Document link widget.
    /// </summary>
    [Localization(typeof(DocumentResources))]
    [ControllerToolboxItem(Name = "DocumentLink_MVC", Title = "Document link", SectionName = ToolboxesConfig.ContentToolboxSectionName, ModuleName = "Libraries", CssClass = DocumentController.WidgetIconCssClass)]
    public class DocumentController : Controller, ICustomWidgetVisualizationExtended, IContentLocatableView
    {
        #region Properties
        /// <summary>
        /// Gets the Document link widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IDocumentModel Model
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

        /// <summary>
        /// Gets the label used when document is not selected or has been deleted.
        /// </summary>
        /// <value>The image was not selected or has been deleted message.</value>
        protected virtual string DocumentWasNotSelectedOrHasBeenDeletedMessage
        {
            get
            {
                return Res.Get<DocumentResources>().DocumentWasNotSelectedOrHasBeenDeleted;
            }
        }

        /// <summary>
        /// Determines whether the page is in design mode.
        /// </summary>
        protected virtual bool IsDesignMode
        {
            get
            {
                return SystemManager.IsDesignMode;
            }
        }

        /// <summary>
        /// Determines whether the page is in inline editing mode.
        /// </summary>
        protected virtual bool IsInlineEditingMode
        {
            get
            {
                return SystemManager.IsInlineEditingMode;
            }
        }
        #endregion

        #region Actions
        /// <summary>
        /// Renders appropriate view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            var viewModel = this.Model.GetViewModel();

            if (viewModel.DocumentWasNotFound &&
                !this.IsEmpty &&
                this.IsDesignMode &&
                !this.IsInlineEditingMode)
            {
                return Content(this.DocumentWasNotSelectedOrHasBeenDeletedMessage);
            }
            else if(this.Model.Id != Guid.Empty)
	        {
                return View(this.TemplateName, viewModel);
	        }
            else
            {
                return new EmptyResult();
            }
        }
        #endregion

        #region ICustomWidgetVisualizationExtended
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
                return Res.Get<DocumentResources>().SelectDocument;
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
                return DocumentController.WidgetIconCssClass;
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
        
        #endregion

        #region Overriden methods

        /// <inheritDoc/>
        protected override void HandleUnknownAction(string actionName)
        {
            ContentLocationHelper.HandlePreview<Telerik.Sitefinity.Libraries.Model.Document>(HttpContext.Request, this.Model.Id, this.Model.ProviderName);

            this.Index().ExecuteResult(this.ControllerContext);
        }

        #endregion

        #region Private methods
        private IDocumentModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IDocumentModel>(this.GetType());
        }
        #endregion

        #region Private fields and constants
        private IDocumentModel model;
        private bool? disableCanonicalUrlMetaTag;
        private string templateName = "DocumentLink";
        private const string WidgetIconCssClass = "sfDownloadLinkIcn";
        #endregion
    }
}
