using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Form widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Form_MVC", Title = "Form", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = FormController.WidgetIconCssClass)]
    [Localization(typeof(FormResources))]
    public class FormController : Controller, IContentLocatableView
    {
        /// <summary>
        /// Gets the Form widget model.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IFormModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        /// <summary>
        /// Renders the form selected via the FormId property of the model.
        /// </summary>
        public ActionResult Index(bool? submitedSuccessfully = null)
        {
            if (submitedSuccessfully.HasValue)
            {
                return this.Content(submitedSuccessfully.Value ? "Successfully submitted!" : "Entry is not valid!");
            }

            var viewModel = this.Model.GetViewModel();
            if (viewModel != null)
            {
                if (string.IsNullOrEmpty(viewModel.Error))
                {
                    var viewPath = this.Model.GetViewPath();
                    return this.View(viewPath, viewModel);
                }
                else
                {
                    return this.Content(viewModel.Error);
                }
            }

            return new EmptyResult();
        }

        /// <summary>
        /// Submits the form selected via the FormId property of the model.
        /// </summary>
        [HttpPost]
        public ActionResult Submit(FormCollection collection)
        {
            var success = this.Model.TrySubmitForm(collection, this.Request.UserHostAddress);

            return this.RedirectToAction("Index", new { submitedSuccessfully = success });
        }

        #region IContentLocatableView

        /// <inheritDoc/>
        public bool? DisableCanonicalUrlMetaTag
        {
            get { return this.disableCanonicalUrlMetaTag; }

            set { this.disableCanonicalUrlMetaTag = value; }
        }

        /// <inheritDoc/>
        public IEnumerable<IContentLocationInfo> GetLocations()
        {
            return this.Model.GetLocations();
        }

        #endregion

        #region Private methods

        private IFormModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IFormModel>(this.GetType());
        }

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfFormsIcn sfMvcIcon";

        private IFormModel model;
        private bool? disableCanonicalUrlMetaTag;

        #endregion
    }
}