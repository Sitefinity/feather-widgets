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
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Forms.Configuration;

namespace Telerik.Sitefinity.Frontend.Forms
{
    internal class FormsDefinitionsExtender : IControlDefinitionExtender
    {
        public void ExtendDefinition(IContentViewControlDefinition contentViewControlDefinition)
        {
            var isHidden = SystemManager.GetModule("Feather") == null;

            this.ExtendBackendDefinition(contentViewControlDefinition, FormsBackendInsertViewName, FieldDisplayMode.Write, isHidden);
            this.ExtendBackendDefinition(contentViewControlDefinition, FormsBackendEditViewName, FieldDisplayMode.Read, isHidden);
            this.ExtendBackendDefinition(contentViewControlDefinition, FormsBackendDuplicateViewName, FieldDisplayMode.Read, isHidden);
        }

        private void ExtendBackendDefinition(IContentViewControlDefinition contentViewControlDefinition, string backendViewName, FieldDisplayMode displayMode, bool isHidden)
        {
            var backendEditViewDefinition = contentViewControlDefinition.Views.FirstOrDefault(v => v.ViewName == backendViewName) as IDetailFormViewDefinition;

            if (backendEditViewDefinition != null)
            {
                var advancedSection = backendEditViewDefinition.Sections.FirstOrDefault(s => s.Name == FormsDefinitionsExtender.AdvancedSectionName);
                if (advancedSection != null)
                {
                    var fieldDefinition = this.BuildFrameworkChoiceFieldDefinition(displayMode, isHidden);
                    ((IList<IFieldDefinition>)advancedSection.Fields).Add(fieldDefinition);
                }
            }
        }

        private ChoiceFieldDefinition BuildFrameworkChoiceFieldDefinition(FieldDisplayMode displayMode, bool isHidden)
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
                FieldType = typeof(ChoiceField),
                Hidden = isHidden
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

        private const string AdvancedSectionName = "MarketoConnectorSection";
        private const string FormsBackendInsertViewName = "FormsBackendInsert";
        private const string FormsBackendEditViewName = "FormsBackendEdit";
        private const string FormsBackendDuplicateViewName = "FormsBackendDuplicate";
    }
}