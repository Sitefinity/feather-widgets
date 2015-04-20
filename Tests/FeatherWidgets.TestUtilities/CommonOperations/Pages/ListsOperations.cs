using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Modules.Lists;
using Telerik.Sitefinity.Workflow;

namespace FeatherWidgets.TestUtilities.CommonOperations.Pages
{
    public class ListsOperations
    {
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