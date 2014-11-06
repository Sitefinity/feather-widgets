using DynamicContent.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Resources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;

namespace DynamicContent
{
    /// <summary>
    /// This class handles the behavior of the dynamic content widget when working with dynamic modules.
    /// </summary>
    internal class MvcWidgetInstallationStrategy : IWidgetInstallationStrategy
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcWidgetInstallationStrategy"/> class.
        /// </summary>
        public MvcWidgetInstallationStrategy()
        {
            this.ActionProcessor = new Dictionary<string, Action<WidgetInstallationContext>>
            {
                { "Install", (WidgetInstallationContext context) => this.Install(context) },
                { "Uninstall", (WidgetInstallationContext context) => this.Uninstall(context) },
                { "UnregisterTemplates", (WidgetInstallationContext context) => this.UnregisterTemplates(context) },
                { "UpdateTemplates", (WidgetInstallationContext context) => this.Update(context) },
                { "ValidateToolboxItemTemplateKeys", (WidgetInstallationContext context) => this.ValidateToolboxItemTemplateKeys(context) },
                { "UpdateToolboxItem", (WidgetInstallationContext context) => this.RegisterToolboxItem(context) }
            };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the action processor dictionary.
        /// </summary>
        /// <value>
        /// The action processor dictionary.
        /// </value>
        public Dictionary<string, Action<WidgetInstallationContext>> ActionProcessor
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public void Process(WidgetInstallationContext context)
        {
            this.pageManager = context.PageManager;

            if (this.pageManager == null)
                this.pageManager = PageManager.GetManager();

            Action<WidgetInstallationContext> action;

            if (this.ActionProcessor.TryGetValue(context.ActionName, out action))
                action(context);
        }

        /// <summary>
        /// Handles installation of the dynamic widget and its templates.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void Install(WidgetInstallationContext context)
        {
            this.RegisterTemplates(context.DynamicModule, context.DynamicModuleType);
            this.RegisterToolboxItem(context.DynamicModule, context.DynamicModuleType, context.ToolboxesConfig);
        }

        /// <summary>
        /// Updates dynamic widget and its templates on update of the module.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void Update(WidgetInstallationContext context)
        {
            this.Install(context);
        }

        /// <summary>
        /// Uninstalls the dynamic widget.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void Uninstall(WidgetInstallationContext context)
        {
            this.UnregisterToolboxItem(context.ContentTypeName);
        }

        /// <summary>
        /// Handles removing of the templates.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void UnregisterTemplates(WidgetInstallationContext context)
        {
            this.UnregisterTemplates(context.ContentTypeName);
        }

        /// <summary>
        /// Registers the toolbox item.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void RegisterToolboxItem(WidgetInstallationContext context)
        {
            this.RegisterToolboxItem(context.DynamicModule, context.DynamicModuleType, context.ToolboxesConfig);
        }

        /// <summary>
        /// Validates the toolbox item template keys.
        /// </summary>
        /// <param name="context">The context.</param>
        public virtual void ValidateToolboxItemTemplateKeys(WidgetInstallationContext context)
        {
            var defaultMasterTemplateId = context.DefaultMasterTemplateId;
            var defaultDetailTemplateId = context.DefaultDetailTemplateId;

            if (defaultMasterTemplateId == Guid.Empty && defaultDetailTemplateId == Guid.Empty)
            {
                if (defaultMasterTemplateId == Guid.Empty && defaultDetailTemplateId == Guid.Empty)
                {
                    this.RegisterTemplates(context.DynamicModule, context.DynamicModuleType);
                }
            }
        }

        /// <summary>
        /// Registers the templates.
        /// </summary>
        /// <param name="dynamicModule">The dynamic module.</param>
        /// <param name="dynamicModuleType">Type of the dynamic module.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dynamicModule")]
        private void RegisterTemplates(Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule dynamicModule, DynamicModuleType dynamicModuleType)
        {
            var area = dynamicModuleType.DisplayName;
            var moduleTitle = dynamicModule.Title;
            var pluralModuleTypeName = PluralsResolver.Instance.ToPlural(area);

            var listTemplateName = string.Format("List.{0}", area);
            //var nameList = string.Format("MVC List of {0}", pluralModuleTypeName.ToLowerInvariant());
            var friendlyControlList = string.Format("MVC {0} - {1} - list", moduleTitle, pluralModuleTypeName);
            var nameForDevelopersList = listTemplateName.Replace('.', '-');

            var detailTemplateName = string.Format("Detail.{0}", area);
            //var nameDetail = string.Format("MVC Full {0} content", area.ToLowerInvariant()); ;
            var friendlyControlDetail = string.Format("MVC {0} - {1} - single", moduleTitle, pluralModuleTypeName);
            var nameForDevelopersDetail = detailTemplateName.Replace('.', '-');

            var dynamicTypeName = dynamicModuleType.GetFullTypeName();
            var content = "<h1>Dynamic module template for <strong>{0}</strong>, installed in the database.</h1>".Arrange(dynamicTypeName);

            var controlType = typeof(DynamicContentController).FullName;

            var versioningManager = VersionManager.GetManager();

            var listTemplate = this.GetRegisteredTemplate(area, listTemplateName, nameForDevelopersList, friendlyControlList, content, dynamicTypeName, controlType);
            versioningManager.CreateVersion(listTemplate, true);

            var detailTemplate = this.GetRegisteredTemplate(area, detailTemplateName, nameForDevelopersDetail, friendlyControlDetail, content, dynamicTypeName, controlType);
            versioningManager.CreateVersion(detailTemplate, true);
            
            versioningManager.SaveChanges();
        }

        private Type GetControlTypeFromKey(string key)
        {
            int indexOfDash = key.IndexOf("-");
            string typeName = (indexOfDash < 0) ? key : (key.Substring(0, indexOfDash));

            return TypeResolutionService.ResolveType(typeName);
        }

        /// <summary>
        /// Get Registered Template
        /// </summary>
        /// <param name="area">the area name</param>
        /// <param name="name">name for the widget</param>
        /// <param name="nameForDevelopers">name for developers</param>
        /// <param name="friendlyControlName">friendly control name</param>
        /// <param name="content">content</param>
        /// <param name="condition">condition</param>
        /// <param name="controlType">controlType</param>
        /// <returns>ControlPresentation</returns>
        private ControlPresentation GetRegisteredTemplate(string area, string name, string nameForDevelopers, string friendlyControlName, string content, string condition, string controlType)
        {
            var template = this.pageManager.CreatePresentationItem<ControlPresentation>();
            template.AreaName = area;
            template.Data = content;
            template.Condition = condition;
            template.ControlType = controlType;
            template.Name = name;
            template.NameForDevelopers = nameForDevelopers;
            template.FriendlyControlName = friendlyControlName;
            template.IsDifferentFromEmbedded = true;
            template.DataType = Presentation.AspNetTemplate;

            this.pageManager.SaveChanges();

            return template;
        }



        /// <summary>
        /// Registers the toolbox item for the dynamic widget.
        /// </summary>
        /// <param name="dynamicModule">The dynamic module.</param>
        /// <param name="moduleType">Type of the module.</param>
        private void RegisterToolboxItem(Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule dynamicModule, DynamicModuleType moduleType, ToolboxesConfig toolboxesConfig)
        {
            this.UnregisterToolboxItem(moduleType.GetFullTypeName());
            var configurationManager = ConfigManager.GetManager();

            if (toolboxesConfig == null)
            {
                toolboxesConfig = configurationManager.GetSection<ToolboxesConfig>();
            }

            var section = this.GetModuleToolboxSection(dynamicModule, toolboxesConfig);
            if (section == null)
                return;

            var toolboxItem = new ToolboxItem(section.Tools)
            {
                Name = moduleType.GetFullTypeName() + "_MVC",
                Title = PluralsResolver.Instance.ToPlural(moduleType.DisplayName) + " MVC",
                Description = string.Empty,
                ModuleName = dynamicModule.Name,
                ControlType = typeof(MvcWidgetProxy).AssemblyQualifiedName,
                ControllerType = typeof(DynamicContentController).FullName,
                Parameters = new NameValueCollection() 
                    { 
                        { "WidgetName", moduleType.TypeName },
                        { "ControllerName", typeof(DynamicContentController).FullName}
                    }
            };

            section.Tools.Add(toolboxItem);

            configurationManager.SaveSection(toolboxesConfig);
        }

        /// <summary>
        /// Gets the toolbox section where the dynamic widget will be placed.
        /// </summary>
        /// <param name="dynamicModule">The dynamic module.</param>
        /// <param name="toolboxesConfig">The toolboxes configuration.</param>
        /// <returns></returns>
        private ToolboxSection GetModuleToolboxSection(DynamicModule dynamicModule, ToolboxesConfig toolboxesConfig)
        {
            var pageControls = toolboxesConfig.Toolboxes["PageControls"];
            var moduleSectionName = String.Concat(DynamicModuleType.defaultNamespace, ".", MvcWidgetInstallationStrategy.moduleNameValidationRegex.Replace(dynamicModule.Name, ""));            
            ToolboxSection section = pageControls.Sections.Where<ToolboxSection>(e => e.Name == moduleSectionName).FirstOrDefault();

            if (section == null)
            {
                var sectionDescription = string.Format(MvcWidgetInstallationStrategy.moduleSectionDescription, dynamicModule.GetTitle());
                section = new ToolboxSection(pageControls.Sections)
                {
                    Name = moduleSectionName,
                    Title = dynamicModule.Title,
                    Description = sectionDescription
                };

                pageControls.Sections.Add(section);
            }

            return section;
        }

        /// <summary>
        /// Gets the name of the toolbox item.
        /// </summary>
        /// <param name="contentTypeName">Name of the content type.</param>
        /// <returns></returns>
        private string GetToolboxItemName(string contentTypeName)
        {
            return contentTypeName + "_MVC";
        }

        /// <summary>
        /// Removes the toolbox item.
        /// </summary>
        /// <param name="contentTypeName">Name of the content type.</param>
        private void UnregisterToolboxItem(string contentTypeName)
        {
            var configurationManager = ConfigManager.GetManager();
            var toolboxesConfig = configurationManager.GetSection<ToolboxesConfig>();
            var pageControls = toolboxesConfig.Toolboxes["PageControls"];
            var moduleSectionName = contentTypeName.Substring(0, contentTypeName.LastIndexOf('.'));
           
            var section = pageControls.Sections.Where<ToolboxSection>(e => e.Name == moduleSectionName).FirstOrDefault();
            if (section != null)
            {
                var itemToDelete = section.Tools.FirstOrDefault<ToolboxItem>(e => e.Name == this.GetToolboxItemName(contentTypeName));
                
                if (itemToDelete!=null)
                {
                    section.Tools.Remove(itemToDelete);
                }

                if (!section.Tools.Any<ToolboxItem>())
                {
                    pageControls.Sections.Remove(section);
                }
            }

            configurationManager.SaveSection(toolboxesConfig);
        }

        /// <summary>
        /// Removes widget templates.
        /// </summary>
        /// <param name="contentTypeName">Name of the content type.</param>
        private void UnregisterTemplates(string contentTypeName)
        {
            var templatesToDelete = this.pageManager.GetPresentationItems<ControlPresentation>()
                .Where(c => c.Condition == contentTypeName + "_MVC")
                .ToArray();

            foreach (var template in templatesToDelete)
            {
                this.pageManager.Delete(template);
            }

            this.pageManager.SaveChanges();
        }

        #endregion

        #region Private fields and constants

        private PageManager pageManager;
        private ModuleBuilderManager moduleBuilderManager;
        private const string moduleSectionDescription = "Holds all dynamic content widgets for the {0} module.";
        internal static Regex moduleNameValidationRegex = new Regex(@"[^a-zA-Z0-9_.]+", RegexOptions.Compiled);

        #endregion
    }
}
