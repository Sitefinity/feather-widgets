using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Helpers
{
    public static class ContentTypeExtensions
    {
        private static readonly IDictionary<string, string> staticTypesMap = new Dictionary<string, string>
        {
            { "NewsItem", "Telerik.Sitefinity.News.Model.NewsItem" },
            { "BlogPost", "Telerik.Sitefinity.Blogs.Model.BlogPost" },
            { "Blog", "Telerik.Sitefinity.Blogs.Model.Blog" },
            { "Image", "Telerik.Sitefinity.Libraries.Model.Image" },
            { "Document", "Telerik.Sitefinity.Libraries.Model.Document" },
            { "Video", "Telerik.Sitefinity.Libraries.Model.Video" },
            { "ListItem", "Telerik.Sitefinity.Lists.Model.ListItem" },
        };

        public static IEnumerable<ContentTypeModel> GetContentTypes(string providerName = null)
        {
            var staticTypes = GetAllStaticTypes();
            var dynamicTypes = GetAllDynamicTypes(providerName);

            return staticTypes.Union(dynamicTypes);
        }

        private static IEnumerable<ContentTypeModel> GetAllStaticTypes()
        {
            return staticTypesMap.Select(i => new ContentTypeModel(Res.Get(typeof(FlatTaxonomyResources), i.Key), i.Value));
        }

        private static IEnumerable<ContentTypeModel> GetAllDynamicTypes(string providerName = null)
        {
            var provider = ModuleBuilderManager.GetManager(providerName).Provider;

            return provider.GetDynamicModuleTypes()
                           .Select(t => new ContentTypeModel(t.DisplayName, String.Format("{0}.{1}", t.TypeNamespace, t.TypeName)));
        }
    }
}
