using System;
using System.IO;
using System.Linq;
using System.Text;
using DynamicContent.Mvc.Controllers;
using DynamicContent.TemplateGeneration.Fields;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Versioning;

namespace DynamicContent.TemplateGeneration
{
    /// <summary>
    /// This class generates and registers templates for dynamic widget.
    /// </summary>
    internal class TemplateGenerator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateGenerator"/> class.
        /// </summary>
        /// <param name="pageManager">The page manager.</param>
        /// <param name="moduleBuilderManager">The module builder manager.</param>
        /// <exception cref="System.ArgumentNullException">
        /// pageManager
        /// or
        /// moduleBuilderManager
        /// </exception>
        public TemplateGenerator(PageManager pageManager, ModuleBuilderManager moduleBuilderManager, VersionManager versionManager) 
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
        public Guid InstallMasterTemplate(DynamicModule dynamicModule, DynamicModuleType moduleType)
        {
            var moduleTitle = dynamicModule.Title;
            var area = string.Format("{0} - {1}", moduleTitle, moduleType.DisplayName);
            var pluralModuleTypeName = PluralsResolver.Instance.ToPlural(moduleType.DisplayName);
            var dynamicTypeName = moduleType.GetFullTypeName();
            var condition = string.Format(TemplateGenerator.MvcTemplateCondition, dynamicTypeName);
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
        public Guid InstallDetailTemplate(DynamicModule dynamicModule, DynamicModuleType moduleType)
        {
            var moduleTitle = dynamicModule.Title;
            var area = string.Format("{0} - {1}", moduleTitle, moduleType.DisplayName);
            var pluralModuleTypeName = PluralsResolver.Instance.ToPlural(moduleType.DisplayName);
            var dynamicTypeName = moduleType.GetFullTypeName();
            var condition = string.Format(TemplateGenerator.MvcTemplateCondition, dynamicTypeName);
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
            var mvcTemplateCondition = string.Format(TemplateGenerator.MvcTemplateCondition, contentTypeName);
            var templatesToDelete = this.pageManager.GetPresentationItems<ControlPresentation>()
                .Where(c => c.Condition == mvcTemplateCondition)
                .ToArray();

            foreach (var template in templatesToDelete)
            {
                this.pageManager.Delete(template);
            }
        }

        /// <summary>
        /// Generates the dynamic field section markup.
        /// </summary>
        /// <returns></returns>
        protected internal virtual string GenerateDynamicFieldSection(DynamicModuleType moduleType)
        {
            var fieldGenerators = ObjectFactory.Container.ResolveAll<IField>(new ParameterOverride("moduleType", moduleType)).ToList();

            StringBuilder fieldsSectionBuilder = new StringBuilder();

            foreach (var fieldGenerator in fieldGenerators)
            {
                var fieldsForType = moduleType.Fields.Where(fieldGenerator.GetCondition);
                if (fieldsForType.Count() != 0)
                {
                    foreach (DynamicModuleField currentField in fieldsForType)
                    {
                        fieldsSectionBuilder.Append(fieldGenerator.GetMarkup(currentField));
                        fieldsSectionBuilder.Append(TemplateGenerator.EmptyLine);
                    }
                }
            }

            return fieldsSectionBuilder.ToString();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Generates the content of master template.
        /// </summary>
        /// <returns></returns>
        private string GenerateMasterTemplate(DynamicModuleType moduleType)
        {
            var defaultTemplateText = this.GetDefaultTemplate(TemplateGenerator.MasterViewDefaultPath);
            var mainPictureMarkup = this.GetMainPictureSection(moduleType);
            var mainShortTextMarkup = string.Format(TemplateGenerator.ListItemPropertyMarkup, moduleType.MainShortTextFieldName);

            defaultTemplateText = defaultTemplateText.Replace(TemplateGenerator.MainShortFieldTextForList, mainShortTextMarkup);
            defaultTemplateText = defaultTemplateText.Replace(TemplateGenerator.MainPictureFieldText, mainPictureMarkup);

            return defaultTemplateText;
        }

        /// <summary>
        /// Generates the content of detail template with all dynamic fields needed.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        /// <returns></returns>
        private string GenerateDetailTemplate(DynamicModuleType moduleType)
        {
            var defaultTemplateText = this.GetDefaultTemplate(TemplateGenerator.DetailViewDefaultPath);
            var generatedFieldsMarkup = this.GenerateDynamicFieldSection(moduleType);
            var mainShortTextMarkup = string.Format(TemplateGenerator.DetailItemPropertyMarkup, moduleType.MainShortTextFieldName);

            defaultTemplateText = defaultTemplateText.Replace(TemplateGenerator.MainShortFieldTextForDetail, mainShortTextMarkup);
            defaultTemplateText = defaultTemplateText.Replace(TemplateGenerator.DynamicFieldsText, generatedFieldsMarkup);

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

        /// <summary>
        /// Gets the main picture section markup.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        /// <returns></returns>
        private string GetMainPictureSection(DynamicModuleType moduleType)
        {
            var firstMediaFieldTypeImage = moduleType.Fields.FirstOrDefault(f => f.FieldType == FieldType.Media
                                                                                && f.FieldStatus != DynamicModuleFieldStatus.Removed
                                                                                && f.MediaType == "image");
            var fieldMarkup = TemplateGenerator.EmptyLine;

            if (firstMediaFieldTypeImage != null)
            {
                fieldMarkup = this.GetImageFieldMarkup(firstMediaFieldTypeImage);
            }

            return fieldMarkup;
        }

        /// <summary>
        /// Gets the image field markup.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        private string GetImageFieldMarkup(DynamicModuleField field)
        {
            var markup = string.Empty;
            if (field.AllowMultipleImages)
            {
                markup = string.Format(TemplateGenerator.MultiImageFieldMarkupTempalte, field.Name);
            }
            else
            {
                markup = string.Format(TemplateGenerator.SingleImageFieldMarkupTempalte, field.Name);
            }

            return markup;
        }

        #endregion

        #region Privte fields and Constants

        private PageManager pageManager;
        private ModuleBuilderManager moduleBuilderManager;
        private VersionManager versionManager;

        internal static readonly string EmptyLine = "\r\n";
        internal const string MvcTemplateCondition = "{0} AND MVC";
        private const string DetailItemPropertyMarkup = "@Model.Item.{0}";
        private const string ListItemPropertyMarkup = "@item.{0}";
        private const string MasterViewDefaultPath = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/ListTemplateContainer.cshtml";
        private const string DetailViewDefaultPath = "~/Frontend-Assembly/DynamicContent/Mvc/Views/Shared/DetailTemplateContainer.cshtml";
        private const string MainShortFieldTextForList = "@*MainTextFieldForList*@";
        private const string MainShortFieldTextForDetail = "@*MainTextFieldForDetail*@";
        private const string DynamicFieldsText = "@*GenerateFieldsSection*@";
        private const string MainPictureFieldText = "@*MainPictureSection*@";

        private const string SingleImageFieldMarkupTempalte = @"@Html.Sitefinity().ImageField(((IEnumerable<ContentLink>)item.{0}).FirstOrDefault(), ""{0}"")";
        private const string MultiImageFieldMarkupTempalte = @"@Html.Sitefinity().ImageField((IEnumerable<ContentLink>)item.{0}, ""{0}"")";

        #endregion
    }
}
