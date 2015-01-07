using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Fields;

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
        public void CreatePressArticle(string title, string dynamicurl, Guid tag, Guid category)
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

            if (tag != null && tag != Guid.Empty)
            {
                pressArticleItem.Organizer.AddTaxa("Tags", tag);
            }

            if (category != null && category != Guid.Empty)
            {
                pressArticleItem.Organizer.AddTaxa("Category", category);
            }

            pressArticleItem.SetString("UrlName", dynamicurl);
            pressArticleItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            pressArticleItem.SetValue("PublicationDate", DateTime.Now);
            pressArticleItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            dynamicModuleManager.Lifecycle.Publish(pressArticleItem);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }

        /// <summary>
        /// Creates the press article with custom taxonomy.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="dynamicurl">The dynamicurl.</param>
        /// <param name="taxonomyName">Name of the taxonomy.</param>
        /// <param name="taxonNames">The taxon names.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "dynamicurl"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public void CreatePressArticleWithCustomTaxonomy(string title, string dynamicurl, string taxonomyName, IEnumerable<string> taxonNames)
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

            this.AddCustomTaxonomy(taxonNames, taxonomyName, pressArticleItem);

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
        public DynamicContent CreatePressArticleItem(string title, string dynamicurl)
        {
            var providerName = string.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type pressArticleType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle");
            Telerik.Sitefinity.DynamicModules.Model.DynamicContent pressArticleItem = dynamicModuleManager.CreateDataItem(pressArticleType);

            // This is how values for the properties are set 
            pressArticleItem.SetValue("Title", title);
            pressArticleItem.SetValue("PublishedBy", "Some PublishedBy");
            pressArticleItem.SetValue("Guid", Guid.NewGuid());

            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            var tag = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Taxonomy.Name == "Tags").FirstOrDefault();
            if (tag != null)
            {
                pressArticleItem.Organizer.AddTaxa("Tags", tag.Id);
            }

            pressArticleItem.SetString("UrlName", dynamicurl);
            pressArticleItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            pressArticleItem.SetValue("PublicationDate", DateTime.Now);
            pressArticleItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            dynamicModuleManager.Lifecycle.Publish(pressArticleItem);

            dynamicModuleManager.RecompileDataItemsUrls(pressArticleType);

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

        // Demonstrates how pressArticleItem is unpublished
        public void UNPublishPressArticle(ILifecycleDataItem pressArticleItem)
        {
            var providerName = string.Empty;
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            var liveDynamicItem = dynamicModuleManager.Lifecycle.GetLive(pressArticleItem);
            dynamicModuleManager.Lifecycle.Unpublish(liveDynamicItem);
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
        public void DeleteDynamicItems(List<DynamicContent> itemsToDelete)
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public void EditPressArticleTitle(DynamicContent pressArticleItem, string newTitle)
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            // Administration -> Settings -> Advanced -> DynamicModules -> Providers
            var providerName = string.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);

            pressArticleItem.SetValue("Title", newTitle);

            dynamicModuleManager.Lifecycle.Publish(pressArticleItem);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }

        /// <summary>
        /// Adds a custom field to the default content types (like news, blogs and so on)
        /// </summary>
        /// <param name="contentTypeFullName">Content type full name</param>
        /// <param name="fieldname">Name of the field</param>
        /// <param name="isHierarchicalTaxonomy">is hierarchical taxonomy</param>
        public void AddCustomTaxonomyToContext(string fieldname, bool isHierarchicalTaxonomy)
        {
            Type pressArticleType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle");
            if (pressArticleType == null)
            {
                throw new ArgumentException("PressArticle type can't be resolved.");
            }

            var context = new CustomFieldsContext(pressArticleType);

            TaxonomyManager manager = TaxonomyManager.GetManager();
            Guid taxonomyId;
            if (isHierarchicalTaxonomy == true)
            {
                var taxonomy = manager.GetTaxonomies<HierarchicalTaxonomy>().Where(t => t.Title == fieldname).SingleOrDefault();
                if (taxonomy != null)
                {
                    taxonomyId = taxonomy.Id;
                }
                else
                {
                    throw new ArgumentException("The taxonomy '" + fieldname + "' does not exist in the system");
                }
            }
            else
            {
                var taxonomy = manager.GetTaxonomies<FlatTaxonomy>().Where(t => t.Title == fieldname).SingleOrDefault();
                if (taxonomy != null)
                {
                    taxonomyId = taxonomy.Id;
                }
                else
                {
                    throw new ArgumentException("The taxonomy '" + fieldname + "' does not exist in the system");
                }
            }

            UserFriendlyDataType userFriendlyDataType = UserFriendlyDataType.Classification;
            var field = new WcfField()
            {
                Name = fieldname,
                ContentType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle",
                FieldTypeKey = userFriendlyDataType.ToString(),
                IsCustom = true,

                ////Field definition
                Definition = new WcfFieldDefinition()
                {
                    Title = fieldname,
                    FieldName = fieldname.ToLower(),
                    FieldType = isHierarchicalTaxonomy ? typeof(HierarchicalTaxonField).FullName : typeof(FlatTaxonField).FullName,
                    TaxonomyId = taxonomyId.ToString(),
                    AllowMultipleSelection = true,
                }
            };

            var fields = new Dictionary<string, WcfField>();
            fields.Add(field.FieldTypeKey, field);

            context.AddOrUpdateCustomFields(fields, pressArticleType.Name);
            context.SaveChanges();
        }

        private void AddCustomTaxonomy(IEnumerable<string> taxonNames, string taxonomyName, DynamicContent pressArticleItem)
        {
            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            foreach (var taxonName in taxonNames)
            {
                var taxon = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Title == taxonName).FirstOrDefault();
                if (taxon != null)
                {
                    pressArticleItem.Organizer.AddTaxa(taxonomyName, taxon.Id);
                }
            }
        }
    }
}
