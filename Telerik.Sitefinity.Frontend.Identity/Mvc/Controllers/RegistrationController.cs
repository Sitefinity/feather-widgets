using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Security;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.CSRF;
using Telerik.Sitefinity.Services.Captcha;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Registration widget.
    /// </summary>
    [Localization(typeof(RegistrationResources))]
    [ControllerToolboxItem(
        Name = RegistrationController.WidgetName,
        Title = nameof(RegistrationResources.RegistrationWidgetTitle),
        Description = nameof(RegistrationResources.RegistrationWidgetDescription),
        ResourceClassId = nameof(RegistrationResources),
        SectionName = "Users",
        CssClass = RegistrationController.WidgetIconCssClass)]
    public class RegistrationController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Registration widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IRegistrationModel Model
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
            var fullTemplateName = this.templateNamePrefix + this.TemplateName;

            var viewModel = new RegistrationViewModel();

            // Caching happens per QS as "sal" is used in some scenarios to determine the current email
            this.Model.InitializeViewModel(viewModel);

            this.ViewBag.ShowSuccessfulRegistrationMsg = false;
            this.ViewBag.Error = this.Model.GetError();

            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Posts the registration form.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        [HttpPost]
        public ActionResult Index(RegistrationViewModel viewModel)
        {
            if (!AntiCsrfHelpers.IsValidCsrfToken(this.Request?.Form))
                return new EmptyResult();

            if (!this.ValidateCaptcha(out string errorMessage))
            {
                this.ViewBag.Error = errorMessage;
                return this.View(this.templateNamePrefix + this.TemplateName, viewModel);
            }

            return RegisterUser(viewModel);
        }

        internal ActionResult RegisterUser(RegistrationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var status = this.Model.RegisterUser(viewModel);
                if (status == MembershipCreateStatus.Success || status == MembershipCreateStatus.DuplicateEmail || status == MembershipCreateStatus.DuplicateUserName)
                {
                    if (this.Model.ActivationMethod == ActivationMethod.AfterConfirmation)
                    {
                        this.ViewBag.ShowActivationMsg = true;
                    }

                    if (this.Model.SuccessfulRegistrationAction == SuccessfulRegistrationAction.ShowMessage)
                    {
                        this.ViewBag.ShowSuccessfulRegistrationMsg = true;
                    }
                    else if (this.Model.SuccessfulRegistrationAction == SuccessfulRegistrationAction.RedirectToPage)
                    {
                        return this.Redirect(this.Model.GetPageUrl(this.Model.SuccessfulRegistrationPageId));
                    }
                }
                else
                {
                    this.ViewBag.Error = this.Model.ErrorMessage(status);
                }
            }

            this.Model.InitializeViewModel(viewModel);

            var fullTemplateName = this.templateNamePrefix + this.TemplateName;
            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Resends the confirmation email.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        public JsonResult ResendConfirmationEmail(string email)
        {
            Web.PageRouteHandler.SetNoCache();
            var isSend = this.Model.ResendConfirmationEmail(email);

            return this.Json(isSend, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoginExternalProvider(string model)
        {
            // Cache per key as the model is used to determine the provider
            this.HttpContext.Request.ParamsGet("model");

            if (!string.IsNullOrEmpty(model))
            {
                var provider = ClaimsManager.CurrentAuthenticationModule.ExternalAuthenticationProviders.FirstOrDefault(x => x.Title == model);

                if (provider != null)
                {
                    this.Model.AuthenticateExternal(provider.Name, this.ControllerContext.HttpContext);
                }
            }

            return new EmptyResult();
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
        /// The <see cref="IRegistrationModel"/>.
        /// </returns>
        private IRegistrationModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IRegistrationModel>(this.GetType());
        }

        private bool ValidateCaptcha(out string errorMessage)
        {
            errorMessage = string.Empty;

            var captchaResponse = this.Request?.Form["sf_captcha_response"];

            return CaptchaService.Validate(captchaResponse, out errorMessage, true);
        }

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfCreateAccountIcn sfMvcIcn";

        private string templateName = "RegistrationForm";
        private IRegistrationModel model;
        private string templateNamePrefix = "Registration.";
        private bool emailAddressShouldBeTheUsername = false;
        private const string WidgetName = "Registration_MVC";
        #endregion
    }
}
