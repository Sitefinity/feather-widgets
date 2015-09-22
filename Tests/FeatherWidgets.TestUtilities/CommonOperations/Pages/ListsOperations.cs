﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Modules.Lists;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.CommonOperations.Multilingual;
using Telerik.Sitefinity.TestUtilities.TestConfig;
using Telerik.Sitefinity.Workflow;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// This class provides access to lists common server operations
    /// </summary>
    public class ListsOperations
    {
        /// <summary>
        /// Creates list
        /// </summary>
        /// <param name="listId">list id</param>
        /// <param name="title">list title</param>
        /// <param name="description">list description</param>
        public void CreateList(Guid listId, string title, string description)
        {
            ListsManager listsManager = ListsManager.GetManager();

            List list = listsManager.CreateList(listId);

            list.Title = title;
            list.Description = description;
            list.UrlName = Regex.Replace(title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
            list.DateCreated = DateTime.Now;

            listsManager.RecompileAndValidateUrls(list);

            listsManager.SaveChanges();
        }

        /// <summary>
        /// Deletes list by given id
        /// </summary>
        /// <param name="listId">list id</param>
        public void DeleteList(Guid listId)
        {
            ListsManager listsManager = ListsManager.GetManager();

            List list = listsManager.GetLists().Where(l => l.Id == listId).FirstOrDefault();

            if (list != null)
            {
                listsManager.DeleteList(list);

                listsManager.SaveChanges();
            }
        }

        /// <summary>
        /// Creates list list item for given list
        /// </summary>
        /// <param name="listItemId">list item id</param>
        /// <param name="parentListId">list id</param>
        /// <param name="title">list item title</param>
        /// <param name="content">list item content</param>
        public void CreateListItem(Guid listItemId, Guid parentListId, string title, string content)
        {
            ListsManager listManager = ListsManager.GetManager();

            List parent = listManager.GetLists().Where(l => l.Id == parentListId).FirstOrDefault();

            if (parent != null)
            {
                ListItem listItem = listManager.CreateListItem(listItemId);

                listItem.Parent = parent;

                listItem.Title = title;
                listItem.Content = content;
                listItem.UrlName = Regex.Replace(title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                listItem.DateCreated = DateTime.Now;

                listManager.RecompileAndValidateUrls(listItem);

                listManager.SaveChanges();

                var bag = new Dictionary<string, string>();
                bag.Add("ContentType", typeof(ListItem).FullName);
                WorkflowManager.MessageWorkflow(listItemId, typeof(ListItem), null, "Publish", false, bag);
            }
        }

        /// <summary>
        /// Deletes list item by given id
        /// </summary>
        /// <param name="masterListItemId">list item id</param>
        public void DeleteListItem(Guid masterListItemId)
        {
            ListsManager listsManager = ListsManager.GetManager();

            ListItem listItem = listsManager.GetListItems().Where(i => i.Id == masterListItemId).FirstOrDefault();

            if (listItem != null)
            {
                listsManager.DeleteListItem(listItem);

                listsManager.SaveChanges();
            }
        }

        /// <summary>
        /// Edit title and content of list item.
        /// </summary>
        /// <param name="listItemId">The list item id.</param>
        /// <param name="newTitle">The list item title.</param>
        /// <param name="newContent">The list item content.</param>
        public void EditListItem(Guid listItemId, string newTitle, string newContent)
        {
            ListsManager listManager = ListsManager.GetManager();

            ListItem listItemMaster = listManager.GetListItems().Where(i => i.Id == listItemId).FirstOrDefault();

            if (listItemMaster == null)
            {
                throw new ItemNotFoundException(string.Format(CultureInfo.CurrentCulture, "List item with id {0} was not found.", listItemId));
            }

            ListItem listItemTemp = listManager.Lifecycle.CheckOut(listItemMaster) as ListItem;

            listItemTemp.Title = newTitle;
            listItemTemp.Content = newContent;

            listItemMaster = listManager.Lifecycle.CheckIn(listItemTemp) as ListItem;
            listManager.Lifecycle.Publish(listItemMaster);
            listManager.SaveChanges();
        }

        /// <summary>
        /// Publish list item with specific date.
        /// </summary>
        /// <param name="listItemId">The list item id.</param>
        /// <param name="publicationDate">Publication datetime.</param>
        public void PublishListItemWithSpecificDate(Guid listItemId, DateTime publicationDate)
        {
            ListsManager listManager = ListsManager.GetManager();

            ListItem listItemMaster = listManager.GetListItems().Where(i => i.Id == listItemId).FirstOrDefault();

            if (listItemMaster == null)
            {
                throw new ItemNotFoundException(string.Format(CultureInfo.CurrentCulture, "List item with id {0} was not found.", listItemId));
            }

            listManager.Lifecycle.PublishWithSpecificDate(listItemMaster, publicationDate);
            listManager.SaveChanges();
        }

        /// <summary>
        /// Adds the taxonomies to list item.
        /// </summary>
        /// <param name="listItemId">The list item id.</param>
        /// <param name="categories">The categories.</param>
        /// <param name="tags">The tags.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        public void AddTaxonomiesToListItem(Guid listItemId, IEnumerable<string> categories, IEnumerable<string> tags)
        {
            ListsManager listManager = ListsManager.GetManager();

            ListItem listItemMaster = listManager.GetListItems().Where(i => i.Id == listItemId).FirstOrDefault();

            if (listItemMaster == null)
            {
                throw new ItemNotFoundException(string.Format(CultureInfo.CurrentCulture, "List item with id {0} was not found.", listItemId));
            }

            ListItem listItemTemp = listManager.Lifecycle.CheckOut(listItemMaster) as ListItem;

            var taxonomyManager = TaxonomyManager.GetManager();
            if (categories != null)
            {
                if (categories.Count() > 0)
                {
                    HierarchicalTaxon category = null;
                    foreach (var c in categories)
                    {
                        category = taxonomyManager.GetTaxa<HierarchicalTaxon>().Single(t => t.Title == c);
                        listItemTemp.Organizer.AddTaxa("Category", category.Id);
                    }
                }
            }

            if (tags != null)
            {
                if (tags.Count() > 0)
                {
                    FlatTaxon tag = null;
                    foreach (var tg in tags)
                    {
                        tag = taxonomyManager.GetTaxa<FlatTaxon>().Single(t => t.Title == tg);
                        listItemTemp.Organizer.AddTaxa("Tags", tag.Id);
                    }
                }
            }

            listItemMaster = listManager.Lifecycle.CheckIn(listItemTemp) as ListItem;
            listManager.Lifecycle.Publish(listItemMaster);
            listManager.SaveChanges();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public string GetCurrentCulture()
        {
            var culture = string.Empty;

            if (ServerOperations.Multilingual().IsCurrentSiteInMultilingual)
            {
                culture = Thread.CurrentThread.CurrentUICulture.Name;
            }
            else
            {
                culture = SystemManager.CurrentContext.CurrentSite.DefaultCulture;
            }

            return culture;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public Guid CreateListItemMultilingual(MultilingualTestConfig config, Guid listId, string listItemTitle, string listItemContent, bool saveAsDraft = false, string culture = null, string provider = null)
        {
            var listItemId = this.CreateListItem(listId, listItemTitle, listItemContent, provider);

            var createdListItem = App.Prepare().SetContentProvider(provider).WorkWith().ListItems().Get().FirstOrDefault(l => l.Id == listItemId);
            if (createdListItem != null)
            {
                Guid currentUserId = SecurityManager.GetCurrentUserId();

                if (string.IsNullOrWhiteSpace(culture) || culture == config.CultureInfo)
                {
                    ServerOperations.Multilingual().Lists().CreateLocalizedListItem(listItemId, listId, listItemTitle, listItemContent, currentUserId, config.CultureInfo, saveAsDraft, provider);
                }

                if (string.IsNullOrWhiteSpace(culture) || culture == config.BgCultureInfo)
                {
                    ServerOperations.Multilingual().Lists().CreateLocalizedListItem(listItemId, listId, listItemTitle, listItemContent, currentUserId, config.BgCultureInfo, saveAsDraft, provider);
                }
            }

            return listItemId;
        }

        private Guid CreateListItem(Guid listId, string listItemTitle, string listItemContent, string provider = null)
        {
            using (var fluent = App.Prepare().SetContentProvider(provider).WorkWith())
            {
                Guid itemId = Guid.Empty;
                fluent.List(listId).CreateListItem().Do(listItem =>
                {
                    itemId = listItem.Id;
                    listItem.DateCreated = DateTime.UtcNow;
                    listItem.PublicationDate = DateTime.UtcNow;
                    listItem.Title = listItemTitle;
                    listItem.Content = listItemContent;
                    ////listItem.Urls.Clear(); - Because of bug in Fluent API
                    listItem.Urls.Clear();
                    listItem.UrlName = Regex.Replace(listItemTitle.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                    listItem.ApprovalWorkflowState = "Draft";
                }).SaveChanges();
                return itemId;
            }
        }
    }
}