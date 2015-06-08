using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Publishing.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Feed widget
    /// </summary>
    [ObjectInfo(typeof(FeedResources), ResourceClassId = "FeedResources", Title = "FeedResourcesTitle", Description = "FeedResourcesDescription")]
    public class FeedResources : Resource
    {
        #region Constructions
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedResources"/> class.
        /// </summary>
        public FeedResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public FeedResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        /// <summary>
        /// Gets Title for the Feed widget resources class.
        /// </summary>
        [ResourceEntry("FeedResourcesTitle",
            Value = "Feed widget resources",
            Description = "Title for the Feed widget resources class.",
            LastModified = "2015/06/08")]
        public string FeedResourcesTitle
        {
            get
            {
                return this["FeedResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the Feed widget resources class.
        /// </summary>
        [ResourceEntry("FeedResourcesDescription",
            Value = "Localizable strings for the Feed widget.",
            Description = "Description for the Feed widget resources class.",
            LastModified = "2015/06/08")]
        public string FeedResourcesDescription
        {
            get
            {
                return this["FeedResourcesDescription"];
            }
        }

        /// <summary>
        /// Phrase: Select a feed
        /// </summary>
        /// <value>Select a feed</value>
        [ResourceEntry("SelectFeed",
            Value = "Select a feed",
            Description = "Phrase: Select a feed",
            LastModified = "2015/06/08")]
        public string SelectFeed
        {
            get
            {
                return this["SelectFeed"];
            }
        }
    }
}
