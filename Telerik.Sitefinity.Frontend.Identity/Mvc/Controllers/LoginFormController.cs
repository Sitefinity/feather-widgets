using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

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
            if (SecurityManager.GetCurrentUserId() == Guid.Empty)
            {
                var viewModel = this.Model.GetLoginFormViewModel();
                var fullTemplateName = this.loginFormTemplatePrefix + this.GetViewName(this.LoginFormTemplate);

                return this.View(fullTemplateName, viewModel);
            }
            else
            {
                return this.Content(Res.Get<LoginFormResources>().AlreadyLogedIn);
            }
        }

        public ActionResult ForgotPassword(bool emailSent = false, string error = null)
        {
            var model = this.Model.GetForgotPasswordViewModel();

            model.Error = error;
            model.EmailSent = emailSent;

            var fullTemplateName = this.forgotPasswordFormTemplatePrefix + this.GetViewName(this.ForgotPasswordTemplate);

            return this.View(fullTemplateName, model);
        }

        public ActionResult SendPasswordResetEmail(string email)
        {
            var viewModel = this.Model.SendResetPasswordEmail(email);
            var pageUrl = this.Model.GetPageUrl(null);
            var queryString = string.Format("emailSent={0}&error={1}", viewModel.EmailSent, viewModel.Error);
            return this.Redirect(string.Format("{0}/ForgotPassword?{1}", pageUrl, queryString));
        }

        public ActionResult ResetPassword(bool resetComplete = false, string error = null)
        {
            var model = this.Model.GetResetPasswordViewModel();
            
            model.Error = error;
            model.ResetComplete = resetComplete;
            model.SecurityToken = this.HttpContext.Request.QueryString.ToQueryString();

            var fullTemplateName = this.resetPasswordFormTemplatePrefix + this.GetViewName(this.ResetPasswordTemplate);

            return this.View(fullTemplateName, model);
        }

        public ActionResult SetResetPassword(ResetPasswordInputModel model)
        {
            bool resetComplete = false;
            string error = string.Empty;

            if (ModelState.IsValid)
            {
                try
                {
                    this.Model.ResetUserPassword(model.NewPassword, model.ResetPasswordAnswer);
                    resetComplete = true;
                }
                catch (NotSupportedException)
                {
                    error = Res.Get<LoginFormResources>().ResetPasswordNotEnabled;
                }
                catch (Exception)
                {
                    error = Res.Get<LoginFormResources>().ResetPasswordGeneralErrorMessage;
                }
            }
            else
            {
                try
                {
                    error = Res.Get<LoginFormResources>().Get(this.Model.GetErrorFromViewModel(this.ModelState));
                }
                catch (KeyNotFoundException)
                {
                    error = Res.Get<LoginFormResources>().ResetPasswordGeneralErrorMessage;
                }
            }

            error = HttpUtility.UrlEncode(error);

            var pageUrl = this.Model.GetPageUrl(null);
            var queryString = string.Format("resetComplete={0}&error={1}", resetComplete, error);
            return this.Redirect(string.Format("{0}/ResetPassword?{1}", pageUrl, queryString));
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
