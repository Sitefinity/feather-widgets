using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.MultipleChoiceField;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.BackendConfigurators
{
    /// <summary>
    /// Configures specifics for Multiple Choice field in the backend.
    /// </summary>
    internal class MultipleChoiceFieldConfigurator : IFieldConfigurator
    {
        /// <inheritDocs/>
        public void Configure(FieldControl backendControl, IFormFieldController<IFormFieldModel> formFieldController)
        {
            var multipleChoiceFieldModel = formFieldController.Model as IMultipleChoiceFieldModel;
            var choices = multipleChoiceFieldModel.DeserializeChoices();
            (backendControl as FormMultipleChoice).Choices.Clear();
            foreach (var choice in choices)
            {
                (backendControl as FormCheckboxes).Choices.Add(new Web.UI.Fields.ChoiceItem() { Text = choice, Value = choice });
            }
        }
    }
}
