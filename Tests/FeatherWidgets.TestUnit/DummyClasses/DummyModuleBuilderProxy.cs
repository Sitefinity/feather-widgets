using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity;

namespace FeatherWidgets.TestUnit.DummyClasses
{
    /// <summary>
    /// This class creates dummy <see cref="IModuleBuilderProxy"/>
    /// </summary>
    public class DummyModuleBuilderProxy : IModuleBuilderProxy
    {
        /// <summary>
        /// Gets the child items.
        /// </summary>
        /// <param name="parentItem">The parent item.</param>
        /// <param name="childType">Type of the child.</param>
        /// <returns></returns>
        public IQueryable<Telerik.Sitefinity.DynamicModules.Model.DynamicContent> GetChildItems(Telerik.Sitefinity.DynamicModules.Model.DynamicContent parentItem, Type childType)
        {
            return null;
        }

        /// <summary>
        /// Gets the live item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public Telerik.Sitefinity.DynamicModules.Model.DynamicContent GetLiveItem(Telerik.Sitefinity.DynamicModules.Model.DynamicContent item)
        {
            return null;
        }
    }
}
