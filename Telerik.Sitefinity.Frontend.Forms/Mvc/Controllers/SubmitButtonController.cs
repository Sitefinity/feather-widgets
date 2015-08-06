using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.SubmitButton;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms submit button.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcSubmitButton", Title = "MvcSubmitButton", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName)]
    public class SubmitButtonController : Controller
    {
        /// <summary>
        /// Gets the Form widget model.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ISubmitButtonModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed
        /// </summary>
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
        /// Provides the default view of this field
        /// </summary>
        public ActionResult Index()
        {
            var viewPath = SubmitButtonController.TemplateNamePrefix + this.TemplateName;
            return this.View(viewPath, this.Model);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        }

        #region Private methods

        private ISubmitButtonModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<ISubmitButtonModel>(this.GetType());
        }

        #endregion

        #region Private fields and constants

        private ISubmitButtonModel model;

        private const string TemplateNamePrefix = "Index.";
        private string templateName = "Default";

        #endregion
    }
}