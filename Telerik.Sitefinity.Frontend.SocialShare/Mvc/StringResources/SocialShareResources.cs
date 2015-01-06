using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.StringResources
{
    [ObjectInfo(typeof(SocialShareResources), Title = "SocialShareResourcesTitle", Description = "SocialShareResourcesDescription")]
    public class SocialShareResources : Resource
    {
        #region Constructions

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareResources" /> class.
        /// </summary>
        public SocialShareResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareResources" /> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public SocialShareResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion
    }
}
