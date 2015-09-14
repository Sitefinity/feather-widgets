using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.BackendConfigurators;
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
                            var newControl = (IFormFieldControl)Activator.CreateInstance(pair.Value.BackendFieldType);

                            newControl.MetaField = formField.MetaField;

                            if (newControl is FieldControl)
                            {
                                ((FieldControl)newControl).Title = formField.MetaField.Title;
                                var fieldController = formField as IFormFieldController<IFormFieldModel>;
                                if (fieldController != null)
                                {
                                    ((FieldControl)newControl).ValidatorDefinition = fieldController.Model.ValidatorDefinition;
                                    if (pair.Value.FieldConfigurator != null)
                                    {
                                        pair.Value.FieldConfigurator.FormId = formId;
                                        pair.Value.FieldConfigurator.Configure((FieldControl)newControl, fieldController);
                                    }
                                }
                            }

                            return (Control)newControl;
                        }
                    }
                }
            }

            return formControl;
        }

        private static readonly Type FormFileUploadType = TypeResolutionService.ResolveType("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFileUpload");
        private static readonly Type[] ignoredTypes = new Type[] { typeof(SubmitButtonController), typeof(RecaptchaController) };

        private static readonly Dictionary<Type, FieldConfiguration> fieldMap = new Dictionary<Type, FieldConfiguration>(5)
        {
            { typeof(CheckboxesFieldController), new FieldConfiguration(typeof(FormCheckboxes), new CheckboxesFieldConfigurator()) },
            { typeof(DropdownListFieldController), new FieldConfiguration(typeof(FormDropDownList), new DropdownListFieldConfigurator()) },
            { typeof(MultipleChoiceFieldController), new FieldConfiguration(typeof(FormMultipleChoice), new MultipleChoiceFieldConfigurator()) },
            { typeof(ParagraphTextFieldController), new FieldConfiguration(typeof(FormParagraphTextBox), null) },
            { typeof(TextFieldController), new FieldConfiguration(typeof(FormTextBox), null) },           
            { typeof(FileFieldController), new FieldConfiguration(BackendFieldFallbackConfigurator.FormFileUploadType, new FileFieldConfigurator()) }
        };
    }
}
