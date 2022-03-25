using System.Collections.Generic;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms
{
    internal class FormsDefinitionsExtender : IControlDefinitionExtender
    {
        public void ExtendDefinition(IContentViewControlDefinition contentViewControlDefinition)
        {
            this.ExtendBackendDefinition(contentViewControlDefinition, FormsDefinitionExtenderHelper.FormsBackendInsertViewName, FieldDisplayMode.Write);
            this.ExtendBackendDefinition(contentViewControlDefinition, FormsDefinitionExtenderHelper.FormsBackendEditViewName, FieldDisplayMode.Read);
            this.ExtendBackendDefinition(contentViewControlDefinition, FormsDefinitionExtenderHelper.FormsBackendDuplicateViewName, FieldDisplayMode.Read);
        }

        private void ExtendBackendDefinition(IContentViewControlDefinition contentViewControlDefinition, string backendViewName, FieldDisplayMode displayMode)
        {
            var choices = new List<ChoiceDefinition>();
            choices.Add(new ChoiceDefinition()
            {
                Text = "MVCOnly",
                ResourceClassId = typeof(FormsResources).Name,
                Value = ((int)FormFramework.Mvc).ToString()
            });

            choices.Add(new ChoiceDefinition()
            {
                Text = "WebFormsOnly",
                ResourceClassId = typeof(FormsResources).Name,
                Value = ((int)FormFramework.WebForms).ToString()
            });

            FormsDefinitionExtenderHelper.ExtendFormsFrameworkFieldDefinition(contentViewControlDefinition, backendViewName, displayMode, choices);
        }
    }
}