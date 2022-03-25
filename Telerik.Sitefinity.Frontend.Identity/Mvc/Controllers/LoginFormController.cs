﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Security;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.ProviderExceptions;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Login Form widget.
    /// </summary>
    [Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes.Localization(typeof(LoginFormResources))]
    [ControllerToolboxItem(
        Name = LoginFormController.WidgetName,
        Title = nameof(LoginFormResources.LoginControlTitle),
        Description = nameof(LoginFormResources.LoginControlDescription),
        ResourceClassId = nameof(LoginFormResources),
        SectionName = "Login",
        CssClass = LoginFormController.WidgetIconCssClass)]
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
            Web.PageRouteHandler.RegisterUserAuthenticatedOutputCacheVariation();

            var viewModel = this.Model.GetLoginFormViewModel();

            var fullTemplateName = this.loginFormTemplatePrefix + this.LoginFormTemplate;

            return this.View(fullTemplateName, viewModel);
        }

        [HttpPost]
        public ActionResult Index(LoginFormViewModel model)
        {
            if (!AntiCsrfHelpers.IsValidCsrfToken(this.Request?.Form))
                return new EmptyResult();

            if (ModelState.IsValid)
            {
                model = this.Model.Authenticate(model, this.ControllerContext.HttpContext);
            }

            this.Model.InitializeLoginViewModel(model);

            var fullTemplateName = this.loginFormTemplatePrefix + this.LoginFormTemplate;
            return this.View(fullTemplateName, model);
        }

        public ActionResult LoginExternalProvider(string model)
        {
            if (!string.IsNullOrEmpty(model))
            {
                var provider = ClaimsManager.CurrentAuthenticationModule.ExternalAuthenticationProviders.FirstOrDefault(x => x.Title == model);

                if (provider != null)
                {
                    this.Model.AuthenticateExternal(provider.Name, this.ControllerContext.HttpContext);
                }

                return new EmptyResult();
            }

            return new EmptyResult();
        }

        public ActionResult ForgotPassword(bool emailSent = false, string email = null, string error = null)
        {
            var model = this.Model.GetForgotPasswordViewModel(email, emailSent, error);

            var fullTemplateName = this.forgotPasswordTemplatePrefix + this.ForgotPasswordTemplate;

            return this.View(fullTemplateName, model);
        }

        [HttpPost]
        public ActionResult SendPasswordResetEmail(string email)
        {
            var viewModel = this.Model.SendResetPasswordEmail(email);
            return this.ForgotPassword(viewModel.EmailSent, viewModel.Email, viewModel.Error);
        }

        public ActionResult ResetPassword()
        {
            PageRouteHandler.RegisterCustomOutputCacheVariation(new ResetPasswordCacheVariation());

            var query = this.HttpContext.Request.QueryString;
            var queryString = query.ToString();
            var securityToken = queryString;
            var resetComplete = false;
            var error = string.Empty;

            var index = queryString.IndexOf("&resetComplete");
            if (index > 0)
            {
                securityToken = queryString.Substring(0, index);
                resetComplete = Convert.ToBoolean(query["resetComplete"]);
                error = query["error"];
            }

            var model = this.Model.GetResetPasswordViewModel(securityToken, resetComplete, error);

            var fullTemplateName = this.resetPasswordTemplatePrefix + this.ResetPasswordTemplate;

            return this.View(fullTemplateName, model);
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordInputModel model)
        {
            bool resetComplete = false;
            string error = string.Empty;

            if (ModelState.IsValid)
            {
                try
                {
                    var securityParams = HttpUtility.ParseQueryString(model.SecurityToken);
                    this.Model.ResetUserPassword(model.NewPassword, model.ResetPasswordAnswer, securityParams);
                    resetComplete = true;
                }
                catch (NotSupportedException)
                {
                    error = Res.Get<LoginFormResources>().ResetPasswordNotEnabled;
                }
                catch (ResetPasswordUserNotFoundException)
                {
                    error = Res.Get<LoginFormResources>().ResetPasswordIncorrectResetPasswordLink;
                }
                catch (Exception ex)
                {
                    if (string.Compare(ex.Message, Res.Get<ErrorMessages>().WrongPasswordAnswer) == 0)
                    {
                        error = Res.Get<LoginFormResources>().ResetPasswordIncorrectAnswerErrorMessage;
                    }
                    else if (ex is ValidationFailedProviderException)
                    {
                        error = ex.Message;
                    }
                    else
                    {
                        error = Res.Get<LoginFormResources>().ResetPasswordGeneralErrorMessage;
                    }
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

            var validator = ObjectFactory.Resolve<IRedirectUriValidator>();
            var pageUrl = this.Model.GetPageUrl(null);

            if (validator.IsValid(pageUrl))
            {
                var queryString = string.Format("{0}&resetComplete={1}&error={2}", model.SecurityToken, resetComplete, error);
                return this.Redirect(string.Format("{0}/ResetPassword?{1}", pageUrl, queryString));
            }

            return this.Redirect("~/");
        }

        /// <inheritDocs/>
        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
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

        internal const string WidgetIconCssClass = "sfLoginIcn sfMvcIcn";

        private string loginFormTemplateName = "LoginForm";
        private string forgotPasswordTemplateName = "ForgottenPassword";
        private string resetPasswordTemplateName = "ResetPassword";
        private string loginFormTemplatePrefix = "LoginForm.";
        private string forgotPasswordTemplatePrefix = "ForgotPassword.";
        private string resetPasswordTemplatePrefix = "ResetPassword.";

        private ILoginFormModel model;
        private const string WidgetName = "LoginForm_MVC";
        #endregion
    }
}
