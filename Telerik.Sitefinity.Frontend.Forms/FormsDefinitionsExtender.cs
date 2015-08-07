using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

internal class FormsDefinitionsExtender : IControlDefinitionExtender
{
    public void ExtendDefinition(IContentViewControlDefinition contentViewControlDefinition)
    {
        this.ExtendBackendDefinition(contentViewControlDefinition, "FormsBackendInsert", FieldDisplayMode.Write);
        this.ExtendBackendDefinition(contentViewControlDefinition, "FormsBackendEdit", FieldDisplayMode.Read);
        this.ExtendBackendDefinition(contentViewControlDefinition, "FormsBackendDuplicate", FieldDisplayMode.Read);
    }

    private void ExtendBackendDefinition(IContentViewControlDefinition contentViewControlDefinition, string backendViewName, FieldDisplayMode displayMode)
    {
        var backendEditViewDefinition = contentViewControlDefinition.Views.FirstOrDefault(v => v.ViewName == backendViewName) as IDetailFormViewDefinition;

        if (backendEditViewDefinition != null)
        {
            var advancedSection = backendEditViewDefinition.Sections.FirstOrDefault(s => s.Name == "MarketoConnectorSection");
            if (advancedSection != null)
            {
                var frameworkChoiceFieldDefinition = this.BuildFrameworkChoiceFieldDefinition(displayMode);
                ((List<IFieldDefinition>)advancedSection.Fields).Add(frameworkChoiceFieldDefinition);
            }
        }
    }

    private ChoiceFieldDefinition BuildFrameworkChoiceFieldDefinition(FieldDisplayMode displayMode)
    {
        var frameworkField = new ChoiceFieldDefinition()
        {
            ID = "FormFramework",
            DataFieldName = "Framework",
            Title = "WebFrameworkTitle",
            Description = "WebFrameworkDescription",
            DisplayMode = displayMode,
            WrapperTag = HtmlTextWriterTag.Li,
            MutuallyExclusive = true,
            ResourceClassId = typeof(PageResources).Name,
            RenderChoiceAs = RenderChoicesAs.RadioButtons,
            CssClass = "sfFormSeparator",
            FieldType = typeof(ChoiceField)
        };

        frameworkField.Choices.Add(new ChoiceDefinition()
        {
            Text = "MVCOnly",
            ResourceClassId = typeof(PageResources).Name,
            Value = ((int)FormFramework.Mvc).ToString()
        });

        frameworkField.Choices.Add(new ChoiceDefinition()
        {
            Text = "WebFormsOnly",
            ResourceClassId = typeof(PageResources).Name,
            Value = ((int)FormFramework.WebForms).ToString()
        });

        return frameworkField;
    }
}