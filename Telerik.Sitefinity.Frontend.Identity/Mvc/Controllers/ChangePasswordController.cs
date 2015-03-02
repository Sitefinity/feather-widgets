using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.ChangePassword;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Change Password widget.
    /// </summary>
    [Localization(typeof(ChangePasswordResources))]
    [ControllerToolboxItem(Name = "ChangePasswordMVC", Title = "Change Password", SectionName = "MvcWidgets")]
    public class ChangePasswordController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Login Status widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IChangePasswordModel Model
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
        public ActionResult Index(bool passwordChanged = false, string error = null)
        {
            var viewModel = this.Model.GetViewModel();
            var fullTemplateName = this.templateNamePrefix + this.TemplateName; 

            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Sets the change password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult SetChangePassword(ChangePasswordInputModel model)
        {
            bool passwordChanged = false;
            string error = null;

            // Logic

            var pageUrl = this.Model.GetPageUrl(null);
            var queryString = string.Format("?passwordChanged={0}&error={1}", passwordChanged, error);
            return this.Redirect(pageUrl + queryString);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="ILoginStatusModel"/>.
        /// </returns>
        private IChangePasswordModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IChangePasswordModel>(this.GetType());
        }

        #endregion
        
        #region Private fields and constants

        private string templateName = "Default";
        private string templateNamePrefix = "ChangePassword.";

        private IChangePasswordModel model;

        #endregion
    }
}
