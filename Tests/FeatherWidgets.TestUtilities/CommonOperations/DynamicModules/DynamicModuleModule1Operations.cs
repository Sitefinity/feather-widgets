using System;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// Common operations for Module1 dynamic module
    /// That contains only one content type (Color) with one field - Title
    /// </summary>
    public class DynamicModuleModule1Operations
    {
        /// <summary>
        /// Creates a color.
        /// </summary>
        /// <param name="color">The color name.</param>
        /// <returns>The color id.</returns>
        public void CreateColor(string color)
        {
            var providerName = string.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type colorType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Module1.Color");
            DynamicContent colorItem = dynamicModuleManager.CreateDataItem(colorType);

            colorItem.SetValue("Title", color);

            colorItem.SetString("UrlName", Regex.Replace(color.ToLower(), urlNameCharsToReplace, urlNameReplaceString));
            colorItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            colorItem.SetValue("PublicationDate", DateTime.Now);
            colorItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            dynamicModuleManager.Lifecycle.Publish(colorItem);

            dynamicModuleManager.SaveChanges();
        }

        private static string urlNameCharsToReplace = @"[^\w\-\!\$\'\(\)\=\@\d_]+";
        private static string urlNameReplaceString = "-";
    }
}
