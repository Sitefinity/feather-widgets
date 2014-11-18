using DynamicContent.Mvc.Controllers;
using DynamicContent.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Frontend.Resources.Resolvers;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace DynamicContent
{
    /// <summary>
    /// This class generates and registers dynamic widget views.
    /// </summary>
    internal class WidgetViewGenerator
    {
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
        public WidgetViewGenerator(PageManager pageManager, ModuleBuilderManager moduleBuilderManager) 
        {
            if (pageManager == null)
                throw new ArgumentNullException("pageManager");

            if (moduleBuilderManager == null)
                throw new ArgumentNullException("moduleBuilderManager");

            this.pageManager = pageManager;
            this.moduleBuilderManager = moduleBuilderManager;
        }
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
            var controlType = typeof(DynamicContentController).FullName;

            var listTemplateName = string.Format("List.{0}", moduleType.DisplayName);
            //var nameList = string.Format("MVC List of {0}", pluralModuleTypeName.ToLowerInvariant());
            var friendlyControlList = string.Format("{0} - {1} - list (MVC)", moduleTitle, pluralModuleTypeName);
            var nameForDevelopersList = listTemplateName.Replace('.', '-');

            var content = this.GenerateMasterTemplate();
            var listTemplate = this.RegisteredTemplate(area, listTemplateName, nameForDevelopersList, friendlyControlList, content, dynamicTypeName, controlType);


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
            var controlType = typeof(DynamicContentController).FullName;

            var detailTemplateName = string.Format("Detail.{0}", moduleType.DisplayName);
            //var nameDetail = string.Format("MVC Full {0} content", area.ToLowerInvariant()); ;
            var friendlyControlDetail = string.Format("{0} - {1} - single (MVC)", moduleTitle, pluralModuleTypeName);
            var nameForDevelopersDetail = detailTemplateName.Replace('.', '-');

            var content = this.GenerateDetailTemplate(moduleType);
            var detailTemplate = this.RegisteredTemplate(area, detailTemplateName, nameForDevelopersDetail, friendlyControlDetail, content, dynamicTypeName, controlType);

            return detailTemplate.Id;
        }

        /// <summary>
        /// Generates the master template.
        /// </summary>
        /// <returns></returns>
        private string GenerateMasterTemplate()
        {
            var defaultTemplateText = this.GetDefaultTemplate(WidgetViewGenerator.masterViewDefaultPath);

            return defaultTemplateText;
        }

        /// <summary>
        /// Generates the detail template and with all dynamic fields needed.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        /// <returns></returns>
        private string GenerateDetailTemplate(DynamicModuleType moduleType)
        {
            var defaultTemplateText = this.GetDefaultTemplate(WidgetViewGenerator.detailViewDefaultPath);
            var generatedFieldsMarkup = DynamicFieldHelper.GenerateFieldsSection(moduleType).ToHtmlString();

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
            var versioningManager = Telerik.Sitefinity.Versioning.VersionManager.GetManager();

            var template = this.pageManager.GetPresentationItems<ControlPresentation>().Where(cp => cp.NameForDevelopers == nameForDevelopers && cp.AreaName == area).FirstOrDefault();
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

            this.pageManager.SaveChanges();

            versioningManager.CreateVersion(template, true);
            versioningManager.SaveChanges();
            return template;
        }

        private readonly PageManager pageManager;
        private readonly ModuleBuilderManager moduleBuilderManager;

        private const string masterViewDefaultPath = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/List.DefaultDynamicContentList.cshtml";
        private const string detailViewDefaultPath = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/Detail.DefaultDetailPage.cshtml";
        private const string DynamicFieldsText = "@DynamicFieldHelper.GenerateFieldsSection()";
    }
}
