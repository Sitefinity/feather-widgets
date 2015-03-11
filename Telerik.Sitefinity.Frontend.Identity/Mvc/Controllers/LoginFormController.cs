using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Login Form widget.
    /// </summary>
    [Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes.Localization(typeof(LoginFormResources))]
    [ControllerToolboxItem(Name = "LoginFormMVC", Title = "Login form", SectionName = "MvcWidgets", CssClass = "sfLoginIcn")]
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
        public string LoginFormTemplate 
        {
            get { return this.loginFormTemplateName; }
            set { this.loginFormTemplateName = value; } 
        }

        /// <summary>
        /// Gets or sets the forgot password template.
        /// </summary>
        /// <value>
        /// The forgot password template.
        /// </value>
        public string ForgotPasswordTemplate
        {
            get { return this.forgotPasswordTemplateName; }
            set { this.forgotPasswordTemplateName = value; }
        }

        /// <summary>
        /// Gets or sets the reset password template.
        /// </summary>
        /// <value>
        /// The reset password template.
        /// </value>
        public string ResetPasswordTemplate
        {
            get { return this.resetPasswordTemplateName; }
            set { this.resetPasswordTemplateName = value; }
        }

        #endregion

        #region Actions

        public ActionResult Index()
        {
            var viewModel = this.Model.GetLoginFormViewModel();
            var fullTemplateName = this.loginFormTemplatePrefix + this.LoginFormTemplate;

            return this.View(fullTemplateName, viewModel);
        }

        [HttpPost]
        public ActionResult Index(LoginFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user;
                UserLoggingReason result = SecurityManager.AuthenticateUser(
                    this.Model.MembershipProvider,
                    model.UserName,
                    model.Password,
                    model.RememberMe,
                    out user);

                if (result == UserLoggingReason.Unknown)
                {
                    model.IncorrectCredentials = true;
                }
                else
                {
                    var redirectUrl = this.Model.GetPageUrl(this.Model.LoginRedirectPageId);
                    SFClaimsAuthenticationManager.ProcessRejectedUser(this.ControllerContext.HttpContext, redirectUrl);

                    return this.Redirect(redirectUrl);
                }
            }

            var fullTemplateName = this.loginFormTemplatePrefix + this.LoginFormTemplate;
            return this.View(fullTemplateName, model);
        }

        public ActionResult ForgotPassword(bool emailSent = false, string error = null)
        {
            var model = this.Model.GetForgotPasswordViewModel();

            model.Error = error;
            model.EmailSent = emailSent;

            var fullTemplateName = this.forgotPasswordTemplatePrefix + this.ForgotPasswordTemplate;

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

            var fullTemplateName = this.resetPasswordTemplatePrefix + this.ResetPasswordTemplate;

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
                    this.Model.ResetUserPassword(model.NewPassword, model.ResetPasswordAnswer, this.HttpContext.Request.QueryString);
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

        #endregion

        #region Private fields and constants

        private string loginFormTemplateName = "LoginForm";
        private string forgotPasswordTemplateName = "ForgottenPassword";
        private string resetPasswordTemplateName = "ResetPassword";
        private string loginFormTemplatePrefix = "LoginForm.";
        private string forgotPasswordTemplatePrefix = "ForgotPassword.";
        private string resetPasswordTemplatePrefix = "ResetPassword.";

        private ILoginFormModel model;

        #endregion
    }
}
