using System;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.CheckboxesField;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.BackendConfigurators
{
    /// <summary>
    /// Configures specifics for checkboxes field in the backend.
    /// </summary>
    internal class CheckboxesFieldConfigurator : IFieldConfigurator
    {
        /// <inheritDocs/>
        public Guid FormId
        {
            get;
            set;
        }

        /// <inheritDocs/>
        public void Configure(FieldControl backendControl, IFormFieldController<IFormFieldModel> formFieldController)
        {
            var checkboxesFieldModel = (ICheckboxesFieldModel)formFieldController.Model;
            var choices = checkboxesFieldModel.DeserializeChoices();

            var checkboxesControl = (FormCheckboxes)backendControl;
            checkboxesControl.Choices.Clear();
            foreach (var choice in choices)
            {
                checkboxesControl.Choices.Add(new Web.UI.Fields.ChoiceItem() { Text = choice, Value = choice });
            }
        }
    }
}
