using System;
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
        // Creates a new pressArticle item
        public void CreatePressArticle()
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = string.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type pressArticleType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle");
            Telerik.Sitefinity.DynamicModules.Model.DynamicContent pressArticleItem = dynamicModuleManager.CreateDataItem(pressArticleType);

            // This is how values for the properties are set
            pressArticleItem.SetValue("Title", "Some Title");
            pressArticleItem.SetValue("PublishedBy", "Some PublishedBy");
            pressArticleItem.SetValue("Guid", Guid.NewGuid());
            LibrariesManager libraryManager = LibrariesManager.GetManager();
            var image = libraryManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live);
           
            if (image != null)
            {
                pressArticleItem.AddImage("Image", image.Id);
            }

            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            var tag = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Taxonomy.Name == "Tags").FirstOrDefault();

            if (tag != null)
            {
                pressArticleItem.Organizer.AddTaxa("Tags", tag.Id);
            }

            pressArticleItem.SetString("UrlName", "SomeUrlName");
            pressArticleItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            pressArticleItem.SetValue("PublicationDate", DateTime.Now);
            pressArticleItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }

        /// <summary>
        /// Overloaded method
        /// </summary>
        /// <param name="title">Dynamic item title</param>
        /// <param name="author">Dynamic author</param>
        /// <param name="dynamicValue">Dynamic guid</param>
        /// <param name="tag">Dynamic tag</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public void CreatePressArticle(string title, Guid dynamicValue, Taxon tag = null)
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
            pressArticleItem.SetValue("Guid", dynamicValue);

            if (tag != null)
            {
                pressArticleItem.Organizer.AddTaxa("Tags", tag.Id);
            }

            pressArticleItem.SetString("UrlName", "SomeUrlName");
            pressArticleItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            pressArticleItem.SetValue("PublicationDate", DateTime.Now);
            pressArticleItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
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

        // Demonstrates how Press Article content item can be retrieved by ID
        public DynamicContent RetrievePressArticleById()
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = string.Empty;
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type pressArticleType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle");
            Guid pressArticleID = new Guid("228c2dee-3640-43f2-881c-43992da8e055");
            this.CreatePressArticleItem(dynamicModuleManager, pressArticleType, pressArticleID, "/DynamicModule");

            // This is how we get the pressArticle item by ID
            DynamicContent pressArticleItem = dynamicModuleManager.GetDataItem(pressArticleType, pressArticleID);
            return pressArticleItem;
        }

        ////// Creates and returns a new pressArticle item
        ////private DynamicContent CreatePressArticleItem(DynamicModuleManager dynamicModuleManager, Type pressArticleType)
        ////{
        ////    DynamicContent pressArticleItem = dynamicModuleManager.CreateDataItem(pressArticleType);

        ////    // This is how values for the properties are set 
        ////    pressArticleItem.SetValue("Title", "Some Title");
        ////    pressArticleItem.SetValue("PublishedBy", "Some PublishedBy");
        ////    pressArticleItem.SetValue("Guid", Guid.NewGuid());
        ////    LibrariesManager libraryManager = LibrariesManager.GetManager();
        ////    var image = libraryManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live);
            
        ////    if (image != null)
        ////    {
        ////        pressArticleItem.AddImage("Image", image.Id);
        ////    }

        ////    TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
        ////    var tag = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Taxonomy.Name == "Tags").FirstOrDefault();
            
        ////    if (tag != null)
        ////    {
        ////        pressArticleItem.Organizer.AddTaxa("Tags", tag.Id);
        ////    }

        ////    pressArticleItem.SetString("UrlName", "SomeUrlName");
        ////    pressArticleItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
        ////    pressArticleItem.SetValue("PublicationDate", DateTime.Now);
        ////    pressArticleItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

        ////    // You need to call SaveChanges() in order for the items to be actually persisted to data store
        ////    dynamicModuleManager.SaveChanges();

        ////    return pressArticleItem;
        ////}

        // Creates a new pressArticle item with predefined ID
        private void CreatePressArticleItem(DynamicModuleManager dynamicModuleManager, Type pressArticleType, Guid pressArticleID, string applicationName)
        {
            DynamicContent pressArticleItem = dynamicModuleManager.CreateDataItem(pressArticleType, pressArticleID, applicationName);

            // This is how values for the properties are set 
            pressArticleItem.SetValue("Title", "Some Title");
            pressArticleItem.SetValue("PublishedBy", "Some PublishedBy");
            pressArticleItem.SetValue("Guid", Guid.NewGuid());
            LibrariesManager libraryManager = LibrariesManager.GetManager();
            var image = libraryManager.GetImages().FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live);
           
            if (image != null)
            {
                pressArticleItem.AddImage("Image", image.Id);
            }

            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            var tag = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Taxonomy.Name == "Tags").FirstOrDefault();
           
            if (tag != null)
            {
                pressArticleItem.Organizer.AddTaxa("Tags", tag.Id);
            }

            pressArticleItem.SetString("UrlName", "SomeUrlName");
            pressArticleItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            pressArticleItem.SetValue("PublicationDate", DateTime.Now);
            pressArticleItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Draft");

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }
    }
}
