using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.CheckboxesField;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.BackendConfigurators
{
    /// <summary>
    /// Configures specifics for checkboxes field in the backend.
    /// </summary>
    internal class CheckboxesFieldConfigurator: IFieldConfigurator
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
            var checkboxesFieldModel = formFieldController.Model as ICheckboxesFieldModel;
            var choices = checkboxesFieldModel.DeserializeChoices();
            (backendControl as FormCheckboxes).Choices.Clear();
            foreach (var choice in choices)
            {
                (backendControl as FormCheckboxes).Choices.Add(new Web.UI.Fields.ChoiceItem() { Text = choice, Value = choice });
            }
        }
    }
}
