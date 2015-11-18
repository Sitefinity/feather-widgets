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
using Telerik.Sitefinity.Web.UI.ContentUI;
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
            ObjectFactory.Container.RegisterInstance<IControlDefinitionExtender>("FormsDefinitionsExtender", new FormsDefinitionsExtender(), new ContainerControlledLifetimeManager());

            ObjectFactory.Container.RegisterType<IFormFieldBackendConfigurator, BackendFieldFallbackConfigurator>(typeof(MvcControllerProxy).FullName);

            EventHub.Unsubscribe<IScriptsRegisteringEvent>(Initializer.RegisteringFormScriptsHandler);
            EventHub.Subscribe<IScriptsRegisteringEvent>(Initializer.RegisteringFormScriptsHandler);

            VirtualPathManager.AddVirtualFileResolver<FormsVirtualRazorResolver>(FormsVirtualRazorResolver.Path + "*", "MvcFormsResolver");

            Initializer.UnregisterTemplatableControl();
            Initializer.AddFieldsToToolbox();
        }

        #region Private Methods

        private static void AddFieldsToToolbox()
        {
            var configurationManager = ConfigManager.GetManager();
            configurationManager.Provider.SuppressSecurityChecks = true;
            var toolboxesConfig = configurationManager.GetSection<ToolboxesConfig>();

            var section = Initializer.GetFormsToolboxSection(toolboxesConfig);
            if (section == null)
                return;

            Initializer.RegisterToolboxItem(section, "MvcTextField", "Textbox", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.TextFieldController", "sfTextboxIcn sfMvcIcn");
            Initializer.RegisterToolboxItem(section, "MvcMultipleChoiceField", "Multiple choice", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.MultipleChoiceFieldController", "sfMultipleChoiceIcn sfMvcIcn");
            Initializer.RegisterToolboxItem(section, "MvcCheckboxesField", "Checkboxes", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.CheckboxesFieldController", "sfCheckboxesIcn sfMvcIcn");
            Initializer.RegisterToolboxItem(section, "MvcParagraphTextField", "Paragraph textbox", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.ParagraphTextFieldController", "sfParagraphboxIcn sfMvcIcn");
            Initializer.RegisterToolboxItem(section, "MvcDropdownListField", "Dropdown list", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.DropdownListFieldController", "sfDropdownIcn sfMvcIcn");
            Initializer.RegisterToolboxItem(section, "MvcSectionHeaderField", "Section header", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.SectionHeaderController", "sfSectionHeaderIcn sfMvcIcn");
            Initializer.RegisterToolboxItem(section, "MvcInstructionalTextField", "Content block", "Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers.ContentBlockController", "sfInstructionIcn sfMvcIcn");
            Initializer.RegisterToolboxItem(section, "MvcFileField", "File upload", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.FileFieldController", "sfFileUploadIcn sfMvcIcn");
            Initializer.RegisterToolboxItem(section, "MvcCaptchaField", "CAPTCHA", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.CaptchaController", "sfCaptchaIcn sfMvcIcn");
            Initializer.RegisterToolboxItem(section, "MvcSubmitButton", "Submit button", "Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.SubmitButtonController", "sfSubmitBtnIcn sfMvcIcn");

            configurationManager.SaveSection(toolboxesConfig);
            configurationManager.Provider.SuppressSecurityChecks = false;
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

        private static void RegisterToolboxItem(ToolboxSection section, string name, string title, string controllerType, string cssClass)
        {
            if (!section.Tools.Any<ToolboxItem>(t => t.Name == name))
            {
                var toolboxItem = new ToolboxItem(section.Tools)
                {
                    Name = name,
                    Title = title,
                    Description = string.Empty,
                    ControlType = typeof(MvcControllerProxy).FullName,
                    ControllerType = controllerType,
                    CssClass = cssClass,
                    Parameters = new NameValueCollection() 
                    { 
                        { "ControllerName", controllerType }
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

        #endregion
    }
}