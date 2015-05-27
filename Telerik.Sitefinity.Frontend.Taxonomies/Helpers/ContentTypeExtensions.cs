using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Helpers
{
    public static class ContentTypeExtensions
    {
        private static readonly IDictionary<string, StaticType> staticTypesMap = new Dictionary<string, StaticType>
        {
            { "NewsItem", new StaticType() {TypeName = "Telerik.Sitefinity.News.Model.NewsItem", ModuleName = "News" }},
            { "BlogPost", new StaticType() {TypeName = "Telerik.Sitefinity.Blogs.Model.BlogPost", ModuleName = "Blogs" } },
            { "Blog", new StaticType() {TypeName = "Telerik.Sitefinity.Blogs.Model.Blog", ModuleName = "Blogs" } },
            { "Image", new StaticType() {TypeName = "Telerik.Sitefinity.Libraries.Model.Image", ModuleName = "Libraries" } },
            { "Document", new StaticType() {TypeName = "Telerik.Sitefinity.Libraries.Model.Document", ModuleName = "Libraries" } },
            { "Video", new StaticType() {TypeName = "Telerik.Sitefinity.Libraries.Model.Video", ModuleName = "Libraries" } },
            { "ListItem", new StaticType() {TypeName = "Telerik.Sitefinity.Lists.Model.ListItem", ModuleName = "Lists" } },
        };

        public static IEnumerable<ContentTypeModel> GetContentTypes(string providerName = null)
        {
            var staticTypes = GetAllStaticTypes();
            var dynamicTypes = GetAllDynamicTypes(providerName);

            return staticTypes.Union(dynamicTypes);
        }

        private static IEnumerable<ContentTypeModel> GetAllStaticTypes()
        {
            return staticTypesMap
                .Where(t => ContentTypeExtensions.IsModuleInstalled(t.Value.ModuleName))
                .Select(i => new ContentTypeModel(Res.Get(typeof(FlatTaxonomyResources), i.Key), i.Value.TypeName));
        }

        private static IEnumerable<ContentTypeModel> GetAllDynamicTypes(string providerName = null)
        {
            var provider = ModuleBuilderManager.GetManager(providerName).Provider;

            return provider.GetDynamicModuleTypes()
                           .Select(t => new ContentTypeModel(t.DisplayName, String.Format("{0}.{1}", t.TypeNamespace, t.TypeName)));
        }

        private static bool IsModuleInstalled(string moduleName)
        {
            return SystemManager.ApplicationModules != null &&
                SystemManager.ApplicationModules.ContainsKey(moduleName) &&
                !(SystemManager.ApplicationModules[moduleName] is InactiveModule);
        }

        private class StaticType
        {
            public string ModuleName { get; set; }

            public string TypeName { get; set; }
        }
    }
}
