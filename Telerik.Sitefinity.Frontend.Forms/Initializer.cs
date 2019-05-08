using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// This class is responsible for initialization of the infrastructure related to the Forms MVC functionality.
    /// </summary>
    public static class Initializer
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            ObjectFactory.Container.RegisterType<IFormFieldBackendConfigurator, BackendFieldFallbackConfigurator>(typeof(MvcControllerProxy).FullName);

            EventHub.Unsubscribe<IScriptsRegisteringEvent>(Initializer.RegisteringFormScriptsHandler);
            EventHub.Subscribe<IScriptsRegisteringEvent>(Initializer.RegisteringFormScriptsHandler);

            Bootstrapper.Initialized -= Initializer.Bootstrapper_Initialized;
            Bootstrapper.Initialized += Initializer.Bootstrapper_Initialized;
        }

        /// <summary>
        /// Uninitializes this instance.
        /// </summary>
        public static void Uninitialize()
        {
            EventHub.Unsubscribe<IScriptsRegisteringEvent>(Initializer.RegisteringFormScriptsHandler);
            Bootstrapper.Initialized -= Initializer.Bootstrapper_Initialized;

            Initializer.UnregisterTemplatableControl();
        }

        #region Private Methods

        private static void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapped")
            {
                VirtualPathManager.AddVirtualFileResolver<FormsVirtualRazorResolver>(FormsVirtualRazorResolver.Path + "*", "MvcFormsResolver");

                Initializer.UnregisterTemplatableControl();
                Initializer.AddFieldsToToolbox();

                ObjectFactory.Container.RegisterType<IFormRulesDecorator, FormRulesDecorator>();
            }
        }

        private static void AddFieldsToToolbox()
        {
            var toolboxesConfig = Config.Get<ToolboxesConfig>();

            var section = Initializer.GetFormsToolboxSection(toolboxesConfig);
            if (section == null)
                return;

            ConfigManager configManager = null;
            var items = new List<KeyValuePair<string, Func<ToolboxItemInfo>>>()
            {
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcTextField", () => new ToolboxItemInfo("Textbox", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.TextFieldController", "sfTextboxIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcPageBreak", () => new ToolboxItemInfo("Page break", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.PageBreakController", "sfPageBreakIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcNavigationField", () => new ToolboxItemInfo("Form navigation", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.NavigationFieldController", "sfFormNavIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcMultipleChoiceField", () => new ToolboxItemInfo("Multiple choice", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.MultipleChoiceFieldController", "sfMultipleChoiceIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcCheckboxesField", () => new ToolboxItemInfo("Checkboxes", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.CheckboxesFieldController", "sfCheckboxesIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcParagraphTextField", () => new ToolboxItemInfo("Paragraph textbox", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.ParagraphTextFieldController", "sfParagraphboxIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcDropdownListField", () => new ToolboxItemInfo("Dropdown list", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.DropdownListFieldController", "sfDropdownIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcSectionHeaderField", () => new ToolboxItemInfo("Section header", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.SectionHeaderController", "sfSectionHeaderIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcInstructionalTextField", () => new ToolboxItemInfo("Content block", "Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers.ContentBlockController", "sfInstructionIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcFileField", () => new ToolboxItemInfo("File upload", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.FileFieldController", "sfFileUploadIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcCaptchaField", () => new ToolboxItemInfo("CAPTCHA", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.CaptchaController", "sfCaptchaIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcHiddenField", () => new ToolboxItemInfo("Hidden field", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.HiddenFieldController", "sfTextboxIcn sfMvcIcn")),
                new KeyValuePair<string, Func<ToolboxItemInfo>>("MvcSubmitButton", () => new ToolboxItemInfo("Submit button", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.SubmitButtonController", "sfSubmitBtnIcn sfMvcIcn")),
            };

            foreach (var item in items)
            {
                if (!section.Tools.Contains(item.Key))
                {
                    if (configManager == null)
                    {
                        configManager = ConfigManager.GetManager();

                        toolboxesConfig = configManager.GetSection<ToolboxesConfig>();
                        section = Initializer.GetFormsToolboxSection(toolboxesConfig);
                    }

                    RegisterToolboxItem(section, item.Key, item.Value());
                }
            }

            if (configManager != null)
            {
                using (new ElevatedModeRegion(configManager))
                {
                    configManager.SaveSection(toolboxesConfig);
                }
            }
        }

        private static void UnregisterTemplatableControl()
        {
            ControlTemplates.UnregisterTemplatableControl(new ControlTemplateInfo() { ControlType = typeof(FormController), AreaName = "Form" });
        }

        private static void RegisteringFormScriptsHandler(IScriptsRegisteringEvent @event)
        {
            var zoneEditor = @event.Sender as ZoneEditor;
            if (zoneEditor != null && zoneEditor.MediaType == DesignMediaType.Form)
            {
                @event.Scripts.Add(new ScriptReference(string.Format("~/Frontend-Assembly/{0}/Mvc/Scripts/Form/form.js", typeof(Initializer).Assembly.GetName().Name)));
            }
        }

        private static void RegisterToolboxItem(ToolboxSection section, string name, ToolboxItemInfo item)
        {
            if (!section.Tools.Contains(name))
            {
                var toolboxItem = new ToolboxItem(section.Tools)
                {
                    Name = name,
                    Ordinal = 0.5f,
                    Title = item.Title,
                    Description = string.Empty,
                    ControlType = typeof(MvcControllerProxy).FullName,
                    ControllerType = item.ControllerType,
                    CssClass = item.CssClass,
                    Parameters = new NameValueCollection() 
                    { 
                        { "ControllerName", item.ControllerType }
                    }
                };

                section.Tools.Add(toolboxItem);
            }
        }

        private static ToolboxSection GetFormsToolboxSection(ToolboxesConfig toolboxesConfig)
        {
            var formsControls = toolboxesConfig.Toolboxes[FormsConstants.FormControlsToolboxName];
            var sectionName = FormsConstants.CommonSectionName;
            ToolboxSection section = formsControls.Sections.Where<ToolboxSection>(e => e.Name == sectionName).FirstOrDefault();

            return section;
        }

        internal class ToolboxItemInfo
        {
            public ToolboxItemInfo(string title, string controllerType, string cssClass)
            {
                this.Title = title;
                this.ControllerType = controllerType;
                this.CssClass = cssClass;
            }

            public string Title
            {
                get;
                private set;
            }

            public string ControllerType
            {
                get;
                private set;
            }

            public string CssClass
            {
                get;
                private set;
            }
        }

        #endregion
    }
}