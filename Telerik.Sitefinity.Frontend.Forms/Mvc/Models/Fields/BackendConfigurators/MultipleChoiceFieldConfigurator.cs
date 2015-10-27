using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
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
        public Guid FormId
        {
            get;
            set;
        }

        /// <inheritDocs/>
        public void Configure(FieldControl backendControl, IFormFieldController<IFormFieldModel> formFieldController)
        {
            var multipleChoiceFieldModel = (IMultipleChoiceFieldModel)formFieldController.Model;
            var choices = multipleChoiceFieldModel.DeserializeChoices();

            var choiceFieldControl = (FormMultipleChoice)backendControl;
            choiceFieldControl.Choices.Clear();
            foreach (var choice in choices)
            {
                choiceFieldControl.Choices.Add(new Web.UI.Fields.ChoiceItem() { Text = choice, Value = choice });
            }
        }
    }
}
