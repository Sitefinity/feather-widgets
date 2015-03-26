using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Lists.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Content block widget
    /// </summary>
    [ObjectInfo(typeof(ListsResources), Title = "ListsResourcesTitle", Description = "ListsResourcesDescription")]
    public class ListsResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockResources"/> class. 
        /// Initializes new instance of <see cref="ContentBlockResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ListsResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentBlockResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public ListsResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion
    }
}