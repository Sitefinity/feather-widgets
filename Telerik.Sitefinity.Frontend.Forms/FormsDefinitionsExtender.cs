using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.Extenders.Definitions;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

internal class FormsDefinitionsExtender : IControlDefinitionExtender
{
    public const string BackendInsertViewName = "FormsBackendInsert";

    public void ExtendDefinition(IContentViewControlDefinition contentViewControlDefinition)
    {
        var backendInsertViewDefinition = contentViewControlDefinition.Views.FirstOrDefault(v => v.ViewName == BackendInsertViewName) as IDetailFormViewDefinition;

        if (backendInsertViewDefinition != null)
        {
            var frameworkSectionDefinition = this.BuildContentViewSectionDefinition();
            var frameworkSectionChoiceFieldDefinition = this.BuildFrameworkChoiceFieldDefinition();
            frameworkSectionDefinition.Fields.Add(frameworkSectionChoiceFieldDefinition);

            ((IList<IContentViewSectionDefinition>)backendInsertViewDefinition.Sections).Add(frameworkSectionDefinition);
        }
    }

    private ContentViewSectionDefinition BuildContentViewSectionDefinition()
    {
        var frameworkSection = new ContentViewSectionDefinition();

        frameworkSection.ExpandableDefinition = new ExpandableControlDefinition();
        frameworkSection.ExpandableDefinition.Expanded = false;

        frameworkSection.CssClass = "sfExpandableForm";
        frameworkSection.Title = "Settings";

        return frameworkSection;
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