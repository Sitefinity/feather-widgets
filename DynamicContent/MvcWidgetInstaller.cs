﻿using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

using DynamicContent.Mvc.Controllers;
using DynamicContent.TemplateGeneration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Events;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning;

namespace DynamicContent
{
    /// <summary>
    /// This class handles the behavior of the dynamic content widget when working with dynamic modules.
    /// </summary>
    internal static class MvcWidgetInstaller
    {
        /// <summary>
        /// Initializes the widget installation logic.
        /// </summary>
        public static void Initialize()
        {
            //// Install/Update
            EventHub.Unsubscribe<IDynamicModuleUpdatingEvent>(MvcWidgetInstaller.DynamicModuleUpdatingEventHandler);
            EventHub.Subscribe<IDynamicModuleUpdatingEvent>(MvcWidgetInstaller.DynamicModuleUpdatingEventHandler);

            EventHub.Unsubscribe<IDynamicModuleTypeCreatingEvent>(MvcWidgetInstaller.DynamicModuleTypeEventHandler);
            EventHub.Subscribe<IDynamicModuleTypeCreatingEvent>(MvcWidgetInstaller.DynamicModuleTypeEventHandler);

            EventHub.Unsubscribe<IDynamicModuleTypeUpdatingEvent>(MvcWidgetInstaller.DynamicModuleTypeEventHandler);
            EventHub.Subscribe<IDynamicModuleTypeUpdatingEvent>(MvcWidgetInstaller.DynamicModuleTypeEventHandler);

            //// Uninstall
            EventHub.Unsubscribe<IDynamicModuleTypeDeletingEvent>(MvcWidgetInstaller.DynamicModuleTypeDeletingEventHandler);
            EventHub.Subscribe<IDynamicModuleTypeDeletingEvent>(MvcWidgetInstaller.DynamicModuleTypeDeletingEventHandler);
        }

        #region Private methods

        private static void RegisterTemplates(Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule dynamicModule, DynamicModuleType dynamicModuleType)
        {
            var transactionName = ((ModuleBuilderDataProvider)dynamicModule.Provider).TransactionName;
            MvcWidgetInstaller.TemplateGaneratorAction(
                templateGenerator =>
                {
                    templateGenerator.InstallMasterTemplate(dynamicModule, dynamicModuleType);
                    templateGenerator.InstallDetailTemplate(dynamicModule, dynamicModuleType);
                }, 
                transactionName);
        }

        private static void RegisterToolboxItem(Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule dynamicModule, DynamicModuleType moduleType)
        {
            MvcWidgetInstaller.UnregisterToolboxItem(moduleType.GetFullTypeName());
            var configurationManager = ConfigManager.GetManager();
            var toolboxesConfig = configurationManager.GetSection<ToolboxesConfig>();

            var section = MvcWidgetInstaller.GetModuleToolboxSection(dynamicModule, toolboxesConfig);
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
                        { "ControllerName", typeof(DynamicContentController).FullName }
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
        private static ToolboxSection GetModuleToolboxSection(DynamicModule dynamicModule, ToolboxesConfig toolboxesConfig)
        {
            var pageControls = toolboxesConfig.Toolboxes["PageControls"];
            var moduleSectionName = string.Concat(DynamicModuleType.defaultNamespace, ".", MvcWidgetInstaller.moduleNameValidationRegex.Replace(dynamicModule.Name, string.Empty));            
            ToolboxSection section = pageControls.Sections.Where<ToolboxSection>(e => e.Name == moduleSectionName).FirstOrDefault();

            if (section == null)
            {
                var sectionDescription = string.Format(MvcWidgetInstaller.ModuleSectionDescription, dynamicModule.GetTitle());
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
        private static string GetToolboxItemName(string contentTypeName)
        {
            return contentTypeName + "_MVC";
        }

        /// <summary>
        /// Removes the toolbox item.
        /// </summary>
        /// <param name="contentTypeName">Name of the content type.</param>
        private static void UnregisterToolboxItem(string contentTypeName)
        {
            var configurationManager = ConfigManager.GetManager();
            var toolboxesConfig = configurationManager.GetSection<ToolboxesConfig>();
            var pageControls = toolboxesConfig.Toolboxes["PageControls"];
            var moduleSectionName = contentTypeName.Substring(0, contentTypeName.LastIndexOf('.'));
           
            var section = pageControls.Sections.Where<ToolboxSection>(e => e.Name == moduleSectionName).FirstOrDefault();
            if (section != null)
            {
                var itemToDelete = section.Tools.FirstOrDefault<ToolboxItem>(e => e.Name == MvcWidgetInstaller.GetToolboxItemName(contentTypeName));
                
                if (itemToDelete != null)
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

        private static void DynamicModuleUpdatingEventHandler(IDynamicModuleUpdatingEvent @event)
        {
            if (@event == null || @event.ChangedProperties == null || @event.Item == null || @event.Item.Types == null || @event.Item.Types.Length == 0)
                return;

            if (@event.ChangedProperties.ContainsKey("Status"))
            {
                var statusChange = @event.ChangedProperties["Status"];
                if ((DynamicModuleStatus)statusChange.NewValue == DynamicModuleStatus.Active && (int)statusChange.OldValue == (int)DynamicModuleStatus.NotInstalled)
                {
                    foreach (var moduleType in @event.Item.Types)
                    {
                        MvcWidgetInstaller.Install(@event.Item, moduleType);
                    }
                }
            }
        }

        private static void DynamicModuleTypeEventHandler(IDynamicModuleTypeItemEvent @event)
        {
            if (@event == null || @event.Item == null)
                return;

            var updatingEvent = @event as IDynamicModuleTypeUpdatingEvent;
            if (updatingEvent != null && updatingEvent.ShouldUpdateWidgetTemplates == false)
                return;

            var module = ModuleBuilderManager.GetManager().Provider.GetDynamicModules().SingleOrDefault(m => m.Id == @event.Item.ParentModuleId);
            if (module != null && module.Status != DynamicModuleStatus.NotInstalled)
            {
                MvcWidgetInstaller.Install(module, @event.Item);
            }
        }

        private static void Install(DynamicModule module, DynamicModuleType type)
        {
            MvcWidgetInstaller.RegisterTemplates(module, type);
            MvcWidgetInstaller.RegisterToolboxItem(module, type);
        }

        private static void DynamicModuleTypeDeletingEventHandler(IDynamicModuleTypeDeletingEvent @event)
        {
            if (@event == null || @event.Item == null || @event.Item.Provider == null)
                return;

            var transactionName = ((ModuleBuilderDataProvider)@event.Item.Provider).TransactionName;
            MvcWidgetInstaller.TemplateGaneratorAction(
                templateGenerator => templateGenerator.UnregisterTemplates(@event.Item.GetFullTypeName()), 
                transactionName);

            MvcWidgetInstaller.UnregisterToolboxItem(@event.Item.GetFullTypeName());
        }

        private static void TemplateGaneratorAction(Action<TemplateGenerator> action, string transactionName)
        {
            var versionManager = VersionManager.GetManager(null, transactionName);
            var pageManager = PageManager.GetManager(null, transactionName);
            var moduleManager = ModuleBuilderManager.GetManager(null, transactionName);

            var templateGenerator = new TemplateGenerator(pageManager, moduleManager, versionManager);

            action(templateGenerator);

            if (transactionName.IsNullOrEmpty())
            {
                moduleManager.SaveChanges();
                pageManager.SaveChanges();
                versionManager.SaveChanges();
            }
        }

        #endregion

        #region Private fields and constants

        private static Regex moduleNameValidationRegex = new Regex(@"[^a-zA-Z0-9_.]+", RegexOptions.Compiled);
        private const string ModuleSectionDescription = "Holds all dynamic content widgets for the {0} module.";

        #endregion
    }
}
