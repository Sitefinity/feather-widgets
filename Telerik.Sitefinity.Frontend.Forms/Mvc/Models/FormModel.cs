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
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Events;
using Telerik.Sitefinity.Modules.Forms.Web;
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
        /// The ajax submit URL.
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
                ViewMode = this.ViewMode,
                CssClass = this.CssClass,
                UseAjaxSubmit = this.UseAjaxSubmit,
                FormId = this.FormId.ToString("D")
            };

            if (this.FormData != null && this.AllowRenderForm())
            {
                if (viewModel.UseAjaxSubmit)
                {
                    string baseUrl;
                    if (this.AjaxSubmitUrl.IsNullOrEmpty())
                    {
                        var currentNode = SiteMapBase.GetCurrentNode();
                        baseUrl = currentNode != null ? currentNode.Url + "/AjaxSubmit" : "";
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
            var form = manager.GetForm(this.FormId);

            var formEntry = new FormEntryDTO(form);
            var formSubmition = new FormsSubmitionHelper();

            if (!this.ValidateFormSubmissionRestrictions(formSubmition, formEntry))
                return SubmitStatus.RestrictionViolation;

            if (!this.RaiseFormValidatingEvent(formEntry) || !this.IsValidForm(form, collection, files, manager))
                return SubmitStatus.InvalidEntry;

            var formFields = new HashSet<string>(form.Controls.Select(this.FormFieldName).Where((f) => !string.IsNullOrEmpty(f)));

            var postedFiles = new Dictionary<string, List<FormHttpPostedFile>>();
            if (files != null)
            {
                for (int i = 0; i < files.AllKeys.Length; i++)
                {
                    if (formFields.Contains(files.AllKeys[i]))
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
                if (formFields.Contains(collection.Keys[i]))
                {
                    formData.Add(collection.Keys[i], collection[collection.Keys[i]]);
                }
            }

            formEntry.PostedData.FormsData = formData;
            formEntry.PostedData.Files = postedFiles;

            if (this.RaiseFormSavingEvent(formEntry))
            {
                formSubmition.Save(formEntry);
                this.RaiseFormSavedEvent(formEntry);

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
        protected virtual bool IsValidForm(FormDescription form, FormCollection collection, HttpFileCollectionBase files, FormsManager manager)
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
                var formField = controlBehaviorObject as IFormFieldController<IFormFieldModel>;
                
                if (formField != null)
                {
                    if (!this.RaiseFormFieldValidatingEvent(formField))
                        return false;

                    IList<HttpPostedFileBase> multipleFiles = files != null ? files.GetMultiple(formField.MetaField.FieldName) : null;
                    object fieldValue;

                    if (multipleFiles != null && multipleFiles.Count() > 0)
                    {
                        fieldValue = multipleFiles;
                    }
                    else if (collection.Keys.Contains(formField.MetaField.FieldName))
                    {
                        collection[formField.MetaField.FieldName] = collection[formField.MetaField.FieldName] ?? string.Empty;
                        fieldValue = collection[formField.MetaField.FieldName];
                    }
                    else
                    {
                        fieldValue = null;
                    }

                    if (!formField.Model.IsValid(fieldValue))
                        return false;
                }
                else
                {
                    var formElement = (IFormElementController<IFormElementModel>)controlBehaviorObject;
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

        /// <summary>
        /// Raises the form saved event.
        /// </summary>
        /// <param name="formEntry">The form entry.</param>
        protected virtual void RaiseFormSavedEvent(FormEntryDTO formEntry)
        {
            var formEvent = this.eventFactory.GetFormSavedEvent(formEntry);
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

        #endregion

        #region Private fields

        private string cssClass;
        private string customConfirmationMessage;
        private readonly FormEventsFactory eventFactory;

        #endregion
    }
}