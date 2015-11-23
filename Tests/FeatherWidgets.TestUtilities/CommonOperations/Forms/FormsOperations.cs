using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Mvc.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.Web.UI;

namespace FeatherWidgets.TestUtilities.CommonOperations.Forms
{
    /// <summary>
    /// This class provides access to forms common server operations
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
    public class FormsOperations
    {
        /// <summary>
        /// Adds the form control to page.
        /// </summary>
        /// <param name="pageId">The page identifier.</param>
        /// <param name="formId">The form identifier.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestIntegration.Data.Content.PageContentGenerator.AddControlToPage(System.Guid,System.Web.UI.Control,System.String,System.String,System.Action<Telerik.Sitefinity.Pages.Model.PageDraftControl>)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "TestForm")]
        public void AddFormControlToPage(Guid pageId, Guid formId, string formName = "TestForm", string placeholder = "Contentplaceholder1")
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(FormController).FullName;
            var controller = new FormController();

            controller.Model.FormId = formId;
            controller.Model.ViewMode = FormViewMode.Write;

            mvcProxy.Settings = new ControllerSettings(controller);

            PageContentGenerator.AddControlToPage(pageId, mvcProxy, formName, placeholder);
        }

        /// <summary>
        /// Creates the form with widget.
        /// </summary>
        /// <param name="widget">The widget.</param>
        /// <returns></returns>
        public Guid CreateFormWithWidget(Control widget)
        {
            var formId = Guid.NewGuid();

            string formSuccessMessage = "Test form success message";

            var formControls = new List<Control>();
            formControls.Add(widget);

            this.CreateForm(formId, "form_" + formId.ToString("N"), formId.ToString("N"), formSuccessMessage, formControls);

            SystemManager.ClearCurrentTransactions();
            SystemManager.RestartApplication(false);
            System.Threading.Thread.Sleep(1000);

            return formId;
        }

        public Guid CreateFormWithWidgets(IList<Control> widgets)
        {
            return this.CreateFormWithWidgets(widgets, null);
        }

        public Guid CreateFormWithWidgets(IEnumerable<Control> widgets, string formTitle = null)
        {
            var formId = Guid.NewGuid();

            string formSuccessMessage = "Test form success message";

            var formControls = new List<Control>();
            foreach (var widget in widgets)
            {
                formControls.Add(widget);
            }

            var formName = "form_" + formId.ToString("N");

            if (formTitle == null)
                formTitle = formId.ToString("N");

            this.CreateForm(formId, formName, formTitle, formSuccessMessage, formControls);

            SystemManager.ClearCurrentTransactions();
            SystemManager.RestartApplication(false);
            System.Threading.Thread.Sleep(1000);

            return formId;
        }

        public Guid CreateFormWithWidgets(IEnumerable<FormFieldType> widgets, string formTitle = null)
        {
            var controls = this.CreateFormFieldsFromFieldTypes(widgets);

            return this.CreateFormWithWidgets(controls, formTitle);
        }

        public Guid CreateFormWithWidgets(IList<Control> formHeaderControls, IList<Control> formBodyControls, IList<Control> formFooterControls)
        {
            return this.CreateFormWithWidgets(formHeaderControls, formBodyControls, formFooterControls, null);
        }

        public Guid CreateFormWithWidgets(IList<Control> formHeaderControls, IList<Control> formBodyControls, IList<Control> formFooterControls, string formTitle)
        {
            var formId = Guid.NewGuid();

            string formSuccessMessage = "Test form success message";

            if (formTitle == null)
                formTitle = formId.ToString("N");

            this.CreateFormWithHeaderAndFooter(formId, "form_" + formId.ToString("N"), formTitle, formSuccessMessage, formHeaderControls, formBodyControls, formFooterControls);

            SystemManager.ClearCurrentTransactions();
            SystemManager.RestartApplication(false);
            System.Threading.Thread.Sleep(1000);

            return formId;
        }

        public Guid CreateFormWithWidgets(IEnumerable<FormFieldType> headerWidgets, IEnumerable<FormFieldType> bodyWidgets, IEnumerable<FormFieldType> footerWidgets)
        {
            return this.CreateFormWithWidgets(headerWidgets, bodyWidgets, footerWidgets, null);
        }

        public Guid CreateFormWithWidgets(IEnumerable<FormFieldType> headerWidgets, IEnumerable<FormFieldType> bodyWidgets, IEnumerable<FormFieldType> footerWidgets, string formTitle)
        {
            var headerControls = this.CreateFormFieldsFromFieldTypes(headerWidgets);
            var bodyControls = this.CreateFormFieldsFromFieldTypes(bodyWidgets);
            var footerControls = this.CreateFormFieldsFromFieldTypes(footerWidgets);

            return this.CreateFormWithWidgets(headerControls, bodyControls, footerControls, formTitle);
        }

        public IList<Control> CreateFormFieldsFromFieldTypes(IEnumerable<FormFieldType> widgets)
        {
            var controls = new List<Control>();

            foreach (var widgetType in widgets)
            {
                var control = new MvcControllerProxy();

                switch (widgetType)
                {
                    case FormFieldType.Captcha:
                        control.ControllerName = typeof(CaptchaController).FullName;
                        control.Settings = new ControllerSettings(new CaptchaController());
                        break;
                    case FormFieldType.CheckboxesField:
                        control.ControllerName = typeof(CheckboxesFieldController).FullName;
                        control.Settings = new ControllerSettings(new CheckboxesFieldController());
                        break;
                    case FormFieldType.DropdownListField:
                        control.ControllerName = typeof(DropdownListFieldController).FullName;
                        control.Settings = new ControllerSettings(new DropdownListFieldController());
                        break;
                    case FormFieldType.FileField:
                        control.ControllerName = typeof(FileFieldController).FullName;
                        control.Settings = new ControllerSettings(new FileFieldController());
                        break;
                    case FormFieldType.MultipleChoiceField:
                        control.ControllerName = typeof(MultipleChoiceFieldController).FullName;
                        control.Settings = new ControllerSettings(new MultipleChoiceFieldController());
                        break;
                    case FormFieldType.ParagraphTextField:
                        control.ControllerName = typeof(ParagraphTextFieldController).FullName;
                        control.Settings = new ControllerSettings(new ParagraphTextFieldController());
                        break;
                    case FormFieldType.SectionHeader:
                        control.ControllerName = typeof(SectionHeaderController).FullName;
                        control.Settings = new ControllerSettings(new SectionHeaderController());
                        break;
                    case FormFieldType.SubmitButton:
                        control.ControllerName = typeof(SubmitButtonController).FullName;
                        control.Settings = new ControllerSettings(new SubmitButtonController());
                        break;
                    case FormFieldType.TextField:
                        control.ControllerName = typeof(TextFieldController).FullName;
                        control.Settings = new ControllerSettings(new TextFieldController());
                        break;
                    case FormFieldType.PageBreak:
                        control.ControllerName = typeof(PageBreakController).FullName;
                        control.Settings = new ControllerSettings(new PageBreakController());
                        break;
                    case FormFieldType.NavigationField:
                        control.ControllerName = typeof(NavigationFieldController).FullName;
                        control.Settings = new ControllerSettings(new NavigationFieldController());
                        break;
                    default:
                        break;
                }

                controls.Add(control);
            }

            return controls;
        }

        /// <summary>
        /// Adds the form widget.
        /// </summary>
        /// <param name="formId">The form identifier.</param>
        /// <param name="widget">The widget.</param>
        public void AddFormWidget(Guid formId, Control widget)
        {
            var formManager = FormsManager.GetManager();
            var form = formManager.GetForms().FirstOrDefault(f => f.Id == formId);
            var draft = formManager.EditForm(form.Id);
            var master = formManager.Lifecycle.CheckOut(draft);

            widget.ID = string.Format(CultureInfo.InvariantCulture, form.Name + "_C" + Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture));
            var formControl = formManager.CreateControl<FormDraftControl>(widget, "Body");

            formControl.IsLayoutControl = widget is LayoutControl;
            formControl.SiblingId = FormsOperations.GetLastControlInPlaceHolder(form, "Body");
            formControl.Caption = widget.GetType().Name;

            master.Controls.Add(formControl);

            master = formManager.Lifecycle.CheckIn(master);
            formManager.Lifecycle.Publish(master);

            formManager.SaveChanges();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public string GetFirstFieldName(FormsManager formManager, FormDescription form)
        {
            var textFieldControlData = form.Controls.Where(c => c.PlaceHolder == "Body" && c.IsLayoutControl == false).FirstOrDefault();
            var mvcfieldProxy = formManager.LoadControl(textFieldControlData) as MvcWidgetProxy;
            var fieldControl = mvcfieldProxy.Controller as IFormFieldControl;
            var fieldName = fieldControl.MetaField.FieldName;

            return fieldName;
        }

        /// <summary>
        /// Submits the field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="submittedValue">The submitted value.</param>
        /// <param name="pageManager">The page manager.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <returns></returns>
        public ActionResult SubmitField(string fieldName, string submittedValue, PageManager pageManager, Guid pageId)
        {
            var pageNode = pageManager.GetPageNode(pageId);
            var pageDataId = pageNode.GetPageData().Id;
            var formCollection = new FormCollection();
            formCollection.Add(fieldName, submittedValue);
            var formControllerProxy = pageManager.LoadPageControls<MvcControllerProxy>(pageDataId).Where(contr => contr.Controller.GetType() == typeof(FormController)).FirstOrDefault();
            var formController = formControllerProxy.Controller as FormController;
            formController.ControllerContext = new ControllerContext();

            var pageUrl = pageNode.GetFullUrl();
            var url = Telerik.Sitefinity.Web.UrlPath.ResolveAbsoluteUrl(pageUrl);

            formController.ControllerContext.HttpContext = new HttpContextWrapper(new HttpContext(
                new HttpRequest(string.Empty, url, string.Empty),
                new HttpResponse(new StringWriter(CultureInfo.InvariantCulture))));

            var result = formController.Index(formCollection);

            return result;
        }

        /// <summary>
        /// Creates a new form in Content -> Forms
        /// </summary>
        /// <param name="formId">Form ID</param>
        /// <param name="formName">Form name</param>
        /// <param name="formTitle">Form title</param>
        /// <param name="formSuccessMessage">Success message after the form is submitted</param>
        /// <param name="formControls">Form widgets like text boxes and buttons</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        public void CreateForm(Guid formId, string formName, string formTitle, string formSuccessMessage, IList<Control> formControls)
        {
            FormsManager formManager = FormsManager.GetManager();
            var form = formManager.GetForms().SingleOrDefault(f => f.Id == formId);
            Guid siblingId = Guid.Empty;

            if (form == null)
            {
                form = formManager.CreateForm(formName, formId);
                form.Framework = FormFramework.Mvc;
                form.Title = formTitle;
                form.UrlName = Regex.Replace(form.Name.ToLower(), ArrangementConstants.UrlNameCharsToReplace, ArrangementConstants.UrlNameReplaceString);
                form.SuccessMessage = formSuccessMessage;

                var culture = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
                var draft = formManager.EditForm(form.Id);
                var master = formManager.Lifecycle.CheckOut(draft, culture);

                if (master != null)
                {
                    if (formControls != null && formControls.Any())
                    {
                        int controlsCounter = 0;
                        foreach (var control in formControls)
                        {
                            controlsCounter++;
                            control.ID = string.Format(CultureInfo.InvariantCulture, formName + "_C" + controlsCounter.ToString(CultureInfo.InvariantCulture).PadLeft(3, '0'));
                            var formControl = formManager.CreateControl<FormDraftControl>(control, "Body");

                            formControl.SiblingId = siblingId;
                            formControl.Caption = ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObject(control).GetType().Name;
                            siblingId = formControl.Id;
                            master.Controls.Add(formControl);
                            formControl.SetPersistanceStrategy();
                        }
                    }

                    master = formManager.Lifecycle.CheckIn(master, culture);
                    formManager.Lifecycle.Publish(master, culture);

                    formManager.SaveChanges(true);
                }
            }
        }

        /// <summary>
        /// Creates a new form in Content -> Forms
        /// </summary>
        /// <param name="formId">Form ID</param>
        /// <param name="formName">Form name</param>
        /// <param name="formTitle">Form title</param>
        /// <param name="formSuccessMessage">Success message after the form is submitted</param>
        /// <param name="formControls">Form widgets like text boxes and buttons</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        public void CreateFormWithHeaderAndFooter(Guid formId, string formName, string formTitle, string formSuccessMessage, IList<Control> formHeaderControls, IList<Control> formBodyControls, IList<Control> formFooterControls)
        {
            FormsManager formManager = FormsManager.GetManager();
            var form = formManager.GetForms().SingleOrDefault(f => f.Id == formId);

            if (form == null)
            {
                form = formManager.CreateForm(formName, formId);
                form.Framework = FormFramework.Mvc;
                form.Title = formTitle;
                form.UrlName = Regex.Replace(form.Name.ToLower(), ArrangementConstants.UrlNameCharsToReplace, ArrangementConstants.UrlNameReplaceString);
                form.SuccessMessage = formSuccessMessage;

                var culture = System.Globalization.CultureInfo.CurrentUICulture;
                var draft = formManager.EditForm(form.Id);
                var master = formManager.Lifecycle.CheckOut(draft, culture);

                if (master != null)
                {
                    this.AppendFormControlsToPlaceholder(formName, formBodyControls, formManager, master, "Body");
                    this.AppendFormControlsToPlaceholder(formName, formHeaderControls, formManager, master, "Header");
                    this.AppendFormControlsToPlaceholder(formName, formFooterControls, formManager, master, "Footer");

                    master = formManager.Lifecycle.CheckIn(master, culture);
                    formManager.Lifecycle.Publish(master, culture);

                    formManager.SaveChanges(true);
                }
            }
        }

        private static Guid GetLastControlInPlaceHolder(FormDescription form, string placeHolder)
        {
            var id = Guid.Empty;
            FormControl control;

            var controls = new List<FormControl>(form.Controls.Where(c => c.PlaceHolder == placeHolder));

            while (controls.Count > 0)
            {
                control = controls.Where(c => c.SiblingId == id).SingleOrDefault();
                id = control.Id;

                controls.Remove(control);
            }

            return id;
        }

        private void AppendFormControlsToPlaceholder(string formName, IList<Control> formControls, FormsManager formManager, FormDraft master, string containerId)
        {
            if (formControls != null && formControls.Any())
            {
                Guid siblingId = Guid.Empty;
                int controlsCounter = 0;
                foreach (var control in formControls)
                {
                    controlsCounter++;
                    control.ID = string.Format(CultureInfo.InvariantCulture, formName + "_" + containerId + "_C" + controlsCounter.ToString(CultureInfo.InvariantCulture).PadLeft(3, '0'));
                    var formControl = formManager.CreateControl<FormDraftControl>(control, containerId);

                    formControl.SiblingId = siblingId;
                    formControl.Caption = ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObject(control).GetType().Name;
                    siblingId = formControl.Id;
                    master.Controls.Add(formControl);
                    formControl.SetPersistanceStrategy();
                }
            }
        }
    }
}
