using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Login Form widget.
    /// </summary>
    [Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes.Localization(typeof(LoginFormResources))]
    [ControllerToolboxItem(Name = "LoginFormMVC", Title = "Login Form", SectionName = "MvcWidgets")]
    public class LoginFormController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Login Status widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ILoginFormModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        /// <summary>
        /// Gets or sets the login form template name.
        /// </summary>
        /// <value>
        /// The login form template.
        /// </value>
        public string LoginFormTemplate { get; set; }

        /// <summary>
        /// Gets or sets the forgot password template.
        /// </summary>
        /// <value>
        /// The forgot password template.
        /// </value>
        public string ForgotPasswordTemplate { get; set; }

        /// <summary>
        /// Gets or sets the reset password template.
        /// </summary>
        /// <value>
        /// The reset password template.
        /// </value>
        public string ResetPasswordTemplate { get; set; }

        #endregion

        #region Actions

        public ActionResult Index()
        {
            var viewModel = this.Model.GetLoginFormViewModel();
            var fullTemplateName = this.loginFormTemplatePrefix + this.GetViewName(this.LoginFormTemplate);

            return this.View(fullTemplateName, viewModel);
        }

        public ActionResult ForgotPassword(ForgotPasswordViewModel model = null)
        {
            if (model == null)
            {
                model = this.Model.GetForgotPasswordViewModel();
            }

            var fullTemplateName = this.forgotPasswordFormTemplatePrefix + this.GetViewName(this.ForgotPasswordTemplate);

            return this.View(fullTemplateName, model);
        }

        public ActionResult PostForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Send password.

                model.EmailSent = true;
            }

            return this.RedirectToAction("ForgotPassword", new { model = model });
        }

        public ActionResult ResetPassword(ResetPasswordViewModel model = null)
        {
            if (model == null)
            {
                model = this.Model.GetResetPasswordViewModel();
            }

            var fullTemplateName = this.resetPasswordFormTemplatePrefix + this.GetViewName(this.ResetPasswordTemplate);

            return this.View(fullTemplateName, model);
        }

        public ActionResult PostResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Send password.

                model.PasswordChanged = true;
            }

            return this.RedirectToAction("ResetPassword", new { model = model });
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="ILoginFormModel"/>.
        /// </returns>
        private ILoginFormModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<ILoginFormModel>(this.GetType());
        }

        /// <summary>
        /// Gets the name of the view or the default view name.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <returns></returns>
        private string GetViewName(string viewName)
        {
            var viewNameToReturn = viewName;

            if (string.IsNullOrEmpty(viewNameToReturn))
            {
                viewNameToReturn = this.defaultTemplateName;
            }

            return viewNameToReturn;
        }

        #endregion

        #region Private fields and constants

        private string defaultTemplateName = "Default";
        private string loginFormTemplatePrefix = "LoginForm.";
        private string forgotPasswordFormTemplatePrefix = "ForgotPassword.";
        private string resetPasswordFormTemplatePrefix = "ResetPassword.";

        private ILoginFormModel model;

        #endregion
    }
}
