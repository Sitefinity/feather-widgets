using System;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// Common operations for Module2 dynamic module
    /// That contains only one content type (Item) with 2 fields - Title and related field, related to Module1 content type (Color)
    /// </summary>
    public class DynamicModuleModule2Operations
    {
        public void CreateItem(string title, string[] relatedColors)
        {
            var providerName = string.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type itemType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Module2.Item");
            DynamicContent itemItem = dynamicModuleManager.CreateDataItem(itemType);

            itemItem.SetValue("Title", title);

            DynamicModuleManager relatedColorManager = DynamicModuleManager.GetManager();
            var relatedColorType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Module1.Color");

            foreach (var relatedColor in relatedColors)
            {
                var relatedColorItem = relatedColorManager.GetDataItems(relatedColorType).Where("Title = \"" + relatedColor + "\"").First();

                if (relatedColorItem != null && relatedColorItem.Status == ContentLifecycleStatus.Master)
                {
                    itemItem.CreateRelation(relatedColorItem, "RelatedColor");
                    dynamicModuleManager.SaveChanges();
                }
            }

            itemItem.SetString("UrlName", Regex.Replace(title.ToLower(), urlNameCharsToReplace, urlNameReplaceString));
            itemItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            itemItem.SetValue("PublicationDate", DateTime.Now);
            itemItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            dynamicModuleManager.Lifecycle.Publish(itemItem);

            dynamicModuleManager.SaveChanges();
        }

        private static string urlNameCharsToReplace = @"[^\w\-\!\$\'\(\)\=\@\d_]+";
        private static string urlNameReplaceString = "-";
    }
}
