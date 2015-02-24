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
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Fields;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    public class DynamicModulePressArticleOperations
    {
        /// <summary>
        /// Creates the press article.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="category">The category.</param>
        /// <param name="publishedBy">The publisher.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public void CreatePressArticle(string title, string url, Guid tag, Guid category, string publishedBy, string providerName)
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type pressArticleType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle");
            Telerik.Sitefinity.DynamicModules.Model.DynamicContent pressArticleItem = dynamicModuleManager.CreateDataItem(pressArticleType);

            // This is how values for the properties are set
            pressArticleItem.SetValue("Title", title);
            pressArticleItem.SetValue("Guid", Guid.NewGuid());

            if (publishedBy.IsNullOrEmpty())
            {
                pressArticleItem.SetValue("PublishedBy", "Some PublishedBy");
            }
            else
            {
                pressArticleItem.SetValue("PublishedBy", publishedBy);
            }

            if (tag != null && tag != Guid.Empty)
            {
                pressArticleItem.Organizer.AddTaxa("Tags", tag);
            }

            if (category != null && category != Guid.Empty)
            {
                pressArticleItem.Organizer.AddTaxa("Category", category);
            }

            pressArticleItem.SetString("UrlName", url);
            pressArticleItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            pressArticleItem.SetValue("PublicationDate", DateTime.Now);
            pressArticleItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            dynamicModuleManager.Lifecycle.Publish(pressArticleItem);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }

        /// <summary>
        /// Creates the press article.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="category">The category.</param>
        /// <param name="publishedBy">The publisher.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public void CreatePressArticle(string title, string url, Guid tag, Guid category, string publishedBy)
        {
            this.CreatePressArticle(title, url, tag, category, publishedBy, null);
        }

        /// <summary>
        /// Creates the press article.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="category">The category.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public void CreatePressArticle(string title, string url, Guid tag, Guid category)
        {
            this.CreatePressArticle(title, url, tag, category, null);
        }

        /// <summary>
        /// Creates the press article.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        /// <param name="publishedBy">The publisher.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public void CreatePressArticle(string title, string url, string publishedBy)
        {
            this.CreatePressArticle(title, url, Guid.Empty, Guid.Empty, publishedBy);
        }

        /// <summary>
        /// Creates the press article.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public void CreatePressArticle(string title, string url)
        {
            this.CreatePressArticle(title, url, Guid.Empty, Guid.Empty, null);
        }

        /// <summary>
        /// Creates the press article with custom taxonomy.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The URL.</param>
        /// <param name="taxonomyName">Name of the taxonomy.</param>
        /// <param name="taxonNames">The taxon names.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public void CreatePressArticleWithCustomTaxonomy(string title, string url, string taxonomyName, IEnumerable<string> taxonNames)
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

            pressArticleItem.SetString("UrlName", url);
            pressArticleItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            pressArticleItem.SetValue("PublicationDate", DateTime.Now);
            pressArticleItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            dynamicModuleManager.Lifecycle.Publish(pressArticleItem);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }
 
        /// <summary>
        /// Creates a new pressArticle item with predefined ID
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="url">The url.</param>
        /// <returns>The press article item.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#")]
        public DynamicContent CreatePressArticleItem(string title, string url)
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

            pressArticleItem.SetString("UrlName", url);
            pressArticleItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            pressArticleItem.SetValue("PublicationDate", DateTime.Now);
            pressArticleItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            dynamicModuleManager.Lifecycle.Publish(pressArticleItem);

            dynamicModuleManager.RecompileDataItemsUrls(pressArticleType);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();

            return pressArticleItem;
        }

        /// <summary>
        /// Demonstrates how pressArticleItem is unpublished
        /// </summary>
        /// <param name="pressArticleItem">The press article item.</param>
        public void UNPublishPressArticle(ILifecycleDataItem pressArticleItem)
        {
            var providerName = string.Empty;
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            var liveDynamicItem = dynamicModuleManager.Lifecycle.GetLive(pressArticleItem);
            dynamicModuleManager.Lifecycle.Unpublish(liveDynamicItem);
            dynamicModuleManager.SaveChanges();
        }
        
        /// <summary>
        /// Retrieves collection of press articles.
        /// </summary>
        /// <returns>The collection of press articles.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<DynamicContent> RetrieveCollectionOfPressArticles()
        {
            return this.RetrieveCollectionOfPressArticles(null);
        }

        /// <summary>
        /// Retrieves collection of press articles.
        /// </summary>
        /// <param name="providerName">The provider name.</param>
        /// <returns>The collection of press articles.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public List<DynamicContent> RetrieveCollectionOfPressArticles(string providerName)
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type pressArticleType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle");

            // This is how we get the collection of Press Article items
            var myCollection = dynamicModuleManager.GetDataItems(pressArticleType).ToList();
            //// At this point myCollection contains the items from type pressArticleType
            return myCollection;
        }

        /// <summary>
        /// Demonstrates how pressArticleItem is deleted
        /// </summary>
        /// <param name="itemsToDelete">Collection of items to delete.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public void DeleteDynamicItems(List<DynamicContent> itemsToDelete)
        {
            this.DeleteDynamicItems(itemsToDelete, null);
        }

        /// <summary>
        /// Demonstrates how pressArticleItem is deleted
        /// </summary>
        /// <param name="itemsToDelete">Collection of items to delete.</param>
        /// <param name="providerName">The provider name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public void DeleteDynamicItems(List<DynamicContent> itemsToDelete, string providerName)
        {
            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);

            for (int i = 0; i < itemsToDelete.Count; i++)
            //// This is how you delete the pressArticleItem
                dynamicModuleManager.DeleteDataItem(itemsToDelete[i]);

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();
        }

        /// <summary>
        /// Publish a press article with specific date.
        /// </summary>
        /// <param name="pressArticleItem">The press article item.</param>
        /// <param name="specificDate">The date.</param>
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

        /// <summary>
        /// Edits a press article title.
        /// </summary>
        /// <param name="pressArticleItem">The press article item.</param>
        /// <param name="newTitle">The new title.</param>
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
        /// Adds a custom field to Press article
        /// </summary>
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

                // Field definition
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

        /// <summary>
        /// Removes a custom field from the Press article
        /// </summary>
        /// <param name="fieldname">custom field name to be removed</param>
        public void RemoveCustomFieldFromContext(string fieldname)
        {
            Type pressArticleType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle");
            if (pressArticleType == null)
            {
                throw new ArgumentException("PressArticle type can't be resolved.");
            }

            var context = new CustomFieldsContext(pressArticleType);

            context.RemoveCustomFields(new string[] { fieldname }, pressArticleType.Name);
            context.SaveChanges();

            SystemManager.RestartApplication(false);
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
