using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Security;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Account Activation widget.
    /// </summary>
    [Localization(typeof(AccountActivationResources))]
    [ControllerToolboxItem(
        Name = AccountActivationController.WidgetName,
        Title = nameof(AccountActivationResources.AccountActivationWidgetTitle),
        Description = nameof(AccountActivationResources.AccountActivationWidgetDescription),
        ResourceClassId = nameof(AccountActivationResources),
        SectionName = "Users",
        CssClass = AccountActivationController.WidgetIconCssClass)]
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
            Web.PageRouteHandler.SetNoCache();

            var model = this.Model.GetViewModel(); 
            var fullTemplateName = this.templateNamePrefix + this.TemplateName;

            return this.View(fullTemplateName, model);
        }

        /// <summary>
        /// Sends again activation link.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="qs">The query string.</param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult SendAgainActivationLink(AccountActivationViewModel model, string qs)
        {
            if (!AntiCsrfHelpers.IsValidCsrfToken(this.Request?.Form) || !this.ModelState.IsValid)
                return new EmptyResult();

            var url = RouteHelper.ResolveUrl(this.Url.Action("Index"), UrlResolveOptions.Absolute);
            this.Model.SendAgainActivationLink(model, url);

            var fullTemplateName = this.templateNamePrefix + this.TemplateName;

            return this.View(fullTemplateName, model);
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
        /// The <see cref="IAccountActivationModel"/>.
        /// </returns>
        private IAccountActivationModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IAccountActivationModel>(this.GetType());
        }

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfAccountActivationIcn sfMvcIcn";

        private string templateName = "AccountActivation";
        private string templateNamePrefix = "AccountActivation.";

        private IAccountActivationModel model;
        private const string WidgetName = "AccountActivation_MVC";
        #endregion
    }
}
