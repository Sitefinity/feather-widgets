using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
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
                    this.customConfirmationMessage = this.FormData == null ? (Lstring)Res.Get<FormResources>().SuccessfullySubmittedMessage : this.FormData.SuccessMessage;
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

        /// <summary>
        /// Represents the current form
        /// </summary>        
        public FormDescription FormData
        {
            get
            {
                FormDescription descr = null;
                if (this.FormId != Guid.Empty)
                {
                    var manager = FormsManager.GetManager();
                    if (this.FormId != Guid.Empty)
                    {
                        descr = manager.GetForm(this.FormId);
                    }
                }

                return descr;
            }
        }

        /// <inheritDoc/>
        public virtual bool NeedsRedirect
        {
            get
            {
                if (this.UseCustomConfirmation)
                    return this.CustomConfirmationMode == CustomConfirmationMode.RedirectToAPage;
                else
                    return this.FormData.SubmitAction == SubmitAction.PageRedirect;
            }
        }

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
        public virtual SubmitStatus TrySubmitForm(FormCollection collection, HttpFileCollectionBase files, string userHostAddress)
        {
            var manager = FormsManager.GetManager();
            var form = manager.GetForm(this.FormId);

            if (!this.ValidateFormSubmissionRestrictions())
                return SubmitStatus.RestrictionViolation;

            if (!this.IsValidForm(form, collection, manager))
                return SubmitStatus.InvalidEntry;

            var formFields = new HashSet<string>(form.Controls.Select(this.FormFieldName).Where((f) => !string.IsNullOrEmpty(f)));

            var formData = new List<KeyValuePair<string, object>>(collection.Count);
            for (int i = 0; i < collection.Count; i++)
            {
            	if (formFields.Contains(collection.Keys[i]))
            	{
                	formData.Add(new KeyValuePair<string, object>(collection.Keys[i], collection[collection.Keys[i]]));
            	}
			}

			var formLanguage = SystemManager.CurrentContext.AppSettings.Multilingual ? CultureInfo.CurrentUICulture.Name : null;
			FormsHelper.SaveFormsEntry(form, formData, null, userHostAddress, ClaimsManager.GetCurrentUserId(), formLanguage);
            

            return SubmitStatus.Success;
        }

        /// <inheritDoc/>
        public virtual string GetRedirectPageUrl()
        {
            if (!this.UseCustomConfirmation && !string.IsNullOrEmpty(this.FormData.RedirectPageUrl))
            {
                return this.FormData.RedirectPageUrl;
            }
            else if (this.CustomConfirmationPageId == Guid.Empty)
            {
                var currentNode = SiteMapBase.GetActualCurrentNode();
                if (currentNode == null)
                    return null;

                this.CustomConfirmationPageId = currentNode.Id;
            }

            return HyperLinkHelpers.GetFullPageUrl(this.CustomConfirmationPageId);
        }

        /// <inheritDoc/>
        public virtual string GetSubmitMessage(SubmitStatus submitedSuccessfully)
        {
            switch (submitedSuccessfully)
            {
                case SubmitStatus.Success:
                    return this.CustomConfirmationMessage;
                case SubmitStatus.InvalidEntry:
                    return Res.Get<FormResources>().UnsuccessfullySubmittedMessage;
                case SubmitStatus.RestrictionViolation:
                    return Res.Get<FormsResources>().YouHaveAlreadySubmittedThisForm;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Validates the form against the preset submit restrictions.
        /// </summary>
        protected virtual bool ValidateFormSubmissionRestrictions()
        {
            string errorMessage;
            var isValid = FormsHelper.ValidateFormSubmissionRestrictions(this.FormData, ClaimsManager.GetCurrentUserId(), HttpContext.Current.Request.UserHostAddress, out errorMessage);

            return isValid;
        }

        /// <summary>
        /// Determines whether a form is valid or not.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="manager">The manager.</param>
        /// <returns>true if form is valid, false otherwise.</returns>
        protected virtual bool IsValidForm(FormDescription form, FormCollection collection, FormsManager manager)
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
        
                if (!controlType.ImplementsGenericInterface(typeof(IFormElementController<>)))
                    continue;
                
                var controlInstance = manager.LoadControl(control);
                var controlBehaviorObject = behaviorResolver.GetBehaviorObject(controlInstance);

                if (controlBehaviorObject.GetType().ImplementsGenericInterface(typeof(IFormFieldController<>)))
                {
                    var formField = (IFormFieldController<IFormFieldModel>)controlBehaviorObject;
                    var fieldValue = collection[formField.MetaField.FieldName];

                    if (!formField.Model.IsValid(fieldValue))
                        return false;
                }
                else
                {
                    var formElement = (IFormElementController<IFormElementdModel>)controlBehaviorObject;
                    if (!formElement.IsValid())
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Sanitizes the form collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        protected virtual void SanitizeFormCollection(FormCollection collection)
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

        #region Private methods

        private string FormFieldName(FormControl control)
        {
            if (control.IsLayoutControl)
                return null;

            var behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
            var controlInstance = FormsManager.GetManager().LoadControl(control);
            var controller = behaviorResolver.GetBehaviorObject(controlInstance);
            var fieldController = controller as IFormFieldControl;

            if (fieldController != null && fieldController.MetaField != null)
                return fieldController.MetaField.FieldName;
            else
                return null;
        }

        #endregion

        #region Private fields

        private string customConfirmationMessage;

        #endregion
    }
}