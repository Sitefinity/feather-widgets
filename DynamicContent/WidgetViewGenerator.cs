using System;
using System.IO;
using System.Linq;
using DynamicContent.Mvc.Controllers;
using DynamicContent.Mvc.Helpers;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Versioning;

namespace DynamicContent
{
    /// <summary>
    /// This class generates and registers dynamic widget views.
    /// </summary>
    internal class WidgetViewGenerator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetViewGenerator"/> class.
        /// </summary>
        /// <param name="pageManager">The page manager.</param>
        /// <param name="moduleBuilderManager">The module builder manager.</param>
        /// <exception cref="System.ArgumentNullException">
        /// pageManager
        /// or
        /// moduleBuilderManager
        /// </exception>
        public WidgetViewGenerator(PageManager pageManager, ModuleBuilderManager moduleBuilderManager, VersionManager versionManager) 
        {
            if (pageManager == null)
                throw new ArgumentNullException("pageManager");

            if (moduleBuilderManager == null)
                throw new ArgumentNullException("moduleBuilderManager");

            this.pageManager = pageManager;
            this.moduleBuilderManager = moduleBuilderManager;
            this.versionManager = versionManager;
        }

        #endregion 

        #region Public methods

        /// <summary>
        /// Installs the default master template.
        /// </summary>
        /// <param name="dynamicModule">The dynamic module.</param>
        /// <param name="moduleType">Type of the module.</param>
        /// <returns></returns>
        public Guid InstallDefaultMasterTemplate(DynamicModule dynamicModule, DynamicModuleType moduleType)
        {
            var moduleTitle = dynamicModule.Title;
            var area = string.Format("{0} - {1}", moduleTitle, moduleType.DisplayName);
            var pluralModuleTypeName = PluralsResolver.Instance.ToPlural(moduleType.DisplayName);
            var dynamicTypeName = moduleType.GetFullTypeName();
            var condition = string.Format(WidgetViewGenerator.MvcTemplateCondition, dynamicTypeName);
            var controlType = typeof(DynamicContentController).FullName;

            var listTemplateName = string.Format("List.{0}", moduleType.DisplayName);
            var friendlyControlList = string.Format("{0} - {1} - list (MVC)", moduleTitle, pluralModuleTypeName);
            var nameForDevelopersList = listTemplateName.Replace('.', '-');

            var content = this.GenerateMasterTemplate(moduleType);
            var listTemplate = this.RegisteredTemplate(area, listTemplateName, nameForDevelopersList, friendlyControlList, content, condition, controlType);

            return listTemplate.Id;
        }

        /// <summary>
        /// Installs the default detail template.
        /// </summary>
        /// <param name="dynamicModule">The dynamic module.</param>
        /// <param name="moduleType">Type of the module.</param>
        /// <returns></returns>
        public Guid InstallDefaultDetailTemplate(DynamicModule dynamicModule, DynamicModuleType moduleType)
        {
            var moduleTitle = dynamicModule.Title;
            var area = string.Format("{0} - {1}", moduleTitle, moduleType.DisplayName);
            var pluralModuleTypeName = PluralsResolver.Instance.ToPlural(moduleType.DisplayName);
            var dynamicTypeName = moduleType.GetFullTypeName();
            var condition = string.Format(WidgetViewGenerator.MvcTemplateCondition, dynamicTypeName);
            var controlType = typeof(DynamicContentController).FullName;

            var detailTemplateName = string.Format("Detail.{0}", moduleType.DisplayName);
            var friendlyControlDetail = string.Format("{0} - {1} - single (MVC)", moduleTitle, pluralModuleTypeName);
            var nameForDevelopersDetail = detailTemplateName.Replace('.', '-');

            var content = this.GenerateDetailTemplate(moduleType);
            var detailTemplate = this.RegisteredTemplate(area, detailTemplateName, nameForDevelopersDetail, friendlyControlDetail, content, condition, controlType);

            return detailTemplate.Id;
        }

        /// <summary>
        /// Removes widget templates.
        /// </summary>
        /// <param name="contentTypeName">Name of the content type.</param>
        public void UnregisterTemplates(string contentTypeName)
        {
            var mvcTemplateCondition = string.Format(WidgetViewGenerator.MvcTemplateCondition, contentTypeName);
            var templatesToDelete = this.pageManager.GetPresentationItems<ControlPresentation>()
                .Where(c => c.Condition == mvcTemplateCondition)
                .ToArray();

            foreach (var template in templatesToDelete)
            {
                this.pageManager.Delete(template);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Generates the content of master template.
        /// </summary>
        /// <returns></returns>
        private string GenerateMasterTemplate(DynamicModuleType moduleType)
        {
            var defaultTemplateText = this.GetDefaultTemplate(WidgetViewGenerator.MasterViewDefaultPath);
            var mainPictureMarkup = DynamicFieldHelper.MainPictureSection(moduleType).ToHtmlString();
            var mainShortTextMarkup = DynamicFieldHelper.MainTextFieldForList(moduleType).ToHtmlString();

            defaultTemplateText = defaultTemplateText.Replace(WidgetViewGenerator.MainShortFieldTextForList, mainShortTextMarkup);
            defaultTemplateText = defaultTemplateText.Replace(WidgetViewGenerator.MainPictureFieldText, mainPictureMarkup);

            return defaultTemplateText;
        }

        /// <summary>
        /// Generates the content of detail template with all dynamic fields needed.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        /// <returns></returns>
        private string GenerateDetailTemplate(DynamicModuleType moduleType)
        {
            var defaultTemplateText = this.GetDefaultTemplate(WidgetViewGenerator.DetailViewDefaultPath);
            var generatedFieldsMarkup = DynamicFieldHelper.GenerateFieldsSection(moduleType).ToHtmlString();
            var mainShortTextMarkup = DynamicFieldHelper.MainTextFieldForDetail(moduleType).ToHtmlString();

            defaultTemplateText = defaultTemplateText.Replace(WidgetViewGenerator.MainShortFieldTextForDetail, mainShortTextMarkup);
            defaultTemplateText = defaultTemplateText.Replace(WidgetViewGenerator.DynamicFieldsText, generatedFieldsMarkup);

            return defaultTemplateText;
        }

        /// <summary>
        /// Gets the default template.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private string GetDefaultTemplate(string path)
        {
            var templateText = string.Empty;

            if (VirtualPathManager.FileExists(path))
            {
                var fileStream = VirtualPathManager.OpenFile(path);

                using (var streamReader = new StreamReader(fileStream))
                {
                    templateText = streamReader.ReadToEnd();
                }
            }

            return templateText;
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
        private ControlPresentation RegisteredTemplate(string area, string name, string nameForDevelopers, string friendlyControlName, string content, string condition, string controlType)
        {
            var template = this.pageManager.GetPresentationItems<ControlPresentation>().Where(cp => cp.NameForDevelopers == nameForDevelopers && cp.AreaName == area && cp.Condition == condition).FirstOrDefault();
            if (template == null)
            {
                template = this.pageManager.CreatePresentationItem<ControlPresentation>();
            }

            template.AreaName = area;
            template.Data = content;
            template.Condition = condition;
            template.ControlType = controlType;
            template.Name = name;
            template.NameForDevelopers = nameForDevelopers;
            template.FriendlyControlName = friendlyControlName;
            template.IsDifferentFromEmbedded = true;
            template.DataType = Presentation.AspNetTemplate;

            this.versionManager.CreateVersion(template, true);

            return template;
        }

        #endregion

        #region Privte fields and Constants

        private PageManager pageManager;
        private ModuleBuilderManager moduleBuilderManager;
        private VersionManager versionManager;

        internal const string MvcTemplateCondition = "{0} AND MVC";
        private const string MasterViewDefaultPath = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/List.DefaultDynamicContentList.cshtml";
        private const string DetailViewDefaultPath = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/Detail.DefaultDetailPage.cshtml";
        private const string MainShortFieldTextForList = "@DynamicFieldHelper.MainTextFieldForList()";
        private const string MainShortFieldTextForDetail = "@DynamicFieldHelper.MainTextFieldForDetail()";
        private const string DynamicFieldsText = "@DynamicFieldHelper.GenerateFieldsSection()";
        private const string MainPictureFieldText = "@DynamicFieldHelper.MainPictureSection()";

        #endregion
    }
}
