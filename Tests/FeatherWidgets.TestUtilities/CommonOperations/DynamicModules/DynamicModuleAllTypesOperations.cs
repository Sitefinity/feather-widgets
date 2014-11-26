using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    public class DynamicModuleAllTypesOperations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dynamicurl")]
        public void CreateFieldWithTitle(string title, string dynamicurl)
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = string.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type allTypes = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.AllTypesModule.Alltypes");
            Telerik.Sitefinity.DynamicModules.Model.DynamicContent allTypesItem = dynamicModuleManager.CreateDataItem(allTypes);

            // This is how values for the properties are set
            allTypesItem.SetValue("Title", title);

            Address address = new Address();
            allTypesItem.SetValue("Address", address);

            allTypesItem.SetString("UrlName", dynamicurl);
            allTypesItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            allTypesItem.SetValue("PublicationDate", DateTime.Now);
            allTypesItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            dynamicModuleManager.Lifecycle.Publish(allTypesItem);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<DynamicContent> RetrieveCollectionOfAllFields()
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = string.Empty;
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type pressArticleType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.AllTypesModule.Alltypes");

            // This is how we get the collection of Press Article items
            var myCollection = dynamicModuleManager.GetDataItems(pressArticleType).ToList();
            //// At this point myCollection contains the items from type pressArticleType
            return myCollection;
        }
    }
}
