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
        this.ExtendBackendInsertDefinition(contentViewControlDefinition);
        this.ExtendBackendEditDefinition(contentViewControlDefinition);
    }

    private void ExtendBackendInsertDefinition(IContentViewControlDefinition contentViewControlDefinition)
    {
        var backendInsertViewDefinition = contentViewControlDefinition.Views.FirstOrDefault(v => v.ViewName == "FormsBackendInsert") as IDetailFormViewDefinition;

        if (backendInsertViewDefinition != null)
        {
            var advancedSection = backendInsertViewDefinition.Sections.FirstOrDefault(s => s.Name == FormsDefinitionsExtender.AdvancedSectionName);
            if (advancedSection != null)
            {
                var frameworkChoiceFieldDefinition = this.BuildFrameworkChoiceFieldDefinition();
                ((List<IFieldDefinition>)advancedSection.Fields).Add(frameworkChoiceFieldDefinition);
            }
        }
    }
    
    private void ExtendBackendEditDefinition(IContentViewControlDefinition contentViewControlDefinition)
    {
        var backendEditViewDefinition = contentViewControlDefinition.Views.FirstOrDefault(v => v.ViewName == "FormsBackendEdit") as IDetailFormViewDefinition;

        if (backendEditViewDefinition != null)
        {
            var advancedSection = backendEditViewDefinition.Sections.FirstOrDefault(s => s.Name == FormsDefinitionsExtender.AdvancedSectionName);
            if (advancedSection != null)
            {
                var frameworkTextFieldDefinition = this.BuildFrameworkTextFieldDefinition();
                ((List<IFieldDefinition>)advancedSection.Fields).Add(frameworkTextFieldDefinition);
            }
        }
    }
    
    private ChoiceFieldDefinition BuildFrameworkChoiceFieldDefinition()
    {
        var frameworkField = new ChoiceFieldDefinition()
        {
            ID = "FormFramework",
            DataFieldName = "Framework",
            Title = "WebFrameworkTitle",
            Description = "WebFrameworkDescription",
            DisplayMode = FieldDisplayMode.Write,
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

    private TextFieldDefinition BuildFrameworkTextFieldDefinition()
    {
        return new TextFieldDefinition()
        {
            ID = "FormFramework",
            DataFieldName = "Framework",
            Title = "WebFrameworkTitle",
            Description = "WebFrameworkDescription",
            DisplayMode = FieldDisplayMode.Read,
            WrapperTag = HtmlTextWriterTag.Li,
            ResourceClassId = typeof(PageResources).Name,
            CssClass = "sfFormSeparator",
            FieldType = typeof(TextField)
        };
    }

    private const string AdvancedSectionName = "MarketoConnectorSection";
}