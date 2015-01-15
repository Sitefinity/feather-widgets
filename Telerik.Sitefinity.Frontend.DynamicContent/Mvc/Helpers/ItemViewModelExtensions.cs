using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Frontend.Mvc.Models;

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
                throw new ArgumentNullException("item");

            var propertyInfo = TypeDescriptor.GetProperties(item.DataItem)[fieldName];
            var successorDescriptor = propertyInfo as TypeSuccessorsPropertyDescriptor;
            if (successorDescriptor == null)
                throw new InvalidOperationException("Could not find a child item property '{0}' for the given item. This extension method should only be used for accessing child items of a DynamicContent.".Arrange(fieldName));

            return (IEnumerable<ItemViewModel>)item.Fields.GetMemberValue(fieldName);
        }

        /// <summary>
        /// Gets the parent of the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>View model of the parent item.</returns>
        public static ItemViewModel ParentItem(this ItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var propertyInfo = TypeDescriptor.GetProperties(item.DataItem)["SystemParentItem"];
            var parentDescriptor = propertyInfo as DataPropertyDescriptor;
            if (parentDescriptor == null)
                throw new InvalidOperationException("Could not find a parent item property for the given item. This extension method should only be used for accessing a parent item of DynamicContent items.");

            return (ItemViewModel)item.Fields.SystemParentItem;
        }
    }
}
