using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;

namespace FeatherWidgets.TestUnit.DummyClasses
{
    /// <summary>
    /// This class creates dummy <see cref="IRelatedDataResolver"/>
    /// </summary>
    public class DummyRelatedDataResolver : IRelatedDataResolver
    {
        /// <summary>
        /// Gets the related item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="childItemType">Type of the child item.</param>
        /// <returns></returns>
        public object GetRelatedItem(Telerik.Sitefinity.Model.IDataItem item, string fieldName, Type childItemType)
        {
            return null;
        }

        /// <summary>
        /// Gets the related items.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="childItemType">Type of the child item.</param>
        /// <returns></returns>
        public IList<Telerik.Sitefinity.Model.IDataItem> GetRelatedItems(Telerik.Sitefinity.Model.IDataItem item, string fieldName, Type childItemType)
        {
            return null;
        }
    }
}
