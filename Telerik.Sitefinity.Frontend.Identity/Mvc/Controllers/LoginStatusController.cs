using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.ActionFilters;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Login Status widget.
    /// </summary>
    [Localization(typeof(LoginStatusResources))]
    [ControllerToolboxItem(Name = "LoginStatusMvc", Title = "Login Status", SectionName = "MvcWidgets")]
    public class LoginStatusController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Login Status widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ILoginStatusModel Model
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
        /// Gets the is design mode.
        /// </summary>
        /// <value>The is design mode.</value>
        protected virtual bool IsDesignMode
        {
            get
            {
                return SystemManager.IsDesignMode;
            }
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

            return this.View(this.TemplateName, viewModel);
        }

        /// <summary>
        /// Returns JSON with the status of the user and his email, first and last names
        /// </summary>
        [JsonResultFilter]
        public ActionResult Status()
        {
            var response = this.Model.GetStatusViewModel();
            
            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="ILoginStatusModel"/>.
        /// </returns>
        private ILoginStatusModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<ILoginStatusModel>(this.GetType());
        }

        #endregion

        #region Private fields and constants

        private string templateName = "LoginStatus";
        private ILoginStatusModel model;

        #endregion
    }
}
