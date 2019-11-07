using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.TemporaryStorage;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.Captcha;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Events;
using Telerik.Sitefinity.Modules.Forms.Web;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Operations;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities;
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
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormModel"/> class.
        /// </summary>
        public FormModel()
        {
            this.eventFactory = new FormEventsFactory();
        }

        #endregion

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
        public string CssClass
        {
            get
            {
                if (this.cssClass == null && this.FormData != null)
                    this.cssClass = this.FormData.CssClass;

                return this.cssClass;
            }

            set
            {
                this.cssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control to use Ajax submit when the form submit button is clicked
        /// </summary>
        public bool UseAjaxSubmit { get; set; }

        /// <summary>
        /// Gets or sets the submit URL when using AJAX for submitting.
        /// </summary>
        /// <value>
        /// The AJAX submit URL.
        /// </value>
        public string AjaxSubmitUrl { get; set; }

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
                    descr = manager.GetForms().FirstOrDefault(f => f.Id == this.FormId && f.Visible);
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
                    return this.FormData != null && this.FormData.SubmitAction == SubmitAction.PageRedirect;
            }
        }

        public bool IsMultiStep { get; set; }

        /// <inheritDoc/>
        public string ConnectorSettings { get; set; }

        /// <inheritDoc/>
        public FormCollection FormCollection { get; set; }

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
                ViewMode = this.ViewMode,
                CssClass = this.CssClass,
                UseAjaxSubmit = this.UseAjaxSubmit,
                FormId = this.FormId.ToString("D"),
                IsMultiStep = this.IsMultiStep,
                FormCollection = this.FormCollection
            };

            if (this.FormData != null && this.AllowRenderForm())
            {
                viewModel.FormRules = this.GetFormRulesViewModel(this.FormData);

                if (viewModel.UseAjaxSubmit)
                {
                    string baseUrl;
                    if (this.AjaxSubmitUrl.IsNullOrEmpty())
                    {
                        var currentNode = SiteMapBase.GetCurrentNode();
                        baseUrl = currentNode != null ? currentNode.Url + "/AjaxSubmit" : string.Empty;
                    }
                    else
                    {
                        baseUrl = this.AjaxSubmitUrl;
                    }

                    viewModel.AjaxSubmitUrl = baseUrl.StartsWith("~/") ? RouteHelper.ResolveUrl(baseUrl, UrlResolveOptions.Rooted) : baseUrl;
                    viewModel.SuccessMessage = this.GetSubmitMessage(SubmitStatus.Success);

                    if (this.NeedsRedirect)
                    {
                        viewModel.RedirectUrl = this.GetRedirectPageUrl();
                        if (viewModel.RedirectUrl.StartsWith("~/"))
                            viewModel.RedirectUrl = RouteHelper.ResolveUrl(viewModel.RedirectUrl, UrlResolveOptions.Rooted);
                    }
                }
            }
            else
            {
                viewModel.Error = Res.Get<FormsResources>().TheSpecifiedFormNoLongerExists;
            }

            return viewModel;
        }

        /// <inheritDoc/>
        public virtual SubmitStatus TrySubmitForm(FormCollection collection, HttpFileCollectionBase files, string userHostAddress)
        {
            var manager = FormsManager.GetManager();

            var formIdString = collection[FormIdName];
            Guid formId;

            if (!string.IsNullOrWhiteSpace(formIdString) && Guid.TryParse(formIdString, out formId))
            {
                this.FormId = formId;
            }

            var form = manager.GetForm(this.FormId);

            var formEntry = new FormEntryDTO(form);
            var formSubmition = new FormsSubmitionHelper();

            if (!this.ValidateFormSubmissionRestrictions(formSubmition, formEntry))
                return SubmitStatus.RestrictionViolation;

            Dictionary<string, IFormFieldController<IFormFieldModel>> currentFormFields;
            List<IFormElementController<IFormElementModel>> formElements;

            this.LoadFormControls(form, collection, files, manager, out currentFormFields, out formElements);

            Dictionary<IFormFieldController<IFormFieldModel>, bool> fieldControls;

            if (!this.RaiseFormValidatingEvent(formEntry, currentFormFields, collection, files) || !this.IsValidForm(form, collection, files, currentFormFields, formElements, out fieldControls))
                return SubmitStatus.InvalidEntry;

            var formFields = fieldControls.Select(p => new { FieldName = this.FormFieldName(p.Key), CanSave = p.Value }).Where(field => !string.IsNullOrEmpty(field.FieldName));
            var postedFiles = new Dictionary<string, List<FormHttpPostedFile>>();
            if (files != null)
            {
                for (int i = 0; i < files.AllKeys.Length; i++)
                {
                    if (formFields.FirstOrDefault(p => p.FieldName == files.AllKeys[i]) != null)
                    {
                        postedFiles[files.AllKeys[i]] = files.GetMultiple(files.AllKeys[i]).Where(f => !f.FileName.IsNullOrEmpty()).Select(f =>
                            new FormHttpPostedFile()
                            {
                                FileName = f.FileName,
                                ContentLength = f.ContentLength,
                                ContentType = f.ContentType,
                                InputStream = f.InputStream
                            }).ToList();
                    }
                }
            }

            var formData = new Dictionary<string, object>(collection.Count);
            for (int i = 0; i < collection.Count; i++)
            {
                var formField = formFields.FirstOrDefault(p => p.FieldName == collection.Keys[i]);
                if (formField != null)
                {
                    formData.Add(collection.Keys[i], formField.CanSave ? collection[collection.Keys[i]] : string.Empty);
                }
            }

            formEntry.PostedData.FormsData = formData;
            formEntry.PostedData.Files = postedFiles;

            var formRules = new List<FormRule>();
            if (!string.IsNullOrWhiteSpace(form.Rules))
            {
                formRules = JsonConvert.DeserializeObject<List<FormRule>>(form.Rules);
            }

            formEntry.NotificationEmails = this.GetSendNotificationEmails(formRules, collection);

            this.UpdateCustomConfirmationMode(formRules, collection);

            this.SetConnectorSettingsToContext();

            if (this.RaiseFormSavingEvent(formEntry))
            {
                formSubmition.Save(formEntry);
                this.RaiseFormSavedEvent(formEntry);

                this.InvalidateCaptchas();

                return SubmitStatus.Success;
            }
            else
            {
                return SubmitStatus.RestrictionViolation;
            }
        }

        /// <summary>
        /// Allows the render form.
        /// </summary>
        /// <returns></returns>
        public bool AllowRenderForm()
        {
            bool renderForm = true;

            var currentLanguage = CultureInfo.CurrentUICulture;

            // Do not display form if the form is not translated in the current language
            if (this.FormData.IsPublished(currentLanguage) == false)
            {
                renderForm = false;
            }

            return renderForm;
        }

        /// <summary>
        /// Raises the before form action event.
        /// </summary>
        public virtual bool RaiseBeforeFormActionEvent()
        {
            var manager = FormsManager.GetManager();
            var form = manager.GetForm(this.FormId);

            var formEntry = new FormEntryDTO(form);
            var formEvent = this.eventFactory.GetBeforeFormActionEvent(formEntry);

            return !this.IsEventCancelled(formEvent);
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
                    return this.InvalidInputMessage;
                case SubmitStatus.RestrictionViolation:
                    return Res.Get<FormsResources>().YouHaveAlreadySubmittedThisForm;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Validates the form against the preset submit restrictions.
        /// </summary>
        protected virtual bool ValidateFormSubmissionRestrictions(FormsSubmitionHelper formsSubmition, FormEntryDTO formEntry)
        {
            string errorMessage;
            var isValid = formsSubmition.ValidateRestrictions(formEntry, out errorMessage);

            return isValid;
        }

        /// <summary>
        /// Determines whether a form is valid or not.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="manager">The manager.</param>
        /// <returns>true if form is valid, false otherwise.</returns>
        protected virtual bool IsValidForm(FormDescription form, FormCollection collection, HttpFileCollectionBase files, FormsManager manager, out Dictionary<IFormFieldController<IFormFieldModel>, bool> fieldControls)
        {
            fieldControls = new Dictionary<IFormFieldController<IFormFieldModel>, bool>();
            this.captchaController = null;

            this.ResetInvalidInputMessage();
            this.SanitizeFormCollection(collection);
            var behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();

            var skipFields = this.SplitCsv(this.GetFormCollectionItemValue(collection, FormSkipFieldsInputName));
            var hiddenFields = this.SplitCsv(this.GetFormCollectionItemValue(collection, FormHiddenFieldsInputName));

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
                var formField = controlBehaviorObject as IFormFieldController<IFormFieldModel>;

                if (formField != null)
                {
                    if (!this.RaiseFormFieldValidatingEvent(formField))
                        return false;

                    var fieldName = formField.MetaField.FieldName;

                    IList<HttpPostedFileBase> multipleFiles = files != null ? files.GetMultiple(fieldName) : null;
                    object fieldValue;

                    if (multipleFiles != null && multipleFiles.Count() > 0)
                    {
                        fieldValue = multipleFiles;
                    }
                    else if (collection.Keys.Contains(fieldName))
                    {
                        collection[fieldName] = collection[fieldName] ?? string.Empty;
                        fieldValue = collection[fieldName];
                    }
                    else
                    {
                        fieldValue = null;
                    }

                    var hideableModel = formField.Model as IHideable;
                    var canSaveField = hideableModel != null ? this.CanSaveField(hiddenFields, skipFields, controlInstance.ID, form.Rules, hideableModel.Hidden) : true;
                    fieldControls.Add(formField, canSaveField);

                    if (canSaveField && !formField.Model.IsValid(fieldValue))
                    {
                        this.SetFormFieldInvalidInputMessage(formField);
                        return false;
                    }
                }
                else
                {
                    var formElement = (IFormElementController<IFormElementModel>)controlBehaviorObject;
                    if (!formElement.IsValid())
                    {
                        this.SetFormElementInvalidInputMessage(formElement);
                        return false;
                    }

                    if (formElement is Controllers.CaptchaController formElementController)
                    {
                        this.captchaController = formElementController;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether a form is valid or not.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="files">The files.</param>
        /// <param name="formFields">The form fields.</param>
        /// <param name="formElements">The form elements.</param>
        /// <param name="fieldControls">The field controls.</param>
        /// <returns>true if form is valid, false otherwise.</returns>
        protected virtual bool IsValidForm(FormDescription form, FormCollection collection, HttpFileCollectionBase files, Dictionary<string, IFormFieldController<IFormFieldModel>> formFields, List<IFormElementController<IFormElementModel>> formElements, out Dictionary<IFormFieldController<IFormFieldModel>, bool> fieldControls)
        {
            fieldControls = new Dictionary<IFormFieldController<IFormFieldModel>, bool>();
            this.captchaController = null;

            this.ResetInvalidInputMessage();
            this.SanitizeFormCollection(collection);

            var skipFields = this.SplitCsv(this.GetFormCollectionItemValue(collection, FormSkipFieldsInputName));
            var hiddenFields = this.SplitCsv(this.GetFormCollectionItemValue(collection, FormHiddenFieldsInputName));

            foreach (var formField in formFields)
            {
                if (!this.RaiseFormFieldValidatingEvent(formField.Value))
                    return false;

                var fieldName = formField.Value.MetaField.FieldName;

                IList<HttpPostedFileBase> multipleFiles = files != null ? files.GetMultiple(fieldName) : null;
                object fieldValue;

                if (multipleFiles != null && multipleFiles.Count() > 0)
                {
                    fieldValue = multipleFiles;
                }
                else if (collection.Keys.Contains(fieldName))
                {
                    collection[fieldName] = collection[fieldName] ?? string.Empty;
                    fieldValue = collection[fieldName];
                }
                else
                {
                    fieldValue = null;
                }

                var hideableModel = formField.Value.Model as IHideable;
                var canSaveField = hideableModel != null ? this.CanSaveField(hiddenFields, skipFields, formField.Key, form.Rules, hideableModel.Hidden) : true;
                fieldControls.Add(formField.Value, canSaveField);

                if (canSaveField && !formField.Value.Model.IsValid(fieldValue))
                {
                    this.SetFormFieldInvalidInputMessage(formField.Value);
                    return false;
                }
            }

            foreach (var formElement in formElements)
            {
                if (!formElement.IsValid())
                {
                    this.SetFormElementInvalidInputMessage(formElement);
                    return false;
                }

                if (formElement is Controllers.CaptchaController formElementController)
                {
                    this.captchaController = formElementController;
                }
            }

            return true;
        }

        protected void LoadFormControls(FormDescription form, FormCollection collection, HttpFileCollectionBase files, FormsManager manager, out Dictionary<string, IFormFieldController<IFormFieldModel>> formFields, out List<IFormElementController<IFormElementModel>> formElements)
        {
            formFields = new Dictionary<string, IFormFieldController<IFormFieldModel>>();
            formElements = new List<IFormElementController<IFormElementModel>>();

            this.SanitizeFormCollection(collection);
            var behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();

            var skipFields = this.SplitCsv(this.GetFormCollectionItemValue(collection, FormSkipFieldsInputName));
            var hiddenFields = this.SplitCsv(this.GetFormCollectionItemValue(collection, FormHiddenFieldsInputName));

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
                var formField = controlBehaviorObject as IFormFieldController<IFormFieldModel>;

                if (formField != null)
                {
                    formFields.Add(controlInstance.ID, formField);
                }
                else
                {
                    var formElement = (IFormElementController<IFormElementModel>)controlBehaviorObject;
                    formElements.Add(formElement);
                }
            }
        }

        /// <summary>
        /// Resets the invalid input message.
        /// </summary>
        protected virtual void ResetInvalidInputMessage()
        {
            this.InvalidInputMessage = Res.Get<FormResources>().UnsuccessfullySubmittedMessage;
        }

        /// <summary>
        /// Sets the form field invalid input message.
        /// </summary>
        /// <param name="formField">The form field.</param>
        protected virtual void SetFormFieldInvalidInputMessage(IFormFieldController<IFormFieldModel> formField)
        {
            string invalidFieldName = formField.MetaField.Title;

            string errorMessage = null;
            var elementModel = formField.Model as FormElementModel;
            if (elementModel != null)
            {
                if (!string.IsNullOrEmpty(elementModel.Validator.ErrorMessage))
                {
                    errorMessage = elementModel.Validator.ErrorMessage;
                }
            }

            if (string.IsNullOrEmpty(errorMessage))
            {
                errorMessage = Res.Get<FormResources>().InvalidInputErrorMessage;
            }

            if (errorMessage.IndexOf("{0}") != -1)
            {
                errorMessage = string.Format(errorMessage, invalidFieldName);
            }

            this.InvalidInputMessage = errorMessage;
        }

        /// <summary>
        /// Sets the form element invalid input message.
        /// </summary>
        /// <param name="formElement">The form element.</param>
        protected virtual void SetFormElementInvalidInputMessage(IFormElementController<IFormElementModel> formElement)
        {
            // Current default IFormElementController<IFormElementModel> elements - Section Header, Submit Button, Captcha.
            if (formElement.Model != null && (formElement.Model is ICaptchaModel))
            {
                this.InvalidInputMessage = Res.Get<FormsResources>().CaptchaErrorMessage;
            }
        }

        /// <summary>
        /// Gets or sets the invalid input message.
        /// </summary>
        /// <value>The invalid input message.</value>
        protected virtual string InvalidInputMessage { get; set; }

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

        /// <summary>
        /// Raises the form saved event.
        /// </summary>
        /// <param name="formEntry">The form entry.</param>
        protected virtual void RaiseFormSavedEvent(FormEntryDTO formEntry)
        {
            var formEvent = this.eventFactory.GetFormSavedEvent(formEntry, false);
            EventHub.Raise(formEvent);
        }

        /// <summary>
        /// Raises the form saving event.
        /// </summary>
        /// <param name="formEntry">The form entry.</param>
        /// <returns>Whether processing should continue.</returns>
        protected virtual bool RaiseFormSavingEvent(FormEntryDTO formEntry)
        {
            var formEvent = this.eventFactory.GetFormSavingEvent(formEntry);
            return !this.IsEventCancelled(formEvent);
        }

        /// <summary>
        /// Raises the form validating event.
        /// </summary>
        /// <param name="formEntry">The form entry.</param>
        /// <returns>Whether validation succeeded.</returns>
        protected virtual bool RaiseFormValidatingEvent(FormEntryDTO formEntry)
        {
            var formEvent = this.eventFactory.GetFormValidatingEvent(formEntry);
            return !this.ValidationEventFailed(formEvent);
        }

        /// <summary>
        /// Raises the form validating event.
        /// </summary>
        /// <param name="formEntry">The form entry.</param>
        /// <param name="formFields">The form fields.</param>
        /// <param name="collection">The form collection.</param>
        /// <param name="files">The files.</param>
        /// <returns>Whether validation succeeded.</returns>
        protected virtual bool RaiseFormValidatingEvent(FormEntryDTO formEntry, Dictionary<string, IFormFieldController<IFormFieldModel>> formFields, FormCollection collection, HttpFileCollectionBase files)
        {
            var postedData = this.GetPostedData(collection, files, formFields);
            var formEvent = this.eventFactory.GetFormValidatingEvent(formEntry, postedData);
            return !this.ValidationEventFailed(formEvent);
        }

        private FormPostedData GetPostedData(FormCollection collection, HttpFileCollectionBase files, Dictionary<string, IFormFieldController<IFormFieldModel>> currentFormFields)
        {
            FormPostedData postedData = new FormPostedData();
            var formFields = currentFormFields.Values.Select(p => new { FieldName = this.FormFieldName(p) })
                .Where(field => !string.IsNullOrEmpty(field.FieldName));
            var postedFiles = new Dictionary<string, List<FormHttpPostedFile>>();
            if (files != null)
            {
                for (int i = 0; i < files.AllKeys.Length; i++)
                {
                    if (formFields.FirstOrDefault(p => p.FieldName == files.AllKeys[i]) != null)
                    {
                        postedFiles[files.AllKeys[i]] = files.GetMultiple(files.AllKeys[i]).Where(f => !f.FileName.IsNullOrEmpty()).Select(f =>
                            new FormHttpPostedFile()
                            {
                                FileName = f.FileName,
                                ContentLength = f.ContentLength,
                                ContentType = f.ContentType,
                                InputStream = f.InputStream
                            }).ToList();
                    }
                }
            }

            var formData = new Dictionary<string, object>(collection.Count);
            for (int i = 0; i < collection.Count; i++)
            {
                var formField = formFields.FirstOrDefault(p => p.FieldName == collection.Keys[i]);
                if (formField != null)
                {
                    formData.Add(collection.Keys[i], collection[collection.Keys[i]]);
                }
            }

            postedData.FormsData = formData;
            postedData.Files = postedFiles;

            return postedData;
        }

        /// <summary>
        /// Raises the form field validating event.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>Whether validation succeeded.</returns>
        protected virtual bool RaiseFormFieldValidatingEvent(IFormFieldControl control)
        {
            var formEvent = new FormFieldValidatingEvent()
            {
                FormFieldControl = control
            };

            return !this.ValidationEventFailed(formEvent);
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

        /// <summary>
        /// Gets a collection of <see cref="CacheDependencyNotifiedObject"/>.
        ///     The <see cref="CacheDependencyNotifiedObject"/> represents a key for which cached items could be subscribed for
        ///     notification.
        ///     When notified, all cached objects with dependency on the provided keys will expire.
        /// </summary>
        /// <param name="viewModel">View model that will be used for displaying the data.</param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public virtual IList<CacheDependencyKey> GetKeysOfDependentObjects(FormViewModel viewModel)
        {
            if (this.ContentType != null)
            {
                var contentResolvedType = this.ContentType;
                var result = new List<CacheDependencyKey>(1);
                if (viewModel != null && !string.IsNullOrWhiteSpace(viewModel.FormId))
                {
                    result.Add(new CacheDependencyKey { Key = viewModel.FormId, Type = contentResolvedType });
                }

                return result;
            }
            else
            {
                return new List<CacheDependencyKey>(0);
            }
        }

        #endregion

        #region Private methods

        private void SetConnectorSettingsToContext()
        {
            if (this.ConnectorSettings != null)
            {
                SystemManager.CurrentHttpContext.Items[FormModel.ConnectorSettingsName] = this.ConnectorSettings;
            }
        }

        private string FormFieldName(IFormFieldController<IFormFieldModel> field)
        {
            if (field.MetaField != null)
                return field.MetaField.FieldName;
            else
                return null;
        }

        private bool IsEventCancelled(IEvent formEvent)
        {
            try
            {
                EventHub.Raise(formEvent);
            }
            catch (Exception ex)
            {
                if (ex.Lookup<CancelationException>() == null)
                    throw;

                return true;
            }

            return false;
        }

        private bool ValidationEventFailed(IEvent formEvent)
        {
            try
            {
                EventHub.Raise(formEvent, true);
            }
            catch (Exception ex)
            {
                if (ex.Lookup<ValidationException>() == null)
                    throw;

                return true;
            }

            return false;
        }

        private List<string> GetFormRulesValues(List<FormRule> rules, FormRuleAction formRuleAction, List<string> keys)
        {
            List<string> values = new List<string>();

            if (rules == null || keys == null)
            {
                return values;
            }

            foreach (var key in keys.Where(p => !string.IsNullOrWhiteSpace(p)))
            {
                int searchIndex;
                if (int.TryParse(key.Replace(string.Format(CultureInfo.InvariantCulture, FormInputValueFormat, formRuleAction), string.Empty), out searchIndex))
                {
                    var index = 0;
                    foreach (var rule in rules.Where(p => p != null))
                    {
                        var value = string.Empty;

                        foreach (var action in rule.Actions)
                        {
                            if (action.Action == formRuleAction)
                            {
                                if (searchIndex == index)
                                {
                                    value = action.Target;
                                    break;
                                }

                                index++;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            values.Add(value);
                            break;
                        }
                    }
                }
            }

            return values;
        }

        private List<string> SplitCsv(string value)
        {
            var list = new List<string>();
            if (!string.IsNullOrWhiteSpace(value))
            {
                list = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            return list;
        }

        private string GetFormCollectionItemValue(FormCollection collection, string key)
        {
            var value = string.Empty;
            if (collection.Keys.Contains(key) && !string.IsNullOrEmpty(collection[key]))
            {
                value = collection[key];
            }

            return value;
        }

        private string GetEvaledExpression(string resourceValue)
        {
            string resourcePrefix = "$Resources:";

            if (!resourceValue.IsNullOrEmpty() && resourceValue.TrimStart().StartsWith(resourcePrefix) && resourceValue.Contains(','))
            {
                var parts = resourceValue.Trim().Replace(resourcePrefix, string.Empty).Split(',');
                var classId = parts[0].Trim();
                var key = parts[1].Trim();

                string message = Res.Get(classId, key, null, false, false);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    return message;
                }
            }

            return resourceValue;
        }

        private void UpdateCustomConfirmationMode(List<FormRule> rules, FormCollection collection)
        {
            var confirmationMessageInputValue = this.GetFormCollectionItemValue(collection, FormMessageInputName);
            if (!string.IsNullOrWhiteSpace(confirmationMessageInputValue))
            {
                var messages = this.GetFormRulesValues(rules, FormRuleAction.ShowMessage, new List<string> { confirmationMessageInputValue });

                if (messages.Any())
                {
                    this.CustomConfirmationMessage = this.GetEvaledExpression(messages.SingleOrDefault());
                    this.UseCustomConfirmation = true;
                    this.CustomConfirmationMode = CustomConfirmationMode.ShowMessageForSuccess;
                }
            }

            var confirmationPageIdValue = this.GetFormCollectionItemValue(collection, FormRedirectPageInputName);
            Guid confirmationPageId;
            if (Guid.TryParse(confirmationPageIdValue, out confirmationPageId) && confirmationPageId != Guid.Empty)
            {
                this.CustomConfirmationPageId = confirmationPageId;
                this.UseCustomConfirmation = true;
                this.CustomConfirmationMode = CustomConfirmationMode.RedirectToAPage;
            }
        }

        private IEnumerable<string> GetSendNotificationEmails(List<FormRule> rules, FormCollection collection)
        {
            IEnumerable<string> value = Enumerable.Empty<string>();

            var notificationsEmailsInputValue = this.GetFormCollectionItemValue(collection, FormNotificationEmailsInputName);
            if (!string.IsNullOrWhiteSpace(notificationsEmailsInputValue))
            {
                var notificationEmailsKeys = notificationsEmailsInputValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var formRulesValues = this.GetFormRulesValues(rules, FormRuleAction.SendNotification, notificationEmailsKeys);
                value =  string.Join(",", formRulesValues).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Distinct();
            }

            return value;
        }

        private bool CanSaveField(List<string> hiddenfields, List<string> skippedFields, string fieldControlId, string rules, bool initiallyHidden)
        {
            bool hasRules = !string.IsNullOrWhiteSpace(rules);
            bool inHiddenFields = hiddenfields.Contains(fieldControlId) && (initiallyHidden || hasRules);
            bool inSkippedFields = skippedFields.Contains(fieldControlId) && hasRules;

            var canSave = !inHiddenFields && !inSkippedFields;
            return canSave;
        }

        private string GetFormRulesViewModel(FormDescription form)
        {
            if (string.IsNullOrWhiteSpace(form.Rules))
            {
                return form.Rules;
            }

            var actionIndexList = this.FormRuleActionsToEncrypt.ToDictionary(p => p, p => 0);
            var rules = JToken.Parse(form.Rules) as JArray;
            foreach (var rule in rules)
            {
                foreach (var action in rule["Actions"] as JArray)
                {
                    var ruleAction = action["Action"].ToObject<FormRuleAction>();
                    if (this.FormRuleActionsToEncrypt.Contains(ruleAction))
                    {
                        action["Target"] = string.Concat(string.Format(CultureInfo.InvariantCulture, FormInputValueFormat, ruleAction), actionIndexList[ruleAction]);
                        actionIndexList[ruleAction]++;
                    }
                }
            }

            return JsonConvert.SerializeObject(rules);
        }

        private void InvalidateCaptchas()
        {
            if (this.captchaController != null)
            {
                if (this.captchaController.Model.GetViewModel(this.captchaController.Model.Value) is CaptchaViewModel captchaViewModel)
                {
                    var key = HttpContext.Current.Request[captchaViewModel.CaptchaKeyFormKey];
                    var keys = key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    ITemporaryStorage tempStorage = ObjectFactory.Resolve<ITemporaryStorage>();
                    foreach (var item in keys)
                    {
                        tempStorage.Remove(item);
                    }
                }
            }
        }

        #endregion

        #region Private fields

        private string cssClass;
        private string customConfirmationMessage;
        private readonly FormEventsFactory eventFactory;
        internal const string FormIdName = "FormId";
        private const string ConnectorSettingsName = "ConnectorSettings";

        private const string FormSkipFieldsInputName = "sf_FormSkipFields";
        private const string FormHiddenFieldsInputName = "sf_FormHiddenFields";
        private const string FormMessageInputName = "sf_FormMessage";
        private const string FormRedirectPageInputName = "sf_FormRedirectPage";
        private const string FormNotificationEmailsInputName = "sf_FormNotificationEmails";

        private const string FormInputValueFormat = "sf_{0}_";

        private Controllers.CaptchaController captchaController;

        private List<FormRuleAction> FormRuleActionsToEncrypt = new List<FormRuleAction>() { FormRuleAction.ShowMessage, FormRuleAction.SendNotification };

        #endregion
    }
}