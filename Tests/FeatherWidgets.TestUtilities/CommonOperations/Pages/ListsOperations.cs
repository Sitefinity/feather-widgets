using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Modules.Lists;
using Telerik.Sitefinity.Workflow;

namespace FeatherWidgets.TestUtilities.CommonOperations.Pages
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
    }
}