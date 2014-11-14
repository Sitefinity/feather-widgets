using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    public class DynamicModulePressArticleOperations
    {       
        /// <summary>
        /// Overloaded method
        /// </summary>
        /// <param name="title">Dynamic item title</param>
        /// <param name="author">Dynamic author</param>
        /// <param name="dynamicValue">Dynamic guid</param>
        /// <param name="tag">Dynamic tag</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dynamicurl"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public void CreatePressArticle(string title, string dynamicurl, Guid tag)
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = string.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type pressArticleType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle");
            Telerik.Sitefinity.DynamicModules.Model.DynamicContent pressArticleItem = dynamicModuleManager.CreateDataItem(pressArticleType);

            // This is how values for the properties are set
            pressArticleItem.SetValue("Title", title);
            pressArticleItem.SetValue("PublishedBy", "Some PublishedBy");
            pressArticleItem.SetValue("Guid", Guid.NewGuid());

            if (tag != null)
            {
                pressArticleItem.Organizer.AddTaxa("Tags", tag);
            }

            pressArticleItem.SetString("UrlName", dynamicurl);
            pressArticleItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            pressArticleItem.SetValue("PublicationDate", DateTime.Now);
            pressArticleItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            dynamicModuleManager.Lifecycle.Publish(pressArticleItem);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }

        // Creates a new pressArticle item with predefined ID
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dynamicurl"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public DynamicContent CreatePressArticleItem(string title, string dynamicurl, Guid pressArticleId, Taxon tag = null)
        {
            var providerName = string.Empty;
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);

            Type pressArticleType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle");
           
            DynamicContent pressArticleItem = dynamicModuleManager.CreateDataItem(pressArticleType, pressArticleId, "/DynamicModule");

            // This is how values for the properties are set 
            pressArticleItem.SetValue("Title", title);
            pressArticleItem.SetValue("PublishedBy", "Some PublishedBy");
            pressArticleItem.SetValue("Guid", Guid.NewGuid());

            if (tag != null)
            {
                pressArticleItem.Organizer.AddTaxa("Tags", tag.Id);
            }

            pressArticleItem.SetString("UrlName", dynamicurl);
            pressArticleItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            pressArticleItem.SetValue("PublicationDate", DateTime.Now);
            pressArticleItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            dynamicModuleManager.Lifecycle.Publish(pressArticleItem);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();

            return pressArticleItem;
        }

        // Demonstrates how pressArticleItem is deleted
        public void DeletePressArticle(DynamicContent pressArticleItem)
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = string.Empty;
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);

            // This is how you delete the pressArticleItem
            dynamicModuleManager.DeleteDataItem(pressArticleItem);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<DynamicContent> RetrieveCollectionOfPressArticles()
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = string.Empty;
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type pressArticleType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle");

            // This is how we get the collection of Press Article items
            var myCollection = dynamicModuleManager.GetDataItems(pressArticleType).ToList();
            //// At this point myCollection contains the items from type pressArticleType
            return myCollection;
        }

        // Demonstrates how pressArticleItem is deleted
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public void DeletePressArticle(List<DynamicContent> itemsToDelete)
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = string.Empty;
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
           
            for (int i = 0; i < itemsToDelete.Count; i++)
            //// This is how you delete the pressArticleItem
                dynamicModuleManager.DeleteDataItem(itemsToDelete[i]);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public void PublishPressArticleWithSpecificDate(DynamicContent pressArticleItem, DateTime specificDate)
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = string.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            
            ////We can set a date and time in the future, for the item to be published
            dynamicModuleManager.Lifecycle.PublishWithSpecificDate(pressArticleItem, specificDate);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }
    }
}
