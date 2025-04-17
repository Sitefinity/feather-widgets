using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Helpers
{
    /// <summary>
    /// This class contains extension methods for the <see cref="ItemViewModel"/> class.
    /// </summary>
    public static class ItemViewModelExtensions
    {
        /// <summary>
        /// Gets the child items of the given item from the a specified field.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>An enumerable containing the view models of the child items.</returns>
        public static IEnumerable<ItemViewModel> ChildItems(this ItemViewModel item, string fieldName)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var propertyInfo = TypeDescriptor.GetProperties(item.DataItem)[fieldName];
            
            var successorDescriptor = propertyInfo as TypeSuccessorsPropertyDescriptor;
            if (successorDescriptor == null)
            {
                throw new InvalidOperationException("Could not find a child item property '{0}' for the given item. This extension method should only be used for accessing child items of a DynamicContent.".Arrange(fieldName));
            }

            var childItems = (IEnumerable<IDataItem>)item.Fields.GetMemberValue(fieldName);

            AddCacheDependenciesForChildItems(item, childItems);

            return childItems.ToArray().Select(d => new ItemViewModel(d));
        }

        /// <summary>
        /// Gets the parent of the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>View model of the parent item.</returns>
        public static ItemViewModel ParentItem(this ItemViewModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var parentItem = ItemViewModelExtensions.GetParentItem(item);

            AddCacheDependenciesForParentItem(item, parentItem);

            return new ItemViewModel(parentItem);
        }

        #region Private Methods

        private static IDataItem GetParentItem(ItemViewModel item)
        {
            var status = ((ILifecycleDataItem)item.DataItem).Status;

            if (status == ContentLifecycleStatus.Live)
            {
                return ItemViewModelExtensions.GetLiveParentItem(item);
            }
            else
            {
                return ItemViewModelExtensions.GetMasterParentItem(item);
            }
        }

        private static IDataItem GetLiveParentItem(ItemViewModel item)
        {
            var propertyInfo = TypeDescriptor.GetProperties(item.DataItem)["ParentItem"];

            var parentDescriptor = propertyInfo as DynamicContentParentPropertyDescriptor;
            if (parentDescriptor == null)
            {
                throw new InvalidOperationException("Could not find a parent item property for the given item. This extension method should only be used for accessing a parent item of DynamicContent items.");
            }

            var parentItem = (IDataItem)parentDescriptor.GetValue(item.DataItem);

            return parentItem;
        }

        private static IDataItem GetMasterParentItem(ItemViewModel item)
        {
            var propertyInfo = TypeDescriptor.GetProperties(item.DataItem)["SystemParentItem"];

            var parentDescriptor = propertyInfo as DataPropertyDescriptor;
            if (parentDescriptor == null)
            {
                throw new InvalidOperationException("Could not find a parent item property for the given item. This extension method should only be used for accessing a parent item of DynamicContent items.");
            }

            var parentItem = (IDataItem)item.Fields.SystemParentItem;

            return parentItem;
        }

        private static void AddCacheDependenciesForChildItems(ItemViewModel item, IEnumerable<IDataItem> childItems)
        {
            if(!childItems.Any())
            {
                return;
            }

            var itemType = childItems.First().GetType();
            var cacheDependencyKeys = new List<CacheDependencyKey>
            {
                new CacheDependencyKey { Key = itemType.FullName, Type = typeof(DynamicModule) }
            };

            var appName = "";
            var provider = childItems.First().Provider as DataProviderBase;
            if (provider != null)
            {
                appName = provider.ApplicationName;
            }

            cacheDependencyKeys.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(itemType, appName));

            AddCacheDependencyKeys(cacheDependencyKeys);
        }

        private static void AddCacheDependenciesForParentItem(ItemViewModel item, IDataItem parentItem)
        {
            if(parentItem == null)
            {
                return;
            }

            var itemType = parentItem.GetType();
            var cacheDependencyKeys = new List<CacheDependencyKey>
            {
                new CacheDependencyKey { Key = itemType.FullName, Type = typeof(DynamicModule) }
            };

            cacheDependencyKeys.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(itemType, parentItem.Id));

            AddCacheDependencyKeys(cacheDependencyKeys);
        }

        private static void AddCacheDependencyKeys(IEnumerable<CacheDependencyKey> cacheDependencyKeys)
        {
            SystemManager.CurrentHttpContext.AddCacheDependencies(cacheDependencyKeys);
        }

        #endregion
    }
}