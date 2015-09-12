using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields
{
    /// <summary>
    /// Prepares Form controls for display in the backend.
    /// </summary>
    internal class BackendFieldFallbackConfigurator : IFormFieldBackendConfigurator
    {
        /// <summary>
        /// Prepares a Form control for display in the backend.
        /// </summary>
        /// <param name="formControl">The form control.</param>
        /// <param name="formId">Id of the form that hosts the field.</param>
        /// <returns>The configured form control.</returns>
        public Control ConfigureFormControl(Control formControl, Guid formId)
        {
            if (formControl != null)
            {
                var actionInvoker = ObjectFactory.Resolve<IControllerActionInvoker>() as Telerik.Sitefinity.Mvc.ControllerActionInvoker;
                if (actionInvoker != null)
                    actionInvoker.DeserializeControllerProperties((Telerik.Sitefinity.Mvc.Proxy.MvcProxyBase)formControl);

                var behaviorObject = ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObject(formControl);
                var behaviorType = behaviorObject.GetType();

                for (var i = 0; i < BackendFieldFallbackConfigurator.ignoredTypes.Length; i++)
                {
                    var ignoredType = BackendFieldFallbackConfigurator.ignoredTypes[i];
                    if (ignoredType.IsAssignableFrom(behaviorType))
                        return null;
                }

                var formField = behaviorObject as IFormFieldControl;
                if (formField != null)
                {
                    foreach (var pair in BackendFieldFallbackConfigurator.fieldMap)
                    {
                        if (pair.Key.IsAssignableFrom(behaviorType))
                        {
                            var newControl = (IFormFieldControl)Activator.CreateInstance(pair.Value);

                            newControl.MetaField = formField.MetaField;
                            this.ConfigureFieldControl(newControl as FieldControl, formField, formId);

                            return (Control)newControl;
                        }
                    }
                }
            }

            return formControl;
        }

        private void ConfigureFieldControl(FieldControl fieldControl, IFormFieldControl mvcField, Guid formId)
        {
            if (fieldControl == null)
                return;

            fieldControl.Title = mvcField.MetaField.Title;

            if (fieldControl.GetType() == BackendFieldFallbackConfigurator.FormFileUploadType)
                this.ConfigureFileUpload(fieldControl, formId);
        }

        private void ConfigureFileUpload(FieldControl fieldControl, Guid formId)
        {
            fieldControl.GetType().GetProperty("FormId").SetValue(fieldControl, formId);
            fieldControl.GetType().GetProperty("FormsProviderName").SetValue(fieldControl, FormsManager.GetDefaultProviderName());
        }

        private static readonly Type FormFileUploadType = TypeResolutionService.ResolveType("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFileUpload");
        private static readonly Type[] ignoredTypes = new Type[] { typeof(SubmitButtonController), typeof(RecaptchaController) };

        private static readonly Dictionary<Type, Type> fieldMap = new Dictionary<Type, Type>(6)
        {
            { typeof(CheckboxesFieldController), typeof(FormCheckboxes) },
            { typeof(DropdownListFieldController), typeof(FormDropDownList) },
            { typeof(MultipleChoiceFieldController), typeof(FormMultipleChoice) },
            { typeof(ParagraphTextFieldController), typeof(FormParagraphTextBox) },
            { typeof(TextFieldController), typeof(FormTextBox) },
            { typeof(FileFieldController), BackendFieldFallbackConfigurator.FormFileUploadType }
        };
    }
}
