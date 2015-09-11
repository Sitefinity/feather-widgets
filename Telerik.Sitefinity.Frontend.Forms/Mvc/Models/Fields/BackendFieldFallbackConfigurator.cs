using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
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
        /// <returns>The configured form control.</returns>
        public Control ConfigureFormControl(Control formControl)
        {
            if (formControl != null)
            {
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
                            if (newControl is FieldControl)
                            {
                                ((FieldControl)newControl).Title = formField.MetaField.Title;
                            }

                            return (Control)newControl;
                        }
                    }
                }
            }

            return formControl;
        }

        private static readonly Type[] ignoredTypes = new Type[] { typeof(SubmitButtonController), typeof(RecaptchaController) };

        private static readonly Dictionary<Type, Type> fieldMap = new Dictionary<Type, Type>(5)
        {
            { typeof(CheckboxesFieldController), typeof(FormCheckboxes) },
            { typeof(DropdownListFieldController), typeof(FormDropDownList) },
            { typeof(MultipleChoiceFieldController), typeof(FormMultipleChoice) },
            { typeof(ParagraphTextFieldController), typeof(FormParagraphTextBox) },
            { typeof(TextFieldController), typeof(FormTextBox) }
        };
    }
}
