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
using Telerik.Sitefinity.Frontend.Resources.Resolvers;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace DynamicContent
{
    internal class WidgetViewGenerator
    {
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
            var area = string.Format("~/Frontend-Assembly/{0}/", this.GetType().Assembly.GetName().Name);
            var resourceName = area + "Mvc/Views/{0}/List.DynamicContentList.cshtml".Arrange(moduleType.TypeName);
            var dynamicTypeName = moduleType.GetFullTypeName();
            var content = this.GenerateMasterTemplate();

            var templateId = this.RegisterTemplate(area, resourceName, ".cshtml", content, dynamicTypeName + "_MVC");
            
            return templateId;
        }

        public Guid InstallDefaultDetailTemplate(DynamicModule dynamicModule, DynamicModuleType moduleType)
        {
            var area = string.Format("~/Frontend-Assembly/{0}/", this.GetType().Assembly.GetName().Name);
            var resourceName = area + "Mvc/Views/{0}/Detail.DetailPage.cshtml".Arrange(moduleType.TypeName);
            var dynamicTypeName = moduleType.GetFullTypeName();
            var content = this.GenerateDetailTemplate(moduleType);

            var templateId = this.RegisterTemplate(area, resourceName, ".cshtml", content, dynamicTypeName + "_MVC");

            return templateId;
        }

        private string GenerateMasterTemplate()
        {
            var defaultTemplateText = this.GetDefaultTemplate(WidgetViewGenerator.masterViewDefaultPath);

            return defaultTemplateText;
        }

        private string GenerateDetailTemplate(DynamicModuleType moduleType)
        {
            var defaultTemplateText = this.GetDefaultTemplate(WidgetViewGenerator.detailViewDefaultPath);
            var generatedFieldsMarkup = DynamicFieldHelper.GenerateFieldsSection(moduleType).ToHtmlString();

            defaultTemplateText = defaultTemplateText.Replace(WidgetViewGenerator.DynamicFieldsText, generatedFieldsMarkup);

            return defaultTemplateText;
        }

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
        /// Registers the template.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="content">The content.</param>
        /// <param name="condition">The condition.</param>
        private Guid RegisterTemplate(string area, string resourceName, string resourceType, string content, string condition)
        {
            var template = this.pageManager.CreatePresentationItem<ControlPresentation>();
            template.NameForDevelopers = resourceName;
            template.AreaName = area;
            template.DataType = resourceType;
            template.Data = content;
            template.Condition = condition;
            this.pageManager.SaveChanges();

            return template.Id;
        }

        private readonly PageManager pageManager;
        private readonly ModuleBuilderManager moduleBuilderManager;

        private const string masterViewDefaultPath = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/List.DefaultDynamicContentList.cshtml";
        private const string detailViewDefaultPath = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/Detail.DefaultDetailPage.cshtml";
        private const string DynamicFieldsText = "@DynamicFieldHelper.GenerateFieldsSection()";
    }
}
