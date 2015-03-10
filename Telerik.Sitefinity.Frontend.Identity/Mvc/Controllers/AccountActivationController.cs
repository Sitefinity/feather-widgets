using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Account Activation widget.
    /// </summary>
    [Localization(typeof(AccountActivationResources))]
    [ControllerToolboxItem(Name = "AccountActivation", Title = "Account activation", SectionName = "MvcWidgets", CssClass = "sfAccountActivationIcn")]
    public class AccountActivationController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Account Activation widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IAccountActivationModel Model
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
        /// <value>
        /// The name of the template that widget will be displayed.
        /// </value>
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

        #region Actions

        /// <summary>
        /// Renders appropriate view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            var model = this.Model.GetViewModel(this.HttpContext.Request.QueryString); 
            var fullTemplateName = this.templateNamePrefix + this.TemplateName;

            return this.View(fullTemplateName, model);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="IAccountActivationModel"/>.
        /// </returns>
        private IAccountActivationModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IAccountActivationModel>(this.GetType());
        }

        #endregion

        #region Private fields and constants

        private string templateName = "AccountActivation";
        private string templateNamePrefix = "AccountActivation.";

        private IAccountActivationModel model;

        #endregion
    }
}
