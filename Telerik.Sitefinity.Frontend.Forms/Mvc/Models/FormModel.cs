using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.Resources;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models
{
    /// <summary>
    /// This class represents the model used for Form widget.
    /// </summary>
    public class FormModel : ContentModelBase, IFormModel
    {
        #region Properties

        /// <inheritDoc/>
        public Guid FormId { get; set; }

        /// <inheritDoc/>
        public FormViewMode ViewMode { get; set; }

        /// <inheritDoc/>
        public bool UseCustomConfirmation { get; set; }

        /// <inheritDoc/>
        public CustomConfirmationMode CustomConfirmationMode { get; set; }

        /// <inheritDoc/>
        public string CustomConfirmationMessage
        {
            get
            {
                if (string.IsNullOrEmpty(this.customConfirmationMessage))
                {
                    return Res.Get<FormResources>().SuccessfullySubmittedMessage;
                }

                return this.customConfirmationMessage;
            }
            set
            {
                this.customConfirmationMessage = value;
            }
        }

        /// <inheritDoc/>
        public Guid CustomConfirmationPageId { get; set; }

        /// <inheritDoc/>
        public string CssClass { get; set; }

        /// <inheritDoc/>
        public bool UseAjaxSubmit { get; set; }

        /// <inheritDoc/>
        public string AjaxSubmitTargetUrl { get; set; }

        #endregion

        #region Public methods

        /// <inheritDoc/>
        public virtual FormViewModel GetViewModel()
        {
            if (this.FormId == Guid.Empty)
            {
                return null;
            }

            var viewModel = new FormViewModel()
            {
                FormId = this.FormId,
                ViewMode = this.ViewMode,
                CssClass = this.CssClass
            };

            if (!FormsManager.GetManager().GetForms().Any(f => f.Id == this.FormId && f.Status == ContentLifecycleStatus.Live && f.Visible))
            {
                viewModel.Error = Res.Get<FormsResources>().TheSpecifiedFormNoLongerExists;
            }

            viewModel.UseAjaxSubmit = this.UseAjaxSubmit;
            if (this.UseAjaxSubmit)
            {
                viewModel.AjaxSubmitTargetUrl = this.AjaxSubmitTargetUrl.IsNullOrEmpty() ? "~/Forms/Submit" : this.AjaxSubmitTargetUrl;
                viewModel.FormName = FormsManager.GetManager().GetForm(this.FormId).Name;
            }

            return viewModel;
        }

        /// <inheritDoc/>
        public virtual string GetViewPath()
        {
            var currentPackage = new PackageManager().GetCurrentPackage();
            if (string.IsNullOrEmpty(currentPackage))
            {
                currentPackage = "default";
            }

            var viewPath = FormsVirtualRazorResolver.Path + currentPackage + "/" + this.FormId.ToString("D") + ".cshtml";

            return viewPath;
        }

        /// <inheritDoc/>
        public virtual bool TrySubmitForm(FormCollection collection, string userHostAddress)
        {
            var success = false;

            var manager = FormsManager.GetManager();
            var form = manager.GetForm(this.FormId);

            if (this.IsValidForm(form, collection, manager))
            {
                var formData = new KeyValuePair<string, object>[collection.Count];
                for (int i = 0; i < collection.Count; i++)
                {
                    formData[i] = new KeyValuePair<string, object>(collection.Keys[i], collection[collection.Keys[i]]);
                }

                var formLanguage = SystemManager.CurrentContext.AppSettings.Multilingual ? CultureInfo.CurrentUICulture.Name : null;

                FormsHelper.SaveFormsEntry(form, formData, null, userHostAddress, ClaimsManager.GetCurrentUserId(), formLanguage);

                success = true;
            }

            return success;
        }

        /// <inheritDoc/>
        public virtual string GetRedirectPageUrl()
        {
            if (this.CustomConfirmationPageId == Guid.Empty)
            {
                var currentNode = SiteMapBase.GetActualCurrentNode();
                if (currentNode == null)
                    return null;

                this.CustomConfirmationPageId = currentNode.Id;
            }

            return HyperLinkHelpers.GetFullPageUrl(this.CustomConfirmationPageId);
        }

        /// <inheritDoc/>
        public virtual string GetSubmitMessage(bool submitedSuccessfully)
        {
            if (submitedSuccessfully)
            {
                return this.CustomConfirmationMessage;
            }

            return Res.Get<FormResources>().UnsuccessfullySubmittedMessage;
        }

        /// <inheritDoc/>
        public virtual bool IsValidForm(FormDescription form, FormCollection collection, FormsManager manager)
        {
            this.SanitizeFormCollection(collection);
            var behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
            foreach (var control in form.Controls)
            {
                if (control.IsLayoutControl)
                    continue;

                Type controlType;
                if (control.ObjectType.StartsWith("~/"))
                    controlType = FormsManager.GetControlType(control);
                else
                    controlType = TypeResolutionService.ResolveType(behaviorResolver.GetBehaviorObjectType(control), true);

                if (!typeof(IFormFieldController<IFormFieldModel>).IsAssignableFrom(controlType))
                    continue;

                var controlInstance = manager.LoadControl(control);
                var fieldControl = (IFormFieldController<IFormFieldModel>)behaviorResolver.GetBehaviorObject(controlInstance);

                var fieldValue = collection[fieldControl.MetaField.FieldName];

                if (!fieldControl.Model.IsValid(fieldValue))
                    return false;
            }

            return true;
        }

        /// <inheritDoc/>
        public virtual void SanitizeFormCollection(FormCollection collection)
        {
            const char ReplacementSymbol = '_';
            var forbidenSymbols = new char[] { '-' };

            if (collection != null)
            {
                var forbidenKeys = collection.AllKeys.Where(k => k.IndexOfAny(forbidenSymbols) >= 0);
                foreach (var key in forbidenKeys)
                {
                    var newKey = key.ToCharArray();
                    for (int i = 0; i < newKey.Length; i++)
                    {
                        if (forbidenSymbols.Contains(newKey[i]))
                        {
                            newKey[i] = ReplacementSymbol;
                        }
                    }

                    collection.Add(new string(newKey), collection[key]);
                    collection.Remove(key);
                }
            }
        }

        #endregion

        #region ContentModelBase

        /// <inheritDoc/>
        public override Type ContentType
        {
            get { return typeof(FormDescription); }
            set { }
        }

        /// <inheritDoc/>
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            return ((FormsManager)this.GetManager()).GetForms().Where(f => f.Framework == FormFramework.Mvc);
        }

        #endregion

        #region Private fields

        private string customConfirmationMessage;

        #endregion
    }
}