using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.ChangePassword;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Change Password widget.
    /// </summary>
    [Localization(typeof(ChangePasswordResources))]
    [ControllerToolboxItem(Name = "ChangePasswordMVC", Title = "Change password", SectionName = "MvcWidgets", CssClass = ChangePasswordController.WidgetIconCssClass)]
    public class ChangePasswordController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Change password widget model.
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
            if (SecurityManager.GetCurrentUserId() == Guid.Empty)
            {
                return this.Content(Res.Get<ChangePasswordResources>().LogInFirst);
            }

            var model = this.Model.GetViewModel();

            model.Error = error;
            model.PasswordChanged = passwordChanged;

            var fullTemplateName = this.templateNamePrefix + this.TemplateName;

            return this.View(fullTemplateName, model);
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
            string error = string.Empty;

            if (ModelState.IsValid)
            {
                try
                {
                    this.Model.ChangePassword(SecurityManager.GetCurrentUserId(), model.OldPassword, model.NewPassword);
                    passwordChanged = true;
                }
                catch (Exception)
                {
                    error = Res.Get<ChangePasswordResources>().ChangePasswordGeneralErrorMessage;
                }
            }
            else
            {
                try
                {
                    error = Res.Get<ChangePasswordResources>().Get(this.Model.GetErrorFromViewModel(this.ModelState));
                }
                catch (KeyNotFoundException)
                {
                    error = Res.Get<ChangePasswordResources>().ChangePasswordGeneralErrorMessage;
                }
            }

            string redirectUrl = string.Empty;

            if (passwordChanged && this.Model.ChangePasswordCompleteAction == ChangePasswordCompleteAction.RedirectToPage)
            {
                redirectUrl = this.Model.GetPageUrl(this.Model.ChangePasswordRedirectPageId);
            }
            else
            {
                error = HttpUtility.UrlEncode(error);
                var pageUrl = this.Model.GetPageUrl(null);
                var queryString = string.Format("?passwordChanged={0}&error={1}", passwordChanged, error);

                redirectUrl = pageUrl + queryString;
            }

            return this.Redirect(redirectUrl);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="IChangePasswordModel"/>.
        /// </returns>
        private IChangePasswordModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IChangePasswordModel>(this.GetType());
        }

        #endregion
        
        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfChangePasswordIcn sfMvcIcn";

        private string templateName = "ChangePassword";
        private string templateNamePrefix = "ChangePassword.";

        private IChangePasswordModel model;

        #endregion
    }
}
