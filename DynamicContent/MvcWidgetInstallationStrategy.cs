using DynamicContent.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
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
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;

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
            this.RegisterToolboxItem(context.DynamicModule, context.DynamicModuleType);
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
            this.RegisterToolboxItem(context.DynamicModule, context.DynamicModuleType);
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

            this.RegisterToolboxItem(context);
        }

        /// <summary>
        /// Registers the templates.
        /// </summary>
        /// <param name="dynamicModule">The dynamic module.</param>
        /// <param name="dynamicModuleType">Type of the dynamic module.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "dynamicModule")]
        private void RegisterTemplates(Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule dynamicModule, DynamicModuleType dynamicModuleType)
        {
            var area = string.Format("~/Frontend-Assembly/{0}/", this.GetType().Assembly.GetName().Name);
            var resourceName = area + "Mvc/Views/{0}/List.DynamicContentList.cshtml".Arrange(dynamicModuleType.TypeName);
            var dynamicTypeName = dynamicModuleType.GetFullTypeName();
            var content = "<h1>Dynamic module template for <strong>{0}</strong>, installed in the database.</h1>".Arrange(dynamicTypeName);

            this.RegisterTemplate(area, resourceName, ".cshtml", content, dynamicTypeName + "_MVC");
        }

        /// <summary>
        /// Registers the template.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="content">The content.</param>
        /// <param name="condition">The condition.</param>
        private void RegisterTemplate(string area, string resourceName, string resourceType, string content, string condition)
        {
            var template = this.pageManager.CreatePresentationItem<ControlPresentation>();
            template.NameForDevelopers = resourceName;
            template.AreaName = area;
            template.DataType = resourceType;
            template.Data = content;
            template.Condition = condition;
            this.pageManager.SaveChanges();
        }

        /// <summary>
        /// Registers the toolbox item for the dynamic widget.
        /// </summary>
        /// <param name="dynamicModule">The dynamic module.</param>
        /// <param name="moduleType">Type of the module.</param>
        private void RegisterToolboxItem(Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule dynamicModule, DynamicModuleType moduleType)
        {
            this.UnregisterToolboxItem(moduleType.GetFullTypeName());

            var configurationManager = ConfigManager.GetManager();
            var toolboxesConfig = configurationManager.GetSection<ToolboxesConfig>();
            var section = this.GetToolboxSection(toolboxesConfig);
            if (section == null)
                return;

            if (moduleType.ParentModuleTypeId == Guid.Empty)
            {
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
                        { "ControllerType", typeof(DynamicContentController).FullName}
                    }
                };

                section.Tools.Add(toolboxItem);

                configurationManager.SaveSection(toolboxesConfig);
            }
        }

        /// <summary>
        /// Gets the toolbox section where the dynamic widget will be placed.
        /// </summary>
        /// <param name="toolboxesConfig">The toolboxes configuration.</param>
        /// <returns></returns>
        private ToolboxSection GetToolboxSection(ToolboxesConfig toolboxesConfig)
        {
            var pageControls = toolboxesConfig.Toolboxes["PageControls"];
            var section = pageControls
                .Sections
                .Where<ToolboxSection>(e => e.Name == "MvcWidgets")
                .FirstOrDefault();

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
            var section = this.GetToolboxSection(toolboxesConfig);
            if (section == null)
                return;

            var toolboxItemName = this.GetToolboxItemName(contentTypeName);
            var toolboxItem = section.Tools.FirstOrDefault<ToolboxItem>(e => e.Name == toolboxItemName);
            if (toolboxItem != null)
            {
                section.Tools.Remove(toolboxItem);
                configurationManager.SaveSection(toolboxesConfig);
            }
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

        #endregion
    }
}
